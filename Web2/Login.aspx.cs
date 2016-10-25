using SSO.Helper;
using System;

namespace Web2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SSOCrossDomain cross = new SSOCrossDomain(this);
                cross.ValidationLogIn("CookieWeb2", new TimeSpan(0, 10, 0));
            }
        }
    }
}