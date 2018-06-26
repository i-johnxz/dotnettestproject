using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace testsign.Model
{
    public static class Encryption
    {

        private static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private const string Base62CodingSpace = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private const string Clist = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly char[] Clistarr = Clist.ToCharArray();

        /// <summary>
        /// MD5 算法
        /// </summary>
        public const string MD5 = "MD5";
        /// <summary>
        /// SHA1算法
        /// </summary>
        public const string SHA1 = "SHA1";

        /// <summary>
        /// 将指定的字符串取哈希值，结果以十六进制字符串展示
        /// 如 "origin" ===> "327B5A9328F00E5C67B213E5A44A28A1"
        /// </summary>
        /// <param name="content">待哈希的字符串</param>
        /// <param name="format">算法(默认MD5)</param>
        /// <returns></returns>
        public static string Hash(this string content, string format = null)
        {
            if (content == null) return null;
            return Hash(content, "", format);
        }
        /// <summary>
        /// 将指定的字符串与哈希盐混淆取哈希值，结果以十六进制字符串返回
        /// </summary>
        /// <param name="content">待哈希的字符串</param>
        /// <param name="salt">哈希盐</param>
        /// <param name="format">算法(默认MD5)</param>
        /// <returns></returns>
        public static string Hash(this string content, string salt, string format)
        {
            if (content == null) return null;
            var contentAndMagic = content + salt;
            if (String.IsNullOrWhiteSpace(format))
            {
                format = MD5;
            }
            using (var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(format))//HashAlgorithm.Create(format))
            {
                if (algorithm == null)
                {
                    throw new ArgumentException("Hash Format");
                }
                var byteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(contentAndMagic));
                return BitConverter.ToString(byteArray).Replace("-", "");
            }
        }

        /// <summary>
        /// 将指定的字符串取哈希值，结果以Base64编码返回
        /// 例如 "orgin" ===> MntakyjwDlxnshPlpEoooQ==
        /// </summary>
        /// <param name="content">待哈希字符串</param>
        /// <param name="format">算法(默认MD5)</param>
        /// <returns></returns>
        public static string HashBase64(this string content, string format = null)
        {
            if (content == null) return null;
            return HashBase64(content, "", format);
        }
        /// <summary>
        /// 将指定的字符串取哈希值，结果以Base64编码返回
        /// 例如 "orgin" ===> MntakyjwDlxnshPlpEoooQ==
        /// </summary>
        /// <param name="content">待哈希字符串</param>
        /// <param name="salt">哈希盐</param>
        /// <param name="format">算法(默认MD5)</param>
        /// <returns></returns>
        public static string HashBase64(this string content, string salt, string format)
        {
            if (content == null) return null;
            var contentAndMagic = content + salt;
            if (String.IsNullOrWhiteSpace(format))
            {
                format = MD5;
            }
            using (var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(format))// HashAlgorithm.Create(format))
            {
                if (algorithm == null)
                {
                    throw new ArgumentException("Hash Format");
                }
                var byteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(contentAndMagic));
                var array = Convert.ToBase64String(byteArray);
                return array;
            }
        }

        /// <summary>
        /// 验证哈希字符串是否匹配指定字符串
        /// </summary>
        /// <param name="hashedTxt">哈希后的字符串</param>
        /// <param name="plainTxt">待检验字符串</param>
        /// <param name="format">哈希算法(默认MD5)</param>
        /// <returns></returns>
        public static bool IsMatch(string hashedTxt, string plainTxt, string format = null)
        {
            var encrypted = plainTxt.Hash(format);
            return encrypted == hashedTxt;
        }
        /// <summary>
        /// 验证哈希字符串是否匹配指定字符串
        /// </summary>
        /// <param name="hashedTxt">哈希后的字符串</param>
        /// <param name="plainTxt">待检验字符串</param>
        /// <param name="salt">哈希盐</param>
        /// <param name="format">哈希算法(默认MD5)</param>
        /// <returns></returns>
        public static bool IsMatch(string hashedTxt, string plainTxt, string salt, string format)
        {
            var encryted = plainTxt.Hash(salt, format);
            return encryted == hashedTxt;
        }

        /// <summary>
        /// 将Base36编码的字符串转换为相应的数字
        /// </summary>
        /// <param name="inputString">Base36编码的字符串</param>
        /// <returns>相应的数字,如果格式错误返回-1</returns>
        public static long Base36Decode(string inputString)
        {
            long result = 0;
            var pow = 0;
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                var c = inputString[i];
                var pos = Clist.IndexOf(c);
                if (pos > -1)
                    result += pos * (long)Math.Pow(Clist.Length, pow);
                else
                    return -1;
                pow++;
            }
            return result;
        }
        /// <summary>
        /// 将数字以Base36方式编码
        /// </summary>
        /// <param name="inputNumber">待编码数字</param>
        /// <returns>编码字符串</returns>
        public static string Base36Encode(long inputNumber)
        {
            var sb = new StringBuilder();
            do
            {
                sb.Append(Clistarr[inputNumber % (long)Clist.Length]);
                inputNumber /= (long)Clist.Length;
            } while (inputNumber != 0);
            return Reverse(sb.ToString());
        }

        /// <summary>
        /// 将数字以Base62算法编码
        /// </summary>
        /// <param name="inputNumber">待编码数字</param>
        /// <returns>编码字符串</returns>
        public static string ToBase62(long inputNumber)
        {
            var result = new StringBuilder();
            do
            {
                var a = inputNumber % 62;
                result.Insert(0, Base62CodingSpace[(int)a]);
                inputNumber = inputNumber / 62;
            } while (inputNumber > 0);
            return result.ToString();
        }

        /// <summary>
        /// 生成的随机数  [1,max]
        /// </summary>
        /// <param name="max">不能小于0</param>
        /// <returns>1 if max less than 1, else [1,max] </returns>
        public static int Rand(int max)
        {
            if (max == 0) return 1;
            if (max < 0) return 0;
            if (max == 1) return 1;
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[4];
                rg.GetBytes(rno);
                int randomvalue = BitConverter.ToInt32(rno, 0);
                var rnd = Math.Abs(randomvalue % (max)) + 1;
                return rnd;
            }
        }

        /// <summary>
        /// 生成的随机数  [min,max]
        /// </summary>
        /// <param name="min">左值</param>
        /// <param name="max">不能小于0</param>
        /// <returns>min if max less than min, else [min,max] </returns>
        public static int Rand(int min, int max)
        {
            if (max <= min)
            {
                return min;
            }
            return Rand(max - min + 1) + min - 1;
        }

        public static int Roulette(int[] weights)
        {
            var total = weights.Sum();
            var boundary = 1;
            var target = Rand(total);
            for (var index = 0; index < weights.Length; index++)
            {
                var w = weights[index];
                if (target >= boundary && target <= boundary + w)
                {
                    return index;
                }
                boundary += w;
            }
            return 0;
        }

        public static T Roulette<T>(T[] players, Func<T, int> weight)
        {
            var weights = players.Select(weight).ToArray();
            var idx = Roulette(weights);
            return players[idx];
        }



        #region DES解密  
        //3DES(DESede>Jave)24位KEY  

        /// <summary>  
        /// 3DES加密  
        /// base64小细节，当使用get请求时，base64生成字符中有“+”号，  
        /// 注意需要转换“%2B”，否则会被替换成空格。POST不存在  
        /// while (str.IndexOf('+') != -1) {  
        ///  str = str.Replace("+","%2B");  
        //  }  
        /// </summary>  
        public static string Encrypt3DES(string content, string key)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            try
            {
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                var des = new TripleDESCryptoServiceProvider();

                des.Mode = CipherMode.ECB;
                Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
                object obj = t.GetField("Encrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);

                MethodInfo mi = des.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
                ICryptoTransform desCrypt = (ICryptoTransform)mi.Invoke(des, new object[] { Encoding.ASCII.GetBytes(key), CipherMode.ECB, null, 0, obj });
                byte[] Buffer = encoding.GetBytes(content);

                return HttpUtility.UrlEncode(Convert.ToBase64String(desCrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)));
                //return ToUrlEnCodeString(Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)));
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>  
        /// 3DES解密  
        /// </summary>  
        /// <returns>解密串</returns>  
        /// <param name="encryptedStr"></param>
        /// <param name="key"></param>  
        public static string Decrypt3DES(string encryptedStr, string key)
        {
            if (string.IsNullOrEmpty(encryptedStr))
            {
                return encryptedStr;
            }
            try
            {
                var des = new TripleDESCryptoServiceProvider();

                des.Mode = CipherMode.ECB;
                des.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                Type t = Type.GetType("System.Security.Cryptography.CryptoAPITransformMode");
                object obj = t.GetField("Decrypt", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly).GetValue(t);
                MethodInfo mi = des.GetType().GetMethod("_NewEncryptor", BindingFlags.Instance | BindingFlags.NonPublic);
                ICryptoTransform DESDecrypt =
                    (ICryptoTransform)
                        mi.Invoke(des, new object[] { Encoding.ASCII.GetBytes(key), CipherMode.ECB, null, 0, obj });
                var new_string = encryptedStr;//FromUrlEnCodeString(a_strString);
                byte[] Buffer = Convert.FromBase64String(new_string);
                return (Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)));
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
