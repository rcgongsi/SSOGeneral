using SSO.Cross.Domain;
using SSO.Same.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SSOGeneralCrossDomain sso = new SSOGeneralCrossDomain(this);
                sso.LogInClient("http://localhost:51666/Login.aspx?link=http://localhost:56757/Login.aspx", "CookiesTest", new TimeSpan(0, 1, 1));
            }
        }
    }
}