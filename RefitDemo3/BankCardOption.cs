using System;
using System.Collections.Generic;
using System.Text;

namespace RefitDemo3
{
    public class BankCardOption
    {
        public string CcdcapiUri { get; set; }
        public XinyanConfig XinyanConfig { get; set; }

    }
    public class XinyanConfig
    {
        public string BaseUrl { get; set; }
        public string PfxPwd { get; set; }
        public string XinyanId { get; set; }
    }
}
