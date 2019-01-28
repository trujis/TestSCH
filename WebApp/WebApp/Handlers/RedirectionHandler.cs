using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Handlers
{
    public class RedirectionHandler
    {


        //private static string nombreWeb = "";
        public static RedirectionHandler LOGIN = new RedirectionHandler("Index", "Login");
        public static RedirectionHandler ADMIN = new RedirectionHandler("Index", "Admin");
        public static RedirectionHandler PAGE_1 = new RedirectionHandler("Index", "Page_1");
        public static RedirectionHandler PAGE_2 = new RedirectionHandler("Index", "Page_2");
        public static RedirectionHandler PAGE_3 = new RedirectionHandler("Index", "Page_3");
        public string Action { get; private set; }
        public string Controller { get; private set; }


        private RedirectionHandler(string action, string controller)
        {
            Action = action;
            Controller = controller;
        }
        
        /*
        public static void init(string _nombreWeb)
        {
            if (_nombreWeb != "/")
                nombreWeb = _nombreWeb;
        }
        */
    }
}