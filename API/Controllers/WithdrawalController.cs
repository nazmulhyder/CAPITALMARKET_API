using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.TradeRestriction;
using Model.DTOs.Withdrawal;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<WithdrawalController> _logger;

        public WithdrawalController(IService service, ILogger<WithdrawalController> logger)
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


        [HttpPost("AddUpdateWithdrawal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdateTradeWithdrawal(int CompanyID, int BranchID,SLWithdrawalEntryDto? WithdrawalEntry)
        {
            try
            {
                return getResponse(await _service.WithdrawalRepository.AddUpdateWithdrawal(CompanyID, BranchID, LoggedOnUser(), WithdrawalEntry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetWithdrawalBankInfo/{CompanyID}/{BranchID}/{ProductID}/{AccountNumber}")]
        public async Task<IActionResult> GetByAccountNo(int CompanyID,int BranchID, int ProductID, string AccountNumber)
        {
            try
            {
                return getResponse(await _service.WithdrawalRepository.GetWithdrawalBankInfo(LoggedOnUser(),CompanyID,BranchID,ProductID,AccountNumber));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("ListWithdrawal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListWithdrawal(int CompanyID, int BranchID, FilterWithdrawalDto filter)
        {
            try
            {
                string userName = LoggedOnUser();
                if(CompanyID == 4)
                {
                    return getResponse(await _service.WithdrawalRepository.ListWithdrawalSL(CompanyID, BranchID, LoggedOnUser(), filter));
                }
                else if (CompanyID == 2)
                {
                    return getResponse(await _service.WithdrawalRepository.ListWithdrawalAML(CompanyID, BranchID, LoggedOnUser(), filter));
                }
                else
                {
                    return getResponse(await _service.WithdrawalRepository.ListWithdrawalIL(CompanyID, BranchID, LoggedOnUser(), filter));
                }
            }
               
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetWithdrawalDetails/{CompanyID}/{BranchID}/{DisburseID}")]
        public async Task<IActionResult> GetWithdrawalDetails(int CompanyID, int BranchID, int DisburseID)
        {
            try
            {
                if(CompanyID==4)
                return getResponse(await _service.WithdrawalRepository.GetWithdrawalDetailsSL(LoggedOnUser(), CompanyID, BranchID, DisburseID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.GetWithdrawalDetailsAML(LoggedOnUser(), CompanyID, BranchID, DisburseID));
                else
                    return getResponse(await _service.WithdrawalRepository.GetWithdrawalDetailsIL(LoggedOnUser(), CompanyID, BranchID, DisburseID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("WithdrawalApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> WithdrawalApproval(int CompanyID, int BranchID, WithdrawalApprovalDto approvalRequest)
        {
            try
            {
                var result = "";

                if (CompanyID == 4)
                    result = await _service.WithdrawalRepository.WithdrawalApprovalSL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (CompanyID == 2)
                    result = await _service.WithdrawalRepository.WithdrawalApprovalAML(LoggedOnUser(), CompanyID, BranchID, approvalRequest);
                if (CompanyID == 3)
                    result = await _service.WithdrawalRepository.WithdrawalApprovalIL(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddUpdatePrepareWithdrawal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddUpdatePrepareWithdrawal(int CompanyID, int BranchID, SLPrepareWithdrawal prepareWithdrawal)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.AddUpdatePrepareWithdrawalSL(CompanyID, BranchID, LoggedOnUser(), prepareWithdrawal));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.AddUpdatePrepareWithdrawalAML(CompanyID, BranchID, LoggedOnUser(), prepareWithdrawal));
                else 
                    return getResponse(await _service.WithdrawalRepository.AddUpdatePrepareWithdrawalIL(CompanyID, BranchID, LoggedOnUser(), prepareWithdrawal));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddChequeLeaf/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddChequeLeaf(int CompanyID, int BranchID, SLLicenseeAccChequeBookDto entryDto)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.AddChequeLeafSL(CompanyID, BranchID, LoggedOnUser(), entryDto));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.AddChequeLeafAML(CompanyID, BranchID, LoggedOnUser(), entryDto));
                else 
                    return getResponse(await _service.WithdrawalRepository.AddChequeLeafIL(CompanyID, BranchID, LoggedOnUser(), entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListChequeLeaves/{CompanyID}/{BranchID}/{DisBankAccID}")]
        public async Task<IActionResult> ListChequeLeaves(int CompanyID, int BranchID,int DisBankAccID)
        {
            try
            {
                string userName = LoggedOnUser();

                if (CompanyID == 4)
                     return getResponse(await _service.WithdrawalRepository.ChequeLeaveListSL( CompanyID, BranchID, DisBankAccID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ChequeLeaveListAML(CompanyID, BranchID, DisBankAccID));
               else
                    return getResponse(await _service.WithdrawalRepository.ChequeLeaveListIL( CompanyID, BranchID,DisBankAccID));

               
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("ListInstrumentValidateAndPrint/{CompanyID}/{BranchID}/{DisBankAccID}/{ProductID}")]
        public async Task<IActionResult> ListInstrumentValidateAndPrint(int CompanyID, int BranchID, int DisBankAccID, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                   return getResponse(await _service.WithdrawalRepository.ListValidateAndPrintSL(CompanyID,BranchID, DisBankAccID, ProductID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ListValidateAndPrintAML(CompanyID, BranchID, DisBankAccID, ProductID));
                else
                    return getResponse(await _service.WithdrawalRepository.ListValidateAndPrintIL(CompanyID, BranchID, DisBankAccID, ProductID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("UpdateInstrumentStatus/{CompanyID}/{BranchID}/{DisbursementID}/{MonInstrumentID}")]
        public async Task<IActionResult> UpdateInstrumentStatus(int CompanyID, int BranchID, int DisbursementID, int MonInstrumentID)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.UpdateInstrumentStatusSL(CompanyID, BranchID, LoggedOnUser(), DisbursementID, MonInstrumentID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.UpdateInstrumentStatusAML(CompanyID, BranchID, LoggedOnUser(), DisbursementID, MonInstrumentID));
                else 
                    return getResponse(await _service.WithdrawalRepository.UpdateInstrumentStatusIL(CompanyID, BranchID, LoggedOnUser(), DisbursementID, MonInstrumentID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListDisbursementBankAcc/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListDisbursementBankAcc(int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.ListDisbursementBankAccSL(CompanyID, BranchID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ListDisbursementBankAccAML(CompanyID, BranchID));
               else
                    return getResponse(await _service.WithdrawalRepository.ListDisbursementBankAccIL(CompanyID, BranchID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListGenOnlineTransfer/{CompanyID}/{BranchID}/{DisBankAccID}/{PaymentMode}/{ProductID}")]
        public async Task<IActionResult> ListGenOnlineTransfer(int CompanyID, int BranchID, int DisBankAccID, string PaymentMode, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.ListGenOnlineTransferSL(CompanyID,BranchID, DisBankAccID, PaymentMode, ProductID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ListGenOnlineTransferAML(CompanyID, BranchID, DisBankAccID, PaymentMode, ProductID));

                else 
                    return getResponse(await _service.WithdrawalRepository.ListGenOnlineTransferIL(CompanyID, BranchID, DisBankAccID, PaymentMode, ProductID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UpdateOnlineGeneratedInstruments/{CompanyID}/{BranchID}/{monInsIDs}")]
        public async Task<IActionResult> UpdateOnlineGeneratedInstruments(int CompanyID, int BranchID, string monInsIDs)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.UpdateOnlineGeneratedInstrumentsSL(LoggedOnUser(), CompanyID,BranchID, monInsIDs));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.UpdateOnlineGeneratedInstrumentsAML(LoggedOnUser(), CompanyID, BranchID, monInsIDs));
               else
                    return getResponse(await _service.WithdrawalRepository.UpdateOnlineGeneratedInstrumentsIL(LoggedOnUser(), CompanyID, BranchID, monInsIDs));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetVoidPaymentInstrument/{CompanyID}/{BranchID}/{RetrievalType}/{InstrumentOrAccountNo}/{ProductID}")]
        public async Task<IActionResult> GetVoidPaymentInstrument(int CompanyID, int BranchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID=0)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.GetVoidPaymentInstrumentSL(CompanyID, BranchID,RetrievalType, InstrumentOrAccountNo, ProductID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.GetVoidPaymentInstrumentAML(CompanyID, BranchID, RetrievalType, InstrumentOrAccountNo, ProductID));
               else
                    return getResponse(await _service.WithdrawalRepository.GetVoidPaymentInstrumentIL(CompanyID, BranchID, RetrievalType, InstrumentOrAccountNo, ProductID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AddVoidPaymentInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AddVoidPaymentInstrument(int CompanyID, int BranchID, EntryVoidPaymentInstrumentSL entryDto)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.AddVoidPaymentInstrumentSL(LoggedOnUser(), CompanyID, BranchID, entryDto));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.AddVoidPaymentInstrumentAML(LoggedOnUser(), CompanyID, BranchID, entryDto));
               else
                    return getResponse(await _service.WithdrawalRepository.AddVoidPaymentInstrumentIL(LoggedOnUser(), CompanyID, BranchID, entryDto));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListVoidPaymentInstrumentApproval/{CompanyID}/{BranchID}/{PageNo}/{PerPage}/{SearchKeyword}/{ListType}/{ProductID}")]
        public async Task<IActionResult> ListVoidPaymentInstrumentApproval(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword, string ListType, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.ListVoidPaymentInstrumentApprovalSL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType, ProductID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ListVoidPaymentInstrumentApprovalAML(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType, ProductID));
                else
                    return getResponse(await _service.WithdrawalRepository.ListVoidPaymentInstrumentApprovalIL(CompanyID, BranchID, PageNo, PerPage, SearchKeyword, ListType, ProductID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("VoidInstrumentOrReversalApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> VoidInstrumentOrReversalApproval(int CompanyID, int BranchID, InstrumentApprovalDto instrumentApproval)
        {
            try
            {
                var result = "";
                if (CompanyID == 4)
                    result = await _service.WithdrawalRepository.VoidInstrumentOrReversalApprovalSL(LoggedOnUser(), CompanyID, BranchID, instrumentApproval.VoidOrWithdrawalInstrumentID, instrumentApproval.ApprovalType, instrumentApproval.IsApproved, instrumentApproval.ApprovalRemark);
                if (CompanyID == 2)
                    result = await _service.WithdrawalRepository.VoidInstrumentOrReversalApprovalAML(LoggedOnUser(), CompanyID, BranchID, instrumentApproval.VoidOrWithdrawalInstrumentID, instrumentApproval.ApprovalType, instrumentApproval.IsApproved, instrumentApproval.ApprovalRemark);
                if (CompanyID == 3)
                    result = await _service.WithdrawalRepository.VoidInstrumentOrReversalApprovalIL(LoggedOnUser(), CompanyID, BranchID, instrumentApproval.VoidOrWithdrawalInstrumentID, instrumentApproval.ApprovalType, instrumentApproval.IsApproved, instrumentApproval.ApprovalRemark);
                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ReleaseChequeLeaf/{CompanyID}/{BranchID}/{ProductID}")]
        public async Task<IActionResult> ReleaseChequeLeaf(int CompanyID, int BranchID, EntryReleaseCheque? entryRelease)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.ReleaseChequeLeafSL(LoggedOnUser(), CompanyID, BranchID, entryRelease.ProductID, entryRelease.DisbBankAccountID, entryRelease.FromCheque, entryRelease.ToCheque));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ReleaseChequeLeafAML(LoggedOnUser(), CompanyID, BranchID, entryRelease.ProductID, entryRelease.DisbBankAccountID, entryRelease.FromCheque, entryRelease.ToCheque));
               else
                    return getResponse(await _service.WithdrawalRepository.ReleaseChequeLeafIL(LoggedOnUser(), CompanyID, BranchID, entryRelease.ProductID, entryRelease.DisbBankAccountID, entryRelease.FromCheque, entryRelease.ToCheque));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetPostOnlineTransfer/{CompanyID}/{BranchID}/{DisbursementBankAccID}/{PaymentMode}/{ProductID}")]
        public async Task<IActionResult> GetPostOnlineTransfer(int CompanyID, int BranchID, int DisbursementBankAccID, string PaymentMode, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.GetPostOnlineTransferSL(LoggedOnUser(), CompanyID, BranchID,  DisbursementBankAccID, PaymentMode, ProductID));
               else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.GetPostOnlineTransferAML(LoggedOnUser(), CompanyID, BranchID, DisbursementBankAccID, PaymentMode, ProductID));
               else
                    return getResponse(await _service.WithdrawalRepository.GetPostOnlineTransferIL(LoggedOnUser(), CompanyID, BranchID, DisbursementBankAccID, PaymentMode, ProductID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ReturnOrClearOnlineTransfer/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ReturnOnlineTransfer(int CompanyID, int BranchID, EntryOnlineTransfer? entryOnlineTransfer)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.UpdatePostOnlineTransferSL(LoggedOnUser(), CompanyID, BranchID,  entryOnlineTransfer));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.UpdatePostOnlineTransferAML(LoggedOnUser(), CompanyID, BranchID, entryOnlineTransfer));
               else
                    return getResponse(await _service.WithdrawalRepository.UpdatePostOnlineTransferIL(LoggedOnUser(), CompanyID, BranchID, entryOnlineTransfer));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("GetChequeClearInfo/{CompanyID}/{BranchID}/{RetrievalType}/{InstrumentOrDisbAccountNo}")]
        public async Task<IActionResult> GetChequeClearInfo(int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.GetChequeClearInfoSL(LoggedOnUser(),CompanyID, BranchID,RetrievalType, InstrumentOrDisbAccountNo));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.GetChequeClearInfoAML(LoggedOnUser(), CompanyID, BranchID, RetrievalType, InstrumentOrDisbAccountNo));
               else
                    return getResponse(await _service.WithdrawalRepository.GetChequeClearInfoIL(LoggedOnUser(), CompanyID, BranchID, RetrievalType, InstrumentOrDisbAccountNo));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ClearCheque/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ClearCheque(int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo)
        {
            try
            {
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.UpdateChequeClearSL(LoggedOnUser(), CompanyID, BranchID, chequeClearInfo));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.UpdateChequeClearAML(LoggedOnUser(), CompanyID, BranchID, chequeClearInfo));
               else
                    return getResponse(await _service.WithdrawalRepository.UpdateChequeClearIL(LoggedOnUser(), CompanyID, BranchID, chequeClearInfo));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListReleaseChequeLeaf/{CompanyID}/{BranchID}/{ProductID}/{BankAccountID}")]
        public async Task<IActionResult> ListReleaseChequeLeaf(int CompanyID, int BranchID, int ProductID, string BankAccountID)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.ListReleaseChequeLeafSL(LoggedOnUser(),CompanyID, BranchID, ProductID, BankAccountID));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.ListReleaseChequeLeafAML(LoggedOnUser(), CompanyID, BranchID, ProductID, BankAccountID));

                else
                    return getResponse(await _service.WithdrawalRepository.ListReleaseChequeLeafIL(LoggedOnUser(), CompanyID, BranchID, ProductID, BankAccountID));

            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Filter/List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> FilterListWithdrawal(int CompanyID, int BranchID,  WithdrawalSearchFilterDto filter)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.FilterListWithdrawalSL(CompanyID,  BranchID, LoggedOnUser(), filter));
                else if (CompanyID == 2)
                    return getResponse(await _service.WithdrawalRepository.FilterListWithdrawalAML(CompanyID, BranchID, LoggedOnUser(), filter));

                else
                    return getResponse(await _service.WithdrawalRepository.FilterListWithdrawalIL(CompanyID, BranchID, LoggedOnUser(), filter));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("BulkPrepareInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BulkPrepareInstrument(int CompanyID, int BranchID, BulkPrepareInstrumentDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                if (CompanyID == 4)
                    return getResponse(await _service.WithdrawalRepository.BulkPrepareInstrumentSL(LoggedOnUser(), CompanyID, BranchID, entry));
                else if (CompanyID == 3)
                    return getResponse(await _service.WithdrawalRepository.BulkPrepareInstrumentIL(LoggedOnUser(), CompanyID, BranchID, entry));

                else
                    return getResponse(await _service.WithdrawalRepository.BulkPrepareInstrumentAML(LoggedOnUser(), CompanyID, BranchID, entry));

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/GetRecentSurrender/{CompanyID}/{BranchID}/{ContractID}")]
        public async Task<IActionResult> GetRecentSurrenderAML(int CompanyID, int BranchID, int ContractID)
        {
            try
            {
                return getResponse(await _service.WithdrawalRepository.GetRecentSurrenderAML(CompanyID, BranchID, LoggedOnUser(), ContractID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("List/BulkInstrumentVoid/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BulkInstrumentVoid(int CompanyID, int BranchID, List<BulkInstrumentVoid> bulkInstrumentVoid)
        {
            try
            {
                return getResponse(await _service.WithdrawalRepository.BulkInstrumentVoid(CompanyID, BranchID, LoggedOnUser(), bulkInstrumentVoid));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Approve/ApproveBulkInstrumentVoid/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ApproveBulkInstrumentVoid(int CompanyID, int BranchID, BulkInstrumentVoidApprovalDto approvalDto)
        {
            try
            {
                return getResponse(await _service.WithdrawalRepository.ApproveBulkInstrumentVoid(LoggedOnUser() ,CompanyID, BranchID, approvalDto));

            }

            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
