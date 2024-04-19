using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeDataCorrection
{
    public class SLTradeFileStatusDto
    {
		public Nullable<int> TradeFileID { get; set; }

		public string? FileName { get; set; }
		public Nullable<decimal> FileSizeInKB { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public string? ExchangeName { get; set; }

		public Nullable<int> BrokerID { get; set; }
		public string? BrokerName { get; set; }

		public Nullable<DateTime> TradeDate { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public Nullable<int> IsReversalRequested { get; set; }
		public string? RequestedBy { get; set; }
		public Nullable<DateTime> RequestedOn { get; set; }

		public Nullable<int> IsReversalDone { get; set; }
		public string? ApprovalStatus { get; set; }
	}
}
