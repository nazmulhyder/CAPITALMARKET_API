using Dapper;
using Model.DTOs.UpdateLog;
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
    public class UpdateLogRepository : IUpdateLogRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public UpdateLogRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<List<UpdateLogPresentDetailDto>> getUpdateLogDetailList(int logid)
        {
            try
            {

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                new SqlParameter { ParameterName = "@IsMaster", Value = false },
                new SqlParameter { ParameterName = "@UpdateLogIDORDataKeyID", Value = logid },
                new SqlParameter { ParameterName = "@UpdateUnitID", Value = 0 }
                };

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[GETUpdateLogList]", parameters.ToArray());

                List<UpdateLogPresentDetailDto> updates = CustomConvert.DataSetToList<UpdateLogPresentDetailDto>(DataSets.Tables[0]).ToList();

                return updates;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UpdateLogMasterDto>> getUpdateLogList(int UpdateLogID, int UpdateUnitID)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                new SqlParameter { ParameterName = "@IsMaster", Value = true },
                new SqlParameter { ParameterName = "@UpdateLogIDORDataKeyID", Value = UpdateLogID },
                new SqlParameter { ParameterName = "@UpdateUnitID", Value = UpdateUnitID }
                };

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[GETUpdateLogList]", parameters.ToArray());

                List<UpdateLogMasterDto> updates = CustomConvert.DataSetToList<UpdateLogMasterDto>(DataSets.Tables[0]).ToList();

                return updates;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveUpdateLog(List<UpdateLogDetailDto> logs, string username, int masterID, int UnitID)
        {
            string sp = "dbo.InsertUpdateLog";
            try
            {
                if (logs.Count > 0)
                {
                    //CREADING UPDATELOG MASTER TABLE INSERT CALL
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UpdateUnitID", UnitID); //for module 2 instrument unit//check UpdateUnit Table
                    parameters.Add("@DataKeyID", masterID);
                    parameters.Add("@Maker", username);
                    parameters.Add("@ApprovalReqSetID", "0");
                    parameters.Add("@UpdateLogID", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    var result = await _dbCommonOperation.InsertUpdateBySP(sp, parameters);

                    foreach (var item in logs) item.UpdateLogID = Convert.ToInt32(result);

                    DynamicParameters DetailParameters = new DynamicParameters();
                    DetailParameters.Add("@UpdateLogDetl", ListtoDataTableConverter.ToDataTable(logs).AsTableValuedParameter("Type_UpdateLogDetl"));
                    sp = "InsertUpdateLogDetail";
                    result = await _dbCommonOperation.InsertUpdateBySP(sp, DetailParameters);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
