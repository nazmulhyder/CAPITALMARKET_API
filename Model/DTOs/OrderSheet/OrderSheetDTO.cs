using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.OrderSheet
{
    public class OrderSheetDTO
    {
        public int ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? ProductName { get; set; }
        public int DocumentID { get; set; }
        public int? SL { get; set; }
        public int CollectedSheet { get; set; }
        public int RemainingSheet { get; set; }
        public string? CurrentStatus { get; set; }
        public string? CollectionDate { get; set; }
        public string? StatusDate { get; set; }
        public string? Remarks { get; set; }
        public int? DocInventoryID { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? StoreLocation { get; set; }

    }

    public class ReleasedOrdersheetDTO : OrderSheetDTO
    {
        public int NoOfReleasedSheet { get; set; }
    }
    
    public class ListOrdersheetDTO
    {
        public int ContractID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? CollectionDate { get; set; }
        public int? CollectedSheet { get; set; } = 0;
        public int? RemainingSheet { get; set; } = 0;
        public string? CurrentStatus { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public int? DocInventoryID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? RMName { get;set; }

    }

    public class OrderSheetPrintDTO
    {
        public decimal? AvgPrice { get; set; }
        public int? ContractID { get; set; }
        public string? ExchangeName { get; set; }
        public string? InstrumentName { get; set; }
        public string? ISIN { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalQty { get; set; }
        public string? TradeDate { get; set; }
        public string? TradeDetailID { get; set; }
        public string? TransactionType { get; set; }
        public string? SecurityCode { get; set; }
        public decimal? Rate { get; set; }

    }

    public class ZeroOrdersheetListDTO
    {
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? RMName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? LastCollectionDate { get; set; }
        public int? TotalCollectedSheets { get; set; }
        public int? TotalRemainingSheets { get; set; }
        
    }

    public class OrdersheetReleasedDTO
    {
        public int? DocInventoryID { get; set; }
        public int? NoOfReleasedSheet { get; set; }
        public string? Remarks { get; set; }

    }

}
