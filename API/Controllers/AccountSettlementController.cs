using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AccountSettlement;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AccountSettlementController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<AccountSettlementController> _logger;
		public AccountSettlementController(IService service, ILogger<AccountSettlementController> logger)
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


		#region AccountClosure
		[HttpPost("BulkUploadAccountClosureValidation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountClosureValidation(int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountClosureValidation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("BulkUploadAccountClosureUpload/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountClosureUpload(int CompanyID, int BranchID,  List<AccountClosureReqDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountClosureUpload(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("ExecuteAccountClosure/{CompanyID}/{BranchID}/{ApprovalStatus}")]
		public async Task<IActionResult> ExecuteAccountClosure(int CompanyID, int BranchID, string ApprovalStatus, List<AccountClosureReqDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ExecuteAccountClosure(LoggedOnUser(), CompanyID, BranchID, ApprovalStatus, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("ListAccountClosure/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> ListAccountClosure(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ListAccountClosure(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("saveAccountClosure/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveAccountClosure(int CompanyID, int BranchID, IFormCollection data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.saveAccountClosure(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("GetAccountClosureDetail/{CompanyID}/{BranchID}/{AccountNo}/{AgrClosureReqID}")]
		public async Task<IActionResult> GetAccountClosureDetail(int CompanyID, int BranchID, string AccountNo,int AgrClosureReqID)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.GetAccountClosureDetail(LoggedOnUser(), CompanyID, BranchID, AccountNo, AgrClosureReqID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		#endregion AccountClosure





		#region withdrawal

		[HttpPost("ApproveAccountSuspensionWithdrawal/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAccountSuspensionWithdrawal(int CompanyID, int BranchID, AccountSuspensionWithdrawalApprovalDto data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ApproveAccountSuspensionWithdrawal(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("ListAccountSuspensionWithdrawalRequest/{CompanyID}/{BranchID}/{listtype}")]
		public async Task<IActionResult> ListAccountSuspensionWithdrawalRequest(int CompanyID, int BranchID,string listtype)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ListAccountSuspensionWithdrawalRequest(LoggedOnUser(), CompanyID, BranchID, listtype));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("saveAccountSuspensionWithdrawalRequest/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> saveAccountSuspensionWithdrawalRequest(int CompanyID, int BranchID, IFormCollection data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.saveAccountSuspensionWithdrawalRequest(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("ListAccountSuspensionForWithdrawalRequest/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ListAccountSuspensionForWithdrawal(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ListAccountSuspensionForWithdrawal(LoggedOnUser(), CompanyID, BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("BulkUploadAccountSuspensionWithdrawalValidation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountSuspensionWithdrawalValidation(int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountSuspensionWithdrawalValidation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("BulkUploadAccountSuspensionWithdrawal/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountSuspensionWithdrawal(int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountSuspensionWithdrawal(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion withdrawal




		#region Suspension

		[HttpGet("GetAccountSuspensionDetail/{CompanyID}/{BranchID}/{AccountNo}/{AgrSuspensionID}")]
		public async Task<IActionResult> GetAccountSuspensionDetail(int CompanyID, int BranchID, string AccountNo, int AgrSuspensionID)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.GetAccountSuspensionDetail(LoggedOnUser(), CompanyID, BranchID, AccountNo, AgrSuspensionID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("GetListAccountSuspensionSL/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> ListAccountSuspensionSL(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ListAccountSuspensionSL(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("SaveUpdateAccountSuspensionDetail/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetAccountSuspensionDetail(int CompanyID, int BranchID, IFormCollection data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.SaveUpdateAccountSuspensionDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
		[HttpPost("ApproveAccountSuspensionDetail/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAccountSuspensionDetail(int CompanyID, int BranchID, AccountSuspensionApprovalDto data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.ApproveAccountSuspensionDetail(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("BulkUploadAccountSuspensionValidation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountSuspensionValidation(int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountSuspensionValidation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("BulkUploadAccountSuspension/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> BulkUploadAccountSuspension(int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data)
		{
			try
			{
				return getResponse(await _service.accountSettlementRepository.BulkUploadAccountSuspension(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion Suspension
	}
}
