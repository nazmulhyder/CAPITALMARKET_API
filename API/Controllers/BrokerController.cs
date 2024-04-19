using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Broker;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BrokerController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<BrokerController> _logger;
        public BrokerController(IService service, ILogger<BrokerController> logger)
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

        [HttpGet("Organisation/List")]
        public async Task<IActionResult> OrganisationList()
        {
            try
            {
                return getResponse(await _service.Organizations.GetAll(1, 1000, ""));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Organisation/detail/{OrganisationID}")]
        public async Task<IActionResult> OrganisationList(int OrganisationID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(_service.Brokers.GetById(OrganisationID, userName));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdate")]
        public async Task<IActionResult> AddUpdateBroker(BrokerOrganisationDetailDto data)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.Brokers.AddUpdate(data, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetCMBrokerList(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                return getResponse(await _service.Brokers.GetAllBrokerList(PageNo,PerPage, SearchKeyword));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
