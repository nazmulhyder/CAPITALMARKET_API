using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Utility;
using Model.DTOs.CoA;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherPrintController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<VoucherPrintController> _logger;

        public VoucherPrintController(IService service, ILogger<VoucherPrintController> logger, IWebHostEnvironment webHostEnvironment)
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
        [HttpGet("report/{CompanyID}/{BranchID}/{VoucherIDs}/{FileType}")]
        public async Task<IActionResult> VoucherPrintReport(int CompanyID, int BranchID, string VoucherIDs, string FileType)
        {
            try
            {
                int VoucherCount = 1;

                VoucherCount = VoucherIDs.Split(",").Length;

                SqlParameter[] sqlParams = new SqlParameter[4];

                sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                sqlParams[3] = new SqlParameter("@voucherIDs", VoucherIDs);
               
				DataSet DataSets = await _service.reportRepository.GetDataSet(sqlParams, "[dbo].[Report_CM_Voucher_Print]");

				DataTable dtVoucher = DataSets.Tables[0];
				DataTable dtVoucherLedgerList = DataSets.Tables[1];


                string mimetype = "";
                int extension = 1;
                string[] Streams = null;
                string path = "";

                if(VoucherCount == 1)
                path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_VoucherPrint_Single.rdlc";
                else path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_VoucherPrint_Multiple.rdlc";

				List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("VoucherCount", VoucherCount.ToString()));
                LocalReport report = new LocalReport();
                report.ReportPath = path;
                report.DataSources.Add(new ReportDataSource("dsVoucher", dtVoucher));
                report.DataSources.Add(new ReportDataSource("dsVoucherLedgerList", dtVoucherLedgerList));
				report.SetParameters(parameters);

				if (FileType == "pdf")
                {
                    byte[] pdf = report.Render("PDF");
                    return File(pdf, "application/pdf");
                }
                else if (FileType == "excel")
                {
                    byte[] pdf = report.Render("Excel");
                    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_VoucherPrint.xls");
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