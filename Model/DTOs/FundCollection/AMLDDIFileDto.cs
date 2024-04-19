using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FundCollection
{

	public class AMLDDIFileTblDto
	{
		public Nullable<int> DDIFileID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public Nullable<int> BankAccountID { get; set; }
		public string FileName { get; set; }
		public Nullable<int> FileSizeInKB { get; set; }
		public string FileExtension { get; set; }

		public string Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string ApprovalStatus { get; set; }
		public string Approver { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }

		public string ApprovalRemark { get; set; }
		public string BankOrgName { get; set; }
		public string BankAccountNumber { get; set; }

		public List<AMLDDIFileTranTblDto>? TransactionList { get; set; }
	}

	public class AMLDDIFileTranTblDto
	{
		public string MemberName { get; set; }

		public Nullable<int> ContractID { get; set; }
		public string AccountNumber { get; set; }
		public string BankOrgName { get; set; }
		public string RoutingNo { get; set; }
		public string BranchName { get; set; }

		public Nullable<int> DDITransactionID { get; set; }
		public Nullable<int> DDIFileID { get; set; }
		public string SL { get; set; }
		public string BatchReferenceID { get; set; }
		public string CollectionMethod { get; set; }

		public string MandateReferenceID { get; set; }
		public string Purpose { get; set; }
		public string TransactionReferenceID { get; set; }
		public string ModeofTransaction { get; set; }
		public string CreditAccountName { get; set; }

		public string CreditAccountNumber { get; set; }
		public string DebitAccountName { get; set; }
		public string DebitAccountNumber { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public string CreditNarration { get; set; }

		public string RoutingNumber { get; set; }
		public string Bank { get; set; }
		public string Branch { get; set; }
		public string UploadDateTime { get; set; }
		public string CycleType { get; set; }

		public string SIStartDate { get; set; }
		public string SIEndDate { get; set; }
		public string ReceiversEmail { get; set; }
		public string ReceiversMobileNumber { get; set; }
		public string Status { get; set; }

		public string ExecutionDateTime { get; set; }
		public string ExecutionSession { get; set; }
		public string CreditDateTime { get; set; }
		public string CreditSession { get; set; }
		public string ReturnDateTime { get; set; }

		public string ReturnSession { get; set; }
		public string ReturnReason { get; set; }
		public string OriginalTransactionID { get; set; }
		public string BatchNoBatchName { get; set; }
		public string CompanyName { get; set; }

		public string CreditCurrency { get; set; }
		public string DebitCreditFlag { get; set; }
		public string DebitBankcode { get; set; }
		public string DebtorName { get; set; }
		public string DebtorReference { get; set; }

		public string CustomerYourReference { get; set; }
		public string ValueDate { get; set; }
		
		public Nullable<int> DDIGeneratedContractID { get; set; }
	}

	public class AMLBBLSCBDDIFileDto
	{
	   public int? DDITransactionID { get; set; }
	   public int? DDIFileID { get; set; }
	
		[JsonProperty("S/L.")]
		public string? SL { get; set; }

		[JsonProperty("Batch Reference ID")]
		public string? BatchReferenceID { get; set; }

		[JsonProperty("Collection Method")]
		public string? CollectionMethod { get; set; }
		public string? Purpose { get; set; }

		[JsonProperty("Mandate Reference ID")]
		public string? MandateReferenceID { get; set; }

		[JsonProperty("Transaction Reference ID")]
		public string? TransactionReferenceID { get; set; }

		[JsonProperty("Mode of Transaction")]
		public string? ModeofTransaction { get; set; }

		[JsonProperty("Credit Account Name")]
		public string? CreditAccountName { get; set; }

		[JsonProperty("Credit Account Number")]
		public string? CreditAccountNumber { get; set; }

		[JsonProperty("Debit Account Name")]
		public string? DebitAccountName { get; set; }

		[JsonProperty("Debit Account Number")]
		public string? DebitAccountNumber { get; set; }
		public string? Amount { get; set; }

		[JsonProperty("Credit Narration")]
		public string? CreditNarration { get; set; }

		[JsonProperty("Routing Number")]
		public string? RoutingNumber { get; set; }
		public string? Bank { get; set; }
		public string? Branch { get; set; }

		[JsonProperty("Upload Date & Time")]
		public string? UploadDateTime { get; set; }

		[JsonProperty("Cycle Type")]
		public string? CycleType { get; set; }

		[JsonProperty("SI Start Date")]
		public string? SIStartDate { get; set; }

		[JsonProperty("SI End Date")]
		public string? SIEndDate { get; set; }

		[JsonProperty("Receiver's Email")]
		public string? ReceiversEmail { get; set; }

		[JsonProperty("Receiver's Mobile Number")]
		public string? ReceiversMobileNumber { get; set; }
		public string? Status { get; set; }

		[JsonProperty("Execution Date & Time")]
		public string? ExecutionDateTime { get; set; }

		[JsonProperty("Execution Session")]
		public string? ExecutionSession { get; set; }

		[JsonProperty("Credit Date & Time")]
		public string? CreditDateTime { get; set; }

		[JsonProperty("Credit Session")]
		public string? CreditSession { get; set; }

		[JsonProperty("Return Date & Time")]
		public string? ReturnDateTime { get; set; }

		[JsonProperty("Return Session")]
		public string? ReturnSession { get; set; }

		[JsonProperty("Return Reason")]
		public string? ReturnReason { get; set; }

		[JsonProperty("Original Transaction ID")]
		public string? OriginalTransactionID { get; set; }

		[JsonProperty("Batch No. / Batch Name")]
		public string? BatchNoBatchName { get; set; }

		[JsonProperty("Company Name")]
		public string? CompanyName { get; set; }

		[JsonProperty("Credit Account")]
		public string? CreditAccount { get; set; }

		[JsonProperty("Credit Currency")]
		public string? CreditCurrency { get; set; }
		
		[JsonProperty("Debit/Credit Flag")]
		public string? DebitCreditFlag { get; set; }

		
		[JsonProperty("Debit Bank code")]
		public string? DebitBankcode { get; set; }

		[JsonProperty("Debtor Name")]
		public string? DebtorName { get; set; }

		[JsonProperty("Debtor Reference")]
		public string? DebtorReference { get; set; }

		[JsonProperty("Customer/Your-Reference")]
		public string? CustomerYourReference { get; set; }

		[JsonProperty("Value Date")]
		public string? ValueDate { get; set; }

	}

	
}
