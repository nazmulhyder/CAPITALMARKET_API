using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateLogController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<UpdateLogController> _logger;

        public UpdateLogController(IService service, ILogger<UpdateLogController> logger)
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

        [HttpGet]
        [Route("Instrument/GetList/{InstrumentID}")]
        public async Task<IActionResult> GetList(int InstrumentID)
        {
            try
            {
                var data = await _service.Logs.getUpdateLogList(InstrumentID, 6); //for instrument
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("Settlement/GetSettlementList/{SettlementScheduleID}")]
        public async Task<IActionResult> GetSettlementList(int SettlementScheduleID)
        {
            try
            {
                var data = await _service.Logs.getUpdateLogList(SettlementScheduleID, 7); //for settlement
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("Netting/GetNettingList/{NettingSetupID}")]
        public async Task<IActionResult> GetNettingList(int NettingSetupID)
        {
            try
            {
                var data = await _service.Logs.getUpdateLogList(NettingSetupID, 8); //for netting
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet]
        [Route("GetDetailList/{UpdateLogID}")]
        public async Task<IActionResult> GetDetailList(int UpdateLogID)
        {
            try
            {
                var data = await _service.Logs.getUpdateLogDetailList(UpdateLogID); //for All
                return getResponse(data);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
    }
}
