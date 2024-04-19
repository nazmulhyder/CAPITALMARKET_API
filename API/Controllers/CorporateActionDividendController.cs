using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Right;
using Service.Interface;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CorporateActionDividendController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CorporateActionDividendController> _logger;
        public CorporateActionDividendController(IService service, ILogger<CorporateActionDividendController> logger)
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

        [HttpPost("AddUpdateCorporateActRightDividend/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCorpActRightDividend(CorpActRightDeclarationDTO entryCorpActRightDeclaration, int CompanyID, int BranchID)
        {
            try
            {
                //entryCorpActRightDeclaration.SubscriptionClosedDate = Utility.DatetimeFormatter.DateFormat(entryCorpActRightDeclaration.SubscriptionClosedDate);
                entryCorpActRightDeclaration.AGMDate = Utility.DatetimeFormatter.DateFormat(entryCorpActRightDeclaration.AGMDate);
                entryCorpActRightDeclaration.RecordDate = Utility.DatetimeFormatter.DateFormat(entryCorpActRightDeclaration.RecordDate);
                entryCorpActRightDeclaration.YearEndDate = Utility.DatetimeFormatter.DateFormat(entryCorpActRightDeclaration.YearEndDate);
                entryCorpActRightDeclaration.DeclareDate = Utility.DatetimeFormatter.DateFormat(entryCorpActRightDeclaration.DeclareDate);

                return getResponse(await _service.corporateActionDividend.InsertCorpActRightDividend(entryCorpActRightDeclaration, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorporateActionDividendDeclarationList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCorpActDivResult(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetAllCorpActDivResult(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("DividendDeclarationList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCorpActDivDeclaration(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetAllCorpActDivDeclaration(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorpActDividendListbyID/{CorpActDeclarationID}")]
        public async Task<IActionResult> GetCorpActDividendListbyID(int CorpActDeclarationID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetCorpActDividendListbyID(CorpActDeclarationID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CorporateActionDividendApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetCorpActDividendApproval(objCorpActDividendApproval, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILAMLCorporateActionClaimList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActionClaimList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetCorpActionClaimList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ILAMLCorpActionClaim/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.corporateActionDividend.ILAMLCorpActionClaim(objILAMLCorpActionClaim, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorpActILAMLClaimApprovalList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLClaimApprovalList(CorpActDeclarationID, CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CorpActDividendBonusClaimApproval/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.corporateActionDividend.InsertCADBonusClaimApproval(entryBonusApproval, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ILAMLUpdateStockDivClaimApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLUpdateStockDivClaimApprove(objILAMLUpdateStockDivClaimApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILAMLCorporateActionCashDividendClaimList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActionCashClaimList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetCorpActionCashClaimList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ILAMLCorpActionCashClaim/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.corporateActionDividend.ILAMLCorpActionCashClaim(objILAMLCorpActionCashClaim, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorpActILAMLClaimCashApprovalList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLCashClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLCashClaimApprovalList(CorpActDeclarationID, CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ILAMLUpdateCashDivClaimApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLUpdateCashDivClaimApprove(objILAMLUpdateCashDivClaimApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CorpActDividendBonusCashClaimApproval/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.corporateActionDividend.InsertCADBonusCashClaimApproval(entryBonusCashApproval, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILAMLCorporateActionBonusFractionClaimList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActionBonusFractionClaimList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetCorpActionBonusFractionClaimList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ILAMLCorpActionBonusFractionClaim/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> ILAMLCorpActionBonusFractionClaim(ILAMLCorpActionBonusFractionClaim objILAMLCorpActionBonusFractionClaim, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.corporateActionDividend.ILAMLCorpActionBonusFractionClaim(objILAMLCorpActionBonusFractionClaim, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILAMLCorpActionBonusFractionClaimApprovalList/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLCorpActionBonusFractionClaimApprovalList(int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.corporateActionDividend.ILAMLCorpActionBonusFractionClaimApprovalList(CorActDeclarationID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("ILAMLUpdateBonusFractionClaimApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLUpdateBonusFractionClaimApprove(ILAMLCorpActionBonusFractionUpdate objILAMLUpdateBonusFractionClaimApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLUpdateBonusFractionClaimApprove(objILAMLUpdateBonusFractionClaimApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMCorpActDividendBonusFractionClaimApproval/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertILAMLCorpActDividendBonusFractionClaimApproval(ILAMLCorpActionBonusFractionClaim entryBonusFractionApproval, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.corporateActionDividend.InsertILAMLCorpActDividendBonusFractionClaimApproval(entryBonusFractionApproval, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMInsertUpdateSLCorpActDivCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertSLCorpActDivCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActDivCollection, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.corporateActionDividend.InsertSLCorpActDivCollection(entryCorpActDivCollection, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMCorporateActionCashCollectionList/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCorpActCashCollectionResult(int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetAllCorpActCashCollectionResult(CorActDeclarationID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMCorpActionCashCollectionApprovalList/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActionCashCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.corporateActionDividend.GetSLCorpActionCashCollectionApprovalList(CorActDeclarationID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("CMUpdateNetAmountCorpActDivCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLUpdateNetAmountSLCorpActDivCollection(SLCorpActionDividendCollectionDTO objSLUpdateNetAmountSLCorpActDivCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetSLUpdateNetAmountSLCorpActDivCollection(objSLUpdateNetAmountSLCorpActDivCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedCorpActDivCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActDivCollectionApprove(SLCorporateActionCashCollectionApproveDTO objSLCorpActDivCollectionApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetSLCorpActDivCollectionApprove(objSLCorpActDivCollectionApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMCorpActionBonusFractionCollectionList/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActionBonusFractionCollectionList(int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetSLCorpActionBonusFractionCollectionList(CompanyID, BranchID, CorActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMInsertUpdateCorpActBonusFractionCollection/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> InsertILAMLCorpActBonusCollection(ILAMLCorpActionBonusCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID)
        //{
        //    try
        //    {


        //        return getResponse(await _service.corporateActionDividend.InsertILAMLCorpActBonusCollection(entryCorpActBonusCollection, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        public async Task<IActionResult> InsertSLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.corporateActionDividend.InsertSLCorpActBonusFractionCollection(entryCorpActBonusFractionCollection, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMCorpActionBonusFractionCollectionApprovalList/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActionBonusFractionCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.corporateActionDividend.GetSLCorpActionBonusFractionCollectionApprovalList(CorActDeclarationID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("CMUpdateNetAmountSLCorpActBonusFrcCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLUpdateNetAmountSLCorpActBonusFrcCollection(SLCorpActionDividendBonusFractionCollectionDTO objSLUpdateNetAmountSLCorpActBonusFrcCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetSLUpdateNetAmountSLCorpActBonusFrcCollection(objSLUpdateNetAmountSLCorpActBonusFrcCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedCorpActDivBonusFrcCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActDivBonusFrcCollectionApprove(SLCorpActionDividendBonusFractionApproveDTO objSLCorpActDivBonusFrcCollectionApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetSLCorpActDivBonusFrcCollectionApprove(objSLCorpActDivBonusFrcCollectionApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ILAMLCorpActionBonusCollectionList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLCorpActionBonusCollectionList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetILAMLCorpActionBonusCollectionList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ILAMLInsertUpdateCorpActBonusCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertILAMLCorpActBonusCollection(ILAMLCorpActionBonusCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.corporateActionDividend.InsertILAMLCorpActBonusCollection(entryCorpActBonusCollection, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        //public async Task<IActionResult> InsertSLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID)
        //{
        //    try
        //    {


        //        return getResponse(await _service.corporateActionDividend.InsertSLCorpActBonusFractionCollection(entryCorpActBonusFractionCollection, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        [HttpGet("ILAMLCorpActionBonusCollectionApprovalList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLCorpActionBonusCollectionApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetILAMLCorpActionBonusCollectionApprovalList(CorpActDeclarationID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ILAMLUpdateBonusCollectionApprove/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLUpdateBonusCollectionApprove(ILAMLCorpActionBonusCollectionUpdateDTO objILAMLUpdateBonusCollectionApprove, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLUpdateBonusCollectionApprove(objILAMLUpdateBonusCollectionApprove, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("ILAMLApprovedCorpActDivBonusCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetILAMLApprovedCorpActDivBonusCollection(ILAMLCorpActionDividendBonusCollectionApproveDTO objILAMLApprovedCorpActDivBonusCollection, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetILAMLApprovedCorpActDivBonusCollection(objILAMLApprovedCorpActDivBonusCollection, CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("SLCorporateActionCashCollectionList/{ProductID}/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLCorpActCashCollectionResult(int ProductID, int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corporateActionDividend.GetSLCorpActCashCollectionResult(ProductID, CorActDeclarationID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }

            //[HttpPost("CorpActDividendBonusCashClaimApprovals/{CompanyID}/{BranchID}")]

            //public string joy()
            //{

            //    return "Saiful";
            //}


        }

        [HttpPost("SLInsertUpdateCorpActBonusCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertSLCorpActBonusCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.corporateActionDividend.InsertSLCorpActBonusCollection(entryCorpActBonusCollection, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet("SLCorpActionBonusFractionCollectionList/{ProductID}/{CorActDeclarationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetOnlySLCorpActionBonusFractionCollectionList(int Productid,int CorActDeclarationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corporateActionDividend.GetOnlySLCorpActionBonusFractionCollectionList(Productid,CompanyID, BranchID, CorActDeclarationID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {CorActDeclarationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("SLInsertUpdateCorpActBonusFractionCollection/{CompanyID}/{BranchID}")]


        public async Task<IActionResult> InsertOnlySLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.corporateActionDividend.InsertOnlySLCorpActBonusFractionCollection(entryCorpActBonusFractionCollection, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}

