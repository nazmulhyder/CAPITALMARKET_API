using Dapper;
using Microsoft.AspNetCore.Http;
using Model.DTOs.Allocation;
using Model.DTOs.Charges;
using Model.DTOs.FundCollection;
using Model.DTOs.PriceFileUpload;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class ChargesRepository : IChargesRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public ChargesRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
        
        public async Task<List<ChargesDto>> GetChargeList(string UserName, int CompanyID, int BranchID, string ListType, string ManualEntryEnable, string AdjustmentEnable)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", ListType);
            sqlParams[4] = new SqlParameter("@AttributeID", "0");
            sqlParams[5] = new SqlParameter("@ManualEntryEnable", ManualEntryEnable);
            sqlParams[6] = new SqlParameter("@AdjustmentEnable", AdjustmentEnable);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListFeesAndCharges]", sqlParams);

            var ChargeList = CustomConvert.DataSetToList<ChargesDto>(DataSets.Tables[0]).ToList();

            return ChargeList;
        }
        
        public async Task<ChargeSetupDto> GetChargeDetail(string UserName, int CompanyID, int BranchID, int AttributeID)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", "all");
            sqlParams[4] = new SqlParameter("@AttributeID", AttributeID);
            sqlParams[5] = new SqlParameter("@ManualEntryEnable", "all");
            sqlParams[6] = new SqlParameter("@AdjustmentEnable", "all");

            var DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListFeesAndCharges]", sqlParams);

           
            SqlParameter[] sqlParams1 = new SqlParameter[4];

            sqlParams1[0] = new SqlParameter("@UserName", UserName);
            sqlParams1[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams1[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams1[3] = new SqlParameter("@AttributeID", AttributeID);

            var DataSets2 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListProductByAttributeID]", sqlParams1);

            ChargeSetupDto chargeSetupDto = new ChargeSetupDto
            {
                charge = CustomConvert.DataSetToList<ChargesDto>(DataSets1.Tables[0]).FirstOrDefault(),
                products = CustomConvert.DataSetToList<ProductAttributeDto>(DataSets2.Tables[0]).ToList()
        };


            return chargeSetupDto;
        }

        public async Task<string> SaveUpdateChargeDetail(string UserName, int CompanyID, int BranchID, decimal ChargeAmount, ChargeSetupDto data)
        {
            List<ChargesDto> charges = new List<ChargesDto>();
            charges.Add(data.charge);

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeAmount", ChargeAmount);
            SpParameters.Add("@AttributeID", data.charge.AttributeID == null ? 0 : data.charge.AttributeID);
            SpParameters.Add("@ListCharge", ListtoDataTableConverter.ToDataTable(charges).AsTableValuedParameter("Type_FeesAndCharges"));
            SpParameters.Add("@ListProduct", ListtoDataTableConverter.ToDataTable(data.products).AsTableValuedParameter("Type_FeesAndChargesProduct"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateFeesAndCharges", SpParameters);
        }

        public async Task<string> ChargeApproval(string UserName, int CompanyID, int BranchID, ChargeApprovalDto data)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AttributeID", data.AttributeID);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveFeesAndCharges", SpParameters);
        }

        public async Task<object> AccrualChargeFileValidateUpload(string UserName, int CompanyID, int BranchID, IFormCollection data)
        {

            List<GenAccrualChargeFileUploadDto> genAccrualChargeFileUploadDtos = JsonConvert.DeserializeObject<List<GenAccrualChargeFileUploadDto>>(data["TransactionList"]);


            foreach (var item in genAccrualChargeFileUploadDtos)
            {
                if(item.ChargeRate != null)
                item.ChargeRate = item.ChargeRate.Replace("%", "");
            }

            SqlParameter[] sqlParams = new SqlParameter[7];

            string OperationType = data["OperationType"].ToString();
            sqlParams[0] = new SqlParameter("@UserName", UserName.ToString());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[2] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[3] = new SqlParameter("@FileName", data["FileName"].ToString());
            sqlParams[4] = new SqlParameter("@FileSizeInKB", (Convert.ToInt32(data["FileSize"]) /1000).ToString());
            sqlParams[5] = new SqlParameter("@OperationType", OperationType);
            sqlParams[6] = new SqlParameter("@ListTransaction", ListtoDataTableConverter.ToDataTable(genAccrualChargeFileUploadDtos));

            var DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertValidatedAccrualChargeFile]", sqlParams);

            if (data["OperationType"] == "validation")
            {
                var Result = new
                {
                    AttributeList = DataSets1.Tables[0],
                    TransactionList = DataSets1.Tables[1]
                };

                return Result;
            }
            else
            {
                var Result = DataSets1.Tables[0].Rows[0][0].ToString();

                return Result;
            }
           
        }

        public async Task<object> ListAccrualChargeFile(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName.ToString());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[2] = new SqlParameter("@BranchID", BranchID.ToString());

            var DataSets1 = new DataSet();

            if(CompanyID == 4)
			 DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeFile]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeFileIL]", sqlParams);

			List<GenAccrualChargeFileUploadModel> FileList = CustomConvert.DataSetToList<GenAccrualChargeFileUploadModel>(DataSets1.Tables[0]);

            return FileList;
        }
        
        public async Task<object> ListAccrualChargeFileDetail(int CompanyID, int BranchID, int ChargeFileID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@ChargeFileID", ChargeFileID.ToString());

            var DataSets1 = new DataSet();
            if(CompanyID==4)
             DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeFileDetail]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeFileDetailIL]", sqlParams);
			


			return new
            {
                TransactionList = DataSets1.Tables[0]
            };
        }
        
        public async Task<string> AccrualChargeFileApproval(string UserName, int CompanyID, int BranchID, AccrualChargeFileApprovalDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeFileID", data.ChargeFileID);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID == 4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccrualChargeFile", SpParameters);
            else
				return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccrualChargeFileIL", SpParameters);

		}

		public async Task<object> GetAccrualAccountList(string UserName, int CompanyID, int BranchID, AccrualAccountFilterDto data)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];

            sqlParams[0] = new SqlParameter("@UserName", UserName.ToString());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[2] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[3] = new SqlParameter("@AttributeID", data.AttributeID.ToString());
            sqlParams[4] = new SqlParameter("@AccruedDateFrom", Utility.DatetimeFormatter.DateFormat(data.AccruedDateFrom));
            sqlParams[5] = new SqlParameter("@AccruedDateTo", Utility.DatetimeFormatter.DateFormat(data.AccruedDateTo));
            sqlParams[6] = new SqlParameter("@Listtype", data.ListType);
            sqlParams[7] = new SqlParameter("@AccountNumber", data.AccountNumber);

            var DataSets1 = new DataSet();
            if(CompanyID==4)
             DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeAccount]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualChargeAccountIL]", sqlParams);
			
            return new
            {
                ChargeAccountList = DataSets1.Tables[0]
            };
        }

        public async Task<string> ApproveAccruedChargeSchedule(string UserName, int CompanyID, int BranchID, AccrualChargeApprovalDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeScheduleIDs", data.ChargeScheduleIDs);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("ApproveAccruedChargeSchedule", SpParameters);
            else
				return await _dbCommonOperation.InsertUpdateBySP("ApproveAccruedChargeScheduleIL", SpParameters);
		}

        public async Task<object> GetCLientInfoForManualChargeEntry(string UserName, int CompanyID, int BranchID, string AccountNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSets1 = new DataSet();

            if(CompanyID == 4)
			DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ClientInfoForManualChargeEntry]", sqlParams);
            else if(CompanyID == 3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ClientInfoForManualChargeEntryIL]", sqlParams);
			

			return CustomConvert.DataSetToList<ManualChargeDto>(DataSets1.Tables[0]).FirstOrDefault();
        }

        public async Task<string> ManualChargeEntry(string UserName, int CompanyID, int BranchID, ManualChargeDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ContractID", data.ContractID);
            SpParameters.Add("@AttributeID", data.AttributeID);
            SpParameters.Add("@ChargeAmount", data.ChargeAmount);
            SpParameters.Add("@TransactionDate", data.TransactionDate);
            SpParameters.Add("@Remarks", data.Remarks);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertManualCharge", SpParameters);
            else
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertManualChargeIL", SpParameters);
        }

        public async Task<object> ListManualCharge(string UserName, int CompanyID, int BranchID, string ListType)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@ListType", ListType);

            var DataSets1 = new DataSet();
            if(CompanyID==4)
            DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListManualCharge]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListManualChargeIL]", sqlParams);

			return DataSets1.Tables[0];
        }

        public async Task<string> ManualChargeApprove(string UserName, int CompanyID, int BranchID, ManualChargeApproveDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeTransactionIDs", data.ChargeTransactionIDs);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveManualCharge", SpParameters);
            else
				return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveManualChargeIL", SpParameters);
		}
        
        public async Task<object> BulkManualChargeEntryValidation(string UserName, int CompanyID, int BranchID, IFormCollection data)
        {

            List<ManualChargeBulkExcelDto> Bulk = JsonConvert.DeserializeObject<List<ManualChargeBulkExcelDto>>(data["ChargeList"]);

            SqlParameter[] sqlParams = new SqlParameter[6];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[2] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[3] = new SqlParameter("@AttributeID", data["AttributeID"].ToString());
            sqlParams[4] = new SqlParameter("@ProductID", data["ProductID"].ToString());
            sqlParams[5] = new SqlParameter("@ChargeList", ListtoDataTableConverter.ToDataTable(Bulk));

            var DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ManualChargeValidationBulkSL]", sqlParams);

            return DataSets1.Tables[0];
        }

        public async Task<string> BulkManualChargeEntry(string UserName, int CompanyID, int BranchID, int AttributeID, int ProductID, List<ManualChargeBulkDto> data)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AttributeID", AttributeID);
            SpParameters.Add("@ProductID", ProductID);
            SpParameters.Add("@ChargeList", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_ManualChargeBulkUpload"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertManualChargeBulkSL", SpParameters);
            else
				return await _dbCommonOperation.InsertUpdateBySP("CM_InsertManualChargeBulkIL", SpParameters);
		}

        public async Task<object> ListAccountChargeForReversalEntry(string UserName, int CompanyID, int BranchID, AccountChargeForReversalEntryDto data)
        {
            data.TransactionFrom = Utility.DatetimeFormatter.DateFormat(data.TransactionFrom);
            data.TransactionTo = Utility.DatetimeFormatter.DateFormat(data.TransactionTo);

            SqlParameter[] sqlParams = new SqlParameter[6];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@AttributeID", data.AttributeID.ToString());
            sqlParams[3] = new SqlParameter("@AccountNumber", data.AccountNumber.ToString());
            sqlParams[4] = new SqlParameter("@TransactionFrom", data.TransactionFrom.ToString());
            sqlParams[5] = new SqlParameter("@TransactionTo", data.TransactionTo.ToString());

            var DataSets1 = new DataSet();
            if(CompanyID==4)
            DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountChargeSL]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountChargeIL]", sqlParams);

			return DataSets1.Tables[0];
        }

        public async Task<string> AccountChargeReversalEntry(string UserName, int CompanyID, int BranchID, AccountChargeReversalEntryDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeTransactionIDs", data.ChargeTransactionIDs);
            SpParameters.Add("@Remarks", data.Remarks);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountChargeReversalSL", SpParameters);
            else
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountChargeReversalIL", SpParameters);
        }

        public async Task<object> AccountChargeReversalList(string UserName, int CompanyID, int BranchID, AccountChargeReversalDto data)
        {
            data.TransactionFrom = Utility.DatetimeFormatter.DateFormat(data.TransactionFrom);
            data.TransactionTo = Utility.DatetimeFormatter.DateFormat(data.TransactionTo);

            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@ListType", data.ListType.ToString());
            sqlParams[3] = new SqlParameter("@AttributeID", data.AttributeID.ToString());
            sqlParams[4] = new SqlParameter("@AccountNumber", data.AccountNumber.ToString());
            sqlParams[5] = new SqlParameter("@TransactionFrom", data.TransactionFrom.ToString());
            sqlParams[6] = new SqlParameter("@TransactionTo", data.TransactionTo.ToString());

            var DataSets1 = new DataSet();
            if(CompanyID==4)
            DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountChargeReversalSL]", sqlParams);
            else if(CompanyID==3)
				DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountChargeReversalIL]", sqlParams);

			return DataSets1.Tables[0];
        }

        public async Task<string> AccountChargeReversalApprove(string UserName, int CompanyID, int BranchID, AccountChargeReversalEntryDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeTransactionIDs", data.ChargeTransactionIDs);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.Remarks);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountChargeReversalSL", SpParameters);
            else
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountChargeReversalIL", SpParameters);
        }
        
        public async Task<string> ChargeAdjustmentEntry(string UserName, int CompanyID, int BranchID, ManualChargeDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ContractID", data.ContractID);
            SpParameters.Add("@AttributeID", data.AttributeID);
            SpParameters.Add("@ChargeAmount", data.ChargeAmount);
            SpParameters.Add("@TransactionDate", data.TransactionDate);
            SpParameters.Add("@Remarks", data.Remarks);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertChargeAdjustment", SpParameters);
            else
            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertChargeAdjustmentIL", SpParameters);
        }

        public async Task<object> ListChargeAdjustment(string UserName, int CompanyID, int BranchID, string ListType)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID.ToString());
            sqlParams[1] = new SqlParameter("@BranchID", BranchID.ToString());
            sqlParams[2] = new SqlParameter("@ListType", ListType);

            var DataSets1 = new DataSet();
            if(CompanyID==4)
            DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListChargeAdjustment]", sqlParams);
            else
            DataSets1 = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListChargeAdjustmentIl]", sqlParams);

            return DataSets1.Tables[0];
        }

        public async Task<string> ChargeAdjustmentApprove(string UserName, int CompanyID, int BranchID, ManualChargeApproveDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ChargeTransactionIDs", data.ChargeTransactionIDs);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            if(CompanyID==4)
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveChargeAdjustment", SpParameters);
            else
            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveChargeAdjustmentIL", SpParameters);
        }
    }
}
