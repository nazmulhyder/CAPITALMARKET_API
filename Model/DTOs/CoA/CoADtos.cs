using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.CoA
{
	public class AccAgreementDetailDto
	{
		public Nullable<int> ContractID { get; set; }

		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountStatus { get; set; }

		public string? ApprovalStatus { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }

		public string? AccountType { get; set; }
		public Nullable<int> BankAccountID { get; set; }
		public string? AccountName { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? BankAccountType { get; set; }

		public Nullable<int> BankOrgID { get; set; }
		public Nullable<int> BankOrgBranchID { get; set; }
		public string? BankOrgName { get; set; }
		public string? BranchName { get; set; }
		public string? RoutingNo { get; set; }

		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? Approver { get; set; }

		public List<AccCifBranchDto> BankList { get; set; }	
	}

	public class AccAgreementApproveDto
	{
		public string ContractIDs { get; set; }
		public string ApproveStatus { get; set; }
		public string ApprovalRemark { get; set; }
	}
	public class AccCifListDto
	{
		public Nullable<int> ContractID { get; set; } = 0;
		public Nullable<int> IndexID { get; set; }

		public string? MemberType { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }

		public string? AccountOpenDate { get; set; }
		public string? AccountNumber { get; set; }
		public string? BankAccountNumber { get; set; }
		public string? AccountType { get; set; }
		public Nullable<int> BankAccountID { get; set; }
		public string? Address { get; set; }

		public string? AddressType { get; set; }
		public string? District { get; set; }
		public string? Division { get; set; }
		public string? Country { get; set; }

		public List<AccCifBranchDto> BankList { get; set; }
	}

	public class AccCifBranchDto
	{
		public Nullable<int> IndexID { get; set; }
		public Nullable<int> BankAccountID { get; set; }

		public string? AccountType { get; set; }
		public string? AccountNumber { get; set; }
		public Nullable<int> BankOrgID { get; set; }
		public Nullable<int> BankOrgBranchID { get; set; }
		public string? BankOrgName { get; set; }

		public string? RoutingNo { get; set; }
		public string? BranchName { get; set; }
		public string? AccountName { get; set; }
		
	}


	public class AccVoucherApproveDto
	{
        public string VoucherIDs { get; set; }
        public string ApproveStatus { get; set; }
        public string ApprovalRemark { get; set; }
    }

	public class AccVoucherListParameterDto
	{
       
        public string? FromVoucherDate { get; set; }
        public string? ToVoucherDate { get; set; }
		public string? VoucherType { get; set; }
		public string? ApprovalStatus { get; set; }
        public string? VoucherNote { get; set; }
        public string? VoucherIssuer { get; set; }
        public string? VoucherRefNo { get; set; }
        public string? EntryType { get; set; }
        public int? LedgerHeadID { get; set; }
        public int? ProductID { get; set; }
        public string? AccountNumber { get; set; }
    }

	public class SingleAccVoucherDto
	{
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> VoucherID { get; set; }
		public Nullable<bool> IsPartyVoucher { get; set; }

		public string? VoucherNo { get; set; }
		public string? IssueDate { get; set; }
		public string? VoucherDate { get; set; }
		public string? VoucherStatus { get; set; }
		public string? VoucherType { get; set; }

		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? VoucherNote { get; set; }
		public string? Maker { get; set; }
		public string? InstrumentType { get; set; }
		public string? InstrumentNo { get; set; }
        public bool? IsVoucherReversed { get; set; }
        public string? NewVoucherNo { get; set; }
		public string? Approver { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? ApprovalDate { get; set; }
		public bool? IsPrepareInstrument { get; set; }
		public DateTime? MakeDate { get; set; }
		public List<SingleAccLedgerDto>? LedgerList { get; set; }
	}

	public class SingleAccLedgerDto
	{
		public Nullable<int> LedgerID { get; set; }

		public Nullable<int> VoucherID { get; set; }
		public Nullable<int> LedgerHeadID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public string? AccountNumber { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public Nullable<Boolean> IsDebited { get; set; }
		public string? LedgerNote { get; set; }

		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public string? ControlHead { get; set; }
		public int? ProductID { get; set; }
		public string? ProductName { get; set; }
		public dynamic? AccountList { get; set; }
		public dynamic? LedgerHeadList { get; set; }

	}

	



	public class AccVoucherDto
	{
		public Nullable<int> VoucherID { get; set; }
		public Nullable<int> ContractID { get; set; }

		public string? VoucherNo { get; set; }
		public string? IssueDate { get; set; }
		public string? VoucherDate { get; set; }
		public string? VoucherStatus { get; set; }
		public string? VoucherType { get; set; }

		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? VoucherNote { get; set; }
		public string? Maker { get; set; }
		public string? InstrumentType { get; set; }
		public string? InstrumentNo { get; set; }
		public bool? IsVoucherReversed { get; set; }
		public string? NewVoucherNo { get; set; }
		public string? Approver { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? ApprovalDate { get; set; }
        public bool? IsPrepareInstrument { get; set; }
        public List<AccLedgerDto>? LedgerList { get; set; }
	}

	public class AccLedgerDto
	{
		public Nullable<int> LedgerID { get; set; }

		public Nullable<int> VoucherID { get; set; }
		public Nullable<int> LedgerHeadID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public string? AccountNumber { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public Nullable<Boolean> IsDebited { get; set; }
		public string? LedgerNote { get; set; }

		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public string? ControlHead { get; set; }
		public int? ProductID { get; set; }
		public string? ProductName { get; set; }
	}



	public class ApproveVoucherPostingDateChangeDto
	{
		public Nullable<int> PostingDateID { get; set; }
		public string ApprovalStatus { get; set; }
		public string ApprovalRemark { get; set; }
	}

	public class VoucherPostingDateChangeDto
	{
		public Nullable<int> TransactionDayID { get; set; }
		public string TransactionDate { get; set; }
		public string VoucherPostingDate { get; set; }
		public string ChangeRemark { get; set; }
	}

	public class ApproveAccLedgerHead
	{
		public string? LedgerHeadIDs { get; set; }
		public string ApprovalStatus { get; set; }
		public string ApprovalRemark { get; set; }
	}
	public class AccLedgerHeadBulkDto
	{
		public Nullable<int> LedgerHeadID { get; set; }
		public int? COAID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> ApprovalReqSetID { get; set; }
		public bool? IsActive { get; set; }
		public bool? EnableJournalVoucher { get; set; } = false;
		public bool? EnablePaymentVoucher { get; set; } = false;
		public bool? EnableCollectionVoucher { get; set; } = false;
		public bool? EnableBalanceCheck { get; set; } = false;
		public bool? EnableBankAdjustmentVoucher { get; set; } = false;
		public bool? EnableNAVProcess { get; set; } = false;
	}
	public class AccLedgerHeadDto
	{
		public Nullable<int> LedgerHeadID { get; set; }
		public string? COAID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public Nullable<int> ApprovalReqSetID { get; set; }
        public bool? IsActive { get; set; }
		public bool? EnableJournalVoucher { get; set; } = false;
        public bool? EnablePaymentVoucher { get; set; } = false;
		public bool? EnableCollectionVoucher { get; set; } = false;
		public bool? EnableBalanceCheck { get; set; } = false;
		public bool? EnableBankAdjustmentVoucher { get; set; } = false;
		public bool? EnableNAVProcess { get; set; } = false;
    }

	public class ApproveCoA
	{
		public string? COAIDs { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemark{ get; set; }
    }

	public class CoADto
	{
		public Nullable<int> COAID { get; set; }
		public string? GLCode { get; set; }
		[Required]
		public string GLName { get; set; }
		public string? ParentGLCode { get; set; }
		public string? key { get; set; }
		public string? label { get; set; }
		[Required]
		public string BalanceType { get; set; }
		public string? ControlHead { get; set; }

		public Nullable<int> AccLevel { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? Approver { get; set; }
		public DateTime? ApprovalDate { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }

		public List<CoADto>? nodes { get; set; }
	}

	public class CoAReverseDto
	{
		public Nullable<int> COAID { get; set; }
		public string? GLCode { get; set; }
		[Required]
		public string GLName { get; set; }
		public string? ParentGLCode { get; set; }
		public string? key { get; set; }
		public string? label { get; set; }
		[Required]
		public string BalanceType { get; set; }
		public string? ControlHead { get; set; }

		public Nullable<int> AccLevel { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<bool> IsLeafNode { get; set; } = false;
		public Nullable<bool> IsSelectedInLedger { get; set; } = false;
		public Nullable<bool> EnableBalanceCheck { get; set; } 
		public Nullable<bool> EnableCollectionVoucher { get; set; }
		public Nullable<bool> EnablePaymentVoucher { get; set; }
		public Nullable<bool> EnableJournalVoucher { get; set; }
		public Nullable<bool> EnableBankAdjustmentVoucher { get; set; }
		public bool? EnableNAVProcess { get; set; } = false;
		public int? ProductID { get; set; }
		public CoAReverseDto? ParentCoA { get; set; }

		public List<string> ParentCoATree { get; set; } = new List<string>(5);
	}
}
