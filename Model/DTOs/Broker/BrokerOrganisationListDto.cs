using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Broker
{
    public class BrokerOrganisationListDto
    {
        public Nullable<int> OrganizationID { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationType { get; set; }
        public string? TIN { get; set; }
    }
}
