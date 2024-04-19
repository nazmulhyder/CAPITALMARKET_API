using Microsoft.AspNetCore.Http;
using Model.DTOs.Approval;
using Model.DTOs.Charges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IChargesRepository
    {
        public Task<List<ChargesDto>> GetChargeList(string UserName, int CompanyID, int BranchID, string ListType, string ManualEntryEnable, string AdjustmentEnable);
        public Task<ChargeSetupDto> GetChargeDetail(string UserName, int CompanyID, int BranchID, int AttributeID);
       
        public Task<string> SaveUpdateChargeDetail(string UserName, int CompanyID, int BranchID, decimal ChargeAmount, ChargeSetupDto data);
        public Task<string> ChargeApproval (string UserName, int CompanyID, int BranchID, ChargeApprovalDto data);

        public Task<object> AccrualChargeFileValidateUpload(string UserName, int CompanyID, int BranchID, IFormCollection data);
        public Task<object> ListAccrualChargeFile(string UserName, int CompanyID, int BranchID);
        public Task<object> ListAccrualChargeFileDetail(int CompanyID, int BranchID, int ChargeFileID);
        public Task<string> AccrualChargeFileApproval(string UserName, int CompanyID, int BranchID, AccrualChargeFileApprovalDto data);

        public Task<object> GetAccrualAccountList(string UserName, int CompanyID, int BranchID, AccrualAccountFilterDto data);
        public Task<string> ApproveAccruedChargeSchedule(string UserName, int CompanyID, int BranchID, AccrualChargeApprovalDto data);
        
        public Task<object> GetCLientInfoForManualChargeEntry(string UserName, int CompanyID, int BranchID, string AccountNumber);
        public Task<string> ManualChargeEntry(string UserName, int CompanyID, int BranchID, ManualChargeDto data);

        public Task<object> ListManualCharge(string UserName, int CompanyID, int BranchID, string ListType);
        public Task<string> ManualChargeApprove(string UserName, int CompanyID, int BranchID, ManualChargeApproveDto data);
        
        public Task<object> BulkManualChargeEntryValidation(string UserName, int CompanyID, int BranchID, IFormCollection data);
        public Task<string> BulkManualChargeEntry(string UserName, int CompanyID, int BranchID,int AttributeID, int ProductID, List<ManualChargeBulkDto> data);

        public Task<object> ListAccountChargeForReversalEntry(string UserName, int CompanyID, int BranchID, AccountChargeForReversalEntryDto data);
        public Task<string> AccountChargeReversalEntry(string UserName, int CompanyID, int BranchID, AccountChargeReversalEntryDto data);
        public Task<object> AccountChargeReversalList(string UserName, int CompanyID, int BranchID, AccountChargeReversalDto data);
        public Task<string> AccountChargeReversalApprove(string UserName, int CompanyID, int BranchID, AccountChargeReversalEntryDto data);
        public Task<string> ChargeAdjustmentEntry(string UserName, int CompanyID, int BranchID, ManualChargeDto data);
        public Task<object> ListChargeAdjustment(string UserName, int CompanyID, int BranchID, string ListType);
        public Task<string> ChargeAdjustmentApprove(string UserName, int CompanyID, int BranchID, ManualChargeApproveDto data);
    }
}
