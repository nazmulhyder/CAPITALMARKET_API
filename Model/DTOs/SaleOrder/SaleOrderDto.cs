using Model.DTOs.BuyOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SaleOrder
{
    public class ListDPMProductPortfolioDto
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal?  Salable { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Weight { get; set; }
        public decimal? MarketValue { get; set; }
    }


    public class ListSaleOrderAccWisePortfolioDto
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
        public decimal? MarketValue { get; set; }

    }

    public class ListDPMProductPortfolioAccountWiseDto
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? Salable { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Weight { get; set; }
        public decimal? MarketValue { get; set; }
    }

    public class SaleOrderDto
    {
        //public int SaleOrderInstrumentID { get; set; }
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? Salable { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? Weight { get; set; }
        //public int SaleOrderID { get; set; }
        public string? CriteriaType { get; set; }
        public decimal? ExpectedPrice { get; set; }
        public decimal? TargetPercentage { get; set; }
        public decimal? OrderPercentage { get; set; }

    }

    public class SaleOrderAccountDto
    {
        public int? InstrumentID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? Instrument { get; set; }
        public string? CriteriaType { get; set; }
        public decimal?  Equity { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Stock { get; set; }
        public decimal? NewStock { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? ExpPrice { get; set; }
        public decimal?  SellValue { get; set; }
        public decimal? Weight { get; set; }
        public decimal? ExpWeight { get; set; }
        
        public decimal? Profit { get; set; }
        public decimal? SalablePortfolio { get; set; }
        public decimal? ProjectedQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? TotalOrderQty { get; set; }

    }

    public class GenerateSaleOrderAccDto
    {
        public int ProductID { get; set; }
        public string AccountSelectionType { get; set; }
        public string? AccountIds { get; set; }

        public List<SaleOrderDto>? saleOrders { get; set; }

    }

    public class SaveSaleOrderDto
    {
        public int ProductID { get; set; }
        public int ExchangeID { get; set; }
        public List<SaleOrderDto>? SaleOrderInstrument { get; set; }
        public List<SaleOrderInstrumentDetail>? SaleOrderInstrumentDetail { get; set; }

    }

    public class SaleOrderInstrumentDetail
    {
        public int InstrumentID { get; set; }
        public int ContractID { get; set; }
        public decimal? Equity { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? Stock { get; set; }
        public decimal? NewStock { get; set; }
        public decimal? AvgPrice { get; set; }
        public decimal? ExpPrice { get; set; }
        public decimal? SellValue { get; set; }
        public decimal? Weight { get; set; }
        public decimal? ExpWeight { get; set; }
        public decimal? Profit { get; set; }
        public decimal? SalablePortfolio { get; set; }
        public decimal? ProjectedQty { get; set; }
        public decimal? OrderQty { get; set; }
        public decimal? TotalOrderQty { get; set; }

    }

    public class SaveSellOrderAccountWise
    {
        public int ProductID { get; set; }
        //public string AccountNo { get; set; }
        public int ExchangeID { get; set; }
        public List<ListSaleOrderAccWisePortfolioDto>? OrderAccWiseList { get; set; }
    }

}
