using Model.DTOs.Approval;
using Model.DTOs.SellSurrender;
using Model.DTOs.UnitPurchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISellSurrenderRepository
    {
        public Task<object> ListUnapprovedUnitPurchaseDetail(int CompanyID, int BranchID, int ContractID );
        public Task<object> GetSellSurrenderMinimumUnitSetup(int CompanyID, int BranchID, string userName, int FundID, string AccountType);

		public Task<string> InsertUpdateMinimumUnitSetup(int CompanyID, int BranchID, string userName, SS_MinimumUnitSetupDto entry);
        public Task<string> InsertUpdateUnitIssueForSale(int CompanyID, int BranchID, string userName, SS_UnitIssueForSaleDto entry);
        public Task<object> GetAllUnitIssueForSaleByFund(int CompanyID, int BranchID, string UserName, int FundID);
        public Task<string> InsertUpdateUnitPurchaseDetailSetup(int CompanyID, int BranchID, string userName, UnitPurchaseDto entry);
        public Task<object> GetCustomerInfoForPurchase(int CompanyID, int BranchID, string UserName, int ContractID);
        public Task<object> ListUnitPurchaseDetail(int CompanyID, int BranchID, SurrenderFilterDto purchaseFilter);
        public Task<object> GetQueryUnitPurchaseDetail(int CompanyID, int BranchID, string UserName, int PurchaseDetailID);
        public Task<string> UnitPurchaseDetailApproval(string userName, int CompanyID, int branchID, ApproveUnitPurchaseDetailDto approvalDto);
        public Task<object> ListUnitActivationRequestAML(int CompanyID, int BranchID, string UserName, int ProductID);
        public Task<object> InsertUnitActivation(int CompanyID, int BranchID, string userName, string PurchaseDetailIDs);
        //public CDBLFileDataDto getAllCDBLData(int ComID, int BrnchID, string UserName, string PurchaseDetailIDs);
        public Task<object> GetGetListOfUnitRequestActivationAML(int CompanyID, int BranchID, string UserName, int ProductID);
        public Task<string> UpdateUnitActivation(int CompanyID, int BranchID, string userName, SS_UpdateActivateRequestDto sS_UpdateActivateReq);
        public Task<object> GetQueryUnitSurrenderAML(int CompanyID, int BranchID, string UserName, string AccNo, int ProductID);
        public Task<string> UpdateUnitSurrenderSetup(int CompanyID, int BranchID, string userName, SS_UnitSurrenderDto entry);
        public Task<List<SS_UnitSurrenderDto>> ListUnitSurrenderAML(int CompanyID, int BranchID, string UserName, SurrenderFilterDto surrenderFilter);
        public Task<string> UnitSurrenderApproval(string userName, int CompanyID, int branchID, ApproveUnitSurrenderDto approvalDto);
        public Task<SS_UnitSurrenderDto> GetUnitSurrenderDetailAML(int CompanyID, int BranchID, string UserName, int SurrenderDetailID);
        public Task<List<SS_UnitSurrenderDto>> GetAllUnitSurrenderApprovalListAML(int CompanyID, int BranchID, string UserName, int ProductID);
        public Task<object> FundCollectionForPurchaseAML(int CompanyID, int BranchID, string UserName);
       

    }
}
