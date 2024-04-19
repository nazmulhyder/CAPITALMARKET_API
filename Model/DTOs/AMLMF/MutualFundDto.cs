using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AMLMF
{
	public class AMLMFBAInterestAdjustmentApproveDto
	{
        public string? IntAdjustmentIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }
    public class AMLMFBAInterestAdjustmentReversalDto
    {
      
        public string? ReversalReason { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? IntAdjustmentID { get; set; }
    }
    public class AMLMFBAInterestAdjustmentReversalApprovalDto
    {
        public int? IntCollReversalID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
       
    }
    public class AMLMFBAInterestAdjustmentDto

	{
		public Nullable<int> IntAdjustmentID { get; set; }

		public string? DateFrom { get; set; }
		public string? DateTo { get; set; }
		public Nullable<decimal> AdjustmentAmount { get; set; }
		public Nullable<Boolean> IsDebited { get; set; }
		public string? Reason { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> TransactionID { get; set; }
		public Nullable<int> MFBankAccountID { get; set; }

		public string? Approver { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? FundName { get; set; }
		public int? FundID { get; set; }
		public string? BankAccountName { get; set; }
		public string? BankAccountNo { get; set; }
		public int? BankOrgID { get; set; }
		public string? BankOrgName { get; set; }
		public string? BranchName { get; set; }
		public string? RoutingNo { get; set; }
	}


	public class AMLIntrestAccualCollectDto
	{
		public Nullable<int> MFBABalanceID { get; set; }
		public Nullable<DateTime> TransactionDate { get; set; }
		public Nullable<decimal> ClosingBalance { get; set; }
		public Nullable<decimal> InterestRate { get; set; }
		public Nullable<decimal> InterestAmount { get; set; }

	}
    public class AMLIntrestAdjustmentCollectDto
    {
        public Nullable<decimal> AccruedInterest { get; set; }
        public Nullable<decimal> AccruedAIT { get; set; }
        public Nullable<decimal> AdjustmentInterest { get; set; }
        public Nullable<decimal> AdjustmentAIT { get; set; }
        public Nullable<decimal> ExciseDuty { get; set; }
        public Nullable<decimal> CollecationInterest { get; set; }
        public String? CollectionDate { get; set; }
        public List<AMLIntrestAccualCollectDto> AMLIntrestAccualCollectDtoList { get; set; }

    }
    public class AMLIntrestAccualList
	{
		public Nullable<int> MFBankAccountID { get; set; }
		public string? FromDate { get; set; }
		public string? ToDate { get; set; }

	}


	public class AMLFundDto
	{
		public Nullable<int> FundID { get; set; }

		public int? ContractID { get; set; }
        public string? FundName { get; set; }

		public List<AMLFundBankAccountDto> BankAccountList { get; set; }
	}

	public class AMLFundBankAccountDto
	{
		public Nullable<int> FundID { get; set; }

		public Nullable<int> MFBankAccountID { get; set; }
		public string? BankAccountName { get; set; }
		public string? BankAccountNo { get; set; }
		public string? BankAccountType { get; set; }
		public string? BankOrgName { get; set; }

		public string? BranchName { get; set; }
		public string? RoutingNo { get; set; }
	}


    public class AMLFundMFDto
    {
        public Nullable<int> FundID { get; set; }

        public int? ContractID { get; set; }
        public string? FundName { get; set; }

        public List<AMLFundBankAccountMFDto> BankAccountList { get; set; }
    }

    public class AMLFundBankAccountMFDto
    {
        public Nullable<int> FundID { get; set; }

        public Nullable<int> MFBankAccountID { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountType { get; set; }
        public string? BankOrgName { get; set; }

        public string? BranchName { get; set; }
        public string? RoutingNo { get; set; }
    }


}
