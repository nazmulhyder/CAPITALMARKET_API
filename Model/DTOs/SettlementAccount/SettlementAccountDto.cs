using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SettlementAccount
{
	public class SettlementAccountDto
	{

		public Nullable<int> ContractID { get; set; }

		public Nullable<int> IndexID { get; set; }
		public Nullable<int> ProductID { get; set; }
		public int? CompanyID { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountType { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? AccountStatus { get; set; }
		public string? BOCode { get; set; }
		public Nullable<int> BankAccountID { get; set; }

		public Nullable<DateTime> OpenDate { get; set; }
		public string? AccountDescription { get; set; }
		public Nullable<int> NettingOptionAgreementParamID { get; set; }
		public Nullable<int> NettingOptionAttributeID { get; set; }
		public Nullable<int> NettingOptionProductAttributeID { get; set; }
		public string? NettingOptionParamValue { get; set; }

		public Nullable<int> BuySettlementAgreementParamID { get; set; }
		public Nullable<int> BuySettlementAttributeID { get; set; }
		public Nullable<int> BuySettlementProductAttributeID { get; set; }
		public string? BuySettlementParamValue { get; set; }

		public Nullable<int> SaleSettlementAgreementParamID { get; set; }
		public Nullable<int> SaleSettlementAttributeID { get; set; }
		public Nullable<int> SaleSettlementProductAttributeID { get; set; }
		public string? SaleSettlementParamValue { get; set; }

		public string? MemberName { get; set; }
		public string? MemberCode { get; set; }
		public string? BankAccountNumber { get; set; }

	}

}
