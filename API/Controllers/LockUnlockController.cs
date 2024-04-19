using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Demat;
using Model.DTOs.LockUnlock;
using Model.DTOs.Remat;
using Service.Interface;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockUnlockController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<LockUnlockController> _logger;
        public LockUnlockController(IService service, ILogger<LockUnlockController> logger)
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
        [HttpGet("CMLockUnlockInstrumentList/{CompanyID}/{BranchID}/{ContractID}/{LockUnlockStatus}")]
        public async Task<IActionResult> GetRematInstrumentList(int CompanyID, int BranchID,int ContractID, string LockUnlockStatus)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.GetLockUnlockInstrumentList( CompanyID, BranchID, ContractID, LockUnlockStatus, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpPost("CMInsertUpdateLockingInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> PostLockingInstruemnt(LockInstrumentListDTO objLockInstrumentListDTO, int CompanyID, int BranchID)
        {
            try
            {
                objLockInstrumentListDTO.EffectiveDate = Utility.DatetimeFormatter.DateFormat(objLockInstrumentListDTO.EffectiveDate);
                objLockInstrumentListDTO.TransactionDate = Utility.DatetimeFormatter.DateFormat(objLockInstrumentListDTO.TransactionDate);
                string Maker = LoggedOnUser();

                return getResponse(await _service.lockUnlockRepository.PostLockingInstruemnt(objLockInstrumentListDTO, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMLockingApprovalList/{LockInstrumentStatus}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetLockingApprovalList(string LockInstrumentStatus, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.GetLockingApprovalList(LockInstrumentStatus, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMLockingInstrumentListbyID/{LockingId}/{CompanyID}/{BranchID}")]
        public IActionResult CMLockingInstrumentListbyID(int LockingId, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.lockUnlockRepository.CMLockingInstrumentListbyID(LockingId, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {LockingId}";
                return getResponse(new Exception(msg));
            }
        }


        [HttpPost("CMUpdateLockingInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PostCMUpdateLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.PostCMUpdateLockingQuantity(objCMLockInstrumentDTO, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedLockingInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PostCMApprovedLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.PostCMApprovedLockingQuantity(objCMLockInstrumentDTO, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMInsertUpdateUnlockingInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> PostUnlockingInstruemnt(UnlockInstrumentListDTO objUnlockInstrumentListDTO, int CompanyID, int BranchID)
        {
            try
            {
                objUnlockInstrumentListDTO.EffectiveDate = Utility.DatetimeFormatter.DateFormat(objUnlockInstrumentListDTO.EffectiveDate);
                objUnlockInstrumentListDTO.TransactionDate = Utility.DatetimeFormatter.DateFormat(objUnlockInstrumentListDTO.TransactionDate);

                string Maker = LoggedOnUser();

                return getResponse(await _service.lockUnlockRepository.PostUnlockingInstruemnt(objUnlockInstrumentListDTO, CompanyID, BranchID, Maker));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnlockingApprovalList/{UnlockInstrumentStatus}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetUnlockingApprovalList(string UnlockInstrumentStatus, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.GetUnlockingApprovalList(UnlockInstrumentStatus, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMUnLockingInstrumentListbyID/{UnLockingId}/{CompanyID}/{BranchID}")]
        public IActionResult CMUnLockingInstrumentListbyID(int UnLockingId, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.lockUnlockRepository.CMUnlockingInstrumentListbyID(UnLockingId, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {UnLockingId}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("CMUpdateUnlockingInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PostCMUpdateUnlockingQuantity(CMUnLockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.PostCMUpdateUnlockingQuantity(objCMUnlockInstrumentDTO, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedUnlockingInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PostCMApprovedUnlockingQuantity(CMUnlockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.lockUnlockRepository.PostCMApprovedUnlockingQuantity(objCMUnlockInstrumentDTO, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        //[HttpPost("InsertUpdateInstrumentConversionDeclaration/{CompanyID}/{BranchID}")]
        //public async Task<IActionResult> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID)
        //{
        //    try
        //    {
        //        EntryICDDTO.RecordDate = Utility.DatetimeFormatter.DateFormat(EntryICDDTO.RecordDate);
        //        return getResponse(await _service.lockUnlockRepository.InsConversionDeclaration(EntryICDDTO, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }

        //}

        //[HttpPost("ApprovedInstrumentConversionDeclaration/{CompanyID}/{BranchID}")]

        //public async Task<IActionResult> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID)
        //{
        //    try
        //    {

        //        return getResponse(await _service.lockUnlockRepository.InstrumentDeclarationApproved(InsConvDeclarationList, CompanyID, BranchID, LoggedOnUser()));
        //    }
        //    catch (Exception ex) { return getResponse(ex); }
        //}

    }
}
