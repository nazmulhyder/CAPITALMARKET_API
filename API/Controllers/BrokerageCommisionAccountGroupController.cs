using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Model.DTOs.BrokerageCommisionAccountGroup;
using Service.Interface;
using System.Security.Principal;
using Model.DTOs.BrokerageCommision;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerageCommisionAccountGroupController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<BrokerageCommisionAccountGroupController> _logger;
        public BrokerageCommisionAccountGroupController(IService service, ILogger<BrokerageCommisionAccountGroupController> logger)
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
        [Route("BrokerageCommisionAccountGroupApprove")]
        public async Task<IActionResult> ApproveBrokerageCommisionAccountGroup(BrokerageCommisionAccountGroupApproveDto Approval)
        {
            try
            {
                var data = await _service.brokerageCommisionAccountGroup.ApproveBrokerageCommisionAccountGroup(Approval, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }


        }

        [HttpGet]
        [Route("GetBrokerageCommisionAccountGroupList/{CompanyID}/{UserName}/{ApprovalStatus}/{PageNo}/{Perpage}/{SearchKeyword}")]
        public async Task<IActionResult> GetBrokerageCommisionAccountGroupList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword)
        {
            try
            {
                var data = await _service.brokerageCommisionAccountGroup.GetBrokerageCommisionAccountGroupList(CompanyID, UserName, ApprovalStatus, PageNo, Perpage, SearchKeyword);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetUnapprovedBrokerageCommisionAccountGroupList/{CompanyID}/{UserName}/{PageNo}/{Perpage}/{SearchKeyword}")]
        public async Task<IActionResult> GetUnapprovedBrokerageCommisionAccountGroupList(int CompanyID, string UserName, int PageNo, int Perpage, string SearchKeyword)
        {
            try
            {
                var data = await _service.brokerageCommisionAccountGroup.GetUnapprovedBrokerageCommisionAccountGroupList(CompanyID, UserName,  PageNo, Perpage, SearchKeyword);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetBrokerageCommisionAccountGroupItem/{AccountGroupID}")]
        public async Task<IActionResult> GetBrokerageCommisionAccountGroupItem(int AccountGroupID)
        {
            try
            {
                var data = await _service.brokerageCommisionAccountGroup.GetBrokerageCommisionAccountGroupItem(AccountGroupID, LoggedOnUser());
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }


        [HttpPost]
        [Route("UpdateBrokerageCommisionAccountGroupItem/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateBrokerageCommisionAccountGroupItem(BrokerageCommisionAccountGroupItemDto commision, int CompanyID, int BranchID)
        {
            try
            {
                var data = await _service.brokerageCommisionAccountGroup.UpdateBrokerageCommisionAccountGroupItem(commision, LoggedOnUser(), CompanyID, BranchID);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }

        }
    }
}
