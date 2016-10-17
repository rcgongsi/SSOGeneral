using System;
using System.Web.UI;

namespace SSO.General.Authorize
{
    public class AuthorizeEventArgs : EventArgs
    {
        public Page Page { get; set; }

        public AuthorizeEventArgs(Page page)
        {
            Page = page;
        }
    }
}