using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.TradeRestriction;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TradeRestrictionController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<TradeRestrictionController> _logger;

        public TradeRestrictionController(IService service, ILogger<TradeRestrictionController> logger)
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

        #region Trade Restriction [Product]
        [HttpPost("AddUpdateTradeRestrictionProduct/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateTradeRestrictionProduct(int CompanyID,int BranchID,TradeRestrictionpProductDto tradeRestrictionEntity)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.TradeRestrictions.AddUpdate(CompanyID,BranchID,tradeRestrictionEntity, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListTradeRestrictionProduct/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListTradeRestrictionProduct(int CompanyID,int BranchID,int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionProduct(CompanyID,BranchID,PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetTradeRestrictionByProductID/{ProductID}")]
        public async Task<IActionResult> GetTradeRestrictionByProduct(int CompanyID,int BranchID,int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.QueryTradeRestrictionProduct(CompanyID,BranchID,ProductID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListApprovalTradeRestrictionProduct/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListApprovalTradeRestrictionProduct(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionApprovalOnProduct(CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Trade Restriction [Account Group]
        [HttpGet("ListTradeRestrictionAccGrp/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListTradeRestrictionAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionAccGrp(CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdateTradeRestrictionAcGrp/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateTradeRestrictionAcGrp(int CompanyID, int BranchID, TradeRestrictionAccGrpDto tradeRestrictionEntity)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.TradeRestrictions.AddUpdateTradeRestrictionAccGrp(CompanyID, BranchID, tradeRestrictionEntity, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetTradeRestrictionByAccountGroup/{CompanyID}/{BranchID}/{AccountGroupID}")]
        public async Task<IActionResult> GetTradeRestrictionByAccountGroup(int CompanyID, int BranchID, int AccountGroupID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.QueryTradeRestrictionAccGrp(CompanyID, BranchID, AccountGroupID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListApprovalTradeRestrictionAccGrp/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListApprovalTradeRestrictionAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionApprovalOnAccGrp(CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Trade Restriction [Account]
        [HttpGet("ListTradeRestrictionAccount/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListTradeRestrictionAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionAccount(CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdateTradeRestrictionAccount/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateTradeRestrictionAccount(int CompanyID, int BranchID, TradeRestrictionAccountDto tradeRestrictionEntity)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.TradeRestrictions.AddUpdateTradeRestrictionAccount(CompanyID, BranchID, tradeRestrictionEntity, userName));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetTradeRestrictionByAccount/{CompanyID}/{BranchID}/{ContractID}")]
        public async Task<IActionResult> GetTradeRestrictionByAccount(int CompanyID, int BranchID, int ContractID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.QueryTradeRestrictionAccount(CompanyID, BranchID, ContractID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetApprovalTradeRestrictionByAccount/{CompanyID}/{BranchID}/{ContractID}")]
        public async Task<IActionResult> GetApprovalTradeRestrictionByAccount(int CompanyID, int BranchID, int ContractID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.QueryApprovalTradeRestrictionAccount(CompanyID, BranchID, ContractID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListApprovalTradeRestrictionAccount/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> ListApprovalTradeRestrictionAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.TradeRestrictions.GetAllTradeRestrictionApprovalOnAccount(CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion

        [HttpPost("RestrictionApproval/{CompanyID}")]
        public async Task<IActionResult> RestrictionApproval(int CompanyID, RestrictionApprovalDto approvalRequest)
        {
            try
            {
                var result = await _service.TradeRestrictions.RestrictionApproval(LoggedOnUser(), CompanyID, approvalRequest);
                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
