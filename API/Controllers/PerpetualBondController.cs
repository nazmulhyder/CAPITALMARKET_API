using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.PerpetualBond;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PerpetualBondController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<AllotmentController> _logger;
        public PerpetualBondController(IService service, ILogger<AllotmentController> logger)
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


        [HttpGet("List/Active/Instrument/Bond/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GsecListForCouponCollection(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.PBActiveBondInstrumentList(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("Declaration/InsertDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertDeclaration(int CompanyID, int BranchID, CouponCol_declarationDto entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.InsertCouponColDeclaration(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Declaration/LastThreeDeclaretionEntries/{CompanyID}/{BranchID}/{InstrumentID}")]
        public async Task<IActionResult> LastThreeDeclaretionEntries(int CompanyID, int BranchID, int InstrumentID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.LastThreeDeclaretionEntries(CompanyID, BranchID, InstrumentID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/New/Declaration/Instrument/Bond/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BondNewDeclaredInstrument(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.BondNewDeclaredInstrument(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/PerpetualBond/Holdings/{CompanyID}/{BranchID}/{InstrumentID}/{ProductID}/{DeclarationID}")]
        public async Task<IActionResult> PerpetualBondHoldings(int CompanyID, int BranchID, int InstrumentID, int ProductID ,int DeclarationID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.GetPerpetualBondHoldings(CompanyID, BranchID, InstrumentID, ProductID, DeclarationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/PerpetualBond/Claim/Holdings/{CompanyID}/{BranchID}/{InstrumentID}/{Year}/{DeclarationID}")]
        public async Task<IActionResult> PerpetualBondClaimedHoldings(int CompanyID, int BranchID, int InstrumentID, string Year, int DeclarationID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.GetPerpetualBondHoldings_Claim(CompanyID, BranchID, InstrumentID, Year, DeclarationID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertPerpetualBond/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertPerpetualBond(int CompanyID, int BranchID, List<PerpetualBondClaim> perpetualBonds)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.InsertPerpetualBond(CompanyID, BranchID, LoggedOnUser(), perpetualBonds));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("InsertPerpetualBondClaim/{CompanyID}/{BranchID}/{DeclarationID}/{ProductID}")]
        public async Task<IActionResult> InsertPerpetualBondClaim(int CompanyID, int BranchID, List<PerpetualBond> perpetualBonds, int DeclarationID, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.perpetualBondRepository.InsertPerpetualBondClaim(CompanyID, BranchID, LoggedOnUser(), perpetualBonds, DeclarationID, ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("PerpetualBond/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> PerpetualBondList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.ListPerpetualBond(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("PerpetualBond/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PerpetualBondApproval(int CompanyID, int BranchID, PerpetualBondApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.perpetualBondRepository.PerpetualBondApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("PerpetualBond/Declaration/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PerpetualBondDeclarationApproval(int CompanyID, int BranchID, PerpetualBondDeclarationApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.perpetualBondRepository.PerpetualBondDeclarationApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("PerpetualBond/Declaration/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> PerpetualBondDeclarationList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.ListPerpetualBondDeclaration(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetPerpetualBond/Reversal/List/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{InstrumentID}")]
        public async Task<IActionResult> PerpetualBondDeclarationList(int CompanyID, int BranchID, int ProductID, string AccountNo, int InstrumentID)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.ListPerpetualBondForReversal(CompanyID, BranchID, ProductID, AccountNo, InstrumentID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertPerpetualBondReversal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertPerpetualBondReversal(int CompanyID, int BranchID, List<PerpetualBondReversalDto> perpetualBondReversals)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.InsertPerpetualBondReversal(CompanyID, BranchID, LoggedOnUser(), perpetualBondReversals));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Reversal/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PerpetualBondReversalApproval(int CompanyID, int BranchID, PerpetualBondReversalApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.perpetualBondRepository.PerpetualBondReversalApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Reversal/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> ListPerpetualBondReversal(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.ListPerpetualBondReversal(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/PerpetualBondClaim/{CompanyID}/{BranchID}/{Year}")]
        public async Task<IActionResult> PerpetualBondClaim(int CompanyID, int BranchID, string Year)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.PerpetualBondClaimList(CompanyID, BranchID, Year));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Collection/FilterDeclarationList/{CompanyID}/{BranchID}/{InstrumentID}/{Year}")]
        public async Task<IActionResult> FilterDeclarationList(int CompanyID, int BranchID, int InstrumentID, string Year)
        {
            try
            {
                return getResponse(await _service.perpetualBondRepository.FilterDeclarationList(CompanyID, BranchID, InstrumentID, Year));

            }

            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
