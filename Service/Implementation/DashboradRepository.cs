using Model.DTOs.Dashborad;
using Model.DTOs.TradeRestriction;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class DashboradRepository : IDashboradRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public DashboradRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<DashboardDTO> GetAll(int CompanyID, int BranchID, string userName)
        {
            DashboardDTO dashboard = new DashboardDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@UserName",  userName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
               
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_Dashboard]", sqlParams);
            dashboard.tradeProcessings = CustomConvert.DataSetToList<TradeProcessingSummaryDto>(DataSets.Tables[0]).ToList();
            dashboard.buySellOrders = CustomConvert.DataSetToList<BuySellOrderSummaryDto>(DataSets.Tables[1]).ToList();
            dashboard.buySellAllocations = CustomConvert.DataSetToList<BuySellAllocationSummaryDto>(DataSets.Tables[2]).ToList();
            dashboard.myActivities = CustomConvert.DataSetToList<MyActivitySummaryDto>(DataSets.Tables[3]).ToList();
            dashboard.pendingApprovals = CustomConvert.DataSetToList<PendingApprovalDto>(DataSets.Tables[4]).ToList();

            return dashboard;
        }
    }
}
