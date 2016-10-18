//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSOGeneralCrossDomain
// created by 晨星宇
// at 2016/10/18 16:26:37
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.General.Helper
{
    /// <summary>
    /// 跨域单点登录
    /// </summary>
    public class SSOGeneralCrossDomain : SSOGeneral
    {
        public SSOGeneralCrossDomain(string cookieName, TimeSpan overdue) : base(cookieName, overdue)
        {
        }

        public override void LogIn(string userData)
        {

        }
    }
}
