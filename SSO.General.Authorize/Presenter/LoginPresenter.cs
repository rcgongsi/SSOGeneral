using SSO.General.Authorize.View;
using SSO.General.Helper;
using SSO.General.Helper.Model;
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

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize(Page page)
        {
            //初始化需要进行获取验证，看是否有其他地方传递的Token
            SSOGeneralCrossClient cross = new SSOGeneralCrossClient(page);
            if (cross.IsRequestToken() == false)
            {
                return;
            }
            SSORequest result = cross.SSORequest;
            //验证其他地方发来的Token
            if (cross.TokenManager.ValidateToken(result))
            {
                cross.SendService.Add("UserData", cross.Operation.GetCookie("CookiesTest")?.Value);
                cross.SendService.Add("Code", result.Code);
                cross.SendService.Add("TimeStamp", result.TimeStamp);
                cross.SendService.Add("AppUrl", result.AppUrl);
                cross.SendService.Add("Token", result.Token);
                cross.SendService.SendRequest(result.AppUrl, cross.Operation);
            }
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
                //同域单点登录
                //SSOGeneral sso = new SSOGeneralSameDomain("CookiesTest", new TimeSpan(0, 1, 0), e.Page);
                //sso.LogIn("Chenxy");

                //跨域单点登录
                SSOGeneral sso = new SSOGeneralCrossDomain("CookiesTest", new TimeSpan(0, 1, 0), e.Page);
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