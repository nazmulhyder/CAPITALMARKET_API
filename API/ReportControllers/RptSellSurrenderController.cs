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
    [Route("api/[controller]")]
    [ApiController]
    public class RptSellSurrenderController : Controller
    {
        private readonly IService _service;
        private readonly ILogger<RptSellSurrenderController> _logger;
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        public readonly IDBCommonOpService _dbCommonOperation;

        public RptSellSurrenderController(IService service, ILogger<RptSellSurrenderController> logger, IWebHostEnvironment webHostEnvironment, IDBCommonOpService dbCommonOperation)
        {
            _service = service;
            _logger = logger;
            this._webHostEnvirnoment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _dbCommonOperation = dbCommonOperation;
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


        [HttpGet("Print/{CompanyID}/{BranchID}/{PurchaseDetailIDs}")]
        public async Task<IActionResult> SellSurrenderPDF(int CompanyID, int BranchID,  string PurchaseDetailIDs)
        {
            var dt = new DataTable();

           SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UserName", LoggedOnUser()),
                new SqlParameter("@CompanyID",  CompanyID),
                new SqlParameter("@BranchID",  BranchID),
                new SqlParameter("@PurchaseDetailIDs",  PurchaseDetailIDs),
           };

            string mimetype = "";
            int extension = 1;
            dt = await _service.reportRepository.GetData(sqlParams, "Report_AML_GenrateCDBLTransferPDFFile");
            string path = path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_AML_GenrateCDBLTransferPDFFile.rdlc";         
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            // LocalReport lr = new LocalReport(path);
            // lr.AddDataSource("dsEmployee", dt);
            // var result = lr.Execute(RenderType.Pdf, extension, parameters, mimetype);
            // return File(result.MainStream, "application/pdf");
            LocalReport report = new LocalReport();
            report.ReportPath = path;
            report.DataSources.Add(new ReportDataSource("dsEmployee", dt));

            byte[] pdf = report.Render("PDF");
            return File(pdf, "application/pdf");

        }
    }
}
