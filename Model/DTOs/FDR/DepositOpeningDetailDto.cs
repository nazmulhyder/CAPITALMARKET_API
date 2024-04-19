using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FDR
{
    public class DepositOpeningDto
    {
        public int? DepositOpenDetlID { get; set; }
        public int? ContractID { get; set; }
        public string? OpeningDate { get; set; }
        public decimal? OpeningAmount { get; set; }
        public int? BankFIOrgID { get; set; }
        public string? DepositProductName { get; set; }
        public int? TransactionID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? DepositID { get; set; }
        public string? DepositACCNo { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? DepositTerm { get; set; }
        public string? MaturityDate { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? InterestAmount { get; set; }
        public string? Status { get; set; }
        public string? AccountNumber { get; set; }
        public string? BankFIOrgName { get; set; }
        public string? ProductName { get; set; }
        public int? IntPayOutPeriodicity { get; set; }
        public bool? IsAutoRenewed { get; set; }
        public int? TermInDays { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? MaturedValue { get; set; }   
        public decimal? AIT { get; set; }
        public string? Periodicity { get; set; }
        public string? RenewalStatus { get; set; }
        public string? FundBank { get; set; }
    }

    public class DepositInstrumentRenewal
    { 
        public int? DepositRenewalID { get; set; }
        public decimal? RenewalDepositAmount { get; set; }
        public int? DepositID { get; set; }
        public decimal? RenewalInterestRate { get; set; }
        public int? TermInDays { get; set; }
        public string? RenewalIssueDate { get; set; }
        public string? RenewalMaturityDate { get; set; }
        public decimal? RenewalAIT { get; set; }
        public decimal? RenewalExciseDuty { get; set; }
        public decimal? AdjustmentInterest { get; set; }
        public decimal? AITRate { get; set;}
        public decimal? AccruedInterest { get; set; }
        public decimal? AccruedAIT { get; set; }
    }

}
