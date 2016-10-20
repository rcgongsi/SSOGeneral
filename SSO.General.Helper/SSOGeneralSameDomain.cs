using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SSO.Same.Domain
{
    /// <summary>
    /// 同一域名单点登录
    /// </summary>
    public class SSOGeneralSameDomain : SSOGeneral
    {
        private Operation Operation { get; set; }

        public SSOGeneralSameDomain(string cookieName, TimeSpan overdue, Page page) : base(cookieName, overdue)
        {
            Operation = new OperationPage(page);
        }

        public SSOGeneralSameDomain(string cookieName, TimeSpan overdue, HttpContextBase context) : base(cookieName, overdue)
        {
            Operation = new OperationHttpContext(context);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userData">用户额外信息</param>
        public override void LogIn(string userData)
        {
            FormsAuthenticationTicket ticket = CreateTicket(userData);
            CreateCookie(ticket);
            RedirectPage();
        }

        /// <summary>
        /// 登录成功跳转
        /// </summary>
        private void RedirectPage()
        {
            if (Operation.GetRequest("link") != "")
            {
                Operation.Redirect(Operation.GetRequest("link") + Operation.GetRequest("ReturnUrl"));
                return;
            }
            if (Operation.GetRequest("ReturnUrl") != "")
            {
                Operation.Redirect(Operation.GetRequest("ReturnUrl"));
                return;
            }
            Operation.Redirect("/");
        }

        /// <summary>
        /// 创建Cookie
        /// </summary>
        private void CreateCookie(FormsAuthenticationTicket ticket)
        {
            HttpCookie cookie = new HttpCookie(ticket.Name, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = ticket.Expiration;
            Operation.SetCookie(cookie);
        }

        /// <summary>
        /// 创建用户凭据
        /// </summary>
        private FormsAuthenticationTicket CreateTicket(string userData)
        {
            return new FormsAuthenticationTicket(1, CookieName, DateTime.Now, DateTime.Now.Add(OverdueInterval), true, userData);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        public static void LogUp()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// 获取当前登录用户绑定值
        /// </summary>
        /// <returns>对应的值</returns>
        public static string GetCookieValue(string cookieName, Page page)
        {
            Operation Operation = new OperationPage(page);
            string result = Operation.GetCookie(cookieName)?.Value;
            return result != null ? FormsAuthentication.Decrypt(result).UserData : "";
        }

        /// <summary>
        /// 获取当前登录用户绑定值
        /// </summary>
        /// <returns>对应的值</returns>
        public static string GetCookieValue(string cookieName, HttpContextBase context)
        {
            Operation Operation = new OperationHttpContext(context);
            string result = Operation.GetCookie(cookieName)?.Value;
            return result != null ? FormsAuthentication.Decrypt(result).UserData : "";
        }

    }
}