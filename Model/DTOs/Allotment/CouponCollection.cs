using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Allotment
{
    public class CouponCol_GsecInsHoldingDto
    {
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? IssueDate { get; set; }
        public string? MaturityDate { get; set; }
        public decimal? CouponRate { get; set; }
        public string? Holding { get; set; }
        public decimal? CouponAmount { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? AvgBuyingCleanPrice { get; set; }
        public decimal? AccruedInterest { get; set; }
        public decimal? AIT { get; set; }
        public decimal? CollectionAmount { get; set; }
        public int? BankAccountID { get; set; }
        public int? DeclarationID { get; set; }
        public string? InstrumentNumber { get; set; }

    }

    public class CouponCol_GSecDto
    {
        public Nullable<int> InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? CouponRate { get; set; }
        public string? CouponFrequency { get; set; }
        public string? LastCouponPaymentDate { get; set; }
        public string? NextCouponDate { get; set; }
    }

    public class CouponCol_declarationDto
    {
        public int? DeclarationID { get; set; }
        public int? InstrumentID { get; set; }
        public string? DeclarationDate { get; set; }
        public decimal? InterestRate { get; set; }
        public string? RecordDate { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public decimal? CouponRate { get; set; }
        public string? TenorStartDate { get; set; }
        public string? TenorEndDate { get; set; }
        public string? Status { get; set; } = "new";
        public string? Year { get; set; }

    }

    public class CouponCollectionDto
    {
        public int? InstColletionID { get; set; }
        public string? InstCollectionDate { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public string? TransactionDate { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? AIT { get; set; }
        public decimal? CollectionAmount { get; set; }
        public string? Remarks { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? TransactionID { get; set; }
        public int? DeclarationID { get; set; }
        public int? BankAccountID { get; set; }
        public int? FundID { get; set; }
        public string? InstrumentNumber { get; set; }

    }

    public class CouponCollectionEntryDto
    {
        public int? DeclarationID { get; set; }
        public int? BankAccountID { get; set; }
        public int? FundID { get; set; }
        public string? InstrumentNumber { get; set; }
        public List<CouponCollectionDto> CouponCollections { get; set; }
    }



    public class CouponCol_approvalListDto
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? InstrumentName { get; set; }
        public string? IssueDate { get; set; }
        public string?  MaturityDate { get; set; }
        public decimal? CouponCollection { get; set; }
        public decimal? AIT { get; set; }
        public decimal? CollectionAmount { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? InstCollectionID { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
    }

    public class GetCollectionCouponForReversal
    {
        public int? ProductID { get; set; }
        public string? AccountNo { get; set; }
        public int? instrumentID { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? FundID { get; set; }
    }
    public class CouponCol_reversalDto
    {
        public int? IntCollReversalID { get; set; }
        public string? ReversalReason { get; set; }
        public int? TransactionID { get; set; }
        public int? InstCollectionID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
    }

    public class CouponCol_getCollectedCoupon
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? TransactionDate { get; set; }
        public decimal? CouponAmount { get; set; }
        public int? TransactionID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public int? InstCollectionID { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public decimal? CollectionAmount { get; set; }
    }

    public class CouponCol_reversalListDto
    {
        public int? IntCollReversalID { get; set; }
        public string? ProductName { get; set; }
        public string? InstrumentName { get; set; }
        public string? IntCollectionDate { get; set; }
        public decimal? CouponAmount { get; set; }
        public string? ReversalReason { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? AccountNumber { get; set; }
        public int? FundID { get; set; }
        public string FundName { get; set;}
        public decimal? CollectionAmount { get; set; }
    }

    public class GsecOffMktInsSaleHoldingDto
    {
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? FaceValue { get; set; }
        public string? NoOfInstrument { get; set; } // this is holding
        public decimal? TotalValue { get; set; }
        public int? ContractID { get; set; }
        public decimal? CostValue { get; set; }
        public decimal? InterestAccured { get; set; }
        public decimal? SettlementValue { get; set; }
        public int? OMIBuyID { get; set; }
        public decimal? AvgBuyingCleanPrice { get; set; }
    }

    public class GSecOffMktInsSaleDto
    {
        public int? OMISaleID { get; set; }
        public int? InstrumentID { get; set; }
        public string? TransactionDate { get; set; }
        public string? Operation { get; set; }
        public int NoOfUnit { get; set; }
        //public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? LedgerID { get; set; }
        public int? TransactionID { get; set; }
        public int? ContractID { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? capitalGain { get; set; }
        public decimal? Yield { get; set; }
        public decimal? AccruedInterest { get; set; }
        public decimal? CleanPrice { get; set; }
        public string? SettlementDate { get; set; }
        public decimal? SettlementValue { get; set; }
    }

    public class ListGSecOffMktInsSaleDto
    {
        public int? OMISaleID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? TotalSaleValue { get; set; }
        public decimal? NoOfInsrument { get; set; }
        public decimal? SaleValue { get; set; }
        public string? Remarks { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
    }
}
