using System;
using System.Collections.Generic;
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
    public class AdminController : MasterController
    {


        readonly static string VIEW = "Admin";


        public ActionResult Index()
        {
            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.ADMIN) )
            {
                return errorAction;
            }
            
            return View(VIEW, (LoginViewModel)Session["user"]);
        }


        protected override bool HasRole()
        {
            return ((LoginViewModel)Session["user"]).ContainsRole(Users.Enums.RolesEnum.ADMIN);
        }
    }
}
