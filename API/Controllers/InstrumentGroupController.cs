using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.InstrumentGroup;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class InstrumentGroupController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<InstrumentGroupController> _logger;

        public InstrumentGroupController(IService service, ILogger<InstrumentGroupController> logger)
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

        [HttpPost("AddUpdateInstrumentGroup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateInstrumentGroup(InstrumentGroupDto data, int CompanyID, int BranchID)
        {
            try
            {
                //return getResponse(await _service.InstrumentGroups.AddUpdateInsGrp(data, LoggedOnUser(), CompanyID, BranchID));
                var result = await _service.InstrumentGroups.AddUpdateInsGrp(data, LoggedOnUser(), CompanyID, BranchID);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetInstrumentGroupList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var reponse = await _service.InstrumentGroups.GetAllInstrumentGrp(LoggedOnUser(), CompanyID, BranchID ,PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Approval-List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetInstrumentGroupApprovalList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var reponse = await _service.InstrumentGroups.GetAllInstrumentGrpApproval(LoggedOnUser(), CompanyID, BranchID, PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetInstrumentGroupByID/{ID}")]
        public async Task<IActionResult> GetInstrumentGroupByID(int ID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse =  _service.InstrumentGroups.GetById(ID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("InstrumentDropdown")]
        public async Task<IActionResult> InstrumentDropdown()
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.InstrumentGroups.InstrumentDropdown();
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("InstrumentGroupDropdown")]
        public async Task<IActionResult> InstrumentGroupDropdown()
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.InstrumentGroups.InstrumentGroupDropdown();
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("InstrumentByInstrumentGrps/{InsGrpIds}")]
        public async Task<IActionResult> InstrumentByInstrumentGrps(string InsGrpIds)
        {
            try
            {
                string userName = LoggedOnUser();
                var _InsGrpIds = InsGrpIds.Substring(0, InsGrpIds.Length - 1);
                var reponse = await _service.InstrumentGroups.InstrumentByInsGrpIds(_InsGrpIds);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("InstrumentGroupApproval")]
        public async Task<IActionResult> InstrumentGroupApproval(InstrumentGroupApprovalDto approvalRequest)
        {
            try
            {
                return getResponse(await _service.InstrumentGroups.InstrumentGroupApproval(LoggedOnUser(), approvalRequest));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
