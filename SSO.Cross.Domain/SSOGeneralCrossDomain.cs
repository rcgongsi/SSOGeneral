//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSOGeneralCrossDomain
// created by 晨星宇
// at 2016/10/19 17:51:44
//--------------------------------------------
using SSO.Cross.Domain.SSOOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace SSO.Cross.Domain
{
    /// <summary>
    /// 跨域单点登录服务端
    /// </summary>
    public class SSOGeneralCrossDomain : SSOGeneral
    {
        public IOperationSecret secretService = new OperationSecret();
        public IOperationToken tokenService = new OperationToken();

        public SSOGeneralCrossDomain(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
        }

        public SSOGeneralCrossDomain(Page page)
        {
            Operation = new OperationPage(page);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userData">额外保存信息</param>
        public void LogIn(string userData = null)
        {
            if (!IsNeedLogin())
            {
                ValidationToken();
            }
            string token = CreateToken();

        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <returns></returns>
        public bool ValidationToken()
        {
            string token = Operation.GetRequest("token");
            return true;
        }

        /// <summary>
        /// 生成凭据
        /// </summary>
        /// <returns></returns>
        public string CreateToken()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 判断是需要登录还是认证
        /// </summary>
        /// <returns>true 登录</returns>
        public bool IsNeedLogin()
        {
            if (string.IsNullOrEmpty(Operation.GetRequest("token")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
