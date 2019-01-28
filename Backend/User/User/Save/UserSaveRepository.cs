using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Models;

namespace Users.Save
{
    class UserSaveRepository : IUserSaveRepository
    {

        public bool Save(User user)
        {
            bool response = false;

            if (user != null)
            {
                user.Save();
                response = true;
            }

            return response;
        }

    }
}
