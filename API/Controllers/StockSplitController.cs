using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.Approval;
using Model.DTOs.StockSplit;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockSplitController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<StockSplitController> _logger;
        public StockSplitController(IService service, ILogger<StockSplitController> logger)
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


        [HttpGet("InstrumentHoldings/{CompanyID}/{BranchID}/{InstrumentID}/{SplitRatio}")]
        public async Task<IActionResult> InstrumentHolding(int CompanyID, int BranchID, int InstrumentID, decimal SplitRatio)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.stockSplitRepository.InstrumentHolding(CompanyID, BranchID, InstrumentID, SplitRatio));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveStockSplit/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveStockSplit(int CompanyID, int BranchID, StockSplitSetting entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.stockSplitRepository.SaveStockSplit(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetStockSplit/{CompanyID}/{BranchID}/{StockSplitSettingID}")]
        public async Task<IActionResult> GetStockSplit(int CompanyID, int BranchID, int StockSplitSettingID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.stockSplitRepository.GetStockSplit(CompanyID, BranchID, LoggedOnUser(), StockSplitSettingID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> StockSplitSettingList(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.stockSplitRepository.StockSplitSettingList(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApprovalStockSplit/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ApprovalStockSplit(int CompanyID, int BranchID, StockSplitApprovalDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.stockSplitRepository.StockSplitApproval( LoggedOnUser(),CompanyID, BranchID, entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
