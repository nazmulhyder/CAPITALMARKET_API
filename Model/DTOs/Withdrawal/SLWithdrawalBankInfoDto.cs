using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Withdrawal
{
    public class SLWithdrawalBankInfoDto
    {
        public int? ContractID { get; set; }
        public string? MemberName { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? MaxWithdrawalAmount { get; set; }
        public string? BankName { get; set; }
        public string? BAName { get; set; }
        public string? BANumber { get; set; }
        public string? BankBranch { get; set; }
        public string? RoutingNo { get; set; }     
        public string? BAType { get; set; }
        public string? InvestorPhoto { get; set; }
        public string? InvestorSignature { get; set; }
    }

    public class SLWithdrawalInvestorDocument
    {
        public int? DocumentID { get; set; }
        public string? DocumentName { get; set; }
        public string? DocFileName { get; set; }
        public string? DocFilePath { get; set; }
        public string? DocumentStatus { get; set; }
    }
}
