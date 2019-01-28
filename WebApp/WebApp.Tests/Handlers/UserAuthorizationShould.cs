using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Users.Enums;
using Users.Models;
using WebApp;
using WebApp.Controllers;
using WebApp.Handlers;
using WebApp.Models;
using WebApp.Tests.Mocks;

namespace WebApp.Tests.Controllers
{
    /* TODO: Test!
    [TestClass]
    public class UserAuthorizationShould
    {

        [TestMethod]
        public void Get()
        {
            // Arrange
            IPrincipal principal = new GenericPrincipal(new BasicAuthenticationIdentity("TestName", "TestPassword"), new[] { "Test Role" });

            HttpActionContext mockActionContext = new HttpActionContext()
            {
                ControllerContext = new HttpControllerContext()
                {
                    Request = new HttpRequestMessage(),
                    RequestContext = new HttpRequestContext() { Principal = principal, }
                },
                ActionArguments = { { "SomeArgument", "null" } }
            };
            mockActionContext.ControllerContext.Configuration = new HttpConfiguration();
            mockActionContext.ControllerContext.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            // Act
            UserAuthorization authAttr = new UserAuthorization()
            {
                Roles = "Tes"
            };
            authAttr.OnAuthorization(mockActionContext);

            // Assert
            Assert.IsTrue(mockActionContext.Response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }

    }
    */
}
