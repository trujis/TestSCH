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
    public class SelectUserToUpdateViewModel
    {


        public SelectUserToUpdateViewModel()
        {
            Login = new LoginViewModel();
        }


        public LoginViewModel Login;

        
        [Display(Name = "Users:")]
        public List<string> UsersAvailable { get; set; }


        [Display(Name = "User to edit:")]
        public string UserSelected { get; set; }
        
    }
}