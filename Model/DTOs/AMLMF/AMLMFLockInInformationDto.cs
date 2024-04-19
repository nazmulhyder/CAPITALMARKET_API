using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AMLMF
{
	public class AMLMFLockInInformationApproveDto
	{
        public string? LockInInformationIDs { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? ApprovalRemarks { get; set; }
	}

	public class AMLMFLockInInformationDto
	{
		public Nullable<int> LockInInformationID { get; set; }

		public Nullable<int> FundID { get; set; }
		public string? InvestmentType { get; set; }
		public Nullable<int> LockInDays { get; set; }
		public string? Remarks { get; set; }
		public string? Maker { get; set; }

		public Nullable<DateTime> MakeDate { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> ApprovalSetID { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }
		public string? Approver { get; set; }
		public bool? HasSlab { get; set; }

		public List<AMLMFLockInInformationSlabDto>? SlabList { get; set; }
	}

	public class AMLMFLockInInformationSlabDto
	{
		public Nullable<int> SlabID { get; set; } = 0;
		public Nullable<int> LockInInformationID { get; set; } = 0;
		public Nullable<int> FromDay { get; set; }
		public Nullable<int> ToDay { get; set; }
		public Nullable<decimal> LockPercentage { get; set; }
		public string? ReferenceKey { get;set; }
	}

}
