using Model.DTOs.Instrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.IPO
{
    public class IPODTO
    {
        public int? IPOInstrumentID { get; set; }
        public int? InstrumentID { get; set; }
        public string? IPOType { get; set; }
        public decimal? ServiceCharges { get; set; }
        //public string? ServiceChargeMode { get; set; }
        public string? IPOStatus { get; set; }
        public string? SecurityCode { get; set; }
        public string? Maker { get; set; }
        public string? Makedate { get; set; }
        public string? Approver { get; set; }
        public string? ApproveDate { get; set; }
        public string? InstrumentName { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? TotalRowCount { get; set; }
        public List<IPODetailsDTO>? IPODetailsList { get; set; }
    }

    public class IPODetailsDTO
    {

        public Nullable<int> IPOInstrumentDetailID { get; set; }
        public Nullable<int> IPOInstrumentID { get; set; }
        public string? ClientAccountType { get; set; }
        public decimal? ServiceCharges { get; set; }
        public String? ServiceChargeMode { get; set; }
        public string? SubscriptionOpeningDate { get; set; }
        public string? SubscriptionClosingDate { get; set; }
        public string? CutOffDate { get; set; }
        public Nullable<decimal> RequiredMarketValueCutOffDate { get; set; }
        public Nullable<decimal> MinApplicationAmount { get; set; }
        public Nullable<decimal> MaxApplicationAmount { get; set; }
        public Nullable<decimal> SubscriptionMultipleOf { get; set; }
    }
    public class IPOInvestorInfo
    {
        public int? ProductID { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? MemberName { get; set; }
        public string? InvestorCategory { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? AppliedAmount { get; set; }
        public int? IpoInstrumentID { get; set; }
        public decimal? GLBalance { get; set; }

    }
    public class IPOInvestorIPOInfo
    {
        public int? SerialNo { get; set; }
        public string? SecurityCode { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? IPOStatus { get; set; }
        public string? IPOType { get; set; }
      

    }

    public class InstrumentDTO
    {
        public int? SerialNo { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }

    }

    public class UnlockInstrumentDTO
    {
        public int? SerialNo { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }

    }

    public class IPOInstrumentInfo
    {

        public int? IPOInstrumentID { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public string? CutOffDate { get; set; }
        public decimal? RequiredMarketValueCutOffDate { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? PercentServiceCharge { get; set; }
        public decimal? ServiceChargeMode { get; set; }
        public decimal? MinApplicationAmount { get; set; }
        public decimal? MaxApplicationAmount { get; set; }
        public decimal? SubsciptionAmountMultipleOf { get; set; }
        public decimal? TotalApplicationAmount { get; set; }
        public decimal? LedgerBalance { get; set; }
        public decimal? CollectionAmount { get; set; }

    }
    public class IPOApplication
    {

        public int? ProductID { get; set; }
        public int? ContractID { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public int? MFBankAccountID { get; set; }
        public int? IPOInstrumentID { get; set; }
        public int? IPOApplicationID { get; set; }
        public decimal? NoOfshare { get; set; }
        public decimal? AppliedAmount { get; set; }
        public int? IPOsourceID { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? TotalApplicationAmount { get; set; }
        public decimal? Rate { get; set; }
        public String? Maker { get; set; }
        public String? Remarks { get; set; }


    }

    public class IPOInvestorInstrumentInfo
    {
        public int? IPOInstrumentID { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public string? CutOffDate { get; set; }
        public decimal? RequiredMarketValueCutOffDate { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? MinApplicationAmount { get; set; }
        public decimal? MaxApplicationAmount { get; set; }
        public decimal? SubsciptionAmountMultipleOf { get; set; }

    }

    public class IPOOrderApproved
    {

        public int? IPOInstrumentdetailId { get; set; }
        public int? IPOApplicationId { get; set; }
        public int? IPOInstrumentID { get; set; }

        public int? ContractID { get; set; }

        public String? AccountNumber { get; set; }

        public String? MemberName { get; set; }

        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }

        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? ProductID { get; set; }
        public String? ClientAccountType { get; set; }
        public String? ProductName { get; set; }
        public String? ApprovalStatus { get; set; }
        public decimal? DeductibleAmount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? NoOfshare { get; set; }
        public String? EnableDisable { get; set; }
        public String? Maker { get; set; }
        public string? Makedate { get; set; }
        public string? Remarks { get; set; }
        public decimal? GLBalance { get; set; }

    }
    public class IPOInstrumentApproval
    {

        public int? IPOInstrumentID { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? IsApproved { get; set; }
        public String? Status { get; set; }

    }
    public class IPOApproval
    {

        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        public List<IPOOrderApproved>? IPOOrderApprovedList { get; set; }
    }

    public class IPOApplicationListID
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? MemberName { get; set; }
        public string? InvestorCategory { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? AppliedAmount { get; set; }
        public int? IPOInstrumentID { get; set; }
        public int? IPOApplicationId { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public string? CutOffDate { get; set; }
        public decimal? RequiredMarketValueCutOffDate { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? ServiceChargeMode { get; set; }
        public decimal? PercentServiceCharge { get; set; }
        public decimal? TotalApplicationAmount  { get; set; } //DeductibleAmount
        public decimal? RemainingBalance { get; set; }
        public decimal? MinApplicationAmount { get; set; }
        public decimal? MaxApplicationAmount { get; set; }
        public decimal? SubsciptionAmountMultipleOf { get; set; }

        public decimal? Rate { get; set; }
        public decimal? NoOfshare { get; set; }
        public decimal? Quantity { get; set; }
        public int? IPOsourceID { get; set; }
        public decimal? IPOChargeAmount { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public String? InstrumentName { get; set; }
        public String? ApprovalStatus { get; set; }

        public String? FundName { get; set; }

        public int? MFBankAccountID { get; set; }
        public decimal? LedgerBalance { get; set; }
        public decimal? CollectionAmount { get; set; }
        public String? Remarks { get; set; }
        public decimal? GLBalance { get; set; }

    }
    public class IPOResultDTO
    {
        public string? TrecNo { get; set; }
        public string? DPID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? BOID { get; set; }

        public string? InvestorCategory { get; set; }
        public string? ScriptsName { get; set; }
        public decimal? NoofShare { get; set; }
        public string? Currency { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? AllotedShare { get; set; }
        public decimal? FinedAmount { get; set; }
        public decimal? RefundAmount { get; set; }

        public string? Remarks { get; set; }



    }

    public class IPOBulkApproved
    {

        public int? IPOInstrumentDetailID { get; set; }
        public int? IPOInstrumentID { get; set; }
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public String? BOCode { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? ProductID { get; set; }
        public String? ClientAccountType { get; set; }
        public String? ProductName { get; set; }
        public decimal? RequiredMarketValueCutOffDate { get; set; }
        public decimal? MarketValue { get; set; }


    }
    public class IPOBulkInsertMaster
    {
        public int? ProductID { get; set; }
        public int? IPOInstrumentID { get; set; }
        public decimal? AppliedAmount { get; set; }
        public List<IPOBulkInsert>? IPOBulkInsertList { get; set; }

    }
    public class IPOBulkInsert
    {
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public String? BOCode { get; set; }
        public String? InvestorCategory { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? IPOInstrumentID { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public String? ClientAccountType { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
    }

    public class IPOListResultforAllocationRefund
    {
        public int? IPOInstrumentID { get; set; }
        public int? IPOResultID { get; set; }
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? InvestorCategory { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? AllotedShare { get; set; }
        public decimal? FineAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? EnableDisable { get; set; }

    }

    public class IPOResultMaster
    {
        public String? UserName { get; set; }
        public int? IPOInstrumentID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<IPOResultList>? IPOResultInsertList { get; set; }
    }
    public class IPOResultList
    {
        public int? IPOInstrumentID { get; set; }
        public int? IPOResultID { get; set; }
        public int? ContractId { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public String? InvestorCategory { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? AllotedShare { get; set; }
        public decimal? FinedAmount { get; set; }
        public decimal? RefundAmount { get; set; }
    }

    public class IPOReversalDTO
    {
        
        public String? MemberName { get; set; }
        public String? AccountNumber { get; set; }
        public int? IpoReverseID { get; set; }
        public decimal? IPOChargeAmount { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? DeductibleAmount { get; set; }
        public String? ClientAccountType { get; set; }
        public string? SecurityCode { get; set; }
        public String? Maker { get; set;}
        public String? Makedate { get; set; }
    }

    public class IPOReversalMaster
    {
        //public String? UserName { get; set; }
        public int? IpoReverseID { get; set; }
        //public int? IPOInstrumentID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        //public List<IPOReversalDTO>? IPOReversalApproveList { get; set; }
    }

    public class IPOBookBuildingDTO
    {
        //public int? IPOInstrumentID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public decimal? AppliedAmount { get; set; }
        public string? Maker { get; set; }
        public string? Makedate { get; set; }
        public string? Approver { get; set; }
    }

    public class IPOBookBuildingAppSubscriptionIPO
    {
        public int? IPOApplicationId { get; set; }
        public decimal? NoOfshare { get; set; }
        public decimal? Rate { get; set; }
    }

    public class IPOBookBuildingAllotmentDTO
    {
        public int? IPOInstrumentID { get; set; }
        public int? IPOApplicationId { get; set; }
        public decimal? AppliedAmount { get; set; }
        //public decimal? Rate { get; set; }
    }

    public class IPOBookBuildingAllotmentInsDTO
    {
        public int? SubscriptionID { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public int? IPOApplicationId { get; set; }
        public int? MFBankAccountID { get; set; }


    }

    public class IPOBookBuildingAllotmentList
    {
        public int? IPOInstrumentID { get; set; }
        public int? IPOApplicationId { get; set; }
        public int? SubscriptionID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? AppliedRate { get; set; }
        public decimal? RefundAmount { get; set; }
        public String? Status { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? EnableDisable { get; set; }

        
            

    }

    public class SubscriptionListByIdDTO
    {
        public int? IPOInstrumentdetailId { get; set; }
        public int? IPOApplicationId { get; set; }
        public int? IPOInstrumentID { get; set; }
        public int? SubscriptionID { get; set; }
        public string? BOID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? InvestorCategory { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalApplicationAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? Maker { get; set; }
        public string? InstrumentName { get; set; }
        public int? MFBankAccountID { get; set; }
        public String? SubscriptionStatus { get; set; }
        public decimal? GLBalance { get; set; }
    }
    public class BookBuildingRefundMaster
    {
     
        public int? IPOInstrumentID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<IPOBookBuildingAllotmentList>? objIPOBookBuildingAllotmentList { get; set; }

    }

    public class AMLCollection
    {
        public int? IPOInstrumentID { get; set; }
        public int? AllotementRefundID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AllotedQuantity { get; set; }
        public string? EnableDisable { get; set; }
        public string? IPOType { get; set; }

    }

    public class AMLCollectionMaster
    {

        public string? IPOType { get; set; }
        public String? Maker { get; set; }
        public List<AMLCollectionList>? ipoInstrumentCollectionList { get; set; }
    }

    public class AMLCollectionList
    {
        public int? IPOCollectionID { get; set; }
        public int? AllotementRefundID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AllotedQuantity { get; set; }
        

    }

    public class AMLCollectionApprovalList
    {

        public int? IPOCollectionID { get; set; }
        public int? AllotmentRefundID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AllotedQuantity { get; set; }
        public string? EnableDisable { get; set; }
        public string? IPOType { get; set; }
    }

    public class AMLCollectionApprovalListDTO
    {

        public int? RightsApplicationID { get; set; }
        //public int? AllotmentRefundID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public string? ApplicationDate { get; set; }
    }

    public class AMLCollectionApproved
    {

        public int? IPOInstrumentID { get; set; }
        public string? IPOType { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        public List<AMLCollectionApprovalList>? AMLCollectionApprovedList { get; set; }
    }

    public class AMLCollectionApprovedDTO
    {

        public int? rightInstSettingID { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        public List<AMLCollectionApprovalListDTO>? amlCorpActRightCollectionApprovedList { get; set; }
    }



    public class IPOFileGenerateIssuer
    {
        public int? Code1 { get; set; }
        public int? Code2 { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? BOID { get; set; }
        public String? AccountType { get; set; }
        public String? Currency { get; set; }
        public decimal? AppliedAmount { get; set; }
        public string? CompanyShortName { get; set;}
    }

    public class IPOInstrumentSetupList 
    {
        public int? IPOInsSettingID { get; set; }
        public int? InstrumentID { get; set; }
        public string? IPOType { get; set;}
        public string? SecurityCode { get; set; }
        public decimal? ServiceCharge { get; set; }
        public String? SrvChargeIsPercentage { get; set; }

    }

    public class IPOApplicationFileDetailDto
    {

        public Nullable<int> IPOApplicationFileDetailID { get; set; }

        public Nullable<int>IPOInsSettingID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNo { get; set; }
        public Nullable<decimal> ApplicationAmount { get; set; }
        public bool? Status { get; set; }
        public string? Maker { get; set; }
        public string? Makedate { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        
    }


    public class IPOApplicationEligibleList
    {

        public int? IPOApplicationFileDetailID { get; set; }
        public int? IPOInsSettingID { get; set; }
        public int? IPOInstrumentdetailId { get; set; }
        public int? ContractID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public decimal? IPOAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? LedgerBalance { get; set; }
        public String? ClientAccountType { get; set; }
        public decimal? MV { get; set; }
        public string? EnableDisable { get; set; }
        public string? Remarks { get; set; }
        
    }

    public class IPOEligibleBulkInsertMaster
    {
        public int? ProductID { get; set; }
        public int? IPOInstrumentID { get; set; }
       
        public List<IPOApplicationEligibleList>? IPOBulkInsertList { get; set; }

    }

    public class AMLBankAccountInfoDTO
    {
        public int? MFBankAccountID { get; set; }
        public string? BankAccountName { get; set; }
        public decimal? GLBalance { get; set; }
    }
    public class ListTransferAmountDTO
    {
        public String? IPOType { get; set; }
        public int? InstrumentID { get; set; }
        public String? InstrumentName { get; set; }
    }

    public class ListTransferDTO
    {
        public int? NoOfApplicant { get; set; }
        public string? AccountType { get; set; }
        public decimal? ApplicationAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
    }

    public class SLILFundTransferToIssuerDTO
    {

        public string? IPOType { get; set; }
        public int? InstrumentID { get; set; }
        public int? DepositBankAccountID { get; set; }
        public decimal? TotalApplicationAmount { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        //public List<ListTransferDTO>? SLILFundTransferApprovedList { get; set; }
    }

    //IPODTO End



}
