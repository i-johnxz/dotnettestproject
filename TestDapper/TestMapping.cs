using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Xunit;
using Xunit.Abstractions;

namespace TestDapper
{
    public class TestMapping
    {
        private readonly ITestOutputHelper _output;

        public static string ConnectionString =>
            @"Server=(localdb)\projects;Database=Asset;Integrated Security=true;Enlist=false";

        public TestMapping(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly Type[] _types = new[]
        {
            typeof(BrokerLoanProduct),
            typeof(DecimalRange),
            typeof(LifeOfLoan),
            typeof(DecimalRange),
            typeof(DecimalRange),
            typeof(DecimalRange),
            typeof(FundingBrokerSetting),
            typeof(InternalRiskControlSetting),
            typeof(IdName),
            typeof(IdName),
            typeof(GuaranteeInfo)
        };

        private readonly Func<object[], BrokerLoanProduct> _map = objects =>
        {
            var borkerLoanProduct = (BrokerLoanProduct)objects[0];
            borkerLoanProduct.AmountRange = (DecimalRange) objects[1];
            borkerLoanProduct.LifeOfLoan = (LifeOfLoan) objects[2];
            borkerLoanProduct.FeeRate = new FeeRate
            {
                PlatformFeeRate = (DecimalRange)objects[3],
                AssetBrokerFeeRate = (DecimalRange)objects[4],
                FundingBrokerFeeRate = (DecimalRange)objects[5]
            };
            borkerLoanProduct.FundingBrokerSetting = (FundingBrokerSetting) objects[6];
            borkerLoanProduct.InternalRiskControlSetting = (InternalRiskControlSetting) objects[7];
            borkerLoanProduct.InternalRiskControlSetting.AuditRuleGroup = (IdName) objects[8];
            borkerLoanProduct.InternalRiskControlSetting.ScoreRuleGroup = (IdName) objects[9];
            borkerLoanProduct.Guarantee = (GuaranteeInfo) objects[10];

            return borkerLoanProduct;
        };


        private readonly string _splitOn = "AmountRange_From,LifeOfLoan_TermsValue,FeeRate_PlatformFeeRate_From,FeeRate_AssetBrokerFeeRate_From,FeeRate_FundingBrokerFeeRate_From,FundingBrokerSetting_FundingBrokerId,InternalRiskControlSetting_NeedAudit,InternalRiskControlSetting_AuditRuleGroup_Id,InternalRiskControlSetting_ScoreRuleGroup_Id,Guarantee_GuaranteeId";



        [Fact]
        public async Task TestCustomeComplexType()
        {
            DataProviderExtension.SetTypeMap(new[]
            {
                typeof(DecimalRange),
                typeof(LifeOfLoan),
                typeof(FundingBrokerSetting),
                typeof(InternalRiskControlSetting),
                typeof(IdName),
                typeof(GuaranteeInfo),
            });




            using (var assetConnection = new SqlConnection(ConnectionString))
            {
                string sql = $@"SELECT
                      [CreatedTime]
                      ,[Id]
                      ,[Version]
                      ,[AssetBrokerId]
                      ,[AssetBrokerCode]
                      ,[Name]
                      ,[Code]
                      ,[BorrowerType]
                      ,[Type]
                      ,[AuditStatus]
                      ,[Enabled]
                      ,[Remark]
	                  ,[RepaymentMethod]
                      ,[InterestRate]
                      ,[InterestBearingPeriod]
                      ,[OverduePenaltyInterestRate]
                      ,[PrepaymentPenaltyInterestRate]
                      ,[AmountRange_From]
                      ,[AmountRange_To]
                      ,[LifeOfLoan_TermsValue]
                      ,[LifeOfLoan_Unit]
                      ,[FeeRate_PlatformFeeRate_From]
                      ,[FeeRate_PlatformFeeRate_To]
                      ,[FeeRate_AssetBrokerFeeRate_From]
                      ,[FeeRate_AssetBrokerFeeRate_To]
                      ,[FeeRate_FundingBrokerFeeRate_From]
                      ,[FeeRate_FundingBrokerFeeRate_To]
                      ,[FundingBrokerSetting_FundingBrokerId]
                      ,[FundingBrokerSetting_NeedWithholding]
                      ,[InternalRiskControlSetting_NeedAudit]
                      ,[InternalRiskControlSetting_NeedSystemAutoAudit]
                      ,[InternalRiskControlSetting_NeedManualReviewAfterSystemMissing]
                      ,[InternalRiskControlSetting_AuditRuleGroup_Id]
                      ,[InternalRiskControlSetting_AuditRuleGroup_Name]
                      ,[InternalRiskControlSetting_ScoreRuleGroup_Id]
                      ,[InternalRiskControlSetting_ScoreRuleGroup_Name]
                      ,[Guarantee_GuaranteeId]
                      ,[Guarantee_Type]
                  FROM [BrokerLoanProducts]
                  where Deleted = 0";

                var assetBroker = await assetConnection.QueryAsync(sql, _types, _map, splitOn: 
                    _splitOn).ConfigureAwait(false);

                 
            }
            
        }
    }


