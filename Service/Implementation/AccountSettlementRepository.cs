using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AccountSettlement;
using Model.DTOs.Broker;
using Model.DTOs.Charges;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
	public class AccountSettlementRepository : IAccountSettlementRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;
		
		public AccountSettlementRepository(IDBCommonOpService dbCommonOperation)
		{
			_dbCommonOperation = dbCommonOperation;
		}


		#region Closure

		public async Task<object> BulkUploadAccountClosureValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList)
		{
			foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(accountList));

			var DataSets = new DataSet();
			
			DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountClosureRequestBulkValidation]", sqlParams);
			

			return DataSets.Tables[0];
		}

		public async Task<object> BulkUploadAccountClosureUpload(string UserName, int CompanyID, int BranchID, List<AccountClosureReqDto> accountList)
		{
			foreach (var data in accountList) { data.AccountNumber = data.AccountNumber.Trim();

				DynamicParameters SpParameters = new DynamicParameters();
				SpParameters.Add("@UserName", UserName);
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);

				SpParameters.Add("@ContractID", data.ContractID);
				SpParameters.Add("@Reason", data.Reason);
				SpParameters.Add("@Remarks", data.Remarks);
				SpParameters.Add("@DocumentCount", 0);
				SpParameters.Add("@DocumentPath", "");

				SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				if (CompanyID == 4)
					 await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountClosureSL", SpParameters);
				else
					 await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountClosureIL", SpParameters);
				
			}
			return "Bulk account closure request uploaded.";
		}



		public async Task<object> ExecuteAccountClosure(string UserName, int CompanyID, int BranchID, string ApprovalStatus, List<AccountClosureReqDto> list)
		{
			foreach(var data in list)
			{
				DynamicParameters SpParameters = new DynamicParameters();
				SpParameters.Add("@UserName", UserName);
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);

				SpParameters.Add("@AgrClosureReqID", data.AgrClosureReqID);
				SpParameters.Add("@InstrumentType", data.Remarks);//used as remark
				SpParameters.Add("@InstrumentNumber", data.InstrumentNumber);
				SpParameters.Add("ApprovalStatus", ApprovalStatus);
				SpParameters.Add("@DisburseAmount", data.AvailableBalance - data.AccruedCharge);
				SpParameters.Add("@BankAccountID", data.BankAccountID);

				SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
				if (CompanyID == 4)
					 await _dbCommonOperation.InsertUpdateBySP("CM_ExecuteAccountClosureSL", SpParameters);
				else
					 await _dbCommonOperation.InsertUpdateBySP("CM_ExecuteAccountClosureIL", SpParameters);
			}

			return "Account coluser request has been executed.";

			
		}

		public async Task<object> ListAccountClosure(string UserName, int CompanyID, int BranchID, string listtype)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
						{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID),
				new SqlParameter("@ListType", listtype)
						};

			var DataSets = new DataSet();

			if (CompanyID == 4)
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountClosureSL]", sqlParams);
			}
			else 
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountClosureIL]", sqlParams);
			}
			

			return DataSets.Tables[0];
		}

		public async Task<object> saveAccountClosure(string UserName, int CompanyID, int BranchID, IFormCollection form)
		{

			AccountClosureReqDto data = JsonConvert.DeserializeObject<AccountClosureReqDto>(form["ClosureData"]);

			string filePath = Utility.FilePath.GetImportedFilesPath() + "AccountClosureDocument\\";

			if (CompanyID == 4) filePath = filePath + "SL\\";
			else if (CompanyID == 3) filePath = filePath + "IL\\";
			else filePath = filePath + "AML\\";

			filePath = filePath + data.ContractID + "\\" + DateTime.Now.ToString("yyMMddhhmm") + "\\";

			foreach (var item in form.Files)
			{
				bool exists = System.IO.Directory.Exists(filePath);
				if (!exists) System.IO.Directory.CreateDirectory(filePath);

				using (Stream fileStream = new FileStream(filePath + item.FileName, FileMode.Create))
				{
					item.CopyTo(fileStream);
				}
			}

			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@Reason", data.Reason);
			SpParameters.Add("@Remarks", data.Remarks);
			SpParameters.Add("@DocumentCount", form.Files.Count);
			SpParameters.Add("@DocumentPath", filePath);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4)
			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountClosureSL", SpParameters);
			else
			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountClosureIL", SpParameters);
		}

		public async Task<object> GetAccountClosureDetail(string UserName, int CompanyID, int BranchID, string AccountNo, int AgrClosureReqID)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
			{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID),
				new SqlParameter("@AccountNo", AccountNo),
				new SqlParameter("@AgrClosureReqID", AgrClosureReqID),
			};

			var DataSets = new DataSet();
		
			if (CompanyID == 4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAccountClosureDetailSL]", sqlParams);
			else  DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAccountClosureDetailIL]", sqlParams);
			
			AccountClosureReqDto data = CustomConvert.DataSetToList<AccountClosureReqDto>(DataSets.Tables[0]).FirstOrDefault();


			if (data != null)
			{
				string FilePath = data.DocumentPath;

				List<string> FileList = new List<string>();

				if (data != null && data.DocumentCount > 0)
				{
					DirectoryInfo d = new DirectoryInfo(FilePath);
					FileInfo[] Files = d.GetFiles(); 

					foreach (FileInfo file in Files)
					{
						FileList.Add(d.ToString() + file.Name);
					}
				}

				return new
				{
					ClosureData = data,
					DocumentList = FileList,
				};
			}
			else
			{
				throw new Exception("No Data Found");
			}
		}

		#endregion Closure



		#region withdrawal

		public async Task<object> ApproveAccountSuspensionWithdrawal(string UserName, int CompanyID, int BranchID, AccountSuspensionWithdrawalApprovalDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@AgrSusWithdrawalIDs", data.AgrSusWithdrawalIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if (CompanyID == 4) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountSuspensionWithdrawalSL", SpParameters);
			else  return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountSuspensionWithdrawalIL", SpParameters);
		}

		public async Task<object> ListAccountSuspensionWithdrawalRequest(string UserName, int CompanyID, int BranchID, string listtype)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
						{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID),
				new SqlParameter("@ListType", listtype)
						};

			var DataSets = new DataSet();

			if (CompanyID == 4)
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionWithdrawalSL]", sqlParams);
			}
			else 
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionWithdrawalIL]", sqlParams);
			}
			
			return DataSets.Tables[0];
		}


		public async Task<object> BulkUploadAccountSuspensionWithdrawalValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList)
		{
			foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(accountList));

			var DataSets = new DataSet();

			if(CompanyID==4)
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionBulkWithdrawalValidationSL]", sqlParams);
			else
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionBulkWithdrawalValidationIL]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> BulkUploadAccountSuspensionWithdrawal(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList)
		{
			foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(accountList));



			var DataSets = new DataSet();
			if(CompanyID==4)
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionWithdrawalBulkSL]", sqlParams);
			else
			DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionWithdrawalBulkIL]", sqlParams);


			return DataSets.Tables[0].Rows[0][0].ToString();
		}

		public async Task<object> saveAccountSuspensionWithdrawalRequest(string UserName, int CompanyID, int BranchID, IFormCollection form)
		{

			AccountSuspensionWithdrawalDto data = JsonConvert.DeserializeObject<AccountSuspensionWithdrawalDto>(form["WithdrawalData"]);

			DynamicParameters SpParameters = new DynamicParameters();


			string filePath = Utility.FilePath.GetImportedFilesPath() + "AccountSuspensionWithdrawDocument\\";

			if (CompanyID == 4) filePath = filePath + "SL\\";
			else if (CompanyID == 3) filePath = filePath + "IL\\";
			else filePath = filePath + "AML\\";

			filePath = filePath + data.ContractID + "\\" + DateTime.Now.ToString("yyMMddhhmm") + "\\";

			foreach (var item in form.Files)
			{

				bool exists = System.IO.Directory.Exists(filePath);
				if (!exists) System.IO.Directory.CreateDirectory(filePath);

				using (Stream fileStream = new FileStream(filePath + item.FileName, FileMode.Create))
				{
					item.CopyTo(fileStream);
				}
			}

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@Remark", data.SuspensionWithdrawalRemark);
			SpParameters.Add("@AgrSuspensionID", data.AgrSuspensionID);
			SpParameters.Add("@DocumentCount", form.Files.Count);
			SpParameters.Add("@DocumentPath", filePath);
		
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4)
			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountSuspensionWithdrawalSL", SpParameters);
			else
			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAccountSuspensionWithdrawalIL", SpParameters);
		}

		public async Task<object> ListAccountSuspensionForWithdrawal(string UserName, int CompanyID, int BranchID)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
						{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID)
						};

			var DataSets = new DataSet();

			if (CompanyID == 4)
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionForWithdrawalSL]", sqlParams);
			}
			else 
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionForWithdrawalIL]", sqlParams);
			}
			

			return DataSets.Tables[0];
		}

		#endregion withdrawal




		#region Suspension

		public async Task<object> GetAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, string AccountNo, int AgrSuspensionID)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
			{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID),
				new SqlParameter("@AccountNo", AccountNo),
				new SqlParameter("@AgrSuspensionID", AgrSuspensionID),
			};

			var DataSets = new DataSet();
			AccountSuspensionWithdrawalDto data = new AccountSuspensionWithdrawalDto();
			string Company = "";

			if (CompanyID == 4)
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAccountSuspensionDetailSL]", sqlParams);
				Company = "SL";
			}
			else 
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAccountSuspensionDetailIL]", sqlParams);
				Company = "IL";
			}
		

			data = CustomConvert.DataSetToList<AccountSuspensionWithdrawalDto>(DataSets.Tables[0]).FirstOrDefault();


			if (data != null)
			{
				string FilePath = data.DocumentPath;

				List<string> FileList = new List<string>();

				if (data != null && data.DocumentCount > 0)
				{
					DirectoryInfo d = new DirectoryInfo(FilePath);
					FileInfo[] Files = d.GetFiles(); //Getting Text files

					foreach (FileInfo file in Files)
					{
						FileList.Add(d.ToString() + file.Name);
					}
				}

				string requestFilePath = data.RequestDocumentPath;

				List<string> requestFileList = new List<string>();

				if (data != null && data.RequestDocumentCount > 0)
				{
					DirectoryInfo d = new DirectoryInfo(requestFilePath);
					FileInfo[] Files = d.GetFiles(); //Getting Text files

					foreach (FileInfo file in Files)
					{
						requestFileList.Add(d.ToString() + file.Name);
					}
				}

				return new
				{
					suspensionData = data,
					DocumentList = FileList,
					RequestDocument = requestFileList
				};
			}
			else
			{
				throw new Exception("No Data Found");
			}
		}

		public async Task<object> ListAccountSuspensionSL(string UserName, int CompanyID, int BranchID, string listtype)
		{
			SqlParameter[] sqlParams = new SqlParameter[]
						{
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@CompanyID", CompanyID),
				new SqlParameter("@BranchID", BranchID),
				new SqlParameter("@ListType", listtype)
						};

			var DataSets = new DataSet();
			
			if (CompanyID == 4)
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionSL]", sqlParams);
			}
			else
			{
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSuspensionIL]", sqlParams);
			}
			
			
			return DataSets.Tables[0];
		}

		public async Task<object> SaveUpdateAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, IFormCollection form)
		{

			AccountSuspensionDto data = JsonConvert.DeserializeObject<AccountSuspensionDto>(form["SuspensionData"]);

			string filePath = Utility.FilePath.GetImportedFilesPath() + "AccountSuspensionSupportingDocument\\" ;

			if (CompanyID == 4) filePath = filePath + "SL\\";
			else if (CompanyID == 3) filePath = filePath + "IL\\";
			else filePath = filePath + "AML\\";

			filePath = filePath + data.ContractID + "\\" + DateTime.Now.ToString("yymmddhhmm") + "\\";

			DynamicParameters SpParameters = new DynamicParameters();

			foreach (var item in form.Files)
			{

				bool exists = System.IO.Directory.Exists(filePath);
				if (!exists) System.IO.Directory.CreateDirectory(filePath);

				using (Stream fileStream = new FileStream(filePath + item.FileName, FileMode.Create))
				{
					item.CopyTo(fileStream);
				}
			}

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@AgrSuspensionID", data.AgrSuspensionID);
			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@Reason", data.Reason);
			SpParameters.Add("@Remarks", data.Remarks);
			SpParameters.Add("@CDBLStatus", data.CDBLStatus);
			SpParameters.Add("@SuspensionDate", data.SuspensionDate == null ? DateTime.Now : data.SuspensionDate);
			SpParameters.Add("@RestrictionStatus", data.RestrictionStatus);
			SpParameters.Add("@DocumentCount", form.Files.Count);
			SpParameters.Add("@DocumentPath", filePath);
			SpParameters.Add("@UploadMode", "MANUAL");
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if (CompanyID == 4) return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateAccountSuspensionDetailSL", SpParameters);
			else  return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateAccountSuspensionDetailIL", SpParameters);
		}

		public async Task<object> ApproveAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, AccountSuspensionApprovalDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@AgrSuspensionIDs", data.AgrSuspensionIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if (CompanyID == 4) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountSuspensionSL", SpParameters);
			else if (CompanyID == 3) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountSuspensionIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccountSuspensionAML", SpParameters);
		}

		public async Task<object> BulkUploadAccountSuspensionValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList)
		{
			foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(accountList));

			var DataSets = new DataSet();
			if(CompanyID ==4)
			DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionDetailBulkValidationSL]", sqlParams);
			else
			DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionDetailBulkValidationIL]", sqlParams);


			return DataSets.Tables[0];
		}

		public async Task<object> BulkUploadAccountSuspension(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList)
		{
			foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(accountList));



			var DataSets = new DataSet();

			if(CompanyID==4)
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionDetailBulkSL]", sqlParams);
			else
				DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountSuspensionDetailBulkIL]", sqlParams);

			return DataSets.Tables[0].Rows[0][0].ToString();
		}

		#endregion Suspension
	}
}
