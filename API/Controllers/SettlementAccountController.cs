using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.SettlementAccount;
using Service.Interface;
using System.Diagnostics.Contracts;
using System.Security.Principal;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class SettlementAccountController : ControllerBase
	{
		private readonly ILogger<SettlementAccountController> _logger;
		private readonly IService _service;

		public SettlementAccountController(ILogger<SettlementAccountController> logger, IService service)
		{
			_logger = logger;
			_service = service;
		}

		private string LoggedOnUser()
		{
			var principal = HttpContext.User;
			_logger.LogInformation("Principal: {0}", principal.Identity.Name);
			var windowsIdentity = principal?.Identity as WindowsIdentity;
			//var userName = WindowsIdentity.GetCurrent().Name;
			string loggedOnUser = windowsIdentity.Name;
			if (loggedOnUser.Contains('\\'))
				loggedOnUser = loggedOnUser.Split("\\")[1];
			return loggedOnUser;
		}

		[HttpPost("SaveUpdateSettlementAccount/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveUpdateSettlementAccount(int CompanyID, int BranchID, SettlementAccountDto data)
		{
			try
			{
				return getResponse(await _service.settlementAccountRepository.SaveUpdateSettlementAccount(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("GetSettlementAccountList/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> GetSettlementAccountList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.settlementAccountRepository.GetSettlementAccountList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

			[HttpGet("GetProductSettlementAccount/{CompanyID}/{BranchID}/{ProductID}/{ContractID}")]
		public async Task<IActionResult> GetProductSettlementAccount(int CompanyID, int BranchID, int ProductID, int ContractID)
		{
			try
			{
				return getResponse(await _service.settlementAccountRepository.GetProductSettlementAccount(LoggedOnUser(), CompanyID, BranchID, ProductID, ContractID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

	}
}
