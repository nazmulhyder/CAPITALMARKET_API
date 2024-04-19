using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLFinanceDisbursementDto
    {
        public int? DisbursementID { get; set; }
        public string? ProcessingDate { get; set; }
        public int? ContractID { get; set; }

    }
}
