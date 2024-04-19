using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AMLMF
{

	public class AMLMFAmortizationSetupApproveDto
	{
		public string? MFASetupIDs { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? ApprovalRemark { get; set; }
	}

	public class AMLMFAmortizationSetupDto
	{

		public Nullable<int> MFASetupID { get; set; } = 0;

		public string? ReferenceNo { get; set; }
		public Nullable<int> FundID { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? AmortizationStartDate { get; set; }

		public string? AmortizationEndDate { get; set; }
		public Nullable<decimal> TotalAmount { get; set; }
		public string? LastAmortizationOn { get; set; }
		public Nullable<decimal> AmortizationCompleted { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }

		public string? ApprovalStatus { get; set; }
		public Nullable<Boolean> IsDSEShariahIndex { get; set; }
		public List<AMLMFAmortizationSetuPdETAILDto>? DetailList { get; set; }	
	}

	public class AMLMFAmortizationSetuPdETAILDto
	{
		public Nullable<int> FundID { get; set; }

		public Nullable<int> AccountID { get; set; }
		public string? AccountHead { get; set; }
		public Nullable<int> MFASetupDetlID { get; set; }
		public Nullable<int> MFASetupID { get; set; }
		public Nullable<decimal> Amount { get; set; }

		public Nullable<Boolean> IsDebited { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
	}
}
