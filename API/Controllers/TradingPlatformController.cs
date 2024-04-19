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
    public class TradingPlatformController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<TradingPlatformController> _logger;

        public TradingPlatformController(IService service, ILogger<TradingPlatformController> logger)
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

        [HttpPost("AddUpdate")]
        public async Task<IActionResult> AddUpdate(CMTradingPlatformDTO endityDto)
        {
            try
            {
                //return getResponse(await _service.CMTradingPlatforms.AddUpdate(endityDto, LoggedOnUser()));
                var result = await _service.CMTradingPlatforms.AddUpdate(endityDto, LoggedOnUser());
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> List(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                return getResponse(await _service.CMTradingPlatforms.GetAll(PageNo, PerPage, SearchKeyword));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetByID/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                string user = LoggedOnUser();
                return getResponse( _service.CMTradingPlatforms.GetById(id, user));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetByExchangeID/{exchangeid}")]
        public async Task<IActionResult> GetByExchangeID(int exchangeid)
        {
            try
            {
                return getResponse(await _service.CMTradingPlatforms.GetCMTradingPlatformByExchangeID(exchangeid));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
