using ExternalLogin.Models;
using GoogleAuthentication.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExternalLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        //public async Task<ActionResult> GoogleLoginCallback(string code)
        //{
        //    var secretId = "";
        //    var clientId = "";
        //    var url = "https://localhost:44394/Login/GoogleLoginCallback";
        //    var token =await GoogleAuth.GetAuthAccessToken(code, clientId, secretId, url);
        //    var userProfile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());
        //    var googleUser = JsonConvert.DeserializeObject<GoogleProfile>(userProfile);
        //    return View();
        //}

        public  ActionResult GoogleLoginCallback(string code)
        {
            if (code != null)
            {
                var client = new RestClient("https://www.googleapis.com/oauth2/v4/token");
                var request = new RestRequest("",Method.Post);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://localhost:44394/Login/GoogleLoginCallback");
                request.AddParameter("client_id", "");
                request.AddParameter("client_secret", "");
                RestResponse restResponse = client.Execute(request);
                var content = restResponse.Content;
                var res = (JObject)JsonConvert.DeserializeObject(content);
                var client2 = new RestClient("https://www.googleapis.com/oauth2/v1/userinfo");
                client2.AddDefaultHeader("Authorization", "Bearer" + res["access_token"]);
                request = new RestRequest("",Method.Get);
                var response2 = client2.Execute(request);
                var content2 = response2.Content;
                var user = (JObject)JsonConvert.DeserializeObject(content2);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}