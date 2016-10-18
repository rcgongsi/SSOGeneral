using SSO.General.Authorize.Presenter;
using SSO.General.Authorize.View;
using System;

namespace SSO.General.Authorize
{
    public partial class Login : System.Web.UI.Page, ILoginView
    {
        public string Password => txtPassword.Value;
        public string UserName => txtUserName.Value;
        public LoginPresenter Presenter { get; private set; }

        public event EventHandler<AuthorizeEventArgs> Submit;

        public Login()
        {
            Presenter = new LoginPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Presenter.Initialize(this);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Submit(sender, new AuthorizeEventArgs(this));
        }
    }
}