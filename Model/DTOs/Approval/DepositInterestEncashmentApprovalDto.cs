using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class DepositInterestEncashmentApprovalDto
    {
        public string EncashmentIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class DepositInterestRenewalApprovalDto
    {
        public string DepositRenewalIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }


}
