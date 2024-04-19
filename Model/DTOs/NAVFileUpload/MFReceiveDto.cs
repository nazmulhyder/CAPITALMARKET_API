using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.NAVFileUpload
{

	public class MFReceiveMasterDto
	{
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberCode { get; set; }
        public string? MemberName { get; set; }
        public string? ProductName { get; set; }
        public int? ProductID { get; set; }
		public List<MFReceiveDetailDto>? InstrumentList { get; set; }
    }

	public class MFReceiveDetailDto
	{
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? InstrumentName { get; set; }
		public Nullable<int> InstrumentTypeID { get; set; }
		public string? InstrumentType { get; set; }
		public Nullable<decimal> NetAssetValue { get; set; }
		public string? ListingStatus { get; set; }
		public Nullable<int> TotalQuantity { get; set; }
		public Nullable<decimal> TotalCost { get; set; }
		public Nullable<DateTime> TransactionDate { get; set; }
		public Nullable<decimal> AveragePrice { get; set; }
		public Nullable<int> ReceiveQuantity { get; set; }
		public Nullable<decimal> Purchaseprice { get; set; }
		public Nullable<decimal> ReceiveTotalCost { get; set; }
		public Nullable<int> MFUnitReceiveID { get; set; }
	}

}
