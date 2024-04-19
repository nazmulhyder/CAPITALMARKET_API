using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class FilterWithdrawalDto
    {
        public int? ContractID { get; set; } = 0;
        public string? ListType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? ProductID { get; set; }
    }

    public class BulkInstrumentVoid
    {
        public string? AccountNumber { get; set; }
        public string? InstrumentNumber { get; set; }
        public string?  Reason { get; set; }
        public string? MakeVoid { get; set; }
    }
}
