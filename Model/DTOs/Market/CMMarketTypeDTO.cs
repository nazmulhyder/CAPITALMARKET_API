using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Market
{
    public class CMMarketTypeDTO
    {
        public int? MarketID { get; set; }
        public string? MarketName { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> TradingPlatformID { get; set; }
        public int? ExchangeID { get; set; }

    }
    public class CMMarketTypeListDTO : CMMarketTypeDTO
    {
        public int? TotalRowCount { get; set; }
        public string? ExchangeName { get; set; }
        public string? PlatformName { get; set; }
        public string? ExchangeShortName { get; set; }
        public string? TradingShortName { get; set; }

    }
}
