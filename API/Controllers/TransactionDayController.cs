using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.AssetManager;
using Model.DTOs.InstrumentGroup;
using Model.DTOs.TransactionDay;
using Service.Interface;
using System.Security.Principal;
namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    //   /api/TransactionDay/TransactionDayList
    public class TransactionDayController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<TransactionDayController> _logger;
        public TransactionDayController(IService service, ILogger<TransactionDayController> logger)
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
      

        [HttpGet("List/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> TransactionDayList(int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(await _service.TransactionDay.GetWeekDays(CompanyID, BranchID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet]
        [Route("GetCalendarDayList/{CompanyID}/{BranchID}/{Month}/{Year}")]
        public async Task<IActionResult> GetCalendarDaysList(int CompanyID, int BranchID, string Month, int Year)
        {
            try
            {
                var data = await _service.TransactionDay.GetCalendarDaysList(CompanyID, BranchID,Month, Year);
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }



        [HttpPost("UpdateTransactionDay/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateTransactionDay(int CompanyID, int BranchID,List<UpdateTransactionDayDto> transactionDays)
        {
            try
            {
                return getResponse(await _service.TransactionDay.UpdateTransactionDay(CompanyID, BranchID,transactionDays, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("UpdateHoliDay/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> UpdateHoliDay(int CompanyID, int BranchID,List<CalendarDayDto>? holiDays)
        {
            try
            {
                return getResponse(await _service.TransactionDay.UpdateHoliDay(CompanyID, BranchID,holiDays, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


    }
}
