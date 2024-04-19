using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Instrument
{
    public class ApprovalInstrumentDto
    {
        public int InstrumentID { get; set; }
        public string? InstrumentType { get; set; }
        public bool IsApproved { get; set; }
        public string? FeedbackRemark { get; set; }
    }
    public class CMBondInstrumentDto
    {
        public string? ApprovalStatus { get; set; }
        public int? TotalRowCount { get; set; }
        public Nullable<int> InstrumentID { get; set; }

        public string? InstrumentName { get; set; }
        public int? SectorID { get; set; }
        public string? Sector { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public Nullable<decimal> AuthorizedCapital { get; set; }
        public Nullable<decimal> PaidUpCapital { get; set; }

        public Nullable<decimal> ReserveCapital { get; set; }
        public Nullable<decimal> NoOfOutstandingShares { get; set; }
        public Nullable<decimal> NetAssetValue { get; set; }
        public Nullable<int> InstrumentTypeID { get; set; }
        public string? InstrumentTypeName { get; set; }
        public string? ISIN { get; set; }

        public Nullable<decimal> FaceValue { get; set; }
        public Nullable<int> MarketLotSize { get; set; }
        public Nullable<decimal> EPS { get; set; }
        public Nullable<decimal> PERatio { get; set; }
        public Nullable<DateTime> LastRecordDate { get; set; }
        public string? LastRecordDateInString { get; set; }


        public Nullable<int> OrganizationID { get; set; }
        public string? OrganizationName { get; set; }
        public string? ListingStatus { get; set; }
        public string? InstrumentStatus { get; set; }
        public Nullable<int> DepositoryID { get; set; }
        public string? DepositoryCompanyName { get; set; }
        public string? DepositoryShortName { get; set; }

        public List<InstrumentExchangeDto> ExchangeList { get; set; }

        public Nullable<int> BondInstDetailID { get; set; }

        public string? IssuerName { get; set; }
        public Nullable<decimal> CouponRate { get; set; }
        public Nullable<decimal> MarketYield { get; set; }
        public string? TrusteeName { get; set; }

        public Nullable<decimal> BondRating { get; set; }
        public Nullable<int> CouponFrequency { get; set; }
        public string? MaturityDateInString { get; set; }
        public Nullable<DateTime> MaturityDate { get; set; }
    }


    public class CMInstrumentDTO
        {
            public string?  ApprovalStatus { get; set; }
            public int? TotalRowCount { get; set; }
            public Nullable<int> InstrumentID { get; set; }

            public string? InstrumentName { get; set; }
            public int? SectorID { get; set; }
            public string? Sector { get; set; }
            public Nullable<int> CategoryID { get; set; }
            public string? CategoryName { get; set; }
            public Nullable<decimal> AuthorizedCapital { get; set; }
            public Nullable<decimal> PaidUpCapital { get; set; }

            public Nullable<decimal> ReserveCapital { get; set; }
            public Nullable<decimal> NoOfOutstandingShares { get; set; }
            public Nullable<decimal> NetAssetValue { get; set; }
            public Nullable<int> InstrumentTypeID { get; set; }
            public string? InstrumentTypeName { get; set; }
            public string? ISIN { get; set; }

            public Nullable<decimal> FaceValue { get; set; }
            public Nullable<int> MarketLotSize { get; set; }
            public Nullable<decimal> EPS { get; set; }
            public Nullable<decimal> PERatio { get; set; }
            public Nullable<DateTime> LastRecordDate { get; set; }
            public string? LastRecordDateInString { get; set; }


            public Nullable<int> OrganizationID { get; set; }
            public string? OrganizationName { get; set; }
            public string? ListingStatus { get; set; }
            public string? InstrumentStatus { get; set; }
            public Nullable<int> DepositoryID { get; set; }
            public string? DepositoryCompanyName { get; set; }
            public string? DepositoryShortName { get; set; }
            //DETAIL
            public Nullable<int> CMMFInstrumentDetailID { get; set; }

            public string? Tenor { get; set; }
            public Nullable<DateTime> IssueDate { get; set; }
            public string? IssueDateInString { get; set; }
            public string? FundManager { get; set; }
            public string? Custody { get; set; }

            public string? Trustee { get; set; }
            public string? EPU { get; set; }
            public Nullable<DateTime> EPUDate { get; set; }
            public string? EPUDateInString { get; set; }

            public Nullable<Decimal> Yield { get; set; }
            public Nullable<Decimal> CouponRate { get; set; }
            public string? CouponFrequency { get; set; }
            public string? MaturityDateInString { get; set; }
            public Nullable<Decimal> IssuePrice { get; set; }

            public List<InstrumentExchangeDto> ExchangeList { get; set; }
        }
        public class CMMFInstrumentDetailDto
        {
            public Nullable<int> CMMFInstrumentDetailID { get; set; }

            public Nullable<int> InstrumentID { get; set; }
            public string Tenor { get; set; }
            public Nullable<DateTime> IssueDate { get; set; }
            public string? IssueDateInString { get; set; }
            public string FundManager { get; set; }
            public string Custody { get; set; }

            public string Trustee { get; set; }
            public string EPU { get; set; }
            public Nullable<DateTime> EPUDate { get; set; }
            public string? EPUDateInString { get; set; }
        }
        public class InstrumentExchangeDto
        {
            public int ExcInstrID { get; set; }
            public string? ScripName { get; set; }
            public string? ScripCode { get; set; }
            public Nullable<int> MarketID { get; set; }
            public string? MarketName { get; set; }
            public Nullable<int> InstrumentID { get; set; }
            public Nullable<int> SettlementCycle { get; set; }
            public Nullable<int> TradingPlatformID { get; set; }
            public string? TradingPlatformName { get; set; }
            public Nullable<int> ExchangeID { get; set; }
            public Nullable<decimal> MarketPrice { get; set; }
            public string? ExchangeName { get; set; }
        }
}
