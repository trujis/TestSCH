using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Handlers;
using WebApp.Models;

namespace WebApp.Controllers.PageControllers
{
    //If I could use Roles...
    //[Authorize(Roles = "PAGE_1")]
    public class Page_1Controller : MasterController
    {
        

        public ActionResult Index()
        {
            ViewBag.Title = "Page_1";

            if (base.HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler.PAGE_1) )
            {
                return errorAction;
            }

            return View("PageContent", (LoginViewModel)Session["user"]);
        }


        protected override bool HasRole()
        {
            return ((LoginViewModel)Session["user"]).ContainsRole(Users.Enums.RolesEnum.PAGE_1);
        }
    }
}
