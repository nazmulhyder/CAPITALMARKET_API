using Model.DTOs.Approval;
using Model.DTOs.Divident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDividendRepository
    {
        public Task<string> InsertUpdateDevidendDeclaration(int CompanyID, int BranchID, string userName, DividendDisbursementDto entry);
        public Task<object> CalculateTotalDividendPayable(int CompanyID, int BranchID, string UserName, DivCalculationParamDto divCalculationParam);
        public Task<List<DividendDisbursementDto>> ListDividendAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int FundID);
        public Task<string> DividendApprovalAML(string userName, int CompanyID, int branchID, DividendApprovalDto approvalDto);

        // cash dividend distribution
        public Task<List<DividendDisbursementDto>> CashDividendInfoListAML(int CompanyID, int BranchID, int FundID);
        public Task<object> CashDividendDistributionListAML(int CompanyID, int BranchID, int MFDividendDecID);
        public Task<string> InsertUpdateCashDividendDistribution(int CompanyID, int BranchID, string userName, List<CashDividendDistribution> cashDividendDistributions);
        public Task<object> GetCashDividendApprovalAML(int CompanyID, int BranchID, string userName, int FundID);
        public Task<string> CashDividentDistributionApproval(string userName, int CompanyID, int branchID, CashDividentDistributionApprovalDto approvalDto);
        public  Task<object> GetCashDivByRecord(int CompanyID, int BranchID, string userName, int DeclarationID);


        // stock dividend distribution
        public Task<List<DividendDisbursementDto>> StockDividendInfoListAML(int CompanyID, int BranchID, int FundID);
        public Task<List<CIPDividendDistribution>> StockDividendDistributionListAML(int CompanyID, int BranchID, string MFDividendDecIDs);
        public Task<string> InsertUpdateStockDividendDistribution(int CompanyID, int BranchID, string userName, List<CIPDividendDistribution> cipDividendDistributions);
        public Task<object> GetCIPDividendApprovalAML(int CompanyID, int BranchID, string userName, int FundID);
        public Task<string> StockDividentDistributionApproval(string userName, int CompanyID, int branchID, StockDividentDistributionApprovalDto approvalDto);
        public Task<object> GetStockDivByRecord(int CompanyID, int BranchID, string userName, int DeclarationID);
        public Task<object> GetGLBalance(int CompanyID, int BranchID, int FundID, string GLCode);

        //dividend allocation
        public Task<object> DividentAllotcationDistribution(int CompanyID, int BranchID, int DivDeclarationID, string PayoutType, decimal Nav);


    }
}
