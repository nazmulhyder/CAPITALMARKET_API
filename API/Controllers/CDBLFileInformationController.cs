using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.DTOs.Broker;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.IPO;
using Model.DTOs.Right;
using Service.Interface;
using System.Data.SqlClient;
using System.Security.Principal;
using Utility;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CDBLFileInformationController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<CDBLFileInformationController> _logger;
        public CDBLFileInformationController(IService service, ILogger<CDBLFileInformationController> logger)
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


        [HttpGet("CDBLFileInformationList/{CompanyID}/{BranchID}/{TransactionDateFrom}/{TransactionDateTo}")]
        public async Task<IActionResult> getCDBLFileInformationList( int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo)
        {
            try
            {
                return getResponse(await _service.CDBLFileInformation.getCDBLFileInformationList(LoggedOnUser(), CompanyID, BranchID, TransactionDateFrom, TransactionDateTo, "D:\\CDBL Downloaded File\\"));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("process/CDBLFile/{CompanyID}/{BranchID}/{TransactionDateFrom}/{TransactionDateTo}")]
        public async Task<IActionResult> ProcessCDBLFile( int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.CDBLFileInformation.ProcessCDBLFile(LoggedOnUser(),CompanyID, BranchID,TransactionDateFrom,TransactionDateTo,data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CDBLIPOFileDataList/{IPOInstrumentID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLIPOFileDataList(int IPOInstrumentID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLIPOFileDataList(IPOInstrumentID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CDBLUpdateIPOFiledata/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLUpdateIPOFiledata(CMCDBLUpdateIPOFiledataMaster objCDBLUpdateIPOFiledata, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLUpdateIPOFiledata(objCDBLUpdateIPOFiledata,CompanyID,BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMCDBLFileProcess/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLFileProcess(CMCDBLUpdateIPOFiledataProcess objGetCDBLFileProcess, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLFileProcess(objGetCDBLFileProcess, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CDBLRightsFileApprovalList/{RightSettingID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLRightsFileApprovalList(int RightSettingID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLRightsFileApprovalList(RightSettingID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CDBLUpdateRightsData/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLUpdateRightsData(CMCDBLUpdateRightsMaster objCDBLUpdateRightsData, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLUpdateRightsData(objCDBLUpdateRightsData, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMCDBLRightsProcess/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLRightsProcess(CMCDBLRightsProcess objCDBLRightsProcess, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLRightsProcess(objCDBLRightsProcess, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMCDBLTransferTransmissionApprovalList/{TransactionDateFrom}/{TransactionDateTo}/{Status}/{CompanyID}/{BranchID}/{CDBLFileInfoID}")]
        public async Task<IActionResult> getCMCDBLTransferTransmissionApprovalList(string TransactionDateFrom, string TransactionDateTo, string Status, int CompanyID, int BranchID, int CDBLFileInfoID)
        {
            try
            {
                return getResponse(await _service.CDBLFileInformation.getCMCDBLTransferTransmissionApprovalList( TransactionDateFrom, TransactionDateTo,Status, CompanyID, BranchID, CDBLFileInfoID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("SLUpdatePriceTransferTransmissionInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetCDBLUpdatePriceTransferTransmissionInstrument(SLTransferTransmissionInstrumentMaster objCDBLUpdatePriceTransferTransmissionInstrument, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetCDBLUpdatePriceTransferTransmissionInstrument(objCDBLUpdatePriceTransferTransmissionInstrument, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMApprovedTransferTransmissionInstrument/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetSLApprovedTransferTransmissionInstrument(SLApprovedTransferTransmissionInstrumentDTO objSLApprovedTransferTransmissionInstrument, int CompanyID, int BranchID)
        {
            try
            {
                string Maker = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetSLApprovedTransferTransmissionInstrument(objSLApprovedTransferTransmissionInstrument, CompanyID, BranchID, Maker);
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("CMInsertUpdateTransferTransmissionInstrument/{CompanyID}/{BranchID}")]

        public async Task<IActionResult> TransferTransmissionInstrument(CMCDBLTransferTransmissionDTO? entryTransferTransmissionInstrument, int CompanyID, int BranchID)
        {
            try
            {

                return getResponse(await _service.CDBLFileInformation.TransferTransmissionInstrument(entryTransferTransmissionInstrument, CompanyID, BranchID, LoggedOnUser()));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentInfobyContractIDandInstrumentID/{InstrumentID}/{ContractID}/{TransactionDate}/{CompanyID}/{BranchID}")]
        public IActionResult InstrumentInfoListbyID(int InstrumentID,int ContractID,string TransactionDate, int CompanyID, int BranchID)
        {
            try
            {
                return getResponse(_service.CDBLFileInformation.InstrumentInfoListbyID(InstrumentID, ContractID, TransactionDate, CompanyID, BranchID));
            }
            catch (Exception ex)
            {
                string msg = $"Instrument not found with this id: {InstrumentID}";
                return getResponse(new Exception(msg));
            }
        }

        [HttpPost("process/CDBLFileValidation/{CompanyID}/{BranchID}/{TransactionDateFrom}/{TransactionDateTo}")]
        public async Task<IActionResult> ValidateCDBLFile(int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data)
        {
            try
            {
                string userName = LoggedOnUser();
                return getResponse(await _service.CDBLFileInformation.ValidateCDBLFile(LoggedOnUser(), CompanyID, BranchID, TransactionDateFrom, TransactionDateTo, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("AMLMFBankAccountList/{FundID}")]
        public async Task<IActionResult> GetAMLMFBankAccountList(int FundID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetAMLMFBankAccountList(FundID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("CMInstrumentInfobyTypeCodeList/{TypeCode}/{AccountNumber}/{ProductID}/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> GetInstrumentInfobyTypeCodeList(int TypeCode,string AccountNumber,int ProductID, int CompanyID, int BranchID)
        {
            try
            {
                string userName = LoggedOnUser();
                var reponse = await _service.CDBLFileInformation.GetInstrumentInfobyTypeCodeList(TypeCode, AccountNumber, ProductID, CompanyID, BranchID, LoggedOnUser());
                return getResponse(reponse);
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
