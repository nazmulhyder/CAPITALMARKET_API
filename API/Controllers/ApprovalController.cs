using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Approval;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalController : ControllerBase
    {
        private readonly ILogger<ApprovalController> _logger;
        private readonly IService _service;

        public ApprovalController(ILogger<ApprovalController> logger, IService service)
        {
            _logger = logger;
            _service = service;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            //var userName = WindowsIdentity.GetCurrent().Name;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }


        [HttpPost("ApproverAction")]
        public IActionResult ApproverAction(ApprovalDto approvalRequest)
        {
            string user = LoggedOnUser();
            int approvalRqSetId = approvalRequest.approvalRequestSetID;

            if (approvalRequest.approvalRequestSetID > 0)
            {
                try
                {
                    ApprovalDetail approvalDetail = (from ad in approvalRequest.approvalDetail
                                                     where ad.approvalLevel == approvalRequest.currentApprovalLevel
                                                     select ad).FirstOrDefault();
                    approvalDetail.approver = user;
                    //approvalRequest.UpdateApprovalStatus(approvalRqSetId,approvalDetail);
                    bool isSuccess = _service.Approvals.UpdateApprovalStatus(approvalRqSetId, approvalDetail);

                    return getResponse(isSuccess ? "Success" : "Something went Wrong!");

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            else
            {
                return BadRequest("Approval request set id should not be empty!");
            }

        }
    }
}
