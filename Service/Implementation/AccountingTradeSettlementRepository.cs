using AutoMapper;
using Dapper;
using Model.DTOs.AccountFundSettlement;
using Model.DTOs.CoA;
using Model.DTOs.ForeignTradeCommissionShare;
using Model.DTOs.MerchantBankAMCSettlementDto;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
	public class AccountingTradeSettlementRepository : IAccountingTradeSettlementRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;
		public IMapper mapper;
		public AccountingTradeSettlementRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
		{
			_dbCommonOperation = dbCommonOperation;
			mapper = _mapper;
		}


		#region ForeignTradeCommissionShare

		public async Task<object> MakePaymentCommissionSharing(string UserName, int CompanyID, int BranchID, int CommissionAllocationSummaryID)
		{

			SqlParameter[] sqlParams = new SqlParameter[4];
			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@CommissionAllocationSummaryID", CommissionAllocationSummaryID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_MakePaymentCommissionSharing]", sqlParams);

			return DataSets.Tables[0].Rows[0][0].ToString();
		}


		
		public async Task<object> getCommissionSharingPayment(string UserName, int CompanyID, int BranchID, DateTime FromDate, DateTime ToDate, string ListType)
		{

			SqlParameter[] sqlParams = new SqlParameter[4];
			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListForeignBrokerCommissionPayable]", sqlParams);

			return DataSets.Tables[0];
		}



		public async Task<string> saveForeignTradeCommissionSharing(string UserName, int CompanyID, int BranchID, decimal AuditFee, decimal Tax, decimal BankFee, List<ForeignTradeCommissionShareDto> data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@DataList", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_ForeignTradeCommissionShareRecalculate"));
			SpParameters.Add("@AuditFee", AuditFee);
			SpParameters.Add("@Tax", Tax);
			SpParameters.Add("@BankFee", BankFee);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_insertForeignTradeCommissionSharing", SpParameters);
		}


		
		public async Task<object> RecalculateForeignTradeCommissionSharing(string UserName, int CompanyID, int BranchID, List<ForeignTradeCommissionShareDto> data)
		{


			SqlParameter[] sqlParams = new SqlParameter[4];
			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			
			sqlParams[3] = new SqlParameter("@DataList", ListtoDataTableConverter.ToDataTable(data));

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListnForeignTradeAllocationForCommissionRecalculate]", sqlParams);

			return DataSets.Tables[0];


		}

	
		public async Task<object> getForeignTradeListForCommissionSharing(string UserName, int CompanyID, int BranchID, DateTime FromDate, DateTime ToDate, string AccountNo)
		{

			SqlParameter[] sqlParams = new SqlParameter[6];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);

			sqlParams[3] = new SqlParameter("@FromDate", FromDate);
			sqlParams[4] = new SqlParameter("@ToDate", ToDate);
			sqlParams[5] = new SqlParameter("@AccountNo", AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListnForeignTradeAllocationForCommissionAllocation]", sqlParams);

			return DataSets.Tables[0];

		}

		#endregion ForeignTradeCommissionShare



		#region IL-AML

		public async Task<object> SaveUpdateBrokerTradeSettlementDetail(string UserName, int CompanyID, int BranchID, MerchantBankAMCSettlementDto data)
		{
			string SettlementIDs = "";
			foreach (var item in data.SettlementData) SettlementIDs = SettlementIDs + "," + item.TradeSettlementID;

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@SettlementIDs", SettlementIDs);
			SpParameters.Add("@SettlementDate", data.SettlementDate);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@PaymentBankAccountID", data.Payment.BankAccountID);

			SpParameters.Add("@CollectionBankAccountID", data.Collection.BankAccountID);

			SpParameters.Add("@DisbursementID", data.Payment.DisbursementID == null ? 0 : data.Payment.DisbursementID);
			SpParameters.Add("@DisbursementInstrumentType", data.Payment.InstrumentType);
			SpParameters.Add("@PayableAmount", data.PayableAmount);

			SpParameters.Add("@CollectionInfoID", data.Collection.CollectionInfoID == null ? 0 : data.Collection.CollectionInfoID);
			SpParameters.Add("@CollectionInfoInstrumentType", data.Collection.InstrumentType);
			SpParameters.Add("@CollectionInfoInstrumentNumber", data.Collection.InstrumentNumber);
			SpParameters.Add("@ReceiableAmount", data.ReceivableAmount);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateBrokerForSettlement", SpParameters);
		}


		public async Task<object> getBrokerTradeSettlementDetail(string UserName, int CompanyID, int BranchID, DateTime SettlementDate, int ContractID)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@SettlementDate", SettlementDate);
			sqlParams[4] = new SqlParameter("@ContractID", ContractID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_BrokerSettlementDetail]", sqlParams);

			MerchantBankAMCSettlementDto Settlement = CustomConvert.DataSetToList<MerchantBankAMCSettlementDto>(DataSets.Tables[0]).FirstOrDefault();

			Settlement.SettlementData = CustomConvert.DataSetToList<MerchantBankAMCSettlementDataDto>(DataSets.Tables[1]).ToList();

			Settlement.Payment = CustomConvert.DataSetToList<MerchantBankAMCSettlementPaymentDto>(DataSets.Tables[2]).FirstOrDefault();

			Settlement.Collection = CustomConvert.DataSetToList<MerchantBankAMCSettlementCollectionDto>(DataSets.Tables[3]).FirstOrDefault();


			return Settlement;

		}


		public async Task<object> getBrokerTradeSettlementList(string UserName, int CompanyID, int BranchID, DateTime SettlementDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@SettlementDate", SettlementDate);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBrokerTradeSettlement]", sqlParams);

			return DataSets.Tables[0];
		}

		#endregion IL-AML

		#region SL

		public async Task<object> SaveUpdateMerchantBankAMCTradeSettlementDetail(string UserName, int CompanyID, int BranchID, MerchantBankAMCSettlementDto data)
		{
			string SettlementIDs = "";
			foreach (var item in data.SettlementData) SettlementIDs = SettlementIDs + "," + item.TradeSettlementID;

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@SettlementIDs", SettlementIDs);
			SpParameters.Add("@SettlementDate", data.SettlementDate);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@BankAccountID", data.Payment.BankAccountID);

			SpParameters.Add("@DisbursementID", data.Payment.DisbursementID == null ? 0 : data.Payment.DisbursementID);
			SpParameters.Add("@DisbursementInstrumentType", data.Payment.InstrumentType);
			SpParameters.Add("@PayableAmount", data.PayableAmount);

			SpParameters.Add("@CollectionInfoID", data.Collection.CollectionInfoID == null? 0 : data.Collection.CollectionInfoID);
			SpParameters.Add("@CollectionInfoInstrumentType", data.Collection.InstrumentType);
			SpParameters.Add("@CollectionInfoInstrumentNumber", data.Collection.InstrumentNumber);
			SpParameters.Add("@ReceiableAmount", data.ReceivableAmount);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateMerchantBankAMCForSettlement", SpParameters);
		}

		public async Task<object> getMerchantBankAMCTradeSettlementDetail(string UserName, int CompanyID, int BranchID, DateTime SettlementDate, int ContractID)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@SettlementDate", SettlementDate);
			sqlParams[4] = new SqlParameter("@ContractID", ContractID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_MerchangeBankAMCSettlementDetail]", sqlParams);

			MerchantBankAMCSettlementDto Settlement = CustomConvert.DataSetToList<MerchantBankAMCSettlementDto>(DataSets.Tables[0]).FirstOrDefault();

			Settlement.SettlementData = CustomConvert.DataSetToList<MerchantBankAMCSettlementDataDto>(DataSets.Tables[1]).ToList();

			Settlement.Payment = CustomConvert.DataSetToList<MerchantBankAMCSettlementPaymentDto>(DataSets.Tables[2]).FirstOrDefault();

			Settlement.Collection = CustomConvert.DataSetToList<MerchantBankAMCSettlementCollectionDto>(DataSets.Tables[3]).FirstOrDefault();


			return Settlement;

		}

		public async Task<object> getMerchantBankAMCTradeSettlementList(string UserName, int CompanyID, int BranchID, DateTime SettlementDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@SettlementDate", SettlementDate);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListMerchangeBankAMCSettlement]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> getExchangeSpotSettlementData(string UserName, int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ExchangeID", ExchangeID);
			sqlParams[4] = new SqlParameter("@SettlementDate", SettlementDate);


			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListSpotTradeSummaryForSettlement]", sqlParams);

			ExchangeSpotSettlementDto settlement = CustomConvert.DataSetToList<ExchangeSpotSettlementDto>(DataSets.Tables[0]).FirstOrDefault();

			if (settlement == null) throw new Exception("No tradding data found");

			settlement.Payment = CustomConvert.DataSetToList<PaymentDto>(DataSets.Tables[1]).FirstOrDefault();
			settlement.Collection = CustomConvert.DataSetToList<CollectionDto>(DataSets.Tables[2]).FirstOrDefault();

			return settlement;
		}

		public async Task<object> getExchangeRegularSettlementData(string UserName, int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ExchangeID", ExchangeID);
			sqlParams[4] = new SqlParameter("@SettlementDate", SettlementDate);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListRegularTradeSummaryForSettlement]", sqlParams);

			ExchangeRegularSettlementSetDto settlement = new ExchangeRegularSettlementSetDto();

			if (settlement == null) throw new Exception("No tradding data found");

			settlement.exchangeRegularSettlementData = CustomConvert.DataSetToList<ExchangeRegularSettlementDto>(DataSets.Tables[0]).ToList();

			settlement.Payment = CustomConvert.DataSetToList<PaymentDto>(DataSets.Tables[1]).FirstOrDefault();

			settlement.Collection = CustomConvert.DataSetToList<CollectionDto>(DataSets.Tables[2]).FirstOrDefault();
		
			return settlement;
		}


		public Task<string> saveExchangeSpotSettlementData(string UserName, int CompanyID, int BranchID, ExchangeSpotSettlementDto data)
		{

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			
			SpParameters.Add("@ExchangeID", data.ExchangeID);
			SpParameters.Add("@SettlementDate", data.SettlementDate);

			SpParameters.Add("@ContractID", data.Payment.ContractID);
			SpParameters.Add("@BankAccountID", data.Payment.BankAccountID);
			
			SpParameters.Add("@DisbursementID", data.Payment.DisbursementID == null ? 0 : data.Payment.DisbursementID);
			SpParameters.Add("@DisbursementInstrumentType", data.Payment.InstrumentType);

			SpParameters.Add("@CollectionInfoID", data.Collection.CollectionInfoID == null ? 0 : data.Collection.CollectionInfoID);
			SpParameters.Add("@CollectionInfoInstrumentType", data.Collection.InstrumentType);

			SpParameters.Add("@CollectionInfoInstrumentNumber", data.Collection.InstrumentNumber);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return _dbCommonOperation.InsertUpdateBySP("CM_InsertSpotTradeSummaryForSettlement", SpParameters);
		}

		public Task<string> saveExchangeRegularSettlementData(string UserName, int CompanyID, int BranchID, ExchangeRegularSettlementSetDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ExchangeID", data.exchangeRegularSettlementData.FirstOrDefault().ExchangeID);
			SpParameters.Add("@SettlementDate", data.exchangeRegularSettlementData.FirstOrDefault().SettlementDate);

			SpParameters.Add("@ContractID", data.Payment.ContractID);
			SpParameters.Add("@BankAccountID", data.Payment.BankAccountID);

			SpParameters.Add("@DisbursementID", data.Payment.DisbursementID);
			SpParameters.Add("@DisbursementInstrumentType", data.Payment.InstrumentType);

			SpParameters.Add("@CollectionInfoID", data.Collection.CollectionInfoID);
			SpParameters.Add("@CollectionInfoInstrumentType", data.Collection.InstrumentType);

			SpParameters.Add("@CollectionInfoInstrumentNumber", data.Collection.InstrumentNumber);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return _dbCommonOperation.InsertUpdateBySP("CM_InsertRegularTradeSummaryForSettlement", SpParameters);
		}

		#endregion SL
	}
}
