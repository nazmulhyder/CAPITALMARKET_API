using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FDR
{
    public class InterestCollectionInfoDto
    {
        public string? DepositACCNo { get; set; }
        public int? BankFIOrgID { get; set; }
        public string? BankFIOrgName { get; set; }
        public string? ProductName { get; set; }
        public int? DepositID { get; set; }
        public string? DepositOpeningDate { get; set; }
        public decimal? DepositAmount { get; set; }
        public int? DepositTerm { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? InterestAmount { get; set; }     
        public string? MaturityDate { get; set; }
        public string FundName { get; set; }
        public int ? FundID { get; set; }
        public decimal? AccruedInterest { get; set; } = 0;
        public decimal? AccruedAIT { get; set; } = 0;
       
    }

    public class InterestCollectionDto
    {
        public int IntCollectionID { get; set; }
        public string? DepositACCNo { get; set; }
        public int? BankFIOrgID { get; set; }
        public string? BankFIOrgName { get; set; }
        public string? ProductName { get; set; }
        public int? DepositID { get; set; }
        public string? DepositOpeningDate { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? AIT { get; set; }
        public decimal? IntCollectionAmount { get; set; }
        public int? DepositTerm { get; set; }
        public decimal? InterestRate { get; set; }
        public decimal? InterestAmount { get; set; }
        public string? MaturityDate { get; set; }
        public string? IntCollectionDate { get; set; }
        public int? BankAccountID { get; set; }
        public decimal? ExciseDuty { get; set; } = 0;
        public decimal? AdjustmentInterest { get; set; } = 0;
        public decimal? AdjustmentAIT { get; set; } = 0;
    }

    public class ListInterestCollectionDto
    {
        public int IntCollectionID { get; set; }
        public string? DepositACCNo { get; set; }
        public int? BankFIOrgID { get; set; }
        public string? BankFIOrgName { get; set; }
        public string? DepositProductName { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? InterestCollected { get; set; }
        public string? IntCollectionDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public decimal? AIT { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public decimal? ExciseDuty { get; set; } = 0;
        public decimal? AdjustmentInterest { get; set; } = 0;
        public decimal? AdjustmentAIT { get; set; } = 0;

    }
}
