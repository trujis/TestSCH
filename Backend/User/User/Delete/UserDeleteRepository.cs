using Core.Services;
using Core.Services.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Models;
using Users.Save;

[assembly: InternalsVisibleTo("Users.Test")]
namespace Users.Delete
{

    internal class UserDeleteRepository : IUserDeleteRepository
    {


        internal ICache cache;


        public UserDeleteRepository()
        {
            cache = new CacheService(); //TODO: IOC
        }


        /// <summary>
        /// Deletes a User by the given UserName
        /// </summary>
        /// <param name="user"></param>
        public void Delete(string userName)
        {
            cache.Remove(userName);
        }

    }
}
