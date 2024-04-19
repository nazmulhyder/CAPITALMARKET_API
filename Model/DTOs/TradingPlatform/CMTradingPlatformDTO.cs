using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradingPlatform
{
    public class CMTradingPlatformDTO
    {
        public Nullable<int> TotalRowCount { get; set; }
        public Nullable<int> TradingPlatformID { get; set; }

        public string? PlatformName { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> ExchangeID { get; set; }
        public string? ExchangeName { get; set; }
        public string? ExchangeShortName { get; set; }
    }
}
