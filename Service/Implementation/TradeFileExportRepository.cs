using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using Model.DTOs.Charges;
using Model.DTOs.TradeFileExport;
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
    public class TradeFileExportRepository : ITradeFileExportRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TradeFileExportRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
        public async Task<object> ListAccount(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ProductID", ProductID);
            sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);
        
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSearch]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<List<SLAgrGrpTradeExportDto>> ListAccountGroup(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", "list");
            sqlParams[4] = new SqlParameter("@AgrGrpID", "0");

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountGroupTradeFileExport]", sqlParams);

            return CustomConvert.DataSetToList<SLAgrGrpTradeExportDto>(DataSets.Tables[0]).ToList();
        }
        public async Task<SLAgrGrpTradeExportDto> AccountGroupDetail(string UserName, int CompanyID, int BranchID, string AgrGrpID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", "detail");
            sqlParams[4] = new SqlParameter("@AgrGrpID", AgrGrpID);


            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountGroupTradeFileExport]", sqlParams);

            var Group = CustomConvert.DataSetToList<SLAgrGrpTradeExportDto>(DataSets.Tables[0]).FirstOrDefault();
            
            Group.AccountList = CustomConvert.DataSetToList<SLAgrGrpMemberDto>(DataSets.Tables[1]).ToList();
            
            return Group;
        }

        public async Task<string> InsertUpdateAccountGroup(string UserName, int CompanyID, int BranchID, SLAgrGrpTradeExportDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AgrGrpID", data.AgrGrpID);
            SpParameters.Add("@GroupName", data.GroupName);
            SpParameters.Add("@FolderName", data.FolderName);
            SpParameters.Add("@AccountList", ListtoDataTableConverter.ToDataTable(data.AccountList).AsTableValuedParameter("Type_SLAgrGrpMember"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountGroupTradeFileExport", SpParameters);
        }

        public async Task<object> TradeFileExport(string UserName, int CompanyID, int BranchID, string TransactionDate, string AgrGrpIDs)
        {

			List<object> list = new List<object>();

			foreach (var AgrGrpID in AgrGrpIDs.Split(","))
            {
				SqlParameter[] sqlParams = new SqlParameter[5];

				sqlParams[0] = new SqlParameter("@UserName", UserName);
				sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
				sqlParams[2] = new SqlParameter("@BranchID", BranchID);
				sqlParams[3] = new SqlParameter("@TransactionDate", TransactionDate);
				sqlParams[4] = new SqlParameter("@AgrGrpID", AgrGrpID);


				var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListTradingDataForExport]", sqlParams);

				

				for (int i = 0; i < DataSets.Tables.Count; i += 2)
				{
					string DateFolder = Convert.ToDateTime(TransactionDate).ToString("dd-MMM-yyyy");
					string TargetFolder = DataSets.Tables[i].Rows[0][1].ToString();

					StringBuilder stringBuilder = new StringBuilder();

					foreach (DataRow item in DataSets.Tables[i + 1].Rows)
					{
						stringBuilder.Append(item[0].ToString() + Environment.NewLine);
					}

					var NewFile = new
					{
						FileName = DataSets.Tables[i].Rows[0][0].ToString(),
						FileContent = stringBuilder.ToString(),
						FileUrl = "/ExportedFiles/TradeFiles/" + DateFolder,
					};

					if (!String.IsNullOrEmpty(NewFile.FileName))
                    {
						list.Add(NewFile);

						

						bool exists = System.IO.Directory.Exists(@"\ExportedFiles\TradeFiles\" + DateFolder + "\\" + TargetFolder + "\\");

						if (!exists)
						{
							System.IO.Directory.CreateDirectory(@"\ExportedFiles\TradeFiles\" + DateFolder + "\\" + TargetFolder + "\\");

							exists = System.IO.Directory.Exists(@"\ExportedFiles\TradeFiles\" + DateFolder + "\\" + TargetFolder + "\\");
						}

						StringBuilder FilePath = new StringBuilder();

						FilePath.Append(@"\ExportedFiles\TradeFiles\" + DateFolder + "\\" + TargetFolder + "\\");

						//saving xml
						if (!String.IsNullOrEmpty(NewFile.FileName))
							using (System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath.ToString() + NewFile.FileName))
							{
								file.WriteLine(stringBuilder.ToString()); // "sb" is the StringBuilder
							}
					}
				}
			}

            

            return list;
        }

        public async Task<object> PayInPayOutFileExport(string UserName, int CompanyID, int BranchID, int ExchangeID, string SettlementDate, string FileType)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ExchangeID", ExchangeID);
            sqlParams[4] = new SqlParameter("@SettlementDate", SettlementDate);
            sqlParams[5] = new SqlParameter("@FileType", FileType);


            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListPayInPayOutForExport]", sqlParams);

			PayInOutFileContentDto payInOutFile = CustomConvert.DataSetToList<PayInOutFileContentDto>(DataSets.Tables[0]).FirstOrDefault();
			
            payInOutFile.FirstRow = CustomConvert.DataSetToList<PayInOutFileContentFirstRowDto>(DataSets.Tables[1]).FirstOrDefault();
			
            payInOutFile.payInOutFileBodyContent = CustomConvert.DataSetToList<PayInOutFileContentBodyDto>(DataSets.Tables[2]).ToList();

			StringBuilder Content = new StringBuilder();
            //first row
            Content.Append(payInOutFile.FirstRow.TotalCount + payInOutFile.FirstRow.TotalQuantity+ payInOutFile.FirstRow.DPCode + payInOutFile.FirstRow.ExchangeCode);

			Content.Append(Environment.NewLine);
			foreach (var item in payInOutFile.payInOutFileBodyContent)
            {
                Content.Append(item.DateString + item.BrokerBO + item.ClientBO + item.TracNo + item.ISIN + item.Quantity + item.FileType + item.AccountNumber + item.RowNumber);
				Content.Append(Environment.NewLine);
			}

			string DateFolder = Convert.ToDateTime(SettlementDate).ToString("dd-MMM-yyyy");
			bool exists = System.IO.Directory.Exists(@"\ExportedFiles\PayInPayOut\" + DateFolder + "\\");

			if (!exists)
			{
				System.IO.Directory.CreateDirectory(@"\ExportedFiles\PayInPayOut\" + DateFolder + "\\");
			}

			StringBuilder FilePath = new StringBuilder();

			FilePath.Append(@"\ExportedFiles\PayInPayOut\" + DateFolder + "\\");

			//saving xml
			if (!String.IsNullOrEmpty(payInOutFile.FileName))
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(FilePath.ToString() + payInOutFile.FileName))
				{
					file.WriteLine(Content.ToString()); // "sb" is the StringBuilder
				}

			return new
            {
                FileName = payInOutFile.FileName,
                FileContent = Content.ToString()
            };
        }
    }
}
