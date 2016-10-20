using SSO.Cross.Domain.SSOOperation;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SSO.Cross.Domain
{
    /// <summary>
    /// 跨域单点登录服务端
    /// </summary>
    public class SSOGeneralCrossDomain : SSOGeneral
    {
        internal IOperationSecret secretService = new OperationSecret();
        internal IOperationToken tokenService;
        internal string Token { get { return Operation.GetRequest("token"); } }
        internal string ReturnUrl { get { return Operation.GetRequest("ReturnUrl"); } }
        internal string Link { get { return Operation.GetRequest("link"); } }
        internal string UserData { get { return Operation.GetRequest("userData"); } }

        public SSOGeneralCrossDomain(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
            tokenService = new OperationCache(Operation);
        }

        public SSOGeneralCrossDomain(Page page)
        {
            Operation = new OperationPage(page);
            tokenService = new OperationCache(Operation);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userData">额外保存信息</param>
        public void LogIn(string userData, string cookieName, TimeSpan overdueTime)
        {
            string token = tokenService.SetToken(userData, overdueTime);
            string url = GetRedirectUrl(token);
            FormsAuthenticationTicket ticket = CreateTicket(cookieName, overdueTime, token);
            CreateCookie(ticket);
            Operation.Redirect(url);
        }

        /// <summary>
        /// 获取需要跳转的Url
        /// </summary>
        /// <param name="token">凭据验证</param>
        /// <returns>跳转Url</returns>
        private string GetRedirectUrl(string token)
        {
            string url = string.Empty;
            if (Link != "")
            {
                url = Link;
            }
            else if (ReturnUrl != "")
            {
                url = ReturnUrl;
            }
            if (url.Contains("?"))
            {
                url += "&token=" + token;
            }
            else if (!url.Contains("token="))
            {
                url += "?token=" + token;
            }
            return url;
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
        private FormsAuthenticationTicket CreateTicket(string cookieName, TimeSpan overdue, string userData)
        {
            return new FormsAuthenticationTicket(1, cookieName, DateTime.Now, DateTime.Now.Add(overdue), true, userData);
        }

        /// <summary>
        /// 客户端登录
        /// </summary>
        /// <param name="serviceUrl">认证服务地址</param>
        public void LogInClient(string serviceUrl, string cookieName, TimeSpan overdueTime)
        {
            //登录或者验证Token不正确
            if (IsLogin())
            {
                Redirect(serviceUrl);
            }
            ValidationToken(serviceUrl);
            string userData = secretService.Decryption(UserData);
            var ticket = CreateTicket(cookieName, overdueTime, userData);
            CreateCookie(ticket);
            Operation.Redirect(ReturnUrl);
        }

        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        /// <param name="serviceUrl">登录页面地址</param>
        private void Redirect(string serviceUrl)
        {
            string url = "";
            if (serviceUrl.Contains("link"))
            {
                url = serviceUrl;
            }
            else
            {
                url = serviceUrl + "?link=" + ReturnUrl;
            }
            Operation.Redirect(url);
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <returns></returns>
        public void ValidationToken(string serviceUrl = null)
        {
            if (string.IsNullOrEmpty(Token) || !string.IsNullOrEmpty(UserData))
            {
                return;
            }
            //是否同域
            bool IsSameDomain = serviceUrl != null && (new Uri(serviceUrl)).Authority != (Operation.Uri()).Authority;
            if (IsSameDomain)
            {
                Operation.Redirect(serviceUrl + "&token=" + Token);
            }
            if (string.IsNullOrEmpty(serviceUrl))
            {
                serviceUrl = Link + "?token=" + Token;
            }
            string userData = tokenService.GetUserData(Token);
            if (userData != "")
            {
                string url = Link + "?token=" + Token + "&userData=" + userData;
                Operation.Redirect(url);
            }
        }

        /// <summary>
        /// 判断是需要登录还是认证
        /// </summary>
        /// <returns>true 登录</returns>
        private bool IsLogin()
        {
            if (string.IsNullOrEmpty(Token))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取UserData明文
        /// </summary>
        /// <returns>明文信息</returns>
        public string GetUserData(string cookieName)
        {
            string ticket = GetEncryUserData(cookieName);
            var model = FormsAuthentication.Decrypt(ticket);
            return model.UserData;
        }

        /// <summary>
        /// 获取UserData密文
        /// </summary>
        private string GetEncryUserData(string cookieName)
        {
            return Operation.GetCookie(cookieName)?.Value;
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        public void LogUp()
        {
            //注销登录的Cookies
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

    }
}