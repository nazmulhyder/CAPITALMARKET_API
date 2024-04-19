using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradingPlatform
{
    public class SLTradeNettingRuleSetDTO
    {
        public Nullable<int> TotalRowCount { get; set; }
        public int? NettingRuleSetID { get; set; }
        public string? NettingRuleSetName { get; set; } = String.Empty;
        public bool? IsNettingAllowed { get; set; }
        public string? NettingType { get; set; } = String.Empty;
        public int? ExchangeID { get; set; }
        public string? ExchangeName { get; set; } = String.Empty;
        public string? ExchangeShortName { get; set; } = String.Empty;
        public List<SLTradeNettingRuleSetDetailDTO>? SLTradeNettingRuleSetDetails { get; set; }

    }

    public class SLTradeNettingRuleSetDetailDTO
    {
        public int? NettingRuleSetDetailID { get; set; }
        public int? NettingRuleSetID { get; set; }
        public int? MarketID { get; set; }
        public int? CategoryID { get; set; }
        public string? Category { get; set; }
        public string? MarketName { get; set; }
        public string? MarketShortName { get; set; }
        public Nullable<int> TradingPlatformID { get; set; }
        public string? PlatformName { get; set; }
    }
}
