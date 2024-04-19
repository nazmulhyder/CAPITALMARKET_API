using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Demat;
using Model.DTOs.PhysicalInstrumentCollectionDelivery;
using Model.DTOs.SecurityElimination;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalInstrumentCollectionDeliveryController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<PhysicalInstrumentCollectionDeliveryController> _logger;
        public PhysicalInstrumentCollectionDeliveryController(IService service, ILogger<PhysicalInstrumentCollectionDeliveryController> logger)
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

        [HttpPost("CMInsertUpdatePhysicalInstrumentCollection/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCMPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryDTO entryIntrumentConversion, int CompanyID, int BranchID)
        {
            try
            {
                entryIntrumentConversion.CollectionDate = Utility.DatetimeFormatter.DateFormat(entryIntrumentConversion.CollectionDate);

                return getResponse(await _service.physicalInstrumentCollectionDelivery.InsertCMPhysicalInstrumentCollection(entryIntrumentConversion, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMPhysicalInstrumentCollectionList/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMPhysicalInstrumentCollectionList(string Status, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.physicalInstrumentCollectionDelivery.CMPhysicalInstrumentCollectionList(Status, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMPhysicalInstrumentCollectionListbyID/{PhyInstCollID}/{CompanyID}/{BranchID}")]
        public IActionResult CMPhysicalInstrumentCollectionListbyID(int PhyInstCollID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.physicalInstrumentCollectionDelivery.CMPhysicalInstrumentCollectionListbyID(PhyInstCollID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {PhyInstCollID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMApprovedPhysicalInstrumentCollection/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovedPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryApprove objCMApprovedPhysicalInstrumentCollection, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.physicalInstrumentCollectionDelivery.GetCMApprovedPhysicalInstrumentCollection(objCMApprovedPhysicalInstrumentCollection, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMInsertUpdatePhysicalInstrumentDelivery/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCMPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryDTO entryIntrumentDelivery, int CompanyID, int BranchID)
        {
            try
            {
                entryIntrumentDelivery.DeliveryDate = Utility.DatetimeFormatter.DateFormat(entryIntrumentDelivery.DeliveryDate);

                return getResponse(await _service.physicalInstrumentCollectionDelivery.InsertCMPhysicalInstrumentDelivery(entryIntrumentDelivery, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMPhysicalInstrumentDeliveryList/{Status}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMPhysicalInstrumentDeliveryList(string Status,int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.physicalInstrumentCollectionDelivery.CMPhysicalInstrumentDeliveryList(Status,CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMPhysicalInstrumentDeliveryListbyID/{PhyInstDelID}/{CompanyID}/{BranchID}")]
        public IActionResult CMPhysicalInstrumentDeliveryListbyID(int PhyInstDelID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.physicalInstrumentCollectionDelivery.CMPhysicalInstrumentDeliveryListbyID(PhyInstDelID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {PhyInstDelID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMApprovedPhysicalInstrumentDelivery/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovedPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryApprove objCMApprovedPhysicalInstrumentDelivery, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.physicalInstrumentCollectionDelivery.GetCMApprovedPhysicalInstrumentDelivery(objCMApprovedPhysicalInstrumentDelivery, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
