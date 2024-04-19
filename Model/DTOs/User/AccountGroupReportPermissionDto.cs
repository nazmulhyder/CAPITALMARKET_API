using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.User
{
    public class AccountGroupReportPermissionDto
    {
        public int? AgrGroupID { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public List<AssignAgrGrpToContractDto>? AgrGrpContracts { get; set; } = null;

    }

    public class AssignAgrGrpToContractDto
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNo { get; set; }
    }
}
