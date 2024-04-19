using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.User
{
	public class UserAccountDto
	{
		public string? EmployeeCIF { get; set; }
		public string? Company { get; set; }
		public string? Branch { get; set; }
		public string? Team { get; set; }
		public string? Designation { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }
		public string? UserName { get; set; }
		public bool? IsExist { get; set; }
		public bool? IsRM { get; set; }
	}
}
