using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLPrepareWithdrawal
    {
        public int MonInstrumentID { get; set; }
        public string InstrumentType { get; set; }
        public string InstrumentNmbr { get; set; }
        public decimal Amount { get; set; }
        public string InstrumentDate { get; set; }
        public string Status { get; set;}
        public int ProductID { get; set; }
        public int BankAccountID { get; set; }
        public int InstIssuedByIndexID { get; set; }
        public int DisbursementID { get; set; }
    }
}
