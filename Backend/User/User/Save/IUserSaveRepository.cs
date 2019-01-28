using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;
using Users.Models;

namespace Users.Save
{
    public interface IUserSaveRepository
    {

        bool Save(User user);

    }
}
