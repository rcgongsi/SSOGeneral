using SSO.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SSOCrossDomain cross = new SSOCrossDomain(this);
                cross.ValidationLogIn("CookiesTest", new TimeSpan(0, 10, 0));
            }
        }
    }
}