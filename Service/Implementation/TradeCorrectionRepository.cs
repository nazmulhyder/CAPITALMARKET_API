using Dapper;
using Microsoft.AspNetCore.Http;
using Model.DTOs.PriceFileUpload;
using Model.DTOs.TradeDataCorrection;
using Model.DTOs.TradeFileUpload;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Utility;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Service.Implementation
{
    public class TradeCorrectionRepository : ITradeCorrectionRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TradeCorrectionRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        #region SL Trade
        public async Task<List<TradeDataListForCorrectionDto>> ListTradeDataForCorrection(int CompanyID,string UserName,string AccountNumber,string ExchaneName,DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, UserName = UserName, AccountNumber= AccountNumber, ExchaneName= ExchaneName, TradeDate = TradeDate };

            return await _dbCommonOperation.ReadSingleTable<TradeDataListForCorrectionDto>("[dbo].[CM_ListTradeDataForCorrection]", values);
        }

        public async Task<string> TradeCorrectionEntry(string UserName,List<Type_TradeCorrectionLogDto> data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@TradeDate", data[0].TradeDate);
            SpParameters.Add("@TradeData", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_TradeCorrectionLog"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertSLTradeCorrectionLog", SpParameters);
        }

        public async Task<List<TradeDataListForCorrectionDto>> ListTradeDataCorrectionListForApproval(int CompanyID, string UserName,  DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, UserName = UserName, TradeDate = TradeDate };

            return await _dbCommonOperation.ReadSingleTable<TradeDataListForCorrectionDto>("[dbo].[CM_ListTradeDataCorrectionForApproval]", values);
        }

        public async Task<string> TradeCorrectioApprove(string UserName, ApproveTradeCorrectionDataDto data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@IsApproved", data.IsApproved);
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@TradeDate", data.TradeData[0].TradeDate);
            SpParameters.Add("@TradeData", ListtoDataTableConverter.ToDataTable(data.TradeData).AsTableValuedParameter("Type_TradeCorrectionLog"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveSLTradeCorrectionLog", SpParameters);
        }

      
        #endregion SL Trade


    }
}
