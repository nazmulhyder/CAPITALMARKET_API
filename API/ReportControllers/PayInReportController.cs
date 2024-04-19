using Api.ReportControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.NETCore;

namespace Api.wwwroot.Reports
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PayInReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<PayInReportController> _logger;

        public PayInReportController(IService service, ILogger<PayInReportController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("payin/reconciliation/payinreport/{CompanyID}/{BranchID}/{TransactionDate}/{FileType}")]
        public async Task<IActionResult> PayInReconciliationReport(int CompanyID, int BranchID, DateTime TransactionDate, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@TransactionDate", TransactionDate);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_SL_PayInReconciliation");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_SL_PayInReconciliation.rdlc";

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsEmployee", dt));

                if (FileType == "pdf")
                {

                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_SL_PayInReconciliation.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }
    }
}
