using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AccountFundSettlement;
using Model.DTOs.AssetManager;
using Model.DTOs.CoA;
using Model.DTOs.ForeignTradeCommissionShare;
using Model.DTOs.MerchantBankAMCSettlementDto;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AccountingTradeSettlementController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<AccountingTradeSettlementController> _logger;
		public AccountingTradeSettlementController(IService service, ILogger<AccountingTradeSettlementController> logger)
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


		#region ForeignTradeCommissionShare

		[HttpGet("MakePaymentCommissionSharing/{CompanyID}/{BranchID}/{CommissionAllocationSummaryID}")]
		public async Task<IActionResult> MakePaymentCommissionSharing(int CompanyID, int BranchID,int CommissionAllocationSummaryID)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.MakePaymentCommissionSharing(LoggedOnUser(), CompanyID, BranchID, CommissionAllocationSummaryID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		
		[HttpGet("getCommissionSharingPayment/{CompanyID}/{BranchID}/{FromDate}/{ToDate}/{ListType}")]
		public async Task<IActionResult> getCommissionSharingPayment(int CompanyID, int BranchID,DateTime FromDate, DateTime ToDate, string ListType)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getCommissionSharingPayment(LoggedOnUser(), CompanyID, BranchID,FromDate, ToDate, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		[HttpPost("RecalculateForeignTradeCommissionSharing/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> RecalculateForeignTradeCommissionSharing(int CompanyID, int BranchID, List<ForeignTradeCommissionShareDto> data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.RecalculateForeignTradeCommissionSharing(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
		[HttpPost("saveForeignTradeCommissionSharing/{CompanyID}/{BranchID}/{AuditFee}/{Tax}/{BankFee}")]
		public async Task<IActionResult> saveForeignTradeCommissionSharing(int CompanyID, int BranchID, decimal AuditFee, decimal Tax, decimal BankFee, List<ForeignTradeCommissionShareDto> data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.saveForeignTradeCommissionSharing(LoggedOnUser(), CompanyID, BranchID,AuditFee, Tax, BankFee, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("getForeignTradeListForCommissionSharing/{CompanyID}/{BranchID}/{FromDate}/{ToDate}/{AccountNo}")]
		public async Task<IActionResult> getForeignTradeListForCommissionSharing(int CompanyID, int BranchID, DateTime FromDate, DateTime ToDate, string AccountNo)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getForeignTradeListForCommissionSharing(LoggedOnUser(), CompanyID, BranchID, FromDate, ToDate, AccountNo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion ForeignTradeCommissionShare



		#region IL-AML


		[HttpPost("SaveUpdateBrokerTradeSettlementDetail/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveUpdateBrokerTradeSettlementDetail(int CompanyID, int BranchID, MerchantBankAMCSettlementDto data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.SaveUpdateBrokerTradeSettlementDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("getBrokerTradeSettlementDetail/{CompanyID}/{BranchID}/{SettlementDate}/{ContractID}")]
		public async Task<IActionResult> getBrokerTradeSettlementDetail(int CompanyID, int BranchID, DateTime SettlementDate, int ContractID)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getBrokerTradeSettlementDetail(LoggedOnUser(), CompanyID, BranchID, SettlementDate, ContractID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("getBrokerTradeSettlementList/{CompanyID}/{BranchID}/{SettlementDate}")]
		public async Task<IActionResult> getBrokerTradeSettlementList(int CompanyID, int BranchID, DateTime SettlementDate)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getBrokerTradeSettlementList(LoggedOnUser(), CompanyID, BranchID, SettlementDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion IL-AML


		#region SL

		[HttpPost("SaveUpdateMerchantBankAMCTradeSettlementDetail/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveUpdateMerchantBankAMCTradeSettlementDetail(int CompanyID, int BranchID, MerchantBankAMCSettlementDto data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.SaveUpdateMerchantBankAMCTradeSettlementDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("getMerchantBankAMCTradeSettlementDetail/{CompanyID}/{BranchID}/{SettlementDate}/{ContractID}")]
		public async Task<IActionResult> getMerchantBankAMCTradeSettlementList(int CompanyID, int BranchID, DateTime SettlementDate, int ContractID)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getMerchantBankAMCTradeSettlementDetail(LoggedOnUser(), CompanyID, BranchID, SettlementDate, ContractID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("getMerchantBankAMCTradeSettlementList/{CompanyID}/{BranchID}/{SettlementDate}")]
		public async Task<IActionResult> getMerchantBankAMCTradeSettlementList(int CompanyID, int BranchID, DateTime SettlementDate)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getMerchantBankAMCTradeSettlementList(LoggedOnUser(), CompanyID, BranchID, SettlementDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		[HttpGet("getExchangeSettlementData/spot/{CompanyID}/{BranchID}/{ExchangeID}/{SettlementDate}")]
		public async Task<IActionResult> getExchangeSettlementData(int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getExchangeSpotSettlementData(LoggedOnUser(), CompanyID, BranchID, ExchangeID, SettlementDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("getExchangeSettlementData/regular/{CompanyID}/{BranchID}/{ExchangeID}/{SettlementDate}")]
		public async Task<IActionResult> getExchangeRegularSettlementData(int CompanyID, int BranchID, int ExchangeID, DateTime SettlementDate)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.getExchangeRegularSettlementData(LoggedOnUser(), CompanyID, BranchID, ExchangeID, SettlementDate));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("saveExchangeSettlementData/spot/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveExchangeSpotSettlementData(int CompanyID, int BranchID, ExchangeSpotSettlementDto data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.saveExchangeSpotSettlementData(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("saveExchangeSettlementData/regular/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveExchangeRegularSettlementData(int CompanyID, int BranchID, ExchangeRegularSettlementSetDto data)
		{
			try
			{
				return getResponse(await _service.accountingTradeSettlementRepository.saveExchangeRegularSettlementData(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		#endregion SL


	}
}
