using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using Service.Interface;
using System.Data.SqlClient;
using System.Data;
using System.Security.Principal;

namespace Api.ReportControllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IService _service;
        private readonly ILogger<SubReportController> _logger;

        public SubReportController(IService service, ILogger<SubReportController> logger, IWebHostEnvironment webHostEnvironment)
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

        public class MainReportData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // Add other properties as needed
        }

        public class Subreport1Data
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            // Add other properties as needed
        }

        public class Subreport2Data
        {
            public int Id { get; set; }
            public string OrderNumber { get; set; }
            public decimal TotalAmount { get; set; }
            // Add other properties as needed
        }

        [HttpGet("report/{CompanyID}/{BranchID}/{VoucherIDs}/{FileType}")]
        public async Task<IActionResult> VoucherPrintReport(int CompanyID, int BranchID, string VoucherIDs, string FileType)
        {
            try
            {
                var mainReportData = GetMainReportData();
                var subreport1Data = GetSubreport1Data();
                var subreport2Data = GetSubreport2Data();

                // Set the path of the main RDLC report file
                var mainReportPath = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_SubReport.rdlc";

                // Create an instance of LocalReport
                LocalReport localReport = new LocalReport();

                // Add data sources for the main report
                localReport.DataSources.Add(new ReportDataSource("MainReportDataSet", mainReportData));

                // Add data sources for the subreports
                localReport.DataSources.Add(new ReportDataSource("Subreport1DataSet", subreport1Data));
                localReport.DataSources.Add(new ReportDataSource("Subreport2DataSet", subreport2Data));


                // Render the report as PDF
                var renderedBytes = localReport.Render("PDF");

                // Return the rendered report to the client
                return File(renderedBytes, "application/pdf", "Report.pdf");

                //SqlParameter[] sqlParams = new SqlParameter[4];

                //sqlParams[0] = new SqlParameter("@UserName", LoggedOnUser());
                //sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                //sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                //sqlParams[3] = new SqlParameter("@voucherIDs", VoucherIDs);

                //DataSet DataSets = await _service.reportRepository.GetDataSet(sqlParams, "[dbo].[Report_CM_Voucher_Print]");

                //DataTable dtVoucher = DataSets.Tables[0];
                //DataTable dtVoucherLedgerList = DataSets.Tables[1];


                //string mimetype = "";
                //int extension = 1;
                //string[] Streams = null;
                //var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reports\\Report_CM_SubReport.rdlc";

                //List<ReportParameter> parameters = new List<ReportParameter>();

                //LocalReport report = new LocalReport();
                //report.ReportPath = path;
                //report.DataSources.Add(new ReportDataSource("dsVoucher", dtVoucher));
                //report.DataSources.Add(new ReportDataSource("dsVoucherLedgerList", dtVoucherLedgerList));

                //if (FileType == "pdf")
                //{
                //    byte[] pdf = report.Render("PDF");
                //    return File(pdf, "application/pdf");
                //}
                //else if (FileType == "excel")
                //{
                //    byte[] pdf = report.Render("Excel");
                //    return File(pdf, "application/msexcel", fileDownloadName: "Report_CM_SubReport.xls");
                //}
                //else //default pdf
                //{
                //    byte[] pdf = report.Render("PDF");
                //    return File(pdf, "application/pdf");
                //}
            }
            catch (Exception ex) { return Ok(ex.Message); }
        }
        private IEnumerable<MainReportData> GetMainReportData()
        {
            var mainReportData = new List<MainReportData>();

            // Add your logic to fetch the data from the data source
            // For example:
            mainReportData.Add(new MainReportData { Id = 1, Name = "John Doe" });
            mainReportData.Add(new MainReportData { Id = 2, Name = "Jane Smith" });

            return mainReportData;
        }

        private IEnumerable<Subreport1Data> GetSubreport1Data()
        {
            // Retrieve and return the data for subreport 1
            var subreport1Data = new List<Subreport1Data>();

            // Add your logic to fetch the data from the data source
            // For example:
            subreport1Data.Add(new Subreport1Data { Id = 1, ProductName = "Product A", Quantity = 10 });
            subreport1Data.Add(new Subreport1Data { Id = 2, ProductName = "Product B", Quantity = 5 });

            return subreport1Data;
        }

        private IEnumerable<Subreport2Data> GetSubreport2Data()
        {
            // Retrieve and return the data for subreport 2
            var subreport2Data = new List<Subreport2Data>();

            // Add your logic to fetch the data from the data source
            // For example:
            subreport2Data.Add(new Subreport2Data { Id = 1, OrderNumber = "ORD001", TotalAmount = 100 });
            subreport2Data.Add(new Subreport2Data { Id = 2, OrderNumber = "ORD002", TotalAmount = 200 });

            return subreport2Data;
        }
    }
}
