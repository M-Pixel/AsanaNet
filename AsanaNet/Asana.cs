using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Web;
using System.Threading;
using System.IO;
using System.Xml;
using System.Threading.Tasks;

using MiniJSON;

namespace AsanaNet
{
    public delegate void AsanaResponseEventHandler(AsanaObject response);
//    public delegate void AsanaCollectionResponseEventHandler(IAsanaObjectCollection response);

	public enum AuthenticationType
	{
		Basic,
		OAuth
	}

    [Serializable]
    public partial class Asana
    {        
        #region Variables

        /// <summary>
        /// The URL we use to prefix all the requests
        /// e.g. https://app.asana.com/api/1.0
        /// </summary>
        private string _baseUrl;

        /// <summary>
        /// An error callback for the outside world
        /// </summary>
        private Action<string, string, string, string> _errorCallback;

        internal readonly HttpClient _baseHttpClient = new HttpClient();

        internal ICache _objectCache;

        public AsanaCacheLevel DefaultCacheLevel = AsanaCacheLevel.UseExisting;

        #endregion

        #region Properties

		/// <summary>
		/// The Authentication Type used for API access
		/// </summary>
		public AuthenticationType AuthType { get; private set; }

        /// <summary>
        /// The API Key assigned object
        /// </summary>
        public string APIKey { get; private set; }

        /// <summary>
        /// The API Key, but base-64 encoded
        /// </summary>
        public string EncodedAPIKey { get; private set; }

		/// <summary>
		/// The OAuth Bearer Token assigned object
		/// </summary>
		public string OAuthToken { get; set; }

        public bool Sync = true;

        #endregion        

        #region Methods

        /// <summary>
        /// Creates a new Asana entry point.
        /// </summary>
		/// <param name="apiKeyOrBearerToken">The API key (for Basic authentication) or Bearer Token (for OAuth authentication) for the account we intend to access</param>
		public Asana(string apiKeyOrBearerToken, AuthenticationType authType, Action<string, string, string, string> errorCallback, ICache cache = null, AsanaCacheLevel defaultCacheLevel = AsanaCacheLevel.UseExisting)
        {   
            _baseUrl = "https://app.asana.com/api/1.0";
            _errorCallback = errorCallback;
            _objectCache = cache ?? new MemCache(Guid.NewGuid() + "/");
            DefaultCacheLevel = defaultCacheLevel;

			AuthType = authType;
			if (AuthType == AuthenticationType.OAuth) {
				OAuthToken = apiKeyOrBearerToken;
			} else {
				APIKey = apiKeyOrBearerToken;
				EncodedAPIKey = Convert.ToBase64String (System.Text.Encoding.ASCII.GetBytes(apiKeyOrBearerToken + ":"));
			}

            var defaultAuth = new System.Net.Http.Headers.AuthenticationHeaderValue(AuthType == AuthenticationType.OAuth ? "Bearer" : "Basic", AuthType == AuthenticationType.OAuth ? OAuthToken : EncodedAPIKey);
            _baseHttpClient.DefaultRequestHeaders.Authorization = defaultAuth;
			_baseHttpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AsanaNet", "1.1-async"));
            _baseHttpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            AsanaFunction.InitFunctions();
        }

        private Uri GetBaseUri(AsanaFunction function, params object[] obj)
        {
            return new Uri(_baseUrl + GetAsanaPartUri(function, obj));
        }

        private Uri GetBaseUriWithParams(AsanaFunction function, Dictionary<string, object> args, params object[] obj)
        {
            var uri = _baseUrl + GetAsanaPartUri(function, obj);
            if (!ReferenceEquals(args, null) && args.Count > 0)
            {
                uri += uri.Contains("?") ? "&" : "?";
                foreach (var kv in args)
                    uri += kv.Key + "=" + Uri.EscapeUriString(kv.Value.ToString()) + "&";
                uri = uri.TrimEnd('&');
            }
            return new Uri(uri);
        }
        internal static string GetAsanaPartUri(AsanaFunction function, params object[] obj)
        {
            return string.Format(new PropertyFormatProvider(), function.Url, obj);
        }

