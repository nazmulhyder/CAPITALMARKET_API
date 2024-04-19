using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class Lic_QryUserDetailDTO
    {
        public int? UserID { get; set; }

        public string UserName { get; set; }
        public string UserStatus { get; set; }
        public string EmployeeCIFNo { get; set; }
        public string EmployeeName { get; set; }

    }
}
