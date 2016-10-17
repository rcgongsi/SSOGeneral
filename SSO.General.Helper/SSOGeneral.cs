using System;

namespace SSO.General.Helper
{
    /// <summary>
    /// 通用身份验证
    /// </summary>
    public abstract class SSOGeneral
    {
        /// <summary>
        /// Cookie名称，请与WebConfig一致
        /// </summary>
        public string CookieName { get; private set; }

        /// <summary>
        /// 过期间隔
        /// </summary>
        public TimeSpan OverdueInterval { get; private set; }

        /// <summary>
        /// 初始化单点登录
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <param name="overdue">时间间隔</param>
        protected SSOGeneral(string cookieName, TimeSpan overdue)
        {
            CookieName = cookieName;
            OverdueInterval = overdue;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        public abstract void LogIn(string userData);
    }
}