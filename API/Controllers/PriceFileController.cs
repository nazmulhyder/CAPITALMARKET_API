using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.PriceFileUpload;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PriceFileController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<PriceFileController> _logger;
        public readonly IConfiguration _configuration;
        public PriceFileController(IService service, ILogger<PriceFileController> logger, IConfiguration configuration)
        {
            _service = service;
            _logger = logger;
            _configuration = configuration;
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

        [HttpGet("SaveNewInstrumentCategory/{CategoryName}")]
        public async Task<IActionResult> SaveNewInstrumentCategory(string CategoryName)
        {
            try
            {
                return getResponse(await _service.priceFile.SaveNewInstrumentCategory(CategoryName));
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpGet("PriceFileComparisonFromFTP/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PriceFileComparisonFromFTP(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.priceFile.PriceFileComparisonFromFTP(LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }

        }

        [HttpPost("PriceFileComparison/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> PriceFileComparison(IFormCollection formData,int CompanyID,int BranchID)
        {
            try
            {
                return getResponse(await _service.priceFile.PriceFileComparison(formData, LoggedOnUser(), CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        
        
        [HttpPost("PriceFileUpload")]
        public async Task<IActionResult> PriceFileUpload(IFormCollection formData)
        {
            try
            {
                return getResponse(await _service.priceFile.PriceFileUpload(formData, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        
        [HttpGet("List/Closing/Price/{CompanyID}/{TradeDate}")]
        public async Task<IActionResult> ClosingPriceFileListDto(int CompanyID, DateTime TradeDate)
        {
            try
            {
                //string userName = LoggedOnUser();
                var reponse = await _service.priceFile.ClosingPriceFileList(CompanyID, TradeDate);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
