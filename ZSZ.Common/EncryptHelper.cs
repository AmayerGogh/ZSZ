using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ZSZ.Common
{
    public static class EncryptHelper
    {
        #region MD5加密
        public static string CalcMD5(this string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CalcMD5(bytes);
        }

        /// <summary>
        /// 两位补齐
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string AlighTwoDigit(string data)
        {
            return data.Length == 1 ? "0" + data : data;
        }

        public static string CalcMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    builder.Append(AlighTwoDigit(computeBytes[i].ToString("X")));
                }
                return builder.ToString();
            }
        }

        public static string CalcMD5(Stream stream)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] computeBytes = md5.ComputeHash(stream);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < computeBytes.Length; i++)
                {
                    builder.Append(AlighTwoDigit(computeBytes[i].ToString("X")));
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}
