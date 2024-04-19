using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.SaleOrder;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AllocationController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<AllocationController> _logger;
        public AllocationController(IService service, ILogger<AllocationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }


        #region Sale Order Allocation

        [HttpGet("BatchInformationOfSaleOrder/List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BatchInformationOfSaleOrder( int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllocationSaleOrderBatchInfo(LoggedOnUser(),CompanyID,BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("SaleOrderTradeDetail/{SaleOrderID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaleOrderTradeDetail(int SaleOrderID,int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllSaleOrderTrade(LoggedOnUser(), CompanyID, BranchID, SaleOrderID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListSaleOrderAllocationAccounts/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListSaleOrderAllocationAccounts(int CompanyID, int BranchID, List<SaleOrderTradeDto>? SaleOrderAllocationTrades)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllSaleOrderAllocationAccounts(CompanyID, BranchID, SaleOrderAllocationTrades));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        //[HttpPost("ListSaleOrderAllocationAccounts_v2/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> ListSaleOrderAccountsListSaleOrderAccounts_v2(int CompanyID, int BranchID, List<SaleOrderTradeDto>? SaleOrderAllocationTrades)
        //{
        //    try
        //    {
        //        return getResponse(await _service.Allocation.GetAllSaleOrderAllocationAccounts_v3(CompanyID, BranchID, SaleOrderAllocationTrades));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}


        [HttpPost("SaveSaleOrderAllocation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveSaleOrderAllocation(int CompanyID, int BranchID, SaveSaleOrderAllcoationDto? SaleOrderAllocation)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.Allocation.SaveSaleOrderAllocation(CompanyID, BranchID,LoggedOnUser(), SaleOrderAllocation));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("DeleteSellBuyOrderByBatch/{CompanyID}/{BranchID}/{OrderID}/{OrderType}")]
        public async Task<IActionResult> DeleteSellBuyOrderByBatch(int CompanyID, int BranchID, int OrderID, string OrderType)
        {
            try
            {
                return getResponse(await _service.Allocation.DeleteSellBuyOrderByBatch(LoggedOnUser(), CompanyID, BranchID, OrderID, OrderType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        #endregion

        #region Buy Order Allocation
        [HttpGet("BatchInformationOfBuyOrder/List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BatchInformationOfBuyOrder( int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllocationBuyOrderBatchInfo(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("BuyOrderTradeDetail/{CompanyID}/{BranchID}/{BuyOrderID}")]
        public async Task<IActionResult> BuyOrderTradeDetail(int CompanyID, int BranchID, int BuyOrderID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllBuyOrderTrade(LoggedOnUser(),CompanyID,BranchID, BuyOrderID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListBuyOrderAllocationAccounts/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListBuyOrderAllocationAccounts(int CompanyID, int BranchID, List<BuyOrderTradeDto>? BuyOrderAllocationTrades)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllBuyOrderAllocationAccounts(CompanyID, BranchID, BuyOrderAllocationTrades));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
     
        //[HttpPost("ListBuyOrderAllocationAccounts_v2/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> ListBuyOrderAccountsListBuyOrderAccounts_v2(int CompanyID, int BranchID, List<BuyOrderTradeDto>? BuyOrderAllocationTrades)
        //{
        //    try
        //    {
        //        return getResponse(await _service.Allocation.GetAllBuyOrderAllocationAccounts_v2(CompanyID, BranchID, BuyOrderAllocationTrades));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        [HttpPost("SaveBuyOrderAllocation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveBuyOrderAllocation(int CompanyID, int BranchID, SaveBuyOrderAllcoationDto? BuyOrderAllocation)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.Allocation.SaveBuyOrderAllocation(CompanyID, BranchID, LoggedOnUser(), BuyOrderAllocation));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Sale Allocation Approval



        [HttpGet("SaleOrderApprovalList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaleOrderApprovalList( int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllSaleOrderAllocation(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("SaleAllocationApprovalDetail/{SaleAllocationID}")]
        public async Task<IActionResult> BuyOrderApprovalDetail(int SaleAllocationID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllSaleOrderAllocationDetails(SaleAllocationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Buy Allocation Approval
        [HttpGet("BuyOrderApprovalList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BuyOrderApprovalList( int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllBuyOrderAllocation(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("BuyAllocationApprovalDetail/{BuyAllocationID}")]
        public async Task<IActionResult> BuyAllocationApprovalDetail(int BuyAllocationID)
        {
            try
            {
                return getResponse(await _service.Allocation.GetAllBuyOrderAllocationDetails(BuyAllocationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion


        [HttpPost("AllocationApproval")]
        public async Task<IActionResult> AllocationApproval(AllocationApprovalDto approvalRequest)
        {
            try
            {
                return getResponse(await _service.Allocation.AllocationApproval(LoggedOnUser(), approvalRequest));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}
