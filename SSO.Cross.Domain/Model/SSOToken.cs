using System;

namespace SSO.Cross.Domain.Model
{
    /// <summary>
    /// Token凭据实体
    /// </summary>
    public class SSOToken
    {
        public string UserData { get; set; }
        public string Token { get; set; }
        public DateTime OverdueTime { get; set; }
    }
}