using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Models
{
    public class HttpMock : HttpService
    {

        public HttpMock()
            : base()
        {

        }


        public new void AddAuthorization(HttpClient httpClient)
        {
            base.AddAuthorization(httpClient);
        }


        public new T ReadResult<T>(Task<HttpResponseMessage> request, out HttpStatusCode statusCode) where T : new()
        {
            return base.ReadResult<T>(request, out statusCode);
        }

    }
}
