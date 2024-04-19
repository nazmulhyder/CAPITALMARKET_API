using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SellSurrender
{
	public class CDBLPayinFileDto
	{

		public Nullable<int> CDBLFileID { get; set; }

		public string? CDBLFileType { get; set; }
		public Nullable<DateTime> TransactionDate { get; set; }
		public string? FileName { get; set; }
		public Nullable<int> RunningSequence { get; set; }
		public string? ControlRecord { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? FirstLine { get; set; }
		public string? SecondLine { get; set; }
		public string? ThirdLine { get; set; }

		public List<CDBLPayinFileContentDto>? ContentList { get; set; }
	}

	public class CDBLPayinFileContentDto
	{
		public Nullable<int> PayInFileContentID { get; set; }

		public Nullable<int> CDBLFileID { get; set; }
		public Nullable<int> UnitActivationID { get; set; }
		public string? FourthLine { get; set; }
	}
}
