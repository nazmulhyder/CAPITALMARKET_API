﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Service.Interface;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;

namespace Api.ReportControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RptWithdrawalController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;

        private readonly IService _service;
        private readonly ILogger<RptWithdrawalController> _logger;
        public RptWithdrawalController(IService service, ILogger<RptWithdrawalController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _logger = logger;
            this._webHostEnvirnoment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
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

        [HttpGet("withdrawal/cheque/{CompanyID}/{BranchID}/{DisbursementID}/{FileType}")]
        public async Task<IActionResult> GetWithdrawalMoneyReciptData(int CompanyID, int BranchID, int DisbursementID, string FileType)
        {

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@DisbursementID", DisbursementID);

            var DataSets = await _service.reportRepository.GetDataSet(sqlParams, "[dbo].[CM_RPT_WithdrawalPaymentRecipt]");


            string mimetype = "";
            int extension = 1;
            var path = String.Empty;

            if (CompanyID == 4) //SL
                path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_SL_Withdrawal_Cheque.rdlc";
            if (CompanyID == 2) //AML
                path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_Withdrawal_Cheque.rdlc";
            if (CompanyID == 3) //IL
                path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_IL_Withdrawal_Cheque.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            LocalReport report = new LocalReport();
            report.ReportPath = path;
            report.DataSources.Add(new ReportDataSource("dsEmployee", DataSets.Tables[0]));

            if (FileType == "pdf")
            {
                byte[] pdf = report.Render("PDF");
                return File(pdf, "application/pdf");
            }
            else if (FileType == "excel")
            {
                byte[] pdf = report.Render("Excel");
                return File(pdf, "application/msexcel", fileDownloadName: "withdrawal_cheque.xls");
            }
            else //default pdf
            {
                byte[] pdf = report.Render("PDF");
                return File(pdf, "application/pdf");

            }

        }
    }
}
