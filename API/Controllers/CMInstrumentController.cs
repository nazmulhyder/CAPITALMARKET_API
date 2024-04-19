using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Instrument;
using Service.Interface;
using System.Security.Principal;
using Utility;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CMInstrumentController : ControllerBase
    {

        private readonly IService _service;
        private readonly ILogger<CMInstrumentController> _logger;

        public CMInstrumentController(IService service, ILogger<CMInstrumentController> logger)
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


        [HttpPost("ApproveInstrument")]
        public async Task<IActionResult> ApproveInstrument(ApprovalInstrumentDto data)
        {
            try
            {
                //return getResponse(await _service.CMInstruments.AddUpdateGsecInstrument(data, userName));
                return getResponse(await _service.CMInstruments.ApproveInstrument(data, LoggedOnUser()));
             
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #region EquityInstrument AddUpdate
        [HttpPost("AddUpdateEquityInstrument")]
        public async Task<IActionResult> AddUpdateEquityInstrument(CMInstrumentDTO currentData)
        {
            try
            {
                string userName = LoggedOnUser();
                currentData.LastRecordDate = !String.IsNullOrEmpty(currentData.LastRecordDateInString) ? DatetimeFormatter.ConvertStringToDatetime(currentData.LastRecordDateInString) : null;
                var result = await _service.CMInstruments.AddUpdate(currentData, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion

        #region BondInstrument AddUpdate
        [HttpPost("AddUpdateBondInstrument")]
        public async Task<IActionResult> AddUpdateBondInstrument(CMBondInstrumentDto data)
        {
            try
            {
                string userName = LoggedOnUser();
                data.MaturityDate = !String.IsNullOrEmpty(data.MaturityDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.MaturityDateInString) : null;
                data.LastRecordDate = !String.IsNullOrEmpty(data.LastRecordDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.LastRecordDateInString) : null;
                //return getResponse(await _service.CMInstruments.AddUpdateBondInstrument(data, userName));
                var result = await _service.CMInstruments.AddUpdateBondInstrument(data, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion

        #region Mutual Fund Instrument AddUpdate
        [HttpPost("AddUpdateMutualFundInstrument")]
        public async Task<IActionResult> AddUpdateMutualFundInstrument(CMInstrumentDTO data)
        {
            try
            {
                string userName = LoggedOnUser();
                data.LastRecordDate = !String.IsNullOrEmpty(data.LastRecordDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.LastRecordDateInString) : null;
                data.IssueDate = !String.IsNullOrEmpty(data.IssueDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.IssueDateInString) : null;
                data.EPUDate = !String.IsNullOrEmpty(data.EPUDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.EPUDateInString) : null;
                //return getResponse(await _service.CMInstruments.AddUpdateMutualFundInstrument(data, userName));
                var result = await _service.CMInstruments.AddUpdateMutualFundInstrument(data, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);

            }
            catch (Exception ex) { return getResponse(ex); }
        }

        #endregion

        #region GsecInstrument AddUpdate
        [HttpPost("AddUpdateGsecInstrument")]
        public async Task<IActionResult> AddUpdateGsecInstrument(GsecInstrumentDto data)
        {
            try
            {
                string userName = LoggedOnUser();
                //data.LastRecordDate = !String.IsNullOrEmpty(data.LastRecordDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.LastRecordDateInString) : null;
                data.IssueDate = !String.IsNullOrEmpty(data.IssueDate) ? DatetimeFormatter.DateFormat(data.IssueDate) : null;
                data.MaturityDate = !String.IsNullOrEmpty(data.MaturityDate) ? DatetimeFormatter.DateFormat(data.MaturityDate) : null;
                data.NextCouponDate = !String.IsNullOrEmpty(data.NextCouponDate) ? DatetimeFormatter.DateFormat(data.NextCouponDate) : null;
                data.LastCouponPaymentDate = !String.IsNullOrEmpty(data.LastCouponPaymentDate) ? DatetimeFormatter.DateFormat(data.LastCouponPaymentDate) : null;
                //data.InformationDate = !String.IsNullOrEmpty(data.InformationDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.InformationDateInString) : null;
                //data.LastInterestPayoutDate = !String.IsNullOrEmpty(data.LastInterestPayoutDateInString) ? DatetimeFormatter.ConvertStringToDatetime(data.LastInterestPayoutDateInString) : null;

                //return getResponse(await _service.CMInstruments.AddUpdateGsecInstrument(data, userName));
                var result = await _service.CMInstruments.AddUpdateGsecInstrument(data, userName);
                if (result.Contains("success"))
                    return getResponse(result);
                else
                    return getResponse(null, result);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        #endregion

        [HttpGet("Equity/List/{PageNo}/{PerPage}/{SearchKeyword}/{ApprovalStatus}")]
        public async Task<IActionResult> GetAllEquityInstrument(int PageNo, int PerPage, string SearchKeyword , string ApprovalStatus)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMInstruments.GetAllInstrumentList("Equity", PageNo, PerPage, SearchKeyword, ApprovalStatus);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Bond/List/{PageNo}/{PerPage}/{SearchKeyword}/{ApprovalStatus}")]
        public async Task<IActionResult> GetAllBondInstrument(int PageNo, int PerPage, string SearchKeyword, string ApprovalStatus)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMInstruments.GetAllInstrumentList("Bond", PageNo, PerPage, SearchKeyword, ApprovalStatus);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("MutualFund/List/{PageNo}/{PerPage}/{SearchKeyword}/{ApprovalStatus}")]
        public async Task<IActionResult> GetAllMutualFundInstrument(int PageNo, int PerPage, string SearchKeyword, string ApprovalStatus)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMInstruments.GetAllInstrumentList("Mutual Fund", PageNo, PerPage, SearchKeyword, ApprovalStatus);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        [HttpGet("Gsec/List/{PageNo}/{PerPage}/{SearchKeyword}/{ApprovalStatus}")]
        public async Task<IActionResult> GetAllGsecInstrument(int PageNo, int PerPage, string SearchKeyword, string ApprovalStatus)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CMInstruments.GetAllInstrumentList("G Sec", PageNo, PerPage, SearchKeyword, ApprovalStatus);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }



        [HttpGet("GetEquityInstrumentById/{InstrumentID}")]
        public async Task<IActionResult> GetEquityInstrumentById(int InstrumentID)
        {
            try
            {
                return getResponse(_service.CMInstruments.GetById(InstrumentID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("GetBondInstrumentById/{InstrumentID}")]
        public async Task<IActionResult> GetBondInstrumentById(int InstrumentID)
        {
            try
            {
                return getResponse(await _service.CMInstruments.GetBondInstrumentById(InstrumentID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("GetMutualFundInstrumentById/{InstrumentID}")]
        public async Task<IActionResult> GetMutualFundInstrumentById(int InstrumentID)
        {
            try
            {
                return getResponse(await _service.CMInstruments.GetMutualFundInstrumentById(InstrumentID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpGet("GetGsecInstrumentById/{InstrumentID}")]
        public async Task<IActionResult> GetGsecInstrumentById(int InstrumentID)
        {
            try
            {
                return getResponse(await _service.CMInstruments.GetGsecInstrumentById(InstrumentID, LoggedOnUser()));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }


    }
}
