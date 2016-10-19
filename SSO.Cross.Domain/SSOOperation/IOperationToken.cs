//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :IOperationToken
// created by 晨星宇
// at 2016/10/19 17:41:28
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
