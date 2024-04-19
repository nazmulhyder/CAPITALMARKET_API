using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class InternalFundTransferApprovalDto
    {
        public string IntFundTransferIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }
}
