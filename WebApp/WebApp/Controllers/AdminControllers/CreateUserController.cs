using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Users.Enums;
using Users.Models;
using WebApp.Handlers;
using WebApp.Models;

namespace WebApp.Controllers.PageControllers.AdminControllers
{
    //If I could use Roles...
    //[Authorize(Roles = "ADMIN")]
    public class CreateUserController : MasterController
    {
        

        readonly static string VIEW = "~/Views/Admin/ManageUsers.cshtml";


        public ActionResult Index()
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }

            return View(VIEW, new ManageUsersViewModel() { Login = (LoginViewModel)Session["user"] });
        }


        [HttpPost]
        public ActionResult Index(ManageUsersViewModel model)
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }
            model.Login = (LoginViewModel)Session["user"];

            if (IsUserValid(model))
            {
                http.SetCredentials(model.Login.UserName, model.Login.Password);
                bool userApi = http.Post<bool>(GetApiUrl("api/user"), out HttpStatusCode statusCode, GetUser(model));

                if (statusCode == HttpStatusCode.Conflict)
                {
                    return GetError(VIEW, model, "Error!! UserName already in use.");
                }
                else if (!userApi)
                {
                    return GetError(VIEW, model, "Error!! Eeeeehhmm... unknown error :(");
                }
            }
            else
            {
                return GetError(VIEW, model, "Error!! Invalid parameters!.");
            }

            return GetRedirection(RedirectionHandler.ADMIN);
        }


        public ExpandoObject GetUser(ManageUsersViewModel model)
        {
            dynamic e = new ExpandoObject();
            e.UserName = model.UserName;
            e.Password = model.Password;
            e.Roles = model.Roles;

            return e;
        }


        private bool IsUserValid(ManageUsersViewModel model)
        {
            return model?.Roles?.Count > 0
                && !string.IsNullOrEmpty(model.UserName)
                && !string.IsNullOrEmpty(model.Password)
                && model.Roles.Where(m => Enum.IsDefined(typeof(RolesEnum), m)).Count() > 0;
        }


        protected override bool HasRole()
        {
            return ((LoginViewModel)Session["user"]).ContainsRole(Users.Enums.RolesEnum.ADMIN);
        }
    }
}
