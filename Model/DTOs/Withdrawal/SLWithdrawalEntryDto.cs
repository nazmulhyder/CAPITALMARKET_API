using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLWithdrawalEntryDto
    {
        public SLFinanceDisbursementDto SLFinanceDisbursement { get; set; }
        public SLFinanceDisbursementPaymentDetailDto SLFinanceDisbursementPaymentDetails { get; set; }

    }
}
