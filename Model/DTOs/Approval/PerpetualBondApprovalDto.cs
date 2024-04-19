using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class PerpetualBondApprovalDto
    {
        public string IntCollectionIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class PerpetualBondDeclarationApprovalDto
    {
        public string DeclarationIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class PerpetualBondReversalApprovalDto
    {
        public string IntCollReversalIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }
}
