//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSOGeneralCrossDomain
// created by 晨星宇
// at 2016/10/19 17:51:44
//--------------------------------------------
using SSO.Cross.Domain.SSOOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IOperationSecret secretService = new OperationSecret();
        public IOperationToken tokenService = new OperationToken();

        public SSOGeneralCrossDomain(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
        }

        public SSOGeneralCrossDomain(Page page)
        {
            Operation = new OperationPage(page);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userData">额外保存信息</param>
        public void LogIn(string userData, string cookieName, TimeSpan overdueTime)
        {
            string token = tokenService.SetToken(userData, overdueTime);
            string url = "";
            if (Operation.GetRequest("link") != "")
            {
                url = Operation.GetRequest("link");
            }
            else if (Operation.GetRequest("ReturnUrl") != "")
            {
                url = Operation.GetRequest("ReturnUrl");
            }
            url += "?token=" + token;

            FormsAuthenticationTicket ticket = CreateTicket(cookieName, overdueTime, token);
            CreateCookie(ticket);
            Operation.Redirect(url);
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
            if (!IsLogin())
            {
                if (ValidationToken())
                {
                    string token = Operation.GetRequest("token");
                    var ticket = CreateTicket(cookieName, overdueTime, token);
                    CreateCookie(ticket);
                    Operation.Redirect(Operation.GetRequest("ReturnUrl"));
                    return;
                }
            }
            string url = "";
            if (serviceUrl.Contains("link"))
            {
                url = serviceUrl;
            }
            else
            {
                url = serviceUrl + "?link=" + Operation.GetRequest("ReturnUrl");
            }
            Operation.Redirect(url);
        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <returns></returns>
        public bool ValidationToken()
        {
            string token = Operation.GetRequest("token");
            //string dectToken = secretService.Decryption(token);
            if (tokenService.GetUserData(token) != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是需要登录还是认证
        /// </summary>
        /// <returns>true 登录</returns>
        public bool IsLogin()
        {
            if (string.IsNullOrEmpty(Operation.GetRequest("token")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取UserData明文
        /// </summary>
        /// <returns>明文信息</returns>
        public string GetUserData(string token = null)
        {
            if (token != null || ValidationToken())
            {
                if (token == null)
                    token = Operation.GetRequest("token");
                return tokenService.GetUserData(token);
            }
            return "";
        }

        /// <summary>
        /// 获取Cookie对应的值
        /// </summary>
        public string GetCookie(string cookieName)
        {
            string ticket = Operation.GetCookie(cookieName)?.Value;
            var model = FormsAuthentication.Decrypt(ticket);
            return model.UserData.ToString();
        }
    }
}
