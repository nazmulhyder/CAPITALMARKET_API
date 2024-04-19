using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.DTOs.Approval;
using Model.DTOs.Audit;
using Model.DTOs.FDR;
using Model.DTOs.InternalFundTransfer;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class InternalFundTransferRepository : IInternalFundTransferRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        public InternalFundTransferRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
        }

        public async Task<object> GetCustomerAvailableBalanceInfo(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            SqlParameter[] Params = new SqlParameter[5];

            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ProductID", ProductID);
            Params[4] = new SqlParameter("@AccountNo", AccountNo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAvailableBalanceInfoIL]", Params);

            return new
            {
                CustomerInfo =    CustomConvert.DataSetToList<CustomerAvailableBalanceInfo>(DataSets.Tables[0]).FirstOrDefault(),
                RelatedAccounts = DataSets.Tables[1]
            };
        }

        public async Task<string> InsertUpdateInternalFundTransfer(int CompanyID, int BranchID, string userName, InternalFundTransferDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateInternalFundTransferIL";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IntFundTransferID", 0);
                SpParameters.Add("@DebitContractID", entry.DebitContractID);
                SpParameters.Add("@CreditContractID", entry.CreditContractID);
                SpParameters.Add("@TransferAmount", entry.TransferAmount);
                SpParameters.Add("@TransferRemarks", entry.TransferRemarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetListInternalFundTransfer(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InternalFundTransferListIL]", Params);

            return new
            {
                Result = DataSets.Tables[0]
            };
        }

        public async Task<string> InternalFundTransferApproval(string userName, int CompanyID, int branchID, InternalFundTransferApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@IntFundTransferIDs", approvalDto.IntFundTransferIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveInternalFundTransferIL", SpParameters);
        }
    }
}
