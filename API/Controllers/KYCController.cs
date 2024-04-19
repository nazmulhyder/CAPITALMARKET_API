using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Approval;
using Model.DTOs.KYC;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KYCController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<KYCController> _logger;
        public KYCController(IService service, ILogger<KYCController> logger)
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

        [HttpGet("PendingKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllPendingKYC(int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.kYCRepository.PendingKYCAML(CompanyID, BranchID, LoggedOnUser()));
                else if (CompanyID == 4)
                    return getResponse(await _service.kYCRepository.PendingKYCSL(CompanyID, BranchID, LoggedOnUser()));               
                else
                    return getResponse(await _service.kYCRepository.PendingKYCIL(CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CompleteKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCompleteKYC(int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                  return getResponse(await _service.kYCRepository.CompleteKYCAML(CompanyID, BranchID, LoggedOnUser()));

                else if (CompanyID == 3)
                    return getResponse(await _service.kYCRepository.CompleteKYCIL(CompanyID, BranchID, LoggedOnUser()));
                else
                    return getResponse(await _service.kYCRepository.CompleteKYCSL(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        #region KYC-IDLCSL
        [HttpGet("CodesKYC/{CompanyID}/{BranchID}/{TypeName}/{TypeID}")]
        public async Task<IActionResult> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, string TypeName, string TypeID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.CodesKYC(CompanyID, BranchID, LoggedOnUser(), TypeName, TypeID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("KYCAccountInfo/{CompanyID}/{BranchID}/{ContractID}/{ProductID}")]
        public async Task<IActionResult> KYCAccountInfo(int CompanyID, int BranchID, int ContractID, int ProductID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.KYCAccountInfoSL(CompanyID, BranchID, LoggedOnUser(), ContractID, ProductID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateKYC/{CompanyID}/{BranchID}/{ActionType}")]
        public async Task<IActionResult> InsertUpdateKYC(int CompanyID, int BranchID, KYCDto KycEntry, string ActionType)
        {
            try
            {
                return getResponse(await _service.kYCRepository.InsertUpdateKYCSL(CompanyID, BranchID, LoggedOnUser(), KycEntry, ActionType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListApprovalKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListApprovalKYC(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.ListApprovalKYCSL(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> KYCApproval(int CompanyID, int BranchID, KYCApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.kYCRepository.KYCApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ReviewComScreenKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ReviewComScreenKYC(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.ReviewComScreenKYCSL(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion KYC-IDLCSL


        #region KYC-IDLCIL
        [HttpPost("IL/InsertUpdateKYC/{CompanyID}/{BranchID}/{ActionType}")]
        public async Task<IActionResult> InsertUpdateKYCIL(int CompanyID, int BranchID, KYCILDto KycEntry, string ActionType)
        {
            try
            {
                return getResponse(await _service.kYCRepository.InsertUpdateKYCIL(CompanyID, BranchID, LoggedOnUser(), KycEntry, ActionType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IL/KYCAccountInfo/{CompanyID}/{BranchID}/{ContractID}/{ProductID}")]
        public async Task<IActionResult> ILKYCAccountInfo(int CompanyID, int BranchID, int ContractID, int ProductID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.KYCAccountInfoIL(CompanyID, BranchID, LoggedOnUser(), ContractID, ProductID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IL/ListApprovalKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListApprovalKYCIL(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.ListApprovalKYCIL(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("IL/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> KYCApprovalIL(int CompanyID, int BranchID, KYCApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.kYCRepository.KYCApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IL/ReviewComScreenKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ReviewComScreenKYCIL(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.ReviewComScreenKYCIL(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion KYC-IDLCIL


        #region KYC-IDLCAML

        [HttpPost("AML/InsertUpdateKYC/{CompanyID}/{BranchID}/{ActionType}")]
        public async Task<IActionResult> InsertUpdateKYCAML(int CompanyID, int BranchID, KYCAMLDto KycEntry, string ActionType)
        {
            try
            {
                return getResponse(await _service.kYCRepository.InsertUpdateKYCAML(CompanyID, BranchID, LoggedOnUser(), KycEntry, ActionType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AML/KYCAccountInfo/{CompanyID}/{BranchID}/{ContractID}/{ProductID}")]
        public async Task<IActionResult> AMLKYCAccountInfo(int CompanyID, int BranchID, int ContractID, int ProductID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.KYCAccountInfoAML(CompanyID, BranchID, LoggedOnUser(), ContractID, ProductID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AML/ListApprovalKYC/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListApprovalKYCAML(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.kYCRepository.ListApprovalKYCAML(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AML/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> KYCApprovalAML(int CompanyID, int BranchID, KYCApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.kYCRepository.KYCApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AML/AllKYC/{CompanyID}/{BranchID}/{Status}")]
        public async Task<IActionResult> AllKYC(int CompanyID, int BranchID, string Status)
        {
            try
            {
                 return getResponse(await _service.kYCRepository.AllKYCAML(CompanyID, BranchID, LoggedOnUser(), Status));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        //[HttpGet("AML/ReviewComScreenKYC/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> ReviewComScreenKYCAML(int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        return getResponse(await _service.kYCRepository.ReviewComScreenKYCAML(CompanyID, BranchID, LoggedOnUser()));

        //    }

        //    catch (Exception ex) { return getResponse(ex); }
        //}
        #endregion KYC-IDLCAML

    }
}
