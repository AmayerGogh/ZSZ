using System;
using System.Text;

namespace ZSZ.Common
{
    public static class CommonHelper
    {
        #region 验证码
        //为了避免混淆，不生成'1'、'l'、'0'、'o'、'z'、'2'等字符：
        public static string GenerateCaptchaCode(int len)
        {
            char[] data = { 'a', 'c', 'd', 'e', 'f', 'g', 'k', 'm', 'p', 'r', 's', 't', 'w', 'x', 'y', '3', '4', '5', '7', '8' };
            StringBuilder sbCode = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                char ch = data[rand.Next(data.Length)];//左闭右开区间
                sbCode.Append(ch);
            }
            return sbCode.ToString();
        }
        #endregion
    }
}
