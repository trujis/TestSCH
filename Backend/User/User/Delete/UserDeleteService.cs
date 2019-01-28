using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Models;

namespace Users.Delete
{

    /// <summary>
    /// Class to get Users
    /// </summary>
    public class UserDeleteService : IUserDeleteService
    {


        public IUserDeleteRepository userRepository;


        public UserDeleteService()
        {
            userRepository = new UserDeleteRepository();
        }

        
        /// <summary>
        /// Deletes a User by the given UserName
        /// </summary>
        /// <param name="user"></param>
        public void Delete(string userName)
        {
            userRepository.Delete(userName);
        }
    }
}
