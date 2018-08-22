using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using IFramework.Infrastructure;
using Newtonsoft.Json;

namespace RefitDemo3
{
    public class BankCardInfo
    {
        [JsonProperty(PropertyName = "bank")]
        public string Code { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "cardType")]
        public BankCardType CardType { get; set; }

        public string Type => CardType.GetDescription();


        public BankCardInfo(string code, string name, BankCardType cardType)
        {
            Code = code;
            Name = name;
            CardType = cardType;
        }
    }


    public enum BankCardType
    {
        [Description("储蓄卡")]
        DC = 1,
        [Description("信用卡")]
        CC = 2,
        [Description("准贷记卡")]
        SCC = 3,
        [Description("预付费卡")]
        PC = 4
    }
}
