using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.KYC
{

    public class KycAccountInfo
    {
        public int? IndexID { get; set; }
        public string? AccountNumber { get; set; }
        public string? BOCode { get; set; }
        //public int? RMID { get; set; }
        //public string? RMName { get; set; }
        public string? TINNo { get; set; }
        public bool? HasTIN { get; set; } = false;
        public bool? IsTINCopyObtained { get; set; }
        public string? NIDNo { get; set; }
        public bool? HasNID { get; set; } = false;
        public bool? IsNIDCopyObtained { get; set; }
        public string? PassportNo { get; set; }
        public bool? HasPassport { get; set; } = false;
        public bool? IsPassportCopyObtained { get; set; }
        public string? Prefession { get; set; }
        public string? VATReg { get; set; }
        public bool? HasVATReg { get; set; } = false;
        public bool? IsVatCopyObtained { get; set; } = false;
        public string? DrivingLicense { get; set; }
        public bool? HasDrivingLicense { get; set; } = false;
        public bool? IsDrivingLicenseObtained { get; set; } = false;    
        public string? ProductName { get; set; }
        public int? ProductID { get; set; }
        public string? MemberName { get; set; }
        public string? NameOfOffiialOpeningAccount { get; set; }
        public string? ApproverRemarks { get; set; }
        public string? ComplianceRemarks { get; set; }
    }

    public class KYCDto : KycAccountInfo
    {
        public int? SLAgrKYCID { get; set; } = 0;
        public int? ContractID { get; set; }
        public string? SourceOfFund { get; set; }
        public string? SOFJustification { get; set; }
        public string? OwnerDetail { get; set; }
        //public int? TypeOfBusiness { get; set; }
        public string? ProfessionOrBusinessType { get; set; }
        public int? ScoreProforBT { get; set; }
        public string? NetWorth { get; set; }
        public int? ScoreNetworth { get; set; }
        public string? ACOpeningMode { get; set; }
        public int? ScoreACOpeningMode { get; set; }
        public string? ExptValMonthlyTransaction { get; set; }
        public int? ScoreExptValMT { get; set; }
        public string? ExptNoOfMonthlyTransaction { get; set; }
        public int? ScoreExptNumMT { get; set; }
        public string? ExptValCashMT { get; set; }
        public string? ExptValMCT { get; set; }
        public int? ScoreExptValMCT { get; set; }
        public string? ExpNumMCT { get; set; }
        public int? ScoreExpNumMCT { get; set; }
        public decimal? TPSNumOfMDT { get; set; }
        public decimal? TPSNumOfMWT { get; set; }
        public decimal? TPSMaxSizeOfDT { get; set; }
        public decimal? TPSMaxSizeOfWT { get; set; }
        public string? ORARiskLevel { get; set; }
        public string? ORARiskRating { get; set; }
        public string? ORADueDiligence { get; set; }
        public int? TotalScore { get; set; }
        public string? Comments { get; set; }
        public bool? IsCustomerAddressVerified { get; set; } = false;
        public string? AddressVerificationNotes { get; set; }
        public bool? IsPEP { get; set; } = false;
        public bool? PEPIsApprovalTaken { get; set; } = false;
        public string? PEPSourceOfWealth { get; set; }
        public string? PEPSOWComments { get; set; }
        public bool? IsFtoFInterviewTaken { get; set; } = false;
        public string? Status { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? RefreeID { get; set; }
        public string? RefreeName { get; set; }
        public string? TypeOfVisa { get; set; }
        public bool? HasDirectorshipOfCompany { get; set; } = false;
        public string? CompanyNameOrRemarks { get; set; }
    }

    public class PendingKYCDto
    {
        public string? AccountNumber { get; set; }
        public int? ContractID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set;}
        public string? Status  { get; set;}
        public string? MemberName { get; set; }
        public string? LastUpdateOn { get; set; }
       

    }

    public class CompleteKYCDto : PendingKYCDto
    {
        public string? Maker
        {
            get; set;
        }

        public class ApprovalKYCDto : CompleteKYCDto
        {
            public int? SLAgrKYCID { get; set; }
            public string? Maker { get; set; }
            public string? MakeDate { get; set; }
        }


        public class ApprovalILKYCDto : CompleteKYCDto
        {
            public int? ILAgrKYCID { get; set; }
            public string? Maker { get; set; }
            public string? MakeDate { get; set; }
        }

        public class ApprovalAMLKYCDto : CompleteKYCDto
        {
            public int? AMLAgrKYCID { get; set; }
            public string? Maker { get; set; }
            public string? MakeDate { get; set; }
        }

        public class CodesKYCDto
        {
            public int? ID
            {
                get; set;
            }
            public string? typeValue
            {
                get; set;
            }
            public int? Score
            {
                get; set;
            }
            public string? RiskLevel
            {
                get; set;
            }
        }

        public class ReviewKYCDto : ApprovalKYCDto
        {
            public string? ApprovalStatus { get; set; }
            public string? ApprovalRemarks { get; set; }
        }

        public class ReviewILKYCDto : ApprovalILKYCDto
        {
            public string? ApprovalStatus { get; set; }
            public string? ApprovalRemarks { get; set; }
        }

        public class ReviewAMLKYCDto : ApprovalAMLKYCDto
        {
            public string? ApprovalStatus { get; set; }
            public string? ApprovalRemarks { get; set; }
        }
    }


}
