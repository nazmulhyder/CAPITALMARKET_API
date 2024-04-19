using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Dashborad
{

    public class TradeProcessingSummaryDto
    {
        public string? ExchangeName { get; set; }
        public string? BrokerName { get; set; }
        public string? FileStatus { get; set; }

    }

    public class BuySellOrderSummaryDto
    {
        public string? OrderType { get; set; }
        public string? ProductName { get; set; }
        public string? OrderCount { get; set; }
        public string? MakeDate { get; set; }

    }

    public class BuySellAllocationSummaryDto
    {
        public string? AllocationType { get; set; }
        public string? BatchNo { get; set; }
        public string? TaskStatus { get; set; }
    }

    public class MyActivitySummaryDto
    {
        public string? Title { get; set; }
        public int? NoOfTask { get; set; }
    }

    public class PendingApprovalDto
    {
        public string? Title { get; set; }
        public int? ID { get; set; }
    }

    public class DashboardDTO
    {
        public List<TradeProcessingSummaryDto>? tradeProcessings;
        public List<BuySellOrderSummaryDto>? buySellOrders;
        public List<BuySellAllocationSummaryDto>? buySellAllocations;
        public List<MyActivitySummaryDto>? myActivities;
        public List<PendingApprovalDto>? pendingApprovals;
    }
}
