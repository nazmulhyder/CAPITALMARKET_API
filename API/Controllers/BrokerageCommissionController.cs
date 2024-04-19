using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.BrokerageCommision;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerageCommissionController : ControllerBase
    {

        private readonly IService _service;
        private readonly ILogger<BrokerageCommissionController> _logger;
        public BrokerageCommissionController(IService service, ILogger<BrokerageCommissionController> logger)
        {
            _service = service;
            _logger = logger;
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

        [HttpPost]
        [Route("ApproveTradeDataUpdateForCommisionUpdate")]
        public async Task<IActionResult> ApproveTradeDataUpdateForCommisionUpdate(ApproveTradeDataCommisionUpdatDto approveData)
        {
            try
            {
                var data = await _service.brokerageCommision.ApproveTradeDataUpdateForCommisionUpdate(LoggedOnUser(), approveData);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpGet]
        [Route("GetTradeDataCommisionUpdateContractList/{TradeDate}")]
        public async Task<IActionResult> GetTradeDataCommisionUpdateContractListDto( DateTime TradeDate)
        {
            try
            {
                var data = await _service.brokerageCommision.GetTradeDataCommisionUpdateContractListDto(LoggedOnUser(), TradeDate);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }


        [HttpGet]
        [Route("GetTradeDataForCommisionUpdate/{ContractID}/{TradeDate}")]
        public async Task<IActionResult> GetTradeDataForCommisionUpdate(int ContractID,DateTime TradeDate)
        {
            try
            {
                var data = await _service.brokerageCommision.GetTradeDataForCommisionUpdate(LoggedOnUser(), ContractID, TradeDate);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }


        [HttpPost]
        [Route("BrokerageCommisionApprove")]
        public async Task<IActionResult> ApproveBrokerageCommision(BrokerageCommisionApproveDto Approval)
        {
            try
            {
                var data = await _service.brokerageCommision.ApproveBrokerageCommision(Approval, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }


        }

   
        [HttpPost]
        [Route("UpdateBrokerageCommisionItem/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateBrokerageCommisionItem( BrokerageCommisionItemDto commision, int CompanyID,int BranchID)
        {
            try
            {
                var data = await _service.brokerageCommision.UpdateBrokerageCommisionItem(commision, LoggedOnUser(), CompanyID, BranchID);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpGet]
        [Route("GeBrokerageCommisionItem/{ContractID}")]
        public async Task<IActionResult> GeBrokerageCommisionItem(int   ContractID)
        {
            try
            {
                var data = await _service.brokerageCommision.GeBrokerageCommisionItem(ContractID, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }
        [HttpGet]
        [Route("GeBrokerageCommisionList/{CompanyID}/{UserName}/{ApprovalStatus}/{PageNo}/{Perpage}/{SearchKeyword}")]
        public async Task<IActionResult> GeBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword)
        {
            try
            {
                var data = await _service.brokerageCommision.GeBrokerageCommisionList(CompanyID, UserName, ApprovalStatus, PageNo, Perpage, SearchKeyword);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetAMLBrokerageCommisionList/{CompanyID}/{UserName}/{ApprovalStatus}")]
        public async Task<IActionResult> GetAMLBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus)
        {
            try
            {
                var data = await _service.brokerageCommisionAML.GetBrokerageCommisionList(CompanyID, UserName, ApprovalStatus);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetAMLBrokerageCommissionDetail/{ContractID}")]
        public async Task<IActionResult> GetAMLBrokerageCommisionList(int ContractID)
        {
            try
            {
                var data = await _service.brokerageCommisionAML.GetAMLBrokerageCommisionByContractID(ContractID, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost]
        [Route("UpdateAMLBrokerageCommission/{CompanyID}/{BranchID}")]
        public async  Task<IActionResult> UpdateAMLBrokerageCommission(List<BrokerageCommissionAMLDetailDto>? BrokerList, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var data = await _service.brokerageCommisionAML.UpdateAMLBrokerageCommission(BrokerList,userName, CompanyID, BranchID);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("AMLBrokerageCommisionApprove")]
        public async Task<IActionResult> ApproveAMLBrokerageCommision(BrokerageCommisionApproveDto Approval)
        {
            try
            {
                var data = await _service.brokerageCommisionAML.ApproveAMLBrokerageCommision(Approval, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

       

    }
}
