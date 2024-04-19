using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class DashboardController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(IService service, ILogger<DashboardController> logger)
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

        [HttpGet("Dashboard/List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DashboardList(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.Dashborad.GetAll(CompanyID, BranchID,LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
