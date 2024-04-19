using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeSettlement
{
	public class ForeginTradeAllocationDto
	{
		public Nullable<int> TradeTransactionID { get; set; }
		public string? TradeType { get; set; }
		public string? AccountNumber { get; set; }

		public Nullable<int> TradeQuantity { get; set; }
		public Nullable<int> ContractID { get; set; }

		public Nullable<decimal> UnitPrice { get; set; }
		public string? AllocatedAccountNumber { get; set; }
		public Nullable<int> AllocatedTradeQuantity { get; set; }
		public Nullable<int> AllocatedContractID { get; set; }
		public Nullable<decimal> AllocatedUnitPrice { get; set; }
		public string? SettlementDate  { get; set; }
		
		
	}


	public class InstrumentWiseTradeDataDto
    {
        public Nullable<int> InstrumentID { get; set; }

        public string? InstrumentName { get; set; }
        public string? ISIN { get; set; }
        public string? TradeType { get; set; }
        public Nullable<int> TradeQuantity { get; set; }
        public Nullable<int> AllocatedQuantity { get; set; }
        public Nullable<int> TradeTransactionID { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> BrokerageCommission { get; set; }
        public Nullable<decimal> Howla { get; set; }
        public Nullable<decimal> Laga { get; set; }
        public Nullable<decimal> AIT { get; set; }
        public Nullable<decimal> TradeAmount { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberCode { get; set; }
        public string? MemberName { get; set; }
        public string? AllocatedAccountNumber { get; set; }
        public List<TradeAllocationDto>? AllocationAccountList { get; set; }
    }

    public class TradeAllocationDto
    {
        public Nullable<int> AllocationID { get; set; }

        public Nullable<int> ContractID { get; set; }
        public Nullable<int> TradeTransactionID { get; set; }
        public Nullable<int> TradeQuantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> TradeAmount { get; set; }
        public Nullable<decimal> BrokerageCommission { get; set; }

        public string? AccountNumber { get; set; }
        public string? BOCode { get; set; }
        public string? MemberName { get; set; }
        public string? MemberType { get; set; }
        public string? Maker { get; set; }

        public Nullable<DateTime> MakeDate { get; set; }
        public Nullable<int> ApprovalSetID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ISIN { get; set; }
        public string? AllocatedAccountNumber { get; set; }
        public string? TradeType { get; set; }
		public string? SettlementDate { get; set; }
	}

    public class ForeignTradeAllocationApproveDto
    {
        public string? TradeTransactionIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }

}
