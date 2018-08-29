using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class TestEnumParse
    {
        private readonly ITestOutputHelper _output;

        public TestEnumParse(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// 婚姻代码
        /// </summary>
        private readonly Dictionary<string, string> _marrayCodes = new Dictionary<string, string>()
        {
            {"其他","QT"},
            {"离婚", "LT"},
            {"丧偶", "SO"},
            {"未婚", "WH"},
            {"已婚未育","YW"}
        };

        /// <summary>
        /// 最高学历
        /// </summary>
        private readonly Dictionary<string, string> _maxDegreeCodes = new Dictionary<string, string>()
        {
            {"小学","XX"},
            {"本科","BK"},
            {"初中","CZ"},
            {"大专","DZ"},
            {"高中","GZ"},
            {"硕士及以上","SS"}
        };

        /// <summary>
        /// 职业
        /// </summary>
        private readonly Dictionary<string, string> _careerCodes = new Dictionary<string, string>()
        {
            {"不便分类的其他", "QT"},
            {"技术", "JS"},
            {"销售", "XS"},
            {"服务人员", "FW"},
            {"生成运输工", "SC "},
            {"待业人员", "DY"},
        };


        [Fact]
        public void Test_ParseEnum()
        {
            _output.WriteLine(_marrayCodes.GetMapValue("123"));
        }
    }

    public enum MarryCode
    {
        /// <summary>
        /// 
        /// </summary>
        LT = 0
    }

    public static class Extesions
    {
        public static string GetMapValue(this IDictionary<string, string> mapping, string key)
        {
            return mapping.FirstOrDefault(f => f.Key == key).Value ?? mapping.FirstOrDefault().Value;
        }
    }
}
