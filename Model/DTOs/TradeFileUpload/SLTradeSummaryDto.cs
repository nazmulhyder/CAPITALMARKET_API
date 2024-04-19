using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeFileUpload
{
    public class SLTradeSummaryDto
    {
		public Nullable<DateTime> TradeDate { get; set; }

		public Nullable<int> ProductID { get; set; }
		public Nullable<int> TradeFileID { get; set; }
		public Nullable<int> BrokerID { get; set; }
		public string? BrokerName { get; set; }
		public string? ProductName { get; set; }
		public Nullable<int> InstrumentCount { get; set; }
		public Nullable<decimal> TotalBuyCommission { get; set; }
		public Nullable<decimal> TotalSellCommission { get; set; }
		public Nullable<decimal> DSESellCommission { get; set; }
		public Nullable<decimal> DSEBuyCommission { get; set; }
		public Nullable<decimal> CSESellCommission { get; set; }

		public Nullable<decimal> CSEBuyCommission { get; set; }
		public Nullable<decimal> DSESellTradeAmount { get; set; }
		public Nullable<decimal> DSEBuyTradeAmount { get; set; }
		public Nullable<decimal> CSESellTradeAmount { get; set; }
		public Nullable<decimal> CSEBuyTradeAmount { get; set; }

		public Nullable<decimal> TotalSellTradeAmount { get; set; }
		public Nullable<decimal> TotalBuyTradeAmount { get; set; }


		public Nullable<DateTime> ApprovalDate { get; set; }
	public string? ApprovalRemarks { get; set; }

	public string? ApprovalStatus { get; set; }
	public string? Approver { get; set; }
}
	public class ILTradeSummaryDto
    {
		public Nullable<int> TradeFileID { get; set; }

		public Nullable<int> BrokerID { get; set; }
		public string BrokerName { get; set; }
		public Nullable<DateTime> TradeDate { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string ProductName { get; set; }

		public Nullable<decimal> TotalBuyCommission { get; set; }
		public Nullable<decimal> TotalSellCommission { get; set; }
		public Nullable<decimal> DSESellCommission { get; set; }
		public Nullable<decimal> DSEBuyCommission { get; set; }
		public Nullable<decimal> CSESellCommission { get; set; }

		public Nullable<decimal> CSEBuyCommission { get; set; }
		public Nullable<decimal> DSESellTradeAmount { get; set; }
		public Nullable<decimal> DSEBuyTradeAmount { get; set; }
		public Nullable<decimal> CSESellTradeAmount { get; set; }
		public Nullable<decimal> CSEBuyTradeAmount { get; set; }

		public Nullable<decimal> TotalSellTradeAmount { get; set; }
		public Nullable<decimal> TotalBuyTradeAmount { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string ApprovalRemarks { get; set; }
		public string ApprovalStatus { get; set; }

		public string Approver { get; set; }

}

	public class AMLProductDto
	{
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> TotalTrade { get; set; }
        public Nullable<decimal> TotalCommission { get; set; }
        public Nullable<decimal> TotalBuy { get; set; }
        public Nullable<decimal> TotalSale { get; set; }
        public List<AMLTradeDetailDto> AMLTradeDetailList = new List<AMLTradeDetailDto>();
    }

    public class AMLTradeDetailDto
    {
        public string? ProductName { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? BrokerName { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Approver { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public Nullable<decimal> BPTrade { get; set; }
        public Nullable<decimal> BPBCommission { get; set; }
        public Nullable<decimal> BPBuy { get; set; }
        public Nullable<decimal> BPSale { get; set; }

        public List<AMLBrokerTradeDto> AMLBrokerTradeList = new List<AMLBrokerTradeDto>();
    }

    public class AMLBrokerTradeDto
    {
        public string BrokerName { get; set; }
        public string ExchangeName { get; set; }
        public string AccountNumber { get; set; }
        public string TradingCode { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public string SecurityCode { get; set; }

        public string TradeType { get; set; }
        public string Market { get; set; }
        public Nullable<decimal> TotalQuantity { get; set; }
        public Nullable<decimal> AveragePrice { get; set; }
        public Nullable<decimal> TotalTrade { get; set; }
        public Nullable<decimal> TotalCommission { get; set; }
        public Nullable<decimal> NettradeAmount { get; set; }
    }



}
