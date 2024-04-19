using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Allotment
{
   public class AllotmentEntryDto
   {
        public int OMIBuyID { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public string? Operation { get; set; }
        public int? NoOfUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? TransactionID { get; set; }
        public decimal? Yield { get; set; }
        public decimal? AccruedInterest { get; set; }
        public string? AuctionNo { get; set; }
        public decimal? SettlementAmount { get; set; }
        public string? SettlementDate { get; set; }
        public decimal? CleanPrice { get; set; }
    }

    public class AMLAllotmentEntryDto : AllotmentEntryDto
    {
        public int? FundID { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? FaceValue { get; set; }
        public int? NoOfDays { get; set; }
        public string? IssueDate { get; set; }
        public string? MaturityDate { get; set; }
        public bool? IsTBond { get; set; }

    }

    public class AllotmentGSecList
    {
        public Nullable<int> InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public Nullable<decimal> FaceValue { get; set; }
        public Nullable<int> MarketLotSize { get; set; }
        public Nullable<decimal> CouponRate { get; set; }
        public Nullable<decimal> AccruedInterest { get; set; }
        public string? NextCouponDate { get; set; }
        public decimal? CouponAmount { get; set; }
        public string? AuctionNo { get; set; }

         
    }

    public class GSecAllotmentListDto : AllotmentEntryDto
    {
        public string? MakeDate { get; set; }
        public string? Maker { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public string? InstrumentStatus { get; set; }
        public string? InstrumentName { get; set; }
        public string? InsrumentType { get; set; }
        public decimal? FaceValue { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankName { get; set; }
        public string? IssueDate { get; set; }
        public string? MaturityDate { get; set; }
    }

    public class GetGSecAllotmentDto : GSecAllotmentListDto
    {
        public int? ProductID { get; set; }
        public Nullable<decimal> FaceValue { get; set; }
        public Nullable<int> MarketLotSize { get; set; }
        public Nullable<decimal> CouponRate { get; set; }
        public Nullable<decimal> AccruedInterest { get; set; }
        public string? NextCouponDate { get; set; }
        public int? CouponFrequency { get; set; }
        public int? BankAccountID { get; set; }
        public int? NoOfDays { get; set; }
        public string? IssueDate { get; set; }
        public string? MaturityDate { get; set; }

    }

    public class CalculateCleanPrice
    {
        public DateTime? SettlmentDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? CouponRate { get; set; }
        public decimal? Yield { get; set; }
    }

    public class FilterGSecAllotment
    {
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? ListType { get; set; }
        public int? FundID { get; set; }
        public string? InstrumentType { get; set; }
    }

    public class SettlementCalculate
    {
        public string? SettlementDate { get; set; }
        public int? InstrumentID { get; set; }
        public decimal? Yield { get; set; }
        public int? NoOfUnit { get; set; }

    }
}
