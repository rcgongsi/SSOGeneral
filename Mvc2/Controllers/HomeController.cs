using SSO.Helper;
using System.Web.Mvc;

namespace Mvc2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public const string cookieName = "CookieMVC2";

        // GET: Home
        public ActionResult Index()
        {
            //TempData["UserData"] = SSOGeneralSameDomain.GetCookieValue("CookiesTest", HttpContext);
            TempData["UserData"] = new SSOCrossDomain(HttpContext).GetUserData(cookieName);
            return View();
        }

        [AllowAnonymous]
        public void Login()
        {
            SSOCrossDomain cross = new SSOCrossDomain(HttpContext);
            cross.ValidationLogIn(cookieName, new System.TimeSpan(0, 2, 0));
        }
    }
}