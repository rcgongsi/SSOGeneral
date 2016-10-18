//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSOGeneralCrossDomain
// created by 晨星宇
// at 2016/10/18 16:26:37
//--------------------------------------------
using SSO.General.Helper.CrossDomain;
using SSO.General.Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SSO.General.Helper
{
    /// <summary>
    /// 跨域单点登录
    /// </summary>
    public class SSOGeneralCrossDomain : SSOGeneral
    {
        public Operation Operation { get; set; }
        public SSORequest SSORequest { get; set; }
        public dynamic RequestContext { get; set; }

        public SSOGeneralCrossDomain(string cookieName, TimeSpan overdue, Page page) : base(cookieName, overdue)
        {
            Operation = new OperationPage(page);
            RequestContext = page;
        }

        public SSOGeneralCrossDomain(string cookieName, TimeSpan overdue, HttpContextBase context) : base(cookieName, overdue)
        {
            Operation = new OperationHttpContext(context);
            RequestContext = context;
        }

        /// <summary>
        /// 用户登录（服务端）
        /// </summary>
        /// <param name="userData">用户额外信息</param>
        public override void LogIn(string userData)
        {
            SSOGeneralSameDomain sameDomain = new SSOGeneralSameDomain(CookieName, OverdueInterval, RequestContext);
            sameDomain.LogIn(userData);
            return;
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        public void LogUp()
        {

        }

    }
}
