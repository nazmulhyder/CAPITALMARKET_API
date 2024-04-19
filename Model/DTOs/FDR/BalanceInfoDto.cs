using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FDR
{
    public class BalanceInfoDto
    {
        public int ContractID { get; set; }
        public string? MemberName { get; set; }
        public string? AccountNumber { get; set; } 
        public decimal? AvailableBalance { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
    }
}
