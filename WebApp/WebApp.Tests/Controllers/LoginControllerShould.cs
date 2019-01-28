using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Services;
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
    public class LoginControllerShould
    {


        string userName = "Nymeria";
        string password = "111111";
        private LoginViewModel userTest;
        private User response;
        private User invalidResponse;
        private Mock<IHttpService> httpServiceMock;
        private LoginControllerMock controller;
        private MockHttpSession sessionMock;
        private Mock<ControllerContext> contextMock;


        [TestInitialize]
        public void Initialize()
        {
            controller = new LoginControllerMock();
            userTest = new LoginViewModel() { UserName = userName, Password = password };
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_1 } };
            invalidResponse = new User() { UserName = null, Password = null };
            sessionMock = new MockHttpSession();
            contextMock = new Mock<ControllerContext>();
            httpServiceMock = new Mock<IHttpService>();

            contextMock.Setup(m => m.HttpContext.Session).Returns(sessionMock);
            controller.ControllerContext = contextMock.Object;
            controller.http = httpServiceMock.Object;
        }


        [TestCleanup]
        public void Cleanup()
        {
            httpServiceMock = null;
            controller.Session["user"] = null;
            controller = null;
            sessionMock = null;
            contextMock = null;
        }


        [TestMethod]
        public void Have_a_valid_title()
        {
            // Arrange

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login Page", result.ViewBag.Title);
        }


        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Not_start_session_and_have_a_null_session_value_when_user_does_not_exists()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(invalidResponse);

            // Act
            controller.Index(userTest);
            var session = controller.Session["user"];

            // Assert
        }


        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Not_start_session_and_have_a_null_session_value_when_returned_user_has_not_defined_roles()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            var lastEnumValue = Enum.GetValues(typeof(RolesEnum)).Cast<RolesEnum>().Last();
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { lastEnumValue+1 } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);
            
            // Act
            controller.Index(userTest);
            var session = controller.Session["user"];

            // Assert
        }


        [TestMethod]
        public void Start_session_and_have_a_non_null_session_value_when_user_exists()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            controller.Index(userTest);
             
            // Assert
            Assert.IsNotNull(controller.Session["user"]);
        }


        [TestMethod]
        public void Start_session_when_user_clicks_login_button_with_a_valid_user()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            var result = (RedirectToRouteResult)controller.Index(userTest);
            LoginViewModel userSession = (LoginViewModel)controller.Session["user"];

            // Assert
            Assert.AreEqual("Nymeria", userSession.UserName);
        }


        [TestMethod]
        public void The_session_be_type_of_user_when_user_is_valid()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            var result = (RedirectToRouteResult)controller.Index(userTest);

            // Assert
            Assert.IsInstanceOfType(controller.Session["user"], typeof(LoginViewModel));
        }


        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Not_set_session_with_a_valid_user_but_without_roles()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password};
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            controller.Index(userTest);
            var session = controller.Session["user"];

            // Assert
        }


        [TestMethod]
        public void Redirect_to_manage_when_user_logged_has_role_ADMIN()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.ADMIN } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(userTest);
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Admin", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Redirect_to_PAGE_1_when_user_logged_has_role_PAGE_1()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_1 } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(userTest);
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Page_1", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Redirect_to_PAGE_2_when_user_logged_has_role_PAGE_2()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_2 } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(userTest);
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Page_2", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Redirect_to_PAGE_3_when_user_logged_has_role_PAGE_3()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_3 } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(userTest);
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Page_3", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Redirect_to_PAGE_1_when_user_logged_has_all_roles_except_admin()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            response = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_1, RolesEnum.PAGE_2, RolesEnum.PAGE_3 } };
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(response);

            // Act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Index(userTest);
            result.RouteValues.TryGetValue("controller", out object controllerResult);
            result.RouteValues.TryGetValue("action", out object actionResult);

            // Assert
            Assert.AreEqual("Page_1", controllerResult);
            Assert.AreEqual("Index", actionResult);
        }


        [TestMethod]
        public void Set_error_in_modelstate_when_user_is_not_found()
        {
            // Arrange
            HttpStatusCode statusCode = HttpStatusCode.OK;
            httpServiceMock.Setup(m => m.Get<User>(It.IsAny<string>(), out statusCode, It.IsAny<IDictionary<string, object>>()))
                .Returns(invalidResponse);

            // Act
            ActionResult result = controller.Index(userTest);

            // Assert
            Assert.IsTrue(controller.ModelState.ContainsKey("error"));
        }


        [TestMethod]
        public void Stop_session_when_user_clicks_logout_button()
        {
            // Arrange
            var result = controller.Index(userTest);

            // Act
            controller.Logout();

            // Assert
            Assert.IsNull(controller.Session["user"]);
        }


        [TestMethod]
        public void Be_a_subclass_of_master_controller()
        {
            // Arrange
            
            // Act

            // Assert
            Assert.IsTrue(typeof(LoginController).IsSubclassOf(typeof(MasterController)));
        }


        public class LoginControllerMock : LoginController
        {

            protected override string GetApiUrl(string path)
            {
                return path;
            }
        }
    }
}
