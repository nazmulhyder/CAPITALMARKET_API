using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.NETCore;

namespace Api.ReportControllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ForeignTradeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;

        private readonly IService _service;
        private readonly ILogger<ForeignTradeController> _logger;
        public ForeignTradeController(IService service, ILogger<ForeignTradeController> logger, IWebHostEnvironment webHostEnvironment)
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


        [HttpGet("trade/data/{CompanyID}/{BranchID}/{AccountNo}/{TradeType}/{TradeDate}/{FileType}")]
        public async Task<IActionResult> TradeData(int CompanyID, int BranchID, string AccountNo,int TradeType, string TradeDate, string FileType)
        {
            var dt = new DataTable();
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@AccountNo", AccountNo);
            sqlParams[4] = new SqlParameter("@TradeDate", TradeDate);
            sqlParams[5] = new SqlParameter("@TradeType", TradeType);

            dt = await _service.reportRepository.GetData(sqlParams, "Report_CM_TradeData");
            
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_TradeData.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();

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
                return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_TradeData.xls");
            }
            else //default pdf
            {
                byte[] pdf = report.Render("PDF");
                return File(pdf, "application/pdf");

            }

        }

    }
}
