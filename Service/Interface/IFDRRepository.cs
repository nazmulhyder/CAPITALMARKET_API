using Model.DTOs.Approval;
using Model.DTOs.FDR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFDRRepository
    {
        // Deposit Opening detail
        public Task<BalanceInfoDto> FDR_GetClientAbailableBalanceIL_AML(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, int FundID);
        public Task<string> InsertUpdateDepositAccountOpening(int CompanyID, int BranchID, string userName, DepositOpeningDto entry);
        public Task<List<DepositOpeningDto>> ListDepositBankAccountOpening(int CompanyID, int BranchID, string FromDate, string ToDate, string SearchKeyword, string ListType, int FundID);
        public Task<string> DepositBankAccountOpeningApprovalIL(string userName, int CompanyID, int branchID, DepositAccountOpeningApprovalDto approvalDto);
        public Task<string> DepositBankAccountOpeningApprovalAML(string userName, int CompanyID, int branchID, DepositAccountOpeningApprovalDto approvalDto);

        //Deposit Interest Collection
        public Task<List<InterestCollectionInfoDto>> InterestCollectionInfo(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, int FundID);
        public Task<string> InsertUpdateDepositInterestCollection(int CompanyID, int BranchID, string userName, InterestCollectionDto entry);
        public Task<List<ListInterestCollectionDto>> ListDepositInterestCollection(int CompanyID, int BranchID, string DateFrom, string DateTo, string SearchKeyword, string ListType, int FundID);
        public Task<string> DepositIntCollectionApprovalIL(string userName, int CompanyID, int branchID, DepositIntCollectionApprovalDto approvalDto);
        public Task<string> DepositIntCollectionApprovalAML(string userName, int CompanyID, int branchID, DepositIntCollectionApprovalDto approvalDto);


        //reversal
        public Task<List<DepositIntCollectionReversalInfoDto>> GetInterestCollectionInfoForReversal(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, string DepositAccNo, int FundID);
        public Task<string> InsertUpdateDepositInterestCollectionReversal(int CompanyID, int BranchID, string userName, DepositInterestCollectionReversalDto entry);
        public Task<List<ListInterestCollectionReversalDto>> ListDepositInterestCollectionReversalIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<string> DepositIntCollectionReversalApprovalIL(string userName, int CompanyID, int branchID, DepositInterestReversalApprovalDto approvalDto);
        public Task<string> DepositIntCollectionReversalApprovalAML(string userName, int CompanyID, int branchID, DepositInterestReversalApprovalDto approvalDto);


        //encashment
        public  Task<List<DepositInterestInfoDto>> GetDepositInterestInfoForEncashment(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, int FundID);
        public Task<string> InsertUpdateDepositInterestEncashment(int CompanyID, int BranchID, string userName, DepositEncashmentDto entry);
        public Task<List<ListDepositEncashmentDto>> ListDepositInterestEncashmentIL_AML(int CompanyID, int BranchID, string FromDate, string ToDate, string SearchKeyword, string ListType, int FundID);
        public Task<string> DepositIntEncashmentApprovalIL(string userName, int CompanyID, int branchID, DepositInterestEncashmentApprovalDto approvalDto);
        public Task<string> DepositIntEncashmentApprovalAML(string userName, int CompanyID, int branchID, DepositInterestEncashmentApprovalDto approvalDto);


        //RENEWAL
        public Task<string> InsertUpdateDepositAccountRenewalAML(int CompanyID, int BranchID, string userName, DepositInstrumentRenewal entry);
        public Task<object> GetRenewalListAML(int CompanyID, int BranchID, string userName, int FundID);
        public Task<object> RenewalListAML(int CompanyID, int BranchID, string userName, string FromDate, string ToDate, int FundID, string ListType);
        public Task<string> DepositAccountRenewalApprovalAML(string userName, int CompanyID, int branchID, DepositInterestRenewalApprovalDto approvalDto);

    }
}
