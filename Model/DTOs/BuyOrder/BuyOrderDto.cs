using Model.DTOs.SaleOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BuyOrder
{
    public class ListDPMProductPortfolioDto
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? Salable { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Weight { get; set; }
        public decimal? TotalQuantity { get; set; }

    }

    public class ListBuyOrderAccWisePortfolioDto
    {
        public int? ContractID { get; set; }
        public string? AccountNo { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? Salable { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Weight { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? PurchasePower { get; set; }

    }

    public class BuyOrderInstrumentDto
    {
        //public int BuyOrderInstrumentID { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public decimal? Salable { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Price { get; set; }
        public int Priority { get; set; }
        public decimal? TargetWeight { get; set; }
        public decimal? TargetQty { get; set; }
        public decimal? OrderQty { get; set; }

    }

    public class BuyOrderAccountDto
    {
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? Instrument { get; set; }
        public int? Priority { get; set; }
        public decimal? Stock { get; set; }
        public decimal? Equity { get; set; }
        public decimal? PurchasePower { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? ExistingWeight { get; set; }
        public decimal? ExpPrice { get; set; }
        public decimal? RequiredWeight { get; set; }
        public decimal? ProjectedQty { get; set; }
        public decimal? RequiredQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? CalculatedPurchasePower { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? TotalOrderQty { get; set; }

    }

    public class GenerateBuyOrderAccDto
    {
        public int ProductID { get; set; }
        public string? AccountSelectionType { get; set; }
        public string? AccountIds { get; set; }
        public bool? IsNettingAllowed { get; set; }
        public List<BuyOrderInstrumentDto>? BuyOrders { get; set; }

    }

    public class SaveBuyOrderDto
    {
        public int ProductID { get; set; }
        public int ExchangeID { get; set; }
        public List<BuyOrderInstrumentDto>? BuyOrders { get; set; }
        public List<BuyOrderAccountDto>? BuyOrderInstrumentDetails { get; set; }

    }

    public class SaveBuyOrderAccountWise
    {
        public int ProductID { get; set; }
        //public string AccountNo { get; set; }
        public int ExchangeID { get; set; }
        public List<ListBuyOrderAccWisePortfolioDto>? OrderAccWiseList { get; set; }
    }

    //public class BuyOrderInstrumentDetail
    //{
    //    public int InstrumentID { get; set; }
    //    public int ContractID { get; set; }
 
    //    public decimal? ExeWeight { get; set; }
    //    public decimal? RequiredQty { get; set; }
    //}

}
