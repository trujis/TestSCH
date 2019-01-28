using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Users.Get;
using Users.Models;

namespace WebApp.Handlers
{
    public class UserAuthorization : AuthorizeAttribute
    {


        IUserGetService userGetService;


        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var identity = Thread.CurrentPrincipal.Identity;

            if (identity != null && identity.IsAuthenticated)
            {
                userGetService = new UserGetService(); //IOC

                var basicAuth = identity as BasicAuthenticationIdentity;

                User user = userGetService.Get(basicAuth.Name, basicAuth.Password);

                if (user != null)
                {
                    basicAuth.User = user;
                    SetPrincipal(Thread.CurrentPrincipal);
                    return true;
                }
            }

            return false;
        }


        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }


    }
}