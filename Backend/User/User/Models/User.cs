using Core.Services.Cache;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Users.Enums;

namespace Users.Models
{
    public class User
    {

        public string UserName { get; set; }
        public string Password { private get; set; }
        public List<RolesEnum> Roles { get; set; }
        private ICache cache;


        public User()
        {
            cache = new CacheService(); //IOC only when used
        }


        /// <summary>
        /// Checks if password is the same than the user
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool HasSamePassword(string password)
        {
            return (!string.IsNullOrEmpty(password))
                ? password.Equals(Password)
                : false;
        }


        /// <summary>
        /// Checks if user has the given Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool ContainsRole(RolesEnum role)
        {
            return Roles != null 
                && Roles.Count > 0 
                && Roles.Contains(role);
        }


        
        /// <summary>
        /// I'm not very fan of doing operations in a Model, but, considering that
        /// password must have private read, if I did a save operation in Repository,
        /// password is not saved.
        /// 
        /// I'm sure that there is a better option than this.
        /// </summary>
        public void Save()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                dynamic e = new ExpandoObject();
                e.UserName = UserName;
                e.Password = Password;
                e.Roles = Roles;

                cache.Add(UserName, e);
            }
        }


        /// <summary>
        /// Because of Runtime cache... In runtime the object is stored as dynamic. 
        /// There's a need to map into User.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static User Map(dynamic obj)
        {
            return new User()
            {
                UserName = obj?.UserName ?? string.Empty,
                Password = obj?.Password ?? string.Empty,
                Roles = (obj.Roles != null) ? obj.Roles.ToObject<List<RolesEnum>>() : new List<RolesEnum>()
            };
        }
    }
}