    public static class DataProviderExtension
    {
        public static void SetTypeMap(Type[] complexTypes)
        {
            foreach (var complexType in complexTypes)
            {
                SetTypeMap(complexType);
            }
        }

        public static void RemoveTypeMap<TComplexType>()
        {
            RemoveTypeMap(typeof(TComplexType));
        }


        public static void SetTypeMaps(Type[] complexTypes)
        {
            foreach (var complexType in complexTypes)
            {
                SetTypeMap(complexType);
            }
        }

        public static void RemoveTypes(Type[] complexTypes)
        {
            foreach (var complexType in complexTypes)
            {
                RemoveTypeMap(complexType);
            }
        }

        public static void SetTypeMap(Type complexType)
        {
            SqlMapper.SetTypeMap(complexType,
                new CustomPropertyTypeMap(complexType,
                    (type, columnName) =>
                    {
                        var test = type.GetProperties()
                            .FirstOrDefault(prop => columnName.EndsWith($"_{prop.Name}") || columnName.EndsWith($"{prop.Name}"));
                        return test;
                    }));
        }

        public static void RemoveTypeMap(Type complexType)
        {
            
            SqlMapper.RemoveTypeMap(complexType);
        }
    }


    public class BrokerLoanProduct
    {
        /// <summary>
        ///     标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        ///     资产渠道标识
        /// </summary>
        public long AssetBrokerId { get; set; }

        /// <summary>
        ///     资产渠道编码
        /// </summary>
        public string AssetBrokerCode { get; set; }

        /// <summary>
        ///     产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     接口调用时使用该编码对应到发标批次。不可重复
        ///     根据渠道编码+还款方式编码自动生成，如重复则加上数字序号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     借款产品主体类型 企业、个人
        /// </summary>
        public int BorrowerType { get; set; }
        

        /// <summary>
        ///     产品类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     借款金额范围
        /// </summary>
        public DecimalRange AmountRange { get; set; }

        /// <summary>
        ///     借款期限信息
        ///     如果期限的单位是“天”，还款方式只能是“一次性还本付息”；
        ///     如果期限的单位是“月”，各还款方式都可以
        /// </summary>
        public LifeOfLoan LifeOfLoan { get; set; }

        /// <summary>
        ///     还款方式
        /// </summary>
        public int RepaymentMethod { get; set; }

        /// <summary>
        ///     借款利率(年化)
        ///     保留两位小数，如10.25，即借款年化利率为10.25%
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        ///     起息日
        ///     默认：0，表示放款当天开始计息，如填1，表示放款次日开始计息
        /// </summary>
        public int InterestBearingPeriod { get; set; }

        /// <summary>
        ///     手续费率(年化)
        /// </summary>
        public FeeRate FeeRate { get; set; }

