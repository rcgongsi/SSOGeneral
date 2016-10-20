using SSO.Cross.Domain;
using SSO.Same.Domain;
using System;

namespace SSO.General.Authorize
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string Token;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //string result = SSOGeneralSameDomain.GetCookieValue("CookiesTest", this);
                    SSOGeneralCrossDomain sso = new SSOGeneralCrossDomain(this);
                    var result = sso.GetUserData("CookiesTest");
                    txtUserData.Text = result;
                    Token = result;
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            SSOGeneralSameDomain.LogUp();
        }
    }
}