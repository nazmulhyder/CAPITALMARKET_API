using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeFileUpload
{
	
	public class ILTradeFileDetailDto
	{

		public string? OrderRefNo { get; set; }
		public string? SecurityCode { get; set; }
		public string? ISIN { get; set; }
		public string? TraderCode { get; set; }
		public string? TradeType { get; set; }
		public Nullable<decimal> Quantity { get; set; }
		public Nullable<decimal> Rate { get; set; }
		public DateTime? TradeDateTime { get; set; }
		public string? Market { get; set; }
		public string? Status { get; set; }
		public string? HowlaType { get; set; }
		public string? ForeginFlag { get; set; }
		public string? AccountNumber { get; set; }
		public string? BOID { get; set; }
		public string? HowlaRefNo { get; set; }
		public string? CompolsarySpot { get; set; }
		public string? InstrumentCategory { get; set; }
	}
}
