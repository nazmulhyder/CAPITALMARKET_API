using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.DTOs.InternalFundTransfer
{
    public class InternalFundTransferDto
    {
        public int? IntFundTransferID { get; set; }
        public int? DebitContractID { get; set; }
        public int? CreditContractID { get; set; }
        public decimal? TransferAmount { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? TransferRemarks { get; set; }

        [JsonIgnore]
        public int? DebitTransactionID { get; set; }

        [JsonIgnore]
        public int? CreditTransactionID { get; set; }

    }

    public class CustomerAvailableBalanceInfo
    {
        public int? PAIndexID { get; set; }
        public string? PACIF { get; set; }
        public string? PAName { get; set; }
        public string? JACIF { get; set; }
        public string? JAName { get; set; }
        public int? JAIndexID { get; set; }
        public int? ContractID { get; set; }
        public decimal? AvailableBalance { get; set; }

    }



}
