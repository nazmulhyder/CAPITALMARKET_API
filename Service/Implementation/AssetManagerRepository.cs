using Dapper;
using Model.DTOs.AssetManager;
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
    public class AssetManagerRepository : IAssetManagerRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public AssetManagerRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> UpdateAccountTradingCodes(List<AMLBrokerDto> data, string userName, int BrokerID)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@BrokerID", BrokerID);
            SpParameters.Add("@AMLACCList", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_AMLFundAccount"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_UpdateAMLFundAccount", SpParameters);

        }

        public async Task<List<AMLBrokerDto>> GetAllAccountTradingCodes(int BrokerID)
        {
            var values = new { BrokerID = BrokerID };

            return await _dbCommonOperation.ReadSingleTable<AMLBrokerDto>("[CM_ListAMLFundAccount]", values);
        }

        public async Task<string> AddUpdate(CMAssetManagerOrganisationDetailDTO data, string userName)
        {
            List<AssetManagerBrokerDto> BrokerList = new List<AssetManagerBrokerDto>();

            List<AMLBrokerDto> aMLBrokers = new List<AMLBrokerDto>();
            foreach (var item in data.BrokerList)
            {
                item.AssetManagerID = data.AssetManagerID;

                foreach (var tc in item.AMLBrokerTradingCodes)
                {
                    tc.BrokerID = item.BrokerID;
                    tc.AssetManagerID = data.AssetManagerID;
                }

                aMLBrokers.AddRange(item.AMLBrokerTradingCodes);

                BrokerList.Add(new AssetManagerBrokerDto
                {
                    BrokerID = item.BrokerID,
                    ActiveStatus = item.ActiveStatus,
                    AMCBrokerID = item.AMCBrokerID,
                    AssetManagerID = item.AssetManagerID,
                    BrokerCodeCSE = item.BrokerCodeCSE,
                    BrokerCodeDSE = item.BrokerCodeDSE,
                    ClearingBOCSE = item.ClearingBOCSE,
                    ClearingBODSE = item.ClearingBODSE,
                    BrokerName = item.BrokerName,
                    BrokerShortName = item.BrokerShortName
                });
            }
            //INSERT NEW BROKER
            if (data.AssetManagerID == 0 || data.AssetManagerID == null)
            {
                string sp = "CM_InsertAssetManager";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@AssetManagerID", data.AssetManagerID);
                SpParameters.Add("@AssetManager", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);
                SpParameters.Add("@BrokerList", ListtoDataTableConverter.ToDataTable(BrokerList).AsTableValuedParameter("Type_AMLBroker"));
                SpParameters.Add("@FundTradingCodeList", ListtoDataTableConverter.ToDataTable(aMLBrokers).AsTableValuedParameter("Type_AMLFundAccount"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            //UPDATE BORKER
            else
            {
                string sp = "CM_UpdateAssetManager";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@AssetManagerID", data.AssetManagerID);
                SpParameters.Add("@AssetManager", data.OrganizationName);
                SpParameters.Add("@ShortName", data.ShortName);
                SpParameters.Add("@OrganizationID", data.OrganizationID);
                SpParameters.Add("@BrokerList", ListtoDataTableConverter.ToDataTable(BrokerList).AsTableValuedParameter("Type_AMLBroker"));
                SpParameters.Add("@FundTradingCodeList", ListtoDataTableConverter.ToDataTable(aMLBrokers).AsTableValuedParameter("Type_AMLFundAccount"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                string Res = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                return Res;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CMAssetManagerOrganisationDetailDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public Task<List<AssetManagerListDto>> GetAllAssetManager(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<AssetManagerListDto>("[CM_ListAssetManager]", values);
        }

        public CMAssetManagerOrganisationDetailDTO GetById(int Id, string user)
        {
            CMAssetManagerOrganisationDetailDTO OrgDetail = new CMAssetManagerOrganisationDetailDTO();


            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@OrganizationID", Id)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_AssetManagerOrganisationDetail]", sqlParams);

            OrgDetail = CustomConvert.DataSetToList<CMAssetManagerOrganisationDetailDTO>(DataSets.Tables[0]).First();

            OrgDetail.ContactPersonList = CustomConvert.DataSetToList<AssetManagerOrganisationContactPersonDto>(DataSets.Tables[1]);

            OrgDetail.BrokerList = CustomConvert.DataSetToList<AssetManagerBrokerListDto>(DataSets.Tables[2]);


            OrgDetail.BankList = CustomConvert.DataSetToList<AssetManagerBankAccountDto>(DataSets.Tables[3]);

            List<AMLBrokerDto> aMLBrokerDtos = CustomConvert.DataSetToList<AMLBrokerDto>(DataSets.Tables[5]);


            foreach(var item in OrgDetail.BrokerList)
            {
                item.AMLBrokerTradingCodes = aMLBrokerDtos.Where(c => c.AMCBrokerID == item.AMCBrokerID && c.BrokerID == item.BrokerID).ToList();
            }

            return OrgDetail;
        }

    }
}
