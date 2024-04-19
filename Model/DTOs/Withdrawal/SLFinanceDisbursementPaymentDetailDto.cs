using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLFinanceDisbursementPaymentDetailDto
    {
        public decimal? DisburseAmount { get; set; }
        public string? DisburseDate { get; set; }
        public int? DisbursementID { get; set; }
        public string? DisbursementHead { get; set; }      
        public int? PaymentDetailID { get; set; }
        public int? PayeeBankAccountID { get; set; }
        public int? PayeeIndexID { get; set; }     
        public int? MonInstrumentID { get; set; }
        public int? TransactionID { get; set; }

    }
}
