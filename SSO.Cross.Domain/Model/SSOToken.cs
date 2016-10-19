using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Cross.Domain.Model
{
    /// <summary>
    /// Token凭据实体
    /// </summary>
    public class SSOToken
    {
        public string UserData { get; set; }
        public string Token { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime OverdueTime { get; set; }
    }
}
