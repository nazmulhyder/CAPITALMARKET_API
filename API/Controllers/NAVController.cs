using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.NAVFileUpload;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class NAVController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<NAVController> _logger;
		public NAVController(IService service, ILogger<NAVController> logger)
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

		[HttpPost("ReceiveMFInstrument/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ReceiveMFInstrument(int CompanyID, int BranchID, MFReceiveDetailDto data)
		{
			try
			{
				return getResponse(await _service.navRepository.ReceiveMFInstrument(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
		[HttpPost("DeliverMFInstrument/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> DeliverMFInstrument(int CompanyID, int BranchID, MFReceiveDetailDto data)
		{
			try
			{
				return getResponse(await _service.navRepository.DeliverMFInstrument(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("GetListMFInstrumentForDelivery/{CompanyID}/{BranchID}/{AccountNo}")]
		public async Task<IActionResult> GetListMFInstrumentForDelivery(int CompanyID, int BranchID, string AccountNo)
		{
			try
			{
				return getResponse(await _service.navRepository.GetListMFInstrumentForDelivery(LoggedOnUser(), CompanyID, BranchID, AccountNo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("GetListMFInstrumentForReceive/{CompanyID}/{BranchID}/{AccountNo}")]
		public async Task<IActionResult> GetListMFInstrumentForReceive(int CompanyID, int BranchID, string AccountNo)
		{
			try
			{
				return getResponse(await _service.navRepository.GetListMFInstrumentForReceive(LoggedOnUser(), CompanyID, BranchID, AccountNo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}



		#region NAVFile

		[HttpPost("GetNavFileValidation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetNavFileValidation(int CompanyID, int BranchID, List<NAVFileDetailDto> data)
		{
			try
			{
				return getResponse(await _service.navRepository.GetNavFileValidation(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("SaveNavFile/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveNavFile(int CompanyID, int BranchID, IFormCollection data)
		{
			try
			{
				return getResponse(await _service.navRepository.SaveNavFile(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("GetNavFileList/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> GetNavFileList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.navRepository.GetNavFileList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("GetNavFileDetail/{CompanyID}/{BranchID}/{FileID}")]
		public async Task<IActionResult> GetNavFileDetail(int CompanyID, int BranchID, int FileID)
		{
			try
			{
				return getResponse(await _service.navRepository.GetNavFileDetail(LoggedOnUser(), CompanyID, BranchID, FileID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("ApproveNAVFile/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveNavFile(int CompanyID, int BranchID, NAVFileApproveDto data)
		{
			try
			{
				return getResponse(await _service.navRepository.ApproveNavFile(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		#endregion NAVFile
	}
}
