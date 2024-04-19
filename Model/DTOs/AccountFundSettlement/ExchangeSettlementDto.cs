using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AccountFundSettlement
{
	public class ExchangeRegularSettlementSetDto
	{
		public List<ExchangeRegularSettlementDto> exchangeRegularSettlementData { get;set; }
		public PaymentDto? Payment { get; set; }
		public CollectionDto? Collection { get; set; }
	}

	public class ExchangeRegularSettlementDto
	{

		public string? TrecHoldingNo { get; set; }

		public Nullable<int> BrokerID { get; set; }
		public string? BrokerName { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public Nullable<DateTime> SettlementDate { get; set; }
		public string? InstrumentGroup { get; set; }

		public string? MARKET { get; set; }
		public Nullable<DateTime> TradeDate { get; set; }
		public Nullable<decimal> BuyAmount { get; set; }
		public Nullable<decimal> Laga { get; set; }
		public Nullable<decimal> Hawla { get; set; }
		public Nullable<int> DisbursementID { get; set; }
		public Nullable<int> CollectionInfoID { get; set; }
		public Nullable<decimal> AIT { get; set; }
		public Nullable<decimal> SellAmount { get; set; }
		public Nullable<decimal> NettedAmount { get; set; }
		public Nullable<decimal> ReceivableAmount { get; set; }
		public Nullable<decimal> PayableAmount { get; set; }

	}
	public class ExchangeSpotSettlementDto
	{
		public Nullable<DateTime> SettlementDate { get; set; }
		public Nullable<int> BrokerID { get; set; }
		public Nullable<int> ExchangeID { get; set; }
		public Nullable<int> DisbursementID { get; set; }
		public Nullable<int> CollectionInfoID { get; set; }
		public string? BrokerName { get; set; }
		public string? TrecHoldingNo { get; set; }
		public Nullable<decimal> SellAmount { get; set; }
		public Nullable<decimal> BuyAmount { get; set; }

		public PaymentDto? Payment {  get; set; }
		public CollectionDto? Collection {  get; set; }
	}

	public class CollectionDto
	{
		public Nullable<int> CollectionInfoID { get; set; }
		public Nullable<DateTime> CollectionDate { get; set; }
		public Nullable<decimal> CollectionAmount { get; set; }
		public string? EntryType { get; set; }
		public Nullable<int> MonInstrumentID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> BankAccountID { get; set; }
		public string? InstrumentType { get; set; }
		public string? InstrumentNumber { get; set; }
	}
	public class PaymentDto
	{
		public Nullable<int> DisbursementID { get; set; }

		public Nullable<DateTime> ProcessingDate { get; set; }
		public Nullable<decimal> DisburseAmount { get; set; }
		public Nullable<DateTime> DisburseDate { get; set; }
		public string? DisbursementHeadCode { get; set; }
		public Nullable<int> MonInstrumentID { get; set; }
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> BankAccountID { get; set; }
		public string? InstrumentNumber { get; set; }
		public string? InstrumentType { get; set; }
		public string? Status { get; set; }
	}
}
