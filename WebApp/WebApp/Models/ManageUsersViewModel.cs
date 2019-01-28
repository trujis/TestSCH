using Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Users.Enums;

namespace WebApp.Models
{
    public class ManageUsersViewModel
    {


        public ManageUsersViewModel()
        {
            Login = new LoginViewModel();
        }


        public LoginViewModel Login;


        [Required]
        [Display(Name = "User Name:")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "User Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

        [Display(Name = "Roles:")]
        public List<RolesEnum> Roles { get; set; }


        [Display(Name = "Roles:")]
        public List<string> RolesAvailable
        {
            get
            {
                List<string> response = new List<string>();

                var values = Enum.GetValues(typeof(RolesEnum));

                foreach (var value in values)
                {
                    if (((RolesEnum)value) != RolesEnum.ADMIN)
                    {
                        response.Add(((RolesEnum)value).GetDisplayAttributeFrom(typeof(RolesEnum)));
                    }
                }
                return response;
            }
        }
        
    }
}