using Dapper;
using Model.DTOs.Market;
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
    public class CMMarketTypeRepository : ICMMarketTypeRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public CMMarketTypeRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }


        public Task<string> AddUpdate(CMMarketTypeDTO dtodata, string userName)
        {
            try
            {

                #region Insert New Data

                if (dtodata.MarketID == 0 || dtodata.MarketID == null)
                {
                    string sp = "CM_InsertMarketType";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@MarketName", dtodata.MarketName);
                    SpParameters.Add("@ShortName", dtodata.ShortName);
                    SpParameters.Add("@TradingPlatformID", dtodata.TradingPlatformID);
                    SpParameters.Add("@ExchangeID", dtodata.ExchangeID);
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateMarketType";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@MarketID", dtodata.MarketID);
                    SpParameters.Add("@MarketName", dtodata.MarketName);
                    SpParameters.Add("@ShortName", dtodata.ShortName);
                    SpParameters.Add("@TradingPlatformID", dtodata.TradingPlatformID);
                    SpParameters.Add("@ExchangeID", dtodata.ExchangeID);
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CMMarketTypeDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<CMMarketTypeDTO>("[CM_ListMarketType]", values);
        }

        public Task<List<CMMarketTypeListDTO>> GetAllMarketType(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<CMMarketTypeListDTO>("[CM_ListMarketType]", values);
        }

        public CMMarketTypeDTO GetById(int Id, string user)
        {
            throw new NotImplementedException();

        }

        public Task<List<CMMarketTypeDTO>> GetByTradingPlatformID(int TradingPlatformID)
        {
            var values = new { TradingPlatformID = TradingPlatformID };
            return _dbCommonOperation.ReadSingleTable<CMMarketTypeDTO>("[CM_QueryMarketListByTradingPlatformID]", values);
        }

        public async Task<CMMarketTypeListDTO> GetCMMarketTypeById(int MarketTypeID, string user)
        {
            CMMarketTypeListDTO cmMarketTypeListDTO = new CMMarketTypeListDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@MarketID", MarketTypeID),
                new SqlParameter("@UserName",  user),
            };
            var DataSets =  _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryMarketType]", sqlParams);
            cmMarketTypeListDTO =  CustomConvert.DataSetToList<CMMarketTypeListDTO>(DataSets.Tables[0]).First();
            return cmMarketTypeListDTO;
        }
    }
}
