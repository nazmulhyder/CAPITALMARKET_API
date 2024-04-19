using Dapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Broker;
using Model.DTOs.Charges;
using Model.DTOs.TradeFileUpload;
using Model.DTOs.TradeSettlement;
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
    public class TradeSettlementRepository : ITradeSettlementRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TradeSettlementRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<List<InstrumentWiseTradeDataDto>> GetForeignTradeDataList(string UserName, int CompanyID, int BranchID,string AccountNumner, string TradeDate)
        {
            List<InstrumentWiseTradeDataDto> list = new List<InstrumentWiseTradeDataDto>();

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@AccountNumber", AccountNumner);
            sqlParams[4] = new SqlParameter("@TradeDate", TradeDate);
           
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListTradeDataForForeginAllocation]", sqlParams);

            list = CustomConvert.DataSetToList<InstrumentWiseTradeDataDto>(DataSets.Tables[0]).ToList();

            List <TradeAllocationDto> AccountList = CustomConvert.DataSetToList<TradeAllocationDto>(DataSets.Tables[1]).ToList();

            foreach(var item in list) { item.AllocationAccountList = AccountList; }

            return list;
        }

		public async Task<string> SaveForeignTradeDataAllocationAccountWise(string UserName, int CompanyID, int BranchID, List<ForeginTradeAllocationDto> data)
		{
            foreach(var item in data)
            {
                item.SettlementDate = Utility.DatetimeFormatter.DateFormat(item.SettlementDate);
            }
			DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@AllocationList", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_ForeginTradeAllocationAccountWise"));
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertForeginTradeAllocationAccountWiseSL", SpParameters);
        }

        public async Task<string> SaveForeignTradeDataAllocation(string UserName, int CompanyID, int BranchID, InstrumentWiseTradeDataDto data)
        {
            List<TradeAllocationDto> allocationList = new List<TradeAllocationDto>();

            foreach (var allocation in data.AllocationAccountList)
            {
                allocation.TradeTransactionID = data.TradeTransactionID;
                allocation.TradeAmount = allocation.TradeQuantity * allocation.UnitPrice;
				allocation.SettlementDate = Utility.DatetimeFormatter.DateFormat(allocation.SettlementDate);
				allocationList.Add(allocation);
            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AllocationList", ListtoDataTableConverter.ToDataTable(allocationList).AsTableValuedParameter("Type_ForeginTradeAllocation"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertForeginTradeAllocationSL", SpParameters);
        }

        public async Task<List<InstrumentWiseTradeDataDto>> GetForeignTradeAllocationList(string UserName, int CompanyID, int BranchID, string ListType)
        {
            List<InstrumentWiseTradeDataDto> list = new List<InstrumentWiseTradeDataDto>();

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", ListType);
            sqlParams[4] = new SqlParameter("@TradeTransactionID", 0.ToString());

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListForeignTradeAllocation]", sqlParams);

            
            list = CustomConvert.DataSetToList<InstrumentWiseTradeDataDto>(DataSets.Tables[0]).ToList();

            List<TradeAllocationDto> AccountList = CustomConvert.DataSetToList<TradeAllocationDto>(DataSets.Tables[1]).ToList();

            foreach (var item in list) { item.AllocationAccountList = AccountList; }

            return list;
        }
        public async Task<string> ApproveForeignTradeAllocation(string UserName, int CompanyID, int BranchID, ForeignTradeAllocationApproveDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@TradeTransactionIDs", data.TradeTransactionIDs);
            SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
            SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveForeignTradeAllocation", SpParameters);
        }

        public async Task<InstrumentWiseTradeDataDto> GetForeignTradeAllocation(string UserName, int CompanyID, int BranchID, int TradeTransactionID)
        {
            InstrumentWiseTradeDataDto DATA = new InstrumentWiseTradeDataDto();

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ListType", "");
            sqlParams[4] = new SqlParameter("@TradeTransactionID", TradeTransactionID.ToString());

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListForeignTradeAllocation]", sqlParams);


            DATA = CustomConvert.DataSetToList<InstrumentWiseTradeDataDto>(DataSets.Tables[0]).FirstOrDefault();

            DATA.AllocationAccountList = CustomConvert.DataSetToList<TradeAllocationDto>(DataSets.Tables[1]).ToList().Where(c=>c.TradeTransactionID == TradeTransactionID).ToList();
           
            return DATA;
        }
    }

}
