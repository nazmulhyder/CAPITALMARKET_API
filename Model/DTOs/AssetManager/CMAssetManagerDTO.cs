using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.AssetManager
{
    public class AMLBrokerDto
    {
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public Nullable<int> AssetManagerID { get; set; }
        public Nullable<int> AMCBrokerID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public string? TradingCode { get; set; }
    }
    public class CMAssetManagerOrganisationDetailDTO
    {
        public Nullable<int> AssetManagerID { get; set; }

        public string? AssetManager { get; set; }
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
        public List<AssetManagerOrganisationContactPersonDto> ContactPersonList { get; set; }
        public List<AssetManagerBrokerListDto> BrokerList { get; set; }
        public List<AssetManagerBankAccountDto> BankList { get; set; }
    }
    public class AssetManagerOrganisationContactPersonDto
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
    public class AssetManagerBrokerListDto
    {

        public Nullable<int> AMCBrokerID { get; set; }
        public Nullable<int> AssetManagerID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public string? BrokerName { get; set; }
        public string? ActiveStatus { get; set; }
        public string? BrokerShortName { get; set; }

        public string? ClearingBOCSE { get; set; }
        public string? ClearingBODSE { get; set; } 
        public string? BrokerCodeCSE { get; set; }
        public string? BrokerCodeDSE { get; set; }

        public List<AMLBrokerDto>? AMLBrokerTradingCodes { get; set; }
    }

    public class AssetManagerBrokerDto
    {

        public Nullable<int> AMCBrokerID { get; set; }
        public Nullable<int> AssetManagerID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public string? BrokerName { get; set; }
        public string? ActiveStatus { get; set; }
        public string? BrokerShortName { get; set; }

        public string? ClearingBOCSE { get; set; }
        public string? ClearingBODSE { get; set; }
        public string? BrokerCodeCSE { get; set; }
        public string? BrokerCodeDSE { get; set; }

    }
    public class AssetManagerBankAccountDto
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
        public string? RoutingNo { get; set; }

    }
    public class AssetManagerListDto
    {
        public Nullable<int> AssetManagerID { get; set; }

        public string? AssetManager { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> IndexID { get; set; }
        public string? MailingAddress { get; set; }
        public string? Country { get; set; }

        public string? District { get; set; }
        public string? Division { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }

        public string? EmailAddress { get; set; }
        public string? WebAddress { get; set; }
        public Nullable<Int16> DistrictCode { get; set; }
        public Nullable<Int16> DivisionCode { get; set; }
        public Nullable<Int16> CountryCode { get; set; }

        public Nullable<int> TotalRowCount { get; set; }
    }
}
