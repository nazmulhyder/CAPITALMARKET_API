using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeFileUpload
{
    public class TradeSummaryDto
    {
		public Nullable<int> TradeFileID { get; set; }

		public string? FileName { get; set; }
		public Nullable<decimal> FileSizeInKB { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public string? ExchangeName { get; set; }
		public Nullable<DateTime> TradeDate { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }

	}

	public class TradeSummaryApprovalDto
    {
        public DateTime TradeDate { get; set; }
        public int CompanyID { get; set; }
		public int BrokerID { get; set; }
        public int ProductID { get; set; }
        public string? ApprovalRemark { get; set; }
    }

    public class NonMarginTradeDataDto
    {
        public Nullable<int> ProductID { get; set; }

        public string? ProductName { get; set; }
        public string? AccountNoPrefix { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<DateTime> TradeDate { get; set; }

        public Nullable<int> InstrumentID { get; set; }
        public Nullable<int> MarketID { get; set; }
        public Nullable<int> InstrumentGroupID { get; set; }
        public string? InstrumentName { get; set; }
        public string? TradeType { get; set; }
        public Nullable<decimal> TradeQuantity { get; set; }
        public Nullable<decimal> PEratio { get; set; }
    }

    public class ListOverBuyILDto
    {
        public Nullable<DateTime> TradeDate { get; set; }

        public Nullable<int> ContractID { get; set; }

        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccNo { get; set; }
        public Nullable<decimal> Equity { get; set; }
        public Nullable<decimal> REDR { get; set; }

        public Nullable<decimal> AvailableBalance { get; set; }
        public Nullable<decimal> SalesReceivables { get; set; }
        public Nullable<decimal> LoanBalance { get; set; }
        public Nullable<decimal> Ratio { get; set; }
        public Nullable<decimal> LoanRatio { get; set; }

        public Nullable<decimal> PurchasePower { get; set; }
        public Nullable<decimal> NetBuy { get; set; }
        public Nullable<decimal> NetSale { get; set; }
        public Nullable<decimal> PPSaleBuy { get; set; }
        public Nullable<decimal> CanBuy { get; set; }

        public Nullable<decimal> ExtraBuy { get; set; }
        public Nullable<decimal> BuySalePercentage { get; set; }
        public Nullable<decimal> ToDayBuySale { get; set; }
        public string? Statuss { get; set; }
        public string? NettingPercentage { get; set; }

        public Nullable<decimal> MarketValue { get; set; }
        public Nullable<decimal> CostValue { get; set; }

    }

}
