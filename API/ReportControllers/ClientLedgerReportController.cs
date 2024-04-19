using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeFileUpload;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Microsoft.Reporting.NETCore;
using System.Collections.Immutable;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientLedgerReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<ClientLedgerReportController> _logger;

        public ClientLedgerReportController(IService service, ILogger<ClientLedgerReportController> logger, IWebHostEnvironment webHostEnvironment)
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

        [HttpGet("ClientLedger/TransactionLedgerReport/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}/{TransactionDateFrom}/{TransactionDateTo}/{FileType}")]
        public async Task<IActionResult> TransactionLedgerReport(int CompanyID, int BranchID, int ProductID, string AccountNumber, DateTime TransactionDateFrom, DateTime TransactionDateTo, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@ProductID", ProductID);
                sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);
                sqlParams[5] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[6] = new SqlParameter("@TransactionDateTo", TransactionDateTo);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_TransactionLedger");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_TransactionLedger.rdlc";
                List<ReportParameter> parameters = new List<ReportParameter>();
                string from = TransactionDateFrom.ToString("dd-MMM-yyyy");
                string to = TransactionDateTo.ToString("dd-MMM-yyyy");
                parameters.Add(new ReportParameter("TransactionDateFrom", from));
                parameters.Add(new ReportParameter("TransactionDateTo", to));

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
                report.SetParameters(parameters);
                if (FileType == "pdf")
                {

                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_TransactionLedger.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }


        [HttpGet("LedgerStatement/LedgerStatementReport/{CompanyID}/{BranchID}/{LedgerHeadID}/{TransactionDateFrom}/{TransactionDateTo}/{FileType}")]
        public async Task<IActionResult> GeneralLedgerStatementReport(int CompanyID, int BranchID, int LedgerHeadID, DateTime TransactionDateFrom, DateTime TransactionDateTo, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[6];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@LedgerHeadID", LedgerHeadID);
                sqlParams[4] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[5] = new SqlParameter("@TransactionDateTo", TransactionDateTo);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_GeneralLedgerStatement");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_GeneralLedgerStatement.rdlc";
                List<ReportParameter> parameters = new List<ReportParameter>();
                string from = TransactionDateFrom.ToString("dd-MMM-yyyy");
                string to = TransactionDateTo.ToString("dd-MMM-yyyy");
                parameters.Add(new ReportParameter("TransactionDateFrom", from));
                parameters.Add(new ReportParameter("TransactionDateTo", to));

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
                report.SetParameters(parameters);
                if (FileType == "pdf")
                {

                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_GeneralLedgerStatement.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }

        [HttpGet("AMLTransaction/AMLTransactionHistory/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}/{TransactionDateFrom}/{TransactionDateTo}/{FileType}")]
        public async Task<IActionResult> AMLTransactionHistory(int CompanyID, int BranchID, int ProductID, string AccountNumber, DateTime TransactionDateFrom, DateTime TransactionDateTo, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@ProductID", ProductID);
                sqlParams[4] = new SqlParameter("@AccountNo", AccountNumber);
                sqlParams[5] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[6] = new SqlParameter("@TransactionDateTo", TransactionDateTo);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_TransactionHistory");

                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_TransactionHistory.rdlc";
                List<ReportParameter> parameters = new List<ReportParameter>();
                string from = TransactionDateFrom.ToString("dd-MMM-yyyy");
                string to = TransactionDateTo.ToString("dd-MMM-yyyy");
                parameters.Add(new ReportParameter("TransactionDateFrom", from));
                parameters.Add(new ReportParameter("TransactionDateTo", to));

                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
                report.SetParameters(parameters);
                if (FileType == "pdf")
                {

                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_AML_TransactionHistory.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");

                }

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }


        [HttpGet("AMLLedgerStatement/AMLLedgerStatementReport/{CompanyID}/{BranchID}/{FundID}/{COAID}/{TransactionDateFrom}/{TransactionDateTo}/{FileType}")]
        public async Task<IActionResult> AMLLedgerStatementRepor(int CompanyID, int BranchID,int FundID, int COAID, DateTime TransactionDateFrom, DateTime TransactionDateTo, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@FundID", FundID);
                sqlParams[4] = new SqlParameter("@COAID", COAID);
                sqlParams[5] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[6] = new SqlParameter("@TransactionDateTo", TransactionDateTo);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_LedgerStatement");


                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_LedgerStatement.rdl";
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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_BalanceSheet.xls");
                }
                else //default pdf
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                

            }
            catch (Exception ex) { return Ok(ex.Message); }
        }


        [HttpGet("AMLCapitalGainLoss/AMLCapitalGainLossReport/{CompanyID}/{BranchID}/{FundID}/{TransactionDateFrom}/{TransactionDateTo}/{InstrumentID}/{FileType}")]
        public async Task<IActionResult> AMLCapitalGainLossReport(int CompanyID, int BranchID, int FundID, DateTime TransactionDateFrom, DateTime TransactionDateTo, int InstrumentID, string FileType)
        {
            try
            {
                var dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@FundID", FundID);
                sqlParams[4] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
                sqlParams[5] = new SqlParameter("@TransactionDateTo", TransactionDateTo);
                sqlParams[6] = new SqlParameter("@InstrumentID", InstrumentID);

                dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_CapitalGainLoss");


                var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_CapitalGainLoss.rdl";
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
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_AML_CapitalGainLoss.xls");
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
