using System.Collections.Generic;
using Users.Models;

namespace Users.Delete
{
    public interface IUserDeleteRepository
    {

        void Delete(string user);

    }
}