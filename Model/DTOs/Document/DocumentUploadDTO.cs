using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Document
{
    public class DocumentUploadDTO
    {
        public int documentId { get; set; }
        public string stage { get; set; }
        public IFormFile docFile { get; set; }
    }
}
