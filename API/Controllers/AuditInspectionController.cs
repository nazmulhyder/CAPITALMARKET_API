using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Audit;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuditInspectionController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<AuditInspectionController> _logger;
        public AuditInspectionController(IService service, ILogger<AuditInspectionController> logger)
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


        [HttpGet("GetObservedClientAccountSL/{CompanyID}/{BranchID}/{TradingCode}/{InstrumentID}/{TradingDate}")]
        public async Task<IActionResult> GetObservedClientAccountSL(int CompanyID, int BranchID, string TradingCode, int InstrumentID, string TradingDate)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetObservedClientAccountSL(LoggedOnUser(), CompanyID, BranchID, TradingCode, InstrumentID, TradingDate));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdate/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdate(IFormCollection formData, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.SaveAuditInspection(LoggedOnUser(), CompanyID, BranchID,formData));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetAuditInspection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAuditInspection(int CompanyID, int BranchID, AuditSearchFilter filter)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetAuditInspection(LoggedOnUser(), CompanyID, BranchID, filter));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetAuditInspectionById/{CompanyID}/{BranchID}/{ReferenceNo}")]
        public async Task<IActionResult> GetAuditInspectionById(int CompanyID, int BranchID, int ReferenceNo)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetAuditInspectionById(CompanyID, BranchID, LoggedOnUser(), ReferenceNo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetPrincipleAndJointApplicantSL/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}")]
        public async Task<IActionResult> GetPrincipleAndJointApplicantSL(int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetPrincipleAndJointApplicantSL(LoggedOnUser(), CompanyID, BranchID,  ProductID, AccountNo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetRequlatoryTWSSL/{CompanyID}/{BranchID}/{ContractID}/{InstrumentID}/{TradingDateFrom}/{TradingDateTo}")]
        public async Task<IActionResult> GetRequlatoryTWSSL(int CompanyID, int BranchID, int ContractID, int InstrumentID, string TradingDateFrom, string TradingDateTo)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetRequlatoryTWSSL(LoggedOnUser(), CompanyID, BranchID, ContractID, InstrumentID, TradingDateFrom, TradingDateTo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveRegulatory/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveRegulatory(IFormCollection formData, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.SaveRegulatory(LoggedOnUser(), CompanyID, BranchID, formData));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetRegulatoryList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRegulatoryList(int CompanyID, int BranchID, RequlatoryQuerySearchFilter filter)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetRegulatoryList(LoggedOnUser(), CompanyID, BranchID, filter));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetRegulatoryById/{CompanyID}/{BranchID}/{RegulatoryQueryID}")]
        public async Task<IActionResult> GetRegulatoryById(int CompanyID, int BranchID, int RegulatoryQueryID)
        {
            try
            {
                return getResponse(await _service.auditInspectionRepository.GetRegulatoryById(CompanyID, BranchID, LoggedOnUser(), RegulatoryQueryID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
