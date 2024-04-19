using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PriceFileUpload
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class GsecPriceFileDto
    {
        [JsonProperty("Security Name")]
        public string SecurityName { get; set; }

        [JsonProperty("Security Short Name")]
        public string SecurityShortName { get; set; }
        public string Currency { get; set; }

        [JsonProperty("Session Name")]
        public string SessionName { get; set; }
        public string Security { get; set; }
        public string Board { get; set; }
        public string ISIN { get; set; }

        [JsonProperty("Accrued Interest")]
        public string AccruedInterest { get; set; }
        public string Spot { get; set; }

        [JsonProperty("Has News")]
        public string HasNews { get; set; }

        [JsonProperty("Bid Price")]
        public string BidPrice { get; set; }

        [JsonProperty("Bid Depth")]
        public string BidDepth { get; set; }

        [JsonProperty("Offer Price")]
        public string OfferPrice { get; set; }

        [JsonProperty("Offer Depth")]
        public string OfferDepth { get; set; }

        [JsonProperty("Ref Price")]
        public string RefPrice { get; set; }

        [JsonProperty("Open Price")]
        public string OpenPrice { get; set; }

        [JsonProperty("Open Qty")]
        public string OpenQty { get; set; }

        [JsonProperty("High Price")]
        public string HighPrice { get; set; }

        [JsonProperty("Low Price")]
        public string LowPrice { get; set; }

        [JsonProperty("Avg Price")]
        public string AvgPrice { get; set; }

        [JsonProperty("Close Price")]
        public string ClosePrice { get; set; }
        public string TOP { get; set; }

        [JsonProperty("Last Price")]
        public string LastPrice { get; set; }

        [JsonProperty("Change Price")]
        public string ChangePrice { get; set; }

        [JsonProperty("Change LTP")]
        public string ChangeLTP { get; set; }

        [JsonProperty("Number of Trades")]
        public string NumberOfTrades { get; set; }

        [JsonProperty("Daily Volume")]
        public string DailyVolume { get; set; }

        [JsonProperty("Daily Value")]
        public string DailyValue { get; set; }

        [JsonProperty("Unadjusted Prev Price")]
        public string UnadjustedPrevPrice { get; set; }

        [JsonProperty("52 Week High Price")]
        public string _52WeekHighPrice { get; set; }

        [JsonProperty("52 Week Low Price")]
        public string _52WeekLowPrice { get; set; }

        [JsonProperty("Change Percent")]
        public string ChangePercent { get; set; }
        public string Orders { get; set; }

        [JsonProperty("Last Auction Price")]
        public string LastAuctionPrice { get; set; }

        [JsonProperty("Last Auction Yield")]
        public string LastAuctionYield { get; set; }
        public string Category { get; set; }

        [JsonProperty("Maturity Date")]
        public string MaturityDate { get; set; }

        [JsonProperty("Bid Yield")]
        public string BidYield { get; set; }

        [JsonProperty("Offer Yield")]
        public string OfferYield { get; set; }

        [JsonProperty("Last Yield")]
        public string LastYield { get; set; }

        [JsonProperty("Current Outstanding Balance")]
        public string CurrentOutstandingBalance { get; set; }
    }


  
}
