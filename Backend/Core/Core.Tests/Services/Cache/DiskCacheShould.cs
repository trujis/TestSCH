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
    public class DiskCacheShould
    {


        readonly string cacheKey = "KEY";
        readonly string userName = "UserName";
        readonly string password = "Password";
        Foo fooResponse;
        ICache cache;


        [TestInitialize]
        public void Initialize()
        {
            fooResponse = new Foo() { UserName = userName, Password = password };
            cache = new DiskCache();
            cache.Remove(cacheKey);
        }


        [TestCleanup]
        public void Cleanup()
        {
            cache.Remove(cacheKey);
            cache = null;
        }


        [TestMethod]
        public void Return_a_null_value_if_gettint_with_an_empty_key()
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

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.IsNull(response);
        }


        [TestMethod]
        public void Return_a_null_value_if_getting_a_cached_value_but_with_different_keys()
        {
            //Arrange
            cache.Add(cacheKey, fooResponse);

            //Act
            Foo response = cache.Get<Foo>(cacheKey+"Other");

            //Assert
            Assert.IsNull(response);
        }


        [TestMethod]
        public void Return_cached_value_when_adding_correctly()
        {
            //Arrange
            cache.Add(cacheKey, fooResponse);

            //Act
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.AreEqual(response, fooResponse);
        }


        [TestMethod]
        public void Return_null_value_when_removing_a_valid_inserted_object()
        {
            //Arrange
            cache.Add(cacheKey, fooResponse);

            //Act
            cache.Remove(cacheKey);
            Foo response = cache.Get<Foo>(cacheKey);

            //Assert
            Assert.IsNull(response);
        }


        
        
    }
}
