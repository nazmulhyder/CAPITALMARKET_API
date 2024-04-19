using System.Diagnostics.Metrics;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Demat;
using Model.DTOs.SecurityElimination;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityEliminationController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<SecurityEliminationController> _logger;
        public SecurityEliminationController(IService service, ILogger<SecurityEliminationController> logger)
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

        [HttpGet("CMSecurityEliminationList/{InstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMSecurityEliminationList(int InstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.securityEliminationRepository.CMSecurityEliminationList(InstrumentID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMInsertUpdateSecurityElimination/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCMSecurityElimination(SecurityEliminationInsertDTO entrySecurityELimination, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.securityEliminationRepository.InsertCMSecurityElimination(entrySecurityELimination, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMSecurityEliminationApprovalList/{InstrumentID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMSecurityEliminationApprovalList(int InstrumentID,string Status ,int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.securityEliminationRepository.CMSecurityEliminationApprovalList(InstrumentID, Status,CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("CMApprovedInstrumentElimination/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovedInstrumentElimination(CMSecurityInstrumentEliminationApproveDTO objCMApprovedInstrumentElimination, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.securityEliminationRepository.GetCMApprovedInstrumentElimination(objCMApprovedInstrumentElimination, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMUpdateSecuirtyElimination/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMUpdateSecurityElimination(CMSecurityEliminationUpdateMaster objCMUpdateSecurityElimination, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.securityEliminationRepository.GetCMUpdateSecurityElimination(objCMUpdateSecurityElimination, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }



    }
}
