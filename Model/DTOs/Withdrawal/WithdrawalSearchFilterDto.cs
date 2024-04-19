using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class WithdrawalSearchFilterDto
    {
        public string? WithdrawalFrom { get; set; }
        public string? WithdrawalTo { get; set; }
        public string? List_Type { get; set; }

    }
}
