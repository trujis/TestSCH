using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Users.Enums;
using WebApp.Enums;
using WebApp.Handlers;
using WebApp.Models;

[assembly: InternalsVisibleTo("WebApp.Tests")]
namespace WebApp.Controllers
{

    public abstract class MasterController : Controller
    {


        internal IHttpService http;


        public MasterController()
        {
            http = new HttpService(); //IOC
        }


        public ActionResult Logout()
        {
            Session["user"] = null;

            return RedirectToAction("Index", "Login");
        }


        protected bool IsUserLogged()
        {
            return Session != null && Session["user"] != null && Session["user"] is LoginViewModel;
        }


        protected RedirectToRouteResult GetRedirection(RedirectionHandler redirection, RedirectionHandler redirectionParams = null)
        {
            object route = null;

            if (redirectionParams != null)
            {
                route = new { e = PageErrorEnum.UNAUTHORIZED, a = redirectionParams.Action, c = redirectionParams.Controller };
            }

            return RedirectToAction(redirection.Action, redirection.Controller, route);
        }


        protected bool HasErrorsRenderingPage(out ActionResult errorAction, RedirectionHandler redirection = null)
        {
            bool hasError = false;
            errorAction = null;

            var model = new LoginViewModel();
            try
            {
                if (IsUserLogged())
                {
                    if (HasRole())
                    {
                        model = (LoginViewModel)Session["user"];
                    }
                    else
                    {
                        hasError = true;
                        errorAction = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    hasError = true;
                    errorAction = GetRedirection(RedirectionHandler.LOGIN, redirection);
                }
            }
            catch { }

            return hasError;
        }


        protected virtual string GetApiUrl(string path = "")
        {
            return string.Format("{0}{1}/{2}",
                HttpContext.Request.Url.GetLeftPart(UriPartial.Authority),
                HttpContext.Request.ApplicationPath,
                path);

            /*return string.Format("{0}://{1}:{2}/{3}",
                Request.Url.Scheme,
                Request.Url.Host,
                Request.Url.Port,
                path);*/
        }


        protected ActionResult GetError(string view, object model, string error)
        {
            ModelState.AddModelError("error", error);
            return View(view, model);
        }


        protected abstract bool HasRole();

    }
}