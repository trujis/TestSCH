using System.Collections.Generic;
using Users.Models;

namespace Users.Get
{
    public interface IUserGetRepository
    {

        User Get(string userName);

        List<string> ListNames();

    }
}