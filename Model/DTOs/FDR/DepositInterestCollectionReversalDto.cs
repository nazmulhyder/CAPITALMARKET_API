using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FDR
{
    public class DepositInterestCollectionReversalDto
    {
        public int? IntCollReversalID { get; set; }
        public string? ReversalReason { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? IntCollectionID { get; set; }
    }

    public class DepositIntCollectionReversalInfoDto
    {
        public int? IntCollectionID { get; set; }
        public string? IntCollectionDate { get; set; }
        public string? InterestAmount { get; set; }
        public string? DepositACCNo { get; set; }
    }

    public class ListInterestCollectionReversalDto
    {
        public int IntCollReversalID { get; set; }
        public int IntCollectionID { get; set; }
        public string? AccountNumber { get; set; }
        public string? DepositACCNo { get; set; }
        public string? ProductName { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? InterestCollected { get; set; }
        public string? IntCollectionDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ReversalReason { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
    }
}
