//-----------------------------------------------------------------------------------
// <copyright file="MD5Helper.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Text;

namespace Al.vNext.Core.Utility
{
    public static class MD5Helper
    {
        public static string MD532ToLower(string value)
        {
            return HashPassword(value).ToLower();
        }

        public static string MD532ToUpper(string value)
        {
            return HashPassword(value);
        }

        public static string MD5UserPassword(string userName, string password)
        {
            return MD5Helper.MD532ToUpper(string.Format("{0}-{1}", userName, password));
        }

        private static string HashPassword(string inputValue)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
                var strResult = BitConverter.ToString(result);

                return strResult.Replace("-", string.Empty);
            }
        }
    }
}