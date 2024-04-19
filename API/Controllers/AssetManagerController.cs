using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AssetManager;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AssetManagerController : ControllerBase
    {

        private readonly IService _service;
        private readonly ILogger<AssetManagerController> _logger;
        public AssetManagerController(IService service, ILogger<AssetManagerController> logger)
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

        [HttpPost("UpdateAccountTradingCodes/{BrokerID}")]
        public async Task<IActionResult> UpdateAccountTradingCodes(List<AMLBrokerDto> data, int BrokerID)
        {
            try
            {
                return getResponse(await _service.CMAssetManagers.UpdateAccountTradingCodes(data, LoggedOnUser(), BrokerID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetAllAccountTradingCodes/{BrokerID}")]
        public async Task<IActionResult> GetAllAccountTradingCodes(int BrokerID)
        {
            try
            {
                return getResponse(_service.CMAssetManagers.GetAllAccountTradingCodes(BrokerID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Organisation/List")]
        public async Task<IActionResult> OrganisationList()
        {
            try
            {
                return getResponse(await _service.Organizations.GetAll(1,100,""));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdateAssetManager")]
        public async Task<IActionResult> AddUpdateAddAssetManager(CMAssetManagerOrganisationDetailDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.CMAssetManagers.AddUpdate(currentData, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetAssetManagerByOrganizationID/{OrganizationID}")]
        public async Task<IActionResult> GetAssetManager(int OrganizationID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = _service.CMAssetManagers.GetById(OrganizationID,userName);
                return getResponse(reponse);
            }
            catch (Exception ex)
            {
                string msg = $"AssetManager not found with this id: {OrganizationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetAllAssetManager(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMAssetManagers.GetAllAssetManager(PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}
