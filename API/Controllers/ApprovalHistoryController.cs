using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApprovalHistoryController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<UpdateLogController> _logger;

        public ApprovalHistoryController(IService service, ILogger<UpdateLogController> logger)
        {
            _service = service;
            _logger = logger;
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

        [HttpGet("GetListBy/{ApprovalTypeCodeID}")]
        public async Task<IActionResult> GetList(int ApprovalTypeCodeID)
        {
            try
            {
                var data = await _service.ApprovalHistory.getApprovalHistoryList(ApprovalTypeCodeID); //for approval History
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
