using System.Security.Principal;
using Users.Models;

namespace WebApp.Handlers
{

    public class BasicAuthenticationIdentity : GenericIdentity
    {
        

        public string Password { get; set; }
        public User User;


        public BasicAuthenticationIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;
        }
        
    }

}