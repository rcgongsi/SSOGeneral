
using Newtonsoft.Json;
using SSO.Cross.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO.Cross.Domain.SSOOperation
{
    public class OperationCookies : IOperationToken
    {
        public List<SSOToken> TokenList { get; set; }
        public Operation Operation { get; set; }

        public OperationCookies(Operation operation)
        {
            Operation = operation;
        }

        public string GetUserData(string token)
        {
            TokenList = GetCookies();
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
            TokenList = GetCookies();
            SSOToken tokenModel = TokenList.FirstOrDefault(p => p.UserData == userData && p.OverdueTime > DateTime.Now);
            if (tokenModel != null)
            {
                tokenModel.OverdueTime = DateTime.Now.Add(saveInterval);
                SetCookies();
                return tokenModel.Token;
            }
            SSOToken ssoModel = new SSOToken
            {
                Token = CreateToken(),
                UserData = userData,
                OverdueTime = DateTime.Now.Add(saveInterval)
            };
            TokenList.Add(ssoModel);
            SetCookies();
            return ssoModel.Token;
        }

        /// <summary>
        /// 设置Cookies存储数据
        /// </summary>
        public void SetCookies()
        {
            string tokenJson = JsonConvert.SerializeObject(TokenList);
            HttpCookie cookie = new HttpCookie("TokenList");
            cookie.Value = tokenJson;
            Operation.SetCookie(cookie);
        }

        /// <summary>
        /// 获取Cookies存储的数据
        /// </summary>
        public List<SSOToken> GetCookies()
        {
            HttpCookie cookie = Operation.GetCookie("TokenList");
            if (cookie == null)
                return new List<SSOToken>();
            string tokenJson = cookie.Value;
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