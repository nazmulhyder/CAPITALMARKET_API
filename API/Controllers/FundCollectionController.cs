using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.FundCollection;
using Service.Interface;
using System.ComponentModel.Design;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FundCollectionController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<FundCollectionController> _logger;
        public FundCollectionController(IService service, ILogger<FundCollectionController> logger)
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

		[HttpGet("GetFundCollectionModeList/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetFundCollectionModeList(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.fundCollectionRepository.GetFundCollectionModeList(LoggedOnUser(), CompanyID, BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

        
		[HttpGet("GetChequeDishonorReasonList/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetChequeDishonorReasonList(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.fundCollectionRepository.GetChequeDishonorReasonList(LoggedOnUser(), CompanyID, BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("GetProductList/{CompanyID}")]
        public async Task<IActionResult> GetProductList(int CompanyID)
        {
            try
            {
                return getResponse(await _service.fundCollectionRepository.GetProductList(LoggedOnUser(), CompanyID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetClientInfo/{CompanyID}/{ProductID}/{AccountNumber}")]
        public async Task<IActionResult> GetClientInfo(int CompanyID, int ProductID, string AccountNumber)
        {
            try
            {
                return getResponse(await _service.fundCollectionRepository.GetAgreementInfo(CompanyID, ProductID, AccountNumber));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdateFundCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateFundCollection(FundCollectionDto data, int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.AddUpdateFundCollectionAML(data, LoggedOnUser(), CompanyID, BranchID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.AddUpdateFundCollectionIL(data, LoggedOnUser(), CompanyID, BranchID));
                else
                    return getResponse(await _service.fundCollectionRepository.AddUpdateFundCollection(data, LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetFundCollectionList")]
        public async Task<IActionResult> GetFundCollectionList(FundCollectionListDto filterData)
        {
            try
            {
                if(filterData.CompanyID== 2)
                return getResponse(await _service.fundCollectionRepository.GetFundCollectionListAML(LoggedOnUser(), filterData));
                else if(filterData.CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.GetFundCollectionListIL(LoggedOnUser(), filterData));
                else
                    return getResponse(await _service.fundCollectionRepository.GetFundCollectionList(LoggedOnUser(), filterData));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("ApproveFundCollection/{CompanyID}")]
        public async Task<IActionResult> ApproveFundCollection(FundCollectionApprovalDto approveData, int CompanyID )
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.ApproveFundCollectionAML(LoggedOnUser(), approveData, CompanyID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.ApproveFundCollectionIL(LoggedOnUser(), approveData, CompanyID));
                else
                    return getResponse(await _service.fundCollectionRepository.ApproveFundCollection(LoggedOnUser(), approveData, CompanyID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetBankAccountList/{CompanyID}/{ProductID}/{FundID}/{SetupLevel}")]
        public async Task<IActionResult> GetBankAccountList(int CompanyID, int ProductID,int FundID, string SetupLevel)
        {
            try
            {
                return getResponse(await _service.fundCollectionRepository.GetBankAccountList(CompanyID, ProductID,FundID, SetupLevel));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetFundCollectionDetail/{CollectionInfoID}/{CompanyID}")]
        public async Task<IActionResult> GetFundCollectionDetail(int CollectionInfoID, int CompanyID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.GetFundCollectionDetailAML(CollectionInfoID, CompanyID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.GetFundCollectionDetailIL(CollectionInfoID, CompanyID));
                else
                    return getResponse(await _service.fundCollectionRepository.GetFundCollectionDetail(CollectionInfoID, CompanyID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("BankStatementUpload")]
        public async Task<IActionResult> BankStatementUpload(IFormCollection data)
        {
            try
            {
                if (Convert.ToInt32(data["CompanyId"]) == 2)
                    return getResponse(await _service.fundCollectionRepository.BankStatementUploadAML(data, LoggedOnUser()));
                else if (Convert.ToInt32(data["CompanyId"]) == 3)
                    return getResponse(await _service.fundCollectionRepository.BankStatementUploadIL(data, LoggedOnUser()));
                else
                    return getResponse(await _service.fundCollectionRepository.BankStatementUpload(data, LoggedOnUser()));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("UploadedStatementList/{CompanyID}/{BranchID}/{TransactionDate}")]
        public async Task<IActionResult> UploadedStatement(int CompanyID, int BranchID, string TransactionDate)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.UploadedStatementAML(TransactionDate, CompanyID, BranchID, LoggedOnUser()));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.UploadedStatementIL(TransactionDate, CompanyID, BranchID, LoggedOnUser()));
                else
                    return getResponse(await _service.fundCollectionRepository.UploadedStatement(TransactionDate, CompanyID, BranchID, LoggedOnUser()));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("InstallmentCollectionListForScheduleTagging/{CompanyID}/{ListType}/{AccountNumber}")]
        public async Task<IActionResult> InstallmentCollectionListForScheduleTagging(int CompanyID, string ListType, string AccountNumber)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.InstallmentCollectionListForScheduleTaggingAML(LoggedOnUser(), CompanyID, ListType, AccountNumber));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.InstallmentCollectionListForScheduleTaggingIL(LoggedOnUser(), CompanyID, ListType, AccountNumber));
                else
                    return getResponse(await _service.fundCollectionRepository.InstallmentCollectionListForScheduleTagging(LoggedOnUser(), CompanyID, ListType, AccountNumber));

            }
            catch (Exception ex) { return getResponse(ex); }
        }
		

		[HttpPost("SaveInstallmentCollectionTag/{InstallmentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveInstallmentCollectionTag(List<InstallmentScheduleDto> data, int InstallmentID, int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.SaveScheduleInstallmentTagAML(LoggedOnUser(), data, InstallmentID, CompanyID, BranchID));
                else
                    return getResponse(await _service.fundCollectionRepository.SaveScheduleInstallmentTagIL(LoggedOnUser(), data, InstallmentID, CompanyID, BranchID));
              

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetScheduleInstallmentTagList/{CompanyID}/{ListType}")]
        public async Task<IActionResult> GetScheduleInstallmentTagList(int CompanyID,  string ListType)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentTagListAML(LoggedOnUser(), CompanyID, ListType));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentTagListIL(LoggedOnUser(), CompanyID, ListType));
                else
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentTagList(LoggedOnUser(), CompanyID, ListType));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApproveScheduleInstallmentTagList/{CompanyID}")]
        public async Task<IActionResult> ApproveScheduleInstallmentTagList(ScheduleInstallmentTagApprovalDto data, int CompanyID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.ApproveScheduleInstallmentTagListAML(LoggedOnUser(), data, CompanyID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.ApproveScheduleInstallmentTagListIL(LoggedOnUser(), data, CompanyID));
                else
                    return getResponse(await _service.fundCollectionRepository.ApproveScheduleInstallmentTagList(LoggedOnUser(), data, CompanyID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }



        [HttpGet("GetScheduleInstallmentListForDDIFile/{CompanyID}/{BranchID}/{FundIDorProductID}/{ListType}/{ScheduleDueDateFrom}/{ScheduleDueDateTo}")]
        public async Task<IActionResult> GetScheduleInstallmentListForDDIFile(int CompanyID, int BranchID, int FundIDorProductID, string ListType, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentListForDDIFileAML(LoggedOnUser(), CompanyID, BranchID, FundIDorProductID, ListType, ScheduleDueDateFrom, ScheduleDueDateTo));
                else
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentListForDDIFileIL(LoggedOnUser(), CompanyID, BranchID, FundIDorProductID, ListType, ScheduleDueDateFrom, ScheduleDueDateTo));
             
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GenerateDDIFile/{BanckAccountID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GenerateDDIFile(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.GenerateDDIFileAML(data, BanckAccountID, CompanyID, BranchID, LoggedOnUser()));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.GenerateDDIFileIL(data, BanckAccountID, CompanyID, BranchID, LoggedOnUser()));
                else
                    return getResponse(await _service.fundCollectionRepository.GenerateDDIFile(data, BanckAccountID, CompanyID, BranchID, LoggedOnUser()));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("DDIFileUpload")]
        public async Task<IActionResult> DDIFileUpload(IFormCollection data)
        {
            try
            {
                
                return getResponse(await _service.fundCollectionRepository.DDIFileUpload(data, LoggedOnUser()));
               
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetScheduleInstallmentListFor2ndDDIFile/{CompanyID}/{BranchID}/{ProductID}/{ScheduleDueDateFrom}/{ScheduleDueDateTo}")]
        public async Task<IActionResult> GetScheduleInstallmentListFor2ndDDIFile(int CompanyID, int BranchID, int ProductID, string ScheduleDueDateFrom, string ScheduleDueDateTo)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentListFor2ndDDIFileAML(LoggedOnUser(), CompanyID, BranchID, ProductID,ScheduleDueDateFrom, ScheduleDueDateTo));
                else 
                    return getResponse(await _service.fundCollectionRepository.GetScheduleInstallmentListFor2ndDDIFileIL(LoggedOnUser(), CompanyID, BranchID, ProductID, ScheduleDueDateFrom, ScheduleDueDateTo));
               
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("UnlockFor2ndDDIFile/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UnlockFor2ndDDIFile(List<ScheduleListForDDIFileDto> data, int CompanyID, int BranchID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.UnlockFor2ndDDIFileAML(data, LoggedOnUser(), CompanyID, BranchID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.UnlockFor2ndDDIFileIL(data, LoggedOnUser(), CompanyID, BranchID));
                else
                    return getResponse(await _service.fundCollectionRepository.UnlockFor2ndDDIFile(data, LoggedOnUser(), CompanyID, BranchID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("DDIFileList/{CompanyID}/{BranchID}/{Status}/{FromDate}/{ToDate}/{ProductID}")]
        public async Task<IActionResult> DDIFileList(int CompanyID, int BranchID, string Status, string FromDate, string ToDate,int ProductID)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.DDIFileListAML(LoggedOnUser(), CompanyID, BranchID, Status, FromDate,ToDate, ProductID));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.DDIFileListIL(LoggedOnUser(), CompanyID, BranchID, Status, FromDate, ToDate, ProductID));
                else
                    return getResponse(await _service.fundCollectionRepository.DDIFileList(LoggedOnUser(), CompanyID, BranchID, Status));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("DDIFileApprove/{CompanyID}/{BranchID}/{DDIFileID}/{ApproveStatus}")]
        public async Task<IActionResult> DDIFileApprove(int CompanyID, int BranchID,int DDIFileID, string ApproveStatus)
        {
            try
            {
                if (CompanyID == 2)
                    return getResponse(await _service.fundCollectionRepository.DDIFileApproveAML(LoggedOnUser(), CompanyID, BranchID, DDIFileID, ApproveStatus));
                else if (CompanyID == 3)
                    return getResponse(await _service.fundCollectionRepository.DDIFileApproveIL(LoggedOnUser(), CompanyID, BranchID, DDIFileID, ApproveStatus));
                else
                    return getResponse(await _service.fundCollectionRepository.DDIFileApprove(LoggedOnUser(), CompanyID, BranchID, DDIFileID, ApproveStatus));


            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
