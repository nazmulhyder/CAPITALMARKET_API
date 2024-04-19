using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AMLMF;
using Model.DTOs.Approval;
using Model.DTOs.Charges;
using Model.DTOs.CoA;
using Model.DTOs.FDR;
using Model.DTOs.FundOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFundOperationRepository
	{
		public Task<object> AMLMFLockInInformationDetailForNewEntry(string UserName, int CompanyID, int BranchID, int FundID, string InvestmentType);

		#region DUE_PAYEMENT
		public Task<object> ApproveFundDuePayment(string UserName, int CompanyID, int BranchID, FundDuePaymenApproveDto data);

		public Task<object> GetFundDuePaymentList(string UserName, int CompanyID, int BranchID, int FundID,  DateTime TenorStartDate, DateTime TenorEndDate,  string ListType);
		
		public Task<string> SaveAMLMFDuePayment(string UserName, int CompanyID, int BranchID, AMLMFDuePaymentDto data);
		
		public Task<object> GetFundDuePaymentDetail(string UserName, int CompanyID, int BranchID, int ChrgDuePaymentID);

		public Task<object> GetFundDueListForPayment(string UserName, int CompanyID, int BranchID, int FundID, int AgreementChargeID, DateTime TenorStartDate,DateTime TenorEndDate);


		#endregion DUE_PAYEMENT

		public Task<object> GetFundChargeForDueAndAdvancePayment(string UserName, int CompanyID, int BranchID, int FundID, string ChargeMode);

		#region Advpayment
		public Task<string> ApproveAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, AMLMFAdvPaymentApprovalDto data);

		public Task<object> AMLMFAdvancePaymentDetail(string UserName, int CompanyID, int BranchID, int ChrgDuePaymentID);

		public Task<object> ListAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, string listType);

		public Task<string> SaveAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, AMLMFAdvPaymentDto data);

		public Task<object> GetAMLMFAdvancePaymentProjectedCharge(string UserName, int CompanyID, int BranchID, int LastChrgDuePaymentID, DateTime TenorStartDate, DateTime TenorEndDate, DateTime NavDate, int FundID);

		public Task<object> GetFundAdvancePaymentDetail(string UserName, int CompanyID, int BranchID, int AgreementChargeID);

		#endregion

		public Task<string> ApproveAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, AMLMFLockInInformationApproveDto data);

		public Task<string> SaveAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, AMLMFLockInInformationDto data);

		public Task<object> AMLMFLockInInformationDetail(string UserName, int CompanyID, int BranchID, int LockInInformationID);

		public Task<object> ListAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, string listType);

		#region NAV_PROCESS

		public Task<object> AMLMFNAVList(string UserName, int CompanyID, int BranchID,int FundID, DateTime DateFrom, DateTime DateTo);

		public Task<string> SaveAMLMFNAVatTransactionDate(string UserName, int CompanyID, int BranchID,int FundID, DateTime TransactionDate, DateTime EffectiveDateTo);

		public Task<object> GetAMLMFNAVatTransactionDate(string UserName, int CompanyID, int BranchID, int FundID, DateTime TransactionDate);


		#endregion


	

		#region AMLMFAmortizationSetup

		public Task<string> ApproveAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, AMLMFAmortizationSetupApproveDto data);

		public Task<string> SaveAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, AMLMFAmortizationSetupDto data);

		public Task<object> AMLMFAmortizationSetupDetail(string UserName, int CompanyID, int BranchID, int MFASetupID, bool IsShariah);

		public Task<object> ListAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, string listtype);

		#endregion

		public Task<object> ListAMLCCAAccountHead(string UserName, int CompanyID, int BranchID, string listtype);


		public Task<string> ApproveAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, AMLMFBAInterestAdjustmentApproveDto data);

		public Task<object> AMLMFBAInterestAdjustmentDetail(string UserName, int CompanyID, int BranchID, int IntAdjustmentID);

		public Task<object> ListAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, string ListType,int FundID);
        public Task<object> ListAMLMFBAInterestAdjustmentReversal(string UserName, int CompanyID, int BranchID, string ListType, int FundID);
        public Task<string> AMLMFBAInterestAdjustmentReversal(int CompanyID, int BranchID, string userName, AMLMFBAInterestAdjustmentReversalDto entry);
        public Task<string> AMLMFBAInterestAdjustmentReversalApproval(int CompanyID, int BranchID, string userName, AMLMFBAInterestAdjustmentReversalApprovalDto entry);

        public Task<string> saveAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, AMLMFBAInterestAdjustmentDto data);

		public Task<string> CollectAccrualInterestBankAccountMFAML(string UserName, int CompanyID, int BranchID,int MFBankAccountID,  AMLIntrestAdjustmentCollectDto InterestData);

		public Task<object> MFAccrualInterestList(string UserName, int CompanyID, int BranchID, AMLIntrestAccualList data);
		
		public Task<List<AMLFundDto>> MutualFundBankList(string UserName, int CompanyID, int BranchID);
        public Task<List<AMLFundMFDto>> MutualFundBankMFList(string UserName, int CompanyID, int BranchID);

        public Task<string> ApproveMutualFundDetail(string UserName, int CompanyID, int BranchID, ApproveAMLMutualFundDto data);

		public Task<object> ListMutualFundDetail(string UserName, int CompanyID, int BranchID, string ListType);

		public Task<string> saveMutualFundDetail(string UserName, int CompanyID, int BranchID, AMLMutualFundDto data);

		public Task<object> getMutualFundDetail(string UserName, int CompanyID, int BranchID, int FundID);

		public Task<string> ApproveMutualFund(string UserName, int CompanyID, int BranchID, MutualFundApproveDto data);

		public Task<string> SaveMutualFund(string UserName, int CompanyID, int BranchID, MutualFundDto data);

		public Task<object> MutualFundDetail(string UserName, int CompanyID, int BranchID, int FundID);

		public Task<object> ListMutualFund(string UserName, int CompanyID, int BranchID, string ListType);
		
        public Task<object> CustodianInformationList(string UserName, int CompanyID, int BranchID, string ListType);

        public Task<object> CustodianInformationDetail(string UserName, int CompanyID, int BranchID, int organizationID);

        public Task<string> SaveCustodianInformation(string UserName, int CompanyID, int BranchID, CustodianTrusteeDto data);

        public Task<string> ApproveCustodianInformation(string UserName, int CompanyID, int BranchID, ApproveCustodianDto data);

		public Task<object> TrusteeInformationList(string UserName, int CompanyID, int BranchID, string ListType);

		public Task<object> TrusteeInformationDetail(string UserName, int CompanyID, int BranchID, int organizationID);

		public Task<string> SaveTrusteeInformation(string UserName, int CompanyID, int BranchID, CustodianTrusteeDto data);

		public Task<string> ApproveTrusteeInformation(string UserName, int CompanyID, int BranchID, ApproveTrusteeDto data);
		public Task<object> InterestandFeesAccuredInfo(string UserName, int CompanyID, int BranchID, int FundID);
		public Task<string> GetInterestandFeesAccuredSaved(string UserName, int CompanyID, int BranchID, int FundID);

    }
}
