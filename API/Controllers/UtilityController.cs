using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UtilityController : Controller
    {
        private readonly IGlobalSettingService _service;
        private readonly ILogger<UtilityController> _logger;
        public UtilityController(IGlobalSettingService service, ILogger<UtilityController> logger)
        {
            _service = service;
            _logger = logger;
        }
        private string LoggedOnUser()
        {
            var principal = HttpContext.User;
            _logger.LogInformation("Principal: {0}", principal.Identity.Name);
            var windowsIdentity = principal?.Identity as WindowsIdentity;
            //var userName = WindowsIdentity.GetCurrent().Name;
            string loggedOnUser = windowsIdentity.Name;
            if (loggedOnUser.Contains('\\'))
                loggedOnUser = loggedOnUser.Split("\\")[1];
            return loggedOnUser;
        }

        [HttpGet("GetTransactionDateStatus")]
        public IActionResult GetTransactionDateStatus(int CompanyID, int BranchID)
        {
            return Ok(_service.GetTransactionDateStatus(LoggedOnUser(), CompanyID, BranchID));
        }

        [HttpGet("GetDropdownByTypeName")]
        public IActionResult TypeCode(string typeName)
        {
            return Ok(_service.GetTypeCodes(typeName));
        }

        [HttpGet("GetDropdownByTypeNameAndTypeId")]
        public IActionResult TypeCodeByPtypeID(string typeName, int pTypeID)
        {
            return Ok(_service.GetTypeCodes(typeName, pTypeID));
        }

        [HttpGet("{CompanyID}/{BranchID}/GetDropdownByTypeNameCompanyWise")]
        public IActionResult TypeCodeCompanyWise(int CompanyID,int BranchID,string TypeName, int InstructionParam)
        {
            return Ok(_service.GetTypeCodesCompanyWise( CompanyID,BranchID,TypeName,InstructionParam));
        }

    }
}
