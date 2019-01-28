using Core.Services;
using Core.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests.Services
{
    [TestClass]
    public class HttpServiceShould
    {


        string userName = "Nymeria";
        string password = "111111";
        HttpMock httpServiceMock;
        Foo fooResponse;
        Mock<FakeHttpMessageHandler> fakeHttpMessageHandler;


        [TestInitialize]
        public void Initialize()
        {
            httpServiceMock = new HttpMock();
            fooResponse = new Foo() { UserName = userName, Password = password };
            fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            HttpService.http = new HttpClient(fakeHttpMessageHandler.Object);
        }


        [TestCleanup]
        public void Cleanup()
        {
            httpServiceMock = null;
            HttpService.http = null;
            fakeHttpMessageHandler = null;
        }


        [TestMethod]
        public void Do_not_add_authorization_when_user_is_empty()
        {
            //Arrange
            httpServiceMock.SetCredentials(string.Empty, password);

            //Act
            httpServiceMock.AddAuthorization(HttpService.http);

            //Assert
            Assert.IsNull(HttpService.http.DefaultRequestHeaders.Authorization);
        }


        [TestMethod]
        public void Do_not_explode_when_requesting_auth_with_a_null_httpclient()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);

            //Act
            //Assert
            try
            {
                httpServiceMock.AddAuthorization(null);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }


        [TestMethod]
        public void Do_not_add_authorization_when_password_is_empty()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, string.Empty);

            //Act
            httpServiceMock.AddAuthorization(HttpService.http);

            //Assert
            Assert.IsNull(HttpService.http.DefaultRequestHeaders.Authorization);
        }


        [TestMethod]
        public void Add_a_basic_auth_when_requesting()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);

            //Act
            httpServiceMock.AddAuthorization(HttpService.http);

            //Assert
            Assert.AreEqual<string>("Basic", HttpService.http.DefaultRequestHeaders.Authorization.Scheme);
        }


        [TestMethod]
        public void Add_auth_string_when_requesting_with_valid_parameters()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);

            //Act
            httpServiceMock.AddAuthorization(HttpService.http);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(HttpService.http.DefaultRequestHeaders.Authorization.Parameter));
        }

        
        [TestMethod]
        public void Return_a_default_object_when_status_code_is_not_ok()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);
            fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(JsonConvert.SerializeObject(fooResponse))
            });
            var request = HttpService.http.GetAsync("http://localhost.com/test");

            //Act
            var response = httpServiceMock.ReadResult<Foo>(request, out HttpStatusCode statusCode);

            //Assert
            Assert.AreEqual(response, new Foo());
        }

        
        [TestMethod]
        public void Return_a_valid_response_when_auth_is_valid_and_status_response_is_ok()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);
            fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(fooResponse))
            });
            var request = HttpService.http.GetAsync("http://localhost.com/test");

            //Act
            var a = httpServiceMock.ReadResult<Foo>(request, out HttpStatusCode statusCode);

            //Assert
            Assert.AreEqual(a, fooResponse);
        }


        [TestMethod]
        public void Get_a_default_response_when_url_is_not_valid_doing_a_get_call()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);

            //Act
            Foo response = httpServiceMock.Get<Foo>("", out HttpStatusCode statusCode);

            //Assert
            Assert.AreEqual(response, new Foo());
        }


        [TestMethod]
        public void Get_a_default_response_when_url_is_not_valid_doing_a_post_call()
        {
            //Arrange
            httpServiceMock.SetCredentials(userName, password);

            //Act
            Foo response = httpServiceMock.Post<Foo>("", out HttpStatusCode statusCode);

            //Assert
            Assert.AreEqual(response, new Foo());
        }

    }
}
