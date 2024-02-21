using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoogleAuthentication.Services;
using Newtonsoft.Json;

namespace ExternalLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cleintId = "";
            var url = "https://localhost:44394/Login/GoogleLoginCallback";
            var response = GoogleAuth.GetAuthUrl(cleintId, url);
            ViewBag.response = response;
            return View();
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}