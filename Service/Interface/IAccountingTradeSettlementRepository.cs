using Model.DTOs.AccountFundSettlement;
using Model.DTOs.ForeignTradeCommissionShare;
using Model.DTOs.MerchantBankAMCSettlementDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IAccountingTradeSettlementRepository
	{

		#region ForeignTradeCommissionShare
		public Task<object> MakePaymentCommissionSharing(string UserName, int CompanyID, int BranchID, int CommissionAllocationSummaryID);
		
		public Task<object> getCommissionSharingPayment(string UserName, int CompanyID, int BranchID, DateTime FromDate, DateTime ToDate, string ListType);

		public Task<object> RecalculateForeignTradeCommissionSharing(string UserName, int CompanyID, int BranchID, List<ForeignTradeCommissionShareDto> data);
		
		public Task<string> saveForeignTradeCommissionSharing(string UserName, int CompanyID, int BranchID, decimal AuditFee, decimal Tax, decimal BankFee, List<ForeignTradeCommissionShareDto> data);
		
		public Task<object> getForeignTradeListForCommissionSharing(string UserName, int CompanyID, int BranchID, DateTime FromDate, DateTime ToDate, string AccountNo);

		#endregion ForeignTradeCommissionShare



		#region IL-AML
		public Task<object> SaveUpdateBrokerTradeSettlementDetail(string UserName, int CompanyID, int BranchID, MerchantBankAMCSettlementDto data);

		public Task<object> getBrokerTradeSettlementDetail(string UserName, int CompanyID, int BranchID, DateTime SettlementDate, int ContractID);
		
		public Task<object> getBrokerTradeSettlementList(string UserName, int CompanyID, int BranchID, DateTime SettlementDate);
		#endregion IL-AML

		#region SL
		public Task<object> SaveUpdateMerchantBankAMCTradeSettlementDetail(string UserName, int CompanyID, int BranchID, MerchantBankAMCSettlementDto data);

		public Task<object> getMerchantBankAMCTradeSettlementDetail(string UserName, int CompanyID, int BranchID, DateTime SettlementDate, int ContractID);

		public Task<object> getMerchantBankAMCTradeSettlementList(string UserName, int CompanyID, int BranchID, DateTime SettlementDate);

		public Task<object> getExchangeSpotSettlementData(string UserName, int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate);

		public Task<object> getExchangeRegularSettlementData(string UserName, int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate);
		
		public Task<string> saveExchangeSpotSettlementData(string UserName, int CompanyID, int BranchID, ExchangeSpotSettlementDto data);
		
		public Task<string> saveExchangeRegularSettlementData(string UserName, int CompanyID, int BranchID, ExchangeRegularSettlementSetDto data);
		#endregion SL
	}
}
