using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Model.DTOs.Approval;
using Model.DTOs.MarginRequest;
using Model.DTOs.SellSurrender;
using Model.DTOs.UnitPurchase;
using Service.Interface;
using System.Security.Principal;
using System.Text;

namespace Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SellSurrenderController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<SellSurrenderController> _logger;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        public SellSurrenderController(IService service, ILogger<SellSurrenderController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _service = service;
            _logger = logger;
            _env = env;
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


		[HttpGet("unapproved/unit/purchase/List/{CompanyID}/{BranchID}/{ContractID}")]
		public async Task<IActionResult> ListUnapprovedUnitPurchaseDetail(int CompanyID, int BranchID, int ContractID)
		{
			try
			{
				return getResponse(await _service.sellSurrenderRepository.ListUnapprovedUnitPurchaseDetail(CompanyID, BranchID, ContractID));

			}

			catch (Exception ex) { return getResponse(ex); }
		}

		[HttpGet("GetSellSurrenderMinimumUnitSetup/{CompanyID}/{BranchID}/{FundID}/{AccountType}")]
		public async Task<IActionResult> GetAllUnitIssueForSaleByFund(int CompanyID, int BranchID, int FundID,string AccountType)
		{
			try
			{
				string userName = LoggedOnUser();
				return getResponse(await _service.sellSurrenderRepository.GetSellSurrenderMinimumUnitSetup(CompanyID, BranchID, LoggedOnUser(), FundID, AccountType));
			}
			catch (Exception ex) { return getResponse(ex); }
		}


		[HttpPost("InsertUpdateMinimumUnitSetup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateMinimumUnitSetup(int CompanyID, int BranchID, SS_MinimumUnitSetupDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.InsertUpdateMinimumUnitSetup(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("InsertUpdateUnitIssueForSale/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateUnitIssueForSale(int CompanyID, int BranchID, SS_UnitIssueForSaleDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.InsertUpdateUnitIssueForSale(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetAllUnitIssueForSaleByFund/{CompanyID}/{BranchID}/{FundID}")]
        public async Task<IActionResult> GetAllUnitIssueForSaleByFund(int CompanyID, int BranchID, int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetAllUnitIssueForSaleByFund(CompanyID, BranchID, LoggedOnUser(), FundID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateUnitPurchaseDetailSetup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateUnitPurchaseDetailSetup(int CompanyID, int BranchID, UnitPurchaseDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.InsertUpdateUnitPurchaseDetailSetup(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetCustomerInfo/{CompanyID}/{BranchID}/{ContractID}")]
        public async Task<IActionResult> GetCustomerInfoForPurchase(int CompanyID, int BranchID, int ContractID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetCustomerInfoForPurchase(CompanyID, BranchID, LoggedOnUser(), ContractID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListUnitPurchaseDetail(int CompanyID, int BranchID, SurrenderFilterDto purchaseFilter)
        {
            try
            {
                return getResponse(await _service.sellSurrenderRepository.ListUnitPurchaseDetail(CompanyID, BranchID, purchaseFilter));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetUnitPurchaseDetail/{CompanyID}/{BranchID}/{PurchaseDetailID}")]
        public async Task<IActionResult> GetUnitPurchaseDetail(int CompanyID, int BranchID, int PurchaseDetailID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetQueryUnitPurchaseDetail(CompanyID, BranchID, LoggedOnUser(), PurchaseDetailID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UnitPurchaseDetailApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UnitPurchaseDetailApproval(int CompanyID, int BranchID, ApproveUnitPurchaseDetailDto approvalRequest)
        {
            try
            {
				return getResponse(await _service.sellSurrenderRepository.UnitPurchaseDetailApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest));

			}
			catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListUnitActivationRequest/{CompanyID}/{BranchID}/{ProductID}")]
        public async Task<IActionResult> ListUnitActivationRequestAML(int CompanyID, int BranchID, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.ListUnitActivationRequestAML(CompanyID, BranchID, LoggedOnUser(), ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUnitActivation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertUpdateMinimumUnitSetup(int CompanyID, int BranchID, string PurchaseDetailIDs)
        {
            try
            {
                string userName = LoggedOnUser();
                //return getResponse(await _service.sellSurrenderRepository.InsertUnitActivation(CompanyID, BranchID, LoggedOnUser(), PurchaseDetailIDs));

                return getResponse(await _service.sellSurrenderRepository.InsertUnitActivation(CompanyID, BranchID, LoggedOnUser(), PurchaseDetailIDs));
				

              
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetListOfUnitRequestActivation/{CompanyID}/{BranchID}/{ProductID}")]
        public async Task<IActionResult> GetGetListOfUnitRequestActivationAML(int CompanyID, int BranchID, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetGetListOfUnitRequestActivationAML(CompanyID, BranchID, LoggedOnUser(), ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UpdateUnitActivation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateUnitActivation(int CompanyID, int BranchID, SS_UpdateActivateRequestDto sS_UpdateActivateReq)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.UpdateUnitActivation(CompanyID, BranchID, LoggedOnUser(), sS_UpdateActivateReq));

            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("GetQueryUnitSurrender/{CompanyID}/{BranchID}/{AccountNo}/{ProductID}")]
        public async Task<IActionResult> GetQueryUnitSurrenderAML(int CompanyID, int BranchID, string AccountNo, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetQueryUnitSurrenderAML(CompanyID, BranchID, LoggedOnUser(), AccountNo, ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateUnitSurrenderSetup/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateUnitSurrenderSetup(int CompanyID, int BranchID, SS_UnitSurrenderDto entry)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.UpdateUnitSurrenderSetup(CompanyID, BranchID, LoggedOnUser(), entry));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ListUnitSurrender/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ListUnitSurrenderAML(int CompanyID, int BranchID,SurrenderFilterDto surrenderFilter)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.ListUnitSurrenderAML(CompanyID, BranchID, LoggedOnUser(), surrenderFilter));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UnitSurrenderApproval/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UnitSurrenderApproval(int CompanyID, int BranchID, ApproveUnitSurrenderDto approvalRequest)
        {
            try
            {
                var result = "";

                result = await _service.sellSurrenderRepository.UnitSurrenderApproval(LoggedOnUser(), CompanyID, BranchID, approvalRequest);

                if (result.Contains("Successfully"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetUnitSurrenderDetail/{CompanyID}/{BranchID}/{SurrenderDetailID}")]
        public async Task<IActionResult> GetUnitSurrenderDetailAML(int CompanyID, int BranchID, int SurrenderDetailID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetUnitSurrenderDetailAML(CompanyID, BranchID, LoggedOnUser(),SurrenderDetailID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetAllUnitSurrenderApprovalList/{CompanyID}/{BranchID}/{ProductID}")]
        public async Task<IActionResult> GetAllUnitSurrenderApprovalListAML(int CompanyID, int BranchID, int ProductID)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.sellSurrenderRepository.GetAllUnitSurrenderApprovalListAML(CompanyID, BranchID, LoggedOnUser(), ProductID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("List/FundCollectionForPurchase/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> FundCollectionForPurchase(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.sellSurrenderRepository.FundCollectionForPurchaseAML(CompanyID, BranchID, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        
    }
}
