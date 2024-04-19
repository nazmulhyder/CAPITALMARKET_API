using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CMStockIndex;
using Service.Interface;
using System.Security.Principal;
using Utility;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CMStockIndexController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CMStockIndexController> _logger;
        public CMStockIndexController(IService service, ILogger<CMStockIndexController> logger)
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
        public async Task<IActionResult> AddUpdate(CMStockIndexDTO entityDto)
        {
            try
            {
                entityDto.TradeDate = !String.IsNullOrEmpty(entityDto.TradeDateInString) ? DatetimeFormatter.ConvertStringToDatetime(entityDto.TradeDateInString) : null;
                return getResponse(await _service.Indexes.AddUpdate(entityDto, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> List(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                return getResponse(await _service.Indexes.GetAll(PageNo, PerPage, SearchKeyword));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetCMStockIndexByID/{ID}")]
        public async Task<IActionResult> GetCMStockIndexByID(int ID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(_service.Indexes.GetById(ID, userName));
            }
            catch (Exception ex)
            {
                return getResponse(ex);
            }
        }

        [HttpGet("GetCMStockIndex/{ExchangeID}")]
        public async Task<IActionResult> GetCMStockIndexByExchangeID(int ExchangeID)
        {
            try
            {
                return getResponse(await _service.Indexes.GetCMStockIndexByExchangeID(ExchangeID));
            }
            catch (Exception ex)
            {
                return getResponse(ex);
            }
        }
    }
}
