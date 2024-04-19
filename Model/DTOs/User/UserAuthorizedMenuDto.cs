using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.User
{
    
        public class UserMenuDto
    {
        public int? OrderID { get; set; }
        public int? AuthorityID { get; set; }
        public int? RoleID { get; set; }
        public string? ModuleName { get; set; }
        public int? MenuID { get; set; }
        public string? IconString { get; set; }
        public string? MenuName { get; set; }
        public string? MenuURL { get; set; }
        public int? ModuleID { get; set; }
        public int? ParentMenuID { get; set; }
        public int? MenuLevel { get; set; }
        public string? MenuLevelName { get; set; }
        public bool? HasEditAccess { get; set; }
    }
    public class UserAuthorizedParrentMenuDto
    {
        public int? OrderID { get; set; }
        public int? AuthorityID { get; set; }
        public int? RoleID { get; set; }
        public string? ModuleName { get; set; }
        public int? MenuID { get; set; }
        public string? IconString { get; set; }
        public string? MenuName { get; set; }
        public string? MenuURL { get; set; }
        public int? ModuleID { get; set; }
        public int? ParentMenuID { get; set; }
        public string? ParentMenuName { get; set; }
        public int? MenuLevel { get; set; }
        public string? MenuLevelName { get; set; }
        public bool? HasEditAccess { get; set; }
        public List<UserAuthorizedParrentMenuDto> ChildMenuList = new List<UserAuthorizedParrentMenuDto>();
    }

}
