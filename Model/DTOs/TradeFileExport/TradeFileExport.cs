using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeFileExport
{
    public class PayInOutFileContentDto
    {
        public string? FileName { get; set; }
        public PayInOutFileContentFirstRowDto FirstRow { get; set; }
		public List<PayInOutFileContentBodyDto> payInOutFileBodyContent { get; set; }
	}
	public class PayInOutFileContentFirstRowDto
	{
		public string? TotalCount { get; set; }
		public string? TotalQuantity { get; set; }
		public string? DPCode { get; set; }
		public string? ExchangeCode { get; set; }
	}
	public class PayInOutFileContentBodyDto
	{
		public string? DateString { get; set; }
		public string? BrokerBO { get; set; }
		public string? ClientBO { get; set; }
		public string? TracNo { get; set; }
		public string? ISIN { get; set; }
		public string? Quantity { get; set; }
		public string? FileType { get; set; }
		public string? AccountNumber { get; set; }
		public string? RowNumber { get; set; }
	}

	public class SLAgrGrpTradeExportDto
    {
        public string? AgrGrpID { get; set; }

        public string? GroupName { get; set; }
        public string? FolderName { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }

        public List<SLAgrGrpMemberDto> AccountList { get;set; }
    }

    public class SLAgrGrpMemberDto
    {
        public Nullable<int> AgrGrpMemberID { get; set; }

        public string? AgrGrpID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public string? DeleteStatus { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberCode { get; set; }

        public string? MemberName { get; set; }
        public string? MemberType { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
    }

}
