//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :OperationToken
// created by 晨星宇
// at 2016/10/19 17:42:42
//--------------------------------------------
using SSO.Cross.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Cross.Domain.SSOOperation
{
    public class OperationToken : IOperationToken
    {
        public static List<SSOToken> tokenList = new List<SSOToken>();

        public string GetUserData(string token)
        {
            SSOToken tokenModel = tokenList.FirstOrDefault(p => p.Token == token && p.OverdueTime > DateTime.Now);
            if (tokenModel != null)
            {
                return tokenModel.UserData;
            }
            tokenList.RemoveAll(p => p.OverdueTime < DateTime.Now);
            return "";
        }

        public string SetToken(string userData, TimeSpan saveInterval)
        {
            SSOToken tokenModel = tokenList.FirstOrDefault(p => p.UserData == userData && p.OverdueTime > DateTime.Now);
            if (tokenModel != null)
            {
                return tokenModel.Token;
            }
            SSOToken ssoModel = new SSOToken
            {
                Token = CreateToken(),
                CreateTime = DateTime.Now,
                UserData = userData,
                OverdueTime = DateTime.Now.Add(saveInterval)
            };
            tokenList.Add(ssoModel);
            
            return ssoModel.Token;
        }

        /// <summary>
        /// 生成凭据
        /// </summary>
        /// <returns></returns>
        public string CreateToken()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
