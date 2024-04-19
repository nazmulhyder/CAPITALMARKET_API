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
    public class SLTradeNettingRuleSetController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<SLTradeNettingRuleSetController> _logger;

        public SLTradeNettingRuleSetController(IService service, ILogger<SLTradeNettingRuleSetController> logger)
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

        [HttpPost("AddUpdateSLTradeNettingRuleSet")]
        public async Task<IActionResult> AddUpdateSLTradeNettingRuleSet(SLTradeNettingRuleSetDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.TradeNettingRules.AddUpdate(currentData, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> List(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeNettingRules.GetAll(PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetSLTradeNettingRuleSetByID/{SLTradeNettingRuleSetID}")]
        public IActionResult GetSLTradeNettingRuleSetByID(int SLTradeNettingRuleSetID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = _service.TradeNettingRules.GetById(SLTradeNettingRuleSetID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex)
            {
                string msg = $"SLTradeNettingRuleSet not found with this id: {SLTradeNettingRuleSetID}";
                return getResponse(new Exception(msg));
            }
        }
    }
}
