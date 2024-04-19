using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.DTOs.Demat;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DematController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<DematController> _logger;
        public DematController(IService service, ILogger<DematController> logger)
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

        [HttpPost("CMInsertUpdateDematInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> GetDematCollection(DematDTO objDematCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.dematRepository.GetDematCollection(objDematCollection, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMDematCollectionList/{TransactionDateFrom}/{TransactionDateTo}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetDematCollectionList(string TransactionDateFrom, string TransactionDateTo, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.dematRepository.GetDematCollectionList(TransactionDateFrom, TransactionDateTo,Status, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMUpdateDematCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMUpdateDematCollection(CMDematCollectionUpdateMaster objCMUpdateDematCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.dematRepository.GetCMUpdateDematCollection(objCMUpdateDematCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedDematCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovedDematCollection(CMApprovedDematCollectionDTO objCMApprovedDematCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.dematRepository.GetCMApprovedDematCollection(objCMApprovedDematCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        
    }
}
