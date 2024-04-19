using Model.DTOs.BrokerageCommision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBrokerageCommisionRepository : IGenericRepository<BrokerageCommisionDto>
    {
        public Task<string> ApproveTradeDataUpdateForCommisionUpdate(string UserName, ApproveTradeDataCommisionUpdatDto approveData);
        public Task<List<TradeDataCommisionUpdateContractListDto>> GetTradeDataCommisionUpdateContractListDto(string UserName, DateTime TradeDate);
        public Task<List<TradeDataForCommisionUpdateDto>> GetTradeDataForCommisionUpdate(string UserName,int ContractID, DateTime TradeDate);
        public Task<string> ApproveBrokerageCommision(BrokerageCommisionApproveDto Approval,string LoggedOnUser);
        public Task<string> UpdateBrokerageCommisionItem(BrokerageCommisionItemDto commision, string UserName, int CompanyID, int BranchID);
        public Task<BrokerageCommisionItemDto> GeBrokerageCommisionItem(int ContractID, string UserName);
        public Task<List<BrokerageCommisionDto>> GeBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword);
        
        
    }
}
