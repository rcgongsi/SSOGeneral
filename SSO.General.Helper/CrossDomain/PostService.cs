//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :PostService
// created by 晨星宇
// at 2016/10/18 16:49:59
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.General.Helper.CrossDomain
{
    /// <summary>
    /// Post服务发送工具
    /// </summary>
    public class PostService : ISendService
    {
        private Dictionary<string, string> Inputs { get; set; } = new Dictionary<string, string>();
        private string Url;
        private const string FormName = "form1";
        private const string Method = "post";

        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        public void SendRequest(string url, Operation operation)
        {
            Url = url;
            StringBuilder builder = new StringBuilder("<html><head></head>");
            builder.AppendFormat("<body onload =\"document.{0}.submit()\">", FormName);
            builder.AppendFormat("<form name =\"{0}\" method =\"{1}\" action =\"{2}\">", FormName, Method, Url);
            foreach (var item in Inputs)
            {
                builder.AppendFormat("<input name =\"{0}\" type =\"hidden\" value =\"{1}\">", item.Key, item.Value);
            }
            builder.AppendFormat("</form></body></html>");
            operation.Write(builder.ToString());
        }
    }
}
