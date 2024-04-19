using Model.DTOs.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.MarginRequest
{
    public class MarginRquestDto
    {
        public int? AppraisalInfoID { get; set; }
        public int? ContractID { get; set; }
        public string? AppraisalDate { get; set; }
        public string? ClientName { get; set; }
        public string? Address { get; set; }
        public decimal? Equity { get; set; }
        public string? FirstDepositClrDate { get; set; }
        public decimal? CurrYearTurnOver { get; set; }
        public decimal? RealizedGain { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? accumulatedGain { get; set; }
        public string? Education { get; set; }
        public string? Subject { get; set; }
        public string? Profession { get; set; }
        public string? Designation { get; set; }
        public string? CompanyName { get; set; }
        public string? smExperiance { get; set; }
        public string? TargetNetWorth { get; set; }
        public string? ClientType { get; set; }
        public decimal? CurrTWRR { get; set; }
        public decimal? ProposedIntRate { get; set; }
        public decimal? ProposedLoanRatio { get; set; }
        public int? MarginRequestID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Reviewer { get; set; }
        public string? ReviewDate { get; set; }
        public string? ReviewRemarks { get; set; }
        public string? CompletedBy { get; set; }
        public string? CompleteDate { get; set; }
        public string? CompleteRemarks { get; set; }
        public string? AccountNumber { get; set; }
        public string? RequestStatus { get; set; }
        public int? Operation { get; set; }
        public string? businessDescription { get; set; }
        public string? educationDescription { get; set; }
        public string? networthDescription { get; set; }
        public List<MarginClinkedClientDto>? marginClinkedClients { get; set; }
        public List<MerginReqValidation>? MerginReqValidations { get; set; }
        public List<EntryDocumentDto>? documents { get; set; }
    }

    public class MarginRequestClientInfo
    {
        public string MemberName { get; set; }
        public string Address { get; set; }
        public decimal? Equity { get; set; }
        public string? FirstDepositClrDate { get; set; }
        public string? FirstTradingDate { get; set; }
        public decimal? CurrYearTurnOver { get; set; }
        public decimal? RealizedGain { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? accumulatedGain { get; set; }
        public int? ContractID { get; set; }
        public string? poaName { get; set; }
        public string? poaEmail { get; set; }
        public string? poaMobile { get; set; }
        public string? poaAddress { get; set; }
    }

    public class MarginClinkedClientDto
    {
        public int? maLinkedClientID { get; set; }
        public int? indexID { get; set; }
        public string? memberCode { get; set; }
        public string? memberName { get; set; }
        public string? relationshipType { get; set; }
        public int? marginRequestID { get; set; }
        public string? accountNumber { get; set; }
        public string? productName { get; set; }
        public int? contractID { get; set; }
        public int? productID { get; set; }
        public string? accountType { get; set; }
    }

    public class ListMarginRequests_ViewtStatusDto
    {
        public int? ContractID { get; set; }
        public int? MarginRequestID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? RequestStatus { get; set; }
        public string? ReviewRemarks { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestedDate { get; set; }
        public string? Branch { get; set; }
        public string? AssessBy { get; set; }
        public string? AssessDate { get; set; }
        public string? CompletedBy { get; set; }
        public string? CompleteDate { get; set; }

    }

    public class MerginReqValidation
    {
        public string? ValidationMsg { get; set; }
        public bool? IsValidateSuccess { get; set; }
    }

    public class FilterDto
    {
        public string? FilterType { get; set; }
        public string? FilterFrom { get; set; }
        public string? FilterTo { get; set; }
    }

    public class MarginMonitoringDto
    {
        public int ContractID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public decimal? Equity { get; set; }
        public decimal? Loan { get; set; }
        public decimal? EDR { get; set; }
        public bool? IsSMSSent { get; set; } = false;
        public bool? IsEmailSent { get; set; } = false;
        public string? EventName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
    }


    public class MarginMonitoringSMSEmailDto
    { 
        public List<MarginMonitoringDto>? MarginCallZoneList { get; set; }
        public List<MarginMonitoringDto>? ForcedSellZoneList { get; set; }
    }

    public class GetMarginRquestDto
    {
        public int? AppraisalInfoID { get; set; }
        public int? ContractID { get; set; }
        public string? AppraisalDate { get; set; }
        public string? ClientName { get; set; }
        public string? Address { get; set; }
        public decimal? Equity { get; set; }
        public string? FirstDepositClrDate { get; set; }
        public decimal? CurrYearTurnOver { get; set; }
        public decimal? RealizedGain { get; set; }
        public decimal? UnrealizedGain { get; set; }
        public decimal? accumulatedGain { get; set; }
        public string? Education { get; set; }
        public string? Subject { get; set; }
        public string? Profession { get; set; }
        public string? Designation { get; set; }
        public string? CompanyName { get; set; }
        public string? SMExperiance { get; set; }
        public string? TargetNetWorth { get; set; }
        public string? ClientType { get; set; }
        public decimal? CurrTWRR { get; set; }
        public decimal? ProposedIntRate { get; set; }
        public decimal? ProposedLoanRatio { get; set; }
        public int? MarginRequestID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Reviewer { get; set; }
        public string? ReviewDate { get; set; }
        public string? ReviewRemarks { get; set; }
        public string? CompletedBy { get; set; }
        public string? CompleteDate { get; set; }
        public string? CompleteRemarks { get; set; }
        public string? AccountNumber { get; set; }
        public string? RequestStatus { get; set; }
        public int? Operation { get; set; }
        public string? businessDescription { get; set; }
        public string? poaName { get; set; }
        public string? poaEmail { get; set; }
        public string? poaMobile { get; set; }
        public string? poaAddress { get; set; }
        public string? educationDescription { get; set; }
        public string? networthDescription { get; set; }
        public List<MarginClinkedClientDto>? marginClinkedClients { get; set; }
        public List<MerginReqValidation>? MerginReqValidations { get; set; }
        public List<DocumentDto>? documents { get; set; }
    }

    public class SendSMSDto
    {
        public string? EventName { get; set; }
        public string? RecipientMobileNo { get; set; }
        public string? SMSText { get; set; }
        public int? DataKeyID { get; set; }
    }

    public class SendEmailDto
    {
        public string? EventName { get; set; }
        public string? RecipientEmailNo { get; set; }
        public string? EmailText { get; set; }
        public int? DataKeyID { get; set; }
    }


}
