namespace testsign.Model
{
    public class BaseRequest
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 哈希算法
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// 哈希值
        /// </summary>
        public string Sign { get; set; }
    }
}