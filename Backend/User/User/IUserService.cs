using Users.Delete;
using Users.Get;
using Users.Save;

namespace Users
{
    public interface IUserService : IUserGetService, IUserSaveService, IUserDeleteService
    {

    }
}