using System.Collections.Generic;
using Users.Models;

namespace Users.Get
{
    public interface IUserGetService
    {
        
        User Get(string userName, string password);

        List<string> ListNames();

        bool Exists(string userName);

    }
}