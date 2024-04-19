using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ImportExportOmnibus
{
    public class ExportFileBrokerDto
    {
        public int BrokerID { get; set; }
        public string BrokerName { get; set; }

    }
    public class ExportFileDto
    {
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public string FileContent { get; set; }

    }
    public class ILAMLCashLimitFileDto
    {
        public int? BrokerID { get; set; }
        public string? TradeDate { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? MemberName { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOCode { get; set; }
        public Nullable<decimal> Purchasepower { get; set; }
        public Nullable<decimal> Deposit { get; set; }
    }
    public class ILAMLShareLimitFileDto
    {
        public int? BrokerID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? BOCode { get; set; }
        public string? MemberName { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public Nullable<decimal> FreeQuantity { get; set; }
        public string ISIN { get; set; }
        public string ScripName { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> Deposit { get; set; }
    }
    public class SLStockLimitFileDto
    {
        public Nullable<int> StockLimitID { get; set; }

        public Nullable<DateTime> TradingDate { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> InstrumentID { get; set; }
        public Nullable<decimal> SalableQuantity { get; set; }
        public Nullable<decimal> IncrementalQuantity { get; set; }

        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<int> ModNo { get; set; }
        public string ISIN { get; set; }
        public string ScriptsName { get; set; }
        public string TradingCode { get; set; }

        public string PositionType { get; set; }
    }

    public class OmnibusShareFileTextDto
    {
        public string? ISIN { get; set; }
        public string? ScriptsName { get; set; }
        public string? BOID { get; set; }
        public string? AccountName { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? SalableQuantity { get; set; }
        public string? AccountNumber { get; set; }
        public string? TradingDate { get; set; }
    }

    public class OmnibusCashFileTextDto
    {
        public string? AccountNumber { get; set; }
        public string? BOID { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public decimal? PurchasePower { get; set; }
        public string? Iselectronic { get; set; }
        public string? TradingDate { get; set; }
    }

    public class LimitFileDto 
    {
        public Nullable<int> OmbLimitFileID { get; set; }

        public string? FileName { get; set; }
        public string? Fileextension { get; set; }
        public string? FileSize { get; set; }
        public Nullable<int> FileRow { get; set; }
        public string? FileType { get; set; }

        public string? ProcessingMode { get; set; }
        public string? Exchangeid { get; set; }
        public Nullable<int> SAContractID { get; set; }
        public Nullable<DateTime> TradingDate { get; set; }
        public string? Maker { get; set; }

        public Nullable<DateTime> Makedate { get; set; }
        public string? ExchangeName { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }

    }


    public class RegistrationTagDto
    {
        public Nullable<int> RegistrationID { get; set; }
        public Nullable<int> ExportFileId { get; set; }
        public Nullable<DateTime> TradingDate { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? DealerID { get; set; }
        public string? BOID { get; set; }
        public string? WithNetAdjustment { get; set; }
        public string? AccountName { get; set; }
        public string? AccuontShortName { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? ICNo { get; set; }
        public string? AccountType { get; set; }
        public string? ShortSellAllowing { get; set; }
        public string? Status { get; set; }
        public string? Exchangeid { get; set; }
        public Nullable<int> Modno { get; set; }
    }

    public class BuySellLimitTagDto
    {
        public Nullable<int> BuyLimitID { get; set; }

        public Nullable<DateTime> TradingDate { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<decimal> PurchasePower { get; set; }
        public Nullable<decimal> IncrementalPP { get; set; }
        public string? ModNo { get; set; }

        public string? TradingCode { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> MarginLimit { get; set; }
    }

    public class MarketLimitTagDto
    {
        public string? TradingCode { get; set; }

        public string? ShortName { get; set; }
        public Nullable<decimal> maxcapitalbuy { get; set; }
        public Nullable<decimal> maxcapitalSell { get; set; }
        public Nullable<decimal> TotalTransaction { get; set; }
        public Nullable<decimal> NetTransaction { get; set; }

    }


}
