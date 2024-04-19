using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDBCommonOpService
    {
        public DataSet FindMultipleDataSetBySP(string storedProcedureName, SqlParameter[] parameters = null);
        public Task<string> InsertUpdateBySP(string execSql, DynamicParameters parameters = null);
        public Task<List<T>> ReadSingleTable<T>(string sql, Object values);
        public Task ExecuteSP(string execSql, DynamicParameters parameters = null);
    }
}
