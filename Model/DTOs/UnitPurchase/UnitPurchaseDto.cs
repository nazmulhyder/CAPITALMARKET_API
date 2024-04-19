using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.UnitPurchase
{
	public class UnitPurchaseDetailDto
	{
		public Nullable<int> PurchaseDetailID { get; set; }

		public Nullable<int> FundID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public string? AccountNumber { get; set; }

		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? BOCode { get; set; }
		public Nullable<decimal> CurrentNav { get; set; }
		public Nullable<int> InstrumentID { get; set; }

		public Nullable<int> PurchaseUnit { get; set; }
		public Nullable<int> LotSize { get; set; }
		public Nullable<int> SalesRMID { get; set; }
		public string? SaleType { get; set; }
		public Nullable<decimal> UnclearCollectionAmount { get; set; }

		public Nullable<decimal> AvailableBalance { get; set; }
		public Nullable<decimal> PurchaseOrderAmount { get; set; }
		public Nullable<decimal> LedgerBalance { get; set; }
		public Nullable<int> TotalRowCount { get; set; }
		
	}

	public class UnitPurchaseDto
	{

		public Nullable<int> FundID { get; set; }

		public Nullable<int> ContractID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public string? AccountNumber { get; set; }
		public string? MemberCode { get; set; }

		public string? MemberName { get; set; }
		public string? BOCode { get; set; }
		public Nullable<decimal> CurrentNav { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? SaleType { get; set; }
		public string? SaleNo { get; set; }
		public Nullable<int> SalesRMID { get; set; }
		public Nullable<int> PurchaseUnit { get; set; }
		public Nullable<int> LotSize { get; set; }
		public Nullable<decimal> UnclearCollectionAmount { get; set; }
		public Nullable<decimal> AvailableBalance { get; set; }
		public Nullable<decimal> PurchaseOrderAmount { get; set; }
		public Nullable<decimal> LedgerBalance { get; set; }
	}
}
