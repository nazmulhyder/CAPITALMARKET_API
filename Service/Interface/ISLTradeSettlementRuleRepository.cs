using Model.DTOs.TradingPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISLTradeSettlementRuleRepository : IGenericRepository<SLTradeSettlementRuleDTO>
    {
        public Task<List<SLTradeSettlementRuleEditDTO>> GetAllSLTradeSettlementRule(int CompanyID, int BranchID);
        public Task<SLTradeSettlementRuleEditDTO> GetSLTradeSettlementRuleById(int SLTradeSettlementRuleID, string user);
    }
}
