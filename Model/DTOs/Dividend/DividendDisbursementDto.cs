using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Divident
{
    public class DividendDisbursementDto
    {
        public int? MFDividendDecID { get; set; }
        public int? InstrumentID { get; set; }
        public string? PeriodFrom { get; set; }
        public string? PeriodTo { get; set; }
        public string? RecordDate { get; set; }
        public string? DividendRate { get; set; }
        public decimal? TotalDividendPayable { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? Status { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public string? DeclarationDate { get; set; }
        public decimal? NonTaxAmtInPercetageInd { get; set; }
        public decimal? NonTaxAmtInPercetageOrg { get; set; }
        public decimal? NonTaxAmtInAmountInd { get; set; }
        public decimal? NonTaxAmtInAmountOrg { get; set; }
        public decimal? PaidupCapital { get; set; }
        public decimal? Nav { get; set; }
    }

    public class DivCalculationParamDto
    {
        public int FundID { get; set; }
        public double divPercentage { get; set; }
        public string RecordDate { get; set; }
        public decimal PaidupCapital { get; set; }
    }

    public class CashDividendDistribution
    {
        public int? ContractID { get; set; }
        public int? MFDividendDecID { get; set; }
        public decimal? HoldingQuantity { get; set; }
        public decimal? HoldingValue { get; set; }
        public decimal? TotalDivAmountCash { get; set; }
        public decimal? NonTaxableAmount { get; set; }
        public decimal? TaxableAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetDividendAmount { get; set; }
        public decimal? NetDividendAmountCash { get; set; }
        public int? BankAccountID { get; set; }
    }

    public class CIPDividendDistribution
    {
        public int? ContractID { get; set; }
        public int? MFDividendDecID { get; set; }
        public decimal? HoldingQuantity { get; set; }
        public decimal? HoldingValue { get; set; }
        public decimal? TotalDivAmountCash { get; set; }
        public decimal? NonTaxableAmount { get; set; }
        public decimal? TaxableAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? NetDividendAmount { get; set; }
        public decimal? NetDividendAmountCash { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? NoOfCIPUnit { get; set; }
        public decimal? UnitCapitalCIP { get; set; }
        public decimal? UnitPremiumCIP { get; set; }
        public decimal? CashNeedToDisburse { get; set; }
    }

   
}
