namespace SSO.Cross.Domain.SSOOperation
{
    /// <summary>
    /// 加密解密操作类
    /// </summary>
    public interface IOperationSecret
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="token">密文Token</param>
        /// <returns>明文userdata</returns>
        string Encryption(string token);

        /// <summary>
        /// 解密
        /// </summary>
        string Decryption(string token);
    }
}