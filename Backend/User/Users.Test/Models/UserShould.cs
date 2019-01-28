using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Users.Enums;
using Users.Models;

namespace Users.Test.Models
{
    
    [TestClass]
    public class UserShould
    {


        string userName = "Nymeria";
        string password = "111111";
        User user;


        [TestInitialize]
        public void Initialize()
        {
            user = new User() { UserName = userName, Password = password, Roles = new List<RolesEnum>() { RolesEnum.PAGE_1 } };
        }


        [TestCleanup]
        public void Cleanup()
        {
            user = null;
        }


        [TestMethod]
        public void Have_private_get_password()
        {
            //Arrange
            PropertyInfo passwordProperty = GetPasswordProperty();

            //Act

            //Assert
            Assert.IsNotNull(passwordProperty);
            Assert.IsTrue(passwordProperty.GetMethod.IsPrivate);
        }


        [TestMethod]
        public void Have_public_set_password()
        {
            //Arrange
            PropertyInfo passwordProperty = GetPasswordProperty();

            //Act

            //Assert
            Assert.IsNotNull(passwordProperty);
            Assert.IsTrue(passwordProperty.SetMethod.IsPublic);
        }


        [TestMethod]
        public void Return_false_when_password_of_user_is_null()
        {
            //Arrange
            User user = new User() { UserName = "", Password = null };

            //Act
            bool response = user.HasSamePassword(password);

            //Assert
            Assert.IsFalse(response);
        }


        [TestMethod]
        public void Return_false_when_password_are_different_than_the_user()
        {
            //Arrange

            //Act
            bool response = user.HasSamePassword(password + "Other");

            //Assert
            Assert.IsFalse(response);
        }


        [TestMethod]
        public void Return_true_when_password_are_the_same_than_the_user()
        {
            //Arrange

            //Act
            bool response = user.HasSamePassword(password);

            //Assert
            Assert.IsTrue(response);
        }


        [TestMethod]
        public void Return_false_when_checking_for_roles_but_user_doesnt_has_one()
        {
            //Arrange
            User user = new User() { UserName = userName, Password = password };

            //Act
            bool response = user.ContainsRole(RolesEnum.PAGE_1);

            //Assert
            Assert.IsFalse(response);
        }


        [TestMethod]
        public void Return_false_when_checking_for_roles_that_user_doesnt_has()
        {
            //Arrange

            //Act
            bool response = user.ContainsRole(RolesEnum.ADMIN);

            //Assert
            Assert.IsFalse(response);
        }


        [TestMethod]
        public void Return_true_when_checking_for_roles_that_user_has()
        {
            //Arrange

            //Act
            bool response = user.ContainsRole(RolesEnum.PAGE_1);

            //Assert
            Assert.IsTrue(response);
        }


        private PropertyInfo GetPasswordProperty()
        {
            User user = new User();
            PropertyInfo passwordProperty = null;

            foreach (PropertyInfo prop in user.GetType().GetTypeInfo().DeclaredProperties)
            {
                if (prop.Name.Equals("Password"))
                {
                    passwordProperty = prop;
                }
            }

            return passwordProperty;
        }
    }
}
