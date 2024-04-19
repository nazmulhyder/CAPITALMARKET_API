using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeFileUpload;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.NETCore;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.Design;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeStatementController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<IncomeStatementController> _logger;

        public IncomeStatementController(IService service, ILogger<IncomeStatementController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("report/{CompanyID}/{BranchID}/{TransactionDateFrom}/{TransactionDateTo}/{FileType}")]
        public async Task<IActionResult> TradingReport(int CompanyID, int BranchID, DateTime TransactionDateFrom, DateTime TransactionDateTo, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[5];
                
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[4] = new SqlParameter("@TransactionDateTo", TransactionDateTo);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_IncomeStatement");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_IncomeStatement.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                report.SetParameters(new ReportParameter("TransactionDateFrom", TransactionDateFrom.ToString()));
                report.SetParameters(new ReportParameter("TransactionDateTo", TransactionDateTo.ToString()));

                if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_IncomeStatement.xls");
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
