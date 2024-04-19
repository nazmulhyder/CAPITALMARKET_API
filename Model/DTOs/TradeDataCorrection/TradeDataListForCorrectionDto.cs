using Model.DTOs.TradeFileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeDataCorrection
{
    public class TradeDataListForCorrectionDto
    {
		
		public Nullable<int> TradeDetailID { get; set; }
		public string? MemberCode { get; set; }

		public string? MemberName { get; set; }
		public string? MemberType { get; set; }
		public Nullable<int> ContractID { get; set; }
		public string? AccountNumber { get; set; }
		public string? NewAccountNumber { get; set; }
		public string? TradingCode { get; set; }

		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public string? ExchangeName { get; set; }
		public Nullable<DateTime> TradeDate { get; set; }

		public Nullable<int> InstrumentID { get; set; }
		public string? InstrumentName { get; set; }
		public Nullable<int> InstrumentTypeID { get; set; }
		public string? InstrumentType { get; set; }
		public Nullable<int> CategoryID { get; set; }

		public string? Category { get; set; }
		public Nullable<decimal> Quantity { get; set; }
		public Nullable<decimal> UnitPrice { get; set; }
		public string? TradeType { get; set; }
		public Nullable<int> MarketID { get; set; }

		public string? MarketName { get; set; }
		public Nullable<decimal> CommissionRate { get; set; }
		public Nullable<decimal> CommissionAmount { get; set; }
		public Nullable<decimal> Hawla { get; set; }
		public Nullable<decimal> Laga { get; set; }

		public Nullable<decimal> AIT { get; set; }
	}
	public class Type_TradeCorrectionLogDto
	{
		public Nullable<int> TradeDetailID { get; set; }
	
		public Nullable<int> ContractID { get; set; }
		public string? AccountNumber { get; set; }
		public string? NewAccountNumber { get; set; }
		
		public Nullable<DateTime> TradeDate { get; set; }

	}

	public class ApproveTradeCorrectionDataDto
	{
		public List<Type_TradeCorrectionLogDto>? TradeData { get; set; }
		public string? ApprovalRemark { get; set; }
		public bool? IsApproved { get; set; }
	}

	public class TradeFileReversalRequest
	{
		public List<TradeSummaryDto>? TradeFiles { get; set; }
		public string? ApprovalRemark { get; set; }
		public int? CompanyID { get; set; }
		public int? BranchID { get; set; }
	}

	public class ApproveTradeFileReversalRequest
	{
		public string? ApprovalRemark { get; set; }
		public int? TradeFileID { get; set; }
		public int? CompanyID { get; set; }
		public int? BranchID { get; set; }
	}
}
