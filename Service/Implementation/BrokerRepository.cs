using Dapper;
using Model.DTOs.Broker;
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
    public class BrokerRepository : IBrokerRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public BrokerRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> AddUpdate(BrokerOrganisationDetailDto data, string userName)
        {
            //INSERT NEW BROKER
            if (data.BrokerID == 0 || data.BrokerID == null)
            {
                string sp = "CM_InsertCMBroker";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@BrokerID", data.BrokerID);
                SpParameters.Add("@BrokerName", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);
                SpParameters.Add("@BankAccountID", data.BankAccountList.Where(B => B.IsSelectedBankAccount == true).FirstOrDefault().BankAccountID);

                SpParameters.Add("@CMBrokerExchangeMembershipList", ListtoDataTableConverter.ToDataTable(data.ExchangeList).AsTableValuedParameter("Type_CMBrokerExchangeMembership"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            //UPDATE BORKER
            else
            {
                string sp = "CM_UpdateCMBroker";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@BrokerID", data.BrokerID);
                SpParameters.Add("@BrokerName", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);
                SpParameters.Add("@BankAccountID", data.BankAccountList.Where(B => B.IsSelectedBankAccount == true).FirstOrDefault().BankAccountID);

                SpParameters.Add("@CMBrokerExchangeMembershipList", ListtoDataTableConverter.ToDataTable(data.ExchangeList).AsTableValuedParameter("Type_CMBrokerExchangeMembership"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrokerOrganisationDetailDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public BrokerOrganisationDetailDto GetById(int Id, string user)
        {
            BrokerOrganisationDetailDto OrgDetail = new BrokerOrganisationDetailDto();


            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@OrganizationID", Id)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_BrokerOrganisationDetail]", sqlParams);

            OrgDetail = CustomConvert.DataSetToList<BrokerOrganisationDetailDto>(DataSets.Tables[0]).First();

            OrgDetail.AddressList = CustomConvert.DataSetToList<BrokerOrganisationAddressDto>(DataSets.Tables[1]);

            OrgDetail.ContactPersonList = CustomConvert.DataSetToList<BrokerOrganisationContactPerson>(DataSets.Tables[2]);

            OrgDetail.BankAccountList = CustomConvert.DataSetToList<BrokerOrganisationBankAccount>(DataSets.Tables[3]);

            OrgDetail.ExchangeList = CustomConvert.DataSetToList<BrokerOrganisationExchangeMembership>(DataSets.Tables[4]);

            return OrgDetail;
        }

        public async Task<List<BrokerListDto>> GetAllBrokerList(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return await _dbCommonOperation.ReadSingleTable<BrokerListDto>("[CM_ListCMBroker]", values);
        }
    }

}
