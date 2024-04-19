using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AccountSettlement
{
	public class AccountSuspensionWithdrawalDto
	{
		public Nullable<int> AgrSuspensionID { get; set; }

		public Nullable<int> ContractID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public Nullable<decimal> Equity { get; set; }
		public Nullable<decimal> LedgerBalance { get; set; }
		public Nullable<decimal> MarketValue { get; set; }
		public string? ProductName { get; set; }
		public string? Reason { get; set; }
		public string? Remarks { get; set; }
		public string? CDBLStatus { get; set; }
		public DateTime? SuspensionDate { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }

		public string? RestrictionStatus { get; set; }
		public Nullable<int> DocumentCount { get; set; }
		public string? DocumentPath { get; set; }
		public string? UploadMode { get; set; }

		public Nullable<int> RequestDocumentCount { get; set; }
		public string? RequestDocumentPath { get; set; }
		public string? RequestUploadMode { get; set; }

		public string? AccountNumber { get; set; }
		public string? MemberCode { get; set; }

		public string? MemberName { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? Approver { get; set; }

		public string? SuspensionWithdrawalRemark { get; set; }
		public int? AgrSusWithdrawalID { get; set; }

	}

	public class AccountSuspensionApprovalDto
	{
		public string? AgrSuspensionIDs { get; set; }
		public string? ApprovalRemark { get; set; }
		public string? ApprovalStatus { get; set; }

	}
	public class AccountSuspensionWithdrawalApprovalDto
	{
		public string? AgrSusWithdrawalIDs { get; set; }
		public string? ApprovalRemark { get; set; }
		public string? ApprovalStatus { get; set; }

	}

	public class AccountSuspensionBulkDto
	{
		public string? AccountNumber { get; set; }
		public string? Reason { get; set; }
		public string? Remarks { get; set; }
		public string? CDBLStatus { get; set; }
		public string? RestrictionStatus { get; set; }
	}

	public class AccountSuspensionDto
	{
		public Nullable<int> AgrSuspensionID { get; set; }

		public Nullable<int> ContractID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public Nullable<decimal> Equity { get; set; }
		public Nullable<decimal> LedgerBalance { get; set; }
		public Nullable<decimal> MarketValue { get; set; }
		public string? ProductName { get; set; }
		public string? Reason { get; set; }
		public string? Remarks { get; set; }
		public string? CDBLStatus { get; set; }
		public DateTime? SuspensionDate { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		
		public string? RestrictionStatus { get; set; }
		public Nullable<int> DocumentCount { get; set; }
		public string? DocumentPath { get; set; }
		public string? UploadMode { get; set; }
		public string? AccountNumber { get; set; }
		public string? MemberCode { get; set; }

		public string? MemberName { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? Approver { get; set; }

		public string? SuspensionWithdrawalRemark { get; set; }
		public int? AgrSusWithdrawalID { get; set; }
	}
}
