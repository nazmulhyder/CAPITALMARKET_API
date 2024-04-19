using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Accounting
{
	public class VoucherBulkDto
	{
		public string? AccountNumber { get; set; }

		public string? GLCode { get; set; }
		public decimal? Amount { get; set; }

		public string? LedgerNote { get; set; }
		public string? LedgerType { get; set; }
	}
}
