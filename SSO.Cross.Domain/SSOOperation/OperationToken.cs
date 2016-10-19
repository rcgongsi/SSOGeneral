//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :OperationToken
// created by 晨星宇
// at 2016/10/19 17:42:42
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Cross.Domain.SSOOperation
{
    public class OperationToken : IOperationToken
    {
        public string GetToken(string token)
        {
            throw new NotImplementedException();
        }

        public string SetToken(string userData)
        {
            throw new NotImplementedException();
        }
    }
}
