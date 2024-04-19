using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class ApprovalDetail
    {
        public int approvalReqID { get; set; }
        public int approvalLevel { get; set; }
        public string approvalStatus { get; set; }
        public string? approver { get; set; }
        public string? approvalDate { get; set; }
        public string approverRemark { get; set; }
    }
}
