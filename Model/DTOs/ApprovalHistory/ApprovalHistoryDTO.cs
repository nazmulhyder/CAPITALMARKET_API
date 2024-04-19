using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ApprovalHistory
{
    public class ApprovalHistoryDTO
    {
        public Nullable<int> ApprovalTypeCode { get; set; }
        public Nullable<int> ApprovalReqID { get; set; }

        public string? RequestedBy { get; set; }
        public Nullable<DateTime> RequestDate { get; set; }

        public Nullable<int> ApprovalLevel { get; set; }
        public string? Approver { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
        public string? ApprovalRemarks { get; set; }
    }
}
