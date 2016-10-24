//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSOSameDomain
// created by 晨星宇
// at 2016/10/24 12:56:42
//--------------------------------------------
using SSO.Helper.HTTPOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SSO.Helper
{
    /// <summary>
    /// 同域单点登录
    /// </summary>
    public class SSOSameDomain
    {
        /// <summary>
        /// HTTP状态操作
        /// </summary>
        public Operation Operation { get; set; }

        public SSOSameDomain(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
        }

        public SSOSameDomain(Page page)
        {
            Operation = new OperationPage(page);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public void LogIn(string cookieName, TimeSpan overdue, string userData)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, cookieName, DateTime.Now, DateTime.Now.Add(overdue), true, userData);
            CreateCookie(ticket);
            RedirectPage();
        }

        /// <summary>
        /// 创建Cookie
        /// </summary>
        internal void CreateCookie(FormsAuthenticationTicket ticket)
        {
            HttpCookie cookie = new HttpCookie(ticket.Name, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = ticket.Expiration;
            Operation.SetCookie(cookie);
        }

        /// <summary>
        /// 登录成功跳转
        /// </summary>
        internal void RedirectPage()
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
        /// 用户注销
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        public string GetUserData(string cookieName)
        {
            string result = Operation.GetCookie(cookieName)?.Value;
            return result != null ? FormsAuthentication.Decrypt(result).UserData : "";
        }
    }
}
