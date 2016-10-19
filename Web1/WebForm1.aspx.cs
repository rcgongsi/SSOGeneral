﻿using SSO.Same.Domain;
using System;

namespace SSO.General.Web1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var result = SSOGeneralSameDomain.GetCookieValue("CookiesTest", this);
                    
                    txtUserData.Text = result;
                }
            }
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            SSOGeneralSameDomain.LogUp();
        }
    }
}