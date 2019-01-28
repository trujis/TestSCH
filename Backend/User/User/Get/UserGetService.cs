using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Models;

namespace Users.Get
{

    /// <summary>
    /// Class to get Users
    /// </summary>
    public class UserGetService : IUserGetService
    {


        public IUserGetRepository userRepository;


        public UserGetService()
        {
            userRepository = new UserGetRepository();
        }


        /// <summary>
        /// Get User.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Get(string userName, string password)
        {
            User response = null;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("UserName/Password must not be empty");
            }
            
            User userRepo = userRepository.Get(userName);

            if (userRepo != null && userRepo.HasSamePassword(password))
            {
                response = userRepo;
            }

            return response;
        }

        
        /// <summary>
        /// List the name of all Users
        /// </summary>
        /// <returns></returns>
        public List<string> ListNames()
        {
            List<string> response = userRepository.ListNames();

            string adminString = RolesEnum.ADMIN.GetDisplayAttributeFrom(typeof(RolesEnum));
            response.Remove(adminString);
            return response;
        }


        /// <summary>
        /// Checks if a UserName exists in repository
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Exists(string userName)
        {
            bool response = true;

            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("UserName must not be empty");
            }

            User userRepo = userRepository.Get(userName);

            response = userRepo != null 
                && !string.IsNullOrEmpty(userRepo.UserName)
                && userRepo.UserName.Equals(userName);

            return response;
        }


        public void Delete(User user)
        {

        }
    }
}
