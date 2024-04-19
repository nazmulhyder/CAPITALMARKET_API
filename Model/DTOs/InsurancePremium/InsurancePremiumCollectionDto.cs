using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.InsurancePremium
{
    public class InsurancePremiumCollectionDto
    {
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string MemberName { get; set; }
        public string? InstallmentDate { get; set; }
        public decimal? InsurancePremiumAmount { get; set; }
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public int? InstallmentID { get; set; }
    }
}
