using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /*
    public class ApiKey : INotifyPropertyChanged
    {
      private string _key;
      // Declare the event 
      public event PropertyChangedEventHandler PropertyChanged;

      public ApiKey()
      {
      }

      public ApiKey(string value)
      {
          this._key = value;
      }

      public string Key
      {
          get { return _key; }
          set
          {
              _key = value;
              // Call OnPropertyChanged whenever the property is updated
              OnPropertyChanged("Key");
          }
      }

      // Create the OnPropertyChanged method to raise the event 
      protected void OnPropertyChanged(string name)
      {
          PropertyChangedEventHandler handler = PropertyChanged;
          if (handler != null)
          {
              handler(this, new PropertyChangedEventArgs(name));
          }
      }
    }
    */
    [Serializable]
    public partial class Asana
    {        
        #region Variables

        /// <summary>
        /// The URL we use to prefix all the requests
        /// e.g. https://app.asana.com/api/1.0
        /// </summary>
        private readonly string _baseUrl;

        /// <summary>
        /// An error callback for the outside world
        /// </summary>
        private Action<string, string, HttpStatusCode, string, int> _errorCallback;

        internal readonly HttpClient BaseHttpClient = new HttpClient();

        internal IAsanaCache ObjectCache;

        public AsanaCacheLevel DefaultCacheLevel = AsanaCacheLevel.UseExisting;

        public HttpStatusCode[] NoRetryStatusCodes = { HttpStatusCode.Forbidden, HttpStatusCode.MethodNotAllowed, HttpStatusCode.RequestUriTooLong, HttpStatusCode.Unauthorized };

        public int AutoRetryCount;

        #endregion

        #region Properties

        /// <summary>
        /// The Authentication Type used for API access
        /// </summary>
        public AuthenticationType AuthType { get; set; }

        public Func<string> ApiKeyOrBearerToken { get; set; }

        /*
        public AuthenticationHeaderValue AuthenticationHeader { get; set; }

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
        */

        #endregion        

        #region Methods

        internal void GenerateAuthenticationHeader()
        {
            var apiKeyOrBearerToken = ApiKeyOrBearerToken();
            string encodedApiKey = String.Empty;
            if (AuthType != AuthenticationType.OAuth)
            {
                encodedApiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(apiKeyOrBearerToken + ":"));
            }

            var defaultAuth = new AuthenticationHeaderValue(AuthType == AuthenticationType.OAuth ? "Bearer" : "Basic", AuthType == AuthenticationType.OAuth ? apiKeyOrBearerToken : encodedApiKey);
            BaseHttpClient.DefaultRequestHeaders.Authorization = defaultAuth;
        }

        static Asana()
        {
            AsanaFunction.InitFunctions();
        }

        /// <summary>
        /// Creates a new Asana entry point.
        /// </summary>
		/// <param name="apiKeyOrBearerToken">The API key (for Basic authentication) or Bearer Token (for OAuth authentication) for the account we intend to access</param>
		public Asana(Func<string> apiKeyOrBearerToken, AuthenticationType authType, Action<string, string, HttpStatusCode, string, int> errorCallback, int autoRetryCount = 3, IAsanaCache cache = null, AsanaCacheLevel defaultCacheLevel = AsanaCacheLevel.UseExisting)
        {   
            _baseUrl = "https://app.asana.com/api/1.0";
            _errorCallback = errorCallback;
            AutoRetryCount = autoRetryCount;
            ObjectCache = cache ?? new MemCache(Guid.NewGuid() + "/");
            DefaultCacheLevel = defaultCacheLevel;

            ObjectCache.AddBannedType(typeof(AsanaDummyObject));
            ObjectCache.AddBannedType(typeof(AsanaProjectBase));

			AuthType = authType;
            ApiKeyOrBearerToken = apiKeyOrBearerToken;

			BaseHttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AsanaNet", "1.1-async"));
            BaseHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                        if (split[1] == "removed" || split[1] == "changed")
                            obj["type"] = "projectbase";
                        else
                        {
                            // hack - until they fix it - tags don't have followers

                            var resource = (Dictionary<string, object>) obj["resource"];
                            if (resource.ContainsKey("followers")
                                && (resource["followers"] as List<object>).Any()
                                && ((resource["followers"] as List<object>).First() as Dictionary<string, object>).ContainsKey("id"))
                                obj["type"] = "project";
                            else
                                obj["type"] = "tag";
                        }
                    }
                    if (split[1] != "changed")
                    {
                        if (obj["parent"] == null) obj["parent"] = new Dictionary<string, object>();
                        ((Dictionary<string, object>) obj["parent"]).Add("sync_" + obj["action"] + obj["type"],
                            obj["resource"]);
                    }
                    else
                    {
                        ((Dictionary<string, object>)obj["resource"]).Add("sync_state", true);
                        obj.Add("sync_" + obj["action"] + obj["type"], obj["resource"]);
                    }
