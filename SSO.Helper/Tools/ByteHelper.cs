using System.Linq;
using System.Text;

namespace SSO.Helper.Tools
{
    public static class ByteHelper
    {
        /// <summary>
        /// Byte数组转换String，
        /// </summary>
        /// <param name="data">Byte数组</param>
        /// <param name="strFg">分割符，默认为空</param>
        /// <returns></returns>
        public static string ToStringByte(this byte[] data, string strFg = "")
        {
            return string.Join(strFg, data.Select(t => t.ToString()).ToArray());
        }

        /// <summary>
        /// 字符串转Byte数组
        /// </summary>
        /// <param name="data">字符串</param>
        /// <returns>byte数组</returns>
        public static byte[] ToByte(this string data)
        {
            return Encoding.Default.GetBytes(data);
        }
    }
}