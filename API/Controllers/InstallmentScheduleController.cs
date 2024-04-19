using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.InstallmentSchedule;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InstallmentScheduleController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<InstallmentScheduleController> _logger;
        public InstallmentScheduleController(IService service, ILogger<InstallmentScheduleController> logger)
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

        [HttpPost("InsertUpdateInstallmentSchedule/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateInstallmentSchedule(int CompanyID, int BranchID, GenInstallmentSchedule? entityDto)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID == 4)
                return getResponse(await _service.InstallmentSchedule.AddInstallmentScheduleSL( CompanyID, BranchID, LoggedOnUser(), entityDto));
                else if (CompanyID == 2)
                return getResponse(await _service.InstallmentSchedule.AddInstallmentScheduleAML(CompanyID, BranchID, LoggedOnUser(), entityDto));
                else
                return getResponse(await _service.InstallmentSchedule.AddInstallmentScheduleIL(CompanyID, BranchID, LoggedOnUser(), entityDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
