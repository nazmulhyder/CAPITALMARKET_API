using Dapper;
using Model.DTOs.BrokerageCommision;
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
    public class BrokerageCommisionRepository : IBrokerageCommisionRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public BrokerageCommisionRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<string> ApproveTradeDataUpdateForCommisionUpdate(string UserName, ApproveTradeDataCommisionUpdatDto approveData)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@ContractID", approveData.ContractID);
            SpParameters.Add("@TradeDate", approveData.TradeDate);
            SpParameters.Add("@ApprovalRemark", approveData.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveSLTradeCommissionChanged", SpParameters);
        }
        public async Task<List<TradeDataCommisionUpdateContractListDto>> GetTradeDataCommisionUpdateContractListDto(string UserName, DateTime TradeDate)
        {
            var values = new {TradeDate = TradeDate };
            return await _dbCommonOperation.ReadSingleTable<TradeDataCommisionUpdateContractListDto>("[CM_ListSLTradeCommissionChanged]", values);
        }

        public async Task<List<TradeDataForCommisionUpdateDto>> GetTradeDataForCommisionUpdate(string UserName,int ContractID, DateTime TradeDate)
        {
            var values = new { ContractID = ContractID, TradeDate = TradeDate};
            return await _dbCommonOperation.ReadSingleTable<TradeDataForCommisionUpdateDto>("[CM_QrySLTradeDataForCommissionUpdate]", values);
        }

        public async Task<string> ApproveBrokerageCommision(BrokerageCommisionApproveDto Approval,string LoggedOnUser)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@ContractID", Approval.ContractID);
            SpParameters.Add("@UserName", LoggedOnUser);
            SpParameters.Add("@CompanyID", Approval.CompanyID);
            SpParameters.Add("@BranchID", Approval.BranchID);
            SpParameters.Add("@ApprovalStatus", Approval.ApprovalStatus);
            SpParameters.Add("@ApprovalFeedback", Approval.ApprovalFeedback);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveBrokerageCommision", SpParameters);
        }

        public async Task<string> UpdateBrokerageCommisionItem(BrokerageCommisionItemDto UpdateCommision,string UserName, int CompanyID, int BranchID)
            {
            //BrokerageCommisionDto PrevCommision = GeBrokerageCommisionItem(UpdateCommision.AccountNumber, "").Result;

            List<BrokerageCommisionItemRateUpdateDto> RateList = UpdateCommision.ChargeList.Select(x => new BrokerageCommisionItemRateUpdateDto()
          { AgreementChargeID = x.AgreementChargeID,AttributeName = x.AttributeName,ChargeAmount = x.ChargeAmount, DerivedFrom = x.DerivedFrom, HasSlab = x.HasSlab })
          .ToList();

            List<BrokerageCommisionItemSlabDto> SlabList = new List<BrokerageCommisionItemSlabDto>();

            foreach (var item in UpdateCommision.ChargeList) SlabList.AddRange(item.SlabList);


        DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", UpdateCommision.CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ContractID", UpdateCommision.ContractID);
            SpParameters.Add("@ApprovalStatus", UpdateCommision.ApprovalStatus);

            SpParameters.Add("@BCAgreementChargeList", ListtoDataTableConverter.ToDataTable(RateList).AsTableValuedParameter("Type_GenAgreementCharge"));
            SpParameters.Add("@BCAgreementChargeSlab", ListtoDataTableConverter.ToDataTable(SlabList).AsTableValuedParameter("Type_GenAgreementChargeSlab"));

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var res = await _dbCommonOperation.InsertUpdateBySP("CM_UpdateBrokerageCommision", SpParameters);

            return res;
        }
        public Task<BrokerageCommisionItemDto> GeBrokerageCommisionItem(int ContractID, string UserName)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
          {
                new SqlParameter("@ContractID", ContractID),
                new SqlParameter("@UserName", UserName)
             
          };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_QryBrokerageCommision", sqlParams);

            BrokerageCommisionItemDto commision = CustomConvert.DataSetToList<BrokerageCommisionItemDto>(DataSets.Tables[0]).FirstOrDefault();

            commision.ChargeList = CustomConvert.DataSetToList<BrokerageCommisionItemRateDto>(DataSets.Tables[1]).ToList();

            var AllSlab = CustomConvert.DataSetToList<BrokerageCommisionItemSlabDto>(DataSets.Tables[2]).ToList();

            foreach (var item in commision.ChargeList) item.SlabList = AllSlab.Where(s => s.AgreementChargeID == item.AgreementChargeID).ToList();

            //CM_ListBrokerageCommision
            return Task.FromResult(commision);
        }

        public Task<List<BrokerageCommisionDto>> GeBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword)
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
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_ListBrokerageCommision", sqlParams);

           List<BrokerageCommisionDto> bc = CustomConvert.DataSetToList<BrokerageCommisionDto>(DataSets.Tables[0]).ToList();

            //CM_ListBrokerageCommision
            return Task.FromResult(bc);
        }
        public Task<string> AddUpdate(BrokerageCommisionDto entity, string userName)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrokerageCommisionDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public BrokerageCommisionDto GetById(int Id, string user)
        {
            throw new NotImplementedException();
        }
    }
}
