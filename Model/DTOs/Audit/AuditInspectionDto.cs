using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Audit
{


    public class AuditInspectionDto
    {
        public int? AuditInspectionID { get; set; }
        public string? ReferenceNo { get; set; }
        public string? AuditYear { get; set; }
        public string? AuditBranch { get; set; }
        public string? AuditAuthority { get; set; }
        public string? AuditObservation { get; set; }
        public string? ViolationRules { get; set; }
        public string? RegulatoryImpact { get; set; }
        public string? QueryType { get; set; }
        public string? UploadedQueryFIlePath { get; set; }
        public string? UploadedResponseFilePath { get; set; }
        public string? RegulatoryAction { get; set; }
        public string? Response { get; set; }
        public string? AuditorName { get; set; }
        public List<AuditInspectedClientDto>? auditInspectedClientDtos { get; set; }

    }

    public class AuditInspectedClientDto
    {
        public int? AuditInspectedClientID { get; set; }
        public int? ContractID { get; set; }
        public string? TradingDate { get; set; }
        public int? RMID { get; set; }
        public int? AuditInspectionID { get; set; }
        public string? TWS { get; set; }
        public int? InstrumentID { get; set; }
    }

    public class AgreementRegulatoryQuery
    {
        public int? RegulatoryQueryID { get; set; }
        public int? ContractID { get; set; }
        public string? QueryType { get; set; }
        public string? Authority { get; set; }
        public string? Instruction { get; set; }
        public int? InstrumentID { get; set; }
        public string? TradingDate { get; set; }
        public string? TWS { get; set; }
        public int? RMID { get; set; }
        public string? RegulatoryLetterNo { get; set; }
        public string? UploadedQueryFilePath { get; set; }
        public string? Response { get; set; }
        public string? UploadedResponseFilePath { get; set; }
        public int? Maker { get; set; }
        public string? MakeDate { get; set; }
    }

    public class AuditSearchFilter
    {
        public string? AuditYear { get; set; }
        public string? AuditBranch { get; set; }
        public string? AuditAuthority { get; set; }
        public string? AuditorName { get; set; }
        public string? ClientAccount { get; set;}
        public string? QueryType { get; set; }

    }

    public class AuditRegulatoryQueryDto
    {
        public int? RegulatoryQueryID { get; set; }
        public int? ContractID { get; set; }
        public string? QueryType { get; set; }
        public string? Authority { get; set; }
        public string? Instruction { get; set; }
        public int? InstrumentID { get; set; }
        public string? TradingDate { get; set; }
        public string? TWS { get; set; }
        public int? RMID { get; set; }
        public string? RegulatoryLetterNo { get; set; }
        public string? UploadedQueryFilePath { get; set; }
        public string? Response { get; set; }
        public string? UploadedResponseFilePath { get; set; }
    }

    public class RequlatoryQuerySearchFilter
    {
        public int? ProductID { get; set; }
        public string? AccountNo { get; set; }
        public string? RegulatoryLetterNo { get; set; }

    }
}
    
