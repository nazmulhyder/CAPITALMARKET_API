using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.User
{
    public class UserDto
    {
        public Nullable<int> UserID { get; set; }
        public string? UserName { get; set; }
        public string? EmployeeID { get; set; }
        public string? UserStatus { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public string? EmployeeCIF { get; set; }
        public string? Designation { get; set; }
        public string? FullName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
        public List<UserAuthority> UserPermissionList { get; set; } = null;
    }

    public class UserAuthority
    {
        public Nullable<int> AuthorityID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int? RoleID { get; set; }
        public int? LicCompanyID { get; set; }
        public int? LicBranchID { get; set; }
    }

    public class CompanyDto
    {
        public int? CompanyID { get; set; }
        public string? CompanyName { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> IndexID { get; set; }
        public List<BranchDto> BranchList { get; set; } = null;
    }

    public class BranchDto
    {
        public int? BranchID { get; set; }
        public int? CompanyID { get; set; }
        public string? BranchName { get; set; }
        public string? BranchAddress { get; set; }
        public string? BranchManager { get; set; }
        public List<RoleDto> RoleList { get; set; } = null;
    }

    public class RoleDto
    {
        public int? RoleID { get; set; }
        public string? RoleName { get; set; }
        public Nullable<bool> HassAccess { get; set; }
    }

}
