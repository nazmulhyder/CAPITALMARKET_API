using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Demat;
using Model.DTOs.Remat;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RematController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<RematController> _logger;
        public RematController(IService service, ILogger<RematController> logger)
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

        [HttpPost("CMInsertUpdateRematInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> GetRematCollection(RematDTO objRematCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.rematRepository.GetRematCollection(objRematCollection, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMRematInstrumentList/{TransactionDateFrom}/{TransactionDateTo}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRematInstrumentList(string TransactionDateFrom, string TransactionDateTo,string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.rematRepository.GetRematInstrumentList(TransactionDateFrom, TransactionDateTo, Status, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMUpdateRematInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetUpdateRematInstrument(CMRematInstrumentUpdateMaster objUpdateRematInstrument, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.rematRepository.GetUpdateRematInstrument(objUpdateRematInstrument, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedRematInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovedRematInstrument(CMApprovedRematInstrumentDTO objCMApprovedRematInstrument, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.rematRepository.GetCMApprovedRematInstrument(objCMApprovedRematInstrument, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
