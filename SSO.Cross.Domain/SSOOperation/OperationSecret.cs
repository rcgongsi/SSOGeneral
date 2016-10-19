//--------------------------------------------
// Copyright (C) 软通动力信息技术（集团）有限公司
// filename :OperationSecret
// created by 晨星宇
// at 2016/10/19 17:46:15
//--------------------------------------------
using SSO.Cross.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Cross.Domain.SSOOperation
{
    public class OperationSecret : IOperationSecret
    {
        public const string Key = "A1B2C3D4";
        public const string Iv = "98745621";
        public string Decryption(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return "";
            }
            return KeyHelper.DeDES(token, Key.ToByte(), Iv.ToByte());
        }

        public string Encryption(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return "";
            }
            return KeyHelper.EnDES(token, Key.ToByte(), Iv.ToByte());
        }
    }
}
