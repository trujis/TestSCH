using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Users.Enums;
using Users.Models;

namespace WebApp.Models
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "User Name:")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "User Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

        [Required]
        [Display(Name = "Roles:")]
        public List<RolesEnum> Roles { get; set; }


        public string RedirectionAction { get; set; }


        public string RedirectionController { get; set; }
        

        /// <summary>
        /// Checks if user has the given Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool ContainsRole(RolesEnum role)
        {
            return Roles != null
                && Roles.Count > 0
                && (Roles.Contains(role)
                    || Roles.Contains(RolesEnum.ADMIN));
        }
    }
}