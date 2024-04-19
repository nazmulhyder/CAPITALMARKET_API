using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class TransactionDateStatusDTO
    {
        public DateTime TransactionDate { get; set; }
        public bool EODStatus { get; set; }
    }

    public class TypeCodeDTO
    {
        public string typeName { get; set; }
        public int typeCode { get; set; }
        public string typeValue { get; set; }
        public string typeShortName { get; set; }
    }

    public class TypeCodebyCompanyDTO
    {
        public string TypeName { get; set; }
        public int TypeCode { get; set; }
        public string TypeValue { get; set; }
        public string TypeShortName { get; set; }

    }
}
