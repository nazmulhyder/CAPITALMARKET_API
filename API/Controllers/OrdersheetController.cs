using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.DTOs.OrderSheet;
using Model.DTOs.Withdrawal;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using Service.Interface;
using System.Diagnostics.Contracts;
using System.Security.Principal;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersheetController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<OrdersheetController> _logger;

        public OrdersheetController(IService service, ILogger<OrdersheetController> logger)
        {
            _service = service;
            _logger = logger;
        }

        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;

        }

        [HttpGet("ListOrdersheet/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{FilterType}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> ListOrdersheet(int CompanyID, int BranchID, int PageNo, int PerPage,string FilterType, string SearchKeyword, string ListType)
        {
            try
            {
                //string userName = LoggedOnUser();
                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.ListOrdersheetSL(CompanyID, BranchID, PageNo, PerPage, FilterType, SearchKeyword, ListType));
                }
                else
                {
                    return getResponse(await _service.orderSheetRepository.ListOrdersheetIL(CompanyID, BranchID, PageNo, PerPage, FilterType, SearchKeyword, ListType));
                }
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetOrdersheet/{CompanyID}/{BranchID}/{ContractID}")]
        public async Task<IActionResult> GetByAccountNo(int CompanyID, int BranchID, int ContractID)
        {
            try
            {
                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.GetOrdersheetDetailsSL(LoggedOnUser(), CompanyID, BranchID, ContractID));
                }
                else
                {
                    return getResponse(await _service.orderSheetRepository.GetOrdersheetDetailsIL(LoggedOnUser(), CompanyID, BranchID, ContractID));
                }


               
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UpdateOrdersheet/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateOrdersheet(int CompanyID, int BranchID, OrderSheetDTO entryDto)
        {
            try
            {
                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.UpdateOrdersheetSL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                }
                else
                {
                    return getResponse(await _service.orderSheetRepository.UpdateOrdersheetIL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                }

                
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("OrdersheetApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> OrdersheetApproval(int CompanyID, int BranchID, OrdersheetApprovalDTO approvalRequest)
        {
            try
            {
                var result = "";

                   if(CompanyID == 4)
                    result = await _service.orderSheetRepository.OrdersheetApprovalSL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (CompanyID == 3)
                    result = await _service.orderSheetRepository.OrdersheetApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListOrdersheetPrint/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}")]
        public async Task<IActionResult> ListOrdersheetPrint(int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            try
            {

                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.GetListOrdersheetPrintSL(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo));
                }
                else
                {
                    return getResponse(await _service.orderSheetRepository.GetListOrdersheetPrintIL(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo));
                }

               
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("OrdersheetListPrintPDF/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GenOrdersheetPDF(int CompanyID, int BranchID, List<OrderSheetPrintDTO>? orderSheets)
        {
            try
            {
                var document = new PdfDocument();
                PdfGenerator.AddPdfPages(document, CompanyID == 4? _service.orderSheetRepository.GetOrdersheetPrintStringSL(LoggedOnUser(), CompanyID, BranchID,orderSheets) : _service.orderSheetRepository.GetOrdersheetPrintStringIL(LoggedOnUser(), CompanyID, BranchID, orderSheets), PageSize.A4);
                byte[]? response = null;

                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    response = ms.ToArray();
                }

                string fileName = "ordersheet" + ".pdf";
                //return getResponse(await _service.orderSheetRepository.GetListOrdersheetPrintSL(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo));
                var res = File(response, "application/pdf", fileName);
                return getResponse(res);
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        //[HttpPost("UpdateOrdersheetPrintStatus")]
        //public async Task<IActionResult> UpdateOrdersheetPrintStatus(int CompanyID, int BranchID, int ContractID)
        //{
        //    try
        //    {
        //        return getResponse(await _service.orderSheetRepository.UpdateOrdersheetPrintStatusSL(CompanyID, BranchID, LoggedOnUser(), ContractID));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}


        [HttpGet("GetAvailableOrdersheetRelease/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}")]
        public async Task<IActionResult> GetAvailableOrdersheetRelease(int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            try
            {

                if (CompanyID == 4)
                {
                    var res = await _service.orderSheetRepository.GetAvailableOrdersheetReleaseSL(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo);
                    return getResponse(res == null? null: res, res == null ? "No ordersheet found for this account" : null);
                }
                else
                {
                    var res = await _service.orderSheetRepository.GetAvailableOrdersheetReleaseIL(LoggedOnUser(), CompanyID, BranchID, ProductID, AccountNo);
                    return getResponse(res == null ? null : res, res == null ? "No ordersheet found for this account" : null);
                }

               
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UpdateOrdersheetReleaseStatus")]
        public async Task<IActionResult> UpdateOrdersheetReleaseStatus(int CompanyID, int BranchID, OrdersheetReleasedDTO releasedOrdersheet)
        {
            try
            {

                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.UpdateOrdersheetReleaseStatusSL(CompanyID, BranchID, LoggedOnUser(), releasedOrdersheet));
                }
                else
                {
                   var res = await _service.orderSheetRepository.UpdateOrdersheetReleaseStatusIL(CompanyID, BranchID, LoggedOnUser(), releasedOrdersheet);
                   return getResponse(await _service.orderSheetRepository.UpdateOrdersheetReleaseStatusIL(CompanyID, BranchID, LoggedOnUser(), releasedOrdersheet));
                }
             }

               
            catch (Exception ex) {
                return getResponse(null, ex.Message.Split("-").FirstOrDefault()); 
            }
        }

        [HttpGet("ListZeroOrdersheet/{CompanyID}/{BranchID}/{ProductID}/{NoOfRemainingSheet}")]
        public async Task<IActionResult> ListZeroOrdersheet(int CompanyID, int BranchID, int ProductID, int NoOfRemainingSheet)
        {
            try
            {
                if (CompanyID == 4)
                {
                    return getResponse(await _service.orderSheetRepository.GetListZeroOrdersheetSL(LoggedOnUser(), CompanyID, BranchID, ProductID, NoOfRemainingSheet));
                }
                else
                {
                    return getResponse(await _service.orderSheetRepository.GetListZeroOrdersheetIL(LoggedOnUser(), CompanyID, BranchID, ProductID, NoOfRemainingSheet));
                }
            }

            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
