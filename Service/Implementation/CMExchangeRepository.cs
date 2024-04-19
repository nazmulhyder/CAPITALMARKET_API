using Dapper;
using Model.DTOs.CMExchange;
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
    public class CMExchangeRepository : ICMExchangeRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public CMExchangeRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
        public async Task<string> AddUpdate(CMExchangeDTO entityDto, string userName)
        {
            try
            {

                #region Insert New Data

                if (entityDto.ExchangeID == 0 || entityDto.ExchangeID == null)
                {
                    string sp = "CM_InsertExchange";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@ExchangeName", entityDto.ExchangeName);
                    SpParameters.Add("@ShortName", entityDto.ShortName);
                    SpParameters.Add("@MailingAddress", entityDto.MailingAddress);
                    SpParameters.Add("@DistrictCode", entityDto.DistrictCode);
                    SpParameters.Add("@DivisionCode", entityDto.DivisionCode);
                    SpParameters.Add("@CountryCode", entityDto.CountryCode);
                    SpParameters.Add("@EmailAddress", entityDto.EmailAddress);
                    SpParameters.Add("@WebAddress", entityDto.WebAddress);
                    SpParameters.Add("@PhoneNo", entityDto.PhoneNo);
                    SpParameters.Add("@MobileNo", entityDto.MobileNo);
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@ContactPerson", ListtoDataTableConverter.ToDataTable(entityDto.ContactPersons).AsTableValuedParameter("Type_CMContactPerson"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateExchange";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                    SpParameters.Add("@MailingAddress", entityDto.MailingAddress);
                    SpParameters.Add("@EmailAddress", entityDto.EmailAddress);
                    SpParameters.Add("@WebAddress", entityDto.WebAddress);
                    SpParameters.Add("@DistrictCode", entityDto.DistrictCode);
                    SpParameters.Add("@DivisionCode", entityDto.DivisionCode);
                    SpParameters.Add("@CountryCode", entityDto.CountryCode);
                    SpParameters.Add("@PhoneNo", entityDto.PhoneNo);
                    SpParameters.Add("@MobileNo", entityDto.MobileNo);
                    SpParameters.Add("@UserName", userName);

                    SpParameters.Add("@ContactPerson", ListtoDataTableConverter.ToDataTable(entityDto.ContactPersons).AsTableValuedParameter("Type_CMContactPerson"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
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

        public Task<List<CMExchangeDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {

            var values = new { CompanyID = 1, PageNo = PageNo, PerPage =Perpage, SearchKeyword = SearchKeyword };
            return  _dbCommonOperation.ReadSingleTable<CMExchangeDTO>("[CM_ListExchange]", values);
        }

        public CMExchangeDTO GetById(int Id, string user)
        {
            CMExchangeDTO cmeExchangeDTO = new CMExchangeDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@ExchangeID", Id),
                new SqlParameter("@UserName",  user),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryExchange]", sqlParams);

            cmeExchangeDTO = CustomConvert.DataSetToList<CMExchangeDTO>(DataSets.Tables[0]).First();
            cmeExchangeDTO.ContactPersons = CustomConvert.DataSetToList<ContactPersonDTO>(DataSets.Tables[1]);

            return cmeExchangeDTO;
        }
    }
}
