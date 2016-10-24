using SSO.Cross.Domain.Tools;

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