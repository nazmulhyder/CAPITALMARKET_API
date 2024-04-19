using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs;
using Model.DTOs.IPO;
using Model.DTOs.Right;

namespace Service.Interface
{
    public interface ICorpActRepository
    {
        public Task<string> InsertRightInstrumentSetting(RightDTO entryRightDTO, int CompanyID, int BranchID, string Maker);
        public Task<List<RightDTO>> GetAllCorpActResult(int CompanyID, int BranchID);
        public Task<CorpActDetailsDTO> GetCorpActRightListbyID(int RightInstSettingID);
        public Task<string> GetCorpActApproval(RightApproveDTO objCorpActRightApproval, string UserName);
        public InstrumentApplicationRights GetInstrumentInformationforRightsApplication(int RightInstSettingID, int CompanyID, int BranchID);
        public object GetInvestorInformationforRightsApplication(int RightInstSettingID, int ProductID, string AccountNumber, int CompanyID, int BranchID);
        public Task<string> InsertRightApplication(RightApplicatonDTO entryRightAppDTO, int CompanyID, int BranchID, string Maker);
        public Task<RightApplicatonListDTO> GetRightsApplicationListbyID(int RightsApplicationID, int CompanyID, int BranchID);
        public Task<List<RightsApplicationforApprovalDTO>> GetRightsApplicationOrderList(int RightInstSettingID, string Status, int CompanyID, int BranchID); //
        public Task<List<RightsApplicationforApprovalDTO>> GetRightsApplicationforApproval(int RightInstSettingID, string Status, int CompanyID, int BranchID);
        public Task<string> RightsApplicationApproved(RightsApplication RightApplicatonListApproved, int CompanyID, int BranchID, string UserName);
        public Task<List<RightsReversalDTO>> GetRightsReversalList(int CompanyID, int BranchID, string UserName);
        public Task<string> InsertCARReversalApproval(RightsReversalMaster entryRightsReversal, int CompanyID, int BranchID, string UserName);
        public Task<List<RightsBulkApproved>> GetRightsApplicationBulkList(int ProductID, int RightInstSettingID, string Maker, int CompanyID, int BranchID);
        public Task<string> InsertCARightBulk(RightsBulkMaster entryRightsBulk, int CompanyID, int BranchID, string UserName);
        public Task<List<RightsCollection>> GetAMLCARInstrumentCollectionList(int RightInstSettingID, int CompanyID, int BranchID);
        public Task<string> AMLCorpActRightInstrumentApproveRequest(AMLRightsCollectionMaster AMLCorpActRightCollectionReq, int CompanyID, int BranchID, string UserName);
        public Task<List<AMLCorpActRightCollectionApprovalList>> GetCorpActRightCollectionApprovalList(int RightInstSettingID,int CompanyID, int BranchID, string UserName);
        public Task<String> AMLCorpActRightInstrumentApproved(AMLCorpActRightCollectionApproved AMLCorpActRightInstrumentList, int CompanyID, int BranchID, string Maker);
        public Task<List<RightDTO>> GetAllCorpActDeclarationApproval(int CompanyID, int BranchID);
        public Task<string> CARApplicationApprovedbyID(RightsApplication data, int RightsApplicationID, int CompanyID, int BranchID, string UserName);
        //public Task<string> InsertCorpActRightDividend(CorpActRightDeclarationDTO entryCorpActRightDeclaration, int CompanyID, int BranchID, string Maker);
        //public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivResult(int CompanyID, int BranchID);
        //public Task<CorpActRightDeclarationListDTO> GetCorpActDividendListbyID(int CorpActDeclarationID);
        //public Task<string> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval, string UserName);
        //public Task<List<ILAMLCorpActionClaimListDTO>> GetCorpActionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        //public Task<string> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName);
        //public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName);
        //public Task<string> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID, string UserName);
        //public Task<string> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID, string Maker);
        //public Task<List<ILAMLCorpActionCashClaimListDTO>> GetCorpActionCashClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        //public Task<string> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID, string UserName);
        //public Task<List<ILAMLCorpActionCashClaimListDTO>> GetILAMLCashClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName);
        //public Task<string> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID, string Maker);
        //public Task<string> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID, string UserName);

    }
}
