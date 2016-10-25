using Newtonsoft.Json;
using SSO.Cross.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SSO.Cross.Domain.SSOOperation
{
    /// <summary>
    /// 使用缓存持久层
    /// </summary>
    public class OperationCache : IOperationToken
    {
        public List<SSOToken> TokenList { get; set; }
        public Operation Operation { get; set; }
        public IOperationSecret secret = new OperationSecret();

        public OperationCache(Operation operation)
        {
            Operation = operation;
        }

        public string GetUserData(string token)
        {
            TokenList = GetCache();
            SSOToken tokenModel = TokenList.FirstOrDefault(p => p.Token == token && p.OverdueTime > DateTime.Now);
            if (tokenModel != null)
            {
                return tokenModel.UserData;
            }
            TokenList.RemoveAll(p => p.OverdueTime < DateTime.Now);
            return "";
        }

        public string SetToken(string userData, TimeSpan saveInterval)
        {
            TokenList = GetCache();
            SSOToken tokenModel = TokenList.FirstOrDefault(p => p.UserData == userData && p.OverdueTime > DateTime.Now);
            if (tokenModel != null)
            {
                tokenModel.OverdueTime = DateTime.Now.Add(saveInterval);
                SetCache();
                return tokenModel.Token;
            }
            SSOToken ssoModel = new SSOToken
            {
                Token = CreateToken(),
                UserData = secret.Encryption(userData),
                OverdueTime = DateTime.Now.Add(saveInterval)
            };
            TokenList.Add(ssoModel);
            SetCache();
            return ssoModel.Token;
        }

        /// <summary>
        /// 设置Cookies存储数据
        /// </summary>
        public void SetCache()
        {
            string tokenJson = JsonConvert.SerializeObject(TokenList);
            Operation.SetCache("TokenList", tokenJson);
        }

        /// <summary>
        /// 获取Cookies存储的数据
        /// </summary>
        public List<SSOToken> GetCache()
        {
            object cache = Operation.GetCache("TokenList");
            if (cache == null)
                return new List<SSOToken>();
            string tokenJson = cache.ToString();
            var list = JsonConvert.DeserializeObject<List<SSOToken>>(tokenJson);
            if (list == null)
            {
                return new List<SSOToken>();
            }
            else
            {
                return list;
            }
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