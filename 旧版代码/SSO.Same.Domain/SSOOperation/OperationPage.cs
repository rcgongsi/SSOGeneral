using System.Web;
using System.Web.UI;

namespace SSO.Same.Domain
{
    /// <summary>
    /// WebForm操作方法
    /// </summary>
    public class OperationPage : Operation
    {
        public Page Page { get; set; }

        public OperationPage(Page page)
        {
            Page = page;
        }

        public override string GetRequest(string request)
        {
            string result = Page.Request[request];
            return result ?? "";
        }

        public override void SetCookie(HttpCookie cookie)
        {
            Page.Response.Cookies.Add(cookie);
        }

        public override HttpCookie GetCookie(string cookieName)
        {
            return Page.Request.Cookies[cookieName];
        }

        public override void Redirect(string url)
        {
            Page.Response.Redirect(url);
        }
    }
}