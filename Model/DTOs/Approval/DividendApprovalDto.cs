using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Approval
{
    public class DividendApprovalDto
    {
        public int MFDividendDecID { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class CashDividentDistributionApprovalDto
    {
        public string MFCashDivDistIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    public class StockDividentDistributionApprovalDto
    {
        public string MFCIPDivDistIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }
}
