using SSO.Helper;
using System;
using System.Web.Mvc;

namespace Mvc1.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public const string cookieName = "CookieMVC1";

        // GET: Default
        public ActionResult Index()
        {
            //TempData["UserData"] = SSOGeneralSameDomain.GetCookieValue(cookeName, HttpContext);
            TempData["UserData"] = new SSOCrossDomain(HttpContext).GetUserData(cookieName);
            return View();
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            TempData["RedirectUrl"] = Request.QueryString["link"];
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JavaScriptResult Login(string name)
        {
            //SSOSameDomain sso = new SSOSameDomain(HttpContext);
            //sso.LogIn(cookeName, new TimeSpan(0, 1, 0), name);
            SSOCrossDomain sso = new SSOCrossDomain(HttpContext);
            sso.LogIn(cookieName, new TimeSpan(0, 3, 0), name, TempData["RedirectUrl"]?.ToString());
            return JavaScript(sso.Operation.PerformJavascript);
        }
    }
}