using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using testsign.Model;

namespace testsign
{
    class Program
    {
        private static readonly string[] _allowedVersions =
        {
            "1.0",
            "2.0"
        };

        private static readonly IDictionary<Type, IDictionary<string, string>> FormatsMap =
            new ConcurrentDictionary<Type, IDictionary<string, string>>();

        static void Main(string[] args)
        {
            var request = new GenenerateSignatureModel()
            {
                AppId = "qd1bxdtroaf2bjpz",
                Secert = "bq7l2yuvta7babisntweusta7qz3goea",
                Hash = "md5",
                Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                Version = "2.0",
                BirthDate = DateTime.Now
            };
            var sigr = GenenerateSignature(request);
            Console.WriteLine(sigr);
            Console.Read();
        }
        public static string GenenerateSignature(GenenerateSignatureModel requestModel)
        {
            var map = new Dictionary<string, object>
            {
                {nameof(requestModel.AppId), requestModel.AppId},
                {nameof(requestModel.Version), requestModel.Version},
                {nameof(requestModel.Timestamp), requestModel.Timestamp},
                {nameof(requestModel.Hash), requestModel.Hash},
                {nameof(requestModel.Secert), requestModel.Secert},
            };

            var formatMap = GetFormatsByType(requestModel.GetType());

            var signStr = map.OrderBy(o => o.Key, StringComparer.Ordinal)
                .Aggregate(new StringBuilder(),
                    (builder, pair) =>
                    {
                        var value = pair.Value;
                        if (value != null && formatMap.ContainsKey(pair.Key))
                        {
                            var format = "{0:" + formatMap[pair.Key] + "}";
                            value = string.Format(format, value);
                        }

                        if (value == null)
                        {
                            value = "";
                        }

                        return builder.Append(LowerFirstCase(pair.Key))
                            .Append('=')
                            .Append(value);
                    }).ToString();
            return signStr.Hash().ToLower();
        }

        private static string LowerFirstCase(string origin)
        {
            return char.ToLower(origin[0]) + origin.Substring(1);
        }

        private static IDictionary<string, string> GetFormatsByType(Type type)
        {
            if (FormatsMap.ContainsKey(type))
            {
                return FormatsMap[type];
            }
            var map = new Dictionary<string, string>();
            var properties = type.GetProperties();
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<FormatsAttribute>();
                if (attr != null)
                {
                    map.Add(prop.Name, attr.Format);
                }
            }

            FormatsMap[type] = map;
            return map;
        }
    }

    public class GenenerateSignatureModel
    {
        public string AppId { get; set; }

        public string Secert { get; set; }

        public string Version { get; set; }

        public long Timestamp { get; set; }

        public string Hash { get; set; }

        [Formats("yyyy-MM-dd")]
        public DateTime? BirthDate { get; set; }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class FormatsAttribute : Attribute
    {
        public FormatsAttribute(string format)
        {
            Format = format;
        }

        public string Format { get; }
    }
}
