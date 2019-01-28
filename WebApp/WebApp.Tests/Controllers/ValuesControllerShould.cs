﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp;
using WebApp.Controllers;
using WebApp.Handlers;

namespace WebApp.Tests.Controllers
{
    /*
    [TestClass]
    public class ValuesControllerShould
    {

        [TestMethod]
        public void Gets()
        {
            // Arrange
            IPrincipal principal = new GenericPrincipal(new BasicAuthenticationIdentity("TestName", "TestPassword"), new[] { "Test Role" });

            HttpActionContext mockActionContext = new HttpActionContext()
            {
                ControllerContext = new HttpControllerContext()
                {
                    Request = new HttpRequestMessage(),
                    RequestContext = new HttpRequestContext() { Principal = principal,  }
                },
                ActionArguments = { { "SomeArgument", "null" } }
            };
            mockActionContext.ControllerContext.Configuration = new HttpConfiguration();
            mockActionContext.ControllerContext.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            // Act
            UserAuthorization authAttr = new UserAuthorization();
            authAttr.OnAuthorization(mockActionContext);

            // Assert
            Assert.IsTrue(mockActionContext.Response.StatusCode == System.Net.HttpStatusCode.Forbidden);
        }


        /*[TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Delete(5);

            // Assert
        }
        */
    //}
}
