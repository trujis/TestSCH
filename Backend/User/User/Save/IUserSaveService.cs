using Users.Enums;
using Users.Models;

namespace Users.Save
{
    public interface IUserSaveService
    {
        
        bool Save(string userName, string password, RolesEnum role);

        void AddRole(User user, RolesEnum role);

    }
}