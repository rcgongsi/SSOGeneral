//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :SSORequest
// created by 晨星宇
// at 2016/10/18 17:09:40
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.General.Helper.Model
{
    /// <summary>
    /// 跨域请求实体
    /// </summary>
    public class SSORequest
    {
        public string ID { get; set; }
        public string TimeStamp { get; set; }
        public string AppUrl { get; set; }
        public string Token { get; set; }
        public string UserData { get; set; }
    }
}
