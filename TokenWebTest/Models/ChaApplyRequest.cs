using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TokenWebTest.Models
{
    public class ChaApplyRequest: BaseRequest
    {
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// 申请单号
        /// 渠道申请单号，在渠道方应是标识某一笔借款的唯一编号，谁来构造？与后面的chaApTr什么关系？长度不一样
        /// </summary>
        [JsonProperty(PropertyName = "applyNo")]
        public string ApplyNo { get; set; }

        [JsonProperty(PropertyName = "apiVersion")]
        public string ApiVersion { get; set; }

        /// <summary>
        /// 记录内容
        /// </summary>
        [JsonProperty(PropertyName = "recordContent")]
        public string RecordContent { get; set; }
    }
}
