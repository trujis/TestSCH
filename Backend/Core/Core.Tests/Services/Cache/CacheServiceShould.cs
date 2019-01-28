using Core.Services;
using Core.Services.Cache;
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
using System.Threading;
using System.Threading.Tasks;

namespace Core.Tests.Services.Cache
{
    [TestClass]
    public class CacheServiceShould
    {


        readonly string cacheKey = "KEY";
        readonly string userName = "UserName";
        readonly string password = "Password";
        Foo fooResponse;
        CacheService cache;
        Mock<ICache> runtimeCacheMock;
        Mock<ICache> diskCacheMock;


        [TestInitialize]
        public void Initialize()
        {
            fooResponse = new Foo() { UserName = userName, Password = password };
            runtimeCacheMock = new Mock<ICache> { };
            diskCacheMock = new Mock<ICache> { };
            cache = new CacheService
            {
                runtimeCache = runtimeCacheMock.Object,
                diskCache = diskCacheMock.Object
            };
        }


        [TestCleanup]
        public void Cleanup()
        {
            cache.Remove(cacheKey);
            cache = null;
            runtimeCacheMock = null;
            diskCacheMock = null;
        }


        [TestMethod]
        public void Do_not_call_add_on_runtime_nor_disk_cache_when_adding_an_object_with_an_empty_key()
        {
            //Arrange

            //Act
            cache.Add(string.Empty, fooResponse);

            //Assert
            runtimeCacheMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<Foo>()), Times.Never);
            runtimeCacheMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<Foo>()), Times.Never);
        }


        [TestMethod]
        public void Do_not_call_add_on_runtime_nor_disk_cache_when_adding_an_object_with_a_null_key()
        {
            //Arrange

            //Act
            cache.Add(null, fooResponse);

            //Assert
            runtimeCacheMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<Foo>()), Times.Never);
            runtimeCacheMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<Foo>()), Times.Never);
        }


        [TestMethod]
        public void Call_add_on_runtime_cache_when_adding_an_object_with_valid_key()
        {
            //Arrange

            //Act
            cache.Add(cacheKey, fooResponse);

            //Assert
            runtimeCacheMock.Verify(m => m.Add(cacheKey, fooResponse), Times.Once);
        }


        [TestMethod]
        public void Call_add_on_disk_cache_when_adding_an_object_with_valid_key()
        {
            //Arrange

            //Act
            cache.Add(cacheKey, fooResponse);

            //Assert
            diskCacheMock.Verify(m => m.Add(cacheKey, fooResponse), Times.Once);
        }


        [TestMethod]
        public void Do_not_call_remove_on_runtime_nor_disk_cache_when_removing_an_object_with_an_empty_key()
        {
            //Arrange

            //Act
            cache.Remove(string.Empty);

            //Assert
            runtimeCacheMock.Verify(m => m.Remove(It.IsAny<string>()), Times.Never);
            runtimeCacheMock.Verify(m => m.Remove(It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void Do_not_call_remove_on_runtime_nor_disk_cache_when_removing_an_object_with_a_null_key()
        {
            //Arrange

            //Act
            cache.Remove(null);

            //Assert
            runtimeCacheMock.Verify(m => m.Remove(It.IsAny<string>()), Times.Never);
            runtimeCacheMock.Verify(m => m.Remove(It.IsAny<string>()), Times.Never);
        }


        [TestMethod]
        public void Call_remove_on_runtime_cache_when_removing_an_object_with_valid_key()
        {
            //Arrange

            //Act
            cache.Remove(cacheKey);

            //Assert
            runtimeCacheMock.Verify(m => m.Remove(cacheKey), Times.Once);
        }


        [TestMethod]
        public void Call_remove_on_disk_cache_when_removing_an_object_with_valid_key()
        {
            //Arrange

            //Act
            cache.Remove(cacheKey);

            //Assert
            diskCacheMock.Verify(m => m.Remove(cacheKey), Times.Once);
        }


        [TestMethod]
        public void Return_a_null_value_if_getting_an_empty_key()
        {
            //Arrange

            //Act
            Foo response = cache.Get<Foo>(string.Empty);

            //Assert
            Assert.IsNull(response);
        }


        [TestMethod]
        public void Return_a_null_value_if_getting_non_cached_object()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);
            diskCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.IsNull(response);
        }

        /* Disabled Runtime Cache
        [TestMethod]
        public void Return_a_valid_value_if_getting_cached_object_on_runtime()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns(fooResponse);
            diskCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.AreEqual(response, fooResponse);
        }
        */


        [TestMethod]
        public void Return_a_valid_value_if_getting_cached_object_on_disk()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);
            diskCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns(fooResponse);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.AreEqual(response, fooResponse);
        }


        /* Disabled Runtime Cache
        [TestMethod]
        public void Add_object_to_runtime_when_getting_a_cached_object_on_disk()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);
            diskCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns(fooResponse);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            runtimeCacheMock.Verify(m => m.Add(cacheKey, fooResponse), Times.Once);
        }*/


        [TestMethod]
        public void Do_not_add_object_to_runtime_when_getting_a_non_cached_object_on_disk()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);
            diskCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns<Foo>(null);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            runtimeCacheMock.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<Foo>()), Times.Never);
        }


        /* Disabled Runtime Cache
        [TestMethod]
        public void Do_not_call_disk_cache_when_return_a_valid_object_cached_in_runtime()
        {
            //Arrange
            runtimeCacheMock.Setup(f => f.Get<Foo>(cacheKey)).Returns(fooResponse);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.AreEqual(response, fooResponse);
        }
        */
        
    }
}
