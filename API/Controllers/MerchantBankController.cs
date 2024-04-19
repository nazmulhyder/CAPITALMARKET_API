using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CMMerchantBank;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MerchantBankController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<MerchantBankController> _logger;

        public MerchantBankController(IService service, ILogger<MerchantBankController> logger)
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
                return getResponse(await _service.Organizations.GetAll(1, 100, ""));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Organisation/detail/{OrganisationID}")]
        public async Task<IActionResult> GetMerchantBankOraganisationDetail(int OrganisationID)
        {
            try
            {
                string user = LoggedOnUser();
                return getResponse( _service.MerchantBanks.GetById(OrganisationID, user));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdate")]
        public async Task<IActionResult> AddUpdateMerchantBank(MerchantBankOraganisationDetailDto data)
        {
            try
            {
                string user = LoggedOnUser();
                return getResponse( await _service.MerchantBanks.AddUpdate(data, user));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetCMMerchantBankList(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                return getResponse(await _service.MerchantBanks.GetCMMerchantBankList(PageNo, PerPage, SearchKeyword));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
