using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using WebApp.Handlers;

namespace WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration
            var mobileHandler = GetSecureHandler(config);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "User Get Controller",
                routeTemplate: "api/user/",
                defaults: new { action = "Get", controller = "User"},
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                handler: mobileHandler
            );

            config.Routes.MapHttpRoute(
                name: "User List Controller",
                routeTemplate: "api/user/list",
                defaults: new { action = "List", controller = "User"},
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) },
                handler: mobileHandler
            );

            config.Routes.MapHttpRoute(
                name: "User Post Controller",
                routeTemplate: "api/user/",
                defaults: new { action = "Post", controller = "User"},
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) },
                handler: mobileHandler
            );

            config.Routes.MapHttpRoute(
                name: "User Delete Controller",
                routeTemplate: "api/user/{userName}",
                defaults: new { action = "Delete", controller = "User"},
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) },
                handler: mobileHandler
            );
        }

        private static HttpMessageHandler GetSecureHandler(HttpConfiguration config)
        {
            DelegatingHandler[] handlers = new DelegatingHandler[] {
                new BasicAuthenticationHandler()
            };

            return HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);
        }
    }
}
