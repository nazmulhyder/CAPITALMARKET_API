using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.OrderSheet
{
    public class OrdersheetApprovalDTO
    {
        public string DocInventoryIDs { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
        public string? ApproveFrom { get; set; }
    }
}
