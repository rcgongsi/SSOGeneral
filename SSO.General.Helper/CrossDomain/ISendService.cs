//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :ISendService
// created by 晨星宇
// at 2016/10/18 16:57:22
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.General.Helper.CrossDomain
{
    public interface ISendService
    {
        void SendRequest(string url, Operation operation);
        void Add(string name, string value);
    }
}
