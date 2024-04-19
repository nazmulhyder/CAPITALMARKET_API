using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.Divident;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DividendController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<DividendController> _logger;
        public DividendController(IService service, ILogger<DividendController> logger)
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

        [HttpPost("InsertUpdate/DividendDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertDeclaration(int CompanyID, int BranchID, DividendDisbursementDto entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.InsertUpdateDevidendDeclaration(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CalculateTotalDividendPayable/DividendDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CalculateTotalDividendPayable(int CompanyID, int BranchID, DivCalculationParamDto divCalculationParam)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.CalculateTotalDividendPayable(CompanyID, BranchID, LoggedOnUser(), divCalculationParam));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListDividend/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}/{FundID}")]
        public async Task<IActionResult> ListDividendAML(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType, int FundID)
        {
            try
            {
                return getResponse(await _service.divident.ListDividendAML(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("DividendApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DividendApprovalAML(int CompanyID, int BranchID, DividendApprovalDto approvalDto)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.DividendApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        // cash dividend distribution

        [HttpGet("CashDividendInfoList/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> CashDividendInfoListAML(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.CashDividendInfoListAML(CompanyID, BranchID, FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CashDividendDistributionList/{CompanyID}/{BranchID}/{MFDividendDecID}")]
        public async Task<IActionResult> CashDividendDistributionListAML(int CompanyID, int BranchID, int MFDividendDecID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.CashDividendDistributionListAML(CompanyID, BranchID, MFDividendDecID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateCashDividendDistribution/{CompanyID}/{BranchID}/")]
        public async Task<IActionResult> InsertUpdateCashDividendDistribution(int CompanyID, int BranchID, List<CashDividendDistribution>? cashDividendDistributions)
        {
            try
            {
                string userName = LoggedOnUser();
                //string MFDividendEntIDs = string.Join(",", cashDividendDistributions.Select(p => p.MFDividendEntID));
                return getResponse(await _service.divident.InsertUpdateCashDividendDistribution(CompanyID, BranchID, LoggedOnUser(), cashDividendDistributions));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetCashDividendApproval/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> GetCashDividendApprovalAML(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.GetCashDividendApprovalAML(CompanyID, BranchID, LoggedOnUser(), FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CashDividendDistributionApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CashDividendDistributionApproval(int CompanyID, int BranchID, CashDividentDistributionApprovalDto approval)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.CashDividentDistributionApproval(LoggedOnUser(), CompanyID, BranchID, approval));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetCashDivByRecord/{CompanyID}/{BranchID}/{DeclarationID}")]
        public async Task<IActionResult> GetCashDivByRecord(int CompanyID, int BranchID, int DeclarationID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.GetCashDivByRecord( CompanyID, BranchID, LoggedOnUser(), DeclarationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }



        // STOCK divident distribution

        [HttpGet("StockDividendInfoList/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> StockDividendInfoListAML(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.StockDividendInfoListAML(CompanyID, BranchID, FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("StockDividendDistributionList/{CompanyID}/{BranchID}/{MFDividendDecIDs}")]
        public async Task<IActionResult> StockDividendDistributionListAML(int CompanyID, int BranchID, string MFDividendDecIDs)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.StockDividendDistributionListAML(CompanyID, BranchID, MFDividendDecIDs));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateStockDividendDistribution/{CompanyID}/{BranchID}/")]
        public async Task<IActionResult> InsertUpdateStockDividendDistribution(int CompanyID, int BranchID, List<CIPDividendDistribution>? cipDividendDistributions)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.InsertUpdateStockDividendDistribution(CompanyID, BranchID, LoggedOnUser(), cipDividendDistributions));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetStockDividendApproval/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> GetCIPDividendApprovalAML(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.GetCIPDividendApprovalAML(CompanyID, BranchID, LoggedOnUser(), FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("StockDividendDistributionApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> StockDividendDistributionApproval(int CompanyID, int BranchID, StockDividentDistributionApprovalDto  approval)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.StockDividentDistributionApproval(LoggedOnUser(), CompanyID, BranchID, approval));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetGLBalance/{CompanyID}/{BranchID}/{FundID}/{GLCode}")]
        public async Task<IActionResult> GetGLBalance(int CompanyID, int BranchID, int FundID, string GLCode)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.GetGLBalance(CompanyID, BranchID, FundID,GLCode));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetCIPDivByRecord/{CompanyID}/{BranchID}/{DeclarationID}")]
        public async Task<IActionResult> GetCIPDivByRecord(int CompanyID, int BranchID, int DeclarationID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.GetStockDivByRecord(CompanyID, BranchID, LoggedOnUser(), DeclarationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        //dividend allocation
        [HttpPost("Dividend/Allocation/{CompanyID}/{BranchID}/{DivDeclarationID}/{PayoutType}/{Nav}")]
        public async Task<IActionResult> DividentAllotcationDistribution(int CompanyID, int BranchID, int DivDeclarationID, string PayoutType, decimal Nav)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.divident.DividentAllotcationDistribution(CompanyID, BranchID, DivDeclarationID, PayoutType, Nav));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
