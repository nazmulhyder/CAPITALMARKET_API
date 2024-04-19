using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AMLMF
{
    
	public class AMLMFNAVProcessingSummaryDto
	{
        public bool? IsNavProcessed { get; set; }
        public string? TransactionDate { get; set; }
        public string? EffectiveDateFrom { get; set; }
        public string? EffectiveDateTo { get; set; }
        public int? FundID { get; set; }
        public string? PrevNAVProcessDate { get; set; }
        public decimal? PrevNAVatMarket { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }
        public string? Approver { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
        public string? ApprovalRemark { get; set; }
        public decimal? TotalAsset { get; set; }
        public decimal? TotalLiability { get; set; }
        public decimal? FundSize { get; set; }
        public decimal? FaceValue { get; set; }
        public decimal? TotalUnit { get; set; }
        public decimal? NAVatCost { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? NAVPerUnitatMarket { get; set; }
        public decimal? NAVPerUnitAtCost { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SurrenderPrice { get; set; }
        public decimal? TotalEquity { get; set; }
        public decimal? TotalIncomeOverExpense { get; set; }
        public decimal? Control { get; set; }

        public List<AMLMFNAVProcessingDetailTreeDto>? LedgerHeadList { get; set; }
	}

	public class AMLMFNAVProcessingDetailDto
	{
		public int? COAID { get; set; }
		public string? ParentGLCode { get; set; }
		public int? AccLevel { get; set; }
		public int? LedgerHeadID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public decimal? PrevClosingBalance { get; set; }
		public decimal? BalanceChange { get; set; }
		public decimal? ClosingBalance { get; set; }

		public string? ParentGLCode1 { get; set; }
		public string? ParentGLCode2 { get; set; }
		public string? ParentGLCode3 { get; set; }
		public string? ParentGLCode4 { get; set; }

		public string? ParentGLName1 { get; set; }
		public string? ParentGLName2 { get; set; }
		public string? ParentGLName3 { get; set; }
		public string? ParentGLName4 { get; set; }
		public string? RootGLCode { get; set; }

	}

	public class AMLMFNAVProcessingDetailTreeDto
	{
		public int? COAID { get; set; }
		public string? ParentGLCode { get; set; }
		public int? AccLevel { get; set; }
		public int? LedgerHeadID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public decimal? PrevClosingBalance { get; set; }
		public decimal? BalanceChange { get; set; }
		public decimal? ClosingBalance { get; set; }

		public List<AMLMFNAVProcessingDetailTreeDto>? subRows { get; set; }
	}


}
