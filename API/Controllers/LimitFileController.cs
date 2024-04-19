using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.ImportExportOmnibus;
using Model.DTOs.TradeDataCorrection;
using Service.Interface;
using System.ComponentModel.Design;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LimitFileController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<LimitFileController> _logger;

        public LimitFileController(IService service, ILogger<LimitFileController> logger)
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

        #region AML IL
        [HttpGet("BrokerList/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> BrokerList(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.BrokerList(CompanyID, BranchID));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("GetExportedFiles/{FileType}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetExportedFiles( string FileType, int CompanyID, int BranchID, IFormCollection form)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.GetExportedFiles(form, LoggedOnUser(),FileType,CompanyID, BranchID));
            }

            catch (Exception ex) { return getResponse(ex); }
        }



        #endregion AML IL

        #region SL
        [HttpPost("SL/Omnibus/CashLimit/ShareLimit/validation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertOmnibusLimitFileValidation(IFormCollection formdata, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.InsertOmnibusLimitFileValidation(formdata, CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("SL/InsertOmnibus/CashLimit/ShareLimit/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> InsertOmnibusLimitFileText(IFormCollection formdata, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.InsertOmnibusLimitFileText(formdata, CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }



        [HttpPost("SL/Limit/FileExport/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SLLimitFileExport(IFormCollection formdata, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.SLLimitFileExport(formdata, CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("SL/Client/Registration/FileExport/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SLClientRegistrationFileExport(IFormCollection formdata, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.SLClientRegistrationFileExport(formdata, CompanyID, BranchID, LoggedOnUser()));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("SL/ClientRegistrationFile/List/{FileType}")]
        public async Task<IActionResult> SL_ListExportClientRegistrationFile( string FileType)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.SL_ListExportClientRegistrationFile( LoggedOnUser(), FileType));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

       
        [HttpGet("SL/GetOmnibusAccountList")]
        public async Task<IActionResult> GetOmnibusAccountList()
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.GetOmnibusAccountList());
            }

            catch (Exception ex) { return getResponse(ex); }
        }

       
        [HttpGet("SL/GetOmnibusLimitFileList/{CompanyID}/{FileType}/{TradeDate}")]
        public async Task<IActionResult> GetOmnibusLimitFileList(int CompanyID,string FileType, DateTime TradeDate)
        {
            try
            {
                return getResponse(await _service.omnibusFileRepository.GetOmnibusLimitFileList(CompanyID, FileType, TradeDate));
            }

            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion SL
    }
}
