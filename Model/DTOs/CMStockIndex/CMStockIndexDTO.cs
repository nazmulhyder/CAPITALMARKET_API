using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.CMStockIndex
{
    public class CMStockIndexDTO
    {
        public int? TotalRowCount { get; set; }
        public Nullable<int> IndexID { get; set; }

        public string? IndexName { get; set; }
        public string? ShortName { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<DateTime> TradeDate { get; set; }
        public string? TradeDateInString { get; set; }
        public Nullable<int> ExchangeID { get; set; }
        public string? Description { get; set; }

        public string? ExchangeName { get; set; }
        public string? ExchangeShortName { get; set; }
    }
}
