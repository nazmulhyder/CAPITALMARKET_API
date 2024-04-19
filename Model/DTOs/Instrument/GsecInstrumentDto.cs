using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Instrument
{
    public class GsecInstrumentDto
    {
        public string? ApprovalStatus { get; set; }
        public Nullable<int> InstrumentID { get; set; }

        public string? InstrumentName { get; set; }
        public int? SectorID { get; set; }
        public string? Sector { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string? CategoryName { get; set; }
        //public Nullable<decimal> AuthorizedCapital { get; set; }
        //public Nullable<decimal> PaidUpCapital { get; set; }

        //public Nullable<decimal> ReserveCapital { get; set; }
        //public Nullable<decimal> NoOfOutstandingShares { get; set; }
        //public Nullable<decimal> NetAssetValue { get; set; }
        public Nullable<int> InstrumentTypeID { get; set; }
        public string? InstrumentTypeName { get; set; }
        public string? ISIN { get; set; }

        public Nullable<decimal> FaceValue { get; set; }
        public Nullable<int> MarketLotSize { get; set; }
        //public Nullable<decimal> EPS { get; set; }
        //public Nullable<decimal> PERatio { get; set; }
        //public Nullable<DateTime> LastRecordDate { get; set; }
        //public string? LastRecordDateInString { get; set; }

        //public Nullable<int> OrganizationID { get; set; }
        public string? ListingStatus { get; set; }
        public string? InstrumentStatus { get; set; }
        public Nullable<int> DepositoryID { get; set; }
        public string? DepositoryCompanyName { get; set; }
        public string? DepositoryShortName { get; set; }

        public Nullable<int> GSecInstDetailID { get; set; }

        //public Nullable<decimal> TotalOutstanding { get; set; }
        public string? IssueDate { get; set; }

        //public Nullable<decimal> IssuePrice { get; set; }
        public string? MaturityDate { get; set; }
        //public string? MaturityDateInString { get; set; }
        public string? CouponFrequency { get; set; }

        public Nullable<decimal> CouponRate { get; set; }
        //public Nullable<decimal> Yield { get; set; }
        //public Nullable<decimal> EPU { get; set; }
        //public string? InformationDate { get; set; }
        //public string? InformationDateInString { get; set; }
 
        //public string? LastInterestPayoutDateInString { get; set; }
        public Nullable<decimal> AccruedInterest { get; set; }
        public string? LastCouponPaymentDate { get; set; }
        public string? NextCouponDate { get; set; }
        public List<GsecInstrumentExchangeDto> ExchangeList { get; set; }

    }
    public class CMGSecInstDetailDto
    {
        public Nullable<int> GSecInstDetailID { get; set; }
        //public Nullable<decimal> TotalOutstanding { get; set; }
        public string? IssueDate { get; set; }
        //public string? IssueDateInString { get; set; }
        //public Nullable<decimal> IssuePrice { get; set; }
        public string? MaturityDate { get; set; }
        //public string? MaturityDateInString { get; set; }
        public string? CouponFrequency { get; set; }
        public Nullable<decimal> CouponRate { get; set; }
        //public Nullable<decimal> Yield { get; set; }
        //public Nullable<decimal> EPU { get; set; }
        //public Nullable<DateTime> InformationDate { get; set; }
        //public string? InformationDateInString { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public string? LastInterestPayoutDate { get; set; }
        public Nullable<decimal> AccruedInterest { get; set; }
    }
    public class GsecInstrumentExchangeDto
    {
        public Nullable<int> ExcInstrID { get; set; }
        public string? ScripName { get; set; }
        public string? ScripCode { get; set; }
        public Nullable<int> MarketID { get; set; }
        public string? MarketName { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public Nullable<int> SettlementCycle { get; set; }
        public Nullable<int> TradingPlatformID { get; set; }
        public string? TradingPlatformName { get; set; }
        public Nullable<decimal> MarketPrice { get; set; }
        public Nullable<int> ExchangeID { get; set; }
        public string? ExchangeName { get; set; }
    }
}
