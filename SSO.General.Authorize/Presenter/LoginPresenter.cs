using SSO.Cross.Domain;
using SSO.General.Authorize.View;
using SSO.Same.Domain;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace SSO.General.Authorize.Presenter
{
    /// <summary>
    /// 登录页面控制类
    /// </summary>
    public class LoginPresenter
    {
        public ILoginView LoginView { get; private set; }
        public Dictionary<bool, List<string>> PressionsList { get; private set; }

        public LoginPresenter(ILoginView loginView)
        {
            LoginView = loginView;
            LoginView.Submit += LoginView_Submit;
        }

        public void Initialize(Page page)
        {
            SSOGeneralCrossDomain sso = new SSOGeneralCrossDomain(page);
            sso.ValidationToken();
        }

        /// <summary>
        /// 用户登录方法
        /// </summary>
        private void LoginView_Submit(object sender, AuthorizeEventArgs e)
        {
            string userName = LoginView.UserName;
            string password = LoginView.Password;
            if (ValidationUserInfo(userName, password))
            {
                ////同域单点登录
                //SSOGeneralSameDomain sso = new SSOGeneralSameDomain("CookiesTest", new TimeSpan(0, 1, 0), e.Page);
                //sso.LogIn(userName);

                //跨域单点登录
                SSOGeneralCrossDomain sso = new SSOGeneralCrossDomain(e.Page);
                sso.LogIn("Chenxy", "CookiesTest", new TimeSpan(1, 0, 0));
            }
        }

        /// <summary>
        /// 用户身份验证
        /// </summary>
        private bool ValidationUserInfo(string userName, string password)
        {
            if (userName.Equals("admin") && password.Equals("admin"))
            {
                return true;
            }
            return false;
        }
    }
}