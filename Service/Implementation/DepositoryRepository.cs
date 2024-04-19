using Dapper;
using Model.DTOs.CMExchange;
using Model.DTOs.Depository;
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
    public class DepositoryRepository : IDepositoryRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public DepositoryRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> AddUpdate(CMDepositoryDTO entityDto, string userName)
        {
            try
            {

                #region Insert New Data

                if (entityDto.DepositoryID == 0 || entityDto.DepositoryID == null)
                {
                    string sp = "CM_InsertDepository";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@CompanyName", entityDto.CompanyName);
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


                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateDepository";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
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

        public Task<List<CMDepositoryDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<CMDepositoryDTO>("[CM_ListDepository]", values);
        }

        public CMDepositoryDTO GetById(int Id, string user)
        {
            CMDepositoryDTO cmeDepositoryDTO = new CMDepositoryDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@DepositoryID", Id),
                new SqlParameter("@UserName",  user),
            };

            var DataSets =  _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryDepository]", sqlParams);

            cmeDepositoryDTO = CustomConvert.DataSetToList<CMDepositoryDTO>(DataSets.Tables[0]).First();
            cmeDepositoryDTO.ContactPersons = CustomConvert.DataSetToList<ContactPersonDTO>(DataSets.Tables[1]);

            return cmeDepositoryDTO;
        }
    }
}
