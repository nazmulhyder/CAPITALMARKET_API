using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class ListReleaseChequeLeafSL
    {
        public long FromCheque { get; set; }
        public long ToCheque { get; set; }
        public long LastChequePrint { get; set; }
        public string Maker { get; set; }
        public string MakeDate { get; set; }
    }

    public class EntryReleaseCheque
    {
        public int ProductID { get; set; }
        public int DisbBankAccountID { get; set; }
        public long FromCheque { get; set; }
        public long ToCheque { get; set; }

    }
}
