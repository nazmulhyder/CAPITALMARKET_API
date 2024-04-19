using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Allocation
{

    public class AllocationApprovalDto
    {
        public int AllocationID { get; set; }
        public string AllocationType { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class SaleAllocationApprovalDetailListDto
    {
        public DateTime? TradingDate { get; set; }
        public int? SaleOrderID { get; set; }
        public int? SaleAllocationID { get; set; }
        public int? SaleAllocationInstID { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }      
        public decimal? ExecutedQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? TradeExecutedQty { get; set; }
        public decimal? RemainingQty { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }

    }
    public class SaleAllocationApprovalListDto
    {
        public int? SaleAllocationID { get; set; }
        public DateTime? TradingDate { get; set; }
        public int? SaleOrderID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? TotalOrderQty { get; set; }
        public decimal? TotalExecutedQuantity { get; set; }
        public decimal? TradeExecutedQuantity { get; set; }
        public int? RequiredLevel { get; set; }
        public int? CurrentLevel { get; set; }
        public int? ApprovalReqID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? NoOfAccounts { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestDate { get; set; }
        public string? BatchNo { get; set; }



    }

    public class BuyAllocationApprovalListDto
    {
        public int? BuyAllocationID { get; set; }
        public DateTime? TradingDate { get; set; }
        public int? BuyOrderID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? TotalOrderQty { get; set; }
        public decimal? TotalExecutedQty { get; set; }
        public decimal? TradeExecutedQuantity { get; set; }
        public int? RequiredLevel { get; set; }
        public int? CurrentLevel { get; set; }
        public int? ApprovalReqID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? NoOfAccounts { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestDate { get; set; }
        public string? BatchNo { get; set; }

    }

    public class BuyAllocationApprovalDetailListDto
    {
        public int? BuyAllocationID { get; set; }
        public DateTime? TradingDate { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? ExecutedQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? TradeExecutedQty { get; set; }
        public decimal? RemainingQty { get; set; }
        public decimal? CalculatedPurchasePower { get; set; }
       
      

    }
}
