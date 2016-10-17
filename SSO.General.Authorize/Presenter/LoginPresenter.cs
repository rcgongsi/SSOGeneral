using SSO.General.Authorize.View;
using SSO.General.Helper;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 用户登录方法
        /// </summary>
        private void LoginView_Submit(object sender, AuthorizeEventArgs e)
        {
            string userName = LoginView.UserName;
            string password = LoginView.Password;
            if (ValidationUserInfo(userName, password))
            {
                SSOGeneral sso = new SSOGeneralSameDomain("CookiesTest", new TimeSpan(0, 1, 0), e.Page);
                sso.LogIn("Chenxy");
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