using Dapper;
using Model.DTOs.CMStockIndex;
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
    public class CMStockIndexRepository : ICMStockIndexRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public CMStockIndexRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<string> AddUpdate(CMStockIndexDTO entityDto, string userName)
        {
            //NEW INSERT
            if (entityDto.IndexID == 0 || entityDto.IndexID == null)
            {
                string sp = "CM_InsertCMStockIndex";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@IndexName", entityDto.IndexName);
                SpParameters.Add("@ShortName", entityDto.ShortName);
                SpParameters.Add("@Value", entityDto.Value);
                SpParameters.Add("@TradeDate", entityDto.TradeDate);
                SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                SpParameters.Add("@Description", entityDto.Description);
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            //UPDATE
            else
            {
                string sp = "CM_UpdateCMStockIndex";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@@IndexID", entityDto.IndexID);
                SpParameters.Add("@IndexName", entityDto.IndexName);
                SpParameters.Add("@ShortName", entityDto.ShortName);
                SpParameters.Add("@Value", entityDto.Value);
                SpParameters.Add("@TradeDate", entityDto.TradeDate);
                SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                SpParameters.Add("@Description", entityDto.Description);
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CMStockIndexDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return await _dbCommonOperation.ReadSingleTable<CMStockIndexDTO>("[CM_ListCMStockIndex]", values);
        }

        public CMStockIndexDTO GetById(int Id, string user)
        {
            CMStockIndexDTO cmStockIndexDTO = new CMStockIndexDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@IndexID", Id),
            };

            var DataSets =  _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMStockIndex]", sqlParams);

            cmStockIndexDTO = CustomConvert.DataSetToList<CMStockIndexDTO>(DataSets.Tables[0]).First();

            return cmStockIndexDTO;


        }

        public async Task<List<CMStockIndexDTO>> GetCMStockIndexByExchangeID(int ID)
        {
            //CMStockIndexDTO cmStockIndexDTO = new CMStockIndexDTO();

            //SqlParameter[] sqlParams = new SqlParameter[]
            //{
            //    new SqlParameter("@ExchangeID", ID),
            //};

            //var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMStockIndexList]", sqlParams);

            //cmStockIndexDTO = CustomConvert.DataSetToList<CMStockIndexDTO>(DataSets.Tables[0]).First();

            //return cmStockIndexDTO;
            var values = new { ExchangeID = ID };
            return await _dbCommonOperation.ReadSingleTable<CMStockIndexDTO>("[CM_QueryCMStockIndexList]", values);
        }
    }
}
