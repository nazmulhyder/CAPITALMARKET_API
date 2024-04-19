using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLWithdrawalInfoListDto
    {
        public int DisbursementID { get; set; }
        public string ProcessingDate { get; set; }
        public int ContractID { get; set; }
        public int ApprovalID { get; set; }
        public string ApprovalStatus { get; set; }
        public string Maker { get; set; }
        public string MakeDate { get; set; }
        public string MemberName { get; set; }
        public int TotalRowCount { get; set; }
        public string ProductName { get; set; }
        public decimal DisburseAmount { get; set; }
        public string DisburseDate { get; set; }
        public string AccountNumber { get; set; }
        public string Approver { get; set; }
        public string Branch { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? MaxWithdrawalAmount { get; set; }
        public int? ProductID { get; set; }
        public string? BranchName { get; set; }

    }
}
