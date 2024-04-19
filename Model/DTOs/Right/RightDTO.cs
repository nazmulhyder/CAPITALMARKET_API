using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.IPO;

namespace Model.DTOs.Right
{
    public class RightDTO
    {
        public int? RightInstSettingID { get; set; }
        public int? InstrumentID { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        public decimal? FaceValue { get; set; }
        public decimal? Premium { get; set; }
        public String? Approver { get; set; }
        public String? ApproveDate { get; set; }
        public int? Rights { get; set; }
        public int? Holdings { get; set; }
        public String? EntitlementRatio { get; set; }
        public String? SubscriptionOpenDate { get; set; }
        public String? SubscriptionClosedDate { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public int? ApprovalSetID { get; set; }
        public String? ApprovalStatus { get; set; }
        //public string? ServiceCharge { get; set; }
        public Nullable<decimal> ServiceCharge { get; set; }
        public String? SrvChargeIsPercentage { get; set; }
        public String? InstrumentName { get; set; }

        //public List<CorpActDetailsDTO>? CorpActionRightList { get; set; }
    }
    public class CorpActDetailsDTO
    {

        public int? RightInstSettingID { get; set; }
        public int? InstrumentID { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        public decimal? FaceValue { get; set; }
        public decimal? Premium { get; set; }
        public String? EntitlementRatio { get; set; }
        public String? EntitlementPercentage { get; set; }
        public String? RightsStatus { get; set; }
        public int? Rights { get; set; }
        public int? Holdings { get; set; }
        public String? SubscriptionOpenDate { get; set; }
        public String? SubscriptionClosedDate { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public int? ApprovalSetID { get; set; }
        public String? ApprovalStatus { get; set; }
        public Nullable<decimal> ServiceCharge { get; set; }
        public String? SrvChargeIsPercentage { get; set; }
    }

    public class RightApproveDTO
    {
        public int? RightInstSettingID { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? UserName { get; set; }


    }

    public class InstrumentApplicationRights
    {
        public int? RightInstSettingID { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        public decimal? Rate { get; set; }
        public String? EntitlementRatio { get; set; }
        public String? ServiceCharge { get; set; }
        public String? SubscriptionOpenDate { get; set; }
        public String? SubscriptionClosedDate { get; set; }

    }

    public class InvestorInformationRights
    {

        public String? AccountNumber { get; set; }
        //public int? RightInstSettingID { get; set; }
        public int? ContractID { get; set; }
        public String? MemberName { get; set; }
        public String? BOID { get; set; }
        public int? ProductID { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? ShareHolding { get; set; }
        public decimal? RightsEntitlement { get; set; }

    }
    public class InvestorInformationRightsDetails
    {
        public int? SerialNo { get; set; }
        public string? SecurityCode { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }

        public decimal? AppliedAmount { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? RightsStatus { get; set; }


    }
    public class RightApplicatonDTO
    {
        public int? RightsApplicationID { get; set; }
        public int? RightInstSettingID { get; set; }
        public int? ProductID { get; set; }
        public int? ContractID { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? Rate { get; set; }
        public String? ServiceCharge { get; set; }
        public int? MFBankAccountID { get; set; }
        public String? Maker { get; set; }


    }

    public class RightApplicatonListDTO {
        public int? RightsApplicationID { get; set; }
        public int? RightInstSettingID { get; set; }
        public int? MFBankAccountID { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        public decimal? Rate { get; set; }
        public String? EntitlementRatio { get; set; }
        public String? ServiceCharge { get; set; }
        public String? SubscriptionOpenDate { get; set; }
        public String? SubscriptionClosedDate { get; set; }
        public String? BOID { get; set; }
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? ShareHolding { get; set; }
        public decimal? RightsEntitlement { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? Maker { get; set; }
        public String? InstrumentName { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? Makedate { get; set; }
        public decimal? GLBalance { get; set; }
    }

    public class RightsApplicationforApprovalDTO
    {
        public int? RightsApplicationId { get; set; }
        public int? RightInstSettingID { get; set; }

        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public decimal? AppliedQuantiy { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? EnableDisable { get; set; }
        public String? ApprovalStatus { get; set; }
        
        public decimal? TotalAmount { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public decimal? GLBalance { get; set; }
        public String? Remarks { get; set; }

    }

    public class RightsApplication
    {

        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        public List<RightsApplicationforApprovalDTO>? corporateApprovedList { get; set; }
    }

    public class RightsApprovalDTO
    {
        public int? RightsApplicationID { get; set; }
        public int? RightInstSettingID { get; set; }
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public decimal? AppliedQuantiy { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? EnableDisable { get; set; }

    }

    public class RightsReversalDTO
    {
        public String? MemberName { get; set; }
        public String? AccountNumber { get; set; }
        public int? RightsApplicationID { get; set; }
        public int? RightsReverseID { get; set; }
        public decimal? AppliedQuantity { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public String? InstrumentName { get; set; }
        public String? Remarks { get; set; }
    }

    public class RightsReversalMaster
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? RightsReverseID { get; set; }
    }

    public class RightsBulkApproved
    {
        public int? ProductID { get; set; }
        public int? RightInstSettingID { get; set; }
        public String? Maker { get; set; }
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public String? BOCode { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? TotalHoldings { get; set; }
        public decimal? RightsEntitlements { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public String? ProductName { get; set; }
    }

    public class RightsBulkMaster
    {
        public int? ProductID { get; set; }
        public int? RightsInstrumentID { get; set; }
        public List<RightsBulkInsert>? RightsBulkInsertList { get; set; }
    }

    public class RightsBulkInsert
    {
        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }
        public String? BOCode { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? RightInstSettingID { get; set; }
        public decimal? TotalHoldings { get; set; }
        public decimal? RightsEntitlements { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
    }

    public class RightsCollection
    {
        public int? RightsApplicationID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public String? ApplicationDate { get; set; }

    }

    public class AMLRightsCollectionMaster
    {
        public String? UserName { get; set; }
        public List<AMLRightsCollectionList>? corpActRightInstrumentCollectionList { get; set; }
    }

    public class AMLRightsCollectionList
    {
        public int? RightsApplicationID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? BOID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public String? ApplicationDate { get; set; }

    }

    public class AMLCorpActRightCollectionApprovalList
    {

        public int? RightsApplicationID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? BOID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? AppliedAmount { get; set; }
        public String? ApplicationDate { get; set; }
    }


    public class AMLCorpActRightCollectionApproved
    {

        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
        public int? RightInstSettingID { get; set; }
        public List<AMLCorpActRightCollectionApprovalList>? AMLCorpActRightCollectionApprovedList { get; set; }
    }

    //public class CorpActRightDeclarationDTO
    //{
    //    public int? CorpActDeclarationID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public int? DividendYear { get; set; }
    //    public String? IsInterim { get; set; }
    //    public String? DeclareDate { get; set; }
    //    public String? RecordDate { get; set; }
    //    public String? AGMDate { get; set; }
    //    public decimal? CashDivPercentage { get; set; }
    //    public decimal? StockDivPercentage { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public String? ApprovalStatus { get; set; }
    //    //public String? Maker { get; set; }
    //    public String? Makedate { get; set; }

    //}

    //public class CorpActRightDeclarationListDTO
    //{
    //    public int? CorpActDeclarationID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public String? InstrumentName { get; set; }
    //    public int? DividendYear { get; set; }
    //    public String? IsInterim { get; set; }
    //    public String? DeclareDate { get; set; }
    //    public String? RecordDate { get; set; }
    //    public String? AGMDate { get; set; }
    //    public decimal? CashDivPercentage { get; set; }
    //    public decimal? StockDivPercentage { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public String? ApprovalStatus { get; set; }
    //    //public String? Maker { get; set; }
    //    public String? Makedate { get; set; }

    //}

    //public class CorpActDeclarationApproveDTO
    //{
    //    public int? CorpActDeclarationID { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public String? ApprovalStatus { get; set; }


    //}

    public class ILAMLCorpActionClaim
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<ILAMLCorpActionClaimDTO>? ILAMLCorpActClaimList { get; set; }
    }

    //public class ILAMLCorpActionClaimApprove
    //{
    //    public String? Status { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public int? CorpActDeclarationID { get; set; }
    //    public List<ILAMLCorpActionApproveDTO>? ILAMLCorpActClaimAList { get; set; }
    //}

    public class ILAMLCorpActionClaimDTO
    {
        public int? Serial { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? Qauntity { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public String? EnableDisable { get; set; }
        public int? StockReceivableFileDetailsID { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        //public String? ApprovalStatus { get; set; }
        //public int? ApprovalSetID { get; set; }
        //public int? CorpActBonusClaimID { get; set; }
        public int? CorpActClaimID { get; set; }

    }

    //public class ILAMLCorpActionApproveDTO
    //{
    //    //public int? Serial { get; set; }
    //    public String? AccountNumber { get; set; }
    //    public String? AccountName { get; set; }
    //    public int? ProductID { get; set; }
    //    public String? ProductName { get; set; }
    //    public decimal? BonusPercent { get; set; }
    //    public decimal? Qauntity { get; set; }
    //    public decimal? BonusEntitlement { get; set; }
    //    public String? EnableDisable { get; set; }
    //    //public int? StockReceivableFileDetailsID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public int? ContractID { get; set; }
    //    //public String? ApprovalStatus { get; set; }
    //    //public int? ApprovalSetID { get; set; }
    //    public int? CorpActClaimID { get; set; }
    //    public int? CorpActBonusClaimID { get; set; }
        
    //}

    public class ILAMLCorpActionClaimListDTO 
    {
        //public int? Serial { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public String? EnableDisable { get; set; }
        public int? StockReceivableFileDetailsID { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }
       

    }

    //public class ILAMLCorpActionClaimListApprovalDTO
    //{
    //    public String? AccountNumber { get; set; }
    //    public String? AccountName { get; set; }
    //    public int? ProductID { get; set; }
    //    public String? ProductName { get; set; }
    //    public decimal? BonusPercent { get; set; }
    //    public decimal? Qauntity { get; set; }
    //    public decimal? BonusEntitlement { get; set; }
    //    public String? EnableDisable { get; set; }
    //    //public int? StockReceivableFileDetailsID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public int? ContractID { get; set; }
    //    //public String? ApprovalStatus { get; set; }
    //    //public int? ApprovalSetID { get; set; }
    //    public int? CorpActClaimID { get; set; }
    //    public int? CorpActBonusClaimID { get; set; }


    //}

    public class ILAMLCorpActionClaimMaster
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActBonusClaimID { get; set; }
    }

    //public class ILAMLUpdateStockDivClaimApprove
    //{
    //    public decimal? BonusEntitlement { get; set; }
    //    public List<ILAMLCorpActionApproveDTO>? ILAMLCorpActClaimList { get; set; }
    //}

    //public class ILAMLCorpActionCashClaimListDTO
    //{
    //    public int? SL { get; set; }
    //    public String? AccountNumber { get; set; }
    //    public String? AccountName { get; set; }
    //    public int? ProductID { get; set; }
    //    public String? ProductName { get; set; }
    //    public decimal? TotalHolding { get; set; }
    //    public decimal? CashDividend { get; set; }
    //    public decimal? GrossAmount { get; set; }
    //    public decimal? TaxPercentage { get; set; }
    //    public decimal? TaxAmount { get; set; }
    //    public decimal? NetAmount { get; set; }
    //    public String? EnableDisable { get; set; }
    //    public int? CashReceivableFileDetailsID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public int? ContractID { get; set; }
    //    //public String? ApprovalStatus { get; set; }
    //    //public int? ApprovalSetID { get; set; }
    //    public int? CorpActClaimID { get; set; }
    //    public int? CorpActDivClaimID { get; set; }

    //}

    public class ILAMLCorpActionCashClaimApproveListDTO
    {
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? CashDividend { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? CashReceivableFileDetailsID { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        //public String? ApprovalStatus { get; set; }
        //public int? ApprovalSetID { get; set; }
        public int? CorpActDivClaimID { get; set; }
        public int? CorpActClaimID { get; set; }

    }

    //public class ILAMLCorpActionCashClaim
    //{
    //    public String? Status { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public int? CorpActDeclarationID { get; set; }
    //    public List<ILAMLCorpActionCashClaimListDTO>? ILAMLCorpActCashClaimList { get; set; }
    //}

    //public class ILAMLCorpActionCashClaimListApprovalDTO
    //{
    //    public String? AccountNumber { get; set; }
    //    public String? AccountName { get; set; }
    //    public int? ProductID { get; set; }
    //    public String? ProductName { get; set; }
    //    public decimal? TaxAmount { get; set; }
    //    public decimal? GrossAmount { get; set; }
    //    public decimal? NetAmount { get; set; }
    //    public String? ApprovalStatus { get; set; }
    //    public int? CorpActDivClaimID { get; set; }

    //}

    //public class ILAMLUpdateCashDivClaimApprove
    //{
    //    public decimal? NetAmount { get; set; }
    //    public List<ILAMLCorpActionCashClaimListDTO>? ILAMLCorpActCashClaimList { get; set; }
    //}

}
