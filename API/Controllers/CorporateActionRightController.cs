using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.DTOs.IPO;
using Model.DTOs.Right;
using Service.Interface;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CorporateActionRightController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CorporateActionRightController> _logger;
        public CorporateActionRightController(IService service, ILogger<CorporateActionRightController> logger)
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

        [HttpPost("AddUpdateCorporateInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertRightInstrumentSetting(RightDTO entryRightDTO, int CompanyID, int BranchID)
        {
            try
            {
                entryRightDTO.SubscriptionClosedDate = Utility.DatetimeFormatter.DateFormat(entryRightDTO.SubscriptionClosedDate);
                entryRightDTO.SubscriptionOpenDate = Utility.DatetimeFormatter.DateFormat(entryRightDTO.SubscriptionOpenDate);
                entryRightDTO.RecordDate = Utility.DatetimeFormatter.DateFormat(entryRightDTO.RecordDate);
                entryRightDTO.DeclareDate = Utility.DatetimeFormatter.DateFormat(entryRightDTO.DeclareDate);

                return getResponse(await _service.corpActRepository.InsertRightInstrumentSetting(entryRightDTO,CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorporateInstrumentRightList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCorpActResult(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetAllCorpActResult(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorpActRightListbyID/{RightInstSettingID}")]
        public async Task<IActionResult> GetCorpActRightListbyID(int RightInstSettingID)
        {
            try
            {
                return getResponse(await _service.corpActRepository.GetCorpActRightListbyID(RightInstSettingID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {RightInstSettingID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CorporateActionRightApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActApproval(RightApproveDTO objCorpActRightApproval)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetCorpActApproval(objCorpActRightApproval, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("InstrumentInformationforRightsApplication/{RightInstSettingID}/{CompanyID}/{BranchID}")]
        public IActionResult GetInstrumentInformationforRightsApplication(int RightInstSettingID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.corpActRepository.GetInstrumentInformationforRightsApplication(RightInstSettingID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"RightsApplication Not Found: {RightInstSettingID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("InvestorInformationforRightsApplication/{RightInstSettingID}/{ProductID}/{AccountNumber}/{CompanyID}/{BranchID}")]
        public IActionResult GetInvestorInformationforRightsApplication(int RightInstSettingID, int ProductID, string AccountNumber, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.corpActRepository.GetInvestorInformationforRightsApplication(RightInstSettingID, ProductID, AccountNumber, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"InvestorInformation Not Found: {ProductID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("AddUpdateRightApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertRightApplication(RightApplicatonDTO entryRightAppDTO, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corpActRepository.InsertRightApplication(entryRightAppDTO, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    return getResponse(new Exception("Right Application Already Exist."));
               else return getResponse(ex); }
        }

        [HttpGet("RightsApplicationOrderList/{RightInstSettingID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRightsApplicationOrderList(int RightInstSettingID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetRightsApplicationOrderList(RightInstSettingID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("RightsApplicationListbyID/{RightsApplicationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRightsApplicationListbyID(int RightsApplicationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corpActRepository.GetRightsApplicationListbyID(RightsApplicationID,CompanyID,BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"RightsApplication not found with this id: {RightsApplicationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("RightsApplicationforApproval/{RightInstSettingID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRightsApplicationforApproval(int RightInstSettingID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetRightsApplicationforApproval(RightInstSettingID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApprovedRightsApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult>RightsApplicationApproved(RightsApplication RightApplicatonListApproved, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.corpActRepository.RightsApplicationApproved(RightApplicatonListApproved, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorpActRightsReversalApprovalList/{CompanyID}/{BranchID}")] 
        public async Task<IActionResult> GetRightsReversalList(int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetRightsReversalList(CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CorpActRightsReversalApproval/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCARReversalApproval(RightsReversalMaster entryRightsReversal, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.corpActRepository.InsertCARReversalApproval(entryRightsReversal, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("RightsApplicationBulkList/{ProductID}/{RightInstSettingID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetRightsApplicationBulkList(int ProductID, int RightInstSettingID, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetRightsApplicationBulkList(ProductID, RightInstSettingID, Maker, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddBulkCARightsApplication/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertCARightBulk(RightsBulkMaster entryRightsBulk, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.corpActRepository.InsertCARightBulk(entryRightsBulk, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CARInstrumentCollectionList/{RightInstSettingID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAMLCARInstrumentCollectionList(int RightInstSettingID, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetAMLCARInstrumentCollectionList(RightInstSettingID, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLCorpActRInstrumentApproveRequest/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> AMLCorpActRightInstrumentApproveRequest(AMLRightsCollectionMaster AMLCorpActRightCollectionReq, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corpActRepository.AMLCorpActRightInstrumentApproveRequest(AMLCorpActRightCollectionReq, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("ILAMLCorpActRightCollectionApprovalList/{RightInstSettingID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCorpActRightCollectionApprovalList(int RightInstSettingID,int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetCorpActRightCollectionApprovalList(RightInstSettingID,CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLApproveCorpActRightInstrumentCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> AMLCorpActRightInstrumentApproved(AMLCorpActRightCollectionApproved AMLCorpActRightInstrumentList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.corpActRepository.AMLCorpActRightInstrumentApproved(AMLCorpActRightInstrumentList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CorporateInstrumentRightApprovalList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllCorpActDeclarationApproval(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.corpActRepository.GetAllCorpActDeclarationApproval(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("ApprovedRightsApplicationById/{RightsApplicationID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CARApplicationApprovedbyID(RightsApplication CorpActApprovedList, int RightsApplicationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.corpActRepository.CARApplicationApprovedbyID(CorpActApprovedList, RightsApplicationID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        //[HttpGet("CorpActDividendListbyID/{CorpActDeclarationID}")]
        //public async Task<IActionResult> GetCorpActDividendListbyID(int CorpActDeclarationID)
        //{
        //    try
        //    {
        //        return getResponse(await _service.corpActRepository.GetCorpActDividendListbyID(CorpActDeclarationID));
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
        //        return getResponse(new Exception(msg));
        //    }
        //}

        //[HttpPost("CorporateActionDividendApproval/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval)
        //{
        //    try
        //    {
        //        string userName = LoggedOnUser();
        //        var reponse = await _service.corpActRepository.GetCorpActDividendApproval(objCorpActDividendApproval, userName);
        //        return getResponse(reponse);
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        //[HttpGet("ILAMLCorporateActionClaimList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetCorpActionClaimList(int CorpActDeclarationID, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        return getResponse(await _service.corpActRepository.GetCorpActionClaimList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
        //        return getResponse(new Exception(msg));
        //    }
        //}

        //[HttpPost("ILAMLCorpActionClaim/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID)
        //{
        //    try
        //    {

        //        return getResponse(await _service.corpActRepository.ILAMLCorpActionClaim(objILAMLCorpActionClaim, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}


        //[HttpPost("CorpActDividendBonusClaimApproval/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        string Maker = LoggedOnUser();

        //        return getResponse(await _service.corpActRepository.InsertCADBonusClaimApproval(entryBonusApproval, CompanyID, BranchID, Maker));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        //[HttpPost("ILAMLUpdateStockDivClaimApprove/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        string Maker = LoggedOnUser();
        //        var reponse = await _service.corpActRepository.GetILAMLUpdateStockDivClaimApprove(objILAMLUpdateStockDivClaimApprove, CompanyID, BranchID, Maker);
        //        return getResponse(reponse);
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        //[HttpGet("ILAMLCorporateActionCashDividendClaimList/{CorpActDeclarationID}/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetCorpActionCashClaimList(int CorpActDeclarationID, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        return getResponse(await _service.corpActRepository.GetCorpActionCashClaimList(CompanyID, BranchID, CorpActDeclarationID, LoggedOnUser()));
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = $"Instrument not found with this id: {CorpActDeclarationID}";
        //        return getResponse(new Exception(msg));
        //    }
        //}

        //[HttpPost("ILAMLCorpActionCashClaim/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID)
        //{
        //    try
        //    {

        //        return getResponse(await _service.corpActRepository.ILAMLCorpActionCashClaim(objILAMLCorpActionCashClaim, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        //[HttpPost("ILAMLUpdateCashDivClaimApprove/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        string Maker = LoggedOnUser();
        //        var reponse = await _service.corpActRepository.GetILAMLUpdateCashDivClaimApprove(objILAMLUpdateCashDivClaimApprove, CompanyID, BranchID, Maker);
        //        return getResponse(reponse);
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        //[HttpPost("CorpActDividendBonusCashClaimApproval/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        string Maker = LoggedOnUser();

        //        return getResponse(await _service.corpActRepository.InsertCADBonusCashClaimApproval(entryBonusCashApproval, CompanyID, BranchID, Maker));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}
    }
}
