using Model.DTOs.BrokerageCommision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBrokerageCommissionAMLRepository
    {
        public Task<List<BrokerageCommissionAMLDto>> GetBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus);
        public Task<object> GetAMLBrokerageCommisionByContractID(int ContractID, string UserName);      
        public Task<string> UpdateAMLBrokerageCommission(List<BrokerageCommissionAMLDetailDto> BrokerList, string userName, int CompanyID, int BranchID);
        public Task<string> ApproveAMLBrokerageCommision(BrokerageCommisionApproveDto approval, string UserName);

    }
}
