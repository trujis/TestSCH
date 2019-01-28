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
namespace Users.Get
{

    internal class UserGetRepository : IUserGetRepository
    {


        internal ICache cache;
        internal IUserSaveRepository repository;
        private static bool firstTime = true; //Crazy!!! Just to have some users at first start
        internal static readonly List<User> defaultUsers = new List<User>()
        {
            new User() { UserName = "ADMIN", Password = "ADMIN", Roles = new List<RolesEnum>() { RolesEnum.ADMIN } },
            new User() { UserName = "USER1", Password = "USER1", Roles = new List<RolesEnum>() { RolesEnum.PAGE_1 } },
            new User() { UserName = "USER2", Password = "USER2", Roles = new List<RolesEnum>() { RolesEnum.PAGE_2} },
            new User() { UserName = "USER3", Password = "USER3", Roles = new List<RolesEnum>() { RolesEnum.PAGE_3} },
            new User() { UserName = "USER4", Password = "USER4", Roles = new List<RolesEnum>() { RolesEnum.PAGE_1, RolesEnum.PAGE_2, RolesEnum.PAGE_3} },
        };


        public UserGetRepository()
        {
            cache = new CacheService(); //TODO: IOC
            repository = new UserSaveRepository(); //TODO: IOC
        }


        public User Get(string userName)
        {
            TrySaveFirstTime(); //Crazy stuff...

            return GetCached(userName);
        }


        public List<string> ListNames()
        {
            return cache.ListFileNames();
        }


        /// <summary>
        /// This is absolute crazy. I'm doing just to have 'default' users stored...
        /// </summary>
        private void TrySaveFirstTime()
        {
            if (firstTime)
            {
                firstTime = false;

                foreach (User user in defaultUsers)
                {
                    User userCached = GetCached(user.UserName);
                    if (userCached == null || string.IsNullOrEmpty(userCached.UserName))
                    {
                        repository.Save(user);
                    }
                }
            }
        }


        private User GetCached(string userName)
        {
            User response = new User();

            dynamic obj = cache.Get<dynamic>(userName);

            if (obj != null)
            {
                response = User.Map(obj);
            }

            return response;
        }
    }
}
