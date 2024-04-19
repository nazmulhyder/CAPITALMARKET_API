using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AccountSettlement;
using Model.DTOs.Broker;
using Model.DTOs.Charges;
using Model.DTOs.NAVFileUpload;
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
	public class NAVRepository : INAVRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;

		public NAVRepository(IDBCommonOpService dbCommonOperation)
		{
			_dbCommonOperation = dbCommonOperation;
		}


		public async Task<object> DeliverMFInstrument(string UserName, int CompanyID, int BranchID, MFReceiveDetailDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@InstrumentID", data.InstrumentID);
			SpParameters.Add("@Quantity", data.ReceiveQuantity);
			
			SpParameters.Add("@PurchaseDate", DateTime.Now);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertMFUnitDelivery", SpParameters);
		}

		public async Task<object> ReceiveMFInstrument(string UserName, int CompanyID, int BranchID, MFReceiveDetailDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@InstrumentID", data.InstrumentID);
			SpParameters.Add("@Quantity", data.ReceiveQuantity);
			SpParameters.Add("@TotalCost", data.Purchaseprice);
			SpParameters.Add("@PurchaseDate", DateTime.Now);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertMFUnitReceive", SpParameters);
		}


		public async Task<object> GetListMFInstrumentForDelivery(string UserName, int CompanyID, int BranchID, string AccountNo)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountNo", AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListClientMFInstrumentForDelivery]", sqlParams);

			MFReceiveMasterDto NewReceive = CustomConvert.DataSetToList<MFReceiveMasterDto>(DataSets.Tables[0]).FirstOrDefault();

			NewReceive.InstrumentList = CustomConvert.DataSetToList<MFReceiveDetailDto>(DataSets.Tables[1]).ToList();

			return NewReceive;
		}
		public async Task<object> GetListMFInstrumentForReceive(string UserName, int CompanyID, int BranchID, string AccountNo)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountNo", AccountNo);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListClientMFInstrumentForReceive]", sqlParams);

			MFReceiveMasterDto NewReceive = CustomConvert.DataSetToList<MFReceiveMasterDto>(DataSets.Tables[0]).FirstOrDefault();

			NewReceive.InstrumentList = CustomConvert.DataSetToList<MFReceiveDetailDto>(DataSets.Tables[1]).ToList();

			return NewReceive;
		}

		#region NAVFile

		public async Task<object> ApproveNavFile(string UserName, int CompanyID, int BranchID, NAVFileApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@NAVFileIDs", data.NAVFileIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApproveStatus", data.ApprovalStatus);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveSLCMNAVFile", SpParameters);
		}

		public async Task<object> SaveNavFile(string UserName, int CompanyID, int BranchID, IFormCollection form)
		{
			List<NAVFileDetailDto> data = JsonConvert.DeserializeObject<List<NAVFileDetailDto>>(form["NavFileData"]);

			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FileName", form.Files[0].FileName.ToString());
			sqlParams[4] = new SqlParameter("@FileDetails", ListtoDataTableConverter.ToDataTable(data));

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertSLCMNAVFile]", sqlParams);

			return DataSets.Tables[0].Rows[0][0].ToString();
		}

		public async Task<object> GetNavFileDetail(string UserName, int CompanyID, int BranchID, int FileID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@NAVFileID", FileID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMNAVFileDetailSL]", sqlParams);

			return new
			{
				NAVFile = DataSets.Tables[0],
				NAVFileDetail = DataSets.Tables[1],
			};
		}

		public async Task<object> GetNavFileList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCMNAVFileSL]", sqlParams);
			
			return DataSets.Tables[0];
		}

		public async Task<object> GetNavFileValidation(string UserName, int CompanyID, int BranchID, List<NAVFileDetailDto> data)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FileDetails", ListtoDataTableConverter.ToDataTable(data));

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListSLCMNAVFileValidation]", sqlParams);

			return DataSets.Tables[0];
		}

		#endregion NAVFile
	}
}
