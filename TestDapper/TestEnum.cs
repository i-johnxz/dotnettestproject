using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace TestDapper
{
    public class TestEnum
    {
        [Fact]
        public void Test_Enum_Method()
        {
            var loanStatus = LoanStatus.Withdrawed;

            var result = loanStatus > LoanStatus.Released;
        }
    }

    public enum LoanStatus
    {
        /// <summary>
        ///     等待发送到资金端
        /// </summary>
        [Obsolete]
        [Description("待确认")] WaitingConfirmed = 0,

        /// <summary>
        ///     募集中
        /// </summary>
        [Description("募集中")] Raising = 1,

        /// <summary>
        ///     已取消
        /// </summary>
        [Description("已取消")] Canceled = 2,

        /// <summary>
        ///     已放款
        /// </summary>
        [Description("已放款")] Released = 4,

        /// <summary>
        ///     已提现
        /// </summary>
        [Description("已提现")] Withdrawed = 8,

        /// <summary>
        /// 正常还款完成
        /// </summary>

        [Description("还款完成")] Finished = 16,

        /// <summary>
        ///     已关闭
        /// </summary>
        [Description("已关闭")] Closed = 32
    }
}
