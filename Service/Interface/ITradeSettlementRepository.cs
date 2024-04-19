using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Broker;
using Model.DTOs.TradeSettlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITradeSettlementRepository
    {
        public Task<List<InstrumentWiseTradeDataDto>> GetForeignTradeDataList(string UserName, int CompanyID, int BranchID,string AccountNumner, string TradeDate);

        public Task<string> SaveForeignTradeDataAllocationAccountWise(string UserName, int CompanyID, int BranchID, List<ForeginTradeAllocationDto> data);

        public Task<string> SaveForeignTradeDataAllocation(string UserName, int CompanyID, int BranchID, InstrumentWiseTradeDataDto data);

        public Task<List<InstrumentWiseTradeDataDto>> GetForeignTradeAllocationList(string UserName, int CompanyID, int BranchID, string ListType);

        public Task<string> ApproveForeignTradeAllocation(string UserName, int CompanyID, int BranchID, ForeignTradeAllocationApproveDto data);

        public Task<InstrumentWiseTradeDataDto> GetForeignTradeAllocation(string UserName, int CompanyID, int BranchID, int TradeTransactionID);

    }
}