        /// <summary>
        /// Converts the raw string into dictionary format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        // was private 
        internal static Dictionary<string, object> GetDataAsDict(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;

            Dictionary<string, object> data2;

            if (data.ContainsKey("sync"))
                data2 = data;
            else
            {
                if (data.ContainsKey("data"))
                    data2 = data["data"] as Dictionary<string, object>;
                else if (data.ContainsKey("errors"))
                {
                    data2 = (data["errors"] as List<object>)[0] as Dictionary<string, object>;
                    data2.Add("errors", true);
                }
                else data2 = data;
            }

            // handle syncing:
            if (data2.ContainsKey("data") && data2.ContainsKey("sync") && (data2["data"] as List<object>).Any() && (((data2["data"] as List<object>)[0]) as Dictionary<string, object>).ContainsKey("resource"))
            {
                foreach (Dictionary<string, object> obj in data2["data"] as List<object>)
                {
                    string[] split = (obj["type"] as string).Split('.');
                    obj.Add("action", split[1]);
                    obj["type"] = split[0];
                    if (split[0] == "project")
                    {
                        // hack - until they fix it
                        if (!((Dictionary<string, object>)obj["resource"]).ContainsKey("team") || !((Dictionary<string, object>)obj["resource"]).ContainsKey("archived"))
                            obj["type"] = "tag";
                    }

                    /*
                    switch (split[0])
                    {
                        case "project":
                            obj["type"] = typeof (AsanaProject);
                            break;
                        case "task":
                            obj["type"] = typeof (AsanaTask);
                            break;
                        case "workspace":
                            obj["type"] = typeof (AsanaWorkspace);
                            break;
                        case "story":
                            obj["type"] = typeof (AsanaStory);
                            break;
                    }
                     * */
                }

                var list = (from x in (data2["data"] as List<object>)
                    group x by ((((x as Dictionary<string, object>)["resource"] as Dictionary<string, object>)["id"]) as Int64?)
                    into grouped
                    select grouped).ToDictionary(x => x.Key, y => y.ToList());

                var newData = new List<object>();

                foreach (var eventElement in list)
                {
                    Dictionary<string, object> oneEvent;
                    var removed = (from ev in eventElement.Value
                                  where (string) ((Dictionary<string, object>)ev)["action"] == "removed"
                                  select ev).ToArray();

                    if (!removed.Any())
                    {
                        var added = (from ev in eventElement.Value
                            where (string) ((Dictionary<string, object>) ev)["action"] == "added"
                            select ev).ToArray();

                        var changed = from ev in eventElement.Value
                            where (string) ((Dictionary<string, object>) ev)["action"] == "changed"
                            orderby DateTime.Parse(((Dictionary<string, object>) ev)["created_at"] as string)
//                            (new AsanaDateTimeConverter().ConvertFrom(((Dictionary<string, object>)ev)["created_at"]))
//                            orderby ((AsanaDateTime)((Dictionary<string, object>)ev)["created_at"]).DateTime
//                            orderby ((AsanaDateTime) ((Dictionary<string, object>) ev)["created_at"]).DateTime
                                descending
                            select ev;
                        if (added.Any())
                        {
                            if (changed.Any())
                            {
                                oneEvent = (Dictionary<string, object>) changed.First(); // leave only the latest change
                                oneEvent["action"] = "added";
                                if (oneEvent["parent"] == null) 
                                    oneEvent["parent"] = ((Dictionary<string, object>) (added.Last()))["parent"]; // but copy the parent
                            }
                            else
                            {
                                oneEvent = (Dictionary<string, object>) added.Last();
                            }
                            if (oneEvent["parent"] == null) oneEvent["parent"] = new Dictionary<string, object>();
                            ((Dictionary<string, object>)oneEvent["parent"]).Add("sync_new" + oneEvent["type"], oneEvent["resource"]);
                            oneEvent.Remove("resource");
                        }
                        else
                        {
                            // just changes on this object
                            oneEvent = (Dictionary<string, object>) changed.First();
                            ((Dictionary<string, object>)oneEvent["resource"]).Add("sync_state", true);
                        }
                    }
                    else
                    {
                        oneEvent = (Dictionary<string, object>) removed.Last();
                        ((Dictionary<string, object>)oneEvent["resource"]).Add("sync_removed", true);
                    }
                    
                    newData.Add(oneEvent);
                }

                data2["data"] = newData;
            }

            return data2;
        }

