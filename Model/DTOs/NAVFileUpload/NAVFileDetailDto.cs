using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.NAVFileUpload
{
	public class NAVFileDetailDto
	{

		public Nullable<int> NAVFileDetailID { get; set; }

		public Nullable<int> NAVFileID { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? ISIN { get; set; }
		public string? InstrumentName { get; set; }
		public Nullable<decimal> NAV { get; set; }
		public string? EffectiveDateFrom { get; set; }
		public string? EffectiveDateTo { get; set; }
	}
	public class NAVFileApproveDto
	{

		public string? NAVFileIDs { get; set; }
		public string? ApprovalStatus { get; set; }
		public string? ApprovalRemark { get; set; }
	
	}


}
