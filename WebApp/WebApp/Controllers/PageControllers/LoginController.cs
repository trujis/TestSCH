using Core.Services;
using Core.Services.Cache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Users.Enums;
using Users.Models;
using WebApp.Enums;
using WebApp.Handlers;
using WebApp.Models;

namespace WebApp.Controllers.PageControllers
{
    
    public class LoginController : MasterController
    {
        

        public ActionResult Index(PageErrorEnum e = 0, string a = "", string c = "")
        {
            ViewBag.Title = "Login Page";

            if (e == PageErrorEnum.UNAUTHORIZED)
            {
                ModelState.AddModelError("error", "Stop! First you have to log in...");
            }

            return View(new LoginViewModel() { RedirectionAction = a, RedirectionController = c});
        }


        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            ActionResult response;

            http.SetCredentials(model.UserName, model.Password);
            User userApi = http.Get<User>(GetApiUrl("api/user"), out HttpStatusCode statusCode);

            if (IsUserValid(userApi))
            {
                response = GetPageRedirection(userApi, model.RedirectionAction, model.RedirectionController);
                
                if (response != null)
                {
                    model.Roles = userApi.Roles;
                    Session["user"] = model;
                }
                else
                {
                    response = GetInvalidLogin(model);
                }
            }
            else
            {
                response = GetInvalidLogin(model);
            }

            return response;
        }


        private bool IsUserValid(User user)
        {
            return user?.Roles?.Count > 0
                && !string.IsNullOrEmpty(user.UserName)
                && user.Roles.Where(m => Enum.IsDefined(typeof(RolesEnum), m)).Count() > 0;
        }


        private bool UserContainsRole(User user)
        {
            return user?.Roles != null
                && user.Roles.Where(m => Enum.IsDefined(typeof(RolesEnum), m)).Count() > 0;
        }


        private ActionResult GetInvalidLogin(LoginViewModel user)
        {
            ModelState.AddModelError("error", "Invalid login attempt.");
            return View(user);
        }


        private RedirectToRouteResult GetPageRedirection(User user, string action = "", string controller = "")
        {
            RedirectToRouteResult pageRedirect = null;

            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(controller))
            {
                pageRedirect = RedirectToAction(action, controller);
            }
            else if(user.ContainsRole(RolesEnum.ADMIN))
            {
                pageRedirect = GetRedirection(RedirectionHandler.ADMIN);
            }
            else if (user.ContainsRole(RolesEnum.PAGE_1))
            {
                pageRedirect = GetRedirection(RedirectionHandler.PAGE_1);
            }
            else if (user.ContainsRole(RolesEnum.PAGE_2))
            {
                pageRedirect = GetRedirection(RedirectionHandler.PAGE_2);
            }
            else if (user.ContainsRole(RolesEnum.PAGE_3))
            {
                pageRedirect = GetRedirection(RedirectionHandler.PAGE_3);
            }

            return pageRedirect;
        }


        protected override bool HasRole()
        {
            return true;
        }
    }
}
