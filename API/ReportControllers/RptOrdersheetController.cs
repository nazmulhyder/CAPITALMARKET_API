using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Implementation;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;
using Model.DTOs.OrderSheet;
using Dapper;
using System.ComponentModel.Design;
using Utility;
using Model.DTOs.TradeFileUpload;
using Microsoft.Reporting.NETCore;
using Microsoft.VisualBasic.FileIO;

namespace Api.ReportControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RptOrdersheetController : Controller
    {
        private readonly IService _service;
        private readonly ILogger<RptOrdersheetController> _logger;
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        public readonly IDBCommonOpService _dbCommonOperation;
        public RptOrdersheetController(IService service, ILogger<RptOrdersheetController> logger, IWebHostEnvironment webHostEnvironment, IDBCommonOpService dbCommonOperation)
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


        //[HttpGet("Ordersheet/Blank/Report/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> OrdersheetBlankReport(int CompanyID, int BranchID)
        //{
        //    string mimetype = "";
        //    int extension = 1;
        //    var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\RPT_BlankOrdersheetSL.rdlc";
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    //parameters.Add("prm", "RDLC report (Set as parameter)");
        //    LocalReport lr = new LocalReport(path);
        //    lr.AddDataSource("dsEmployee",new DataTable());

        //    var result = lr.Execute(RenderType.Pdf, extension, parameters, mimetype);
        //    return File(result.MainStream, "application/pdf");
        //}

        [HttpGet("Ordersheet/Print/{CompanyID}/{BranchID}/{ContractID}/{TradeDetailIDs}/{printType}")]
        public async Task<IActionResult> Ordersheet(int CompanyID, int BranchID, int ContractID, string TradeDetailIDs, string printType)
        {

            string sp = (CompanyID == 4 ? "Report_SL_Ordersheet" : "Report_IL_Ordersheet");

            var dt = new DataTable();

            SqlParameter[] sqlParams = new SqlParameter[6];           
            sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@ContractID", ContractID);
            sqlParams[4] = new SqlParameter("@TradeDetailIDs", printType == "ordersheet_blank" ? "0" : TradeDetailIDs);
            sqlParams[5] = new SqlParameter("@printType", printType);

            dt = await _service.reportRepository.GetData(sqlParams, sp);
            //if (reportDto.printType != "ordersheet_blank")
            //    dt = await _service.reportRepository.GetData(sqlParams, sp);
            //string mimetype = "";
            //int extension = 1;
            var path = "";
            if(CompanyID == 4) {
                if (printType == "ordersheet_with_data")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_Ordersheet_with_data.rdlc";
                if (printType == "ordersheet_only_data")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_OrdersheetOnlyData.rdlc";
                if (printType == "ordersheet_blank")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_Blank_OrderSheet.rdlc";
            }
            else
            {
                if (printType == "ordersheet_with_data")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_IL_Ordersheet_with_data.rdlc";
                if (printType == "ordersheet_only_data")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_IL_Ordersheet_only_data.rdlc";
                if (printType == "ordersheet_blank")
                    path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_IL_Blank_OrderSheet.rdlc";

            }
            

            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("CompanyID", CompanyID.ToString()));
            //parameters.Add("prm", "RDLC report (Set as parameter)");
            LocalReport report = new LocalReport();         
            report.ReportPath = path;
            report.DataSources.Add(new ReportDataSource("dsEmployee", dt));
            report.SetParameters(parameters);
            //}
            byte[] pdf = report.Render("PDF");
            return File(pdf, "application/pdf");

        }
    }
}
