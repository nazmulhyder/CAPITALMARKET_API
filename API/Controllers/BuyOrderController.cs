using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.BuyOrder;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class BuyOrderController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<BuyOrderController> _logger;

        public BuyOrderController(IService service, ILogger<BuyOrderController> logger)
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

        [HttpGet("GetBuyOrderPortfolioByProduct/{ProductID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetBuyOrderPortfolioByProduct(int ProductID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.BuyOrder.BuyOrderPortfolioListByProduct(LoggedOnUser(), ProductID, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListBuyOrderAccounts/{companyID}/{BranchID}")]
        public async Task<IActionResult> ListBuyOrderAccounts(int companyID, int BranchID,GenerateBuyOrderAccDto BuyOrderAccData)
        {
            try
            {
                return getResponse(await _service.BuyOrder.GetAllBuyOrderAccounts(companyID, BranchID, BuyOrderAccData));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveBuyOrder/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveBuyOrder(int CompanyID, int BranchID, SaveBuyOrderDto BuyOrder)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.BuyOrder.SaveBuyOrder(userName, CompanyID,BranchID, BuyOrder));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #region Account Wise Buy Order

        [HttpGet("GetBuyOrderAccountWisePortfolio/{ProductID}/{AccountNo}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSaleOrderAccountWisePortfolio(int ProductID, string AccountNo, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.BuyOrder.BuyOrderAcccountWisePortfolioList(LoggedOnUser(), ProductID, AccountNo, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveBuyOrderAccountWise/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveBuyOrderAccountWise( int CompanyID, int BranchID,SaveBuyOrderAccountWise saveSaleOrderOrderAccountWise)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.BuyOrder.SaveBuyOrderAccountWise(LoggedOnUser(), CompanyID , BranchID, saveSaleOrderOrderAccountWise));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion


    }
}
