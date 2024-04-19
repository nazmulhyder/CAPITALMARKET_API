using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.FDR;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FDRController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<FDRController> _logger;
        public FDRController(IService service, ILogger<FDRController> logger)
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

        [HttpGet("GetClientBalanceInfo/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{FundID}")]
        public async Task<IActionResult> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            try
            {
                 return getResponse(await _service.fDRRepository.FDR_GetClientAbailableBalanceIL_AML(CompanyID, BranchID, LoggedOnUser() ,ProductID, AccountNo, FundID));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateDepositAccountOpening/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertDeclaration(int CompanyID, int BranchID, DepositOpeningDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.fDRRepository.InsertUpdateDepositAccountOpening(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/{CompanyID}/{BranchID}/{FromDate}/{ToDate}/{SearchKeyword}/{ListType}/{FundID}")]
        public async Task<IActionResult> ListDepositBankAccountOpening(int CompanyID, int BranchID, string FromDate, string ToDate ,string SearchKeyword, string ListType,int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.ListDepositBankAccountOpening(CompanyID, BranchID, FromDate, ToDate, SearchKeyword, ListType, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ApproveDepositBankAccountOpening(int CompanyID, int BranchID, DepositAccountOpeningApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                if(CompanyID == 3)
                result = await _service.fDRRepository.DepositBankAccountOpeningApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (CompanyID == 2)
                    result = await _service.fDRRepository.DepositBankAccountOpeningApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetInterestCollectionInfo/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{FundID}")]
        public async Task<IActionResult> GetInterestCollectionInfo(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.InterestCollectionInfo(CompanyID, BranchID, LoggedOnUser(), ProductID, AccountNo, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateDepositInterestCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateDepositInterestCollection(int CompanyID, int BranchID, InterestCollectionDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.fDRRepository.InsertUpdateDepositInterestCollection(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/DepositInterest/{CompanyID}/{BranchID}/{DateFrom}/{DateTo}/{SearchKeyword}/{ListType}/{FundID}")]
        public async Task<IActionResult> ListDepositInterest(int CompanyID, int BranchID, string DateFrom, string DateTo, string SearchKeyword, string ListType, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.ListDepositInterestCollection(CompanyID, BranchID, DateFrom, DateTo, SearchKeyword, ListType, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approval/DepositInterest/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DepositIntCollectionApproval(int CompanyID, int BranchID, DepositIntCollectionApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                if(CompanyID ==3)
                result = await _service.fDRRepository.DepositIntCollectionApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (CompanyID == 2)
                result = await _service.fDRRepository.DepositIntCollectionApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetInterestCollectionReversalInfo/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{DepositAccNo}/{FundID}")]
        public async Task<IActionResult> GetInterestCollectionReversalInfo(int CompanyID, int BranchID, int ProductID, string AccountNo,string DepositAccNo, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.GetInterestCollectionInfoForReversal(CompanyID, BranchID, LoggedOnUser(), ProductID, AccountNo, DepositAccNo, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateDepositInterestCollReversal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateDepositInterestCollectionReversal(int CompanyID, int BranchID, DepositInterestCollectionReversalDto entry)
        {
            try
            {
                return getResponse(await _service.fDRRepository.InsertUpdateDepositInterestCollectionReversal(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/Reversal/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> ListDepositInterestReversal(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                return getResponse(await _service.fDRRepository.ListDepositInterestCollectionReversalIL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/Reversal/DepositInterest/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DepositIntCollectionReversalApproval(int CompanyID, int BranchID, DepositInterestReversalApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                if(CompanyID ==3)
                result = await _service.fDRRepository.DepositIntCollectionReversalApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (CompanyID == 2)
                    result = await _service.fDRRepository.DepositIntCollectionReversalApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        //encashment

        [HttpGet("GetDepositInterestInfoForEncashment/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{FundID}")]
        public async Task<IActionResult> GetDepositInterestInfoForEncashment(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.GetDepositInterestInfoForEncashment(CompanyID, BranchID, LoggedOnUser(), ProductID, AccountNo, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateDepositInterestEncashment/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateDepositInterestEncashment(int CompanyID, int BranchID, DepositEncashmentDto entry)
        {
            try
            {
                return getResponse(await _service.fDRRepository.InsertUpdateDepositInterestEncashment(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/Encashment/{CompanyID}/{BranchID}/{FromDate}/{ToDate}/{SearchKeyword}/{ListType}/{FundID}")]
        public async Task<IActionResult> ListDepositInterestEncashment(int CompanyID, int BranchID, string FromDate, string ToDate, string SearchKeyword, string ListType, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.ListDepositInterestEncashmentIL_AML(CompanyID, BranchID, FromDate, ToDate, SearchKeyword, ListType,FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approval/Encashment/DepositInterest/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DepositIntEncashmentApproval(int CompanyID, int BranchID, DepositInterestEncashmentApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                if(CompanyID ==3)
                result = await _service.fDRRepository.DepositIntEncashmentApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (CompanyID == 2)
                    result = await _service.fDRRepository.DepositIntEncashmentApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        //renewal

        [HttpPost("InsertUpdateDepositInterestRenewal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateDepositInterestRenewal(int CompanyID, int BranchID, DepositInstrumentRenewal entry)
        {
            try
            {
                return getResponse(await _service.fDRRepository.InsertUpdateDepositAccountRenewalAML(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetRenewalList/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> GetRenewalList(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.GetRenewalListAML(CompanyID, BranchID, LoggedOnUser(), FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/RenewalList/{CompanyID}/{BranchID}/{FromDate}/{ToDate}/{ListType}/{FundID}")]
        public async Task<IActionResult> GetRenewalList(int CompanyID, int BranchID, string FromDate, string ToDate,  string ListType, int FundID)
        {
            try
            {
                return getResponse(await _service.fDRRepository.RenewalListAML(CompanyID, BranchID, LoggedOnUser(), FromDate, ToDate, FundID, ListType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approval/DepositInterest/Renewal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> DepositIntRenewalApproval(int CompanyID, int BranchID, DepositInterestRenewalApprovalDto approvalRequest)
        {
            try
            {
                return getResponse(await _service.fDRRepository.DepositAccountRenewalApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }


}
