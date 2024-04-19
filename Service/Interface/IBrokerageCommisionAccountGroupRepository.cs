using Model.DTOs.BrokerageCommision;
using Model.DTOs.BrokerageCommisionAccountGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBrokerageCommisionAccountGroupRepository : IGenericRepository<BrokerageCommisionAccountGroupDto>
    {
        public Task<string> ApproveBrokerageCommisionAccountGroup(BrokerageCommisionAccountGroupApproveDto Approval, string LoggedOnUser);
        public Task<BrokerageCommisionAccountGroupItemDto> GetBrokerageCommisionAccountGroupItem(int AccountGroupID, string UserName);
        public Task<string> UpdateBrokerageCommisionAccountGroupItem(BrokerageCommisionAccountGroupItemDto commision, string UserName, int CompanyID, int BranchID);
        public Task<List<BrokerageCommisionAccountGroupDto>> GetBrokerageCommisionAccountGroupList(int CompanyID, string UserName, string ApprovalStatus, int PageNo, int Perpage, string SearchKeyword);
        public Task<List<BrokerageCommisionAccountGroupDto>> GetUnapprovedBrokerageCommisionAccountGroupList(int CompanyID, string UserName, int PageNo, int Perpage, string SearchKeyword);
    }
}
