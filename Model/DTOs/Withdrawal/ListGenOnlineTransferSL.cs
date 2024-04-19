using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class ListGenOnlineTransferSL
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string DebitBankAccountNumber { get; set; }
        public decimal DisburseAmount { get; set; }
        public string DisburseDate { get; set; }
        public string BankBranch { get; set; }
        public string RoutingNo    { get; set; }
        public string BankName { get; set; }
        public string BAName { get; set; }
        public string BANumber     { get; set; }
        public string? MobileNo { get; set; }
        public int MonInstrumentID { get; set; }
        public string? AccountType { get; set; }
        public string? Reason { get; set; }
        public string? EmailAddress { get; set; }
        public string? PayeeBankAccountNumber { get; set; }
        public string? PaymentDate { get; set; }



    }
}
