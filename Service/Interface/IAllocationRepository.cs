using Model.DTOs.Allocation;
using Model.DTOs.AssetManager;
using Model.DTOs.SaleOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAllocationRepository
    {
        #region SALE ORDER ALLOCATION
        public Task<List<SaleAllocationBatchInfoDto>> GetAllocationSaleOrderBatchInfo(string userName, int CompanyID, int BranchID);
        public Task<List<SaleOrderTradeDto>> GetAllSaleOrderTrade(string userName, int CompanyID, int BranchID, int SaleOrderID);
        //public Task<List<AccountSaleOrderAllocationDto>> GetAllSaleOrderAllocationAccounts(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades);
        public Task<object> GetAllSaleOrderAllocationAccounts(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades);
        //public Task<List<AccountSaleOrderAllocation_v2_Dto>> GetAllSaleOrderAllocationAccounts_v3(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades);
        public Task<string> SaveSaleOrderAllocation(int CompanyID,int BranchID,string userName, SaveSaleOrderAllcoationDto SaleOrderAllocation);     
        #endregion

        #region BUY ORDER ALLOCATION
        public Task<List<BuyAllocationBatchInfoDto>> GetAllocationBuyOrderBatchInfo(string userName, int CompanyID, int BranchID);
        public Task<List<BuyOrderTradeDto>> GetAllBuyOrderTrade(string userName, int CompanyID, int BranchID, int BuyOrderID);
        //public Task<List<AccountBuyOrderAllocationDto>> GetAllBuyOrderAllocationAccounts(int CompanyID, int BranchID, List<BuyOrderTradeDto> BuyOrderTrades);
        public Task<object> GetAllBuyOrderAllocationAccounts(int CompanyID, int BranchID, List<BuyOrderTradeDto> BuyOrderTrades);
        public Task<string> SaveBuyOrderAllocation(int CompanyID, int BranchID, string userName, SaveBuyOrderAllcoationDto BuyOrderAllocation);
        #endregion

        #region Sale Allocation Approval
        public Task<List<SaleAllocationApprovalListDto>> GetAllSaleOrderAllocation(string userName, int CompanyID, int BranchID);
        public Task<string> AllocationApproval(string userName, AllocationApprovalDto approvalDto);
        public Task<List<SaleAllocationApprovalDetailListDto>> GetAllSaleOrderAllocationDetails(int SaleAllocationID);
        #endregion

        #region Buy Allocation Approval
        public Task<List<BuyAllocationApprovalListDto>> GetAllBuyOrderAllocation(string userName, int CompanyID, int BranchID);
        public Task<List<BuyAllocationApprovalDetailListDto>> GetAllBuyOrderAllocationDetails(int BuyAllocationID);
        #endregion
        public Task<string> DeleteSellBuyOrderByBatch(string userName, int CompanyID, int BranchID, int OrderID, string OrderType);

    }
}
