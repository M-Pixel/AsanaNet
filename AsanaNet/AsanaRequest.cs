using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace AsanaNet
{
    /// <summary>
    /// Wraps up the request
    /// </summary>
    internal class AsanaRequest
    {
        /// <summary>
        /// Static because all Asana requests will have a common throttle
        /// </summary>
        private static bool _throttling = false;
        private static ManualResetEvent _throttlingWaitHandle = new ManualResetEvent(false);

        private static void ThrottleFor(int seconds)
        {
            _throttling = true;
            Timer timer = null;
            timer = new Timer(s =>
                {
                    _throttling = false;
                    _throttlingWaitHandle.Set();
                    _throttlingWaitHandle = new ManualResetEvent(false);
                    timer.Dispose();
                }, null, 1000 * seconds, Timeout.Infinite);
        }

        /// <summary>
        /// Begins the request
        /// </summary>
        public static async Task<string> GoAsync(Asana asana, AsanaFunction function, Uri uri, HttpContent content = null)
        {
            return await Task.Factory.StartNew<string>(() =>
            {
                if (_throttling)
                {
                    _throttlingWaitHandle.WaitOne();
                }

                // Initalise a response object
                HttpResponseMessage response = null;

                if (function.Method != "GET" && content == null)
                {
//                    content = new StringContent(null, Encoding.UTF8, "application/json");
                    content = new StringContent(String.Empty);
                    content.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }

                switch (function.Method)
                {
                    default: //GET
                        response = asana._baseHttpClient.GetAsync(uri).Result;
                        break;
                    case "POST":
                        response = asana._baseHttpClient.PostAsync(uri, content).Result;
                        break;
                    case "PUT":
                        response = asana._baseHttpClient.PutAsync(uri, content).Result;
                        break;
                    case "DELETE":
                        response = asana._baseHttpClient.DeleteAsync(uri).Result;
                        break;
                }

                try
                {
                    response.EnsureSuccessStatusCode();
#if DEBUG
                    asana.ErrorCallback(function.Method, uri.AbsoluteUri, response.StatusCode.ToString(), "SUCCESS");
#endif
                }
                catch (HttpRequestException e)
                {
                    asana.ErrorCallback(function.Method, uri.AbsoluteUri, response.StatusCode.ToString(), e.Message);

                    if (!ReferenceEquals(response.Headers.RetryAfter, null) && response.Headers.RetryAfter.Delta.HasValue)
                    {
                        var retryAfter = response.Headers.RetryAfter.Delta.Value.Seconds;
//                        var retryAfter = DateTime.Now + response.Headers.RetryAfter.Delta.Value;
                        ThrottleFor(retryAfter);
                        return GoAsync(asana, function, uri, content).Result;
                    }

                    throw;
                }
                return response.Content.ReadAsStringAsync().Result;

                // Create a content object for the request
                //HttpContent content_ = new System.Net.Http.StringContent(root.ToString(), Encoding.UTF8, "application/json");
            });
        }


        /// <summary>
        /// Begins the request
        /// </summary>
        public static T GetResponse<T>(string responseContent, Asana asana, T cachedObject = null) where T : AsanaObject
        {
            // if cachedObject present - fills it with new data (leaving old there)

            var outputObjectDict = Asana.GetDataAsDict(responseContent);
            AsanaObject outputObject = cachedObject ?? AsanaObject.Create(typeof (T));

            // make sure to set the ID first so that the cache references can be valid
            if (outputObjectDict.ContainsKey("id"))
                outputObject.ID = (Int64)outputObjectDict["id"];

            //          var outputObject = AsanaObject.Create(typeof(T));
            //                outputObject = AsanaObject.Create(typeof(T), thisId, asana);

            Parsing.Deserialize(outputObjectDict, outputObject, asana);
            return (T)outputObject;
        }

        /// <summary>
        /// Begins the request
        /// </summary>
        public static AsanaObjectCollection<T> GetResponseCollection<T>(string responseContent, Asana asana, AsanaObjectCollection<T> cachedCollection = null, bool fillCachedElements = true) where T : AsanaObject
        {
            // if cachedCollection present - fills it with new data (leaving old there)

            var k = Asana.GetDataAsDictArray(responseContent);

            AsanaObjectCollection<T> outputCollection;
            if (cachedCollection != null)
            {
                outputCollection = cachedCollection;
                outputCollection.Clear();
            }
            else 
                outputCollection = new AsanaObjectCollection<T>();

            foreach (var outputObjectDict in k)
            {
                var thisId = (Int64) outputObjectDict["id"];
                AsanaObject newObject;
//                if (outputObjectDict.ContainsKey("id"))
                    newObject = AsanaObject.Create(typeof(T), thisId, fillCachedElements ? asana : null);
//                else
//                    newObject = AsanaObject.Create(typeof(T));

                Parsing.Deserialize(outputObjectDict, newObject, asana);
                outputCollection.Add((T)newObject);
            }
            return outputCollection;
        }

    }
}
