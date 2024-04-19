using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.MerchantBankAMCSettlementDto
{

	

	public class MerchantBankAMCSettlementDto
	{
		public Nullable<int> IndexID { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public Nullable<int> ContractID { get; set; }
		public Nullable<DateTime> SettlementDate { get; set; }
		public string? AccountNumber { get; set; }
		public Nullable<decimal> ReceivableAmount { get; set; }
		public Nullable<decimal> PayableAmount { get; set; }
		public string? NettingOption { get; set; }
		public Nullable<int> ProductID { get; set; }
		public List<MerchantBankAMCSettlementDataDto>? SettlementData { get; set; }

		public MerchantBankAMCSettlementPaymentDto? Payment { get; set; }

		public MerchantBankAMCSettlementCollectionDto? Collection { get; set; }

	}

	public class MerchantBankAMCSettlementDataDto
	{
		public Nullable<int> TradeSettlementID { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public string? InstrumentGroup { get; set; }
		public string? Market { get; set; }
		public Nullable<DateTime> TradeDate { get; set; }

		public Nullable<decimal> BuyAmount { get; set; }
		public Nullable<decimal> SellAmount { get; set; }
		public Nullable<decimal> BrokerageCommission { get; set; }
		public Nullable<decimal> NettedAmount { get; set; }
		public Nullable<decimal> ReceivableAmount { get; set; }

		public Nullable<decimal> PayableAmount { get; set; }
		public Nullable<DateTime> SettlementDate { get; set; }
		public Nullable<int> DisbursementID { get; set; }
		public Nullable<int> CollectionInfoID { get; set; }
	}

	public class MerchantBankAMCSettlementPaymentDto
	{
		public Nullable<int> DisbursementID { get; set; }

		public Nullable<DateTime> ProcessingDate { get; set; }
		public Nullable<decimal> DisburseAmount { get; set; }
		public Nullable<DateTime> DisburseDate { get; set; }
		public string? DisbursementHeadCode { get; set; }
		public Nullable<int> ContractID { get; set; }

		public Nullable<int> BankAccountID { get; set; }
		public string? InstrumentNumber { get; set; }
		public string? InstrumentType { get; set; }
		public string? Status { get; set; }
		public string? PaymentStatus { get; set; }
	}

	public class MerchantBankAMCSettlementCollectionDto
	{
		public Nullable<int> CollectionInfoID { get; set; }

		public Nullable<DateTime> CollectionDate { get; set; }
		public Nullable<decimal> CollectionAmount { get; set; }
		public string? EntryType { get; set; }
		public Nullable<int> MonInstrumentID { get; set; }
		public Nullable<int> ContractID { get; set; }

		public Nullable<int> BankAccountID { get; set; }
		public string? InstrumentNumber { get; set; }
		public string? InstrumentType { get; set; }
		public string? CollectionStatus { get; set; }
	}
}
