using System.Web.Mvc;

namespace Mvc2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //TempData["UserData"] = SSOGeneralSameDomain.GetCookieValue("CookiesTest", HttpContext);
            return View();
        }
    }
}