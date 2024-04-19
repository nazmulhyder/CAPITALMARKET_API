using Dapper;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class DBCommonOpService : IDBCommonOpService
    {
        public readonly IConfiguration _configuration;
        public DBCommonOpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataSet FindMultipleDataSetBySP(string storedProcedureName, SqlParameter[] parameters = null)
        {
            DataSet tablesDataset = new DataSet();

            SqlConnection con = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]);

            SqlCommand cmd = new SqlCommand(storedProcedureName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600;
            foreach (SqlParameter param in parameters)
            {
                if(param.ParameterName == "@ReturnMessage")
                {
                    cmd.Parameters.Add(param.ParameterName, SqlDbType.NVarChar,size:500).Direction = ParameterDirection.Output;
                }
                else
                {
                    cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
                }
                
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(tablesDataset);

            con.Close();

            return tablesDataset;
        }

        public async Task<string> InsertUpdateBySP(string execSql, DynamicParameters parameters = null)
        {
            string response = "";
            try
            {
                using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
                {
                    try
                    {
                        await db.OpenAsync();
                        var result = await db.ExecuteAsync(execSql, parameters,commandTimeout:600, commandType: CommandType.StoredProcedure);

                        try
                        {
                            response = parameters.Get<string>("ReturnMessage");
                        }
                        catch (KeyNotFoundException x)
                        {
                            try
                            {
                                response = parameters.Get<string>("UpdateLogID");
                            }
                            catch (KeyNotFoundException ex)
                            {
                                response = "";
                            }
                            catch (Exception ex)
                            {
                                response = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            response = "";
                        }

                        //returnVal = true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                return response;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> ReadSingleTable<T>(string sql, Object values)
        {
            using (IDbConnection con = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {
                var data =  con.Query<T>(sql, values,commandTimeout:600, commandType: CommandType.StoredProcedure).ToList();
                return data;
            }
        }

        public DataTable ToDataTableUnordered<T>(T item)
        {
            DataTable dataTable = new DataTable();
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties().ToArray();

            List<PropertyInfo> orderedProperties = Props.ToList();

            foreach (PropertyInfo prop in orderedProperties)
            // foreach (var prop in properties)
            {
                if (prop.PropertyType.Namespace.StartsWith("System") && !prop.PropertyType.Namespace.StartsWith("System.Collection"))
                {
                    if (!prop.Name.ToLower().Contains("maker") && !prop.Name.ToLower().Contains("approvalstatus") && !prop.Name.ToLower().Contains("makedate") && !prop.Name.ToLower().Contains("approvalreqsetid") && !prop.Name.ToLower().Contains("utl_"))
                    {
                        DataColumn column = new DataColumn(prop.Name);
                        column.AllowDBNull = true;
                        dataTable.Columns.Add(column);
                    }
                }

            }

            int NoOfColumn = dataTable.Columns.Count, ColumnCount = 0;
            object[] values = new object[(NoOfColumn)];

            // for (int i = 0; (i<= (Props.Length - 1)); i++)
            foreach (var prop in orderedProperties)
            {
                // inserting property values to datatable rows
                if (prop.PropertyType.Namespace.StartsWith("System") && !prop.PropertyType.Namespace.StartsWith("System.Collection"))
                {
                    if (!prop.Name.ToLower().Contains("maker") && !prop.Name.ToLower().Contains("approvalstatus") && !prop.Name.ToLower().Contains("makedate") && !prop.Name.ToLower().Contains("approvalreqsetid") && !prop.Name.ToLower().Contains("utl_"))
                    {
                        if (prop.GetValue(item, null) == null)
                            values[ColumnCount++] = DBNull.Value;
                        else
                            values[ColumnCount++] = prop.GetValue(item, null);

                        //values[ColumnCount++] = prop.GetValue(item, null);
                    }
                }
            }
            dataTable.Rows.Add(values);

            // put a breakpoint here and check datatable
            return dataTable;

        }


        public async Task ExecuteSP(string execSql, DynamicParameters parameters = null)
        {
            string response = "";
            try
            {
                using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
                {
                    try
                    {
                        await db.OpenAsync();
                        var result = await db.ExecuteAsync(execSql, parameters, commandTimeout: 600, commandType: CommandType.StoredProcedure);

                        try
                        {
                            response = parameters.Get<string>("ReturnMessage");
                        }
                        catch (Exception ex)
                        {
                            response = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
