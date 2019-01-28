using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Users.Enums;
using Users.Models;
using WebApp;
using WebApp.Controllers;
using WebApp.Controllers.PageControllers;
using WebApp.Models;
using WebApp.Tests.Mocks;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class Page_1ControllerShould
    {

        readonly private LoginViewModel userTest = new LoginViewModel() { UserName = "Nymeria", Password = "111111" };

        
        [TestMethod]
        public void Have_a_valid_title()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login Page", result.ViewBag.Title);
        }

        /*
        [TestMethod]
        public void Should_start_session_and_have_a_non_null_session_value()
        {
            // Arrange
            LoginController controller = GetControllerWithSessionMock();

            // Act
            var result = (RedirectToRouteResult)controller.Index(userTest);

            // Assert
            Assert.IsNotNull(controller.Session["user"]);
        }


        [TestMethod]
        public void Should_the_session_be_type_of_user()
        {
            // Arrange
            LoginController controller = GetControllerWithSessionMock();

            // Act
            var result = (RedirectToRouteResult)controller.Index(userTest);

            // Assert
            Assert.IsInstanceOfType(controller.Session["user"], typeof(UserSession));
        }


        [TestMethod]
        public void Should_start_session_when_user_clicks_login_button()
        {
            // Arrange
            LoginController controller = GetControllerWithSessionMock();

            // Act
            var result = (RedirectToRouteResult)controller.Index(userTest);
            UserSession userSession = (UserSession)controller.Session["user"];

            // Assert
            Assert.AreEqual("Nymeria", userSession.UserName);
        }


        [TestMethod]
        public void Should_stop_session_when_user_clicks_logout_button()
        {
            // Arrange
            LoginController controller = GetControllerWithSessionMock();
            var result = controller.Index(userTest);

            // Act
            controller.Logout();

            // Assert
            Assert.IsNull(controller.Session["user"]);
        }
        */

        [TestMethod]
        public void Be_a_subclass_of_master_controller()
        {
            // Arrange
            
            // Act

            // Assert
            Assert.IsTrue(typeof(LoginController).IsSubclassOf(typeof(MasterController)));
        }

        /*
        private LoginController GetControllerWithSessionMock()
        {
            LoginController controller = new LoginController();
            var session = new MockHttpSession();
            var context = new Mock<ControllerContext>();
            context.Setup(m => m.HttpContext.Session).Returns(session);
            controller.ControllerContext = context.Object;

            return controller;
        }*/
    }
}
