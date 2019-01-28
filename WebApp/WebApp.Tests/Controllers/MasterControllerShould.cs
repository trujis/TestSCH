using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Users.Enums;
using Users.Models;
using WebApp;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Tests.Mocks;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class MasterControllerShould
    {


        string userName = "Nymeria";
        string password = "111111";
        private LoginViewModel userTest;
        private MyControllerTesting controller;


        [TestInitialize]
        public void Initialize()
        {
            userTest = new LoginViewModel() { UserName = userName, Password = password };
            controller = new MyControllerTesting();
        }


        [TestCleanup]
        public void Cleanup()
        {
            controller = null;
        }


        [TestMethod]
        public void Redirect_to_login_page_when_user_logouts()
        {
            // Arrange

            // Act
            var result = (RedirectToRouteResult) controller.Logout();
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Login", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Not_finalize_session_when_user_does_nothing()
        {
            // Arrange

            // Act
            controller.StartSession(userTest);

            // Assert
            Assert.IsNotNull(controller.Session["user"]);
        }


        [TestMethod]
        public void Finalize_session_when_user_logouts()
        {
            // Arrange
            controller.StartSession(userTest);

            // Act
            controller.Logout();

            // Assert
            Assert.IsNull(controller.Session["user"]);
        }
        

        private class MyControllerTesting : MasterController
        {

            //Arrange Controller...
            public MyControllerTesting()
            {
                var session = new MockHttpSession();
                var context = new Mock<ControllerContext>();
                context.Setup(m => m.HttpContext.Session).Returns(session);
                ControllerContext = context.Object;
            }
            

            public void StartSession(LoginViewModel user)
            {
                Session["user"] = user;
            }
            

            protected override bool HasRole()
            {
                throw new System.NotImplementedException();
            }
        }

        
    }
}
