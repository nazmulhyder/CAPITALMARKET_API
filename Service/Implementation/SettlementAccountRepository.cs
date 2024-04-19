using AutoMapper;
using Dapper;
using Model.DTOs.AccountFundSettlement;
using Model.DTOs.SettlementAccount;
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
	public class SettlementAccountRepository : ISettlementAccountRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;
		public IMapper mapper;
		public SettlementAccountRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
		{
			_dbCommonOperation = dbCommonOperation;
			mapper = _mapper;
		}

		public async Task<object> SaveUpdateSettlementAccount(string UserName, int CompanyID, int BranchID, SettlementAccountDto data)
		{

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@IndexID", data.IndexID);
			SpParameters.Add("@ProductID", data.ProductID);
			SpParameters.Add("@AccountNumber", data.AccountNumber);
			SpParameters.Add("@BankAccountID", data.BankAccountID);

			SpParameters.Add("@NettingOptionAgreementParamID", data.NettingOptionAgreementParamID);
			SpParameters.Add("@NettingOptionProductAttributeID", data.NettingOptionProductAttributeID);
			SpParameters.Add("@NettingOptionAttributeID", data.NettingOptionAttributeID);
			SpParameters.Add("@NettingOptionParamValue", data.NettingOptionParamValue);

			SpParameters.Add("@BuySettlementAgreementParamID", data.BuySettlementAgreementParamID);
			SpParameters.Add("@BuySettlementProductAttributeID", data.BuySettlementProductAttributeID);
			SpParameters.Add("@BuySettlementAttributeID", data.BuySettlementAttributeID);
			SpParameters.Add("@BuySettlementParamValue", data.BuySettlementParamValue);

			SpParameters.Add("@SaleSettlementAgreementParamID", data.SaleSettlementAgreementParamID);
			SpParameters.Add("@SaleSettlementProductAttributeID", data.SaleSettlementProductAttributeID);
			SpParameters.Add("@SaleSettlementAttributeID", data.SaleSettlementAttributeID);
			SpParameters.Add("@SaleSettlementParamValue", data.SaleSettlementParamValue);
			
			SpParameters.Add("@SaleSettlementParamValue", data.SaleSettlementParamValue);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateSettlementAccount", SpParameters);
		}

		public async Task<object> GetSettlementAccountList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);
		
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListSettlementAccount]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> GetProductSettlementAccount(string UserName, int CompanyID, int BranchID, int ProductID, int ContractID)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ProductID", ProductID);
			sqlParams[4] = new SqlParameter("@ContractID", ContractID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_getSettlementAccountDetail]", sqlParams);

			return new
			{
				Account = CustomConvert.DataSetToList<SettlementAccountDto>(DataSets.Tables[0]).FirstOrDefault(),
				NettingOption = DataSets.Tables[1],
			};
		}
	}
}
