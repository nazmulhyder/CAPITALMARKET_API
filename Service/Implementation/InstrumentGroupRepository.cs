using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs.Allocation;
using Model.DTOs.InstrumentGroup;
using Newtonsoft.Json.Linq;
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
    public class InstrumentGroupRepository : IInstrumentGroupRepository
    { 
          public readonly IDBCommonOpService _dbCommonOperation;

        public InstrumentGroupRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> AddUpdate(InstrumentGroupDto data, string userName)
        {
           throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<InstrumentGroupDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public InstrumentGroupDto GetById(int Id, string user)
        {
            InstrumentGroupDto instrumentGroup = new InstrumentGroupDto();

            SqlParameter[] sqlParams = new SqlParameter[]
              {
                    new SqlParameter("@InstrumentGroupID", Id)
              };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMInstrumentGroup]", sqlParams);

            instrumentGroup = CustomConvert.DataSetToList<InstrumentGroupDto>(DataSets.Tables[0]).First();

            instrumentGroup.InstrumentList = CustomConvert.DataSetToList<InstrumentGroupDetailDto>(DataSets.Tables[1]);

            return instrumentGroup;
        }

        public async Task<List<InstrumentByInstrumentGrpDto>> InstrumentByInsGrpIds(string InsGrpIds)
        {
            var values = new { InsGrpIds = InsGrpIds };
            return await _dbCommonOperation.ReadSingleTable<InstrumentByInstrumentGrpDto>("[LstInstrumentByInstrumentGrp]", values);
        }

        public async Task<List<InstrumentDropdownDto>> InstrumentDropdown()
        {
            var values = new { SearchKeyword = ""};
            return await _dbCommonOperation.ReadSingleTable<InstrumentDropdownDto>("[List_CMInstrument]", values);
        }

        public async Task<List<InstrumentGroupDropdownDto>> InstrumentGroupDropdown()
        {
            var values = new { SearchKeyword = "" };
            return await _dbCommonOperation.ReadSingleTable<InstrumentGroupDropdownDto>("[CM_DropdownCMInstrumentGroup]", values);
        }

        public async Task<string> InstrumentGroupApproval(string userName, InstrumentGroupApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@InstrumentGroupID", approvalDto.InstrumentGroupID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveInstrumentGroup", SpParameters);
        }

        public Task<List<InstrumentGroupDto>> GetAllInstrumentGrp(string UserName, int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<InstrumentGroupDto>("[CM_ListCMInstrumentGroup]", values);
        }

        public Task<List<InstrumentGroupApprovalListDto>> GetAllInstrumentGrpApproval(string UserName, int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<InstrumentGroupApprovalListDto>("[CM_ListApprovalCMInstrumentGroup]", values);
        }

        public Task<string> AddUpdateInsGrp(InstrumentGroupDto data, string userName, int CompanyID, int BranchID)
        {
            //INSERT NEW InstrumentGroup
            if (data.InstrumentGroupID == 0 || data.InstrumentGroupID == null)
            {
                string sp = "CM_InsertCMInstrumentGroup";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@InstrumentGroupID", data.InstrumentGroupID);
                SpParameters.Add("@GroupName", data.GroupName);
                SpParameters.Add("@GroupDetail", data.GroupDetail);
                SpParameters.Add("@InstrumentGroupDetailList", ListtoDataTableConverter.ToDataTable(data.InstrumentList).AsTableValuedParameter("Type_InstrumentGroupDetail"));
                SpParameters.Add("@UserName",userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            else
            {
                string sp = "CM_UpdateCMInstrumentGroup";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@InstrumentGroupID", data.InstrumentGroupID);
                SpParameters.Add("@GroupName", data.GroupName);
                SpParameters.Add("@GroupDetail", data.GroupDetail);
                SpParameters.Add("@InstrumentGroupDetailList", ListtoDataTableConverter.ToDataTable(data.InstrumentList).AsTableValuedParameter("Type_InstrumentGroupDetail"));
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
        }
    }
}
