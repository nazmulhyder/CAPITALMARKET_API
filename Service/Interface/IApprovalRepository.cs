using Model.DTOs.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IApprovalRepository
    {
        public bool UpdateApprovalStatus(int approvalReqSetId, ApprovalDetail approvalDetl);
    }
}
