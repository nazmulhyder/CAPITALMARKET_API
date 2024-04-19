using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class ListDisbursementBankAccountSL
    {
        public int BankAccountID { get; set; }
        public string AccountNumber { get; set; }
        public string? BankName { get; set; }

    }
}
