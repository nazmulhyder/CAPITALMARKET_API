using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Market;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CMMarketTypeController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CMMarketTypeController> _logger;

        public CMMarketTypeController(IService service, ILogger<CMMarketTypeController> logger)
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

        [HttpPost("AddUpdateExchange")]
        public async Task<IActionResult> AddUpdateAddMarketType(CMMarketTypeDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                //return getResponse(await _service.CMMarketTypes.AddUpdate(currentData, userName));
                var result = await _service.CMMarketTypes.AddUpdate(currentData, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetAllMarketType(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMMarketTypes.GetAllMarketType(PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetMarketTypeByID/{MarketTypeID}")]
        public async Task<IActionResult> GetMarketType(int MarketTypeID)
        {
            try
            {
                return getResponse(await _service.CMMarketTypes.GetCMMarketTypeById(MarketTypeID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"MarketType not found with this id: {MarketTypeID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("GetByTradingPlatformID/{TradingPlatformID}")]
        public async Task<IActionResult> GetByTradingPlatformID(int TradingPlatformID)
        {
            try
            {
                return getResponse(await _service.CMMarketTypes.GetByTradingPlatformID(TradingPlatformID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
