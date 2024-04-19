using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AMLMF
{
	public class FundDuePaymenApproveDto
	{
		public string? ChrgDuePaymentIDs { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? Remark { get; set; }
}

	public class AMLMFDuePaymentDto
	{


		public Nullable<int> FundID { get; set; }
		public Nullable<int> ChrgDuePaymentID { get; set; }
		
		public string? TenorStartDate { get; set; }
		public string? TenorEndDate { get; set; }
		public Nullable<int> AgreementChargeID { get; set; }
		public Nullable<decimal> TotalCharge { get; set; }
		public Nullable<decimal> TotalTax { get; set; }
		public Nullable<decimal> TotalVat { get; set; }
		public int? BankAccountID { get; set; }
		public string? PaymentMode { get; set; }
		public string? BankAccountNo { get; set; }
		public string? BankAccountName { get; set; }
		public string? BankName { get; set; }
		public List<AMLMFDuePaymentChargesDto>? ChargeTransactionList { get; set; }
	}

	public class AMLMFDuePaymentChargesDto
	{
		public Nullable<int> Summaryid { get; set; }

		public Nullable<DateTime> TransactionDate { get; set; }
		public Nullable<int> FundId { get; set; }
		public Nullable<int> AttributeID { get; set; }
		public string? AtributeName { get; set; }
		public string? ReferenceNumber { get; set; }

		public Nullable<decimal> AssetsValue { get; set; }
		public Nullable<decimal> NetChargesAssets { get; set; }
		public Nullable<decimal> Liabilities { get; set; }
		public Nullable<decimal> NetChargesLiabilities { get; set; }
		public Nullable<decimal> NetAssetValues { get; set; }
		public Nullable<decimal> NetAmount { get; set; }

		public Nullable<decimal> ActualNetAssetValues { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
		public Nullable<decimal> TaxAmount { get; set; }
		public Nullable<decimal> VatAmount { get; set; }
		public Nullable<decimal> TotalAmount { get; set; }

		public Nullable<Boolean> IsvoucherPosted { get; set; }
		public Nullable<int> TransactionID { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> AgreementChargeID { get; set; }

	}

	public class AMLMFAdvPaymentApprovalDto
	{
        public string? ChrgAdvPaymentIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }
	public class AMLMFAdvPaymentProjectedChargeDto
	{
        public Nullable<decimal> NavAtStartDate { get; set; }
        public decimal? ProjectedCharge { get; set; }
    }
	public class AMLMFAdvPaymentDto
	{
		public Nullable<int> FundID { get; set; }

		public string? FundName { get; set; }
		public Nullable<int> LastChrgAdvPaymentID { get; set; }
		public Nullable<int> AgreementChargeID { get; set; }
		public string? LastTenorStartDate { get; set; }
		public string? LastTenorEndDate { get; set; }
		public string? LastPaymentDate { get; set; }

		public decimal? LastProjectedCharge { get; set; }
		public decimal? LastCalculatedCharge { get; set; }
		public decimal? Deviation { get; set; }
		public string? TenorStartDate { get; set; }
		public string? TenorEndDate { get; set; }
		public string? NAVDate { get; set; }

		public decimal? NavAtStartDate { get; set; }
		public decimal? ProjectedCharge { get; set; }
		public decimal? ProjectedChargeTAX { get; set; }
		public decimal? ProjectedChargeVAT { get; set; }
		public decimal? TotalPayable { get; set; }
		public decimal? NetPayable { get; set; }
		public string? TransactionDate { get; set; }
		public string? Maker { get; set; }

		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? Approver { get; set; }
		public string? PaymentMethod { get; set; }
		public int? BankAccountID { get; set; }
		public string? InstrumentNumber { get; set; }

	}
}
