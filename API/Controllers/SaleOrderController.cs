using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.DTOs.BuyOrder;
using Model.DTOs.Depository;
using Model.DTOs.SaleOrder;
using Service.Implementation;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleOrderController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<SaleOrderController> _logger;

        public SaleOrderController(IService service, ILogger<SaleOrderController> logger)
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

        [HttpGet("GetSaleOrderPortfolioByProduct/{ProductID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSaleOrderPortfolioByProduct(int ProductID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.SaleOrder.SaleOrderPortfolioListByProduct(LoggedOnUser(),ProductID, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListSaleOrderAccounts/{companyID}/{BranchID}")]
        public async Task<IActionResult> ListSaleOrderAccounts(int companyID, int BranchID,GenerateSaleOrderAccDto SaleOrderAccData)
        {
            try
            {
                 return getResponse(await _service.SaleOrder.GetAllSaleOrderAccounts(companyID, BranchID,SaleOrderAccData));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveSaleOrder")]
        public async Task<IActionResult> SaveSaleOrder(SaveSaleOrderDto SaleOrder)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.SaleOrder.SaveSaleOrder(LoggedOnUser(),SaleOrder));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #region Account Wise Sale Order

        [HttpGet("GetSaleOrderAccountWisePortfolioList/{ProductID}/{AccountNo}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSaleOrderAccountWisePortfolio(int ProductID,string AccountNo, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.SaleOrder.SaleOrderAcccountWisePortfolioList(LoggedOnUser(), ProductID, AccountNo, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveSaleOrderAccountWise/{companyID}/{BranchID}")]
        public async Task<IActionResult> SaveSaleOrderAccountWise(int companyID, int BranchID,SaveSellOrderAccountWise saveOrderOrderAccountWise)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.SaleOrder.SaveSaleOrderAccountWise(LoggedOnUser(),companyID, BranchID, saveOrderOrderAccountWise));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("OrderInstrumentList/{CompanyID}/{BranchID}/{ProductID}")]
        public async Task<IActionResult> OrderGenInstrumentList(int CompanyID, int BranchID, int ProductID)
        {
            try
            {
                return getResponse(await _service.SaleOrder.AllInstrumentPortfolio(LoggedOnUser(), CompanyID, BranchID, ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

    }
}
