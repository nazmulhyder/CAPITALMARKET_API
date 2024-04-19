using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.User
{
	public class RoleMasterDto
	{
		public int? RoleID { get; set; }
		public string? RoleName { get; set; }
		public string? Purpose { get; set; }
		public List<ModuleListDto>? ModuleList { get; set; } = null;

	}
	public class ModuleListDto
	{
		public int? ModuleID { get; set; }
		public string? ModuleName { get; set; }
		public List<RoleDetailDto> roleDetailList { get; set; } = null;
	}
    public class MenuModuleListDto
    {
        public int? ModuleID { get; set; }
        public string? ModuleName { get; set; }
        public List<UserAuthorizedParrentMenuDto> MenuList { get; set; } = null;
    }


    public class RoleDetailDto
	{
		public string? RoleName { get; set; }

		public Nullable<int> RoleDetailID { get; set; }
		public int? RoleID { get; set; }
		public int? ModuleID { get; set; }
		public int? OrderID { get; set; }
		public int? MenuID { get; set; }
		public string? ModuleName { get; set; }

		public string? MenuName { get; set; }
		public int? MenuLevel { get; set; }
		public string? MenuURL { get; set; }
		public int? ParentMenuID { get; set; }
		public string? ParentMenuName { get; set; }
        public int? AuthorityID { get; set; }
        public string? IconString { get; set; }
        public string? MenuLevelName { get; set; }
        public bool? HasEditAccess { get; set; }
    }
}
