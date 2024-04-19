using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class InstrumentApprovalDto
    {
            public int VoidOrWithdrawalInstrumentID { get; set; }
            public string ApprovalType { get; set; }
            public  bool IsApproved { get; set; }
            public string? ApprovalRemark { get; set; }
    }
}
