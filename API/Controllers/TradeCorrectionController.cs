using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeDataCorrection;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TradeCorrectionController : ControllerBase
    {
        private readonly ILogger<ApprovalController> _logger;
        private readonly IService _service;

        public TradeCorrectionController(ILogger<ApprovalController> logger, IService service)
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


        #region SL Trade

        [HttpGet("List/{CompanyID}/{AccountNumber}/{ExchaneName}/{TradeDate}")]
        public async Task<IActionResult> ListTradeDataForCorrection(int CompanyID, string AccountNumber, string ExchaneName, DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.tradeCorrection.ListTradeDataForCorrection(CompanyID,LoggedOnUser(),AccountNumber,ExchaneName,TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Entry")]
        public async Task<IActionResult> TradeCorrectionEntry(List<Type_TradeCorrectionLogDto> data)
        {
            try
            {
                return getResponse(await _service.tradeCorrection.TradeCorrectionEntry(LoggedOnUser(),data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/For/Approval/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> ListTradeDataCorrectionListForApproval(int CompanyID,  DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.tradeCorrection.ListTradeDataCorrectionListForApproval(CompanyID, LoggedOnUser(), TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approve")]
        public async Task<IActionResult> TradeCorrectioApprove(ApproveTradeCorrectionDataDto data)
        {
            try
            {
                return getResponse(await _service.tradeCorrection.TradeCorrectioApprove(LoggedOnUser(), data));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        #endregion SL Trade


    }
}
