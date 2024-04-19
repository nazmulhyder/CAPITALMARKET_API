using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CMExchange;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CMExchangeController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CMExchangeController> _logger;

        public CMExchangeController(IService service, ILogger<CMExchangeController> logger)
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
        public async Task<IActionResult> AddUpdateExchange(CMExchangeDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                var result = await _service.CMExchanges.AddUpdate(currentData, userName);
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
                //string userName = LoggedOnUser();
                var reponse = await _service.CMExchanges.GetAll(PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetExchangeByID/{ExchangeID}")]
        public IActionResult GetExchangeByID(int ExchangeID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse =  _service.CMExchanges.GetById(ExchangeID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex)
            {
                string msg = $"Exchange not found with this id: {ExchangeID}";
                return getResponse(new Exception(msg));
            }
        }

    }
}
