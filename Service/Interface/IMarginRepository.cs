using Model.DTOs.MarginRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMarginRepository
    {
        public Task<MarginRequestClientInfo> GetClientInfoForMarginRequest(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo);
        public Task<GetMarginRquestDto> InsertUpdateMarginRequest(int CompanyID, int BranchID, string userName, MarginRquestDto entry);
        public Task<List<ListMarginRequests_ViewtStatusDto>> ListViewMarginRequestStatus(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<List<ListMarginRequests_ViewtStatusDto>> ListViewCompletedMarginRequestStatus(int CompanyID, int BranchID, FilterDto filter);
        public Task<GetMarginRquestDto> GetMarginRequestById(int CompanyID, int BranchID, string UserName, int MarginReqID);
        public Task<MarginMonitoringSMSEmailDto> ListMarginMonitoring(int CompanyID, int BranchID, string UserName);
        public Task<string> MarginMonitoringSMSEmailSent(int CompanyID, int BranchID, string UserName, MarginMonitoringSMSEmailDto marginMonitoringSMSEmail);
        public Task<List<CodesMarginRequestDto>> CodesMarginRequest(int CompanyID, int BranchID, string UserName, string TypeName);
        public Task<List<CodesMarginJson>> AllCodesMargin(int CompanyID, int BranchID, string UserName, int MarginReqID);

    }
}
