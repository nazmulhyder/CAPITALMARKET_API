using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Charges;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChargesController : ControllerBase
    {
        private readonly ILogger<ChargesController> _logger;
        private readonly IService _service;

        public ChargesController(ILogger<ChargesController> logger, IService service)
        {
            _logger = logger;
            _service = service;
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

        [HttpGet("GetChargeList/{CompanyID}/{BranchID}/{ListType}/{ManualEntryEnable}/{AdjustmentEnable}")]
        public async Task<IActionResult> GetChargeList(int CompanyID, int BranchID, string ListType, string ManualEntryEnable, string AdjustmentEnable)
        {
            try
            {
                return getResponse(await _service.chargesRepository.GetChargeList(LoggedOnUser(), CompanyID, BranchID, ListType, ManualEntryEnable, AdjustmentEnable));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetChargeDetail/{CompanyID}/{BranchID}/{AttributeID}")]
        public async Task<IActionResult> GetChargeDetail(int CompanyID, int BranchID, int AttributeID)
        {
            try
            {
                return getResponse(await _service.chargesRepository.GetChargeDetail(LoggedOnUser(), CompanyID, BranchID, AttributeID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveUpdateChargeDetail/{CompanyID}/{BranchID}/{ChargeAmount}")]
        public async Task<IActionResult> SaveUpdateChargeDetail(int CompanyID, int BranchID, decimal ChargeAmount, ChargeSetupDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.SaveUpdateChargeDetail(LoggedOnUser(), CompanyID, BranchID, ChargeAmount, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("ChargeApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ChargeApproval(int CompanyID, int BranchID, ChargeApprovalDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ChargeApproval(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AccrualChargeFileValidateUpload/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AccrualChargeFileValidateUpload(int CompanyID, int BranchID, IFormCollection data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.AccrualChargeFileValidateUpload(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListAccrualChargeFile/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListAccrualChargeFile(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ListAccrualChargeFile(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListAccrualChargeFileDetail/{CompanyID}/{BranchID}/{ChargeFileID}")]
        public async Task<IActionResult> ListAccrualChargeFileDetail(int CompanyID, int BranchID, int ChargeFileID)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ListAccrualChargeFileDetail(CompanyID, BranchID, ChargeFileID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AccrualChargeFileApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AccrualChargeFileApproval(int CompanyID, int BranchID, AccrualChargeFileApprovalDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.AccrualChargeFileApproval(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetAccruedChargeAccountList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAccrualAccountList(int CompanyID, int BranchID, AccrualAccountFilterDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.GetAccrualAccountList(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("UpdateAccruedChargeScheduleStatus/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ApproveAccruedChargeSchedule(int CompanyID, int BranchID, AccrualChargeApprovalDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ApproveAccruedChargeSchedule(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetCLientInfoForManualChargeEntry/{CompanyID}/{BranchID}/{AccountNumber}")]
        public async Task<IActionResult> GetCLientInfoForManualChargeEntry(int CompanyID, int BranchID, string AccountNumber)
        {
            try
            {
                return getResponse(await _service.chargesRepository.GetCLientInfoForManualChargeEntry(LoggedOnUser(), CompanyID, BranchID, AccountNumber));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ManualChargeEntry/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ManualChargeEntry(int CompanyID, int BranchID, ManualChargeDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ManualChargeEntry(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListManualCharge/{CompanyID}/{BranchID}/{ListType}")]
        public async Task<IActionResult> ListManualCharge(int CompanyID, int BranchID, string ListType)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ListManualCharge(LoggedOnUser(), CompanyID, BranchID, ListType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ManualChargeApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ManualChargeApprove(int CompanyID, int BranchID, ManualChargeApproveDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ManualChargeApprove(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("BulkManualChargeEntryValidation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BulkManualChargeEntryValidation(int CompanyID, int BranchID, IFormCollection data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.BulkManualChargeEntryValidation(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("BulkManualChargeEntry/{CompanyID}/{BranchID}/{AttributeID}/{ProductID}")]
        public async Task<IActionResult> BulkManualChargeEntry(int CompanyID, int BranchID,int AttributeID, int ProductID, List<ManualChargeBulkDto> data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.BulkManualChargeEntry(LoggedOnUser(), CompanyID, BranchID, AttributeID, ProductID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        

        [HttpPost("ListAccountChargeForReversalEntry/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListAccountChargeForReversalEntry( int CompanyID, int BranchID, AccountChargeForReversalEntryDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ListAccountChargeForReversalEntry(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AccountChargeReversalEntry/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AccountChargeReversalEntry(int CompanyID, int BranchID, AccountChargeReversalEntryDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.AccountChargeReversalEntry(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AccountChargeReversalList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AccountChargeReversalList(int CompanyID, int BranchID, AccountChargeReversalDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.AccountChargeReversalList(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AccountChargeReversalApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AccountChargeReversalApprove(int CompanyID, int BranchID, AccountChargeReversalEntryDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.AccountChargeReversalApprove(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("ChargeAdjustmentEntry/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ChargeAdjustmentEntry(int CompanyID, int BranchID, ManualChargeDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ChargeAdjustmentEntry(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListChargeAdjustment/{CompanyID}/{BranchID}/{ListType}")]
        public async Task<IActionResult> ListChargeAdjustment(int CompanyID, int BranchID, string ListType)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ListChargeAdjustment(LoggedOnUser(), CompanyID, BranchID, ListType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ChargeAdjustmentApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ChargeAdjustmentApprove(int CompanyID, int BranchID, ManualChargeApproveDto data)
        {
            try
            {
                return getResponse(await _service.chargesRepository.ChargeAdjustmentApprove(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
