using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Depository;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoryController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<DepositoryController> _logger;

        public DepositoryController(IService service, ILogger<DepositoryController> logger)
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

        [HttpPost("AddUpdateDepository")]
        public async Task<IActionResult> AddUpdateAddDepository(CMDepositoryDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                var result = await _service.Depositories.AddUpdate(currentData, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpGet("List/{PageNo}/{PerPage}/{SearchKeyword}")]
        public async Task<IActionResult> GetAllDepository(int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.Depositories.GetAll(PageNo, PerPage, SearchKeyword);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("GetDepositoryByID/{DepositoryID}")]
        public IActionResult GetDepository(int DepositoryID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = _service.Depositories.GetById(DepositoryID, userName);
                return getResponse(reponse);
            }
            catch (Exception ex)
            {
                string msg = $"Depository not found with this id: {DepositoryID}";
                return getResponse(new Exception(msg));
            }
        }
    }
}
