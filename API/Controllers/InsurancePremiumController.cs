using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.FundCollection;
using Model.DTOs.InsurancePremium;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InsurancePremiumController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<InsurancePremiumController> _logger;
        public InsurancePremiumController(IService service, ILogger<InsurancePremiumController> logger)
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

        [HttpGet("GetInsurancePremiumCollection/{CompanyID}/{BranchID}/{FundID}/{InstallmentDate}")]
        public async Task<IActionResult> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, int FundID, string InstallmentDate)
        {
            try
            { 
                return getResponse(await _service.insurancePremiumRepository.GetInsurancePremiumCollection(CompanyID, BranchID, LoggedOnUser(), FundID, InstallmentDate));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GenerateDDIFile/{BanckAccountID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GenerateDDIFile(List<InsurancePremiumCollectionDto> data, int BanckAccountID, int CompanyID, int BranchID)
        {
            try
            {

                   return getResponse(await _service.insurancePremiumRepository.GenerateDDIFileAML(data, BanckAccountID, CompanyID, BranchID, LoggedOnUser()));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("DDIFileUpload")]
        public async Task<IActionResult> DDIFileUpload(IFormCollection data)
        {
            try
            {
                 return getResponse(await _service.insurancePremiumRepository.DDIFileUploadAML(data, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("DDIFileList/{CompanyID}/{BranchID}/{Status}")]
        public async Task<IActionResult> DDIFileList(int CompanyID, int BranchID, string Status)
        {
            try
            {

               return getResponse(await _service.insurancePremiumRepository.DDIFileListAML(LoggedOnUser(), CompanyID, BranchID, Status));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("DDIFileApprove/{CompanyID}/{BranchID}/{DDIFileID}/{ApproveStatus}")]
        public async Task<IActionResult> DDIFileApprove(int CompanyID, int BranchID, int DDIFileID, string ApproveStatus)
        {
            try
            {
                 return getResponse(await _service.insurancePremiumRepository.DDIFileApproveAML(LoggedOnUser(), CompanyID, BranchID, DDIFileID, ApproveStatus));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}
