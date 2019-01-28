using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Get;
using Users.Models;

namespace Users.Save
{

    /// <summary>
    /// Class to save Users into repository.
    /// </summary>
    public class UserSaveService : IUserSaveService
    {


        public IUserSaveRepository userRepository;
        public IUserGetService userGetService;


        public UserSaveService()
        {
            userRepository = new UserSaveRepository();
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
            bool response = false;

            if (IsValid(userName, password, role))
            {
                if (!userGetService.Exists(userName))
                {
                    User user = new User()
                    {
                        UserName = userName,
                        Password = password,
                        Roles = new List<RolesEnum> { role }
                    };

                    userRepository.Save(user);
                    response = true;
                }
            }

            return response;
        }


        public void AddRole(User user, RolesEnum role)
        {
            if (user != null)
            {
                if (user.Roles == null)
                {
                    user.Roles = new List<RolesEnum>();
                }

                user.Roles.Add(role);

                userRepository.Save(user);
            }
        }


        private bool IsValid(string userName, string password, RolesEnum role)
        {
            return !string.IsNullOrEmpty(userName)
                && !string.IsNullOrEmpty(password);
        }

    }
}
