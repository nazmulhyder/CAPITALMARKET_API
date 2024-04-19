using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FAQ
{
    public class GetClientNameByProductAndAccountDto
    {
        public int ContractID { get; set; }
        public string MemberName { get; set; }
    }


    public class InstrumentFaceValueListDto
    {
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? FaceValue { get; set; }
    }
}
