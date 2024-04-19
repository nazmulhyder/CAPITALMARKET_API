using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeFileUpload
{
  
    public class TradeDetail
    {
        //public Attributes _attributes { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string ISIN { get; set; }
        public string AssetClass { get; set; }
        public string OrderID { get; set; }
        public string RefOrderID { get; set; }
        public string Side { get; set; }
        public string BOID { get; set; }
        public string SecurityCode { get; set; }
        public string Board { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Value { get; set; }
        public string ExecID { get; set; }
        public string Session { get; set; }
        public string FillType { get; set; }
        public string Category { get; set; }
        public string CompulsorySpot { get; set;}
        public string ClientCode { get; set; }
        public string TraderDealerID { get; set; }
        public string OwnerDealerID { get; set; }
        public string TradeReportType { get; set; }
        public string version { get; set; }
        public string encoding { get; set; }

        public string TraderCode { get; set; }
        public string ScripCode { get; set; }
        public string HowlaNo { get; set; }
        public string ClientType { get; set; }

        public string TradeNo { get; set; }
        public string SettlementValue { get; set; }
        public string Yield { get; set; }
        public string AccruedInterest { get; set; }
    }

    public class TradeData
    {
        public Trades Trades { get; set; }
    }

    public class Trades
    {
        public List<TradeDetail> Detail { get; set; }
    }

   
}
