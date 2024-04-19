using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ForeignTradeCommissionShare
{
	public class ForeignTradeCommissionShareRecalculateDto
	{

		public Nullable<int> AllocationID { get; set; }
		public decimal? ShareToIDLC { get; set; }
		public Nullable<decimal> ShareToForeignBroker { get; set; }
		public string? Remarks { get; set; }
	}

	public class ForeignTradeCommissionShareDto
	{

		public Nullable<int> AllocationID { get; set; }

		public Nullable<DateTime> TradeDate { get; set; }
		public Nullable<int> ExecutionContractID { get; set; }
		public string? ExecutionAccountNumber { get; set; }
		public string? ExecutionMemberCode { get; set; }
		public string? ExecutionMemberName { get; set; }

		public Nullable<int> ClientContractID { get; set; }
		public string? ClientAccountNumber { get; set; }
		public string? ClientMemberCode { get; set; }
		public string? ClientMemberName { get; set; }
		public string? TradeType { get; set; }

		public string? InstrumentName { get; set; }
		public Nullable<int> NominalQty { get; set; }
		public Nullable<decimal> Turnover { get; set; }
		public Nullable<decimal> BrokerageCommissionRate { get; set; }
		public Nullable<decimal> BrokerageCommission { get; set; }

		public Nullable<decimal> ForeignBrokerCommission { get; set; }
		public Nullable<decimal> LocalBrokerCommission { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public Nullable<decimal> ForeignBrokerTax { get; set; }
		public Nullable<decimal> Laga { get; set; }
		public Nullable<decimal> ForeignBrokerLaga { get; set; }
		public Nullable<decimal> Hawla { get; set; }
		public Nullable<decimal> ForeignBrokerHawla { get; set; }
		public Nullable<decimal> FundingCost { get; set; }
		public Nullable<decimal> FundingCostForeignBroker { get; set; }
		public Nullable<decimal> FeesChargesForeignBroker { get; set; }
		public Nullable<decimal> DelaySettlementFundingCost { get; set; }
		public Nullable<decimal> NetCommissionPayable { get; set; }

		
		public Nullable<decimal> ShareToIDLC { get; set; }
		public Nullable<decimal> ShareToForeignBroker { get; set; }
		public string? Remarks { get; set; }
	}
}
