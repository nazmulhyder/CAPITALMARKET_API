using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CoA;
using Model.DTOs.VoucherTemplate;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class VoucherTemplateController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<VoucherTemplateController> _logger;
		public VoucherTemplateController(IService service, ILogger<VoucherTemplateController> logger)
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

		[HttpPost("save/update/AccVoucherTmplateLink/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> InsertUpdateAccVoucherTmplateLink(int CompanyID, int BranchID, List<VoucherTempleteLinkDto> data)
		{
			try
			{
				return getResponse(await _service.voucherTemplateRepository.InsertUpdateAccVoucherTmplateLink(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/update/Ledgerhead/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveUpdateLedgerTemplate(int CompanyID, int BranchID, VoucherLedgerheadDto LedgerHead)
		{
			try
			{
				return getResponse(await _service.voucherTemplateRepository.SaveUpdateLedgerTemplate(LoggedOnUser(), CompanyID, BranchID, LedgerHead));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("List/GetAllAccVoucherTmplateLink/{CompanyID}/{BranchID}/{ProductID}")]
		public async Task<IActionResult> GetAllAccVoucherTmplateLink(int CompanyID, int BranchID, int ProductID)
		{
			try
			{
				return getResponse(await _service.voucherTemplateRepository.GetAllAccVoucherTmplateLink(LoggedOnUser(),CompanyID,BranchID, ProductID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

			[HttpGet("List/Ledgerhead/{CompanyID}/{BranchID}/{ProductID}")]
		public async Task<IActionResult> GetAllLedgerHead(int CompanyID, int BranchID,int ProductID)
		{
			try
			{
				return getResponse(await _service.voucherTemplateRepository.GetAllLedgerHead(LoggedOnUser(),CompanyID,BranchID, ProductID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
	}
}
