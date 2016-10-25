using SSO.Helper.HTTPOperation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SSO.Helper
{
    /// <summary>
    /// 跨域单点登录
    /// </summary>
    public class SSOCrossDomain
    {
        public Operation Operation { get; set; }
        private dynamic HttpContextType;

        public SSOCrossDomain(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
            HttpContextType = context;
        }

        public SSOCrossDomain(Page page)
        {
            Operation = new OperationPage(page);
            HttpContextType = page;
        }

        /// <summary>
        /// 用户登录授权
        /// <param name="userData">用户信息</param>
        /// </summary>
        public void LogIn(string cookieName, TimeSpan overdue, string userData, string redirectUrl = "")
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, cookieName, DateTime.Now, DateTime.Now.Add(overdue), true, userData);
            CreateCookie(ticket);
            PerformJavascript("logIn", redirectUrl, userData);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
            string loginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            if (string.IsNullOrEmpty(loginUrl))
            {
                string authorizeUrl = ConfigurationManager.AppSettings["AuthorizeUrl"];
                Operation.Redirect(authorizeUrl + "&logType=logOut");
                return;
            }
            PerformJavascript("logOut", "");
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        public string GetUserData(string cookieName)
        {
            string result = Operation.GetCookie(cookieName)?.Value;
            return result != null ? FormsAuthentication.Decrypt(result).UserData : "";
        }

        /// <summary>
        /// 验证登录用户
        /// </summary>
        public void ValidationLogIn(string cookieName, TimeSpan overdue)
        {
            string logTypeParameter = Operation.GetRequest("logType");
            string redirectLink = Operation.GetRequest("link");
            if (string.IsNullOrEmpty(logTypeParameter))
            {
                string authorizeUrl = ConfigurationManager.AppSettings["AuthorizeUrl"];
                if (string.IsNullOrEmpty(authorizeUrl))
                {
                    return;
                }
                else
                {
                    Operation.Redirect(authorizeUrl);
                    return;
                }
            }
            SSOSameDomain sameDomain = new SSOSameDomain(HttpContextType);
            switch (logTypeParameter)
            {
                case "logIn":
                    sameDomain.LogIn(cookieName, overdue, Operation.GetRequest("userData"));
                    break;

                case "logOut":
                    FormsAuthentication.SignOut();
                    if (string.IsNullOrEmpty(redirectLink))
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                    {
                        Operation.Redirect(redirectLink);
                    }
                    break;

                default:
                    throw new InvalidOperationException("登录认证状态无效");
            }
        }

        /// <summary>
        /// 执行前端js跳转，授权
        /// </summary>
        private void PerformJavascript(string logType, string redirectLink, string userData = "")
        {
            Uri uri = Operation.Uri();
            string redirectUrl = "";
            if (string.IsNullOrEmpty(redirectLink))
            {
                redirectUrl = GetPageUrl();
                //如果返回网址包含Http，则直接跳转。不包含则本网址内跳转
                if (!redirectUrl.Contains("http"))
                {
                    redirectUrl = uri.Scheme + "://" + uri.Authority + GetPageUrl();
                }
            }
            else
            {
                redirectUrl = redirectLink;
            }
            StringBuilder resultMethod = new StringBuilder("LogIn('" + redirectUrl + "',");
            foreach (string url in GetUrlList())
            {
                resultMethod.Append("'");
                resultMethod.Append(string.Format("{0}?logType={1}&userData={2}", url, logType, userData));
                resultMethod.Append("',");
            }
            resultMethod.Remove(resultMethod.Length - 1, 1);
            resultMethod.Append(")");
            Operation.PerformJs("<script>" + resultMethod + "</script>");
        }

        /// <summary>
        /// 获取需要跳转的URL
        /// </summary>
        private string GetPageUrl()
        {
            if (!string.IsNullOrEmpty(Operation.GetRequest("link")))
            {
                return Operation.GetRequest("link") + Operation.GetRequest("ReturnUrl");
            }
            if (!string.IsNullOrEmpty(Operation.GetRequest("ReturnUrl")))
            {
                return Operation.GetRequest("ReturnUrl");
            }
            return "/";
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
        /// 获取所有可登陆的网址
        /// </summary>
        private IList<string> GetUrlList()
        {
            List<string> urlList = new List<string>();
            string urls = ConfigurationManager.AppSettings["LoginUrl"];
            if (string.IsNullOrEmpty(urls))
            {
                return urlList;
            }
            if (urls.IndexOf(',') < 0)
            {
                urlList.Add(urls);
            }
            else
            {
                urlList.AddRange(urls.Split(','));
                urlList.Remove(""); //避免配置时结尾为逗号
            }

            return urlList;
        }
    }
}