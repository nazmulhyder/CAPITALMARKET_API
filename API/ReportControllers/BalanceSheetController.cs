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
using System.Reflection.Metadata;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceSheetController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<BalanceSheetController> _logger;

        public BalanceSheetController(IService service, ILogger<BalanceSheetController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("voucher-report/balancesheet/{CompanyID}/{BranchID}/{AsOnDate}/{Level}/{FileType}")]
        public async Task<IActionResult> TradingReport(int CompanyID, int BranchID, string FileType, DateTime AsOnDate, int Level)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[5];
                
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@AsOnDate", AsOnDate);
                sqlParams[4] = new SqlParameter("@Level", Level);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_BalanceSheet");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_BalanceSheet_PwBi.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                report.SetParameters(new ReportParameter("AsOnDate", AsOnDate.ToString()));
                report.SetParameters(new ReportParameter("Level", Level.ToString()));

                if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_BalanceSheet.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
            }
            catch (Exception ex) { return null; }
        }

        [HttpGet("voucher-report/balancesheet-aml/{CompanyID}/{BranchID}/{AsOnDate}/{Level}/{FundID}/{FileType}")]
        public async Task<IActionResult> AMLReport(int CompanyID, int BranchID, DateTime AsOnDate, int Level, int FundID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[6];

                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@AsOnDate", AsOnDate);
                sqlParams[4] = new SqlParameter("@Level", Level);
                sqlParams[5] = new SqlParameter("@FundID", FundID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_BalanceSheet");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_BalanceSheet.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                report.SetParameters(new ReportParameter("AsOnDate", AsOnDate.ToString()));
                report.SetParameters(new ReportParameter("Level", Level.ToString()));
                report.SetParameters(new ReportParameter("FundID", FundID.ToString()));

                if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_AML_BalanceSheet.xls");
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
