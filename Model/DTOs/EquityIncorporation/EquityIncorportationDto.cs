using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.EquityIncorporation
{

    public class EquityDeductionDto
    {

        public Nullable<int> ContractID { get; set; }

        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public Nullable<int> FinancialInfoID { get; set; }

        public Nullable<decimal> AvailableBalance { get; set; }
        public string? ComEquityPercentage { get; set; }
        public Nullable<decimal> Equity { get; set; }
        public Nullable<decimal> ClientEquity { get; set; }
        public Nullable<decimal> ComEquityAmount { get; set; }
        public Nullable<decimal> TotalDeductedAmount { get; set; }

        public Nullable<decimal> AmountTobeDeducted { get; set; }
    }


    public class ClientInfoDto
    {
        public string MemberName { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
    public class EquityAdditionDto
    {
        public string? MemberName { get; set; }

        public Nullable<int> CollectionInfoID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<decimal> CollectionAmount { get; set; }
        public string? EntryType { get; set; }
        public string? InstrumentType { get; set; }

        public string? CollectionStatus { get; set; }
        public string? InstrumentNumber { get; set; }
        public Nullable<int> MonInstrumentID { get; set; }
        public string? InstrumentDate { get; set; }
        public string? CollectionDate { get; set; }

        public Nullable<int> ComEquityAddID { get; set; }
        public Nullable<decimal> ComEquityPercentage { get; set; }
        public Nullable<decimal> ComEquityAmount { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Maker { get; set; }

        public Nullable<DateTime> MakerDate { get; set; }

        public Nullable<int> DebitContractID { get; set; }
        public Nullable<int> CreditContractID { get; set; }
    }

    public class EquityAdditionApprovalDto
    {
        public Nullable<int> ComEquityAddID { get; set; }
        public Nullable<int> ComEquityDedID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
    }
}
