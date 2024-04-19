using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.CMExchange
{
    public class CMExchangeDTO
    {

        public int? TotalRowCount { get; set; }
        public int? ExchangeID { get; set; }

        public string? ExchangeName { get; set; }
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
        public short? CountryCode { get; set; }
        public short? DivisionCode { get; set; }
        public short? DistrictCode { get; set; }

        public List<ContactPersonDTO> ContactPersons { get; set; }

    }

    public class ContactPersonDTO
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

    }
}
