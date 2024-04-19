using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Approval;
using Model.DTOs.InternalFundTransfer;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InternalFundTransferController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<InternalFundTransferController> _logger;
        public InternalFundTransferController(IService service, ILogger<InternalFundTransferController> logger)
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

        [HttpGet("CustomerAvailableBalanceInfo/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}")]
        public async Task<IActionResult> CustomerAvailableBalanceInfo(int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            try
            {
                return getResponse(await _service.internalFundTransferRepository.GetCustomerAvailableBalanceInfo(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateInternalFundTransfer/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateInternalFundTransfer(int CompanyID, int BranchID, InternalFundTransferDto entry)
        {
            try
            {
                return getResponse(await _service.internalFundTransferRepository.InsertUpdateInternalFundTransfer( CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetListInternalFundTransfer(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.internalFundTransferRepository.GetListInternalFundTransfer(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InternalFundTransferApproval(int CompanyID, int BranchID, InternalFundTransferApprovalDto approvalDto)
        {
            try
            {
                return getResponse(await _service.internalFundTransferRepository.InternalFundTransferApproval(LoggedOnUser(), CompanyID, BranchID, approvalDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
