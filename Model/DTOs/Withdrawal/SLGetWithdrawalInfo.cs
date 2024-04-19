using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLGetWithdrawalInfo
    {
        public SLFinanceDisbursementDetailDto slFinanceDisbursement { get; set; }
        public SLFinanceDisbursementPaymentInfoDto slFinanceDisbursementPaymentDetails { get; set; }
        public List<SLWithdrawalInvestorDocument> SLWithdrawalInvestorDocuments { get; set; }

    }

    public class SLFinanceDisbursementDetailDto : SLFinanceDisbursementDto
    {
        public string? AccountNumber { get; set;}
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? BranchID { get; set; }
        public string? Branch { get; set; }
        public string? MemberName { get; set; }

    }

    public class SLFinanceDisbursementPaymentInfoDto : SLFinanceDisbursementPaymentDetailDto
    {
        public int? ContractID { get; set; }
        public string? MemberName { get; set; }
        public string? BankBranch { get; set; }
        public int? BankAccountID { get; set; }
        public string? RoutingNo { get; set; }
        public string? BankName { get; set; }
        public string? BAName { get; set; }
        public string? BANumber { get; set; }
        public string? BAType { get; set; }
        public decimal? MaxWithdrawalAmount { get; set; }

    }
}
