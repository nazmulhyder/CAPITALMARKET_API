using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BrokerageCommision
{
    public class TradeDataForCommisionUpdateDto
    {
	
			public Nullable<int> ExchangeID { get; set; }

			public string? ExchangeName { get; set; }
			public string? Category { get; set; }
			public Nullable<int> CategoryID { get; set; }
			public Nullable<int> InstrumentTypeID { get; set; }
			public string? InstrumentType { get; set; }
			public Nullable<int> ProductID { get; set; }
			public string? ProductName { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? InstrumentName { get; set; }
		public string? TradeType { get; set; }
		public Nullable<decimal> NewCommissionRate { get; set; }
			public Nullable<decimal> NewCommissionAmount { get; set; }
			public Nullable<decimal> PrevCommissionRate { get; set; }

		public Nullable<decimal> PrevCommissionAmount { get; set; }
		public Nullable<decimal> TotalTrade { get; set; }
		
	}


	public class TradeDataCommisionUpdateContractListDto
	{
		public Nullable<int> ContractID { get; set; }

		public Nullable<DateTime> TradeDate { get; set; }
		public string? AccountNumber { get; set; }
		public string? MemberName { get; set; }
		public Nullable<decimal> TotalTrade { get; set; }
		public Nullable<decimal> NewCommissionAmount { get; set; }
        public string? ProductName { get; set; }
        public Nullable<decimal> PreviousCommissionAmount { get; set; }
	}
	public class ApproveTradeDataCommisionUpdatDto
	{
		public string? ContractID { get; set; }

		public Nullable<DateTime> TradeDate { get; set; }
		public bool IsApprove { get; set; }
		public string? ApprovalRemark { get; set; }
	}




}
