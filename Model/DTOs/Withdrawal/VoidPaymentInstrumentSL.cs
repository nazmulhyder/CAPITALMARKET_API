using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class VoidPaymentInstrumentSL
    {
        public int MonInstrumentID { get; set; }
        public string InstrumentType { get; set; }
        public string InstrumentNumber { get; set; }
        public string InstrumentDate { get; set; }
        public decimal Amount { get; set; }
        public int ContractID { get; set; }
        public string AccountNumber { get; set; }
        public string ProductName { get; set; }

    }

    public class EntryVoidPaymentInstrumentSL
    {
        public string VoidOperationType { get;  set; }
        public string VoidReason { get; set; }
        public int MonInstrumentID { get; set; }
    }

    public class PostOnlineTransferSL
    {
        public int MonInstrumentID { get; set; }
        public string InstrumentType { get; set; }
        public string InstrumentNumber { get; set; }
        public string InstrumentDate { get; set; }
        public decimal Amount { get; set; }
        public int ContractID { get; set; }
        public string AccountNumber { get; set; }
        public string ProductName { get; set; }
        public string BankName { get; set; }
        public string BAName { get; set; }
        public string BANumber { get; set; }
        public string RoutingNo { get; set; }
        public string VoidReason { get; set; }
        public int PaymentDetailID { get; set; }
        public int? ProductID { get; set; }

    }

    public class EntryOnlineTransfer
    {
        public string OperationType { get; set; }
        public List<PostOnlineTransferSL> postOnlineTransfers { get; set; }
    }

    public class ChequeClearInfo
    {
        public int MonInstrumentID { get; set; }
        public int DisbBankAccountID { get; set; }
        public string DisbBankAccountNo { get; set; }
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public string ProductName { get; set; }
        public string InstrumentNo { get; set; }
        public string InstrumentType { get; set; }
        public string PaymentDate { get; set; }
        public int PaymentDetailID { get; set; }


    }

}
