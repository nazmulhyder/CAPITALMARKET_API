using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BrokerageCommision
{
	public class BrokerageCommisionApproveDto
    {
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int ContractID { get; set; }
        public string? ApprovalStatus { get; set; }
		public string? ApprovalFeedback { get; set; }
		public string? UserName { get; set; }
    }

	public class BrokerageCommisionDto
    {
        public string? ProductName { get; set; }

        public string? AccountNoPrefix { get; set; }
        public string? CompanyID { get; set; }

		public string? CompanyName { get; set; }
		public Nullable<int> IndexID { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }

		public string? AccountNumber { get; set; }
		public Nullable<int> ProductID { get; set; }
		public Nullable<int> AccountGroupID { get; set; }
		public string? AccountGroupName { get; set; }

		public Nullable<int> ContractID { get; set; }
		public string? ApprovalStatus { get; set; }
		public Nullable<int> BCEquityIncomeAgreementChargeID { get; set; }
		public Nullable<decimal> BCEquityIncome { get; set; }
		public string? BCEquityIncomeDerivedFrom { get; set; }


		public Nullable<decimal> BCEquityIncomePrevChargeAmount { get; set; }
		public string? BCEquityIncomePrevDerivedFrom { get; set; }


		public Nullable<bool> BCEquityIncomeHasSlab { get; set; }
		public Nullable<bool> BCEquityIncomeSlabIsModified { get; set; }
		public Nullable<int> BCEquityPayableAgreementChargeID { get; set; }
		public Nullable<decimal> BCEquityPayable { get; set; }
		public string? BCEquityPayableDerivedFrom { get; set; }

		public Nullable<decimal> BCEquityPayablePrevChargeAmount { get; set; }
		public string? BCEquityPayablePrevDerivedFrom { get; set; }

		public Nullable<bool> BCEquityPayableHasSlab { get; set; }
		public Nullable<bool> BCEquityPayableSlabIsModified { get; set; }
		public Nullable<int> BCBondIncomeAgreementChargeID { get; set; }
		public Nullable<decimal> BCBondIncome { get; set; }

		public string? BCBondIncomeDerivedFrom { get; set; }

		public Nullable<decimal> BCBondIncomePrevChargeAmount { get; set; }
		public string? BCBondIncomePrevDerivedFrom { get; set; }

		public Nullable<bool> BCBondIncomeHasSlab { get; set; }
		public Nullable<bool> BCBondIncomeSlabIsModified { get; set; }
		public Nullable<int> BCBondPayableAgreementChargeID { get; set; }
		public Nullable<decimal> BCBondPayable { get; set; }
		public string? BCBondPayableDerivedFrom { get; set; }

		public Nullable<decimal> BCBondPayablePrevChargeAmount { get; set; }
		public string? BCBondPayablePrevDerivedFrom { get; set; }

		public Nullable<bool> BCBondPayableHasSlab { get; set; }
		public Nullable<bool> BCBondPayableSlabIsModified { get; set; }
		public Nullable<int> BCMutualFundIncomeAgreementChargeID { get; set; }

		public Nullable<decimal> BCMutualFundIncome { get; set; }
		public string? BCMutualIncomeFundDerivedFrom { get; set; }

		public Nullable<decimal> BCMutualFundIncomePrevChargeAmount { get; set; }
		public string? BCMutualFundIncomePrevDerivedFrom { get; set; }

		public Nullable<bool> BCMutualFundIncomeHasSlab { get; set; }
		public Nullable<bool> BCMutualFundIncomeSlabIsModified { get; set; }
		public Nullable<int> BCMutualFundPayableAgreementChargeID { get; set; }
		public Nullable<decimal> BCMutualFundPayable { get; set; }
		public string? BCMutualPayableFundDerivedFrom { get; set; }

		public Nullable<decimal> BCMutualFundPayablePrevChargeAmount { get; set; }
		public string? BCMutualFundPayablePrevDerivedFrom { get; set; }

		public Nullable<bool> BCMutualFundPayableHasSlab { get; set; }
		public Nullable<bool> BCMutualFundPayableSlabIsModified { get; set; }
		public Nullable<int> BCGsecIncomeAgreementChargeID { get; set; }
		public Nullable<decimal> BCGsecIncome { get; set; }
		public string? BCGsecIncomeDerivedFrom { get; set; }

		public Nullable<decimal> BCGsecIncomePrevChargeAmount { get; set; }
		public string? BCGsecIncomePrevDerivedFrom { get; set; }

		public Nullable<bool> BCGsecIncomeHasSlab { get; set; }
		public Nullable<bool> BCGsecIncomeSlabIsModified { get; set; }
		public Nullable<int> BCGsecPayableAgreementChargeID { get; set; }
		public Nullable<decimal> BCGsecPayable { get; set; }

		public string? BCGsecPayableDerivedFrom { get; set; }

		public Nullable<decimal> BCGsecPayablePrevChargeAmount { get; set; }

		public string? BCGsecPayablePrevDerivedFrom { get; set; }
		public Nullable<bool> BCGsecPayableHasSlab { get; set; }
		public Nullable<bool> BCGsecPayableSlabIsModified { get; set; }
		public Nullable<int> TotalRowCount { get; set; }

	}


	public class BrokerageCommisionItemDto
	{
		public string? CompanyID { get; set; }

		public string? CompanyName { get; set; }
		public Nullable<int> IndexID { get; set; }
		public string? MemberCode { get; set; }
		public string? MemberName { get; set; }
		public string? MemberType { get; set; }

		public string? AccountNumber { get; set; }
		public Nullable<int> ProductID { get; set; }
		public string? ProductName { get; set; }
		public Nullable<int> AccountGroupID { get; set; }
		public string? AccountGroupName { get; set; }

		public Nullable<int> ContractID { get; set; }
		public string? ApprovalStatus { get; set; }

		public string? PanelBrokerName { get; set; }

		public List<BrokerageCommisionItemRateDto>? ChargeList { get; set; } 
		
	}

	public class BrokerageCommisionItemRateUpdateDto
	{
		public Nullable<int> AgreementChargeID { get; set; }

		public string? AttributeName { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
		public string? DerivedFrom { get; set; }
		public Nullable<Boolean> HasSlab { get; set; }

	}
	public class BrokerageCommisionItemRateDto
	{
		public Nullable<int> AgreementChargeID { get; set; }

		public string? AttributeName { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
		public string? DerivedFrom { get; set; }

		public Nullable<decimal> PrevChargeAmount { get; set; }
		public string? PrevDerivedFrom { get; set; }

		public Nullable<Boolean> HasSlab { get; set; }

		public Nullable<decimal> ProductSettingChargeAmount { get; set; }
		public Nullable<decimal> AccountGroupLevelChargeAmount { get; set; }
		public Nullable<decimal> ILPanelBrokerEquityCommission { get; set; }
		public List<BrokerageCommisionItemSlabDto> SlabList { get; set; } = null;
	}

	public class BrokerageCommisionItemSlabDto
    {
        public int SlabID { get; set; }
		public Nullable<int>  SlNo { get; set; }
        public Nullable<decimal> MinValue { get; set; }
		public Nullable<decimal> PrevMinValue { get; set; }
		public Nullable<decimal> MaxValue { get; set; }
		public Nullable<decimal> PrevMaxValue { get; set; }
		public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public int AgreementChargeID { get; set; }
	}
}
