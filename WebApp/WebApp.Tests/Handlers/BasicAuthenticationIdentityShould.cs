using System.Collections.Generic;
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
    [TestClass]
    public class BasicAuthenticationIdentityShould
    {
        

        [TestMethod]
        public void Set_a_correct_password()
        {
            // Arrange
            string password = "Password";

            // Act
            BasicAuthenticationIdentity auth = new BasicAuthenticationIdentity("", password);

            // Assert
            Assert.AreEqual(auth.Password, password);
        }


        [TestMethod]
        public void Set_a_correct_username()
        {
            // Arrange
            string username = "Username";

            // Act
            BasicAuthenticationIdentity auth = new BasicAuthenticationIdentity(username, "");

            // Assert
            Assert.AreEqual(auth.Name, username);
        }


        [TestMethod]
        public void Have_a_basic_authentication_type()
        {
            // Arrange

            // Act
            BasicAuthenticationIdentity auth = new BasicAuthenticationIdentity("", "");

            // Assert
            Assert.AreEqual(auth.AuthenticationType, "Basic");
        }

        
    }
}
