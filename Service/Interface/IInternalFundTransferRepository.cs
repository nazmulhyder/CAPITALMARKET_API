using Model.DTOs.Approval;
using Model.DTOs.InternalFundTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IInternalFundTransferRepository
    {
        public  Task<object> GetCustomerAvailableBalanceInfo(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNo);
        public  Task<string> InsertUpdateInternalFundTransfer(int CompanyID, int BranchID, string userName, InternalFundTransferDto entry);
        public Task<object> GetListInternalFundTransfer(string UserName, int CompanyID, int BranchID);
        public Task<string> InternalFundTransferApproval(string userName, int CompanyID, int branchID, InternalFundTransferApprovalDto approvalDto);
    }
}
