using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.DTOs.Document
{
    public class DocumentDto
    {
        public int? documentID { get; set; } = null;
        public int? docCheckListID { get; set; } = null;
        public string? documentName { get; set; } = null;
        public string? docFileName { get; set; } = null;
        public string? docFilePath { get; set; } = null;
        //public bool isMandatory { get; set; }=false;
        // public Stream fileContent { get; set; }=null;
        public string? documentStatus { get; set; } = null;
        public string? docCollectionDate { get; set; } = null;
        public string? removalStatus { get; set; } = null;
        public string? remarks { get; set; } = null;
        public string? expectedDate { get; set; } = null;
        public object? docObj { get; set; } = null;
        public int approvalReqSetId { get; set; }
        public string? approvalStatus { get; set; }
        public string? makeDate { get; set; }
        public string? maker { get; set; }
    }

    public class EntryDocumentDto
    {

        public int? docCheckListID { get; set; } = null;
        public string? docCollectionDate { get; set; } = null;
        public string? docFileName { get; set; } = null;
        public string? docFilePath { get; set; } = null;
        public object? docObj { get; set; } = null;
        public int? documentID { get; set; } = null;
        public string? documentName { get; set; } = null;
        public string? documentStatus { get; set; } = null;
        public string? expectedDate { get; set; } = null;
        public string? remarks { get; set; } = null;
        public string? removalStatus { get; set; }=null;
       
        
       
    }
}
