using System;
using System.IO;
using System.Security.Cryptography;

namespace SSO.Helper.Tools
{
    public class KeyHelper
    {
        /// <summary>
        /// 对称加密-->DES加密
        /// </summary>
        /// <param name="data">待加密的字符数据</param>
        /// <param name="key">密钥，长度必须为64位(byte[8])</param>
        /// <param name="iv">iv向量，长度必须为64位(byte[8])</param>
        /// <returns>加密后的字符串</returns>
        public static string EnDES(string data, byte[] key, byte[] iv)
        {
            DES des = DES.Create();//定义DES对象
            byte[] tmp = data.ToByte();//转换字节序列
            byte[] encryptoData;
            ICryptoTransform encryptor = des.CreateEncryptor(key, iv);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (var cs = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(cs))
                    {
                        writer.Write(data);
                        writer.Flush();
                    }
                }
                encryptoData = memoryStream.ToArray();
            }
            des.Clear();
            return Convert.ToBase64String(encryptoData);
        }

        /// <summary>
        /// 对称加密-->DES解密
        /// </summary>
        /// <param name="data">加密后的字符串</param>
        /// <param name="key">密钥，长度必须为64位(byte[8])</param>
        /// <param name="iv">iv向量，长度必须为64位(byte[8])</param>
        /// <returns>待加密的字符数据</returns>
        public static string DeDES(string data, byte[] key, byte[] iv)
        {
            string resultData = string.Empty;
            byte[] tmpData = Convert.FromBase64String(data);//转换的格式
            DES des = DES.Create();
            ICryptoTransform decryptor = des.CreateDecryptor(key, iv);
            using (var memoryStream = new MemoryStream(tmpData))
            {
                using (var cs = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    StreamReader reader = new StreamReader(cs);
                    resultData = reader.ReadLine();
                }
            }
            return resultData;
        }
    }
}