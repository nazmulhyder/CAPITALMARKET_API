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
    public class IpoReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<IpoReportController> _logger;

        public IpoReportController(IService service, ILogger<IpoReportController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("Ipo/InstrumentStatusReport/{CompanyID}/{BranchID}/{FileType}")]
        public async Task<IActionResult> IpoInstrumentReport(int CompanyID, int BranchID,  string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_IPOInstrumentList");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_IPOInstrumentList.rdlc";
              

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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_IPOInstrumentList.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet("Ipo/ApplicationReport/{CompanyID}/{BranchID}/{IPOInstrumentID}/{FileType}")]
        public async Task<IActionResult> IpoApplicationReport(int CompanyID, int BranchID, int IPOInstrumentID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@IPOInstrumentID", IPOInstrumentID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_IPOApplication");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_IPOApplication.rdlc";


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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_IPOApplication.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet("Ipo/ApplicationListSummary/{CompanyID}/{BranchID}/{IPOInstrumentID}/{FileType}")]
        public async Task<IActionResult> IpoApplicationListSummaryReport(int CompanyID, int BranchID, int IPOInstrumentID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@IPOInstrumentID", IPOInstrumentID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_IPOApplicationListSummary");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_IPOApplicationListSummary.rdlc";


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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_IPOApplicationListSummary.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet("Ipo/ResultStatus/{CompanyID}/{BranchID}/{IPOInstrumentID}/{FileType}")]
        public async Task<IActionResult> IpoApplicationResultStatusReport(int CompanyID, int BranchID, int IPOInstrumentID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@IPOInstrumentID", IPOInstrumentID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_IPOResultStatus");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_IPOResultStatus.rdlc";


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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_IPOResultStatus.xls");
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
