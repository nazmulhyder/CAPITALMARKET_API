using Microsoft.AspNetCore.Http;
using Model.DTOs.Approval;
using Model.DTOs.Charges;
using Model.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IReportRepository
    {
        public Task<List<MenuWiseReportDto>> MenuWiseReportList(string UserName, int CompanyID, int BranchID, int MenuID);
        public Task<DataTable> GetData(SqlParameter[] sqlParams, string spname);
        public Task<DataSet> GetDataSet(SqlParameter[] sqlParams, string spname);

    }
}
