using SSO.General.Helper;
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
                string url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.QueryString["ReturnUrl"];
                SSOGeneralCrossClient client = new SSOGeneralCrossClient(this);
                client.LogIn("WEB1", url, "http://localhost:51666/Login.aspx", new TimeSpan(0, 1, 1));
            }
        }
    }
}