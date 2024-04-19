using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SellSurrender
{
    public class CDBLFileDataDto
    {
        public string? FileName { get; set; }
        public string? FileContent_ControlRecord { get; set; }
        public string? FileContent_Line1 { get; set; }
        public string? FileContent_Line2 { get; set; }
        public string? FileContent_Line3 { get; set; }
        public List<ContenDto> Content = new List<ContenDto>();

    }

    public class ContenDto
    {
        public string? Content { get; set; }
    }
}
