using Dapper;
using Model.DTOs.CMMerchantBank;
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
    public class CMMerchantBankRepository : ICMMerchantBankRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public CMMerchantBankRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<string> AddUpdate(MerchantBankOraganisationDetailDto data, string userName)
        {
            //INSERT NEW MERCHANT BANK
            if (data.MerchantBankID == 0 || data.MerchantBankID == null)
            {
                string sp = "CM_InsertMerchantBank";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@MerchantBankID", data.MerchantBankID);
                SpParameters.Add("@MerchantBankName", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);

                SpParameters.Add("@Type_PanelBrokerList", ListtoDataTableConverter.ToDataTable(data.PanelBrokerList).AsTableValuedParameter("Type_PanelBroker"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            //UPDATE MERCHANT BANK
            else
            {
                string sp = "CM_UpdateMerchantBank";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@MerchantBankID", data.MerchantBankID);
                SpParameters.Add("@MerchantBankName", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);

                SpParameters.Add("@Type_PanelBrokerList", ListtoDataTableConverter.ToDataTable(data.PanelBrokerList).AsTableValuedParameter("Type_PanelBroker"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MerchantBankOraganisationDetailDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public MerchantBankOraganisationDetailDto GetById(int Id, string user)
        {
            MerchantBankOraganisationDetailDto OrgDetail = new MerchantBankOraganisationDetailDto();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@OrganizationID", Id),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_MerchantBankOrganisationDetail]", sqlParams);

            OrgDetail = CustomConvert.DataSetToList<MerchantBankOraganisationDetailDto>(DataSets.Tables[0]).First();

            OrgDetail.ContactPersonList = CustomConvert.DataSetToList<MerchantBankOraganisationMembershipDto>(DataSets.Tables[1]);

            OrgDetail.PanelBrokerList = CustomConvert.DataSetToList<MerchanntBankPanelBrokerDto>(DataSets.Tables[2]);

            OrgDetail.BankAccountList = CustomConvert.DataSetToList<MerchanntBankBankAccountDto>(DataSets.Tables[3]);

            return OrgDetail;
        }

        public async Task<List<MerchantBankListDto>> GetCMMerchantBankList(int PageNo, int PerPage, string SearchKeyword)
        {
            var values = new { PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
            return await _dbCommonOperation.ReadSingleTable<MerchantBankListDto>("[CM_ListCMMerchantBank]", values);
        }
    }
}
