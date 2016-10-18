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
    public class TokenManager : ITokenManager
    {
        private const string Key = "22362E7A9285DD53A0BBC2932F9733C505DC04EDBFE00D70";
        private const string Iv = "1E7FA9231E7FA923";

        /// <summary>
        /// 创建Token凭证
        /// </summary>
        public void CreateToken(SSORequest sso)
        {
            string token = EncryptToken(sso);
            sso.Token = token;
        }

        /// <summary>
        /// 验证Token凭证
        /// </summary>
        public bool ValidateToken(SSORequest sso)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 加密Token
        /// 加密规则：ID + 时间戳 + 来源Url 进行 字符串反转 进行 DES加密
        /// </summary>
        private string EncryptToken(SSORequest sso)
        {
            return "";
        }

        /// <summary>
        /// 解密Token
        /// </summary>
        private SSORequest DecryptToken(string token)
        {
            return new SSORequest();
        }
    }
}
