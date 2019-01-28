using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Delete;
using Users.Enums;
using Users.Get;
using Users.Models;
using Users.Save;

namespace Users
{
    public class UserService : IUserService
    {


        internal IUserGetService getService;
        internal IUserSaveService saveService;
        internal IUserDeleteService deleteService;
        //internal IUserGetService getService;


        public UserService()
        {
            getService = new UserGetService();
            saveService = new UserSaveService();
            deleteService = new UserDeleteService();
        }


        /// <summary>
        /// Get User.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Get(string userName, string password)
        {
            return getService.Get(userName, password);
        }


        /// <summary>
        /// List the name of all Users
        /// </summary>
        /// <returns></returns>
        public List<string> ListNames()
        {
            return getService.ListNames();
        }


        /// <summary>
        /// Checks if a UserName exists in repository
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Exists(string userName)
        {
            return getService.Exists(userName);
        }


        public void AddRole(User user, RolesEnum role)
        {
            saveService.AddRole(user, role);
        }


        /// <summary>
        /// Saves a user in Repository
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool Save(string userName, string password, RolesEnum role)
        {
            return saveService.Save(userName, password, role);
        }


        /// <summary>
        /// Deletes a User by the given UserName
        /// </summary>
        /// <param name="user"></param>
        public void Delete(string userName)
        {
            deleteService.Delete(userName);
        }
    }
}
