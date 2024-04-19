using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.InstrumentGroup
{
    public class InstrumentGroupDto
    {
        public int? TotalRowCount { get; set; }
        public Nullable<int> InstrumentGroupID { get; set; }

        public string GroupName { get; set; }
        public string GroupDetail { get; set; }
        public string? ApprovalStatus { get; set; }

        public List<InstrumentGroupDetailDto> InstrumentList { get; set; }
    }

    public class InstrumentGroupDetailDto
    {
        public int? InstrumentGroupDetailID { get; set; }

        public int? InstrumentGroupID { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set;}
        public decimal? PERatio { get; set; }
    }

    public class InstrumentDropdownDto
    {
        public string InstrumentName { get; set; }
        public int InstrumentID { get; set; }
        public decimal PERatio { get; set; }
    }

    public class InstrumentGroupDropdownDto
    {
        public int InstrumentGroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDetail { get; set; }
    }

    public class InstrumentByInstrumentGrpDto
    {
        public string InstrumentName { get; set; }
        public int InstrumentID { get; set; }
    }
}
