using System.ComponentModel.Design;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Model.DTOs.Allocation;
using Model.DTOs.IPO;
using Model.DTOs.NAVFileUpload;
using Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class IPOController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<IPOController> _logger;
        public IPOController(IService service, ILogger<IPOController> logger)
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

        [HttpPost("AddupdateIPOInstruments/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> IPOSettings(IPODTO EntryIPODTO, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.iPORepository.InsertIPO(EntryIPODTO, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet("IPOInstrumentList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllIPOInstrument(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetAllIPOInstrument(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOInstrumentListID/{IPOInstrumentID}")]
        public IActionResult GetIPOInstrumentById(int IPOInstrumentID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOInstrumentById(IPOInstrumentID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {IPOInstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("IPOInstrumentAccountInfo/{ProductId}/{AccountNumber}/{CompanyID}/{BranchID}")]
        public IActionResult GetIPOAccountInfo(int ProductId, string AccountNumber, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOAccountInfo(ProductId, AccountNumber, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Account Number Not Found: {AccountNumber}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("IPOInstrumentInstrumentInfo/{CompanyID}/{BranchID}")]
        public IActionResult GetIPOInstrumentInfo(IPOInvestorInfo? EntryIPOInvestorInfo, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOInstrumentInfo(EntryIPOInvestorInfo,CompanyID,BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"This Account Number is not eligible for this IPO";
                
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("AddUpdateIPOApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> IPOApplication(IPOApplication? entryIPOApplication, int CompanyID, int BranchID)
        {
            try
            {
                //return getResponse(await _service.iPORepository.InsertIPOApplication(IPOInvestorInstrumentInfo.EntryIPOInvestorInfo, entryIPOInvestorInstrumentInfo.EntryIPOInstrumentInfo, entryIPOInvestorInstrumentInfo.AppliedAmount.GetValueOrDefault(), LoggedOnUser()));

                return getResponse(await _service.iPORepository.InsertIPOApplication(entryIPOApplication, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOInstrumentListforApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> IPOInstrumentListforApproval(int CompanyID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.IPOInstrumentListforApproval(CompanyID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("IPOInstrumentApproval")]
        public async Task<IActionResult> GetIPOInstrumentApproval(IPOInstrumentApproval objIPOInstrumentApproval)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOInstrumentApproval(objIPOInstrumentApproval, userName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        //[HttpPost("ApproveIPOApplication")]

        //public async Task<IActionResult> ApproveIPOApplication(IPODTO? ApprovalInfo, string ApprovalRemark)
        //{
        //    try
        //    {
        //        //return getResponse(await _service.iPORepository.InsertIPOApplication(IPOInvestorInstrumentInfo.EntryIPOInvestorInfo, entryIPOInvestorInstrumentInfo.EntryIPOInstrumentInfo, entryIPOInvestorInstrumentInfo.AppliedAmount.GetValueOrDefault(), LoggedOnUser()));

        //        return getResponse(await _service.iPORepository.ApproveIPOApplication(ApprovalInfo, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}
        [HttpGet("IPOApplicationOrderList/{IPOInstrumentID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOApplicationOrderList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOApplicationOrderList(IPOInstrumentID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("ApprovedIPOApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> IPOApplicationApproved(IPOApproval IPOOrderApprovedList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.iPORepository.IPOApplicationApproved(IPOOrderApprovedList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("ApprovedIPOApplicationbyID/{IPOApplicationID}/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> IPOApplicationApprovedbyID(IPOApproval IPOOrderApprovedList, int IPOApplicationID, int CompanyID, int BranchID)
        {
            try
            {
                //return getResponse(await _service.iPORepository.InsertIPOApplication(IPOInvestorInstrumentInfo.EntryIPOInvestorInfo, entryIPOInvestorInstrumentInfo.EntryIPOInstrumentInfo, entryIPOInvestorInstrumentInfo.AppliedAmount.GetValueOrDefault(), LoggedOnUser()));

                return getResponse(await _service.iPORepository.IPOApplicationApprovedbyID(IPOOrderApprovedList, IPOApplicationID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet("IPOApplicationListbyID/{IPOApplicationID}/{CompanyID}/{BranchID}")]
        public IActionResult IPOApplicationListbyID(int IPOApplicationID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.IPOApplicationListbyID(IPOApplicationID,CompanyID,BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {IPOApplicationID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("IPOApplicationBulkList/{ProductID}/{IPOInstrumentID}/{CompanyID}/{BranchID}")] // IPO Controller Start
        public async Task<IActionResult> GetIPOApplicationBulkList(int ProductID, int IPOInstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOApplicationBulkList(ProductID, IPOInstrumentID, Maker, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddBulkIPOApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertIPOBulk(IPOBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.InsertIPOBulk(entryIPOBulk, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("IPOResultUpload/{IPOInstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertIPOResultFileText(IFormFile formdata, int IPOInstrumentID, int CompanyID, int BranchID )
        {
            try
            {
                return getResponse(await _service.iPORepository.InsertIPOResultFileText(formdata, IPOInstrumentID, CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet("IPOResultforAllocationRefundList/{IPOInstrumentID}/{CompanyID}/{BranchID}")] // IPO Controller Start
        public async Task<IActionResult> GetResultforAllocationRefundList(int IPOInstrumentID, int CompanyID,int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.iPORepository.GetResultforAllocationRefundList(IPOInstrumentID, Maker, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApproveIPOAllocationRefund/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertIPOResult(IPOResultMaster entryIPOResult, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.InsertIPOResult(entryIPOResult, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOReversalApprovalList/{CompanyID}/{BranchID}/{IPOType}")] // IPO Controller Start
        public async Task<IActionResult> GetIPOReversalList(int CompanyID, int BranchID, string IPOType)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOReversalList(CompanyID, BranchID, UserName, IPOType);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("IPOReversalApproval/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertReversalApproval(IPOReversalMaster entryIPOReversal, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.InsertReversalApproval(entryIPOReversal, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("IPOBookbuildingApplicationSubscription/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertApplicationSubscription(IPOBookBuildingAppSubscriptionIPO entryIPOAppSubscription, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.InsertApplicationSubscription(entryIPOAppSubscription, Maker,CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOBookBuildingAllotment/{IPOInstrumentID}/{ContractID}/{CompanyID}/{BranchID}")]
        public IActionResult GetIPOBookBuildingAllotmentInfo(int IPOInstrumentID, int ContractID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOBookBuildingAllotmentInfo(IPOInstrumentID, ContractID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Account Number Not Found: {IPOInstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("IPOBookBuildingAllocationAddUpdate/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertApplicationAllotMent(IPOBookBuildingAllotmentInsDTO entryIPOBookBuildingAllotment, int CompanyID, int BranchID)
        {
            try
            {   
                string Maker = LoggedOnUser();
               

                return getResponse(await _service.iPORepository.InsertApplicationAllotMent(entryIPOBookBuildingAllotment, Maker, CompanyID,BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOSubsCriptionforApprovalList/{IPOInstrumentID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOSubsCriptionforApprovalList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOSubsCriptionforApprovalList(IPOInstrumentID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOSubscriptionListbyID/{SubscriptionID}/{CompanyID}/{BranchID}")]
        public IActionResult GetIPOSubscriptionListbyID(int SubscriptionID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOSubscriptionListbyID(SubscriptionID,CompanyID,BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {SubscriptionID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ApproveBookBuildingAllocationRefund/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> ApproveBookBuildingRefundMaster(BookBuildingRefundMaster entryBookBuildingRefundMaster, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.ApproveBookBuildingRefundMaster(entryBookBuildingRefundMaster, Maker, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AMLIPOCollectionList/{IPOInstrumentID}/{IPOType}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAMLIPOCollectionList(int IPOInstrumentID,string IPOType, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetAMLIPOCollectionList(IPOInstrumentID, IPOType, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLCollectionApproveRequest/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> AMLCollectionApproveRequest(AMLCollectionMaster AMLCollectionReq, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.iPORepository.AMLCollectionApproveRequest(AMLCollectionReq, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AMLIPOCollectionApprovalList/{IPOInsturmentID}/{IPOType}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstrumentCollectionlList(int IPOInsturmentID,string IPOType, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetInstrumentCollectionlList(IPOInsturmentID, IPOType, CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApproveAMLIPOInstrumentCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> AMLIPOInstrumentApproved(AMLCollectionApproved AMLIPOInstrumentList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.iPORepository.AMLIPOInstrumentApproved(AMLIPOInstrumentList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOFileGenerateToIssuer/{IPOInstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> IPOFileGenerate(int IPOInstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                return  getResponse(await _service.iPORepository.IPOFileGenerate(IPOInstrumentID, CompanyID, BranchID, UserName));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {IPOInstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        //[HttpGet("IPOInstrumentSetupInfo/{IPOInstrumentID}/{IPOType}/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> GetIPOInstrumentSetupInfo(int IPOInstrumentID,string IPOType, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        string userName = LoggedOnUser();
        //        var reponse = await _service.iPORepository.GetIPOInstrumentSetupInfo(IPOInstrumentID, IPOType, CompanyID, BranchID);
        //        return getResponse(reponse);
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

        [HttpGet("IPOInstrumentSetupInfo/{IPOInstrumentID}/{IPOType}/{CompanyID}/{BranchID}")]
        public IActionResult GetIPOInstrumentSetupInfo(int IPOInstrumentID, string IPOType, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.iPORepository.GetIPOInstrumentSetupInfo(IPOInstrumentID, IPOType, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOApplicationWaitingSubmissionList/{IPOInstrumentID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOApplicationWaitingSubmissionList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOApplicationWaitingSubmissionList(IPOInstrumentID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOSubmittedApplicationList/{IPOInstrumentID}/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOSubmittedApplicationList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOSubmittedApplicationList(IPOInstrumentID, Status, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetIPOApplicationFileValidation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOApplicationFileValidation(int CompanyID, int BranchID, List<IPOApplicationFileDetailDto>? data)
        {
            try
            {
                return getResponse(await _service.iPORepository.GetIPOApplicationFileValidation(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("SaveIPOApplicationFile/{InstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveIPOApplicationFile(int CompanyID, int BranchID,int InstrumentID ,IFormCollection data)
        {
            try
            {
                return getResponse(await _service.iPORepository.SaveIPOApplicationFile(LoggedOnUser(), CompanyID, BranchID,InstrumentID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("IPOEligibleInvestorList/{IPOInsSettingID}/{EnableDisable}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetIPOEligibleInvestorList(int IPOInsSettingID, string EnableDisable,int CompanyID, int BranchID)
        {
            try
            {
                string UserName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetIPOEligibleInvestorList(IPOInsSettingID, EnableDisable, CompanyID, BranchID, UserName);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddEligibleBulkIPOApplication/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertEligibleIPOBulk(IPOEligibleBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.InsertEligibleIPOBulk(entryIPOBulk, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AMLIPOSubscriptionList/{IPOInstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAMLIPOSubsCriptionList(int IPOInstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetAMLIPOSubsCriptionList(IPOInstrumentID, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApproveIPOAMLCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> AMLIPOSubscriptionCollectionApproved(AMLCollectionApprovedDTO AMLIPOInstrumentList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.iPORepository.AMLIPOSubscriptionCollectionApproved(AMLIPOInstrumentList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMListFundTransferInstrument/{TransactionType}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMListTransferfundamountbyIPORights(string TransactionType, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetCMListTransferfundamountbyIPORights(TransactionType, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AMLIPOApplicationFundGLBalanceInfo/{MFBankAccountID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAMLBankAccountInformationByID(int MFBankAccountID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetAMLBankAccountInformationByID(MFBankAccountID, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("FundTransferToIssuerList/{TransactionType}/{InstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetFundTransferToIssuerList(string TransactionType,int InstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.iPORepository.GetFundTransferToIssuerList(TransactionType, InstrumentID, CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SaveFundTransferToIssuer/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> SLILSaveFundTransferToIssuer(SLILFundTransferToIssuerDTO SLILFundTransfer, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.iPORepository.SLILSaveFundTransferToIssuer(SLILFundTransfer, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ILApproveBookBuildingRefund/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> ApproveBookBuildingRefund(BookBuildingRefundMaster entryBookBuildingRefundMaster, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.iPORepository.ApproveBookBuildingRefund(entryBookBuildingRefundMaster, Maker, CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