//                    obj.Remove("resource");

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
                            where (((x as Dictionary<string, object>)["resource"] as Dictionary<string, object>).ContainsKey("id"))
                            orderby DateTime.Parse(((Dictionary<string, object>)x)["created_at"] as string) descending
                            group x by ((((x as Dictionary<string, object>)["resource"] as Dictionary<string, object>)["id"]) as Int64?)
                            into grouped
                            select grouped).ToDictionary(x => x.Key, y => y.ToList());

                var newData = new List<object>();

                foreach (var eventElement in list)
                {
                    Dictionary<string, object> oneEvent;

                    var outputEvents = (from ev in eventElement.Value
                                  where 
                                  (string) ((Dictionary<string, object>)ev)["action"] == "removed" 
                                  ||
                                  (string)((Dictionary<string, object>)ev)["action"] == "added"
                                  select ev).ToList();

//                    var added = (from ev in eventElement.Value
//                                 where (string)((Dictionary<string, object>)ev)["action"] == "added"
//                                 select ev).ToArray();

                    var changed = from ev in eventElement.Value
                                  where (string)((Dictionary<string, object>)ev)["action"] == "changed"
                                  orderby DateTime.Parse(((Dictionary<string, object>)ev)["created_at"] as string)
                                  descending
                                  select ev;

                    if (changed.Any())
                    {
                        outputEvents.Add(changed.First()); // leave only the latest change
//                        oneEvent = (Dictionary<string, object>)changed.First(); 
//                        oneEvent["action"] = "added";
//                        if (oneEvent["parent"] == null)
//                            oneEvent["parent"] = ((Dictionary<string, object>)(added.Last()))["parent"]; // but copy the parent
                    }
                    /*
                    if (!removed.Any())
                    {

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
                    */

                    newData.AddRange(outputEvents);
                    newData = (from ev in newData
                                          orderby DateTime.Parse(((Dictionary<string, object>) ev)["created_at"] as string)
                                          ascending
                                          select ev).ToList();
                    newData.ForEach(ev => ((Dictionary<string, object>)ev).Remove("resource"));
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
        internal void ErrorCallback(string method, string requestString, HttpStatusCode statusCode, string error, int retryNo)
        {
            _errorCallback(method, requestString, statusCode, error, retryNo);
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

        internal static void RemoveFromAllCacheListsOfType<T>(AsanaObject obj, Asana Host) where T : AsanaObject
        {
            var listsPossiblyContainingThis = Host.ObjectCache.GetAllOfType<AsanaObjectCollection<T>>("/");
            foreach (var list in listsPossiblyContainingThis)
            {
                list.Remove((T)obj);
            }
            Host.ObjectCache.Remove(obj.ID.ToString());
//            obj.IsRemoved = true;
            
//            obj.ID = (Int64)AsanaExistance.Deleted;
        }
        public Type GetCacheTypeById(long id)
        {
            var obj = ObjectCache.Get(id.ToString());
            if (obj == null)
                return null;
            return obj.GetType();
        }
    }
    public enum AsanaCacheLevel
    {
        Ignore = 0, // Always fetch new objects
        FillExisting = 1, // If Possible
        UseExisting = 2, // If Possible
        UseExistingOrNull = 3, 
        Default = UseExisting
    }
}
