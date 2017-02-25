using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZtxFrameWork.Utilities
{
   public class SecretUtil
    {
        //
        // 一 用户密码加密函数
        //

        /// <summary>
        /// 用户密码加密函数
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密值</returns>
        public static string md5(string password)
        {
            return md5(password, 32);
        }
        /// <summary>
        /// 加密用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="codeLength">多少位</param>
        /// <returns>加密密码</returns>
        public static string md5(string password, int codeLength)
        {
            if (!string.IsNullOrEmpty(password))
            {
                // 16位MD5加密（取32位加密的9~25字符）  
                if (codeLength == 16)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower().Substring(8, 16);
                }

                // 32位加密
                if (codeLength == 32)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();
                }
            }
            return string.Empty;
        }
    }
}
