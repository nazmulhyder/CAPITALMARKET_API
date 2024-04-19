using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class ApprovalDto
    {
        public int approvalRequestSetID { get; set; }
        public string approvalStatus { get; set; }
        public int approvalLevelRequired { get; set; }
        public int currentApprovalLevel { get; set; }
        public string approvalRequestedBy { get; set; }
        public string approvalRequestDate { get; set; }
        public string approvalType { get; set; }
        public List<ApprovalDetail> approvalDetail { get; set; }
    }
}
