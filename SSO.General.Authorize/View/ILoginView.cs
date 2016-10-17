using System;

namespace SSO.General.Authorize.View
{
    /// <summary>
    /// 登录页面视图
    /// </summary>
    public interface ILoginView
    {
        string UserName { get; }
        string Password { get; }

        event EventHandler<AuthorizeEventArgs> Submit;
    }
}