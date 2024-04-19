using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.DTOs.SellSurrender
{

    public class SS_UnitPurchaseDetailSetupDto
    {
        public int? PurchaseDetailID { get; set; } = 0;
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public string? TransactionDate { get; set; }
        public int? UnitPrice { get; set; }
        public decimal? PurchaseUnit { get; set; }
        public string? SaleNo { get; set; }
        public int? SalesRMID { get; set; }
        public string? SaleType { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? TransactionID { get; set; }
        public int? LedgerID { get; set; }
        public string? ActivationStatus { get; set; }
        public string? RequestSource { get; set; }
        public string? MemberName { get; set; }
        public string? ProductName { get; set; }
        public string? BOCode { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? UnclearChequeBalance { get; set; }
        public decimal? LedgerBalance { get; set; }
        public decimal? CurrentNav { get; set; } // PURCHASE PRICE
        public decimal? PurchaseAmount { get; set; }
        public string? RMName { get; set; }
        public int? ProductID { get;set; }
        public string? AccountNumber { get; set; }
        public int? UnitActivationID { get; set; }


    }


    public class SS_UpdateActivateRequestDto
    {
       public string? UnitActivationIDs { get; set; }
       public string? Action { get; set; }
       public string? CancelReason { get; set; }
    }

    public class SS_UnitSurrenderDto
    {
        public int? SurrenderDetailID { get; set; } = 0;
        public string? TransactionDate { get; set; } 
        public int? ContractID { get; set; } 
        public int? InstrumentID { get; set; } 
        public int? SurrenderUnit { get; set; } 
        public decimal? SurrenderPrice { get; set; } 
        public decimal? ExitLoadValue { get; set; } 
        public decimal? NetSurrenderValue { get; set; } 
        public string? SurrenderNo { get; set; } 
        public string? RequestSource { get; set; } 
        public string? Maker { get; set; } 
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string ? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? BOCode { get; set; }
        public decimal? TotalUnits { get; set; }
        public decimal? AvailableUnits { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set;}
        public decimal? GrossSurrenderValue { get; set; } = 0;
        public string? RetrievePurchaseDate { get; set; }
        public decimal? ExitLoadValueInAmount { get; set; } = 0;
    }

}
