using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.InstallmentSchedule
{
    public class GenInstallmentSchedule
    {
        public int InstallmentID { get; set; }
        public int ContractID { get; set; }
        public string DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string CollectionMode { get; set; }
        public string Remarks { get; set; }
    }
}
