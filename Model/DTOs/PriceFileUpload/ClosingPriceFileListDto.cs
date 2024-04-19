using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PriceFileUpload
{
    public class ClosingPriceFileListDto
    {
			public Nullable<int> PriceFileID { get; set; }

			public Nullable<DateTime> TradingDate { get; set; }
			public string? ExchangeName { get; set; }
			public Nullable<int> ExchangeID { get; set; }
			public Nullable<Boolean> IsGsecFile { get; set; }
			public string? FileName { get; set; }

			public Nullable<decimal> FileSizeInKB { get; set; }
			public Nullable<DateTime> MakeDate { get; set; }
			public string? Maker { get; set; }
			public string? Status { get; set; }
			public Nullable<int> TotalRowCount { get; set; }

		}

}
