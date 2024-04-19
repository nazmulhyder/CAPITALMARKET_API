using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<FAQController> _logger;
        public FAQController(IService service, ILogger<FAQController> logger)
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

        [HttpGet("GetClientName/{CompanyID}/{BranchID}/{ProductID}/{AccountNo}/{FundID}")]
        public async Task<IActionResult> GetClientName(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            try
            {

               return getResponse(await _service.FAQRepository.GetClientNameByProductAndAccount(CompanyID, BranchID, ProductID, AccountNo, FundID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CustomerListByName/{CompanyID}/{BranchID}/{CustomerName}")]
        public async Task<IActionResult> CustomerListByName(int CompanyID, int BranchID, string CustomerName)
        {
            try
            {

                return getResponse(await _service.FAQRepository.CustomerListByName(CompanyID, BranchID, CustomerName, LoggedOnUser()));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetDocumentCheckList/{docTypeName}/{pTypeCode}")]
        public async Task<IActionResult> GetDocumentCheckList(string docTypeName, string pTypeCode)
        {
            try
            {

                return getResponse(await _service.FAQRepository.GetDocumentCheckList(docTypeName, pTypeCode));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("ListRM/{CompanyID}")]
        public async Task<IActionResult> ListRM(int CompanyID)
        {
            try
            {

                return getResponse(await _service.FAQRepository.ListRM(CompanyID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetAllInstrumentWithFaceValue/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetAllInstrumentWithFaceValue(int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.FAQRepository.ListInstrumentFaceVal(CompanyID, BranchID));

            }

            catch (Exception ex) { return getResponse(ex); }
        }



    }
}
