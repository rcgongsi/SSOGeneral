//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :ITokenManager
// created by 晨星宇
// at 2016/10/18 17:38:49
//--------------------------------------------
using SSO.General.Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.General.Helper.CrossDomain
{
    /// <summary>
    /// Token相关操作
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// 创建Token
        /// </summary>
        string CreateToken(SSORequest sso);
        /// <summary>
        /// 验证Token
        /// </summary>
        bool ValidateToken(SSORequest sso);
    }
}
