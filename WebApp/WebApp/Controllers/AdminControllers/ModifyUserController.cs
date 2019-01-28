using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class ModifyUserController : MasterController
    {


        readonly static string VIEW = "~/Views/Admin/SelectUserToUpdate.cshtml";


        public ActionResult Index()
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }

            http.SetCredentials(((LoginViewModel)Session["user"]).UserName, ((LoginViewModel)Session["user"]).Password);
            List<string> usersApi = http.Get<List<string>>(GetApiUrl("api/user/list"), out HttpStatusCode statusCode);

            SelectUserToUpdateViewModel model = new SelectUserToUpdateViewModel()
            {
                Login = (LoginViewModel)Session["user"]
            };

            if (statusCode == HttpStatusCode.OK)
            {
                model.UsersAvailable = usersApi;
            }
            else
            {
                ModelState.AddModelError("error", "Error!! Eeeeehhmm... unknown error :(");
            }

            return View(VIEW, model);
        }
        

        [HttpPost]
        public ActionResult Index(SelectUserToUpdateViewModel model, string command)
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }
            model.Login = (LoginViewModel)Session["user"];

            string action = Request.Params["action"];
            if (action == "Modify")
            {
                return Modify(model);
            }
            else if (action == "Delete")
            {
                return Delete(model);
            }
            
            return GetError(VIEW, model, "ToDo");
        }


        public ActionResult Modify(SelectUserToUpdateViewModel model)
        {
            return GetError(VIEW, model, "ToDo");
        }

        
        private ActionResult Delete(SelectUserToUpdateViewModel model)
        {
            if (string.IsNullOrEmpty(model.UserSelected))
            {
                return GetError(VIEW, model, "Deleting an empty UserName is a bad idea");
            }

            http.SetCredentials(model.Login.UserName, model.Login.Password);
            bool usersApi = http.Delete<bool>(GetApiUrl("api/user/"+ model.UserSelected), out HttpStatusCode statusCode);

            if (statusCode == HttpStatusCode.OK)
            {
                return GetRedirection(RedirectionHandler.ADMIN);
            }
            else
            {
                return GetError(VIEW, model, "Error!! Eeeeehhmm... unknown error :(");
            }
        }

        

        public ActionResult UpdateUsers()
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }
            /*
            http.SetCredentials(((LoginViewModel)Session["user"]).UserName, ((LoginViewModel)Session["user"]).Password);
            List<string> usersApi = http.Get<List<string>>(GetApiUrl("api/user/list"), out HttpStatusCode statusCode);
            */
            UpdateUserViewModel model = new UpdateUserViewModel()
            {
                Login = (LoginViewModel)Session["user"]
            };

            //if (statusCode == HttpStatusCode.OK)
            {
                //model.UsersAvailable = usersApi;
                return View(model);
            }
            /*else
            {
                return GetError(model, "Error!! Eeeeehhmm... unknown error :(");
            }*/
        }


        [HttpPost]
        public ActionResult UpdateUsers(ManageUsersViewModel model)
        {

            ModelState.AddModelError("error", "UpdateUsers");
            return View(model);
            /*
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN))
            {
                return errorAction;
            }
            model.Login = (LoginViewModel)Session["user"];

            if (IsUserValid(model))
            {

                http.SetCredentials(model.Login.UserName, model.Login.Password);
                bool userApi = http.Post<bool>(GetApiUrl("api/user"), out HttpStatusCode statusCode, new User() { UserName = model.UserName, Password = model.Password, Roles = model.Roles });

                if (statusCode == HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError("error", "Error!! UserName already in use.");
                    return View(model);
                }
                else if (!userApi)
                {
                    ModelState.AddModelError("error", "Error!! Eeeeehhmm... unknown error :(");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("error", "Error!! Invalid parameters!.");
                return View(model);
            }

            return GetRedirection(RedirectionHandler.ADMIN);
            */
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


        private ActionResult GetError(object model, string error)
        {
            ModelState.AddModelError("error", error);
            return View(model);
        }
    }
}
