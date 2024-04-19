using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.PriceFileUpload
{
    public class CSEPriceFileDto
    {
        public List<Td>? td { get; set; }
    }
    public class Td
    {
        public string? slNo { get; set; }
        public string? ScripId { get; set; }
        public string? ScripCd { get; set; }
        public string? ScripName { get; set; }
        public string? ClosePrice { get; set; }
    }

}
