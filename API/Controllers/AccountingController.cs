using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Accounting;
using Model.DTOs.CoA;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AccountingController : ControllerBase
	{
		private readonly IService _service;
		private readonly ILogger<AccountingController> _logger;
		public AccountingController(IService service, ILogger<AccountingController> logger)
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

		[HttpPost("GetBulkVoucherValidation/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetBulkVoucherValidation(int CompanyID, int BranchID, List<VoucherBulkDto> LedgerList)
		{
			try
			{
				return getResponse(await _service.coARepository.GetBulkVoucherValidation(LoggedOnUser(),CompanyID,BranchID, LedgerList));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
        
		[HttpGet("ListAccLedgerHeadForVoucherFilter/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ListAccLedgerHeadForVoucherFilter(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.coARepository.ListAccLedgerHeadForVoucherFilter(LoggedOnUser(),CompanyID,BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
        
		[HttpGet("GetCoA/List/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> GetCoAList(int CompanyID, int BranchID,string ListType)
		{
			try
			{
				return getResponse(await _service.coARepository.GetCoAList(LoggedOnUser(),CompanyID,BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("GetCoA/all/List/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> AllCoAList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.coARepository.AllCoAList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("SaveUpdateCoA/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveUpdateCoA(int CompanyID, int BranchID, CoADto data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveUpdateCoA(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("Approve/CoA/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveCoA(int CompanyID, int BranchID, ApproveCoA data)
		{
			try
			{
				return getResponse(await _service.coARepository.ApproveCoA(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/AccLedgerHeadBulk/{CompanyID}/{BranchID}/{ProductID}")]
		public async Task<IActionResult> SaveAccLedgerHeadBulk(int CompanyID, int BranchID,int ProductID, List<AccLedgerHeadBulkDto> data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveUpdateAccLedgerHeadBulk(LoggedOnUser(), CompanyID, BranchID, ProductID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
		
		[HttpPost("save/AccLedgerHead/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveAccLedgerHead(int CompanyID, int BranchID, AccLedgerHeadDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveUpdateAccLedgerHead(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("list/AccLedgerHead/{CompanyID}/{BranchID}/{ProductID}")]
		public async Task<IActionResult> ListAccLedgerHead( int CompanyID, int BranchID, int ProductID)
		{
			try
			{
				return getResponse(await _service.coARepository.ListAccLedgerHead(LoggedOnUser(), CompanyID, BranchID, ProductID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("status/wise/list/AccLedgerHead/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> ListAccLedgerHeadAll(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.coARepository.ListAccLedgerHeadAll(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("Approve/AccLedgerHead/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAccLedgerHead(int CompanyID, int BranchID, ApproveAccLedgerHead data)
		{
			try
			{
				return getResponse(await _service.coARepository.ApproveAccLedgerHead(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/voucher/posting/date/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> GetVoucherPostingDate(int CompanyID, int BranchID)
		{
			try
			{
				return getResponse(await _service.coARepository.GetVoucherPostingDate(LoggedOnUser(), CompanyID, BranchID));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/voucher/posting/date/change/list/{CompanyID}/{BranchID}/{ListType}")]
		public async Task<IActionResult> GetVoucherPostingDateChangeList(int CompanyID, int BranchID, string ListType)
		{
			try
			{
				return getResponse(await _service.coARepository.GetVoucherPostingDateChangeList(LoggedOnUser(), CompanyID, BranchID, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/VoucherPostingDate/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveVoucherPostingDateChange(int CompanyID, int BranchID, VoucherPostingDateChangeDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveVoucherPostingDateChange(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("approve/VoucherPostingDatechange/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveVoucherPostingDateChange(int CompanyID, int BranchID, ApproveVoucherPostingDateChangeDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.ApproveVoucherPostingDateChange(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("search/account/{CompanyID}/{BranchID}/{accountnumber}")]
		public async Task<IActionResult> SearchAccount(int CompanyID, int BranchID, string accountnumber)
		{
			try 
			{
				return getResponse(await _service.coARepository.SearchAccount(LoggedOnUser(), CompanyID, BranchID, accountnumber));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("Get/LedgerHeads/For/AccVoucher/{CompanyID}/{BranchID}/{AccountNumber}/{VoucherType}")]
		public async Task<IActionResult> GetLedgerHeadsForAccVoucher(int CompanyID, int BranchID, string AccountNumber, string VoucherType)
		{
			try
			{
				return getResponse(await _service.coARepository.GetLedgerHeadsForAccVoucher(LoggedOnUser(), CompanyID, BranchID, AccountNumber, VoucherType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/Voucher/Posting/{CompanyID}/{BranchID}/{IssueType}/{IsPartyVoucher}")]
		public async Task<IActionResult> SaveAccVoucher(int CompanyID, int BranchID, string IssueType, bool IsPartyVoucher, AccVoucherDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveAccVoucher(LoggedOnUser(), CompanyID, BranchID, IssueType, IsPartyVoucher, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("Voucher/list/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ListAccVoucher(int CompanyID, int BranchID, AccVoucherListParameterDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.ListAccVoucher(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("Get/Voucher/{CompanyID}/{BranchID}/{VoucherRefNo}")]
		public async Task<IActionResult> GetAccVoucher(int CompanyID, int BranchID, string VoucherRefNo)
		{
			try
			{
				return getResponse(await _service.coARepository.GetAccVoucher(LoggedOnUser(), CompanyID, BranchID, VoucherRefNo));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("approve/Voucher/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAccVoucher(int CompanyID, int BranchID, AccVoucherApproveDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.ApproveAccVoucher(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		
		[HttpPost("save/reverse/Voucher/Posting/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> SaveReverseAccVoucher(int CompanyID, int BranchID, AccVoucherDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.SaveReverseAccVoucher(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpGet("get/product/list/{CompanyID}/{BranchID}/{IsCompanyAsProductShow}")]
		public async Task<IActionResult> Listproduct(int CompanyID, int BranchID, bool IsCompanyAsProductShow)
		{
			try
			{
				return getResponse(await _service.coARepository.Listproduct(LoggedOnUser(), CompanyID, BranchID, IsCompanyAsProductShow));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/cif/list/{CompanyID}/{BranchID}/{SearchKeyword}")]
		public async Task<IActionResult> ListCif(int CompanyID, int BranchID,string SearchKeyword)
		{
			try
			{
				return getResponse(await _service.coARepository.ListCif(LoggedOnUser(), CompanyID, BranchID, SearchKeyword));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("save/agreement/{CompanyID}/{BranchID}/{IsFundAccount}")]
		public async Task<IActionResult> saveAgreement(int CompanyID, int BranchID, bool IsFundAccount, AccCifListDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.saveAgreement(LoggedOnUser(), CompanyID, BranchID, IsFundAccount, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/agreement/list/{CompanyID}/{BranchID}/{ListType}/{IsFundAccount}")]
		public async Task<IActionResult> ListGenAgreement(int CompanyID, int BranchID, bool IsFundAccount, string ListType)
		{
			try
			{
				return getResponse(await _service.coARepository.ListGenAgreement(LoggedOnUser(), CompanyID, BranchID, IsFundAccount, ListType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("get/agreement/detail/{CompanyID}/{BranchID}/{ContractID}/{IsFundAccount}")]
		public async Task<IActionResult> GetGenAgreement(int CompanyID, int BranchID, int ContractID, bool IsFundAccount)
		{
			try
			{
				return getResponse(await _service.coARepository.GetGenAgreement(LoggedOnUser(), CompanyID, BranchID, ContractID, IsFundAccount));
			}
			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpPost("approve/agreement/{CompanyID}/{BranchID}")]
		public async Task<IActionResult> ApproveAccAgreement(int CompanyID, int BranchID, AccAgreementApproveDto data)
		{
			try
			{
				return getResponse(await _service.coARepository.ApproveAccAgreement(LoggedOnUser(), CompanyID, BranchID, data));
			}
			catch (Exception ex) { return getResponse(ex); }
		}
	}
}
