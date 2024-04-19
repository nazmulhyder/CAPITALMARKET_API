using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class KYCApprovalDto
    {
        public string AgrKYCIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
        public string UserType { get; set; }

    }
}
