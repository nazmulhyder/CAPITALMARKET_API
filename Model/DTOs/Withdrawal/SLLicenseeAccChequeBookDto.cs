using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLLicenseeAccChequeBookDto
    {
        public int? ChequeBookID { get; set; }
        public long? FromCheque { get; set; }
        public long? ToCheque { get; set; }
        public long? LastChequePrint { get; set; }
        public string? DisbBankAccountID { get; set; }
        public string? BranchID { get; set; }

    }

    public class ChequeLeavesListDto :  SLLicenseeAccChequeBookDto 
    {
        public int NoOfCheque { get; set; }
    }
}
