using Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Reports;
using Model.DTOs.TradeFileUpload;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using ControllerBase = Api.Controllers.ControllerBase;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<ForeignTradeController> _logger;

        public ReportController(IService service, ILogger<ForeignTradeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _logger = logger;
            this._webHostEnvirnoment = webHostEnvironment;
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

        [HttpGet("list/menu/wise/{CompanyID}/{BranchID}/{MenuID}")]
        public async Task<IActionResult> MenuWiseReportList(int CompanyID, int BranchID, int MenuID)
        {
            try
            {
                return getResponse(await _service.reportRepository.MenuWiseReportList(LoggedOnUser(), CompanyID, BranchID, MenuID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
