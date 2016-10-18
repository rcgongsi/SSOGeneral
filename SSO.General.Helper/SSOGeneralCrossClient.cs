using SSO.General.Helper.CrossDomain;
using SSO.General.Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace SSO.General.Helper
{
    /// <summary>
    /// 跨域单点登录，客户端类
    /// </summary>
    public class SSOGeneralCrossClient
    {
        public Operation Operation { get; set; }
        public SSORequest SSORequest { get; set; }
        public ITokenManager TokenManager { get; set; } = new TokenManager();
        public ISendService SendService { get; set; } = new PostService();

        public SSOGeneralCrossClient(Page page)
        {
            Operation = new OperationPage(page);
        }

        public SSOGeneralCrossClient(HttpContextBase context)
        {
            Operation = new OperationHttpContext(context);
        }

        /// <summary>
        /// 验证请求中是否存在认证信息
        /// </summary>
        public bool IsRequestToken()
        {
            if (string.IsNullOrEmpty(Operation.GetRequest("Code")))
            {
                return false;
            }
            GetSSORequest();
            return true;
        }

        /// <summary>
        /// 获取页面中SSO的请求
        /// </summary>
        /// <returns>请求的对象</returns>
        private SSORequest GetSSORequest()
        {
            SSORequest sso = new SSORequest();
            sso.Code = Operation.GetRequest("Code");
            sso.TimeStamp = Operation.GetRequest("TimeStamp");
            sso.Token = Operation.GetRequest("Token");
            sso.UserData = Operation.GetRequest("UserData");
            sso.AppUrl = Operation.GetRequest("AppUrl");
            return SSORequest = sso;
        }


        /// <summary>
        /// 客户端登录
        /// </summary>
        /// <param name="code">登录客户端编码</param>
        /// <param name="url">当前地址</param>
        /// <param name="toUrl">目标地址</param>
        public void LogIn(string code, string url, string toUrl, TimeSpan overdueInterval)
        {
            SSORequest sso = new SSORequest
            {
                Code = code,
                AppUrl = url,
                TimeStamp = overdueInterval.ToString()
            };
            ITokenManager tokenManager = new TokenManager();
            sso.Token = tokenManager.CreateToken(sso);
            SSORequest = sso;
            Post(toUrl);
        }


        /// <summary>
        /// 封装调用请求
        /// </summary>
        /// <param name="toUrl">目标地址</param>
        private void Post(string toUrl)
        {
            ISendService sendService = new PostService();
            sendService.Add("Code", SSORequest.Code);
            sendService.Add("AppUrl", SSORequest.AppUrl);
            sendService.Add("TimeStamp", SSORequest.TimeStamp);
            sendService.Add("Token", SSORequest.Token);
            sendService.SendRequest(toUrl, Operation);
        }

    }
}
