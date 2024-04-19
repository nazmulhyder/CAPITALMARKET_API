using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.InstrumentGroup
{
    public class InstrumentGroupApprovalDto
    {
        public int InstrumentGroupID { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }


    public class InstrumentGroupApprovalListDto
    {
        public int? TotalRowCount { get; set; }
        public Nullable<int> InstrumentGroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDetail { get; set; }
        public string ApprovalStatus { get; set; }

    }
}
