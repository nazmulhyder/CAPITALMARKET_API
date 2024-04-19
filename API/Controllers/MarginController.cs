using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.MarginRequest;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MarginController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<MarginController> _logger;
        public MarginController(IService service, ILogger<MarginController> logger)
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


        [HttpGet("GetClientInfoForMarginRequest/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}")]
        public async Task<IActionResult> GetClientInfoForMarginRequest(int CompanyID, int BranchID, int ProductID, string AccountNo)
        {
            try
            {
                return getResponse(await _service.marginRepository.GetClientInfoForMarginRequest(CompanyID, BranchID, LoggedOnUser(), ProductID, AccountNo));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateMarginRequest/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateMarginRequest(int CompanyID, int BranchID, MarginRquestDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.marginRepository.InsertUpdateMarginRequest(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("ListViewMarginRequestStatus/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> ListViewMarginRequestStatus(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                return getResponse(await _service.marginRepository.ListViewMarginRequestStatus(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListViewCompletedMarginRequestStatus/{CompanyID}/{BranchID}/")]
        public async Task<IActionResult> ListViewCompletedMarginRequestStatus(int CompanyID, int BranchID, FilterDto? filter)
        {
            try
            {
                return getResponse(await _service.marginRepository.ListViewCompletedMarginRequestStatus(CompanyID, BranchID, filter));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetMarginRequest/{CompanyID}/{BranchID}/{MarginRequestID}")]
        public async Task<IActionResult> GetMarginRequest(int CompanyID, int BranchID, int MarginRequestID)
        {
            try
            {
                return getResponse(await _service.marginRepository.GetMarginRequestById(CompanyID, BranchID, LoggedOnUser(),MarginRequestID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListMarginMonitoring/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListMarginMonitoring(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.marginRepository.ListMarginMonitoring(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SentMarginMonitoringSMSEmail/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> MarginMonitoringSMSEmailSent(int CompanyID, int BranchID, MarginMonitoringSMSEmailDto marginMonitoringSMSEmail)
        {
            try
            {
                return getResponse(await _service.marginRepository.MarginMonitoringSMSEmailSent(CompanyID, BranchID, LoggedOnUser(), marginMonitoringSMSEmail));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("CodesMarginRequest/{CompanyID}/{BranchID}/{TypeName}")]
        public async Task<IActionResult> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, string TypeName)
        {
            try
            {
                return getResponse(await _service.marginRepository.CodesMarginRequest(CompanyID, BranchID, LoggedOnUser(), TypeName));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CodesMarginRequestJson/{CompanyID}/{BranchID}/{MarginReqID}")]
        public async Task<IActionResult> CodesMarginRequest(int CompanyID, int BranchID, int MarginReqID)
        {
            try
            {
                return getResponse(await _service.marginRepository.AllCodesMargin(CompanyID, BranchID, LoggedOnUser(), MarginReqID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
