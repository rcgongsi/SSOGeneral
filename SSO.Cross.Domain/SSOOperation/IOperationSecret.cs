//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :IOperationSecret
// created by 晨星宇
// at 2016/10/19 17:43:36
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Cross.Domain.SSOOperation
{
    /// <summary>
    /// 加密解密操作类
    /// </summary>
    public interface IOperationSecret
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="token">密文Token</param>
        /// <returns>明文userdata</returns>
        string Encryption(string token);

        /// <summary>
        /// 解密
        /// </summary>
        string Decryption(string token);
    }
}
