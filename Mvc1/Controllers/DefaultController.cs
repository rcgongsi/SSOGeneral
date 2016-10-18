using SSO.General.Helper;
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
        // GET: Default
        public ActionResult Index()
        {
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
            SSOGeneral sso = new SSOGeneralSameDomain("CookiesTest", new TimeSpan(0, 1, 0), HttpContext);
            sso.LogIn(name);
        }
    }
}