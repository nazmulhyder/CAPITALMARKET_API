using Model.DTOs.CMExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Depository
{
    public class CMDepositoryDTO
    {
        public int? TotalRowCount { get; set; }
        public int? DepositoryID { get; set; }

        public string? CompanyName { get; set; }
        public string? ShortName { get; set; }
        public int? IndexID { get; set; }
        public string? MailingAddress { get; set; }
        public int? OrganizationID { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
        public string? WebAddress { get; set; }
        public string? District { get; set; }
        public string? Division { get; set; }
        public string? Country { get; set; }
        public Int16? CountryCode { get; set; }
        public Int16? DivisionCode { get; set; }
        public Int16? DistrictCode { get; set; }

        public List<ContactPersonDTO> ContactPersons { get; set; }
    }
}
