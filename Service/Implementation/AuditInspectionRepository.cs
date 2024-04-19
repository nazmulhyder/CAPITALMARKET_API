using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model.DTOs;
using Model.DTOs.Audit;
using Model.DTOs.StockSplit;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class AuditInspectionRepository : IAuditInspectionRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        public AuditInspectionRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
        }


        #region Audit Inspection

        public async Task<object> SaveAuditInspection(string UserName, int CompanyID, int BranchID, IFormCollection formData)
        {

            string sp = "";

            DynamicParameters SpParameters = new DynamicParameters();
            List<AuditInspectedClientDto> auditInspectedClients = new JSONSerialize().GetListFromJSON<AuditInspectedClientDto>(formData["auditInspectedClients"]);
            var FileUpload = AuditInspectionFileUpload(formData);

            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AuditInspectionID", formData["AuditInspectionID"]);
            SpParameters.Add("@ReferenceNo", new Random().Next(0, 999999).ToString());
            SpParameters.Add("@AuditYear", formData["AuditYear"].ToString());
            SpParameters.Add("@AuditBranch", formData["AuditBranch"].ToString());
            SpParameters.Add("@AuditAuthority", formData["AuditAuthority"].ToString());
            SpParameters.Add("@AuditObservation", formData["AuditObservation"].ToString());
            SpParameters.Add("@ViolationRules", formData["ViolationRules"].ToString());
            SpParameters.Add("@RegulatoryImpact", formData["RegulatoryImpact"].ToString());
            SpParameters.Add("@QueryType", formData["QueryType"].ToString());
            SpParameters.Add("@Response", formData["Response"].ToString());
            SpParameters.Add("@UploadedQueryFIlePath", FileUpload[0].ToString());
            SpParameters.Add("@UploadedResponseFilePath", FileUpload[1].ToString());
            SpParameters.Add("@RegulatoryAction", formData["RegulatoryAction"].ToString());
            SpParameters.Add("@AuditorName", formData["AuditorName"].ToString());
            SpParameters.Add("@AuditPeriodFrom", Utility.DatetimeFormatter.DateFormat(formData["AuditPeriodFrom"].ToString()));
            SpParameters.Add("@AuditPeriodTo", Utility.DatetimeFormatter.DateFormat(formData["AuditPeriodTo"].ToString()));
            SpParameters.Add("@auditInspectedClients", ListtoDataTableConverter.ToDataTable(auditInspectedClients).AsTableValuedParameter("Type_AuditInspectedClient"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAuditInspectionSL", SpParameters);
        }

        public List<string> AuditInspectionFileUpload(IFormCollection formData)
        {
            string filePath = Utility.FilePath.GetAuditInspectionFileUploadURL();
            var ReferenceNo = new Random().Next(0, 9999).ToString();
            string fileFullPath = String.Empty;
            List<string> result = new List<string>();

            foreach (var item in formData.Files)
            {
                ReferenceNo = new Random().Next(0, 9999).ToString();
                fileFullPath = filePath + ReferenceNo;
                if (Path.GetExtension(item.FileName) == ".pdf") //PDF
                {
                    using (Stream fileStream = new FileStream(fileFullPath + ".pdf", FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                        result.Add(fileFullPath + ".pdf");
                    }
                }

              
                if (Path.GetExtension(item.FileName) == ".jpeg" || Path.GetExtension(item.FileName) == ".jpg" || Path.GetExtension(item.FileName) == ".png") //IMAGES
                {
                    using (Stream fileStream = new FileStream(fileFullPath + ".jpg", FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                        result.Add(fileFullPath + ".jpg");
                    }
                }

                if (Path.GetExtension(item.FileName) == ".zip") //zip
                {
                    using (Stream fileStream = new FileStream(fileFullPath + ".zip", FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                        result.Add(fileFullPath + ".zip");
                    }
                }
            }

            return result;
        }

        public async Task<object> GetObservedClientAccountSL(string UserName, int CompanyID, int BranchID, string TradingCode, int InstrumentID,  string TradingDate)
        {
            SqlParameter[] Params = new SqlParameter[6];

            Params[0] = new SqlParameter("@CompanyID", CompanyID);
            Params[1] = new SqlParameter("@BranchID", BranchID);
            Params[2] = new SqlParameter("@TradingCode", TradingCode);
            Params[3] = new SqlParameter("@InstrumentID", InstrumentID);
            Params[4] = new SqlParameter("@TradingDate", Utility.DatetimeFormatter.DateFormat(TradingDate));
            Params[5] = new SqlParameter("@UserName", UserName);
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetObservedClientAccountSL]", Params);

            return new
            {
                ObservedClientAccounts = DataSets.Tables[0]
            };
        }

        public async Task<object> GetAuditInspection(string UserName, int CompanyID, int BranchID, AuditSearchFilter filter)
        {
            SqlParameter[] Params = new SqlParameter[9];
            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@AuditYear", filter.AuditYear);
            Params[4] = new SqlParameter("@AuditBranch", filter.AuditBranch);
            Params[5] = new SqlParameter("@AuditAuthority", filter.AuditAuthority);
            Params[6] = new SqlParameter("@AuditorName", filter.AuditorName);
            Params[7] = new SqlParameter("@ClientAccount", filter.ClientAccount);
            Params[8] = new SqlParameter("@QueryType", filter.QueryType);
           
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAuditInspectionSL]", Params);

            return new
            {
                AuditInspections = DataSets.Tables[0]
            };

        }

        public async Task<object> GetAuditInspectionById(int CompanyID, int BranchID, string userName, int ReferenceNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@ReferenceNo", ReferenceNo)

            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_GetAuditInspection", sqlParams);
            //GetAuditInspection auditInspection = CustomConvert.DataSetToList<AuditInspectionDto>(DataSets.Tables[0]).FirstOrDefault();
            //auditInspection.auditInspectedClientDtos = CustomConvert.DataSetToList<AuditInspectedClientDto>(DataSets.Tables[1]).ToList();

            return new
            {
                auditInspection = DataSets.Tables[0],
                auditInspectedClients = DataSets.Tables[1]
            };

        }

        #endregion Audit Inspection


        #region Requlatory Query Entry

        public async Task<object> GetPrincipleAndJointApplicantSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            SqlParameter[] Params = new SqlParameter[5];

            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ProductID", ProductID);
            Params[4] = new SqlParameter("@AccountNo", AccountNo);
          
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetPrincipleAndJointApplicantSL]", Params);

            return new
            {
                Result = DataSets.Tables[0]
            };
        }

        public async Task<object> GetRequlatoryTWSSL(string UserName, int CompanyID, int BranchID, int ContractID, int InstrumentID, string TradingDateFrom, string TradingDateTo)
        {
            SqlParameter[] Params = new SqlParameter[7];

            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ContractID", ContractID);
            Params[4] = new SqlParameter("@InstrumentID", InstrumentID);
            Params[5] = new SqlParameter("@TradingDateFrom", Utility.DatetimeFormatter.DateFormat(TradingDateFrom));
            Params[6] = new SqlParameter("@TradingDateTo", Utility.DatetimeFormatter.DateFormat(TradingDateTo));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetRequlatoryTWSSL]", Params);

            return new
            {
                Result = DataSets.Tables[0]
            };
        }

        public async Task<object> SaveRegulatory(string UserName, int CompanyID, int BranchID, IFormCollection formData)
        {

            string sp = "";

            DynamicParameters SpParameters = new DynamicParameters();
            var FileUpload = AuditInspectionFileUpload(formData);


            SpParameters.Add("@UserName", UserName.ToString());
            SpParameters.Add("@CompanyID", CompanyID.ToString());
            SpParameters.Add("@BranchID", BranchID.ToString());
            SpParameters.Add("@RegulatoryQueryID", formData["RegulatoryQueryID"].ToString());
            SpParameters.Add("@ContractID", formData["ContractID"].ToString());
            SpParameters.Add("@QueryType", formData["QueryType"].ToString());
            SpParameters.Add("@Authority", formData["Authority"].ToString());
            SpParameters.Add("@Instruction", formData["Instruction"].ToString());
            //SpParameters.Add("@AuditObservation", formData["AuditObservation"].ToString());
            SpParameters.Add("@InstrumentID", formData["InstrumentID"].ToString());
            SpParameters.Add("@TradingDateFrom", Utility.DatetimeFormatter.DateFormat(formData["TradingDateFrom"].ToString()));
            SpParameters.Add("@TradingDateTo", Utility.DatetimeFormatter.DateFormat(formData["TradingDateTo"].ToString()));
            SpParameters.Add("@TWS", formData["TWS"].ToString());
            SpParameters.Add("@RMID", formData["RMID"].ToString() == "null" ? "0" : formData["RMID"].ToString());
            SpParameters.Add("@RegulatoryLetterNo", formData["RegulatoryLetterNo"].ToString());
            SpParameters.Add("@Response", formData["Response"].ToString());
            SpParameters.Add("@UploadedQueryFIlePath", FileUpload[0].ToString());
            SpParameters.Add("@UploadedResponseFilePath", FileUpload[1].ToString());
                      
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAuditRegulatorySL", SpParameters);
        }

        public async Task<object> GetRegulatoryList(string UserName, int CompanyID, int BranchID, RequlatoryQuerySearchFilter filter)
        {
            SqlParameter[] Params = new SqlParameter[6];
            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ProductID", filter.ProductID);
            Params[4] = new SqlParameter("@AccountNo", filter.AccountNo);
            Params[5] = new SqlParameter("@RegulatoryLetterNo", filter.RegulatoryLetterNo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[GetRegulatoryListSL]", Params);

            return new
            {
                AuditInspections = DataSets.Tables[0]
            };

        }

        public async Task<object> GetRegulatoryById(int CompanyID, int BranchID, string userName, int RegulatoryQueryID)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@RegulatoryQueryID", RegulatoryQueryID)

            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_GetRegulatoryById", sqlParams);
            //GetAuditInspection auditInspection = CustomConvert.DataSetToList<AuditInspectionDto>(DataSets.Tables[0]).FirstOrDefault();
            //auditInspection.auditInspectedClientDtos = CustomConvert.DataSetToList<AuditInspectedClientDto>(DataSets.Tables[1]).ToList();

            return new
            {
                result = DataSets.Tables[0]
            };

        }

        #endregion Requlatory Query Entry


    }
}
