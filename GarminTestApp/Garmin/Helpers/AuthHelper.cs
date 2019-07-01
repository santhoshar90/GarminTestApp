using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CHBase_FitBit_TestProject.Garmin.Helpers
{
    public static class AuthHelper
    {
        public static string GenerateOAuthSignature(string Key,String normalizedRequestUrl)
        {

            var hashKey = Key;

            if (HttpContext.Current != null &&
                HttpContext.Current.Session["Token_Secret"] != null)
            {
                hashKey = hashKey + HttpContext.Current.Session["Token_Secret"];
            }
           // logger.Log("Apply HMAC Sha1 with secret : " + hashKey);

            var base64_hash = GenerateHmacSHA1(hashKey, normalizedRequestUrl);

            //logger.Log("Base 64ed hash : " + base64_hash);
           // logger.Log("URL encoded : " + UpperCaseUrlEncode(base64_hash));

            return UpperCaseUrlEncode(base64_hash);
        }
        public static string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }
            return new string(temp);
        }

        internal static string GenerateNounce()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch.ToString();
        }

        internal static string GenerateNounce(DateTime dt)
        {
            TimeSpan t = dt - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch.ToString();
        }

        public static string GenerateHmacSHA1(string key, string body)
        {
            var encoding = Encoding.ASCII;
            byte[] computedHash = null;

            using (HMACSHA1 hmac = new HMACSHA1(encoding.GetBytes(key)))
            {
                byte[] buffer = encoding.GetBytes(body);
                computedHash = hmac.ComputeHash(buffer);
            }

            if (computedHash != null)
                return Convert.ToBase64String(computedHash);
            return string.Empty;
        }

    }
}