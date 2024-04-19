using Model.DTOs.Allocation;
using Model.DTOs.TradeRestriction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITradeRestrictionRepository
    {
        #region Trade Restriction [Product]
        public Task<string> AddUpdate(int CompanyID,int BranchID,TradeRestrictionpProductDto entityDto, string userName);
        public Task<List<ListTradeRestrictionProductDto>> GetAllTradeRestrictionProduct(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        public Task<List<ListTradeRestrictionApprovalProductDto>> GetAllTradeRestrictionApprovalOnProduct(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        public Task<TradeApprovalRestrictionProductDto> QueryTradeRestrictionProduct(int CompanyID, int BranchID, int ProductID, string UserName);

        #endregion

        #region Trade Restriction [Account Group]
        public Task<List<ListTradeRestrictionAccGrpDto>> GetAllTradeRestrictionAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        public Task<string> AddUpdateTradeRestrictionAccGrp(int CompanyID, int BranchID, TradeRestrictionAccGrpDto entityDto, string userName);
        public Task<TradeApprovalRestrictionAccGrpDto> QueryTradeRestrictionAccGrp(int CompanyID, int BranchID, int AccountGroupID, string UserName);
        public Task<List<ListTradeRestrictionApprovalAccGrpDto>> GetAllTradeRestrictionApprovalOnAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        #endregion


        #region Trade Restriction [Account Group]
        public Task<List<ListTradeRestrictionAccountDto>> GetAllTradeRestrictionAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        public Task<string> AddUpdateTradeRestrictionAccount(int CompanyID, int BranchID, TradeRestrictionAccountDto entityDto, string userName);
        public Task<TradeRestrictionAccountDto> QueryTradeRestrictionAccount(int CompanyID, int BranchID, int ContractID, string UserName);
        public Task<TradeApprovalRestrictionAccountDto> QueryApprovalTradeRestrictionAccount(int CompanyID, int BranchID, int ContractID, string UserName);
        public Task<List<ListTradeRestrictionApprovalAccountDto>> GetAllTradeRestrictionApprovalOnAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword);
        #endregion


        public Task<string> RestrictionApproval(string userName,int CompanyID, RestrictionApprovalDto approvalDto);


    }
}
