using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class TestRandom
    {
        private readonly ITestOutputHelper _output;

        public TestRandom(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_Output()
        {
            //var model = new BaseRequest();
            //var test = JsonConvert.SerializeObject(model);
            var model = new SignRequest()
            {
                Test = "123",
                Use = true,
                AccessToken = "123444",
                Openid = "22222"
            };
            var test = JsonConvert.SerializeObject(model);
            _output.WriteLine(test);
        }

        
    }

    public static class StringExstion
    {
        public static string Md5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }


    public class SignRequest : BaseRequest
    {
        public string Test { get; set; }

        public bool Use { get; set; }
    }

    public class BaseRequest
    {
        private readonly Random _random = new Random();

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        [JsonProperty(PropertyName = "nonce_str")]
        public string NonceStr => _random.Next(10000, 99999).ToString();

        [JsonProperty(PropertyName = "sign")] public string Sign { get; set; }
        //public string Sign {
        //    get
        //    {
        //        var map = new Dictionary<string, object>();

        //        var properties = this.GetType().GetProperties().Where(s => s.Name != "Sign");

        //        foreach (var prop in properties)
        //        {
        //            var key = prop.GetCustomAttribute<JsonPropertyAttribute>() != null
        //                ? prop.GetCustomAttribute<JsonPropertyAttribute>().PropertyName
        //                : prop.Name;


        //            map.Add(key, prop.GetValue(this, null));
        //        }

        //        var signStr = map
        //            .OrderBy(o => o.Key, StringComparer.Ordinal)
        //            .Aggregate(new StringBuilder(),
        //                (builder, pair) =>
        //                {
        //                    var value = pair.Value;

        //                    return builder
        //                        .Append("&" + pair.Key)
        //                        .Append('=')
        //                        .Append(value);
        //                })
        //            .ToString();
        //        var sign = signStr.Substring(1, signStr.Length - 1).Md5();
        //        return sign;
        //    }
        //}
    }


}
