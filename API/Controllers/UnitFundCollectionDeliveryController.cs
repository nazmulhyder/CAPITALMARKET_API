using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.UnitFundCollectionDelivery;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitFundCollectionDeliveryController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<UnitFundCollectionDeliveryController> _logger;
        public UnitFundCollectionDeliveryController(IService service, ILogger<UnitFundCollectionDeliveryController> logger)
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

        [HttpPost("CMInsertUpdateUnitFundCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> GetUnitFundCollection(UnitFundCollectionDTO objUnitFundCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.unitFund.GetUnitFundCollection(objUnitFundCollection, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnitFundCollectionList/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetUnitFundCollectionList(string Status,int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.unitFund.GetUnitFundCollectionList(Status,CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnitFundCollectionListByID/{UnitFundCollID}/{CompanyID}/{BranchID}")]
        public IActionResult CMUnitFundCollectionListByID(int UnitFundCollID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.unitFund.CMUnitFundCollectionListByID(UnitFundCollID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {UnitFundCollID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMInsertUpdateUnitFundDelivery/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> GetUnitFundDelivery(UnitFundDeliveryDTO objUnitFundDelivery, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();

                return getResponse(await _service.unitFund.GetUnitFundDelivery(objUnitFundDelivery, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnitFundDeliveryList/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetUnitFundDeliveryList(string Status,int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.unitFund.GetUnitFundDeliveryList(Status,CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnitFundDeliveryListByID/{UnitFundDelID}/{CompanyID}/{BranchID}")]
        public IActionResult GetCMUnitFundDeliveryListByID(int UnitFundDelID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.unitFund.GetCMUnitFundDeliveryListByID(UnitFundDelID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {UnitFundDelID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMApprovedUnitFundCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetApprovedUnitFundCollection(UnitFundCollectionApprove objApprovedUnitFundCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.unitFund.GetApprovedUnitFundCollection(objApprovedUnitFundCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedUnitFundDelivery/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetApprovedUnitFundDelivery(UnitFundDeliveryApprove objApprovedUnitFundDelivery, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.unitFund.GetApprovedUnitFundDelivery(objApprovedUnitFundDelivery, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
