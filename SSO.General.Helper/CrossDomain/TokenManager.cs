//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :TokenManager
// created by 晨星宇
// at 2016/10/18 17:39:02
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSO.General.Helper.Model;

namespace SSO.General.Helper.CrossDomain
{
    /// <summary>
    /// Token管理类
    /// </summary>
    public class TokenManager : ITokenManager
    {
        private const string Key = "5213ABCD";
        private const string Iv = "ACBD9874";

        /// <summary>
        /// 创建Token凭证
        /// </summary>
        public string CreateToken(SSORequest sso)
        {
            return EncryptToken(sso);
        }

        /// <summary>
        /// 验证Token凭证
        /// </summary>
        public bool ValidateToken(SSORequest sso)
        {
            if (sso.Token == EncryptToken(sso))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 加密Token
        /// 加密规则：ID + 时间戳 + 来源Url 然后对所有进行 DES加密
        /// </summary>
        private string EncryptToken(SSORequest sso)
        {
            string tokenText = sso.Code + sso.TimeStamp + sso.AppUrl;
            return EDSHelper.EnDES(tokenText, Key.ToByte(), Iv.ToByte());
        }
    }
}
