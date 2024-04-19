using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Broker;
using Model.DTOs.TradeSettlement;
using Service.Interface;
using System.Security.Principal;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TradeSettlementController : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger<TradeSettlementController> _logger;
        public TradeSettlementController(IService service, ILogger<TradeSettlementController> logger)
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

        [HttpGet("Foreign/Trade/Data/List/{CompanyID}/{BranchID}/{AccountNumner}/{TradeDate}")]
        public async Task<IActionResult> GetForeignTradeDataList(int CompanyID, int BranchID,string AccountNumner,string TradeDate)
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.GetForeignTradeDataList(LoggedOnUser(),CompanyID, BranchID,AccountNumner, TradeDate));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpPost("Save/Foreign/Trade/Data/Allocation/AccountWise/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveForeignTradeDataAllocationAccountWise(int CompanyID, int BranchID, List<ForeginTradeAllocationDto> data)
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.SaveForeignTradeDataAllocationAccountWise(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }
        
        [HttpPost("Save/Foreign/Trade/Data/Allocation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> SaveForeignTradeDataAllocation(int CompanyID, int BranchID, InstrumentWiseTradeDataDto data)
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.SaveForeignTradeDataAllocation(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Foreign/Trade/Allocation/List/{CompanyID}/{BranchID}/{ListType}")]
        public async Task<IActionResult> GetForeignTradeAllocationList(int CompanyID, int BranchID, string ListType )
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.GetForeignTradeAllocationList(LoggedOnUser(), CompanyID, BranchID, ListType));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

        [HttpGet("Foreign/Trade/Allocation/Detail/{CompanyID}/{BranchID}/{TradeTransactionID}")]
        public async Task<IActionResult> GetForeignTradeAllocation(int CompanyID, int BranchID, int TradeTransactionID)
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.GetForeignTradeAllocation(LoggedOnUser(), CompanyID, BranchID, TradeTransactionID));
            }
            catch (Exception ex) { return getResponse(ex); }
        }


        [HttpPost("Approve/Foreign/Trade/Allocation/{CompanyID}/{BranchID}")]
        public async Task<IActionResult> ApproveForeignTradeAllocation(int CompanyID, int BranchID, ForeignTradeAllocationApproveDto data)
        {
            try
            {
                return getResponse(await _service.tradeSettlementRepository.ApproveForeignTradeAllocation(LoggedOnUser(), CompanyID, BranchID, data));
            }
            catch (Exception ex) { return getResponse(ex); }
        }

    }
}
