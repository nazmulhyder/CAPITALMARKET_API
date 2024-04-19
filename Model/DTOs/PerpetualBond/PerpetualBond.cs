
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PerpetualBond
{

    public class PerpetualBond
    {
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        //public string? IssueDate { get; set; }
        //public string? MaturityDate { get; set; }
        //public decimal? CouponRate { get; set; }
        public decimal? NoOfHoldings { get; set; }
        public decimal? ValueOfHolding { get; set; }
        public decimal? AIT { get; set; }
        public decimal? CollectionAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public int? ContractID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? InterestAmount { get; set; }

    }

    public class PerpetualBondClaim
    {
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        //public string? IssueDate { get; set; }
        //public string? MaturityDate { get; set; }
        //public decimal? CouponRate { get; set; }
        public decimal? NoOfHoldings { get; set; }
        public decimal? ValueOfHolding { get; set; }
        public decimal? AIT { get; set; }
        public decimal? CollectionAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public int? ContractID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public decimal? InterestAmount { get; set; }
        public int? ClaimID { get; set; }

    }

    public class PB_ActiveBondInstrument
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? CouponRate { get; set; }
    }

    public class BondNewDeclaredInstrument
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? Status { get; set; }
    }

    public class ListPerpetualBond
    {
        public int? InstCollectionID { get; set; }
        public string? AccountNumber { get; set; }
        public string? ProductName { get; set; }
        public string? CollectionAmount { get; set; }
        public string? InterestRate { get; set; }
        public string? AIT { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? InstrumentName { get; set; }
    }

    public class PerpetualBondDeclarationDto
    {
        public int? DeclarationID { get; set; }
        public string? InstrumentName { get; set; }
        public string? TenorStartDate { get; set; }
        public string? TenorEndDate { get; set; }
        public string? RecordDate { get; set; }
        public decimal? InterestRate { get; set; }
        public string? ApprovalStatus { get; set; }
    }

    public class PerpetualBondForReversalDto
    {
        public int? InstCollectionID { get; set; }
        public string? IntCollectionDate { get; set; }
        public decimal? CollectionAmount { get; set; }
        public decimal? InterestRate { get; set; }
    }

    public class PerpetualBondReversalDto
    {
        public int? IntCollReversalID { get; set; }
        public int? InstCollectionID { get; set; }
        public string? IntCollectionDate { get; set; }
        public decimal? CollectionAmount { get; set; }
        public decimal? InterestRate { get; set; }
        public string? ReversalReason { get; set; } 
    }

}
