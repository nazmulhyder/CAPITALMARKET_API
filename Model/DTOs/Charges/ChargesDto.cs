using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Charges
{


    public class AccountChargeReversalEntryDto
    {
        public string? ChargeTransactionIDs { get; set; }
        public string? Remarks { get; set; }
        public string? ApprovalStatus { get; set; }
    }

    public class AccountChargeReversalDto
    {
        public string? ListType { get; set; }
        public int? AttributeID { get; set; }
        public string? AccountNumber { get; set; }
        public string? TransactionFrom { get; set; }
        public string? TransactionTo { get; set; }
    }
    public class AccountChargeForReversalEntryDto
    {
        public int? AttributeID { get; set; }
        public string? AccountNumber { get; set; }
        public string? TransactionFrom { get; set; }
        public string? TransactionTo { get; set; }
    }

    public class ManualChargeBulkDto
    {
        public string? MemberName { get; set; }
        public string? AccountNumber { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? AttributeID { get; set; }
        public string? AttributeName { get; set; }
        public string? ValueType { get; set; }
        public decimal? ChargeAmount { get; set; }
        public string? Remarks { get; set; }
        public int? IssueLevel { get; set; }
        public string? IssueMessage { get; set; }
    }
    public class ManualChargeBulkExcelDto
    {
        [JsonProperty("Customer_Name(optional)")]
        public string? Customer_Nameoptional { get; set; }
        public string? Account_Number { get; set; }
        public string? Charge_Amount { get; set; }
        public string? Remarks { get; set; }
    }
    public class ManualChargeApproveDto
    {
        public string? ChargeTransactionIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }

    public class ManualChargeDto
    {
        public Nullable<int> ProductID { get; set; }

        public string? ProductName { get; set; }
        public string? MemberName { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<decimal> AvailableBalance { get; set; }

        public Nullable<decimal> LedgerBalance { get; set; }
        public Nullable<decimal> AccruedCharge { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<int> IsDebited { get; set; }

        public Nullable<int> AttributeID { get; set; }
        public Nullable<int> ApprovalSetID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Remarks { get; set; }
        public string? Maker { get; set; }

        public Nullable<DateTime> MakeDate { get; set; }
    }

    public class AccrualChargeApprovalDto
    {
        public string? ChargeScheduleIDs { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }
    public class AccrualAccountChargeDto
    {
        public string? MemberName { get; set; }

        public string? AccountNumber { get; set; }
        public Nullable<int> AttributeID { get; set; }
        public string? AttributeName { get; set; }
        public Nullable<int> AccrualFromFile { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }

        public Nullable<Boolean> IsApplied { get; set; }
        public Nullable<decimal> CalculatedOnAmount { get; set; }
        public Nullable<decimal> ChargeRate { get; set; }
        public Nullable<decimal> ChargeAmount { get; set; }
    }

    public class AccrualAccountFilterDto
    {
        public string? ListType { get; set; }
        public Nullable<int> AttributeID { get; set; }
        public string? AccruedDateFrom { get; set; }
        public string? AccruedDateTo { get; set; }
        public string? AccountNumber { get; set; }
    }
    public class AccrualChargeFileApprovalDto
    {
        public Nullable<int> ChargeFileID { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalRemark { get; set; }
    }
    public class GenAccrualChargeFileUploadModel
    {
        public Nullable<int> ChargeFileID { get; set; }

        public string? FileName { get; set; }
        public Nullable<int> FileSizeInKB { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; }
        public Nullable<DateTime> RefDate { get; set; }
        public Nullable<int> ApprovalSetID { get; set; }
        public string? ApprovalStatus { get; set; }

        public string? ProcessingStatus { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }

        public List<GenAccrualChargeFileDetailModel> TransactionList = new List<GenAccrualChargeFileDetailModel>();
    }

    public class GenAccrualChargeFileDetailModel
    {
        public Nullable<int> ChargeFileDetailID { get; set; }

        public Nullable<int> ChargeFileID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> AgreementChargeID { get; set; }
        public Nullable<int> ChargeScheduleID { get; set; }
        public string? BOID { get; set; }

        public string? TransDescription { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> TotalCharge { get; set; }
        public string? BOShortName { get; set; }
        public string? TransRefNo { get; set; }

        public Nullable<decimal> TransValue { get; set; }
        public Nullable<decimal> ServiceTax { get; set; }
        public string? ISINShortName { get; set; }
        public Nullable<decimal> ChargeRate { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }

        public Nullable<decimal> TotalAmountForBO { get; set; }
        public Nullable<decimal> TotalAmountForDP { get; set; }
        public string? ISIN { get; set; }
        public Nullable<DateTime> RefDate { get; set; }
    }


    public class GenAccrualChargeFileUploadDto
    {
        public Nullable<int> ChargeFileDetailID { get; set; }

        public Nullable<int> ChargeFileID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> AgreementChargeID { get; set; }
        public Nullable<int> ChargeScheduleID { get; set; }
        public string? BOID { get; set; }

        public string? TransDescription { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> TotalCharge { get; set; }
        public string? BOShortName { get; set; }
        public string? TransRefNo { get; set; }

        public Nullable<decimal> TransValue { get; set; }
        public Nullable<decimal> ServiceTax { get; set; }
        public string? ISINShortName { get; set; }
        public string? ChargeRate { get; set; }
        public Nullable<decimal> TotalAmt { get; set; }

        public Nullable<decimal> TotalAmountForBO { get; set; }
        public Nullable<decimal> TotalAmountForDP { get; set; }
        public string? ISIN { get; set; }
        public Nullable<DateTime> RefDate { get; set; }
    }

    public class ChargesDto
    {
        public Nullable<int> AttributeID { get; set; }

        public string? AttributeName { get; set; }
        public string? ValueType { get; set; }
        public Nullable<int> IsPercentage { get; set; }
        public string? AttributeType { get; set; }
        public string? Products { get; set; }

        public string? Condition { get; set; }
        public string? DeleteStatus { get; set; }
        public Nullable<int> ChargeDetailID { get; set; }
        public string? ShortName { get; set; }
        public string? ChargeType { get; set; }

        public string? CalculatedOn { get; set; }
        public string? ApplyFrequency { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }

        public Nullable<int> ApprovalReqSetID { get; set; }
        public Nullable<bool> ManualEntryEnable { get; set; }
        public Nullable<bool> AdjustmentEnable { get; set; }
    }

    public class ProductAttributeDto
    {
	    public Nullable<int> AttributeID { get; set; }

        public Nullable<int> ProductAttributeID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? AttributeValue { get; set; }
        public string? ValueType { get; set; }
    }

    public class ChargeSetupDto
    {
        public ChargesDto? charge { get; set; } 
        public List<ProductAttributeDto> products { get; set; } 
    }

    public class ChargeApprovalDto
    {
        public Nullable<int> AttributeID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }

}
