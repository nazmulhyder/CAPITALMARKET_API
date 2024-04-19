using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradingPlatform;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SLTradeSettlementRuleController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<SLTradeSettlementRuleController> _logger;

        public SLTradeSettlementRuleController(IService service, ILogger<SLTradeSettlementRuleController> logger)
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

        [HttpPost("AddUpdateSLTradeSettlementRule")]
        public async Task<IActionResult> AddUpdateSLTradeSettlementRule(SLTradeSettlementRuleDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.TradeSettlementRules.AddUpdate(currentData, userName));
            }
            catch (Exception ex) 
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
					ex = new Exception("Settlement schedule already exists, please check the list.");
				}
                return getResponse(ex); 
            }
        }


        [HttpGet("List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllSLTradeSettlementRule(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeSettlementRules.GetAllSLTradeSettlementRule(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetSettlementRuleByID/{SettlementRuleID}")]
        public async Task<IActionResult> GetSLTradeSettlementRule(int SettlementRuleID)
        {
            try
            {
                return getResponse(await _service.TradeSettlementRules.GetSLTradeSettlementRuleById(SettlementRuleID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"SettlementRule not found with this id: {SettlementRuleID}";
                return getResponse(new Exception(msg));
            }
        }
    }
}
