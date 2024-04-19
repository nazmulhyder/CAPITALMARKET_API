using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Allocation
{
    public class InstrumentRedemption
    {
        public int? InstRedemptionID { get; set; }
        public int? InstrumentID { get; set; }
        public string? RedemptionDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public List<InstrumentRedemptionDetail>? instrumentRedemptionDetails { get; set; }
    }

    public class InstrumentRedemptionDetail
    {
        public int? InstRedDetlID { get; set; }
        public int? ContractID { get; set; }
        public int? HoldingUnit { get; set; }
        public decimal? Amount { get; set; }
        public int? TransactionID { get; set; }
        public int? InstRedemptionID { get; set; }
        public int? BankAccountID { get; set; }
    }
    public class InstrumentRedemptionAML
    {
        public int? InstRedemptionID { get; set; }
        public int? InstrumentID { get; set; }
        public string? RedemptionDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public List<InstrumentRedemptionDetailAML>? instrumentRedemptionDetails { get; set; }
    }

    public class InstrumentRedemptionDetailAML
    {
        public int? InstRedDetlID { get; set; }
        public int? ContractID { get; set; }
        public int? HoldingUnit { get; set; }
        public decimal? Amount { get; set; }
        public int? TransactionID { get; set; }
        public int? InstRedemptionID { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? AIT { get; set; }
        public decimal? InterestAdjustment { get; set; }
        public decimal? AccruedInterest { get; set; }
        public int? OMIBuyID { get; set; }
    }


    public class InsRed_getHoldings
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? FaceValue { get; set; }
        public decimal? HoldingUnit { get; set; }
        public decimal? Amount { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? InstRedDetlID { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public int? BankAccountID { get; set; }
        public string? BankAccountName { get;set; }
        public List<MFBankList> bankLists { get; set; } = new List<MFBankList>();

    }

    public class InsRed_getHoldingsSummary
    {

        public decimal? TotalAccounts { get; set; }
        public decimal? TotalUnits { get; set; }
        public decimal? TotalValue { get; set; }
        public int? InstRedemptionID { get; set; }
        public int? InstrumentID { get; set; }


    }

    public class MFBankList
    {
        public int? FundID { get; set; }
        public int? MFBankAccountID { get; set; }
        public string? BankAccountName { get; set; }
    }

    public class InsRed_GetList
    {
        public int? InstRedemptionID { get; set; }
        public string? InstrumentName { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? TotalNoOfInstrument { get; set; }
        public decimal? TotalValue { get; set; }

    }

    public class GSecRedemption
    {
        public Nullable<int> InstrumentID { get; set; }
        public string? InstrumentName { get; set; }

    }
}
