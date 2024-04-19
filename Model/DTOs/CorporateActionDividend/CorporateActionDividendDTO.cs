using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.Right;

namespace Model.DTOs.CorporateActionDividend
{
    public class CorporateActionDividendDTO
    {

    }

    public class CorpActRightDeclarationDTO
    {
        public int? CorpActDeclarationID { get; set; }
        public int? InstrumentID { get; set; }
        public int? DividendYear { get; set; }
        public String? IsInterim { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        
        public String? AGMDate { get; set; }
        public decimal? CashDivPercentage { get; set; }
        public decimal? StockDivPercentage { get; set; }
        public int? ApprovalSetID { get; set; }
        public String? ApprovalStatus { get; set; }
        //public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public String? YearEndDate { get; set; }

    }

    public class CorpActRightDeclarationListDTO
    {
        public int? CorpActDeclarationID { get; set; }
        public int? InstrumentID { get; set; }
        public String? InstrumentName { get; set; }
        public int? DividendYear { get; set; }
        public String? IsInterim { get; set; }
        public String? DeclareDate { get; set; }
        public String? RecordDate { get; set; }
        public String? YearEndDate { get; set; }
        public String? AGMDate { get; set; }
        public decimal? CashDivPercentage { get; set; }
        public decimal? StockDivPercentage { get; set; }
        public int? ApprovalSetID { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? Maker { get; set; }
        public String? Makedate { get; set; }
        public String? EnableDisable { get; set; }
        public string? Approver { get; set; }
        public string? ApproveDate { get; set; }

    }

    public class CorpActDeclarationApproveDTO
    {
        public int? CorpActDeclarationID { get; set; }
        public String? ApprovalRemark { get; set; }
        public String? ApprovalStatus { get; set; }


    }

    public class ILAMLCorpActionBonusFractionClaim
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<ILAMLCorpActionBonusFractionClaimListDTO>? ILAMLCorpActBonusFractionClaimList { get; set; }
    }

    public class ILAMLCorpActionBonusFractionUpdate
    {
        public decimal? BonusEntitlement { get; set; }
        public List<ILAMLCorpActionBonusFractionClaimListDTO>? ILAMLCorpActBonusFractionClaimList { get; set; }
    }

    public class ILAMLCorpActionBonusFractionClaimListDTO
    {
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? ApprovalSetID { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusFrcClaimID { get; set; }
        public int? BonusFractionFileDetailID { get; set; }
        //public decimal? NetAmount { get; set; }
        //public decimal? MarketPrice { get; set; }
        //public int? CorpActBonusClaimID { get; set; }
        

    }

    public class ILAMLCorpActionStockDivClaimListDTO
    {
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }

    }

    public class ILAMLCorpActionClaimListApprovalDTO
    {
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? CorpActBonusClaimID { get; set; }
        public int? ContractID { get; set; }

    }

    public class ILAMLCorpActionClaimMaster
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActBonusClaimID { get; set; }
    }

    public class ILAMLUpdateStockDivClaimApprove
    {
        public decimal? BonusEntitlement { get; set; }
        public List<ILAMLCorpActionStockDivClaimListDTO>? ILAMLCorpActClaimList { get; set; }
    }

