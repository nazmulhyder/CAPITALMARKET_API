using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FDR
{
    public class DepositEncashmentDto
    {
        public int? EncashmentID { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? AIT { get; set; }
        public decimal? TotalCharge { get; set; }
        public string? EncashmentDate { get; set; }
        public decimal? EncashmentAmount { get; set; }
        public int? TransactionID { get; set; }
        public int? DepositID { get; set; }
        public decimal? ExciseDuty { get; set; }
        public decimal? PrematureCharge { get; set; }
        public int? BankAccountID { get; set; }
    }

    public class DepositInterestInfoDto
    {
        public string? DepositACCNo { get; set; }
        public int? BankFIOrgID { get; set; }
        public string? BankFIOrgName { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? DepositID { get; set; }
        public string? DepositOpeningDate { get; set; }
        public int? DepositTerm { get; set; }
        public string? MaturityDate { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? IntCollectionAmount { get; set; }
    }

    public class ListDepositEncashmentDto
    {
        public int? EncashmentID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? DepositACCNo { get; set; }
        public string? EncashmentDate { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? EncashmentAmount { get; set; }
        public string? DepositBank { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? AIT { get; set; }
        public decimal? ExciseDuty { get; set; }
        public decimal? PrematureCharge { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public string? RenewalIssueDate { get; set; }
        public decimal? IntRate { get; set; }
        public decimal? aitRate { get; set; }
        public decimal? accAIT { get; set; }
        public decimal? accInterest { get; set; }
    }
}
