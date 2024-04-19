using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FundCollection
{
    public class DDIFileDto
    {
        public int DDIFileID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? BankAccountID { get; set; }
        public string? FileName { get; set; }
        public int FileSizeInKB { get; set; }
        public string? FileExtension { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Approver { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
        public string? ApprovalRemark { get; set; }
        public string? BankOrgName { get; set; }
        public string? BankAccountNumber { get; set; }
        public decimal? TotalTransactionAmount { get; set; }
        public List<DDIFileTransactionTblDto> TransactionList { get; set; }
        public List<DDIFileReturnTblDto> ReturnList { get; set; }
    }

    public class DDIFileReturnTblDto
    {
        public string? MemberName { get; set; }
        public string? AccountNumber { get; set; }
        public string? InstrumentNumber { get; set; }
        public string? InstrumentType { get; set; }
        public string? InstrumentDate { get; set; }
        public string? BankOrgName { get; set; }
        public string? RoutingNo { get; set; }
        public string? BranchName { get; set; }
        public int? ContractID { get; set; }
        public int? AttemptSLNo { get; set; }
        public int? InstallmentID { get; set; }
        public int DDIFileID { get; set; }

        public int CMTQLCUSTID { get; set; }
        public string? DDT_BATCH_NO { get; set; }
        public string? RETDDICODE { get; set; }
        public string? RETURN_STATUS { get; set; }
        public string? RETURN_REASON { get; set; }
        public DateTime? RETURN_DATE { get; set; }
        public decimal TRANSACTION_AMT { get; set; }
        public string? DEBITOR_AC_NUMBER { get; set; }
        public string? DEBTOR_AC_NAME { get; set; }
        public DateTime? DEBIT_SENT_DATE { get; set; }
        public string? DEBITOR_AC_BANK { get; set; }
        public string? DEBITOR_AC_BRANCH { get; set; }
        public string? DEBITOR_REFERENCE { get; set; }

    }

    public class DDIFileTransactionTblDto
    {
		public Nullable<int> DDIFileTransactionID { get; set; }

		public Nullable<int> DDIFileID { get; set; }
		public string? MandateReference { get; set; }
		public string? CreditAccount { get; set; }
		public string? CreditCurrency { get; set; }
		public string? CreditBankCode { get; set; }

		public string? Company { get; set; }
		public string? CompanyName { get; set; }
		public string? CreditAccountCountry { get; set; }
		public string? BatchNoBatchName { get; set; }
		public string? DepositDate { get; set; }

		public string? CustomerYourReference { get; set; }
		public string? InstrumentStatus { get; set; }
		public string? PayerName { get; set; }
		public string? PayerAccountNumber { get; set; }
		public string? PayerBankCode { get; set; }

		public Nullable<decimal> TransactionAmount { get; set; }
		public string? ValueDate { get; set; }
		public string? DebitCreditFlag { get; set; }
		public string? Channel { get; set; }
		public string? Status { get; set; }

		public string? Product { get; set; }
		public string? PayerBankName { get; set; }
		public string? MemberName { get; set; }
		public string? AccountNumber { get; set; }
		public string? BankOrgName { get; set; }
		public string? RoutingNo { get; set; }
		public string? BranchName { get; set; }
		public string? ReturnReason { get; set; }
		public int? ContractID { get; set; }
		public int? DDIGeneratedContractID { get; set; }
	}

    

    public class DDIFileTransactionDto
    {
        public int? DDIFileTransactionID { get; set; }
        public int? DDIFileID { get; set; }
        [JsonProperty("Mandate Reference")]
		public string? MandateReference { get; set; }

		[JsonProperty("Credit Account")]
		public string? CreditAccount { get; set; }

		[JsonProperty("Credit Currency")]
		public string? CreditCurrency { get; set; }

		[JsonProperty("Credit Bank Code")]
		public string? CreditBankCode { get; set; }
		public string? Company { get; set; }

		[JsonProperty("Company Name")]
		public string? CompanyName { get; set; }

		[JsonProperty("Credit Account Country")]
		public string? CreditAccountCountry { get; set; }

		[JsonProperty("Batch No. / Batch Name")]
		public string? BatchNoBatchName { get; set; }

		[JsonProperty("Deposit Date")]
		public string? DepositDate { get; set; }

		[JsonProperty("Customer/Your Reference")]
		public string? CustomerYourReference { get; set; }

		[JsonProperty("Instrument Status")]
		public string? InstrumentStatus { get; set; }

		[JsonProperty("Payer Name")]
		public string? PayerName { get; set; }

		[JsonProperty("Payer Account Number")]
		public string? PayerAccountNumber { get; set; }

		[JsonProperty("Payer Bank Code  ")]
		public string? PayerBankCode { get; set; }

		[JsonProperty("Transaction Amount")]
		public string? TransactionAmount { get; set; }

		[JsonProperty("Value Date")]
		public string? ValueDate { get; set; }

		[JsonProperty("Debit/Credit Flag")]
		public string? DebitCreditFlag { get; set; }
		[JsonProperty("Return Reason")]
		public string? ReturnReason { get; set; }
		public string? Channel { get; set; }
		public string? Status { get; set; }
		public string? Product { get; set; }

		[JsonProperty("Payer Bank Name")]
		public string? PayerBankName { get; set; }
	}
    public class DDIFileheadTagDto
    {
        public string? HeadTag { get; set; }

        public Nullable<int> HeadNumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? OrganizationName { get; set; }
    }

    public class DDIFileContentTagDto
    {

        public string? TransactionType { get; set; }

        public string? MemberName { get; set; }
        public string? InstrumentNumber { get; set; }
        public string? InstrumentDate { get; set; }

        public Nullable<decimal> Amount { get; set; }
        public string? CollectionType { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> ProductID { get; set; }

        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> InstallmentID { get; set; }
        public string? CollectionMode { get; set; }
        public string? Status { get; set; }

        public string? DueDate { get; set; }
        public Nullable<int> BankOrgID { get; set; }
        public Nullable<int> OwnbankOrgID { get; set; }
        public string? BankOrgName { get; set; }
        public string? BranchName { get; set; }

        public string? RoutingNo { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankCode { get; set; }
        public Nullable<Boolean> IsSameBank { get; set; }
    }

    public class DDIFileTailTagDto
    {
        public string? TailTag { get; set; }

        public Nullable<int> TransactionCount { get; set; }
        public decimal? TotalAmount { get; set; }
    }


    public class ScheduleListForDDIFileDto
    {
        public string? MemberName { get; set; }
       
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> InstallmentID { get; set; }

        public string? Status { get; set; }
        public string? DueDate { get; set; }
        public Nullable<int> DueDays { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> BankAccountID { get; set; }

        public string? BankOrgName { get; set; }
        public Nullable<int> BankOrgBranchID { get; set; }
        public string? BranchName { get; set; }
        public Nullable<int> RoutingID { get; set; }
        public string? RoutingNo { get; set; }

        public string? BankAccountNumber { get; set; }
    }

    public class SchedulenstrumentDto
    {
        public Nullable<int> InstCollectionID { get; set; }

        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? CollectionStatus { get; set; }
        public Nullable<int> InstallmentID { get; set; }

        public Nullable<decimal> Amount { get; set; }
        public string? CollectionMode { get; set; }
        public string?   DueDate { get; set; }
        public Nullable<int> DueDays { get; set; }
        public string? InstrumentDate { get; set; }

        public string? InstrumentNumber { get; set; }
        public Nullable<decimal> InstrumentAmount { get; set; }
    }
    public class InstallmentScheduleDto
    {
        public string? MemberName { get; set; }

        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> InstallmentID { get; set; }
        public string? Status { get; set; }
        public string? DueDate { get; set; }

        public Nullable<int> DueDays { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string? CollectionMode { get; set; }
        public string Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }

    }
    public class CollectionListForInstallmentTagDto
    {
        public string? MemberName { get; set; }

        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<decimal> CollectionAmount { get; set; }
        public string? EntryType { get; set; }
        public string? InstrumentType { get; set; }

        public string? CollectionStatus { get; set; }
        public string? InstrumentNumber { get; set; }
        public Nullable<int> MonInstrumentID { get; set; }
        public string? InstrumentDate { get; set; }
        public string? CollectionDate { get; set; }

        public string? Status { get; set; }
        public string? ApprovalStatus { get; set; }
        public Nullable<int> InstallmentID { get; set; }
    }
    public class BankStatementTransactionDto
    {
        public int? StatementTransactionID { get; set; }
        public string? AccountName { get; set; }
        public string? CustomerReference  { get; set; }
        public string? TransactionReference { get; set; }
        public string? AsAtDate{ get; set; }
        public string? VAFlag { get; set; }
        public string? VANumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? DRCRFlag { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string? TransactionDate { get; set; }
        public string? TransactionDetailion { get; set; }
        public int? BankStatementFileID { get; set; }
    }
    public class BankAccountDto
    {
        public int DepositBankAccID { get; set; }
        public int BankAccountID { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? RoutingNo { get; set; }
        public int BankOrgID { get; set; }
        public int OrgBranchID { get; set; }
       
    }
    public class FundCollectionApprovalDto
    {
        public string? MonInstrumentIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
        public string? DepositBankAccountID { get; set; }
    }
    public class ScheduleInstallmentTagApprovalDto
    {
        public string? InstCollectionIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }
    public class FundCollectionDto
    {
        public string? MemberName { get; set; }

        public string? MemberCode { get; set; }
        public string? MemberType { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }

        public string? AccountStatus { get; set; }
        public string? AccountApprovalStatus { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> CollectionInfoID { get; set; }
        public string? CollectionDate { get; set; }

        public Nullable<decimal> CollectionAmount { get; set; }
        public Nullable<int> ApprovalSetID { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }
        public Nullable<int> MonInstrumentID { get; set; }

        public string? InstrumentType { get; set; }
        public Nullable<int> BankAccountID { get; set; }
        public Nullable<int> DepositBankAccountID { get; set; }
        public string? InstrumentDate { get; set; }
        public string? InstrumentNumber { get; set; }
        public string? InstrumentStatus { get; set; }

        public Nullable<int> InstIssuedByIndexID { get; set; }
        public Nullable<int> BankOrgID { get; set; }
        public string? BankOrgName { get; set; }
        public Nullable<int> BankOrgBranchID { get; set; }
        public string? BranchName { get; set; }

        public Nullable<int> RoutingID { get; set; }
        public string? RoutingNo { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Remarks { get; set; }

        public string? EntryType { get; set; }
        public string? Approver { get; set; }
        public string? ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; }
        public string? DepositToBankMaker { get; set; }

        public string? DepositToBankMakeDate { get; set; }
        public string? DepositToBankRemark { get; set; }
        public string? ClearDepositMaker { get; set; }
        public string? ClearDepositMakeDate { get; set; }
        public string? ClearDepositRemark { get; set; }

        public string? DishonorDepositMaker { get; set; }
        public string? DishonorDepositMakeDate { get; set; }
        public string? DishonorDepositRemark { get; set; }
        public string? CancelDepositMaker { get; set; }
        public string? CancelDepositMakeDate { get; set; }

        public string? CancelDepositRemark { get; set; }
        public string? CancelDepositApproveMaker { get; set; }
        public string? CancelDepositApproveMakeDate { get; set; }
        public string? CancelDepositApproveRemark { get; set; }
    }
    public class FundCollectionListDto
    {
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int ProductID { get; set; }
        public string? ListType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? RefNo { get; set; }
        public string? AccountNo { get; set; }
    }
    public class ProductDto
    {
        public Nullable<int> productID { get; set; }

        public string? productName { get; set; }
        public string? productOwner { get; set; }
        public string? productManagement { get; set; }
        public string? investmentMode { get; set; }
        public string? initialInvestment { get; set; }

        public string? maker { get; set; }
        public string? makeDate { get; set; }
        public string? approvalStatus { get; set; }
        public string? ProductPrefix { get; set; }
    }
}