    public class ILAMLCorpActionClaimApprove
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<ILAMLCorpActionApproveDTO>? ILAMLCorpActClaimList { get; set; }
    }

    public class ILAMLCorpActionApproveDTO
    {
        //public int? Serial { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? Qauntity { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public String? EnableDisable { get; set; }
        //public int? StockReceivableFileDetailsID { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        //public String? ApprovalStatus { get; set; }
        //public int? ApprovalSetID { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }

    }

    public class ILAMLCorpActionCashClaimListDTO
    {
        public int? SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? CashDividend { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? CashReceivableFileDetailsID { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActDivClaimID { get; set; }

        

    }

    public class ILAMLCorpActionCashClaim
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<ILAMLCorpActionCashClaimListDTO>? ILAMLCorpActCashClaimList { get; set; }
    }

    public class ILAMLCorpActionCashClaimListApprovalDTO
    {
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? CorpActDivClaimID { get; set; }

    }

    public class ILAMLUpdateCashDivClaimApprove
    {
        public decimal? NetAmount { get; set; }
        public List<ILAMLCorpActionCashClaimListDTO>? ILAMLCorpActCashClaimList { get; set; }
    }

    public class SLCorporateActionCashCollectionListDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? CashDividend { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActDivClaimID { get; set; }
        public string? GLBankName { get; set; }

    }
    public class SLCorporateActionCashCollectionInsertDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<SLCorporateActionCashCollectionListDTO>? SLCorpActCashClaimList { get; set; }
    }

    public class SLCorpActionDividendCollectionDTO
    {
        public decimal? NetAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public List<SLCorpActionDividendCollectionList>? SLCorpoActionDivdCollectionList { get; set; }

    }

    public class SLCorpActionDividendCollectionList
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? CashDividend { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActDivClaimID { get; set; }
        public int? CorpActDivCollID { get; set; }
        public string? GLBankName { get; set; }
    }

    public class SLCorporateActionCashCollectionApproveDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<SLCorpActionDividendCollectionList>? SLCorpActCashClaimApprove { get; set; }
    }

    public class SLCorpActionBonusFractionCollectionListDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusDividend { get; set; }
        public decimal? FractionQuantity { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActBonusFrcClaimID { get; set; }
        public string? GLBankName { get; set; }
    }

    public class SLCorporateActionFractionCollectionInsertDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<SLCorpActionBonusFractionCollectionListDTO>? SLCorpActFractionClaimList { get; set; }
    }

    public class SLCorpActionBonusFractionCollectionApprovalListDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusDividend { get; set; }
        public decimal? FractionQuantity { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? NetAmount { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActBonusFrcClaimID { get; set; }
        public int? CorpActBonusFrcCollID { get; set; }
        public string? GLBankName { get; set; }
    }

    public class SLCorpActionDividendBonusFractionCollectionDTO
    {
        public Nullable<decimal> MarketPrice { get; set; }
        public Nullable<decimal> NetAmount { get; set; }
        public List<SLCorpActionBonusFractionCollectionApprovalListDTO>? SLCorpoActionDivBonusFracCollectionList { get; set; }

    }

    public class SLCorpActionDividendBonusFractionApproveDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<SLCorpActionBonusFractionCollectionApprovalListDTO>? SLCorpoActionDivBonusFracCollectionList { get; set; }

    }

    public class ILAMLCorpActionBonusCollectionListDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        //public String? BOID { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public decimal? FreeQuantity { get; set; }
        public decimal? LockinQuantity { get; set; }
        public decimal? FreezQuantity { get; set; }
        public decimal? BlockQuantity { get; set; }

        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }
        public int? CortActBonusCollID { get; set; }


    }

    public class ILAMLCorpActionBonusCollectionApprovalListDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public decimal? FreeQuantity { get; set; }
        public decimal? LockinQuantity { get; set; }
        public decimal? FreezQuantity { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? AverageRate { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }
        public int? CortActBonusCollID { get; set; }


    }

    public class ILAMLCorpActionBonusCollectionDTO
    {
        public int SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BonusPercent { get; set; }
        public decimal? TotalHolding { get; set; }
        public decimal? BonusEntitlement { get; set; }
        public decimal? FreeQuantity { get; set; }
        public decimal? LockinQuantity { get; set; }
        public decimal? FreezQuantity { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? Rate { get; set; }
        public String? EnableDisable { get; set; }
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public int? CorpActClaimID { get; set; }
        public int? CorpActBonusClaimID { get; set; }
        public int? CortActBonusCollID { get; set; }
        //public int? CorpActDeclarationID { get; set; }


    }

    public class ILAMLCorpActionBonusCollectionInsertDTO 
    {
        public string? Status { get; set; }

        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<ILAMLCorpActionBonusCollectionDTO>? ILAMLCorpoActionDivBonusCollectionList { get; set; }
    }

    public class ILAMLCorpActionBonusCollectionUpdateDTO
    {
        public decimal? BonusEntitlement { get; set; }
        public List<ILAMLCorpActionBonusCollectionDTO>? ILAMLCorpoActionDivBonusCollectionUpList { get; set; }
    }

    public class ILAMLCorpActionDividendBonusCollectionApproveDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public int? CorpActDeclarationID { get; set; }
        public List<ILAMLCorpActionBonusCollectionDTO>? ILAMLCorpoActionDivBonusCollectionAList { get; set; }

    }

    //public class SLCorpActionCashCollectionInsertDTO
    //{
    //    public String? CollectionStatus { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public List<SLCorporateActionCashCollectionListDTO>? SLCorpActCashClaimList { get; set; }
    //}
}
