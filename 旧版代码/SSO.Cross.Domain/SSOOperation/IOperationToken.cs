using System;

namespace SSO.Cross.Domain.SSOOperation
{
    /// <summary>
    /// Token持久操作类
    /// </summary>
    public interface IOperationToken
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        string GetUserData(string token);

        /// <summary>
        /// 设置Token
        /// </summary>
        string SetToken(string userData, TimeSpan saveInterval);
    }
}