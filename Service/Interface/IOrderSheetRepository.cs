using Model.DTOs.OrderSheet;
using Model.DTOs.Withdrawal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderSheetRepository
    {
        public Task<List<ListOrdersheetDTO>> ListOrdersheetSL(int CompanyID, int branchID, int PageNo, int Perpage, string FilterType, string SearchKeyword, string ListType);
        public Task<List<ListOrdersheetDTO>> ListOrdersheetIL(int CompanyID, int branchID, int PageNo, int Perpage, string FilterType, string SearchKeyword, string ListType);

        public Task<OrderSheetDTO> GetOrdersheetDetailsSL(string UserName, int CompanyID, int BranchID, int ContractID);
        public Task<OrderSheetDTO> GetOrdersheetDetailsIL(string UserName, int CompanyID, int BranchID, int ContractID);

        public Task<string> UpdateOrdersheetSL(int companyId, int branchID, string userName, OrderSheetDTO entityDto);
        public Task<string> UpdateOrdersheetIL(int companyId, int branchID, string userName, OrderSheetDTO entityDto);

        public Task<string> OrdersheetApprovalSL(string userName, int CompanyID, int branchID, OrdersheetApprovalDTO approvalDto);
        public Task<string> OrdersheetApprovalIL(string userName, int CompanyID, int branchID, OrdersheetApprovalDTO approvalDto);

        public Task<List<OrderSheetPrintDTO>> GetListOrdersheetPrintSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);
        public Task<List<OrderSheetPrintDTO>> GetListOrdersheetPrintIL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);

        public string GetOrdersheetPrintStringSL(string UserName, int companyID, int BranchID, List<OrderSheetPrintDTO> orderSheets);
        public string GetOrdersheetPrintStringIL(string UserName, int companyID, int BranchID, List<OrderSheetPrintDTO> orderSheets);
        //public Task<string> UpdateOrdersheetPrintStatusSL(int companyId, int branchID, string userName, int ContractID);
        public Task<ReleasedOrdersheetDTO> GetAvailableOrdersheetReleaseSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);
        public Task<ReleasedOrdersheetDTO> GetAvailableOrdersheetReleaseIL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber);

        public Task<string> UpdateOrdersheetReleaseStatusSL(int companyId, int branchID, string userName,  OrdersheetReleasedDTO releasedOrdersheet);
        public Task<string> UpdateOrdersheetReleaseStatusIL(int companyId, int branchID, string userName, OrdersheetReleasedDTO releasedOrdersheet);

        public Task<List<ZeroOrdersheetListDTO>> GetListZeroOrdersheetSL(string UserName, int CompanyID, int BranchID, int ProductID, int NoOfRemainingSheet);
        public Task<List<ZeroOrdersheetListDTO>> GetListZeroOrdersheetIL(string UserName, int CompanyID, int BranchID, int ProductID, int NoOfRemainingSheet);
    }
}