        /// <summary>
        /// Converts the raw string into list of dictionaries format
        /// </summary>
        /// <param name="dataString"></param>
        /// <returns></returns>
        // was private
        internal static Dictionary<string, object>[] GetDataAsDictArray(string dataString)
        {
            var data = Json.Deserialize(dataString) as Dictionary<string, object>;
            var data2 = data["data"] as List<object>;
            var data3 = new Dictionary<string, object>[data2.Count];
            for (int i = 0; i < data2.Count; ++i)
                data3[i] = data2[i] as Dictionary<string, object>;
            return data3;
        }

        /// <summary>
        /// The callback for response errors
        /// </summary>
        /// <param name="error"></param>
        internal void ErrorCallback(string method, string requestString, string statusCode, string error)
        {
            _errorCallback(method, requestString, statusCode, error);
        }

        /// <summary>
        /// Tells the asana object to save the specified object
        /// </summary>
        internal async Task<T> Save<T>(T obj, AsanaFunction func, Dictionary<string, object> data = null) where T: AsanaObject
        {
            IAsanaData idata = obj as IAsanaData;
            if (idata == null)
                throw new NullReferenceException("All AsanaObjects must implement IAsanaData in order to Save themselves.");

            if (data == null)
				data = Parsing.Serialize(obj, true, !idata.IsObjectLocal);

            AsanaFunctionAssociation afa = AsanaFunction.GetFunctionAssociation(obj.GetType());

            if (func == null)
                func = idata.IsObjectLocal ? afa.Create : afa.Update;

            var uri = GetBaseUriWithParams(func, data, obj);
            var response = await AsanaRequest.GoAsync(this, func, uri);
            return AsanaRequest.GetResponse(response, this, obj); // will also update the object
            
            // TODO: serialize to JSON
            // http://stackoverflow.com/questions/12458532/suppress-requiredattribute-validation-for-the-jsonmediatypeformatter-in-asp-net
            // http://www.asp.net/web-api/overview/web-api-clients/calling-a-web-api-from-a-net-client
        }
        /*
        /// <summary>
        /// Tells asana to delete the specified object
        /// </summary>
        /// <param name="obj"></param>
        internal Task<T> Delete<T>(T obj) where T : AsanaObject
        {
            AsanaFunction func;

            IAsanaData idata = obj as IAsanaData;
            if (idata == null)
                throw new NullReferenceException("All AsanaObjects must implement IAsanaData in order to Delete themselves.");

            AsanaFunctionAssociation afa = AsanaFunction.GetFunctionAssociation(obj.GetType());

            if (idata.IsObjectLocal == false)
                func = afa.Delete;
            else 
                throw new Exception("Object is local, cannot delete.");

            if (Object.ReferenceEquals(func, null)) throw new NotImplementedException("This object cannot delete itself.");

            return Save(obj, func, new Dictionary<string, object>(0));
        }
        */
        #endregion
        public static void RemoveFromAllCacheListsOfType<T>(AsanaObject obj, Asana Host) where T : AsanaObject
        {
            var listsPossiblyContainingThis = Host._objectCache.GetAllOfType<AsanaObjectCollection<T>>("/");
            foreach (var list in listsPossiblyContainingThis)
            {
                list.Remove((T)obj);
            }
            obj.ID = (Int64)AsanaExistance.Deleted;
        }
    }
    public enum AsanaCacheLevel
    {
        Ignore = 0, // Always fetch new objects
        FillExisting = 1, // If Possible
        UseExisting = 2, // If Possible
        UseExistingOrNull = 3, 
        Default = AsanaCacheLevel.UseExisting
    }
}
