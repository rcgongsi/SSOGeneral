using System.Web;

namespace SSO.General.Helper
{
    /// <summary>
    /// MVC操作方法
    /// </summary>
    public class OperationHttpContext : Operation
    {
        public HttpContext Context { get; set; }

        public OperationHttpContext(HttpContext context)
        {
            Context = context;
        }

        public override string GetRequest(string request)
        {
            return Context.Request[request];
        }

        public override void SetCookie(HttpCookie cookie)
        {
            Context.Response.Cookies.Add(cookie);
        }

        public override HttpCookie GetCookie(string cookieName)
        {
            return Context.Request.Cookies[cookieName];
        }

        public override void Redirect(string url)
        {
            Context.Response.Redirect(url);
        }
    }
}