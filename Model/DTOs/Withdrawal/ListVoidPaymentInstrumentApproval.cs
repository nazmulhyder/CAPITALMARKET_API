using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class ListVoidPaymentInstrumentApproval
    {
            public int VoidInsOrWithdrawalCnclID { get; set; }
            public string Reason { get; set; }
			public int ApprovalSetID{ get; set; }
            public string ApprovalStatus{ get; set; }
            public string Maker{ get; set; }
            public string MakeDate{ get; set; }
            public int MonInstrumentID { get; set; }
            public string InstrumentType { get; set; }
            public string InstrumentNumber { get; set; }
            public string InstrumentDate { get; set; }
            public decimal Amount { get; set; }
            public int ContractID{ get; set; }
            public string AccountNumber { get; set; }
            public string ProductName { get; set; }
            public string ApprovalType { get; set; }
            public int ProductID { get; set; }
    }
}
