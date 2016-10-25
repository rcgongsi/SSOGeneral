using SSO.Helper;
using System;

namespace SSO.General.Authorize
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //string result = new SSOSameDomain(this).GetUserData("CookiesTest");
                    //txtUserData.Text = result;
                    SSOCrossDomain cross = new SSOCrossDomain(this);
                    txtUserData.Text = cross.GetUserData("CookiesTest");
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            //new SSOSameDomain(this).LogOut();
            SSOCrossDomain cross = new SSOCrossDomain(this);
            cross.LogOut();
        }
    }
}