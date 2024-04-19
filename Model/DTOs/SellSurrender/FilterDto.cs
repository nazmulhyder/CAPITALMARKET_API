using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SellSurrender
{
    public class SurrenderFilterDto
    {
        public int ProductID { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? AccountNumber { get; set; }
        public string? ListType { get;set; }
    }
}
