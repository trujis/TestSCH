using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Users.Models;
using Users.Get;
using Core.Services;
using Core.Services.Cache;
using Users.Save;

namespace Users.Test.Get
{
    [TestClass]
    public class UserGetRepositoryShould
    {


        readonly string userName = "TestName";
        Mock<ICache> cacheServiceMock;
        Mock<IUserSaveRepository> repositoryMock;
        //string password = "TestPassword";


        [TestInitialize]
        public void Initialize()
        {
            cacheServiceMock = new Mock<ICache>();
            repositoryMock = new Mock<IUserSaveRepository>();
        }


        [TestCleanup]
        public void Cleanup()
        {
            cacheServiceMock = null;
        }

        
        [TestMethod]
        public void Add_all_default_users_on_first_time()
        {
            //Arrange
            cacheServiceMock.Setup(m => m.Get<dynamic>(It.IsAny<string>())).Returns<dynamic>(null);
            UserGetRepository userRepository = new UserGetRepository
            {
                cache = cacheServiceMock.Object,
                repository = repositoryMock.Object
            };

            //Act
            userRepository.Get(userName);

            //Assert
            repositoryMock.Verify(m => m.Save(It.IsAny<User>()), 
                Times.Exactly(UserGetRepository.defaultUsers.Count())
            );
        }

        
        [TestMethod]
        public void Do_not_add_users_on_second_time()
        {
            //Arrange
            UserGetRepository userRepository = new UserGetRepository();
            userRepository.Get(userName);
            userRepository.cache = cacheServiceMock.Object;

            //Act
            userRepository.Get(userName);

            //Assert
            cacheServiceMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Never);
        }

        /*
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
        */
    }
}
