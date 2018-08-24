using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Rabbit_AspNetCore_Utility.Encrypt
{
    public static class EncryptHelper
    {
        #region MD5加密
        /// <summary>
        /// 生成MD5加密字符串
        /// </summary>
        /// <param name="original">源字符串</param>
        /// <returns>加密字符串</returns>
        public static string MD5EncryptByString(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return "";
            }
            else
            {
                using (MD5 md5 = MD5.Create())
                {
                    var byteOriginal = Encoding.Default.GetBytes(original);
                    var md5Ret = md5.ComputeHash(byteOriginal);
                    var md5Sber = new StringBuilder();
                    foreach (var item in md5Ret)
                    {
                        md5Sber.Append(item.ToString("X2"));
                    }
                    return md5Sber.ToString();
                }
            }
        }
        /// <summary>
        /// 生成MD5加密字符串
        /// </summary>
        /// <param name="original">byte 数组</param>
        /// <returns>加密字符串</returns>
        public static string MD5EncryptByBytes(byte[] original)
        {
            if (original == null)
            {
                return "";
            }
            else
            {
                using (MD5 md5 = MD5.Create())
                {
                    var md5Ret = md5.ComputeHash(original);
                    var md5Sber = new StringBuilder();
                    foreach (var item in md5Ret)
                    {
                        md5Sber.Append(item.ToString("X2"));
                    }
                    return md5Sber.ToString();
                }
            }
        }
        /// <summary>
        /// 生成MD5加密字符串
        /// </summary>
        /// <param name="original">字节流</param>
        /// <returns>加密字符串</returns>
        public static string MD5EncryptByStream(Stream original)
        {
            if (original == null)
            {
                return "";
            }
            else
            {
                using (MD5 md5 = MD5.Create())
                {
                    var md5Ret = md5.ComputeHash(original);
                    var md5Sber = new StringBuilder();
                    foreach (var item in md5Ret)
                    {
                        md5Sber.Append(item.ToString("X2"));
                    }
                    return md5Sber.ToString();
                }
            }
        }
        #endregion



    }
}
