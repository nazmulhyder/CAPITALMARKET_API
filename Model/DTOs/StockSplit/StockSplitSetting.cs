using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.StockSplit
{
    public class StockSplitSetting
    {
        public int? StockSplitSettingID { get; set; } = 0;
        public decimal? SplitRatio { get; set; }
        public decimal? FaceValue { get; set; }
        public decimal? NewFaceValue { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public List<StockSplitDetail>? stockSplitDetails  {get;set;}
            
    }
    public class StockSplitDetail
    {
        public int? StockSplitDetailID { get; set; } = 0;
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public int? HoldingQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public int? ProjectedQuantity { get; set; }
        public decimal? ProjectedAvgPrice { get; set; }
        public int? ReceiveQuantity { get; set; }
        public int? InstLedgerID { get; set; }
        public int? StockSplitSettingID { get; set; }

    }

    public class InstrumentHoldingDto
    {
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? ProductName { get; set; }
        public int? ProductID { get; set; }
        public int? InstrumentID { get; set; }
        public int? HoldingQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public int? ProjectedQuantity { get; set; }
        public decimal? ProjectedAvgPrice { get; set; }
        public int? ReceiveQuantity { get; set; }
        public decimal? TotalCost { get; set; }
    }


}
