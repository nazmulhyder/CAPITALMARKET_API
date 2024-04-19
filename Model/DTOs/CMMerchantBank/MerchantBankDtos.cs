using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.CMMerchantBank
{
    public class MerchantBankListDto
    {
        public int? TotalRowCount { get; set; }
        public Nullable<int> MerchantBankID { get; set; }

        public string? MerchantBankName { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public string? OrganizationType { get; set; }
        public Nullable<int> IndexID { get; set; }

        public string? TIN { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? WebAddress { get; set; }

        public string? EmailAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? AltMobileNo { get; set; }
        public string? TelephoneNo { get; set; }
    }
    public class MerchantBankOraganisationDetailDto
    {

        public Nullable<int> MerchantBankID { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public Nullable<int> IndexID { get; set; }
        public string? OrganizationName { get; set; }
        public Nullable<DateTime> IncorporationDate { get; set; }
        public string? LegalStatus { get; set; }

        public string? TIN { get; set; }
        public Nullable<DateTime> CommencementDate { get; set; }
        public string? OrganizationType { get; set; }
        public string? TradeLicenseNo { get; set; }
        public Nullable<DateTime> LicenseIssueDate { get; set; }

        public string? LicIssuingAuthority { get; set; }
        public string? IncorporationCountry { get; set; }
        public string? RegistrationNo { get; set; }
        public Nullable<DateTime> RegistrationDate { get; set; }
        public string? RegistrationAuthority { get; set; }

        public string? RegistrationCountry { get; set; }
        public Nullable<Boolean> IsListedonSE { get; set; }
        public string? MobileNo { get; set; }
        public string? TelephoneNo { get; set; }
        public string? FAX { get; set; }

        public string? AltMobileNo { get; set; }
        public string? EmailAddress { get; set; }
        public string? WebAddress { get; set; }
        public string? Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }

        public Nullable<int> ApprovalReqSetID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? BIN { get; set; }
        public string? VATRegistrationNo { get; set; }
        public string? BusComCertNo { get; set; }

        public Nullable<DateTime> BusComDate { get; set; }
        public string? LEI { get; set; }
        public string? DSEClassification { get; set; }

        public List<MerchantBankOraganisationMembershipDto> ContactPersonList { get; set; }
        public List<MerchanntBankPanelBrokerDto> PanelBrokerList { get; set; }

        public List<MerchanntBankBankAccountDto> BankAccountList { get; set; }
    }
    public class MerchantBankOraganisationMembershipDto
    {
        public Nullable<int> MemberLEID { get; set; }

        public Nullable<int> IndexID { get; set; }
        public string? LERelationshipType { get; set; }
        public string? Name { get; set; }
        public string? RelationshipDetail { get; set; }
        public string? PhotoDocName { get; set; }

        public string? PhotoDocPath { get; set; }
        public string? SignatureDocName { get; set; }
        public string? SignatureDocPath { get; set; }
        public Nullable<int> LEIndexID { get; set; }
        public string? DeletedStatus { get; set; }

        public string? Designation { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailAddress { get; set; }
    }
    public class MerchanntBankPanelBrokerDto
    {
        public string? BrokerName { get; set; }

        public string? BrokerShortName { get; set; }
        public Nullable<int> PanelBrokerID { get; set; }
        public string? ActiveStatus { get; set; }
        public Nullable<int> MerchantBankID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public Nullable<decimal> BrokerageCommission { get; set; }
    }
    public class MerchanntBankBankAccountDto
    {
        public Nullable<Boolean> IsSelectedBankAccount { get; set; }

        public Nullable<int> BankAccountID { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }

        public string? BranchName { get; set; }
        public string? BranchAddressLine1 { get; set; }
        public string? BranchAddressLine2 { get; set; }
        public string? BranchAddressLine3 { get; set; }
        public string? BranchAddressType { get; set; }

    }
}