        /// <summary>
        ///     逾期罚息率
        ///     该罚息率是按天算的，不填默认0
        /// </summary>
        public decimal OverduePenaltyInterestRate { get; set; }

        /// <summary>
        ///     提前还款罚息率
        /// </summary>
        public decimal PrepaymentPenaltyInterestRate { get; set; }

        /// <summary>
        ///     资金渠道配置
        /// </summary>
        public FundingBrokerSetting FundingBrokerSetting { get; set; }

        /// <summary>
        ///     风控配置
        /// </summary>
        public InternalRiskControlSetting InternalRiskControlSetting { get; set; }

        /// <summary>
        ///     担保方信息
        /// </summary>
        public GuaranteeInfo Guarantee { get; set; }

        /// <summary>
        ///     审核状态
        ///     状态如：草稿、待审核、审核通过、审核不通过
        ///     注：待审核状态时不可修改借款产品信息，其他状态（草稿、审核通过、审核不通过状态）可以修改
        /// </summary>
        public int AuditStatus { get; set; }
        

        /// <summary>
        ///     是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        public DateTime CreatedTime { get; set; }
    }


    public class LifeOfLoan
    {
        public LifeOfLoan()
        {

        }
        

        public string TermsValue { get; set; }

        /// <summary>
        ///     天/月
        ///     如果期限的单位是“天”，还款方式只能是“一次性还本付息”；
        ///     如果期限的单位是“月”，各还款方式都可以
        /// </summary>
        public int Unit { get; set; }
        
    }

    public class FeeRate
    {

        public FeeRate()
        {

        }


        public FeeRate(DecimalRange platformFeeRate, DecimalRange assetBrokerFeeRate, DecimalRange fundingBrokerFeeRate)
        {
            PlatformFeeRate = platformFeeRate;
            AssetBrokerFeeRate = assetBrokerFeeRate;
            FundingBrokerFeeRate = fundingBrokerFeeRate;
        }

        /// <summary>
        ///     平台借款手续费率(年化)
        /// </summary>
        public DecimalRange PlatformFeeRate { get; set; }

        /// <summary>
        ///     资产端借款手续费率(年化)
        /// </summary>
        public DecimalRange AssetBrokerFeeRate { get; set; }

        /// <summary>
        ///     资金端借款手续费率(年化)
        /// </summary>
        public DecimalRange FundingBrokerFeeRate { get; set; }
    }

    public class FundingBrokerSetting
    {

        public FundingBrokerSetting()
        {

        }


        /// <summary>
        ///     资金渠道标识
        /// </summary>
        public long FundingBrokerId { get; set; }

        /// <summary>
        ///     是否需要代扣
        /// </summary>
        public bool NeedWithholding { get; set; }
    }

    public class InternalRiskControlSetting
    {

        public InternalRiskControlSetting()
        {

        }

        /// <summary>
        ///     是否需要审核
        /// </summary>
        public bool NeedAudit { get; set; }

        /// <summary>
        ///     是否风控系统自动审核
        /// </summary>
        public bool NeedSystemAutoAudit { get; set; }

        /// <summary>
        ///     自动审核未命中(审核通过)后是否人工审核
        /// </summary>
        public bool NeedManualReviewAfterSystemMissing { get; set; }

        /// <summary>
        ///     风控审核组标识
        /// </summary>
        public IdName AuditRuleGroup { get; set; }

        /// <summary>
        ///     风控评分组标识
        /// </summary>
        public IdName ScoreRuleGroup { get; set; }
    }



    public class GuaranteeInfo
    {

        public GuaranteeInfo()
        {

        }

        /// <summary>
        /// 担保方标识
        /// </summary>
        public long GuaranteeId { get; set; }
        /// <summary>
        /// 担保方类型
        /// </summary>
        public int Type { get; set; }
        
    }


    public class DecimalRange
    {

        public DecimalRange()
        {

        }

        public DecimalRange(decimal @from, decimal to)
        {
            From = @from;
            To = to;
        }

        public Decimal From { get; set; }
        public Decimal To { get; set; }
    }
}
