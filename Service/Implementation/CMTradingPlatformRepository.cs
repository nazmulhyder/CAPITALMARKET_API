using Dapper;
using Model.DTOs.TradingPlatform;
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
    public class CMTradingPlatformRepository : ICMTradingPlatformRepository
    {

        public readonly IDBCommonOpService _dbCommonOperation;
        public CMTradingPlatformRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> AddUpdate(CMTradingPlatformDTO entityDto, string userName)
        {
            //NEW INSERT
            if (entityDto.TradingPlatformID == 0 || entityDto.TradingPlatformID == null)
            {
                string sp = "CM_InsertTradingPlatform";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@PlatformName", entityDto.PlatformName);
                SpParameters.Add("@ShortName", entityDto.ShortName);
                SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            //UPDATE
            else
            {
                string sp = "CM_UpdateTradingPlatform";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@TradingPlatformID", entityDto.TradingPlatformID);
                SpParameters.Add("@PlatformName", entityDto.PlatformName);
                SpParameters.Add("@ShortName", entityDto.ShortName);
                SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CMTradingPlatformDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<CMTradingPlatformDTO>("[CM_ListTradingPlatform]", values);
        }

        public CMTradingPlatformDTO GetById(int Id, string user)
        {
            CMTradingPlatformDTO cmeTradingPlatformDTO = new CMTradingPlatformDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@TPID", Id),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryTradingPlatform]", sqlParams);
            cmeTradingPlatformDTO = CustomConvert.DataSetToList<CMTradingPlatformDTO>(DataSets.Tables[0]).First();
            return cmeTradingPlatformDTO;
        }

        public Task<List<CMTradingPlatformDTO>> GetCMTradingPlatformByExchangeID(int ExchangeID)
        {
            var values = new { ExchangeID = ExchangeID };
            return _dbCommonOperation.ReadSingleTable<CMTradingPlatformDTO>("[CM_QueryTradingPlatformList]", values);
        }
    }
}
