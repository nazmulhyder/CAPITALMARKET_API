using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradingPlatform
{
    public class SLTradeSettlementRuleDTO
    {
        public int InstrumentTypeID { get; set; }
        public int SettlementRuleID { get; set; }
        public int ExchangeID { get; set; }
        public int TradingPlatformID { get; set; }
        public int MarketID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> PurInstSettleAt { get; set; }
        public Nullable<int> PurFundPaymentAt { get; set; }
        public Nullable<int> SellInstSettleAt { get; set; }
        public Nullable<int> SellFundRcvAt { get; set; }
        public string? Remark { get; set; }
    }

    public class SLTradeSettlementRuleEditDTO 
    {
        public int SettlementRuleID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> PurInstSettleAt { get; set; }
        public Nullable<int> PurFundPaymentAt { get; set; }
        public Nullable<int> SellInstSettleAt { get; set; }
        public Nullable<int> SellFundRcvAt { get; set; }
        public int MarketID { get; set; }

        //
        public Nullable<int> TotalRowCount { get; set; }
        public string? Category { get; set; }
        public string? MarketName { get; set; }
        public string? MarketShortName { get; set; }
        public int? TradingPlatformID { get; set; }
        public string? TPlatformShortName { get; set; }
        public int? ExchangeID { get; set; }
        public string? ExchangeName { get; set; }
        public string? ExchangeShortName { get; set; }
        public string? Remark { get; set; }
        public int InstrumentTypeID { get; set; }
        public string InstrumentTypeName { get; set; }

    }
}
