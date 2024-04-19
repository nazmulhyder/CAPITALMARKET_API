using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeFileUpload;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;

using System.Collections.Immutable;
using Microsoft.Reporting.NETCore;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<ClientReportController> _logger;

        public ClientReportController(IService service, ILogger<ClientReportController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("Portfolio/PortfolioStatement/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}/{TransactionDate}/{FileType}")]
        public async Task<IActionResult> PortfolioStatementReport(int CompanyID, int BranchID, int ProductID, string AccountNumber, DateTime TransactionDate, string FileType)
        {
            try
            {
                LocalReport report = new LocalReport();
                if (CompanyID != 2)
                {
                    var dt = new DataSet();
                    SqlParameter[] sqlParams = new SqlParameter[6];
                    sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                    sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                    sqlParams[3] = new SqlParameter("@ProductID", ProductID);
                    sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);
                    sqlParams[5] = new SqlParameter("@TransactionDate", TransactionDate);


                    dt = await _service.reportRepository.GetDataSet(sqlParams, "Report_CM_Portfolio_Statement");

                    var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_Portfolio_Statement.rdlc";

                    
                    report.ReportPath = path;
                    report.DataSources.Add(new ReportDataSource("ds1", dt.Tables[0]));
                    report.DataSources.Add(new ReportDataSource("ds2", dt.Tables[1]));
                    report.DataSources.Add(new ReportDataSource("ds3", dt.Tables[4]));
                }
                else {
                    var dt = new DataTable();
                    SqlParameter[] sqlParamsAML = new SqlParameter[5];
                    sqlParamsAML[0] = new SqlParameter("@UserName", LoggedOnUser());
                    sqlParamsAML[1] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParamsAML[2] = new SqlParameter("@BranchID", BranchID);
                    sqlParamsAML[3] = new SqlParameter("@ProductID", ProductID);
                    sqlParamsAML[4] = new SqlParameter("@AccountNo", AccountNumber);

                    dt = await _service.reportRepository.GetData(sqlParamsAML, "Report_AML_Portfolio");

                    var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_Portfolio.rdlc";
                    List<ReportParameter> parameters = new List<ReportParameter>();

                   
                    report.ReportPath = path;
                    report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
                    report.SetParameters(parameters);
                }



                if (FileType == "pdf")
                {

                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_Portfolio_Statement.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }




        [HttpGet("TrialBalance/TrialBalanceReport/{CompanyID}/{BranchID}/{TransactionDate}/{FileType}")]
        public async Task<IActionResult> TrialBalanceSummaryReport(int CompanyID, int BranchID, DateTime TransactionDate, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@TransactionDate", TransactionDate);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_TrialBalance");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_TrialBalance.rdlc";


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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_TrialBalance.xls");
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
