using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class ListValidateAndPrintSLDto
    {
        public int? DisbursementID { get; set; }
        public string? PayeeName { get; set; }
        public string? BANumber { get; set; }
        public string? ChequeNo { get; set; }
        public decimal? Amount { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? ChequeStatus { get; set; }
        public int? MonInstrumentID { get; set; }
        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public string? RoutingNo { get; set; }

    }
}
