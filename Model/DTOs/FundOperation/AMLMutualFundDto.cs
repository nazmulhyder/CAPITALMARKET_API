using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.FundOperation
{
	public class ApproveAMLMutualFundDto
	{
        public string? ApprovalStatus { get; set; }
        public string? ApprovalRemark { get; set; }
        public string? FundIDs { get; set; }
    }
	public class AMLMutualFundDto
	{
		public Nullable<int> FundID { get; set; }

		public string? FundName { get; set; }
		public string? ShortName { get; set; }
		public string? FundType { get; set; }
		public Nullable<int> LotSize { get; set; }
		public Nullable<decimal> FaceValue { get; set; }

		public string? FundStatus { get; set; }
		public string? FundApprovalStatus { get; set; }
		public Nullable<int> FundApprovalReqSetID { get; set; }
		public string? FundMaker { get; set; }
		public string? FundMakeDate { get; set; }

		public Nullable<DateTime> FundApprovalDate { get; set; }
		public string? FundApprovalRemarks { get; set; }
		public string? FundApprover { get; set; }
		public Nullable<int> MFDetailID { get; set; }
		public Nullable<int> SponsorOrgID { get; set; }

		public string? SponsorOrgName { get; set; }
		public string? SponsorOrgCIF { get; set; }
		public Nullable<int> CustodianID { get; set; }
		public Nullable<int> CustodianOrganizationID { get; set; }
		public string? CustodianCIF { get; set; }

		public string? CustodianName { get; set; }
		public Nullable<int> TrusteeID { get; set; }
		public Nullable<int> TrusteeOrganizationID { get; set; }
		public string? TrusteeCIF { get; set; }
		public string? TrusteeName { get; set; }

		public Nullable<decimal> SponsorContribution { get; set; }
		public string? TDRegistrationDate { get; set; }
		public string? FormationDate { get; set; }
		public string? IPOSubsStartDate { get; set; }
		public string? IPOSubsEndDate { get; set; }

		public Nullable<decimal> PreIPOSubsFund { get; set; }
		public Nullable<decimal> IPOSubsFund { get; set; }
		public Nullable<decimal> NewIssuedFund { get; set; }
		public string? RegistrationNo { get; set; }
		public Nullable<decimal> FundSize { get; set; }

		public Nullable<int> ContractID { get; set; }
		public Nullable<int> NonSipProductID { get; set; }
		public Nullable<int> SipProductID { get; set; }
		public Nullable<int> IndexID { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? InstrumentName { get; set; }
		public string? ISIN { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountType { get; set; }

		public string? AccountApprovalStatus { get; set; }
		public string? AccountMaker { get; set; }
		public string? AccountMakeDate { get; set; }
		public string? AccountStatus { get; set; }

		public Nullable<int> ApprovalReqSetID { get; set; }
		public string? SetupApprovalStatus { get; set; }
		public string? SetupMaker { get; set; }
		public string? SetupMakeDate { get; set; }

		public Nullable<DateTime> SetupApprovalDate { get; set; }
		public string? SetupApprovalRemarks { get; set; }
		public string? SetupApprover { get; set; }

		public List<FundProductDto> ProductList { get; set; }
        public List<FundBankAccountDto> BankAccountList { get; set; }
        public List<FundChargeDto> FundChargeList { get; set; }
		public List<FundInvestmentRuleDto> InvestmentRuleAll { get; set; }
		public List<FundInvestmentRuleDto> FundInvestmentRuleList { get; set; }
		public List<FundLedgerHeadDto> LedgerHeadList { get; set; }
    }

	public class FundLedgerHeadDto
	{
		public Nullable<int> LedgerHeadID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public string? BalanceType { get; set; }
		public string? ControlHead { get; set; }

	}

	public class AMLMutualFundEntityDto
	{
		public Nullable<int> FundID { get; set; }

		public string? FundName { get; set; }
		public string? ShortName { get; set; }
		public string? FundType { get; set; }
		public Nullable<int> LotSize { get; set; }
		public Nullable<decimal> FaceValue { get; set; }

		public string? FundStatus { get; set; }
		public string? FundApprovalStatus { get; set; }
		public Nullable<int> FundApprovalReqSetID { get; set; }
		public string? FundMaker { get; set; }
		public string? FundMakeDate { get; set; }

		public Nullable<DateTime> FundApprovalDate { get; set; }
		public string? FundApprovalRemarks { get; set; }
		public string? FundApprover { get; set; }
		public Nullable<int> MFDetailID { get; set; }
		public Nullable<int> SponsorOrgID { get; set; }

		public string? SponsorOrgName { get; set; }
		public string? SponsorOrgCIF { get; set; }
		public Nullable<int> CustodianID { get; set; }
		public Nullable<int> CustodianOrganizationID { get; set; }
		public string? CustodianCIF { get; set; }

		public string? CustodianName { get; set; }
		public Nullable<int> TrusteeID { get; set; }
		public Nullable<int> TrusteeOrganizationID { get; set; }
		public string? TrusteeCIF { get; set; }
		public string? TrusteeName { get; set; }

		public Nullable<decimal> SponsorContribution { get; set; }
		public string? TDRegistrationDate { get; set; }
		public string? FormationDate { get; set; }
		public string? IPOSubsStartDate { get; set; }
		public string? IPOSubsEndDate { get; set; }

		public Nullable<decimal> PreIPOSubsFund { get; set; }
		public Nullable<decimal> IPOSubsFund { get; set; }
		public Nullable<decimal> NewIssuedFund { get; set; }
		public string? RegistrationNo { get; set; }
		public Nullable<decimal> FundSize { get; set; }

		public Nullable<int> ContractID { get; set; }
		public Nullable<int> NonSipProductID { get; set; }
		public Nullable<int> SipProductID { get; set; }
		public Nullable<int> IndexID { get; set; }
		public Nullable<int> InstrumentID { get; set; }
		public string? InstrumentName { get; set; }
		public string? ISIN { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountType { get; set; }

		public string? AccountApprovalStatus { get; set; }
		public string? AccountMaker { get; set; }
		public string? AccountMakeDate { get; set; }
		public string? AccountStatus { get; set; }

		public Nullable<int> ApprovalReqSetID { get; set; }
		public string? SetupApprovalStatus { get; set; }
		public string? SetupMaker { get; set; }
		public string? SetupMakeDate { get; set; }

		public Nullable<DateTime> SetupApprovalDate { get; set; }
		public string? SetupApprovalRemarks { get; set; }
		public string? SetupApprover { get; set; }
	}

	public class FundInvestmentRuleDto
	{
		public Nullable<int> ProductID { get; set; }
		public Nullable<int> ProductAttributeID { get; set; }

		public Nullable<int> AttributeID { get; set; }
		public string? AttributeName { get; set; }
		public string? ValueType { get; set; }
		public string? Condition { get; set; }
		public string? AttributeValue { get; set; }
		public string? DeletedStatus { get; set; }
		public int? AgreementParamID { get; set; }
		public int? ContractID { get; set; }
		public string? ParamValue { get; set; }
		public string? DerivedFrom { get; set; }

	}

	public class FundChargeDto
	{
		public Nullable<int> ContractID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public Nullable<int> AttributeID { get; set; }
		public Nullable<int> ProductAttributeID { get; set; }
		public string? AttributeName { get; set; }
		public string? ValueType { get; set; }

		public string? Condition { get; set; }
		public string? AttributeValue { get; set; }
		public string? ChargeApprovalStatus { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
		public string? DerivedFrom { get; set; }

		public string? ChargeDeletedStatus { get; set; }
		public Nullable<Boolean> HasSlab { get; set; }
		public string? ChargeMaker { get; set; }
		public Nullable<DateTime> ChargeMakeDate { get; set; }
		public Nullable<decimal> PrevChargeAmount { get; set; }

		public string? PrevDerivedFrom { get; set; }
		public string? CalculationFrequency { get; set; }
		public string? ChargeMode { get; set; }
		public string? FeeCap { get; set; }
		public string? FeeCapPeriodicity { get; set; }
		public string? CalculationBasis { get; set; }
		public string? ChargeFrequency { get; set; }
		public decimal? Tax { get; set; }
		public decimal? Vat { get; set; }
		public int? AgreementChargeID { get; set; }
		public List<FundChargeSlabDto>? SlabList { get; set; }
	}

	public class FundChargeEntityDto
	{
		public Nullable<int> ContractID { get; set; }

		public Nullable<int> ProductID { get; set; }
		public Nullable<int> AttributeID { get; set; }
		public Nullable<int> ProductAttributeID { get; set; }
		public string? AttributeName { get; set; }
		public string? ValueType { get; set; }

		public string? Condition { get; set; }
		public string? AttributeValue { get; set; }
		public string? ChargeApprovalStatus { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
		public string? DerivedFrom { get; set; }

		public string? ChargeDeletedStatus { get; set; }
		public Nullable<Boolean> HasSlab { get; set; }
		public string? ChargeMaker { get; set; }
		public Nullable<DateTime> ChargeMakeDate { get; set; }
		public Nullable<decimal> PrevChargeAmount { get; set; }

		public string? PrevDerivedFrom { get; set; }
		public string? CalculationFrequency { get; set; }
		public string? ChargeMode { get; set; }
		public string? FeeCap { get; set; }
		public string? FeeCapPeriodicity { get; set; }
		public string? CalculationBasis { get; set; }
		public string? ChargeFrequency { get; set; }
		public decimal? Tax { get; set; }
		public decimal? Vat { get; set; }
		public int? AgreementChargeID { get; set; }
	}

	public class FundChargeSlabDto
	{
		public Nullable<int> SlabID { get; set; }

		public string? SlNo { get; set; }
		public Nullable<decimal> MinValue { get; set; }
		public Nullable<decimal> PrevMinValue { get; set; }
		public Nullable<decimal> MaxValue { get; set; }
		public Nullable<decimal> PrevMaxValue { get; set; }

		public Nullable<decimal> ChargeAmount { get; set; }
		public Nullable<decimal> PrevChargeAmount { get; set; }
		public Nullable<int> AgreementChargeID { get; set; }
        public string? ReferenceKey { get; set; }
    }

	public class FundBankAccountDto
	{
		public Nullable<int> MFBankAccountID { get; set; } = 0;

		public string? BankAccountNo { get; set; }
		public string? BankAccountName { get; set; }
		public string? BankAccountType { get; set; }
		public Nullable<decimal> InterestRate { get; set; }
		public Nullable<int> BankOrgID { get; set; }

		public Nullable<int> BankOrgBranchID { get; set; }
		public string? InterestStartDate { get; set; }
		public Nullable<int> FundID { get; set; }
		public Nullable<int> LedgerHeadID { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? BankOrgName { get; set; }
		public string? BranchName { get; set; }
		public string? RoutingNo { get; set; }
        public bool? IsActive { get; set; }
        public Nullable<int> ProductID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public string? BalanceType { get; set; }
		public string? ControlHead { get; set; }
		public bool? HasSlab { get; set; }
		public decimal? AIT { get; set; }
		public string? Modules { get; set; }

		public List<SalListDto>? SlabList { get; set; } = new List<SalListDto> { };

	}

	public class FundBankAccountTypeTableDto
	{
		public Nullable<int> MFBankAccountID { get; set; } = 0;

		public string? BankAccountNo { get; set; }
		public string? BankAccountName { get; set; }
		public string? BankAccountType { get; set; }
		public Nullable<decimal> InterestRate { get; set; }
		public Nullable<int> BankOrgID { get; set; }

		public Nullable<int> BankOrgBranchID { get; set; }
		public string? InterestStartDate { get; set; }
		public Nullable<int> FundID { get; set; }
		public Nullable<int> LedgerHeadID { get; set; }

		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }
		public string? BankOrgName { get; set; }
		public string? BranchName { get; set; }
		public string? RoutingNo { get; set; }
		public bool? IsActive { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? GLCode { get; set; }
		public string? GLName { get; set; }
		public string? BalanceType { get; set; }
		public string? ControlHead { get; set; }
		public bool? HasSlab { get; set; }
		public decimal? AIT { get; set; }
		public string? Modules { get; set; }


	}

	public class SalListDto
	{
		public int? MFBankAccountID { get; set; } = 0;
		public int? SlabID { get; set; } = 0;
		public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public decimal? ChargeAmount { get; set; }
        public string? ReferenceKey { get; set; }
    }

	public class FundProductDto
	{
		public Nullable<int> MFProductLinkID { get; set; }
        public string? ProductName { get; set; }
        public Nullable<int> ProductID { get; set; }
		public Nullable<int> FundID { get; set; }
		public Nullable<Boolean> IsNonSip { get; set; }
		public string? Maker { get; set; }
		public Nullable<DateTime> MakeDate { get; set; }

	}



}
