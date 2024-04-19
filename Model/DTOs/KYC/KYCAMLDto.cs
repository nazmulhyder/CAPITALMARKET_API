using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.KYC
{
    public class KycAMLAccountInfo
    {
        public int? IndexID { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOCode { get; set; }
        public string? ProductName { get; set; }
        public int? ProductID { get; set; }
        public string? MemberName { get; set; }
        public string? ApproverRemarks { get; set; }
        public string? ComplianceRemarks { get; set; }
    }

    public class KYCAMLDto : KycAMLAccountInfo
    {
        public int AMLAgrKYCID { get; set; }
        public int ContractID { get; set; }
        public string? Status { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int ApprovalSetID { get; set; }
        public int? AddressVerifiedByID { get; set; }
        public string? AddressVerifiedBy { get; set; }
        public bool? IsHighRiskBT { get; set; } = false;
        public string? SourceOfFund { get; set; }
        public bool? DepositMatchWithProfile { get; set; } = false;
        public string? SourceOfAddFund { get; set; }
        public int? SourceOfFundID { get; set; }
        public string? HowSOFVerified { get; set; }
        public bool? IsPEP { get; set; } = false;
        public string? RiskGrading { get; set; }
        public string? ApplicantType { get; set; }
        public int? IdenDocTypeID { get; set; }
        public string? IdenDocType { get; set; }
        public int? BusinessTypeID { get; set; }
        public string? BusinessType { get; set; }
        public int? OccDocID { get; set; }
        public string? OccDocName { get; set; }
        public string? VatRegistrationNo { get; set; }
        public string? CommentsOnClientProfile { get; set; }
        public string? ShareholderInformation { get; set; }
        public string? Comments { get; set; }
    }
}
