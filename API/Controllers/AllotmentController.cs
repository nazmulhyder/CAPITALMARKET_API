using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Allocation;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.OrderSheet;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AllotmentController : ControllerBase
    {

        private readonly IService _service;
        private readonly ILogger<AllotmentController> _logger;
        public AllotmentController(IService service, ILogger<AllotmentController> logger)
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

        #region Gsec Allotment

        [HttpPost("InsertUpdateAllotment/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateAllotment(int CompanyID, int BranchID, AllotmentEntryDto? entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.AllotmentRepository.InsertUpdateAllotmentIL(CompanyID, BranchID, LoggedOnUser(), entryDto));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("AML/InsertUpdateAllotment/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateAllotmentAML(int CompanyID, int BranchID, AMLAllotmentEntryDto? entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                 return getResponse(await _service.AllotmentRepository.InsertUpdateAllotmentAML(CompanyID, BranchID, LoggedOnUser(), entryDto));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/Gsec/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GsecListForAllotment(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.GSecForAllotmentListIL(CompanyID, BranchID));
                else
                    return getResponse(await _service.AllotmentRepository.GSecForAllotmentListAML(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSecAllotmentApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GSecAllotmentApproval(int CompanyID, int BranchID, GsecAllotmentApprovalDto approvalRequest)
        {
            try
            {
                var result = "";
                if (CompanyID == 3)
                    result = await _service.AllotmentRepository.GSecAllotmentApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                else
                    result = await _service.AllotmentRepository.GSecAllotmentApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListGSecAllotment/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListOrdersheet(int CompanyID, int BranchID, FilterGSecAllotment filter)
        {
            try
            {
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.ListGSecAllotmentIL(CompanyID, BranchID, filter));
                else
                    return getResponse(await _service.AllotmentRepository.ListGSecAllotmentAML(CompanyID, BranchID, filter));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetGsecAllotment/{CompanyID}/{BranchID}/{OMIBuyID}")]
        public async Task<IActionResult> GetGsecAllotment(int CompanyID, int BranchID, int OMIBuyID)
        {
            try
            {
                if(CompanyID == 3)
                return getResponse(await _service.AllotmentRepository.GetGSecAllotmentIL(LoggedOnUser(),CompanyID, BranchID, OMIBuyID));
                else
                    return getResponse(await _service.AllotmentRepository.GetGSecAllotmentAML(LoggedOnUser(), CompanyID, BranchID, OMIBuyID));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Coupon Collection
        [HttpGet("CouponCollection/GetAllGSecInstrumentHolding/{CompanyID}/{BranchID}/{InstrumentID}")]
        public async Task<IActionResult> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, int InstrumentID)
        {
            try
            {
                return getResponse(await _service.AllotmentRepository.GetAllGSecInstrumentHolding(CompanyID, BranchID, InstrumentID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("CouponCollection/List/Gsec/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GsecListForCouponCollection(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.AllotmentRepository.GsecListForCouponCollection(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/InsertDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertDeclaration(int CompanyID, int BranchID, CouponCol_declarationDto entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID ==3)
                 return getResponse(await _service.AllotmentRepository.InsertCouponColDeclarationIL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                else
                 return getResponse(await _service.AllotmentRepository.InsertCouponColDeclarationAML(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/LastThreeDeclaretionEntries/{CompanyID}/{BranchID}/{InstrumentID}")]
        public async Task<IActionResult> LastThreeDeclaretionEntries(int CompanyID, int BranchID, int InstrumentID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.LastThreeDeclaretionEntriesIL(CompanyID, BranchID,InstrumentID));
                else
                    return getResponse(await _service.AllotmentRepository.LastThreeDeclaretionEntriesAML(CompanyID, BranchID, InstrumentID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/InsertCouponCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertCouponCollection(int CompanyID, int BranchID, CouponCollectionEntryDto couponCollection)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 3)
                return getResponse(await _service.AllotmentRepository.InsertCouponCollectionIL(CompanyID, BranchID, LoggedOnUser(), couponCollection));
                else
                    return getResponse(await _service.AllotmentRepository.InsertCouponCollectionAML(CompanyID, BranchID, LoggedOnUser(), couponCollection));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CouponCollection/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> CouponCollectionList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.ListCouponCollectionIL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));
                else
                    return getResponse(await _service.AllotmentRepository.ListCouponCollectionAML(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CouponCollectionApproval(int CompanyID, int BranchID, CouponCollectionApprovalDto approvalRequest)
        {
            try
            {
                var result = "";
                if(CompanyID ==3)
                result = await _service.AllotmentRepository.CouponCollectionApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                else
                    result = await _service.AllotmentRepository.CouponCollectionApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        #endregion

        #region Reversal
        [HttpPost("CouponCollection/Reversal/GetCollectionCouponForReversal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCollectionCouponForReversal(int CompanyID, int BranchID, GetCollectionCouponForReversal collectionCouponForReversal)
        {
            try
            {
                return getResponse(await _service.AllotmentRepository.GetCollectionCouponForReversal(CompanyID, BranchID, collectionCouponForReversal.ProductID.GetValueOrDefault(), collectionCouponForReversal.AccountNo, collectionCouponForReversal.instrumentID.GetValueOrDefault(), collectionCouponForReversal.FromDate, collectionCouponForReversal.ToDate, collectionCouponForReversal.FundID.GetValueOrDefault()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/Reversal/InsertCouponCollectionReversal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertCouponCollectionReversal(int CompanyID, int BranchID, CouponCol_reversalDto entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID == 3)
                return getResponse(await _service.AllotmentRepository.InsertCouponCollectionReversalIL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                else
                    return getResponse(await _service.AllotmentRepository.InsertCouponCollectionReversalAML(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CouponCollection/Reversal/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> CouponCollectionReversalList(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.ListCouponCollectionReversalIL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));
                else
                return getResponse(await _service.AllotmentRepository.ListCouponCollectionReversalAML(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CouponCollection/Reversal/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CouponCollectionReversalApproval(int CompanyID, int BranchID, CouponCollectionReversalApprovalDto approvalRequest)
        {
            try
            {
                var result = "";
                if (CompanyID == 3)
                    result = await _service.AllotmentRepository.CouponCollectionReversalApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                else
                    result = await _service.AllotmentRepository.CouponCollectionReversalApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Off market sale
        [HttpGet("GSec/Sale/GetOffMktSaleInstrumentHolding/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{FundID}")]
        public async Task<IActionResult> GetOffMktSaleInstrumentHolding(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            try
            {
                return getResponse(await _service.AllotmentRepository.GetAllOffMktSaleInsHolding(CompanyID, BranchID, ProductID, AccountNo, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSec/Sale/InsertUpdateOffMktSale/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateOffMktSale(int CompanyID, int BranchID, GSecOffMktInsSaleDto entryDto)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.InsertOffMktInsSaleIL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                else
                    return getResponse(await _service.AllotmentRepository.InsertOffMktInsSaleAML(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSec/Sale/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> OffMarketSaleApproval(int CompanyID, int BranchID, OffMktSaleApprovalDto approvalRequest)
        {
            try
            {
                var result = "";
                if (CompanyID == 3)
                    result = await _service.AllotmentRepository.OffMktSaleApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                else
                    result = await _service.AllotmentRepository.OffMktSaleApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GSec/Sale/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        public async Task<IActionResult> ListGSecOffMktInsSale(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        {
            try
            {
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.ListGSecOffMktInsSaleIL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));
                else
                     return getResponse(await _service.AllotmentRepository.ListGSecOffMktInsSaleAML(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region Redemption
        [HttpGet("Gsec/Redemption/{CompanyID}/{BranchID}/{InstrumentID}")]
        public async Task<IActionResult> RedemptionHoldings(int CompanyID, int BranchID, int InstrumentID)
        {
            try
            {
                if(CompanyID ==3)
                return getResponse(await _service.AllotmentRepository.GetRedemptionInsHoldingIL(CompanyID, BranchID, InstrumentID));
                else
                    return getResponse(await _service.AllotmentRepository.GetRedemptionInsHoldingAML(CompanyID, BranchID, InstrumentID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSec/Redemption/InsertGSecRedemption/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertGSecRedemption(int CompanyID, int BranchID, InstrumentRedemption entryDto)
        {
            try
            {
                return getResponse(await _service.AllotmentRepository.InsertGSecRedemptionIL(LoggedOnUser(),CompanyID, BranchID,  entryDto));
                
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AML/GSec/Redemption/InsertGSecRedemption/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertGSecRedemptionaAML(int CompanyID, int BranchID, InstrumentRedemptionAML entryDto)
        {
            try
            {
                  return getResponse(await _service.AllotmentRepository.InsertGSecRedemptionAML(LoggedOnUser(), CompanyID, BranchID, entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AML/GSec/TBillZeroCupon/Redemption/InsertGSecRedemption/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertGSecRedemptionAML(int CompanyID, int BranchID, InstrumentRedemptionAML entryDto)
        {
            try
            {
                 return getResponse(await _service.AllotmentRepository.InsertGSecRedemptionAML(LoggedOnUser(), CompanyID, BranchID, entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSec/Redemption/Approval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GSecInstrumentRedemptionApproval(int CompanyID, int BranchID, InstrumentRedemptionApprovalDto approvalRequest)
        {
            try
            {
                var result = "";
                if (CompanyID == 3)
                    result = await _service.AllotmentRepository.GSecInstrumentRedemptionApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                else
                    result = await _service.AllotmentRepository.GSecInstrumentRedemptionApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GSec/TBillZCoupon/Redemption/List/{CompanyID}/{BranchID}/{FundID}/{ListType}/{FromDate}/{ToDate}")]
        public async Task<IActionResult> GSecTBillZCouponRedemptionApproval(int CompanyID, int BranchID, int FundID, string ListType, string FromDate, string ToDate)
        {
            try
            {
                 return getResponse(await _service.AllotmentRepository.TBillZCouponRedemptionListAML(CompanyID, BranchID, FundID, ListType, FromDate, ToDate));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        //[HttpGet("GSec/Redemption/List/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}")]
        //public async Task<IActionResult> ListGSecInsRedemption(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType)
        //{
        //    try
        //    {
        //        return getResponse(await _service.AllotmentRepository.ListGSecInsRedemption(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType));

        //    }

        //    catch (Exception ex) { return getResponse(ex); }
        //}

        [HttpGet("List/Gsec/Redemption/{CompanyID}/{BranchID}/{ListType}")]
        public async Task<IActionResult> GSecForRedemptionList(int CompanyID, int BranchID, string ListType)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID == 3)
                return getResponse(await _service.AllotmentRepository.GSecForRedemptionListIL(CompanyID, BranchID, ListType));
                else
                    return getResponse(await _service.AllotmentRepository.GSecForRedemptionListAML(CompanyID, BranchID, ListType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Gsec/Redemption/GetRedemption/{CompanyID}/{BranchID}/{InstrumentID}")]
        public async Task<IActionResult> GetRedemption(int CompanyID, int BranchID, int InstrumentID)
        {
            try
            {
                if (CompanyID == 3)
                    return getResponse(await _service.AllotmentRepository.GetRedemptionByIDIL(CompanyID, BranchID, InstrumentID));
                else
                   return getResponse(await _service.AllotmentRepository.GetRedemptionByIDAML(CompanyID, BranchID, InstrumentID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetCleanPrice")]
        public async Task<IActionResult> GetCleanPrice(int ComID, int BranchID, CalculateCleanPrice calculateCleanPrice)
        {
            try
            {              
                    return getResponse(_service.AllotmentRepository.GetCleanPrice(ComID, BranchID, calculateCleanPrice));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SettlementCalculator")]
        public async Task<IActionResult> SettlementCalculator(int ComID, int BranchID, SettlementCalculate calculateCleanPrice)
        {
            try
            {
                return getResponse( await _service.AllotmentRepository.CalculateSettlementValue(ComID, BranchID, calculateCleanPrice));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion
    }
}

