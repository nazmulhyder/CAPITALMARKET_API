using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Charges;
using Model.DTOs.TradeFileExport;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{


    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TradeFileExportController : ControllerBase
    {
        private readonly ILogger<TradeFileExportController> _logger;
        private readonly IService _service;

        public TradeFileExportController(ILogger<TradeFileExportController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            //var userName = WindowsIdentity.GetCurrent().Name;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }


        [HttpGet("ListAccount/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}")]
        public async Task<IActionResult> ListAccount(int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.ListAccount(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNumber));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListAccountGroup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListAccountGroup(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.ListAccountGroup(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        
        [HttpGet("AccountGroupDetail/{CompanyID}/{BranchID}/{AgrGrpID}")]
        public async Task<IActionResult> AccountGroupDetail(int CompanyID, int BranchID, string AgrGrpID)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.AccountGroupDetail(LoggedOnUser(), CompanyID, BranchID, AgrGrpID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateAccountGroup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateAccountGroup(int CompanyID, int BranchID, decimal ChargeAmount, SLAgrGrpTradeExportDto data)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.InsertUpdateAccountGroup(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetExportedTradeFile/{CompanyID}/{BranchID}/{TransactionDate}/{AgrGrpIDs}")]
        public async Task<IActionResult> TradeFileExport(int CompanyID, int BranchID,string TransactionDate, string AgrGrpIDs)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.TradeFileExport(LoggedOnUser(), CompanyID, BranchID, TransactionDate, AgrGrpIDs));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("PayInPayOutFileExport/{CompanyID}/{BranchID}/{ExchangeID}/{SettlementDate}/{FileType}")]
        public async Task<IActionResult> PayInPayOutFileExport(int CompanyID, int BranchID, int ExchangeID, string SettlementDate, string FileType)
        {
            try
            {
                return getResponse(await _service.tradeFileExportRepository.PayInPayOutFileExport(LoggedOnUser(), CompanyID, BranchID, ExchangeID, SettlementDate, FileType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
