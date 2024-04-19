using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FundOperation
{
	public class MutualFundApproveDto
	{
		public string? FundIDs { get; set; }
		public string? ApprovalRemark { get; set; }
		public string? ApprovalStatus { get; set; }
	}
	public class MutualFundDto
	{
		public Nullable<int> FundID { get; set; } = 0;

		public string? FundName { get; set; }
		public string? ShortName { get; set; }
		public string? FundType { get; set; }
		public Nullable<int> LotSize { get; set; }
		public Nullable<decimal> FaceValue { get; set; }

		public string? FundStatus { get; set; }
		public Nullable<int> ContractID { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> ApprovalReqSetID { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? Approver { get; set; }
		public Nullable<DateTime> ApprovalDate { get; set; }
		public string? ApprovalRemarks { get; set; }

		public Nullable<int> MFDetailID { get; set; }
		public Nullable<int> SponsorOrgID { get; set; }
		public Nullable<int> CustodianID { get; set; }
		public Nullable<int> TrusteeID { get; set; }

		public Nullable<decimal> SponsorContribution { get; set; }
		public Nullable<decimal> PreIPOSubsFund { get; set; }
		public Nullable<decimal> IPOSubsFund { get; set; }
		public Nullable<decimal> NewIssuedFund { get; set; }
		public string? RegistrationNo { get; set; }

		public string? TDRegistrationDate { get; set; }
		public string? FormationDate { get; set; }
		public Nullable<decimal> FundSize { get; set; }
		public string? IPOSubsStartDate { get; set; }
		public string? IPOSubsEndDate { get; set; }

		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? InstrumentName { get; set; }
		public string? InstrumentType { get; set; }
		public string? SponsorOrgName { get; set; }

		public string? CustodianName { get; set; }
		public string? CustodianCIF { get; set; }
		public string? TrusteeName { get; set; }
		public string? TrusteeCIF { get; set; }
	}

	public class ApproveTrusteeDto
	{
		public string? TrusteeIDs { get; set; }
		public string? ApprovalRemark { get; set; }
		public string? ApprovalStatus { get; set; }
	}


	public class ApproveCustodianDto
	{
		public string? CustodianIDs { get; set; }
		public string? ApprovalRemark { get; set; }
		public string? ApprovalStatus { get; set; }
	}

	public class CustodianTrusteeDto
	{

		public Nullable<int> CustodianID { get; set; }
		public Nullable<int> TrusteeID { get; set; }

		public Nullable<int> OrganizationID { get; set; } = 0;
		public string? ShortName { get; set; }
		public string? CPName { get; set; }
		public string? CPMobileNo { get; set; }
		public string? CPEmailAddress { get; set; }

		public string? DPCode { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> ApprovalReqSetID { get; set; }

		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }
		public string? OrganizationType { get; set; }
		public string? MobileNo { get; set; }

		public string? AltMobileNo { get; set; }
		public string? BIN { get; set; }
		public string? EmailAddress { get; set; }
		public Nullable<DateTime> IncorporationDate { get; set; }
		public Nullable<int> IndexID { get; set; }

		public string? LicIssuingAuthority { get; set; }
		public string? TelephoneNo { get; set; }
		public string? TIN { get; set; }
		public string? RegistrationNo { get; set; }
		public string? VATRegistrationNo { get; set; }
		public string? Address { get; set; }
	}

    public class ApproveInterestandFeesAccuredDTO
    {
        public string? FundIDs { get; set; }
        public string? ApprovalRemark { get; set; }
        public string? ApprovalStatus { get; set; }
    }
}
