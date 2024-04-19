using Model.DTOs.Approval;
using Model.DTOs.StockSplit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IStockSplitRepository
    {
        public Task<List<InstrumentHoldingDto>> InstrumentHolding(int CompanyID, int BranchID, int InstrumentID, decimal SplitRatio);
        public Task<string> SaveStockSplit(int CompanyID, int BranchID, string userName, StockSplitSetting entry);
        public Task<StockSplitSetting> GetStockSplit(int CompanyID, int BranchID, string userName, int StockSplitSettingID);
        public Task<List<StockSplitSetting>> StockSplitSettingList(int CompanyID, int BranchID);
        public Task<string> StockSplitApproval(string userName, int CompanyID, int branchID, StockSplitApprovalDto approvalDto);

    }
}
