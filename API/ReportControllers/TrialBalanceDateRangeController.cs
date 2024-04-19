using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.NETCore;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TrialBalanceDateRangeController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<TrialBalanceDateRangeController> _logger;

        public TrialBalanceDateRangeController(IService service, ILogger<TrialBalanceDateRangeController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("trial-balance-report/{CompanyID}/{BranchID}/{TransactionDateFrom}/{TransactionDateTo}/{FundID}/{FileType}")]
        public async Task<IActionResult> TradingReport(int CompanyID, int BranchID, DateTime TransactionDateFrom, DateTime TransactionDateTo, int FundID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[6];
                
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[4] = new SqlParameter("@TransactionDateTo", TransactionDateTo);
                sqlParams[5] = new SqlParameter("@FundID", FundID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_TrialBalance_DateRange");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_TrialBalanceDateRange.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                report.SetParameters(new ReportParameter("CompanyID", CompanyID.ToString()));
                report.SetParameters(new ReportParameter("BranchID", BranchID.ToString()));
                report.SetParameters(new ReportParameter("TransactionDateFrom", TransactionDateFrom.ToString()));
                report.SetParameters(new ReportParameter("TransactionDateTo", TransactionDateTo.ToString()));
                report.SetParameters(new ReportParameter("FundID", FundID.ToString()));

                if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_TrialBalanceDateRange.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
            }
            catch (Exception ex) { return null; }
        }
    }
}
