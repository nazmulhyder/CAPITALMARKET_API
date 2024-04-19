using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.BrokerageCommisionAccountGroup;
using System.Data;
using System.Data.SqlClient;
using Model.DTOs.BrokerageCommision;
using Utility;

namespace Service.Implementation
{
    public class BrokerageCommisionAccountGroupRepository : IBrokerageCommisionAccountGroupRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public BrokerageCommisionAccountGroupRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
        public Task<string> AddUpdate(BrokerageCommisionAccountGroupDto entity, string userName)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrokerageCommisionAccountGroupDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }
        public async Task<string> ApproveBrokerageCommisionAccountGroup(BrokerageCommisionAccountGroupApproveDto Approval, string LoggedOnUser)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@AccountGroupID", Approval.AccountGroupID);
            SpParameters.Add("@UserName", LoggedOnUser);
            SpParameters.Add("@CompanyID", Approval.CompanyID);
            SpParameters.Add("@BranchID", Approval.BranchID);
            SpParameters.Add("@ApprovalStatus", Approval.ApprovalStatus);
            SpParameters.Add("@ApprovalFeedback", Approval.ApprovalFeedback);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveBrokerageCommisionAccountGroup", SpParameters);
        }


        public Task<List<BrokerageCommisionAccountGroupDto>> GetBrokerageCommisionAccountGroupList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@ApprovalStatus", ApprovalStatus),
                new SqlParameter("@PageNo", PageNo),
                new SqlParameter("@Perpage", Perpage),
               new SqlParameter("@SearchKeyword", SearchKeyword)
            };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_ListBrokerageCommisionAccountGroup", sqlParams);

            List<BrokerageCommisionAccountGroupDto> user = CustomConvert.DataSetToList<BrokerageCommisionAccountGroupDto>(DataSets.Tables[0]).ToList();

            //CM_ListBrokerageCommisionAccountGroup
            return Task.FromResult(user);
        }

        public Task<List<BrokerageCommisionAccountGroupDto>> GetUnapprovedBrokerageCommisionAccountGroupList(int CompanyID, string UserName, int PageNo, int Perpage, string SearchKeyword)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@ApprovalStatus", "Unapproved"),
                new SqlParameter("@PageNo", PageNo),
                new SqlParameter("@Perpage", Perpage),
               new SqlParameter("@SearchKeyword", SearchKeyword)
            };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_ListBrokerageCommisionAccountGroup", sqlParams);

            List<BrokerageCommisionAccountGroupDto> user = CustomConvert.DataSetToList<BrokerageCommisionAccountGroupDto>(DataSets.Tables[0]).ToList();

            //CM_ListBrokerageCommisionAccountGroup
            return Task.FromResult(user);
        }

        public BrokerageCommisionAccountGroupDto GetById(int Id, string user)
        {
            throw new NotImplementedException();
        }

        public Task<BrokerageCommisionAccountGroupItemDto> GetBrokerageCommisionAccountGroupItem(int AccountGroupID, string UserName)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
          {
                new SqlParameter("@AccountGroupID", AccountGroupID),
                new SqlParameter("@UserName", UserName)

          };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_QryBrokerageCommisionAccountGroup", sqlParams);

            BrokerageCommisionAccountGroupItemDto commision = CustomConvert.DataSetToList<BrokerageCommisionAccountGroupItemDto>(DataSets.Tables[0]).FirstOrDefault();

            commision.ChargeList = CustomConvert.DataSetToList<BrokerageCommisionAccountGroupItemRateDto>(DataSets.Tables[1]).ToList();

            var AllSlab = CustomConvert.DataSetToList<BrokerageCommisionAccountGroupItemSlabDto>(DataSets.Tables[2]).ToList();

            foreach (var item in commision.ChargeList) item.SlabList = AllSlab.Where(s => s.AccountGroupChargeID == item.AccountGroupChargeID).ToList();

            //CM_ListBrokerageCommision
            return Task.FromResult(commision);
        }

        public async Task<string> UpdateBrokerageCommisionAccountGroupItem(BrokerageCommisionAccountGroupItemDto UpdateCommision, string UserName, int CompanyID, int BranchID)
        {
            //BrokerageCommisionDto PrevCommision = GeBrokerageCommisionItem(UpdateCommision.AccountNumber, "").Result;

            List<BrokerageCommisionAccountGroupItemRateUpdateDto> RateList = UpdateCommision.ChargeList.Select(x => new BrokerageCommisionAccountGroupItemRateUpdateDto()
            { AccountGroupChargeID = x.AccountGroupChargeID,  ChargeAmount = x.ChargeAmount,  HasSlab = x.HasSlab, AttributeName = x.AttributeName, ProductAttributeID = x.ProductAttributeID })
          .ToList();

            List<BrokerageCommisionAccountGroupItemSlabDto> SlabList = new List<BrokerageCommisionAccountGroupItemSlabDto>();

            foreach (var item in UpdateCommision.ChargeList) SlabList.AddRange(item.SlabList);


            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID",CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@AccountGroupID", UpdateCommision.AccountGroupID);
            SpParameters.Add("@ApprovalStatus", UpdateCommision.ApprovalStatus);

            SpParameters.Add("@BCAccGroupChargeList", ListtoDataTableConverter.ToDataTable(RateList).AsTableValuedParameter("Type_GenAccGroupCharge"));
            SpParameters.Add("@@BCAccGroupChargeSlab", ListtoDataTableConverter.ToDataTable(SlabList).AsTableValuedParameter("Type_GenAccGroupChargeSlab"));

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var res = await _dbCommonOperation.InsertUpdateBySP("CM_UpdateBrokerageCommisionAccGroup", SpParameters);

            return res;
        }

       
    }
}
