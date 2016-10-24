using System;

namespace SSO.General.Web1
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
                    //var result = SSOGeneralSameDomain.GetCookieValue("CookiesTest", this);
                    //SSOGeneralCrossDomainbase sso = new SSOGeneralCrossDomainbase(this);
                    //var result = sso.GetUserData("CookiesTest");
                    //txtUserData.Text = result;
                    //Token = Request["token"];
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            //SSOGeneralSameDomain.LogOut();
            //new SSOGeneralCrossDomainbase(this).LogOut();
        }
    }
}