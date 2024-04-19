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
    public class PortfolioController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<PortfolioController> _logger;

        public PortfolioController(IService service, ILogger<PortfolioController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("portfolioSEC/{CompanyID}/{BranchID}/{FileType}")]
        public async Task<IActionResult> PortfolioSECReport(int CompanyID, int BranchID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[3];

                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_PortfolioSEC");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_PortfolioSEC.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));

                if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_AML_PortfolioSEC.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
            }
            catch (Exception ex) { return null; }
        }

        [HttpGet("InvestmentSummary/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}/{FileType}")]
        public async Task<IActionResult> InvestmentSummaryReport(int CompanyID, int BranchID, int ProductID, string AccountNumber, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[5];

                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@ProductID", ProductID);
                sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_InvestmentSummary");

                string mimetype = "";
                int extension = 1;

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_InvestmentSummary.rdl";
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                report.SetParameters(new ReportParameter("ProductID", ProductID.ToString()));
                report.SetParameters(new ReportParameter("AccountNumber", AccountNumber.ToString()));



                //if (FileType == "pdf")
                //{
                //    byte[] pdf = report.Render("PDF");
                //    return File(pdf, "application/pdf");
                //}
                //else if (FileType == "excel")
                //{
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_AML_InvestmentSummary.xls");
                //}
                //else //default pdf
                //{
                //    byte[] pdf = report.Render("PDF");
                //    return File(pdf, "application/pdf");
                //}
            }
            catch (Exception ex) { return null; }
        }
    }
}
