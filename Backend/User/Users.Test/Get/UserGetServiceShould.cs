using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Users.Models;
using Users.Get;

namespace Users.Test.Get
{
    [TestClass]
    public class UserGetServiceShould
    {


        UserGetService userService;
        Mock<IUserGetRepository>  userRepositoryMock;
        string userName = "TestName";
        string password = "TestPassword";


        [TestInitialize]
        public void Initialize()
        {
            userService = new UserGetService();
            userRepositoryMock = new Mock<IUserGetRepository>();
            userService.userRepository = userRepositoryMock.Object;
        }


        [TestCleanup]
        public void Cleanup()
        {
            userService = null;
            userRepositoryMock = null;
        }


        [TestMethod]
        public void Call_repository_when_obtaining_one_user_with_a_valid_username_and_password()
        {
            //Arrange

            //Act
            userService.Get(userName, password);

            //Assert
            userRepositoryMock.Verify(m => m.Get(userName), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "UserName/Password must not be empty")]
        public void Not_call_repository_when_getting_an_empty_username()
        {
            //Arrange

            //Act
            userService.Get("", password);

            //Assert
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "UserName/Password must not be empty")]
        public void Not_call_repository_when_password_is_empty()
        {
            //Arrange

            //Act
            userService.Get(userName, "");

            //Assert
        }

        

        [TestMethod]
        public void Return_a_null_value_when_username_is_not_found_in_repository()
        {
            //Arrange
            userRepositoryMock.Setup(m => m.Get(userName)).Returns<User>(null);

            //Act
            User user = userService.Get(userName, password);

            //Assert
            Assert.IsNull(user);
        }


        [TestMethod]
        public void Return_a_null_value_when_username_is_found_in_repository_but_passwords_are_not_the_same()
        {
            //Arrange
            User userReturn = new User()
            {
                UserName = userName,
                Password = password + "Other"
            };
            userRepositoryMock.Setup(m => m.Get(userName)).Returns(userReturn);

            //Act
            User user = userService.Get(userName, password);

            //Assert
            Assert.IsNull(user);
        }


        [TestMethod]
        public void Return_a_valid_user_username_is_found_in_repository_with_the_same_password()
        {
            //Arrange
            User userReturn = new User()
            {
                UserName = userName,
                Password = password
            };
            userRepositoryMock.Setup(m => m.Get(userName)).Returns(userReturn);

            //Act
            User user = userService.Get(userName, password);

            //Assert
            Assert.IsInstanceOfType(user, typeof(User));
            Assert.AreSame(user, userReturn);
        }

        
        [TestMethod]
        public void Call_repository_when_verifying_if_user_exists()
        {
            //Arrange

            //Act
            userService.Exists(userName);

            //Assert
            userRepositoryMock.Verify(m => m.Get(userName), Times.Once);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "UserName must not be empty")]
        public void Not_call_repository_when_verifying_if_exists_an_empty_username()
        {
            //Arrange

            //Act
            userService.Exists("");

            //Assert
            userRepositoryMock.Verify(m => m.Get(It.IsAny<string>()), Times.Never);
        }

        
        
        [TestMethod]
        public void Return_false_when_checking_if_exists_username_and_user_is_not_found()
        {
            //Arrange
            userRepositoryMock.Setup(m => m.Get(userName)).Returns<User>(null);

            //Act
            bool exists = userService.Exists(userName);

            //Assert
            Assert.IsFalse(exists);
        }

        
        
        [TestMethod]
        public void Return_true_when_checking_if_exists_username_and_user_is_not_found()
        {
            //Arrange
            User userReturn = new User()
            {
                UserName = userName
            };
            userRepositoryMock.Setup(m => m.Get(userName)).Returns(userReturn);

            //Act
            bool exists = userService.Exists(userName);

            //Assert
            Assert.IsTrue(exists);
        }
        
    }
}
