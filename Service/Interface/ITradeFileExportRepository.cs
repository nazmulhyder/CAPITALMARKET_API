using Model.DTOs.TradeFileExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITradeFileExportRepository
    {
        public Task<object> ListAccount(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);
        public Task<string> InsertUpdateAccountGroup(string UserName, int CompanyID, int BranchID, SLAgrGrpTradeExportDto data);
        public Task<List<SLAgrGrpTradeExportDto>> ListAccountGroup(string UserName, int CompanyID, int BranchID);
        public Task<SLAgrGrpTradeExportDto> AccountGroupDetail(string UserName, int CompanyID, int BranchID, string AgrGrpID);

        public Task<object> TradeFileExport(string UserName, int CompanyID, int BranchID, string TransactionDate, string AgrGrpIDs);
        
        public Task<object> PayInPayOutFileExport(string UserName, int CompanyID, int BranchID, int ExchangeID, string SettlementDate, string FileType);

    }
}
