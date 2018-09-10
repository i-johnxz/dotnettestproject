using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace RefitDemo3
{
    public interface IAliPayBankCardParseService
    {
        /// <summary>
        /// 根据支付宝未公开接口返回银行卡以及类型
        /// https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&cardNo=6217920159440572&cardBinCheck=true
        /// </summary>
        /// <param name="_input_charset"></param>
        /// <param name="cardNo"></param>
        /// <param name="cardBinCheck"></param>
        /// <returns></returns>
        [Get("/validateAndCacheCardInfo.json")]
        Task<BankCardInfo> ParseBankCardAsync(string _input_charset = "utf-8", string cardNo = "",
            bool cardBinCheck = true);
    }
}
