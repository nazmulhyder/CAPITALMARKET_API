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
using Microsoft.Reporting.NETCore;
using Microsoft.VisualBasic.FileIO;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RptMarginRequestController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<RptMarginRequestController> _logger;

        public RptMarginRequestController(IService service, ILogger<RptMarginRequestController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("margin/scorecard/{CompanyID}/{BranchID}/{marginRequestID}")]
        public async Task<IActionResult> MenuWiseReportList(int CompanyID, int BranchID, int marginRequestID)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[1];                             
                sqlParams[0] = new SqlParameter("@MarginReqID", marginRequestID);

                dt = await _service.reportRepository.GetData(sqlParams, "Rpt_MarginRequestScore");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_SL_Margin_Request.rdlc";


                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
                
                byte[] pdf = report.Render("Excel");
                return File(pdf, "application/msexcel", fileDownloadName: "Report_SL_Margin_Request.xls");
                
               

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

    }
}
