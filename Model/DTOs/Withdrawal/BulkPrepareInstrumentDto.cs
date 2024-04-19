using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class BulkPrepareInstrumentDto
    {
        public string? DisbursementIDs { get; set; }
        public string? TransactionMode { get; set; }
        public int? PaymentBankAccountId { get; set; }

    }
}
