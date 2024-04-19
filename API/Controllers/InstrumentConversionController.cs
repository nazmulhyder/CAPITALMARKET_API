using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Demat;
using Model.DTOs.InstrumentConversion;
using Model.DTOs.LockUnlock;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentConversionController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<InstrumentConversionController> _logger;
        public InstrumentConversionController(IService service, ILogger<InstrumentConversionController> logger)
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

        [HttpPost("InsertUpdateInstrumentConversionDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID)
        {
            try
            {
                EntryICDDTO.RecordDate = Utility.DatetimeFormatter.DateFormat(EntryICDDTO.RecordDate);
                return getResponse(await _service.instrumentConversion.InsConversionDeclaration(EntryICDDTO, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpGet("CMInstrumentConversionDeclarationList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstConversionDeclarationList(int CompanyID, int BranchID)
        {
            try
            {

                var reponse = await _service.instrumentConversion.GetInstConversionDeclarationList(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentConversionDeclarationListbyID/{InstConversionID}/{CompanyID}/{BranchID}")]
        public IActionResult CMInstrumentConversionListbyID(int InstConversionID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.instrumentConversion.CMInstrumentConversionListbyID(InstConversionID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstConversionID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("ApprovedInstrumentConversionDeclaration/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.instrumentConversion.InstrumentDeclarationApproved(InsConvDeclarationList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentConversionList/{InstConversionID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMInstrumentConversionList(int InstConversionID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.instrumentConversion.CMInstrumentConversionList(InstConversionID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstConversionID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMInsertUpdateInstrumentConversion/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCMInstrumentConversion(CMInstrumentConversionInsert entryIntrumentConversion, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.instrumentConversion.InsertCMInstrumentConversion(entryIntrumentConversion, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentConversionApprovalList/{InstConversionID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMInstrumentConversionApprovalList(int InstConversionID, int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.instrumentConversion.CMInstrumentConversionApprovalList(InstConversionID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("CMApprovedInstrumentConversion/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovednstrumentConversion(CMInstrumentConversionApprove objCMApprovedInstrumentElimination, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.instrumentConversion.GetCMApprovednstrumentConversion(objCMApprovedInstrumentElimination, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMUpdateInstrumentConversionTotalCost/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstrumentConversionTotalCost(CMInstrumentConversionUpdateBaseQuantityDTO objInsConversionUpdate, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.instrumentConversion.GetInstrumentConversionTotalCost(objInsConversionUpdate, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("InsertUpdateInstrumentSplitDeclaration/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsSplitDeclaration(InstrumentSplitDeclarationDTO EntryISDDTO, int CompanyID, int BranchID)
        {
            try
            {
                EntryISDDTO.RecordDate = Utility.DatetimeFormatter.DateFormat(EntryISDDTO.RecordDate);
                return getResponse(await _service.instrumentConversion.InsSplitDeclaration(EntryISDDTO, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpGet("CMInstrumentSplitDeclarationListbyID/{InstSplitedID}/{CompanyID}/{BranchID}")]
        public IActionResult GetCMInstrumentSplitListbyID(int InstSplitedID, int CompanyID, int BranchID)
        {

            try
            {
                return getResponse(_service.instrumentConversion.GetCMInstrumentSplitListbyID(InstSplitedID, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstSplitedID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("CMInstrumentSplitDeclarationList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstSplitDeclarationList(int CompanyID, int BranchID)
        {
            try
            {

                var reponse = await _service.instrumentConversion.GetInstSplitDeclarationList(CompanyID, BranchID);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("ApprovedInstrumentSplitDeclaration/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InstrumentSplitDeclarationApproved(InstrumentSplitDeclarationApprove InsSplitDeclarationList, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.instrumentConversion.InstrumentSplitDeclarationApproved(InsSplitDeclarationList, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentSplitList/{InstSplitedID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMInstrumentSplitList(int InstSplitedID, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.instrumentConversion.CMInstrumentSplitList(InstSplitedID, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstSplitedID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMInsertUpdateSplitConversion/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> InsertCMInstrumentSplit(CMInstrumentSplitInsert entryIntrumentSplit, int CompanyID, int BranchID)
        {
            try
            {


                return getResponse(await _service.instrumentConversion.InsertCMInstrumentSplit(entryIntrumentSplit, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentSplitApprovalList/{InstSplitedID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> CMInstrumentSplitApprovalList(int InstSplitedID, int CompanyID, int BranchID)
        {
            try
            {
                var response = await _service.instrumentConversion.CMInstrumentSplitApprovalList(InstSplitedID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(response);
            }
            catch (Exception ex) { return getResponse(ex); }

        }


        [HttpPost("CMUpdateInstrumentSplittedQuantity/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstrumentSplittedQuantity(CMInstrumentSplitUpdateSpittedQuantityDTO objInsSplitUpdate, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.instrumentConversion.GetInstrumentSplittedQuantity(objInsSplitUpdate, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedInstrumentSplit/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCMApprovednstrumentSplit(CMInstrumentSplitApprove objCMApprovedInstrumentSplit, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.instrumentConversion.GetCMApprovednstrumentSplit(objCMApprovedInstrumentSplit, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }


}
