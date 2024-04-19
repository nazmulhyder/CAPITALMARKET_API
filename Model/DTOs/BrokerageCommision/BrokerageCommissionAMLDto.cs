using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BrokerageCommision
{
    public class BrokerageCommissionAMLDto
    {
        public int CompanyID { get; set; }
        public int ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? ProductName { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? Brokers { get; set; }

    }



}
