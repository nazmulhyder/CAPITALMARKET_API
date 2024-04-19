using Model.DTOs.Approval;
using Model.DTOs.InsurancePremium;
using Model.DTOs.KYC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.DTOs.KYC.CompleteKYCDto;

namespace Service.Interface
{
    public interface IKYCRepository
    {
        #region IDLC-SL
        public Task<List<PendingKYCDto>> PendingKYCSL(int CompanyID, int BranchID, string UserName);
        public Task<List<CompleteKYCDto>> CompleteKYCSL(int CompanyID, int BranchID, string UserName);
        public Task<List<CodesKYCDto>> CodesKYC(int CompanyID, int BranchID, string UserName, string TypeName, string TypeID);
        public Task<KYCDto> KYCAccountInfoSL(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID);
        public Task<string> InsertUpdateKYCSL(int CompanyID, int BranchID, string userName, KYCDto entry, string ActionType);
        public Task<List<ApprovalKYCDto>> ListApprovalKYCSL(int CompanyID, int BranchID, string UserName);
        public Task<string> KYCApproval(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto);
        public Task<List<ReviewKYCDto>> ReviewComScreenKYCSL(int CompanyID, int BranchID, string UserName);
        #endregion IDLC-SL

        #region IDLC-IL
        public Task<List<PendingKYCDto>> PendingKYCIL(int CompanyID, int BranchID, string UserName);
        public Task<List<CompleteKYCDto>> CompleteKYCIL(int CompanyID, int BranchID, string UserName);
        public Task<string> InsertUpdateKYCIL(int CompanyID, int BranchID, string userName, KYCILDto entry, string ActionType);
        public Task<KYCILDto> KYCAccountInfoIL(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID);
        public Task<List<ApprovalILKYCDto>> ListApprovalKYCIL(int CompanyID, int BranchID, string UserName);
        public Task<string> KYCApprovalIL(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto);
        public Task<List<ReviewILKYCDto>> ReviewComScreenKYCIL(int CompanyID, int BranchID, string UserName);
        #endregion IDLC-IL


        #region IDLC-AML
        public Task<List<PendingKYCDto>> PendingKYCAML(int CompanyID, int BranchID, string UserName);
        public Task<List<CompleteKYCDto>> CompleteKYCAML(int CompanyID, int BranchID, string UserName);
        public Task<List<CompleteKYCDto>> AllKYCAML(int CompanyID, int BranchID, string UserName, string Status);
        public Task<string> InsertUpdateKYCAML(int CompanyID, int BranchID, string userName, KYCAMLDto entry, string ActionType);
        public Task<KYCAMLDto> KYCAccountInfoAML(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID);
        public Task<List<ApprovalAMLKYCDto>> ListApprovalKYCAML(int CompanyID, int BranchID, string UserName);
        public Task<string> KYCApprovalAML(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto);
        //public Task<List<ReviewAMLKYCDto>> ReviewComScreenKYCAML(int CompanyID, int BranchID, string UserName);
        #endregion IDLC-AML

    }
}
