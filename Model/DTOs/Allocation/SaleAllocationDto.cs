using Model.DTOs.SaleOrder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Allocation
{

    #region Sale Order Allocation

    public class SaleAllocationBatchInfoDto
    {
        public int SaleOrderID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? BatchNo { get; set; }
        public string? Maker { get; set; }
        public DateTime TradingDate { get; set; }
        public int ExchangeID { get; set; }
        public string? ExchangeName { get; set; }

    }

    public class SaleOrderTradeDto
    {
        public int SaleOrderID { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public string CriteriaType { get; set; }
        public int? SaleOrderInstrumentID { get; set; }
        public decimal ExpectedPrice { get; set; }
        public decimal OrderQty { get; set; }
        public decimal ExecutedQty { get; set; }
        public decimal ExeAvgPrice { get; set; }
        //public decimal Equity { get; set; }
        public decimal QtyToBeAllocated { get; set; }
        public string AllocationType { get; set; }
    }

    public class AccountSaleOrderAllocationDto
    {

        public int InstrumentID { get; set; }
        public string? Instrument { get; set; }
        public int? SaleOrderInstrumentDetailID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int ContractID { get; set; }
        public string? AccountNo { get; set; }     
        public decimal? Stock { get; set; }
        public decimal? Equity { get; set; }
        public decimal? NetWorth { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? ExeWeight { get; set; }
        public decimal? ExpPrice { get; set; }
        public decimal? ReqWeight { get; set; }
        public decimal? ProjectedQty { get; set; }
        //public decimal? RequiredQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? AllocatedQty { get; set; }

    }

    public class AccountSaleOrderAllocation_v2_Dto
    {

        public Nullable<int> InstrumentID { get; set; }
        public string Instrument { get; set; }
        public Nullable<int> SaleOrderInstrumentDetailID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string AccountNo { get; set; }
        public Nullable<decimal> Stock { get; set; }
        public Nullable<decimal> Equity { get; set; }
        public Nullable<int> NetWorth { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> ExeWeight { get; set; }
        public Nullable<decimal> ExpPrice { get; set; }
        public Nullable<int> ReqWeight { get; set; }
        public Nullable<decimal> ProjectedQty { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        public string? AllocationType { get; set; }
        public Nullable<decimal> AllocatedQty { get; set; }
        public Nullable<decimal> totalExecutedQty { get; set; }
        public Nullable<decimal> availableExecutedQty { get; set; }
        public Nullable<decimal> ReqOrderQty { get; set; }
        public Nullable<decimal> totalAllocatedOrderQty { get; set; }

    }

    public class SaveSaleOrderAllcoationDto
    {
        public int SaleOrderID { get; set; }
        public List<SaleOrderTradeDto>? SaleOrderTrades { get; set; }
        public List<AccountSaleOrderAllocationDto>? SaleOrderAllocationAccounts { get; set; }

    }

    #endregion

    #region Buy Order Allocation
    public class BuyAllocationBatchInfoDto
    {
        public int BuyOrderID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? BatchNo { get; set; }
        public string? Maker { get; set; }
        public DateTime TradingDate { get; set; }
        public int ExchangeID { get; set; }
        public string? ExchangeName { get; set; }

    }
    public class BuyOrderTradeDto
    {
        public int BuyOrderID { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal Price { get; set; }
        public decimal Priority { get; set; }
        public int? BuyOrderInstrumentID { get; set; }
        public decimal OrderQty { get; set; }
        public decimal ExecutedQty { get; set; }
        public decimal ExeAvgPrice { get; set; }
        public decimal QtyToBeAllocated { get; set; }
        public string AllocationType { get; set; }
        //public decimal Equity { get; set; }
    }

    public class AccountBuyOrderAllocationDto
    {

        public int InstrumentID { get; set; }
        public string? Instrument { get; set; }
        public int? SaleOrderInstrumentDetailID { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int ContractID { get; set; }
        public string? AccountNo { get; set; }
        //public decimal? RequiredQty { get; set; }
        public Nullable<int> NetWorth { get; set; }
        public decimal? ExistingWeight { get; set; }
        public decimal? ProjectedQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? AllocatedQty { get; set; }
        public decimal? Stock { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? ExpPrice { get; set; }
        public string? Priority { get; set; } 

    }
    public class AccountBuyOrderAllocation_v2_Dto
    {

        public Nullable<int> InstrumentID { get; set; }
        public string Instrument { get; set; }
        public Nullable<int> SaleOrderInstrumentDetailID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string AccountNo { get; set; }
        public Nullable<decimal> Stock { get; set; }
        public Nullable<decimal> Equity { get; set; }
        public Nullable<int> NetWorth { get; set; }
        public Nullable<decimal> AvgPrice { get; set; }
        public Nullable<decimal> ExistingWeight { get; set; }
        public Nullable<decimal> ExpPrice { get; set; }
        public Nullable<int> ReqWeight { get; set; }
        public Nullable<decimal> ProjectedQty { get; set; }
        public Nullable<decimal> OrderQty { get; set; }
        public string? AllocationType { get; set; }
        public Nullable<decimal> AllocatedQty { get; set; }
        public Nullable<decimal> totalExecutedQty { get; set; }
        public Nullable<decimal> availableExecutedQty { get; set; }
        public Nullable<decimal> ReqOrderQty { get; set; }
        public Nullable<decimal> totalAllocatedOrderQty { get; set; }
        public string? Priority { get; set; }

    }
    public class SaveBuyOrderAllcoationDto
    {
        public int BuyOrderID { get; set; }
        public List<BuyOrderTradeDto>? BuyOrderTrades { get; set; }
        public List<AccountBuyOrderAllocationDto>? BuyOrderAllocationAccounts { get; set; }

    }
    #endregion
}
