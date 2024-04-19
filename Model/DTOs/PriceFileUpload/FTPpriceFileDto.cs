using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PriceFileUpload
{
    public class priceFileFTPDto
    {
        public Declaration? _declaration { get; set; }
        public FTPEODTickers? EODTickers { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);


    public class FTPEODTickers
    {
        public List<FTPTicker>? Ticker { get; set; }
    }



    public class FTPTicker
    {
        public string? version { get; set; }
        public string? encoding { get; set; }
        public string? SecurityCode { get; set; }
        public string? ISIN { get; set; }
        public string? AssetClass { get; set; }
        public string? CompulsorySpot { get; set; }
        public string? TradeDate { get; set; }
        public string? Close { get; set; }
        public string? Open { get; set; }
        public string? High { get; set; }
        public string? Low { get; set; }
        public string? Var { get; set; }
        public string? VarPercent { get; set; }
        public string? Category { get; set; }
        public string? Sector { get; set; }
    }
}
