using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class CommonResponseDto
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public object Data { get; set; }
    }
}
