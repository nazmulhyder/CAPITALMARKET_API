using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic.FileIO;
using Model.DTOs.EquityIncorporation;
using Model.DTOs.FundCollection;
using Model.DTOs.ImportExportOmnibus;
using Model.DTOs.TradeFileUpload;
using Model.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
    public class FundCollectionRepository : IFundCollectionRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IConfiguration _configuration;
        public FundCollectionRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            this._configuration = configuration;
            _dbCommonOperation = dbCommonOperation;
        }

		public async Task<object> GetFundCollectionModeList(string UserName, int CompanyID, int BranchID)
        {
			SqlParameter[] sqlParams = new SqlParameter[2];			
			sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[1] = new SqlParameter("@BranchID", CompanyID);
		
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListFundCollectionMode]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> GetChequeDishonorReasonList(string UserName, int CompanyID, int BranchID)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);


			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListChequeDishonorReason]", sqlParams);

            return DataSets.Tables[0];
		}

		public async Task<List<ProductDto>> GetProductList(string UserName, int CompanyID)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("OwnerCompanyID", CompanyID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListProduct]", sqlParams);

            //return CustomConvert.DataSetToList<ProductDto>(DataSets.Tables[0]).Where(p => p.approvalStatus.ToLower() == "approved").ToList();
            return CustomConvert.DataSetToList<ProductDto>(DataSets.Tables[0]).ToList();
        }

        public async Task<object> GetAgreementInfo(int CompanyID, int ProductID, string AccountNumber)
        {

            FundCollectionDto clientInfo = new FundCollectionDto();

            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@ProductID", ProductID);
            sqlParams[2] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionClientInfoSL]", sqlParams);

			clientInfo = CustomConvert.DataSetToList<FundCollectionDto>(DataSets.Tables[0]).ToList().FirstOrDefault();

			return new {
				ClientInfo = clientInfo,
                BankList = DataSets.Tables[1]
			};
        }

        public async Task<object> GetBankAccountList(int CompanyID, int ProductID, int FundID, string SetupLevel)
        {

            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@ProductID", ProductID);
            sqlParams[2] = new SqlParameter("@FundID", FundID);
            sqlParams[3] = new SqlParameter("@SetupLevel", SetupLevel);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBankAccount]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<object> GetScheduleInstallmentListFor2ndDDIFile(string UserName, int CompanyID, int BranchID, string ScheduleDueDate)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ScheduleDueDate", ScheduleDueDate);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListScheduleInstallmentFor2ndDDIFile]", sqlParams);

            return new
            {
                ScheduleAccountList = DataSets.Tables[0]
            };
        }

     
        #region SL
     
        public async Task<object> GetFundCollectionList(string UserName, FundCollectionListDto filterData)
        {
            filterData.FromDate = Utility.DatetimeFormatter.DateFormat(filterData.FromDate);
            filterData.ToDate = Utility.DatetimeFormatter.DateFormat(filterData.ToDate);

			SqlParameter[] sqlParams = new SqlParameter[10];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", filterData.CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", filterData.BranchID);
			sqlParams[3] = new SqlParameter("@ListType", filterData.ListType);
			sqlParams[4] = new SqlParameter("@FilterBranchID", filterData.BranchID);
			sqlParams[5] = new SqlParameter("@ProductID", filterData.ProductID);
			sqlParams[6] = new SqlParameter("@FromDate", filterData.FromDate);
			sqlParams[7] = new SqlParameter("@ToDate", filterData.ToDate);
			sqlParams[8] = new SqlParameter("@RefNo", filterData.RefNo);
			sqlParams[9] = new SqlParameter("@AccountNo", filterData.AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionListSL]", sqlParams);

            return DataSets.Tables[0];

		}


        public async Task<string> AddUpdateFundCollection(FundCollectionDto data, string UserName, int CompanyID, int BranchID)
        {
            data.CollectionDate = Utility.DatetimeFormatter.DateFormat(data.CollectionDate);
            data.InstrumentDate = Utility.DatetimeFormatter.DateFormat(data.InstrumentDate);

            List<FundCollectionDto> dataList = new List<FundCollectionDto>();
            dataList.Add(data);

            if (data.CollectionInfoID > 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_UpdateFundCollectionSL", SpParameters);
            }
            else
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertFundCollectionSL", SpParameters);
            }


        }
        
        public async Task<string> ApproveFundCollection(string UserName, FundCollectionApprovalDto approveData, int CompanyID)
        {
            var Ids = approveData.MonInstrumentIDs.Split(",");

            string ResurnMsg = "";

            foreach (var id in Ids.Where(i => i.Length > 0))
            {
                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@MonInstrumentID", id);
                SpParameters.Add("@ApprovalStatus", approveData.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", approveData.ApprovalRemark);
                SpParameters.Add("@DepositBankAccID", approveData.DepositBankAccountID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResurnMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveFundCollectionSL", SpParameters);
            }

            return await Task.FromResult(ResurnMsg);
        }

        public async Task<FundCollectionDto> GetFundCollectionDetail(int CollectionInfoID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@CollectionInfoID", CollectionInfoID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionDetailSL]", sqlParams);

            return CustomConvert.DataSetToList<FundCollectionDto>(DataSets.Tables[0]).FirstOrDefault();
        }
        
        public async Task<object> BankStatementUpload(IFormCollection formData, string UserName)
        {
            List<BankStatementTransactionDto> bankStatements = new List<BankStatementTransactionDto>();

            List<dynamic> datalist = JsonConvert.DeserializeObject<List<dynamic>>(formData["TransactionList"]);


            foreach (var item in datalist[0])
            {
                string AccountName = item["Account Name"];
                string CustomerReference = item["Customer Reference"];
                string TransactionReference = item["Transaction Reference"];
                string AsAtDate = item["As At Date"];
                string VAFlag = item["VA Flag"];
                string VANumber = item["VA Number"];
                string AccountNumber = item["Account Number"];
                string DRCRFlag = item["DR / CR Flag"];
                string CurrencyCode = item["Currency Code"];
                string TransactionAmount = item["Transaction Amount"];
                string TransactionDate = item["Transaction Date"];
                string TransactionDetailion = item["Transaction Detail"];
                //.Substring(1, item["Transaction Date"].Length - 2)
                bankStatements.Add(new BankStatementTransactionDto
                {
                    AccountName = AccountName,
                    CustomerReference = CustomerReference,
                    TransactionReference = TransactionReference,
                    AsAtDate = Utility.DatetimeFormatter.DateFormat(AsAtDate),
                    VAFlag = VAFlag,
                    VANumber = VANumber,
                    AccountNumber = AccountNumber,
                    DRCRFlag = DRCRFlag,
                    CurrencyCode = CurrencyCode,
                    TransactionAmount = Convert.ToDecimal(TransactionAmount),
                    TransactionDate = Utility.DatetimeFormatter.DateFormat(TransactionDate),
                    TransactionDetailion = TransactionDetailion

                });
            }

            int CompanyID = Convert.ToInt32(formData["CompanyId"]);
            int BranchID = Convert.ToInt32(formData["BranchId"]);
            int BankAccountID = Convert.ToInt32(formData["BankAccountID"]);
            int FileSize = Convert.ToInt32(formData["FileSize"]);
            string FileName = formData["FileName"].ToString();
            string UploadTpe = formData["UploadType"].ToString();


            SqlParameter[] SpParameters = new SqlParameter[9];

            SpParameters[0] = new SqlParameter("@userName", UserName);
            SpParameters[1] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[2] = new SqlParameter("@BranchID", BranchID);
            SpParameters[3] = new SqlParameter("@BankAccountID", BankAccountID);
            SpParameters[4] = new SqlParameter("@FileName", FileName);
            SpParameters[5] = new SqlParameter("@FileExtension", Path.GetExtension(FileName));
            SpParameters[6] = new SqlParameter("@FileSizeInKB", FileSize / 1000);
            SpParameters[7] = new SqlParameter("@OperationType", UploadTpe);
            SpParameters[8] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(bankStatements));



            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_InsertCollectionFromBankStatementSL", SpParameters);

            if (DataSets.Tables[1].Rows[0][0].ToString() != "" && UploadTpe == "Save")
            {
                return await Task.FromResult("Bank Statement Upload Successful");
            }
            else
            {
                var Reult = new
                {
                    Message = DataSets.Tables[1].Rows[0][0].ToString(),
                    TransactionList = DataSets.Tables[0]
                };

                return Reult;
            }


        }
        
        public async Task<object> UploadedStatement(string date, int CompanyID, int BranchID, string UserName)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@TransactionDate", Utility.DatetimeFormatter.DateFormat(date));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBankStatementCollectionSL]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<object> InstallmentCollectionListForScheduleTagging(string username, int CompanyID, string ListType, string AccountNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", username);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@ListType", ListType);
            sqlParams[3] = new SqlParameter("@CollectionMode", "Cheque");
            sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallMentScheduleSL]", sqlParams);

            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", username);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCollectionForInstallMentScheduleTaggingSL]", sqlParam);

            return new
            {
                ScheduleList = DataSets.Tables[0],
                CollectionList = DataSet.Tables[0]
            };
        }

        

        public async Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagList(string UserName, int CompanyID, string ListType)
        {
            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", UserName);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@ListType", ListType);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallmentOfSchedule]", sqlParam);

            return CustomConvert.DataSetToList<SchedulenstrumentDto>(DataSet.Tables[0]);
        }
        
        public async Task<string> ApproveScheduleInstallmentTagList(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID)
        {
            string[] Arr = data.InstCollectionIDs.Split(",");

            string ResultMsg = string.Empty;

            foreach (var item in Arr)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@InstCollectionID", item);
                SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResultMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveScheduleInstallmentTagSL", SpParameters);
            }

            return ResultMsg;
        }

        public async Task<object> DDIFileApprove(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@DDIFileID", DDIFileID);
            SpParameters.Add("@ApproveStatus", ApproveStatus);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("ApproveDDIFileSL", SpParameters);
        }
       
        public async Task<object> DDIFileList(string UserName, int CompanyID, int BranchID, string Status)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@Status", Status);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileListSL]", sqlParams);

            List<DDIFileDto> FileList = new List<DDIFileDto>();

            FileList = CustomConvert.DataSetToList<DDIFileDto>(DataSets.Tables[0]);

            List<DDIFileTransactionTblDto> TransactionList = CustomConvert.DataSetToList<DDIFileTransactionTblDto>(DataSets.Tables[1]);

            List<DDIFileReturnTblDto> ReturnTransactionList = CustomConvert.DataSetToList<DDIFileReturnTblDto>(DataSets.Tables[2]);

            foreach (var item in FileList)
            {
                item.TransactionList = TransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
                item.ReturnList = ReturnTransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
            }

            return FileList;
        }
        
        //public async Task<object> DDIFileUpload(IFormCollection data, string UserName)
        //{
        //    try
        //    {
        //        List<DDIFileTransactionDto> TransactionList = JsonConvert.DeserializeObject<List<DDIFileTransactionDto>>(data["Transaction"]);
        //        List<DDIFileReturnDto> ReturnList = JsonConvert.DeserializeObject<List<DDIFileReturnDto>>(data["Return"]);

        //        string FileName = data["FileName"];
        //        string ExecutionType = data["ExecutionType"];
        //        int FileSize = Convert.ToInt32(data["FileSize"]);
        //        string FileExtension = Path.GetExtension(FileName);
        //        int CompanyID = Convert.ToInt32(data["CompanyID"]);
        //        int BranchID = Convert.ToInt32(data["BranchID"]);
        //        int BankAccountID = Convert.ToInt32(data["BankAccountID"]);

        //        SqlParameter[] sqlParams = new SqlParameter[10];

        //        sqlParams[0] = new SqlParameter("@UserName", UserName);
        //        sqlParams[1] = new SqlParameter("@BankAccountID", BankAccountID);
        //        sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
        //        sqlParams[3] = new SqlParameter("@BranchID", BranchID);
        //        sqlParams[4] = new SqlParameter("@FileName", FileName);
        //        sqlParams[5] = new SqlParameter("@FileSize", FileSize);
        //        sqlParams[6] = new SqlParameter("@FileExtension", FileExtension);
        //        sqlParams[7] = new SqlParameter("@ExecutionType", ExecutionType);
        //        sqlParams[8] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(TransactionList));
        //        sqlParams[9] = new SqlParameter("@ReturnTransactionList", ListtoDataTableConverter.ToDataTable(ReturnList));

        //        var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileUploadValidationSL]", sqlParams);

        //        return DataSets.Tables[0].Rows[0][0].ToString();


        //    }
        //    catch (Exception ex) { return null; };
        //}
        
        public async Task<object> GenerateDDIFile(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName)
        {
            string InstallmentIDs = string.Empty;
            foreach (ScheduleListForDDIFileDto schedule in data)
                InstallmentIDs = InstallmentIDs + schedule.InstallmentID.ToString() + ",";

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@BankAccountID", BanckAccountID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@InstallmentIDs", InstallmentIDs);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListDDIFileContentSL]", sqlParams);

            DDIFileheadTagDto HeadTag = CustomConvert.DataSetToList<DDIFileheadTagDto>(DataSets.Tables[0]).FirstOrDefault();

            List<DDIFileContentTagDto> Trnasactions = CustomConvert.DataSetToList<DDIFileContentTagDto>(DataSets.Tables[1]);

            DDIFileTailTagDto TailTag = CustomConvert.DataSetToList<DDIFileTailTagDto>(DataSets.Tables[2]).FirstOrDefault();

            StringBuilder OtherBankFileContent = new StringBuilder();
            StringBuilder SameBankFileContent = new StringBuilder();


            // txt file name
            string DateTimeName = DateTime.Now.ToString("ddmmyyhhmm");

            string SameBankDDIFileName = HeadTag.OrganizationName + "_" + "SB_" + DateTimeName + ".txt";
            string OtherBankDDIFileName = HeadTag.OrganizationName + "_" + "OB_" + DateTimeName + ".txt";

            //OTHER BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == false).Count() > 0)
            {
                OtherBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                OtherBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == false))
                {
                    OtherBankFileContent.Append(item.TransactionType + "," + item.RoutingNo + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    OtherBankFileContent.Append(Environment.NewLine);
                }

                OtherBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + OtherBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }

            //SAME BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == true).Count() > 0)
            {
                SameBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                SameBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == true))
                {
                    SameBankFileContent.Append(item.TransactionType + "," + item.BankCode + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    SameBankFileContent.Append(Environment.NewLine);
                }

                SameBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + SameBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }


            return new
            {
                SameBankFileName = SameBankDDIFileName,
                SamneBankFileContent = SameBankFileContent.ToString(),
                OtherBankFileName = OtherBankDDIFileName,
                OtherBankFileContent = OtherBankFileContent.ToString()
            };
        }

        public async Task<string> UnlockFor2ndDDIFile(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID)
        {
            string InstallmentIDs = string.Empty;

            foreach (var item in data)
            {
                InstallmentIDs = InstallmentIDs + item.InstallmentID + ",";
            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@InstallmentIDs", InstallmentIDs);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_UnlockInstallmentFor2ndDDISL", SpParameters);
        }
        
      
        #endregion SL

     
        #region AML
        public async Task<object> GetFundCollectionListAML(string UserName, FundCollectionListDto filterData)
        {
            filterData.FromDate = Utility.DatetimeFormatter.DateFormat(filterData.FromDate);
            filterData.ToDate = Utility.DatetimeFormatter.DateFormat(filterData.ToDate);

			SqlParameter[] sqlParams = new SqlParameter[10];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", filterData.CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", filterData.BranchID);
			sqlParams[3] = new SqlParameter("@ListType", filterData.ListType);
			sqlParams[4] = new SqlParameter("@FilterBranchID", filterData.BranchID);
			sqlParams[5] = new SqlParameter("@ProductID", filterData.ProductID);
			sqlParams[6] = new SqlParameter("@FromDate", filterData.FromDate);
			sqlParams[7] = new SqlParameter("@ToDate", filterData.ToDate);
			sqlParams[8] = new SqlParameter("@RefNo", filterData.RefNo);
			sqlParams[9] = new SqlParameter("@AccountNo", filterData.AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionListAML]", sqlParams);

			return DataSets.Tables[0];

			//var spParameters = new
			//{
			//	UserName = UserName,
			//	CompanyID = filterData.CompanyID,
			//	BranchID = filterData.BranchID,
			//	ListType = filterData.ListType,
			//	FilterBranchID = filterData.BranchID,
			//	ProductID = filterData.ProductID,
			//	FromDate = filterData.FromDate,
			//	ToDate = filterData.ToDate
			//};


			//return await _dbCommonOperation.ReadSingleTable<FundCollectionDto>("CM_FundCollectionListAML", spParameters);
        }

        public async Task<string> AddUpdateFundCollectionAML(FundCollectionDto data, string UserName, int CompanyID, int BranchID)
        {
            data.CollectionDate = Utility.DatetimeFormatter.DateFormat(data.CollectionDate);
            data.InstrumentDate = Utility.DatetimeFormatter.DateFormat(data.InstrumentDate);

            List<FundCollectionDto> dataList = new List<FundCollectionDto>();
            dataList.Add(data);

            if (data.CollectionInfoID > 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", CompanyID);
				SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_UpdateFundCollectionAML", SpParameters);
            }
            else
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", CompanyID);
                SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertFundCollectionAML", SpParameters);
            }


        }

        public async Task<string> ApproveFundCollectionAML(string UserName, FundCollectionApprovalDto approveData, int CompanyID)
        {
            var Ids = approveData.MonInstrumentIDs.Split(",");

            string ResurnMsg = "";

            foreach (var id in Ids.Where(i => i.Length > 0))
            {
                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@MonInstrumentID", id);
                SpParameters.Add("@ApprovalStatus", approveData.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", approveData.ApprovalRemark);
                SpParameters.Add("@DepositBankAccID", approveData.DepositBankAccountID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResurnMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveFundCollectionAML", SpParameters);
            }

            return await Task.FromResult(ResurnMsg);
        }

        public async Task<FundCollectionDto> GetFundCollectionDetailAML(int CollectionInfoID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@CollectionInfoID", CollectionInfoID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionDetailAML]", sqlParams);

            return CustomConvert.DataSetToList<FundCollectionDto>(DataSets.Tables[0]).FirstOrDefault();
        }
        
        public async Task<object> BankStatementUploadAML(IFormCollection formData, string UserName)
        {
            List<BankStatementTransactionDto> bankStatements = new List<BankStatementTransactionDto>();

            List<dynamic> datalist = JsonConvert.DeserializeObject<List<dynamic>>(formData["TransactionList"]);


            foreach (var item in datalist[0])
            {
                string AccountName = item["Account Name"];
                string CustomerReference = item["Customer Reference"];
                string TransactionReference = item["Transaction Reference"];
                string AsAtDate = item["As At Date"];
                string VAFlag = item["VA Flag"];
                string VANumber = item["VA Number"];
                string AccountNumber = item["Account Number"];
                string DRCRFlag = item["DR / CR Flag"];
                string CurrencyCode = item["Currency Code"];
                string TransactionAmount = item["Transaction Amount"];
                string TransactionDate = item["Transaction Date"];
                string TransactionDetailion = item["Transaction Detail"];
                //.Substring(1, item["Transaction Date"].Length - 2)
                bankStatements.Add(new BankStatementTransactionDto
                {
                    AccountName = AccountName,
                    CustomerReference = CustomerReference,
                    TransactionReference = TransactionReference,
                    AsAtDate = Utility.DatetimeFormatter.DateFormat(AsAtDate),
                    VAFlag = VAFlag,
                    VANumber = VANumber,
                    AccountNumber = AccountNumber,
                    DRCRFlag = DRCRFlag,
                    CurrencyCode = CurrencyCode,
                    TransactionAmount = Convert.ToDecimal(TransactionAmount),
                    TransactionDate = Utility.DatetimeFormatter.DateFormat(TransactionDate),
                    TransactionDetailion = TransactionDetailion

                });
            }

            int CompanyID = Convert.ToInt32(formData["CompanyId"]);
            int BranchID = Convert.ToInt32(formData["BranchId"]);
            int BankAccountID = Convert.ToInt32(formData["BankAccountID"]);
            int FileSize = Convert.ToInt32(formData["FileSize"]);
            string FileName = formData["FileName"].ToString();
            string UploadTpe = formData["UploadType"].ToString();


            SqlParameter[] SpParameters = new SqlParameter[9];

            SpParameters[0] = new SqlParameter("@userName", UserName);
            SpParameters[1] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[2] = new SqlParameter("@BranchID", BranchID);
            SpParameters[3] = new SqlParameter("@BankAccountID", BankAccountID);
            SpParameters[4] = new SqlParameter("@FileName", FileName);
            SpParameters[5] = new SqlParameter("@FileExtension", Path.GetExtension(FileName));
            SpParameters[6] = new SqlParameter("@FileSizeInKB", FileSize / 1000);
            SpParameters[7] = new SqlParameter("@OperationType", UploadTpe);
            SpParameters[8] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(bankStatements));



            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_InsertCollectionFromBankStatementAML", SpParameters);

            if (DataSets.Tables[1].Rows[0][0].ToString() != "" && UploadTpe == "Save")
            {
                return await Task.FromResult("Bank Statement Upload Successful");
            }
            else
            {
                var Reult = new
                {
                    Message = DataSets.Tables[1].Rows[0][0].ToString(),
                    TransactionList = DataSets.Tables[0]
                };

                return Reult;
            }


        }

        public async Task<object> UploadedStatementAML(string date, int CompanyID, int BranchID, string UserName)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@TransactionDate", Utility.DatetimeFormatter.DateFormat(date));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBankStatementCollectionAML]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<object> InstallmentCollectionListForScheduleTaggingAML(string username, int CompanyID, string ListType, string AccountNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", username);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@ListType", ListType);
            sqlParams[3] = new SqlParameter("@CollectionMode", "Cheque");
            sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallMentScheduleAML]", sqlParams);

            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", username);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCollectionForInstallMentScheduleTaggingAML]", sqlParam);

            return new
            {
				MemberCode = DataSets.Tables[1].Rows[0][0].ToString(),
				MemberName = DataSets.Tables[1].Rows[0][1].ToString(),
				AvailableBalance = DataSets.Tables[1].Rows[0][2].ToString(),
				ProductID = DataSets.Tables[1].Rows[0][3].ToString(),
				AccountNumber = DataSets.Tables[1].Rows[0][4].ToString(),
				ScheduleList = DataSets.Tables[0],
                CollectionList = DataSet.Tables[0]
            };
        }


        public async Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagListAML(string UserName, int CompanyID, string ListType)
        {
            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", UserName);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@ListType", ListType);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallmentOfScheduleAML]", sqlParam);

            return CustomConvert.DataSetToList<SchedulenstrumentDto>(DataSet.Tables[0]);
        }

        public async Task<string> ApproveScheduleInstallmentTagListAML(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID)
        {
            string[] Arr = data.InstCollectionIDs.Split(",");

            string ResultMsg = string.Empty;

            foreach (var item in Arr)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@InstCollectionID", item);
                SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResultMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveScheduleInstallmentTagAML", SpParameters);
            }

            return ResultMsg;
        }

        public async Task<object> GetScheduleInstallmentListForDDIFileAML(string UserName, int CompanyID, int BranchID, int ProductID, string ListType, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ProductID", ProductID);
            sqlParams[4] = new SqlParameter("@ListType", ListType);
            sqlParams[5] = new SqlParameter("@ScheduleDueDateFrom", ScheduleDueDateFrom);
            sqlParams[6] = new SqlParameter("@ScheduleDueDateTo", ScheduleDueDateTo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListScheduleInstallmentForDDIFileAML]", sqlParams);

            return new
            {
                ScheduleAccountList = DataSets.Tables[0]
            };
        }

        public async Task<object> GenerateDDIFileAML(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName)
        {
            string InstallmentIDs = string.Empty;
            foreach (ScheduleListForDDIFileDto schedule in data)
                InstallmentIDs = InstallmentIDs + schedule.InstallmentID.ToString() + ",";

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@BankAccountID", BanckAccountID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@InstallmentIDs", InstallmentIDs);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListDDIFileContentAML]", sqlParams);

            DDIFileheadTagDto HeadTag = CustomConvert.DataSetToList<DDIFileheadTagDto>(DataSets.Tables[0]).FirstOrDefault();

            List<DDIFileContentTagDto> Trnasactions = CustomConvert.DataSetToList<DDIFileContentTagDto>(DataSets.Tables[1]);

            DDIFileTailTagDto TailTag = CustomConvert.DataSetToList<DDIFileTailTagDto>(DataSets.Tables[2]).FirstOrDefault();

            StringBuilder OtherBankFileContent = new StringBuilder();
            StringBuilder SameBankFileContent = new StringBuilder();


            // txt file name
            string DateTimeName = DateTime.Now.ToString("ddmmyyhhmm");

            string SameBankDDIFileName = HeadTag.OrganizationName + "_" + "SB_" + DateTimeName + ".txt";
            string OtherBankDDIFileName = HeadTag.OrganizationName + "_" + "OB_" + DateTimeName + ".txt";

            //OTHER BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == false).Count() > 0)
            {
                OtherBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                OtherBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == false))
                {
                    OtherBankFileContent.Append(item.TransactionType + "," + item.RoutingNo + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    OtherBankFileContent.Append(Environment.NewLine);
                }

                OtherBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + OtherBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }

            //SAME BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == true).Count() > 0)
            {
                SameBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                SameBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == true))
                {
                    SameBankFileContent.Append(item.TransactionType + "," + item.BankCode + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    SameBankFileContent.Append(Environment.NewLine);
                }

                SameBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + SameBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }


            return new
            {
                SameBankFileName = SameBankDDIFileName,
                SamneBankFileContent = SameBankFileContent.ToString(),
                OtherBankFileName = OtherBankDDIFileName,
                OtherBankFileContent = OtherBankFileContent.ToString()
            };
        }
        
        public async Task<object> GetScheduleInstallmentListFor2ndDDIFileAML(string UserName, int CompanyID, int BranchID, int ProductID, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", ProductID);
			sqlParams[4] = new SqlParameter("@ScheduleDueDateFrom", ScheduleDueDateFrom);
			sqlParams[5] = new SqlParameter("@ScheduleDueDateTo", ScheduleDueDateTo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListScheduleInstallmentFor2ndDDIFileAML]", sqlParams);

            return new
            {
                ScheduleAccountList = DataSets.Tables[0]
            };
        }

        public async Task<string> UnlockFor2ndDDIFileAML(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID)
        {
            string InstallmentIDs = string.Empty;

            foreach (var item in data)
            {
                InstallmentIDs = InstallmentIDs + item.InstallmentID + ",";
            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@InstallmentIDs", InstallmentIDs);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_UnlockInstallmentFor2ndDDIAML", SpParameters);
        }
        
        public async Task<object> DDIFileUpload(IFormCollection data, string UserName)
        {
            try
            {
				string FileName = data["FileName"];
				string ExecutionType = data["ExecutionType"];
				int FileSize = Convert.ToInt32(data["FileSize"]);
				string FileExtension = Path.GetExtension(FileName);
				int CompanyID = Convert.ToInt32(data["CompanyID"]);
				int ProductID = Convert.ToInt32(data["ProductID"]);
				int BranchID = Convert.ToInt32(data["BranchID"]);
				int BankAccountID = Convert.ToInt32(data["BankAccountID"]);
				
                //for AML BBL-SCB
                if(CompanyID == 2)
                {
					List<AMLBBLSCBDDIFileDto> TransactionList = JsonConvert.DeserializeObject<List<AMLBBLSCBDDIFileDto>>(data["Transaction"]);


					SqlParameter[] sqlParams = new SqlParameter[10];

					sqlParams[0] = new SqlParameter("@UserName", UserName);
					sqlParams[1] = new SqlParameter("@BankAccountID", BankAccountID);
					sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
					sqlParams[3] = new SqlParameter("@BranchID", BranchID);
					sqlParams[4] = new SqlParameter("@FundID", ProductID);
					sqlParams[5] = new SqlParameter("@FileName", FileName);
					sqlParams[6] = new SqlParameter("@FileSize", FileSize);
					sqlParams[7] = new SqlParameter("@FileExtension", FileExtension);
					sqlParams[8] = new SqlParameter("@ExecutionType", ExecutionType);
					sqlParams[9] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(TransactionList));

					var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileUploadValidationAML]", sqlParams);

					return DataSets.Tables[0].Rows[0][0].ToString();
				}
                //for IL SCB
                else
                {
					List<DDIFileTransactionDto> TransactionList = JsonConvert.DeserializeObject<List<DDIFileTransactionDto>>(data["Transaction"]);

					SqlParameter[] sqlParams = new SqlParameter[10];

					sqlParams[0] = new SqlParameter("@UserName", UserName);
					sqlParams[1] = new SqlParameter("@BankAccountID", BankAccountID);
					sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
					sqlParams[3] = new SqlParameter("@BranchID", BranchID);
					sqlParams[4] = new SqlParameter("@ProductID", ProductID);
					sqlParams[5] = new SqlParameter("@FileName", FileName);
					sqlParams[6] = new SqlParameter("@FileSize", FileSize);
					sqlParams[7] = new SqlParameter("@FileExtension", FileExtension);
					sqlParams[8] = new SqlParameter("@ExecutionType", ExecutionType);
					sqlParams[9] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(TransactionList));

					var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileUploadValidationIL]", sqlParams);

					return DataSets.Tables[0].Rows[0][0].ToString();
				}

				


            }
            catch (Exception ex) { return null; };
        }

		

		public async Task<object> DDIFileApproveAML(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus)
        {
           
			DynamicParameters sqlParams = new DynamicParameters();

			sqlParams.Add("@UserName", UserName);
			sqlParams.Add("@DDIFileID", DDIFileID);
			sqlParams.Add("@ApproveStatus", ApproveStatus);
			sqlParams.Add("@CompanyID", CompanyID);
			sqlParams.Add("@BranchID", BranchID);
			sqlParams.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("[dbo].[ApproveDDIFileAML]", sqlParams);

        }


        public async Task<object> DDIFileListAML(string UserName, int CompanyID, int BranchID, string Status, string FromDate,string ToDate, int ProductID)
        {
            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FromDate", FromDate);
            sqlParams[4] = new SqlParameter("@ToDate", ToDate);
            sqlParams[5] = new SqlParameter("@Status", Status);
            sqlParams[6] = new SqlParameter("@ProductID", ProductID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileListAML]", sqlParams);

            List<AMLDDIFileTblDto> FileList = new List<AMLDDIFileTblDto>();

            FileList = CustomConvert.DataSetToList<AMLDDIFileTblDto>(DataSets.Tables[0]);

			List<AMLDDIFileTranTblDto> TransactionList = CustomConvert.DataSetToList<AMLDDIFileTranTblDto>(DataSets.Tables[1]);

            foreach (var item in FileList)
            {
                item.TransactionList = TransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
            }

            return FileList;
        }


        #endregion AML

       
        #region IL

        public async Task<object> GetFundCollectionListIL(string UserName, FundCollectionListDto filterData)
        {
            filterData.FromDate = Utility.DatetimeFormatter.DateFormat(filterData.FromDate);
            filterData.ToDate = Utility.DatetimeFormatter.DateFormat(filterData.ToDate);

			SqlParameter[] sqlParams = new SqlParameter[10];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", filterData.CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", filterData.BranchID);
			sqlParams[3] = new SqlParameter("@ListType", filterData.ListType);
			sqlParams[4] = new SqlParameter("@FilterBranchID", filterData.BranchID);
			sqlParams[5] = new SqlParameter("@ProductID", filterData.ProductID);
			sqlParams[6] = new SqlParameter("@FromDate", filterData.FromDate);
			sqlParams[7] = new SqlParameter("@ToDate", filterData.ToDate);
			sqlParams[8] = new SqlParameter("@RefNo", filterData.RefNo);
			sqlParams[9] = new SqlParameter("@AccountNo", filterData.AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionListIL]", sqlParams);

			return DataSets.Tables[0];

			//var spParameters = new
			//{
			//	UserName = UserName,
			//	CompanyID = filterData.CompanyID,
			//	BranchID = filterData.BranchID,
			//	ListType = filterData.ListType,
			//	FilterBranchID = filterData.BranchID,
			//	ProductID = filterData.ProductID,
			//	FromDate = filterData.FromDate,
			//	ToDate = filterData.ToDate
			//};


			//return await _dbCommonOperation.ReadSingleTable<FundCollectionDto>("CM_FundCollectionListIL", spParameters);
        }

        public async Task<string> AddUpdateFundCollectionIL(FundCollectionDto data, string UserName, int CompanyID, int BranchID)
        {
            data.CollectionDate = Utility.DatetimeFormatter.DateFormat(data.CollectionDate);
            data.InstrumentDate = Utility.DatetimeFormatter.DateFormat(data.InstrumentDate);

            List<FundCollectionDto> dataList = new List<FundCollectionDto>();
            dataList.Add(data);

            if (data.CollectionInfoID > 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", CompanyID);
				SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_UpdateFundCollectionIL", SpParameters);
            }
            else
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", CompanyID);
                SpParameters.Add("@FundCollection", ListtoDataTableConverter.ToDataTable(dataList).AsTableValuedParameter("Type_FundCollectionSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertFundCollectionIL", SpParameters);
            }


        }

        public async Task<string> ApproveFundCollectionIL(string UserName, FundCollectionApprovalDto approveData, int CompanyID)
        {
            var Ids = approveData.MonInstrumentIDs.Split(",");

            string ResurnMsg = "";

            foreach (var id in Ids.Where(i => i.Length > 0))
            {
                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@MonInstrumentID", id);
                SpParameters.Add("@ApprovalStatus", approveData.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", approveData.ApprovalRemark);
                SpParameters.Add("@DepositBankAccID", approveData.DepositBankAccountID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResurnMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveFundCollectionIL", SpParameters);
            }

            return await Task.FromResult(ResurnMsg);
        }

        public async Task<FundCollectionDto> GetFundCollectionDetailIL(int CollectionInfoID, int CompanyID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@CollectionInfoID", CollectionInfoID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_FundCollectionDetailIL]", sqlParams);

            return CustomConvert.DataSetToList<FundCollectionDto>(DataSets.Tables[0]).FirstOrDefault();
        }

        public async Task<object> BankStatementUploadIL(IFormCollection formData, string UserName)
        {
            List<BankStatementTransactionDto> bankStatements = new List<BankStatementTransactionDto>();

            List<dynamic> datalist = JsonConvert.DeserializeObject<List<dynamic>>(formData["TransactionList"]);


            foreach (var item in datalist[0])
            {
                string AccountName = item["Account Name"];
                string CustomerReference = item["Customer Reference"];
                string TransactionReference = item["Transaction Reference"];
                string AsAtDate = item["As At Date"];
                string VAFlag = item["VA Flag"];
                string VANumber = item["VA Number"];
                string AccountNumber = item["Account Number"];
                string DRCRFlag = item["DR / CR Flag"];
                string CurrencyCode = item["Currency Code"];
                string TransactionAmount = item["Transaction Amount"];
                string TransactionDate = item["Transaction Date"];
                string TransactionDetailion = item["Transaction Detail"];
                //.Substring(1, item["Transaction Date"].Length - 2)
                bankStatements.Add(new BankStatementTransactionDto
                {
                    AccountName = AccountName,
                    CustomerReference = CustomerReference,
                    TransactionReference = TransactionReference,
                    AsAtDate = Utility.DatetimeFormatter.DateFormat(AsAtDate),
                    VAFlag = VAFlag,
                    VANumber = VANumber,
                    AccountNumber = AccountNumber,
                    DRCRFlag = DRCRFlag,
                    CurrencyCode = CurrencyCode,
                    TransactionAmount = Convert.ToDecimal(TransactionAmount),
                    TransactionDate = Utility.DatetimeFormatter.DateFormat(TransactionDate),
                    TransactionDetailion = TransactionDetailion

                });
            }

            int CompanyID = Convert.ToInt32(formData["CompanyId"]);
            int BranchID = Convert.ToInt32(formData["BranchId"]);
            int BankAccountID = Convert.ToInt32(formData["BankAccountID"]);
            int FileSize = Convert.ToInt32(formData["FileSize"]);
            string FileName = formData["FileName"].ToString();
            string UploadTpe = formData["UploadType"].ToString();


            SqlParameter[] SpParameters = new SqlParameter[9];

            SpParameters[0] = new SqlParameter("@userName", UserName);
            SpParameters[1] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[2] = new SqlParameter("@BranchID", BranchID);
            SpParameters[3] = new SqlParameter("@BankAccountID", BankAccountID);
            SpParameters[4] = new SqlParameter("@FileName", FileName);
            SpParameters[5] = new SqlParameter("@FileExtension", Path.GetExtension(FileName));
            SpParameters[6] = new SqlParameter("@FileSizeInKB", FileSize / 1000);
            SpParameters[7] = new SqlParameter("@OperationType", UploadTpe);
            SpParameters[8] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(bankStatements));



            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_InsertCollectionFromBankStatementIL", SpParameters);

            if (DataSets.Tables[1].Rows[0][0].ToString() != "" && UploadTpe == "Save")
            {
                return await Task.FromResult("Bank Statement Upload Successful");
            }
            else
            {
                var Reult = new
                {
                    Message = DataSets.Tables[1].Rows[0][0].ToString(),
                    TransactionList = DataSets.Tables[0]
                };

                return Reult;
            }


        }

        public async Task<object> UploadedStatementIL(string date, int CompanyID, int BranchID, string UserName)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@TransactionDate", Utility.DatetimeFormatter.DateFormat(date));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBankStatementCollectionIL]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<object> InstallmentCollectionListForScheduleTaggingIL(string username, int CompanyID, string ListType, string AccountNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", username);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@ListType", ListType);
            sqlParams[3] = new SqlParameter("@CollectionMode", "Cheque");
            sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallMentScheduleIL]", sqlParams);

            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", username);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@AccountNumber", AccountNumber);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCollectionForInstallMentScheduleTaggingIL]", sqlParam);

			return new
			{
				MemberCode = DataSets.Tables[1].Rows[0][0].ToString(),
				MemberName = DataSets.Tables[1].Rows[0][1].ToString(),
				AvailableBalance = DataSets.Tables[1].Rows[0][2].ToString(),
				ProductID = DataSets.Tables[1].Rows[0][3].ToString(),
				AccountNumber = DataSets.Tables[1].Rows[0][4].ToString(),
				ScheduleList = DataSets.Tables[0],
				CollectionList = DataSet.Tables[0]
			};
		}

		public async Task<string> SaveScheduleInstallmentTagAML(string UserName, List<InstallmentScheduleDto> InstalmentScheduleList, int MonInstrumentID, int CompanyID, int BranchID)
		{
            string InstallmentIDs = string.Empty;

			foreach (var item in InstalmentScheduleList) InstallmentIDs = InstallmentIDs + ',' + item.InstallmentID;

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@userName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@MonInstrumentID", MonInstrumentID);
			SpParameters.Add("@InstallmentIDs", InstallmentIDs);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAMLInstallmentCollection", SpParameters);

		}

		public async Task<string> SaveScheduleInstallmentTagIL(string UserName, List<InstallmentScheduleDto> InstalmentScheduleList, int MonInstrumentID, int CompanyID, int BranchID)
        {

			string InstallmentIDs = string.Empty;

			foreach (var item in InstalmentScheduleList) InstallmentIDs = InstallmentIDs + ',' + item.InstallmentID;

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@userName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@MonInstrumentID", MonInstrumentID);
			SpParameters.Add("@InstallmentIDs", InstallmentIDs);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertILInstallmentCollection", SpParameters);

        }

        public async Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagListIL(string UserName, int CompanyID, string ListType)
        {
            SqlParameter[] sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter("@UserName", UserName);
            sqlParam[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParam[2] = new SqlParameter("@ListType", ListType);

            var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListInstallmentOfScheduleIL]", sqlParam);

            return CustomConvert.DataSetToList<SchedulenstrumentDto>(DataSet.Tables[0]);
        }

        public async Task<string> ApproveScheduleInstallmentTagListIL(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID)
        {
            string[] Arr = data.InstCollectionIDs.Split(",");

            string ResultMsg = string.Empty;

            foreach (var item in Arr)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", UserName);
                SpParameters.Add("@InstCollectionID", item);
                SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                ResultMsg = await _dbCommonOperation.InsertUpdateBySP("CM_ApproveScheduleInstallmentTagIL", SpParameters);
            }

            return ResultMsg;
        }

        public async Task<object> GetScheduleInstallmentListForDDIFileIL(string UserName, int CompanyID, int BranchID, int ProductID, string ListType, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
			SqlParameter[] sqlParams = new SqlParameter[7];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ProductID", ProductID);
			sqlParams[4] = new SqlParameter("@ListType", ListType);
			sqlParams[5] = new SqlParameter("@ScheduleDueDateFrom", ScheduleDueDateFrom);
			sqlParams[6] = new SqlParameter("@ScheduleDueDateTo", ScheduleDueDateTo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListScheduleInstallmentForDDIFileIL]", sqlParams);

            return new
            {
                ScheduleAccountList = DataSets.Tables[0]
            };
        }

        public async Task<object> GenerateDDIFileIL(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName)
        {
            string InstallmentIDs = string.Empty;
            foreach (ScheduleListForDDIFileDto schedule in data)
                InstallmentIDs = InstallmentIDs + schedule.InstallmentID.ToString() + ",";

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@BankAccountID", BanckAccountID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@InstallmentIDs", InstallmentIDs);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListDDIFileContentIL]", sqlParams);

            DDIFileheadTagDto HeadTag = CustomConvert.DataSetToList<DDIFileheadTagDto>(DataSets.Tables[0]).FirstOrDefault();

            List<DDIFileContentTagDto> Trnasactions = CustomConvert.DataSetToList<DDIFileContentTagDto>(DataSets.Tables[1]);

            DDIFileTailTagDto TailTag = CustomConvert.DataSetToList<DDIFileTailTagDto>(DataSets.Tables[2]).FirstOrDefault();

            StringBuilder OtherBankFileContent = new StringBuilder();
            StringBuilder SameBankFileContent = new StringBuilder();


            // txt file name
            string DateTimeName = DateTime.Now.ToString("ddmmyyhhmm");

            string SameBankDDIFileName = HeadTag.OrganizationName + "_" + "SB_" + DateTimeName + ".txt";
            string OtherBankDDIFileName = HeadTag.OrganizationName + "_" + "OB_" + DateTimeName + ".txt";

            //OTHER BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == false).Count() > 0)
            {
                OtherBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                OtherBankFileContent.Append(Environment.NewLine);

                int TransactionCount1 = 0;
                decimal TotalAmount1 = 0;

				foreach (var item in Trnasactions.Where(t => t.IsSameBank == false))
                {
                    OtherBankFileContent.Append(item.TransactionType + "," + item.RoutingNo + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,," + item.InstrumentNumber);
                    OtherBankFileContent.Append(Environment.NewLine);

					TransactionCount1++;
					TotalAmount1 = TotalAmount1 + Convert.ToDecimal(item.Amount);
                }

                OtherBankFileContent.Append(TailTag.TailTag + "," + TransactionCount1 + "," + "0" + "," + TotalAmount1 + "," + "0");


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + OtherBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }

			//SAME BANK ACCOUNT DDI
			int TransactionCount = 0;
			decimal TotalAmount = 0;
			if (Trnasactions.Where(t => t.IsSameBank == true).Count() > 0)
            {
                SameBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                SameBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == true))
                {
                    SameBankFileContent.Append(item.TransactionType + "," + item.BankCode + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,," + item.InstrumentNumber);
                    SameBankFileContent.Append(Environment.NewLine);

					TransactionCount++;
					TotalAmount = TotalAmount + Convert.ToDecimal(item.Amount);
				}

                SameBankFileContent.Append(TailTag.TailTag + "," + TransactionCount + "," + "0" + "," + TotalAmount + "," + "0");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + SameBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }


            return new
            {
                SameBankFileName = SameBankDDIFileName,
                SamneBankFileContent = SameBankFileContent.ToString(),
                OtherBankFileName = OtherBankDDIFileName,
                OtherBankFileContent = OtherBankFileContent.ToString()
            };
        }

        public async Task<object> GetScheduleInstallmentListFor2ndDDIFileIL(string UserName, int CompanyID, int BranchID, int ProductID, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ProductID", ProductID);
            sqlParams[4] = new SqlParameter("@ScheduleDueDateFrom", ScheduleDueDateFrom);
            sqlParams[5] = new SqlParameter("@ScheduleDueDateTo", ScheduleDueDateTo);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListScheduleInstallmentFor2ndDDIFileIL]", sqlParams);

            return new
            {
                ScheduleAccountList = DataSets.Tables[0]
            };
        }

        public async Task<string> UnlockFor2ndDDIFileIL(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID)
        {
            string InstallmentIDs = string.Empty;

            foreach (var item in data)
            {
                InstallmentIDs = InstallmentIDs + item.InstallmentID + ",";
            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@InstallmentIDs", InstallmentIDs);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_UnlockInstallmentFor2ndDDIIL", SpParameters);
        }

        public async Task<object> DDIFileListIL(string UserName, int CompanyID, int BranchID, string Status, string FromDate, string ToDate, int ProductID)
        {
			SqlParameter[] sqlParams = new SqlParameter[7];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FromDate", FromDate);
			sqlParams[4] = new SqlParameter("@ToDate", ToDate);
			sqlParams[5] = new SqlParameter("@Status", Status);
			sqlParams[6] = new SqlParameter("@ProductID", ProductID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileListIL]", sqlParams);

            List<DDIFileDto> FileList = new List<DDIFileDto>();

            FileList = CustomConvert.DataSetToList<DDIFileDto>(DataSets.Tables[0]);

            List<DDIFileTransactionTblDto> TransactionList = CustomConvert.DataSetToList<DDIFileTransactionTblDto>(DataSets.Tables[1]);

          
            foreach (var item in FileList)
            {
                item.TransactionList = TransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
            }

            return FileList;
        }

        public async Task<object> DDIFileApproveIL(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@DDIFileID", DDIFileID);
            SpParameters.Add("@ApproveStatus", ApproveStatus);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("ApproveDDIFileIL", SpParameters);
        }

        #endregion IL


    }
}
