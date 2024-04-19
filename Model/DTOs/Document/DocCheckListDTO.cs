using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Document
{
    public class DocCheckListDTO
    {
        public string typeName { get; set; }
        public int docCheckListID { get; set; }
        public string documentName { get; set; }
        public bool isMandatory { get; set; }
    }
}
