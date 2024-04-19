using Model.DTOs.Charges;
using Model.DTOs.Reports;
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
    public class ReportRepository : IReportRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public ReportRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
        public async Task<DataTable> GetData(SqlParameter[] sqlParams, string spname)
        {
            SqlParameter[] Params = new SqlParameter[sqlParams.Length];
            int count = 0;
            foreach(var item in sqlParams)
            {
                Params[count] = item;
                count++;
            }
           
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP(spname, Params);

            return DataSets.Tables[0];
        }

        public async Task<DataSet> GetDataSet(SqlParameter[] sqlParams, string spname)
        {
            SqlParameter[] Params = new SqlParameter[sqlParams.Length];
            int count = 0;
            foreach (var item in sqlParams)
            {
                Params[count] = item;
                count++;
            }

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP(spname, Params);

            return DataSets;
        }

        public async Task<List<MenuWiseReportDto>> MenuWiseReportList(string UserName, int CompanyID, int BranchID, int MenuID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@MenuID", MenuID);
         
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListReport]", sqlParams);

            var ReportList = CustomConvert.DataSetToList<MenuWiseReportDto>(DataSets.Tables[0]).ToList();

            var ParameterList = CustomConvert.DataSetToList<MenuWiseReportParameterDto>(DataSets.Tables[1]).ToList();

            foreach(var item in ReportList) item.ParameterList = ParameterList.Where(c=>c.MenuID == item.MenuID).ToList().OrderBy(a=>a.ParameterOrder).ToList();

            return ReportList;
        }
    }
}
