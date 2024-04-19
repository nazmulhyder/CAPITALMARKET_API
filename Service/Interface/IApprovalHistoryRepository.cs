using Model.DTOs.ApprovalHistory;
using Model.DTOs.UpdateLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IApprovalHistoryRepository
    {
        public Task<List<ApprovalHistoryDTO>> getApprovalHistoryList(int approvalTypeCodeId);
    }
}
