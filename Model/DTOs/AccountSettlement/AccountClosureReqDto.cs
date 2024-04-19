using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AccountSettlement
{
	public class AccountClosureReqDto
	{

		public Nullable<int> ContractID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public string? AccountNumber { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }

		public Nullable<int> AgrClosureReqID { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> DocumentCount { get; set; }
		public string? DocumentPath { get; set; }
		public string? Maker { get; set; }

		public Nullable<DateTime> MakeDate { get; set; }
		public string? Reason { get; set; }
		public string? Remarks { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }

		public string? Approver { get; set; }
		public Nullable<decimal> Equity { get; set; }
		public Nullable<decimal> LedgerBalance { get; set; }
		public Nullable<decimal> MarketValue { get; set; }
		public Nullable<decimal> AvailableBalance { get; set; }

		public Nullable<decimal> SalesReceivables { get; set; }
		public Nullable<decimal> IPOAmount { get; set; }
		public Nullable<decimal> RightAmount { get; set; }
		public Nullable<decimal> AccruedCharge { get; set; }
		public Nullable<decimal> DividendReceivable { get; set; }

		public Nullable<decimal> ValueOfFDR { get; set; }
		
		public Nullable<int> DisbursementID { get; set; }

		public Nullable<DateTime> DisburseDate { get; set; }
		public Nullable<decimal> DisburseAmount { get; set; }
		public string? DisbursementHeadCode { get; set; }
		public Nullable<DateTime> ProcessingDate { get; set; }
		public Nullable<DateTime> InstrumentDate { get; set; }

		public string? InstrumentType { get; set; }
		public string? InstrumentNumber { get; set; }
		public int? BankAccountID { get; set; }
	}
}
