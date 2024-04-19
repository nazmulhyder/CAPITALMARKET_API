using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.VoucherTemplate
{

public class VoucherTempleteLinkDto
	{
		public Nullable<int> LinkID { get; set; }

		public Nullable<int> VoucherTempleteID { get; set; }
		public Nullable<int> AccountID { get; set; }
		public Nullable<int> CounterVchrTempleteID { get; set; }
		public string? SelectionKeyName { get; set; }
		public Nullable<int> SelectionKeyID { get; set; }

		public Nullable<int> ApprovalSetID { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }

	
		public string? VoucherTitle { get; set; }
		
	}


public class VoucherLedgerheadDto
	{

		public Nullable<int> LedgerHeadID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public Nullable<int> COAID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }

		public string? ControlHead { get; set; }
		public string? ParentGLCode { get; set; }
		public int? AccLevel { get; set; }

		public List<VoucherTemplateDto>? Templates { get; set; }
	}

	public class VoucherTemplateDto
	{
		public Nullable<int> VoucherTempleteID { get; set; }

		public Nullable<int> LedgerHeadID { get; set; }
		public Nullable<Boolean> IsDebited { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public Nullable<Boolean> IsPercentage { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }

		public string? ApprovalStatus { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
	}
}
