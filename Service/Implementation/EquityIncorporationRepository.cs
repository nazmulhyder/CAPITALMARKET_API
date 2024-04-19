using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs.EquityIncorporation;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.FundCollection;
using Utility;

namespace Service.Implementation
{

    public class EquityIncorporationRepository : IEquityIncorporationRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IConfiguration _configuration;
        public EquityIncorporationRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            this._configuration = configuration;
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<object> ListCollectionForEquityAddition(string UserName, int CompanyID, int BranchID, string AccountNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@AccountNumber", AccountNo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCollectionForEquityIncorporation]", sqlParams);


            var Result = new
            {
                AccountInfo = CustomConvert.DataSetToList<ClientInfoDto>(DataSets.Tables[0]).FirstOrDefault(),
                CompanyEquityAddition = DataSets.Tables[1]
            };

            return Result;
        }
        public async Task<object> ListEquityAddition(string UserName, int CompanyID, int BranchID, string Status)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@Status", Status);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListEquityIncorporation]", sqlParams);


            var Result = new
            {
                EquityAdditionList = DataSets.Tables[0]
            };

            return Result;
        }

        public async Task<string> EquityIncorporationApprove(string UserName, int CompanyID, int BranchID, EquityAdditionApprovalDto data)
        {
            if(data.ComEquityAddID != null && data.ComEquityAddID > 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ComEquityAddID", data.ComEquityAddID);
                SpParameters.Add("@@ComEquityDedID", data.ComEquityDedID);
                SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("ApproveEquityIncorporation", SpParameters);
            }
            else
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@@ComEquityDedID", data.ComEquityDedID);
                SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("ApproveEquityDeduction", SpParameters);
            }
            
        }

        public async Task<string> InsertEquityAddition(string UserName, int CompanyID, int BranchID, EquityAdditionDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@CollectionInfoID", data.CollectionInfoID);
            SpParameters.Add("@DebitContractID", data.ContractID);
            SpParameters.Add("@CreditContractID", data.CreditContractID);
            SpParameters.Add("@ComEquityPercentage", data.ComEquityPercentage);
            SpParameters.Add("@ComEquityAmount", data.ComEquityAmount);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("InsertEquityIncorporation", SpParameters);
        }


        public async Task<object> ClientInfoForEquityDeduction(string UserName, int CompanyID, int BranchID, string AccountNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@AccountNumber", AccountNo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ClientInfoForEquityDeduction]", sqlParams);


            return CustomConvert.DataSetToList<EquityDeductionDto>(DataSets.Tables[0]).FirstOrDefault();
        }


        public async Task<string> SaveEquityDeduction(string UserName, int CompanyID, int BranchID, EquityDeductionDto deductionDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@CustomerContractID", deductionDto.ContractID);
            SpParameters.Add("@ComEquityPercentage", deductionDto.ComEquityPercentage);
            SpParameters.Add("@ComEquityAmount", deductionDto.AmountTobeDeducted);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[InsertEquityDeduction]", SpParameters);
        }

        public async Task<object> ListEquityDeduction(string UserName, int CompanyID, int BranchID, string Status)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@Status", Status);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListEquityDeduction]", sqlParams);


            return DataSets.Tables[0];
        }
    }
}
