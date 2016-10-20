using SSO.Same.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc1.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public const string cookeName = "CookiesTest";
        // GET: Default
        public ActionResult Index()
        {
            TempData["UserData"] = SSOGeneralSameDomain.GetCookieValue(cookeName, HttpContext);
            return View();
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public void Login(string name)
        {
            SSOGeneral sso = new SSOGeneralSameDomain(cookeName, new TimeSpan(0, 1, 0), HttpContext);
            sso.LogIn(name);
        }
    }
}