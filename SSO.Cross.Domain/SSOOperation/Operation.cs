using System.Web;

namespace SSO.Cross.Domain
{
    /// <summary>
    /// 单点登录操作工厂
    /// </summary>
    public abstract class Operation
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="request">参数名</param>
        /// <returns>参数值</returns>
        public abstract string GetRequest(string request);

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookie">Cookie实体</param>
        public abstract void SetCookie(HttpCookie cookie);

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        public abstract HttpCookie GetCookie(string cookieName);

        /// <summary>
        /// 重定向制定页面
        /// </summary>
        /// <param name="url">目标URL</param>
        public abstract void Redirect(string url);
    }
}