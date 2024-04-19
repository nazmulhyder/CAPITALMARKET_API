using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PriceFileUpload
{
  

    public class priceFileDto
    {
        public Declaration? _declaration { get; set; }
        public EODTickers? EODTickers { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attributes
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

    public class Declaration
    {
        public Attributes? _attributes { get; set; }
    }

    public class EODTickers
    {
        public List<Ticker>? Ticker { get; set; }
    }

  

    public class Ticker
    {
        public Attributes? _attributes { get; set; }
    }


}
