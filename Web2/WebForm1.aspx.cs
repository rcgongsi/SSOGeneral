using System;

namespace SSO.General.Web2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //string result = SSOGeneralSameDomain.GetCookieValue("CookiesTest", this);
                    //txtUserData.Text = result;
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            //SSOGeneralSameDomain.LogOut();
        }
    }
}