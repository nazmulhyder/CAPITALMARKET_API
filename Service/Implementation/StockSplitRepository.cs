using AutoMapper;
using Dapper;
using Model.DTOs.Allocation;
using Model.DTOs.Approval;
using Model.DTOs.BrokerageCommision;
using Model.DTOs.Divident;
using Model.DTOs.StockSplit;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class StockSplitRepository : IStockSplitRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public StockSplitRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        public async Task<List<InstrumentHoldingDto>> InstrumentHolding(int CompanyID, int BranchID, int InstrumentID, decimal SplitRatio)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID, SplitRatio = SplitRatio };
            return await _dbCommonOperation.ReadSingleTable<InstrumentHoldingDto>("CM_GetInstrumentHoldings", values);
        }

        public Task<string> SaveStockSplit(int CompanyID, int BranchID, string userName, StockSplitSetting entry)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@StockSplitSettingID", entry.StockSplitSettingID);
            SpParameters.Add("@SplitRatio", entry.SplitRatio); 
            SpParameters.Add("@NewFaceValue", entry.NewFaceValue);
            SpParameters.Add("@InstrumentID", entry.InstrumentID);
            SpParameters.Add("@StockSplitDetails", ListtoDataTableConverter.ToDataTable(entry.stockSplitDetails).AsTableValuedParameter("Type_StockSplitDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateStockSplitSetting", SpParameters);
        }

        public Task<StockSplitSetting> GetStockSplit(int CompanyID, int BranchID, string userName, int StockSplitSettingID)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@StockSplitSettingID", StockSplitSettingID)

            };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_GetStockSplit", sqlParams);
            StockSplitSetting stockSplitSetting = CustomConvert.DataSetToList<StockSplitSetting>(DataSets.Tables[0]).FirstOrDefault();
            stockSplitSetting.stockSplitDetails = CustomConvert.DataSetToList<StockSplitDetail>(DataSets.Tables[1]).ToList();

            return Task.FromResult(stockSplitSetting);
        }

        public async Task<List<StockSplitSetting>> StockSplitSettingList(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID};
            return await _dbCommonOperation.ReadSingleTable<StockSplitSetting>("CM_GetStockSplitSettingList", values);
        }

        public async Task<string> StockSplitApproval(string userName, int CompanyID, int branchID, StockSplitApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@StockSplitSettingIDs", approvalDto.StockSplitSettingIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveStockSplit", SpParameters);
        }

    }
}
