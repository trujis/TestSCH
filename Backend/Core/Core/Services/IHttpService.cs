using System.Collections.Generic;
using System.Net;

namespace Core.Services
{
    public interface IHttpService
    {

        T Get<T>(string url, out HttpStatusCode statusCode, IDictionary<string, object> paramsToSend = null) where T : new();

        T Delete<T>(string url, out HttpStatusCode statusCode, IDictionary<string, object> paramsToSend = null) where T : new();

        T Post<T>(string url, out HttpStatusCode statusCode, object objectToSend = null) where T : new();

        void SetCredentials(string userName, string password);

    }
}