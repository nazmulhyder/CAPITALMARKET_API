using Model.DTOs.Instrument;
using Model.DTOs.Market;
using Model.DTOs.Withdrawal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IWithdrawalRepository
    {
        public Task<string> AddUpdateWithdrawal(int companyId,int branchID, string userName, SLWithdrawalEntryDto entityDto);
        public Task<object> GetWithdrawalBankInfo(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);
        public Task<SLGetWithdrawalInfo> GetWithdrawalDetailsSL(string UserName, int CompanyID, int BranchID, int DisburseID);
        public Task<SLGetWithdrawalInfo> GetWithdrawalDetailsIL(string UserName, int CompanyID, int BranchID, int DisburseID);
        public Task<SLGetWithdrawalInfo> GetWithdrawalDetailsAML(string UserName, int CompanyID, int BranchID, int DisburseID);

        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalSL(int CompanyID, int BranchID, string userName, FilterWithdrawalDto filter);
        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalIL(int CompanyID, int BranchID, string userName, FilterWithdrawalDto filter);
        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalAML(int CompanyID, int BranchID, string userName, FilterWithdrawalDto filter);

        //public Task<List<SLWithdrawalInfoListDto>> ListUnauthorizedWithdrawal(int PageNo, int Perpage, string SearchKeyword);
        public Task<string> WithdrawalApprovalSL(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto);
        public Task<string> WithdrawalApprovalIL(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto);
        public Task<string> WithdrawalApprovalAML(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto);

        public Task<string> AddUpdatePrepareWithdrawalSL(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto);
        public Task<string> AddUpdatePrepareWithdrawalIL(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto);
        public Task<string> AddUpdatePrepareWithdrawalAML(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto);


        public Task<string> AddChequeLeafSL(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto);
        public Task<string> AddChequeLeafIL(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto);
        public Task<string> AddChequeLeafAML(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto);


        public Task<List<ChequeLeavesListDto>> ChequeLeaveListSL(int CompanyID, int BranchID, int DisBankAccId);
        public Task<List<ChequeLeavesListDto>> ChequeLeaveListIL(int CompanyID, int BranchID, int DisBankAccId);
        public Task<List<ChequeLeavesListDto>> ChequeLeaveListAML(int CompanyID, int BranchID, int DisBankAccId);

        public Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintSL(int CompanyID, int BranchID, int DisBankAccID, int ProductID);
        public Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintIL(int CompanyID, int BranchID, int DisBankAccID, int ProductID);
        public Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintAML(int CompanyID, int BranchID, int DisBankAccID, int ProductID);

        public Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccSL(int CompanyID, int BranchID);
        public Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccIL(int CompanyID, int BranchID);
        public Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccAML(int CompanyID, int BranchID);

        public Task<string> UpdateInstrumentStatusSL(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID);
        public Task<string> UpdateInstrumentStatusIL(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID);
        public Task<string> UpdateInstrumentStatusAML(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID);


        public Task<object> ListGenOnlineTransferSL(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID);
        public Task<object> ListGenOnlineTransferIL(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID);
        public Task<object> ListGenOnlineTransferAML(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID);

        public Task<object> UpdateOnlineGeneratedInstrumentsSL(string userName, int companyId, int branchID, string monInsIDs);
        public Task<object> UpdateOnlineGeneratedInstrumentsIL(string userName, int companyId, int branchID, string monInsIDs);
        public Task<object> UpdateOnlineGeneratedInstrumentsAML(string userName, int companyId, int branchID, string monInsIDs);

        public Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentSL(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID);
        public Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentIL(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID);
        public Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentAML(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID);

        public Task<string> AddVoidPaymentInstrumentSL(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto);
        public Task<string> AddVoidPaymentInstrumentIL(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto);
        public Task<string> AddVoidPaymentInstrumentAML(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto);

        public Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalSL(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID);
        public Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalIL(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID);
        public Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalAML(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID);

        public Task<string> VoidInstrumentOrReversalApprovalSL(string userName, int CompanyID,  int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark);
        public Task<string> VoidInstrumentOrReversalApprovalIL(string userName, int CompanyID, int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark);

        public Task<string> VoidInstrumentOrReversalApprovalAML(string userName, int CompanyID, int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark);


        public Task<List<PostOnlineTransferSL>> GetPostOnlineTransferSL(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID);
        public Task<List<PostOnlineTransferSL>> GetPostOnlineTransferIL(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID);

        public Task<List<PostOnlineTransferSL>> GetPostOnlineTransferAML(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID);

        public Task<string> UpdatePostOnlineTransferSL(string userName,int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer);
        public Task<string> UpdatePostOnlineTransferIL(string userName, int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer);

        public Task<string> UpdatePostOnlineTransferAML(string userName, int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer);


        public Task<List<ChequeClearInfo>> GetChequeClearInfoSL(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo);
        public Task<List<ChequeClearInfo>> GetChequeClearInfoIL(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo);
        public Task<List<ChequeClearInfo>> GetChequeClearInfoAML(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo);


        public Task<string> UpdateChequeClearSL(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo);
        public Task<string> UpdateChequeClearIL(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo);

        public Task<string> UpdateChequeClearAML(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo);


        public Task<string> ReleaseChequeLeafSL(string userName, int CompanyID, int BranchID,int ProductID, int DisbBankAccountID ,long FromCheque, long ToCheque);
        public Task<string> ReleaseChequeLeafIL(string userName, int CompanyID, int BranchID, int ProductID, int DisbBankAccountID, long FromCheque, long ToCheque);

        public Task<string> ReleaseChequeLeafAML(string userName, int CompanyID, int BranchID, int ProductID, int DisbBankAccountID, long FromCheque, long ToCheque);

        public Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafSL(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID);
        public Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafIL(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID);

        public Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafAML(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID);


        public  Task<object> FilterListWithdrawalIL(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter);
        public  Task<object> FilterListWithdrawalSL(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter);
        public  Task<object> FilterListWithdrawalAML(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter);


        public  Task<string> BulkPrepareInstrumentSL(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry);
        public  Task<string> BulkPrepareInstrumentIL(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry);
        public  Task<string> BulkPrepareInstrumentAML(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry);


        public Task<object> GetRecentSurrenderAML(int CompanyID, int BranchID, string UserName, int ContractID);

        public Task<object> BulkInstrumentVoid(int CompanyID, int BranchID, string UserName, List<BulkInstrumentVoid> bulkInstrumentVoid);
        public Task<string> ApproveBulkInstrumentVoid(string userName, int CompanyID, int branchID, BulkInstrumentVoidApprovalDto approvalDto);


    }
}
