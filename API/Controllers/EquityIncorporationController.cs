using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.EquityIncorporation;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EquityIncorporationController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<EquityIncorporationController> _logger;
        public EquityIncorporationController(IService service, ILogger<EquityIncorporationController> logger)
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


        [HttpGet("ListCollectionForEquityAddition/{CompanyID}/{BranchID}/{AccountNo}")]
        public async Task<IActionResult> ListCollectionForEquityAddition(int CompanyID, int BranchID, string AccountNo)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.ListCollectionForEquityAddition(LoggedOnUser(), CompanyID, BranchID, AccountNo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListEquityAddition/{CompanyID}/{BranchID}/{Status}")]
        public async Task<IActionResult> ListEquityAddition(int CompanyID, int BranchID, string Status)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.ListEquityAddition(LoggedOnUser(), CompanyID, BranchID, Status));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("EquityIncorporationApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> EquityIncorporationApprove(int CompanyID, int BranchID, EquityAdditionApprovalDto data)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.EquityIncorporationApprove(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveEquityAddition/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertEquityAddition(int CompanyID, int BranchID, EquityAdditionDto data)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.InsertEquityAddition(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("ClientInfoForEquityDeduction/{CompanyID}/{BranchID}/{AccountNo}")]
        public async Task<IActionResult> ClientInfoForEquityDeduction(int CompanyID, int BranchID, string AccountNo)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.ClientInfoForEquityDeduction(LoggedOnUser(), CompanyID, BranchID, AccountNo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("SaveEquityDeduction/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveEquityDeduction(int CompanyID, int BranchID, EquityDeductionDto data)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.SaveEquityDeduction(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListEquityDeduction/{CompanyID}/{BranchID}/{Status}")]
        public async Task<IActionResult> ListEquityDeduction(int CompanyID, int BranchID, string Status)
        {
            try
            {
                return getResponse(await _service.equityIncorporationRepository.ListEquityDeduction(LoggedOnUser(), CompanyID, BranchID, Status));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}
