using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AMLMF;
using Model.DTOs.CoA;
using Model.DTOs.FDR;
using Model.DTOs.FundOperation;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class FundOperationController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<FundOperationController> _logger;
		public FundOperationController(IService service, ILogger<FundOperationController> logger)
		{
			_service = service;
			_logger = logger;
		}

		private string LoggedOnUser()
		{
			var principal = HttpContext.User;
			_logger.LogInformation("Principal: {0}", principal.Identity.Name);
			var windowsIdentity = principal?.Identity as WindowsIdentity;
			string loggedOnUser = windowsIdentity.Name;
			if (loggedOnUser.Contains('\\'))
				loggedOnUser = loggedOnUser.Split("\\")[1];
			return loggedOnUser;
		}

		#region DUE_PAYMENT

		[HttpGet("get/AMLMF/GetFundDueListForPayment/{CompanyID}/{BranchID}/{FundID}/{AgreementChargeID}/{TenorStartDate}/{TenorEndDate}")]
		public async Task<IActionResult> GetFundDueListForPayment(int CompanyID, int BranchID, int FundID,int AgreementChargeID, DateTime TenorStartDate, DateTime TenorEndDate)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetFundDueListForPayment(LoggedOnUser(), CompanyID, BranchID, FundID, AgreementChargeID, TenorStartDate,TenorEndDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		
		[HttpPost("approve/AMLMF/FundDuePayment/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveFundDuePayment(int CompanyID, int BranchID, FundDuePaymenApproveDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveFundDuePayment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		
		}
		
		[HttpPost("save/AMLMF/DueTransactionList/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveAMLMFDuePayment(int CompanyID, int BranchID, AMLMFDuePaymentDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveAMLMFDuePayment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/AMLMF/GetFundDuePaymentDetail/{CompanyID}/{BranchID}/{ChrgDuePaymentID}")]
		public async Task<IActionResult> GetFundDuePaymentDetail(int CompanyID, int BranchID, int ChrgDuePaymentID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetFundDuePaymentDetail(LoggedOnUser(), CompanyID, BranchID, ChrgDuePaymentID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/AMLMF/GetFundDuePaymentList/{CompanyID}/{BranchID}/{FundID}/{StartDate}/{EndDate}/{ListType}")]
		public async Task<IActionResult> GetFundDuePaymentList(int CompanyID, int BranchID, int FundID, DateTime StartDate, DateTime EndDate, string ListType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetFundDuePaymentList(LoggedOnUser(), CompanyID, BranchID, FundID, StartDate, EndDate, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion DUEPAYMENT

		#region AdvancePaymentAndDue

		[HttpGet("get/AMLMF/ChargeForAdvancePaymentAndDue/List/{CompanyID}/{BranchID}/{FundID}/{ChargeMode}")]
		public async Task<IActionResult> GetFundChargeForDueAndAdvancePayment(int CompanyID, int BranchID, int FundID, string ChargeMode)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetFundChargeForDueAndAdvancePayment(LoggedOnUser(), CompanyID, BranchID, FundID, ChargeMode));
			}
			catch (Exception ex) { return getResponse(ex); }
		}




		[HttpPost("approve/AMLMFAdvancePayment/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAMLMFAdvancePayment(int CompanyID, int BranchID, AMLMFAdvPaymentApprovalDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveAMLMFAdvancePayment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/AMLMF/AdvancePayment/Detail/{CompanyID}/{BranchID}/{ChrgDuePaymentID}")]
		public async Task<IActionResult> AMLMFAdvancePaymentDetail(int CompanyID, int BranchID, int ChrgDuePaymentID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFAdvancePaymentDetail(LoggedOnUser(), CompanyID, BranchID, ChrgDuePaymentID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/AMLMF/AdvancePayment/List/{CompanyID}/{BranchID}/{listType}")]
		public async Task<IActionResult> ListAMLMFAdvancePayment(int CompanyID, int BranchID, string listType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListAMLMFAdvancePayment(LoggedOnUser(), CompanyID, BranchID, listType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("approve/SaveAMLMFAdvancePayment/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveAMLMFAdvancePayment(int CompanyID, int BranchID, AMLMFAdvPaymentDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveAMLMFAdvancePayment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		[HttpGet("get/AMLMF/AdvancePaymentProjectedCharge/{CompanyID}/{BranchID}/{AgreementChargeID}/{TenorStartDate}/{TenorEndDate}/{NavDate}/{FundID}")]
		public async Task<IActionResult> GetAMLMFAdvancePaymentProjectedCharge(int CompanyID, int BranchID, int AgreementChargeID, DateTime TenorStartDate, DateTime TenorEndDate, DateTime NavDate, int FundID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetAMLMFAdvancePaymentProjectedCharge(LoggedOnUser(), CompanyID, BranchID, AgreementChargeID, TenorStartDate, TenorEndDate, NavDate, FundID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/AMLMF/AdvancePaymentDetail/{CompanyID}/{BranchID}/{AgreementChargeID}")]
		public async Task<IActionResult> GetFundAdvancePaymentDetail(int CompanyID, int BranchID, int AgreementChargeID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetFundAdvancePaymentDetail(LoggedOnUser(), CompanyID, BranchID, AgreementChargeID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

	

		#endregion

		[HttpPost("Approve/AMLMFLockInInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAMLMFLockInInformation(int CompanyID, int BranchID, AMLMFLockInInformationApproveDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveAMLMFLockInInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("Save/AMLMFLockInInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveAMLMFLockInInformation(int CompanyID, int BranchID, AMLMFLockInInformationDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveAMLMFLockInInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("AMLMFLockInInformationDetailForNewEntry/{CompanyID}/{BranchID}/{FundID}/{InvestmentType}")]
		public async Task<IActionResult> AMLMFLockInInformationDetailForNewEntry(int CompanyID, int BranchID, int FundID, string InvestmentType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFLockInInformationDetailForNewEntry(LoggedOnUser(), CompanyID, BranchID, FundID, InvestmentType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
		[HttpGet("AMLMFLockInInformation/detail/{CompanyID}/{BranchID}/{LockInInformationID}")]
		public async Task<IActionResult> AMLMFLockInInformationDetailindividual(int CompanyID, int BranchID, int LockInInformationID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFLockInInformationDetail(LoggedOnUser(), CompanyID, BranchID, LockInInformationID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("List/AMLMFLockInInformation/{CompanyID}/{BranchID}/{listType}")]
		public async Task<IActionResult> ListAMLMFLockInInformation(int CompanyID, int BranchID, string listType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListAMLMFLockInInformation(LoggedOnUser(), CompanyID, BranchID, listType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#region NAV_Process

		[HttpGet("get/AMLMFNAVList/{CompanyID}/{BranchID}/{FundID}/{DateFrom}/{DateTo}")]
		public async Task<IActionResult> AMLMFNAVList(int CompanyID, int BranchID, int FundID, DateTime DateFrom, DateTime DateTo)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFNAVList(LoggedOnUser(), CompanyID, BranchID, FundID, DateFrom, DateTo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("process/AMLMFNAVatTransactionDate/{CompanyID}/{BranchID}/{FundID}/{TransactionDate}/{EffectiveDateTo}")]
		public async Task<IActionResult> SaveAMLMFNAVatTransactionDate(int CompanyID, int BranchID, int FundID, DateTime TransactionDate, DateTime EffectiveDateTo)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveAMLMFNAVatTransactionDate(LoggedOnUser(), CompanyID, BranchID, FundID, TransactionDate, EffectiveDateTo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		[HttpGet("get/AMLMF/NAVatTransactionDate/{CompanyID}/{BranchID}/{FundID}/{TransactionDate}")]
		public async Task<IActionResult> GetAMLMFNAVatTransactionDate(int CompanyID, int BranchID, int FundID, DateTime TransactionDate)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.GetAMLMFNAVatTransactionDate(LoggedOnUser(), CompanyID, BranchID, FundID, TransactionDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion NAV_Process


		[HttpPost("approve/AMLMFAmortizationSetup/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAMLMFAmortizationSetup(int CompanyID, int BranchID, AMLMFAmortizationSetupApproveDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveAMLMFAmortizationSetup(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/AMLMFAmortizationSetup/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveAMLMFAmortizationSetup(int CompanyID, int BranchID, AMLMFAmortizationSetupDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveAMLMFAmortizationSetup(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		[HttpGet("get/AMLMFAmortizationSetup/detail/{CompanyID}/{BranchID}/{FundID}/{IsShariah}")]
		public async Task<IActionResult> AMLMFAmortizationSetupDetail(int CompanyID, int BranchID, int FundID, bool IsShariah)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFAmortizationSetupDetail(LoggedOnUser(), CompanyID, BranchID, FundID, IsShariah));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/AMLMFAmortizationSetup/List/{CompanyID}/{BranchID}/{listtype}")]
		public async Task<IActionResult> ListAMLMFAmortizationSetup( int CompanyID, int BranchID, string listtype)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListAMLMFAmortizationSetup(LoggedOnUser(), CompanyID, BranchID, listtype));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		
		[HttpGet("get/AMLCCAAccountHead/List/{CompanyID}/{BranchID}/{listtype}")]
		public async Task<IActionResult> ListAMLCCAAccountHead( int CompanyID, int BranchID, string listtype)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListAMLCCAAccountHead(LoggedOnUser(), CompanyID, BranchID, listtype));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("ApproveAMLMFBAInterestAdjustment/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAMLMFBAInterestAdjustment(int CompanyID, int BranchID, AMLMFBAInterestAdjustmentApproveDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveAMLMFBAInterestAdjustment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/AMLMFBAInterestAdjustmentDetail/{CompanyID}/{BranchID}/{IntAdjustmentID}")]
		public async Task<IActionResult> AMLMFBAInterestAdjustmentDetail(int CompanyID, int BranchID, int IntAdjustmentID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.AMLMFBAInterestAdjustmentDetail(LoggedOnUser(), CompanyID, BranchID, IntAdjustmentID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/ListAMLMFBAInterestAdjustment/{CompanyID}/{BranchID}/{ListType}/{FundID}")]
		public async Task<IActionResult> ListAMLMFBAInterestAdjustment(int CompanyID, int BranchID, string ListType, int FundID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListAMLMFBAInterestAdjustment(LoggedOnUser(), CompanyID, BranchID, ListType, FundID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

        [HttpGet("get/ListAMLMFBAInterestAdjustmentReversal/{CompanyID}/{BranchID}/{ListType}/{FundID}")]
        public async Task<IActionResult> ListAMLMFBAInterestAdjustmentReversal(int CompanyID, int BranchID, string ListType, int FundID)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.ListAMLMFBAInterestAdjustmentReversal(LoggedOnUser(), CompanyID, BranchID, ListType, FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLMFBAInterestAdjustmentReversal/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AMLMFBAInterestAdjustmentReversal(int CompanyID, int BranchID, AMLMFBAInterestAdjustmentReversalDto entry)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.AMLMFBAInterestAdjustmentReversal(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AMLMFBAInterestAdjustmentReversalApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> AMLMFBAInterestAdjustmentReversalApproval(int CompanyID, int BranchID, AMLMFBAInterestAdjustmentReversalApprovalDto entry)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.AMLMFBAInterestAdjustmentReversalApproval(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("saveAMLMFBAInterestAdjustment/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveAMLMFBAInterestAdjustment(int CompanyID, int BranchID, AMLMFBAInterestAdjustmentDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.saveAMLMFBAInterestAdjustment(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("CollectAccrualInterestBankAccountMFAML/{CompanyID}/{BranchID}/{MFBankAccountID}")]
		public async Task<IActionResult> CollectAccrualInterestBankAccountMFAML(int CompanyID, int BranchID, int MFBankAccountID,  AMLIntrestAdjustmentCollectDto InterestData)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.CollectAccrualInterestBankAccountMFAML(LoggedOnUser(), CompanyID, BranchID, MFBankAccountID,  InterestData));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("MFAccrualInterestList/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> MFAccrualInterestList(int CompanyID, int BranchID, AMLIntrestAccualList data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.MFAccrualInterestList(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/MutualFundBankList/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> MutualFundBankList(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.MutualFundBankList(LoggedOnUser(), CompanyID, BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

        [HttpGet("get/MutualFundBankListByMFBankAccountID/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> MutualFundBankMFList(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.MutualFundBankMFList(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApproveMutualFund/setup/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveMutualFundDetail(int CompanyID, int BranchID, ApproveAMLMutualFundDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveMutualFundDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/ListMutualFund/setup/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> ListMutualFundDetail(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListMutualFundDetail(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		[HttpPost("saveMutualFund/setup/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveMutualFundDetail(int CompanyID, int BranchID, AMLMutualFundDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.saveMutualFundDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/MutualFund/setup/Detail/{CompanyID}/{BranchID}/{FundID}")]
		public async Task<IActionResult> getMutualFundDetail(int CompanyID, int BranchID, int FundID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.getMutualFundDetail(LoggedOnUser(), CompanyID, BranchID, FundID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("ApproveMutualFund/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveMutualFund(int CompanyID, int BranchID, MutualFundApproveDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveMutualFund(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("SaveMutualFund/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveMutualFund(int CompanyID, int BranchID, MutualFundDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveMutualFund(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/MutualFundDetail/{CompanyID}/{BranchID}/{FundID}")]
		public async Task<IActionResult> MutualFundDetail(int CompanyID, int BranchID, int FundID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.MutualFundDetail(LoggedOnUser(), CompanyID, BranchID, FundID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/ListMutualFund/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> ListMutualFund(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ListMutualFund(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}




		[HttpGet("get/CustodianInformationList/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> CustodianInformationList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.CustodianInformationList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/CustodianInformationDetail/{CompanyID}/{BranchID}/{organizationID}")]
		public async Task<IActionResult> CustodianInformationDetail(int CompanyID, int BranchID, int organizationID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.CustodianInformationDetail(LoggedOnUser(), CompanyID, BranchID, organizationID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("SaveCustodianInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveCustodianInformation(int CompanyID, int BranchID, CustodianTrusteeDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveCustodianInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("ApproveCustodianInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveCustodianInformation(int CompanyID, int BranchID, ApproveCustodianDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveCustodianInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/TrusteeInformationList/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> TrusteeInformationList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.TrusteeInformationList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/TrusteeInformationDetail/{CompanyID}/{BranchID}/{organizationID}")]
		public async Task<IActionResult> TrusteeInformationDetail(int CompanyID, int BranchID, int organizationID)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.TrusteeInformationDetail(LoggedOnUser(), CompanyID, BranchID, organizationID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("SaveTrusteeInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveTrusteeInformation(int CompanyID, int BranchID, CustodianTrusteeDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.SaveTrusteeInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("ApproveTrusteeInformation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveTrusteeInformation(int CompanyID, int BranchID, ApproveTrusteeDto data)
		{
			try
			{
				return getResponse(await _service.fundOperationRepository.ApproveTrusteeInformation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

        [HttpGet("AML_InterestandFeesAccuredInfo/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> InterestandFeesAccuredInfo(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.InterestandFeesAccuredInfo(LoggedOnUser(), CompanyID, BranchID, FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("AML_InterestandFeesAccuredSaved/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> GetInterestandFeesAccuredSaved(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                return getResponse(await _service.fundOperationRepository.GetInterestandFeesAccuredSaved(LoggedOnUser(), CompanyID, BranchID, FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
