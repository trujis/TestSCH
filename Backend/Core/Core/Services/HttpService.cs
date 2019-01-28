using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


[assembly: InternalsVisibleTo("Core.Tests")]
namespace Core.Services
{
    public class HttpService : IHttpService
    {


        internal static HttpClient http;
        protected string userName;
        protected string password;


        public HttpService()
        {
            http = new HttpClient(); //IOC
        }


        /// <summary>
        /// Do HTTP Get call, with an optional parameters (URL Parameters)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paramsToSend"></param>
        /// <returns></returns>
        public T Get<T>(string url, out HttpStatusCode statusCode, IDictionary<string, object> paramsToSend = null) where T : new()
        {
            return CallWithParams<T>(url, out statusCode, true, paramsToSend);
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="statusCode"></param>
        /// <param name="paramsToSend"></param>
        /// <returns></returns>
        public T Delete<T>(string url, out HttpStatusCode statusCode, IDictionary<string, object> paramsToSend = null) where T : new()
        {
            return CallWithParams<T>(url, out statusCode, false, paramsToSend);
        }

        
        public T CallWithParams<T>(string url, out HttpStatusCode statusCode, bool isGet, IDictionary<string, object> paramsToSend) where T : new()
        {
            var response = new T();
            statusCode = HttpStatusCode.OK;

            try
            {
                using (http)
                {
                    AddAuthorization(http);

                    string finalUrl = BuildUrl(url, paramsToSend);

                    var request = (isGet)
                        ? http.GetAsync(finalUrl)
                        : http.DeleteAsync(finalUrl);

                    response = ReadResult<T>(request, out statusCode);
                }
            }
            catch
            {
                //Crossing fingers... --> Should track
            }

            return response;
        }


        /// <summary>
        /// Do HTTP Post call, with an optional body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="objectToSend"></param>
        /// <returns></returns>
        public T Post<T>(string url, out HttpStatusCode statusCode, object objectToSend = null) where T : new()
        {
            var response = new T();
            statusCode = HttpStatusCode.OK;

            try
            {
                HttpContent stringContent = null;
                if (objectToSend != null)
                {
                    stringContent = new StringContent(JsonConvert.SerializeObject(objectToSend), UnicodeEncoding.UTF8, "application/json");
                }

                using (http)
                {
                    AddAuthorization(http);

                    var request = http.PostAsync(url, stringContent);
                    response = ReadResult<T>(request, out statusCode);
                }
            }
            catch
            {
                //Crossing fingers... --> Should track
            }

            return response;
        }


        private string BuildUrl(string url, IDictionary<string, object> paramsToSend = null)
        {
            StringBuilder finalUrl = new StringBuilder(url);

            if (paramsToSend != null)
            {
                foreach (var key in paramsToSend.Keys)
                {
                    finalUrl.Append(finalUrl.ToString().Equals(url) ? '?' : '&');
                    finalUrl.Append(key);
                    finalUrl.Append('=');
                    finalUrl.Append(paramsToSend[key].ToString());
                }
            }

            return finalUrl.ToString();
        }


        /// <summary>
        /// Initialize credentials (Basic auth)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void SetCredentials(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }


        /// <summary>
        /// Ads the basic authorization to the call
        /// </summary>
        /// <param name="httpClient"></param>
        protected void AddAuthorization(HttpClient httpClient)
        {
            if (AreAuthorizationParamsValid(httpClient))
            {
                string authStr = string.Format("{0}:{1}", userName, password);
                var byteArray = Encoding.ASCII.GetBytes(authStr);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
        }


        private bool AreAuthorizationParamsValid(HttpClient httpClient)
        {
            return httpClient != null 
                && !string.IsNullOrEmpty(userName) 
                && !string.IsNullOrEmpty(password);
        }


        protected T ReadResult<T>(Task<HttpResponseMessage> request, out HttpStatusCode statusCode) where T : new()
        {
            var response = new T();
            statusCode = request.Result.StatusCode;

            if (statusCode == HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<T>(request.Result.Content.ReadAsStringAsync().Result);
            }

            return response;
        }
    }
}
