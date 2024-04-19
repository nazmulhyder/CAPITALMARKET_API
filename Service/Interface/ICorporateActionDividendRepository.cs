using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Right;

namespace Service.Interface
{
    public interface ICorporateActionDividendRepository
    {
        public Task<List<SLCorpActionBonusFractionCollectionListDTO>> GetOnlySLCorpActionBonusFractionCollectionList(int Productid, int CorActDeclarationID, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertOnlySLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID, string Maker);
        public  Task<string> InsertSLCorpActBonusCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCorpActRightDividend(CorpActRightDeclarationDTO entryCorpActRightDeclaration, int CompanyID, int BranchID, string Maker);
        public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivResult(int CompanyID, int BranchID);
        public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivDeclaration(int CompanyID, int BranchID);
        public Task<CorpActRightDeclarationListDTO> GetCorpActDividendListbyID(int CorpActDeclarationID);
        public Task<string> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval, string UserName);
        public Task<List<ILAMLCorpActionClaimListDTO>> GetCorpActionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        public Task<string> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName);
        public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName);
        public Task<string> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID, string UserName);
        public Task<string> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID, string Maker);
        public Task<List<ILAMLCorpActionCashClaimListDTO>> GetCorpActionCashClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        public Task<string> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID, string UserName);
        public Task<List<ILAMLCorpActionCashClaimListDTO>> GetILAMLCashClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName);
        public Task<string> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID, string UserName);
        public Task<string> InsertSLCorpActDivCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActDivCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<SLCorporateActionCashCollectionListDTO>> GetAllCorpActCashCollectionResult(int CorActDeclarationID,int CompanyID, int BranchID, string Maker);
        public Task<List<ILAMLCorpActionBonusFractionClaimListDTO>> ILAMLCorpActionBonusFractionClaimApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetILAMLUpdateBonusFractionClaimApprove(ILAMLCorpActionBonusFractionUpdate objILAMLUpdateBonusFractionClaimApprove, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertILAMLCorpActDividendBonusFractionClaimApproval(ILAMLCorpActionBonusFractionClaim entryBonusFractionApproval, int CompanyID, int BranchID, string UserName);
        public Task<List<SLCorpActionDividendCollectionList>> GetSLCorpActionCashCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker);
        public Task<List<ILAMLCorpActionBonusFractionClaimListDTO>> GetCorpActionBonusFractionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        public Task<string> ILAMLCorpActionBonusFractionClaim(ILAMLCorpActionBonusFractionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName);
        //public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CompanyID, int BranchID, string UserName);
        public Task<string> InsertSLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> GetSLUpdateNetAmountSLCorpActDivCollection(SLCorpActionDividendCollectionDTO objSLUpdateNetAmountSLCorpActDivCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<SLCorpActionBonusFractionCollectionListDTO>> GetSLCorpActionBonusFractionCollectionList(int CompanyID, int BranchID, int CorActDeclarationID, string Maker);
        public Task<string> GetSLUpdateNetAmountSLCorpActBonusFrcCollection(SLCorpActionDividendBonusFractionCollectionDTO objSLUpdateNetAmountSLCorpActBonusFrcCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> GetSLCorpActDivCollectionApprove(SLCorporateActionCashCollectionApproveDTO objSLCorpActDivCollectionApprove, int CompanyID, int BranchID, string Maker);
        public Task<string> GetSLCorpActDivBonusFrcCollectionApprove(SLCorpActionDividendBonusFractionApproveDTO objSLCorpActDivBonusFrcCollectionApprove, int CompanyID, int BranchID, string Maker);
        public Task<List<SLCorpActionBonusFractionCollectionApprovalListDTO>> GetSLCorpActionBonusFractionCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker);
        public Task<List<ILAMLCorpActionBonusCollectionListDTO>> GetILAMLCorpActionBonusCollectionList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker);
        public Task<string> InsertILAMLCorpActBonusCollection(ILAMLCorpActionBonusCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<ILAMLCorpActionBonusCollectionApprovalListDTO>> GetILAMLCorpActionBonusCollectionApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetILAMLUpdateBonusCollectionApprove(ILAMLCorpActionBonusCollectionUpdateDTO objILAMLUpdateBonusCollectionApprove, int CompanyID, int BranchID, string Maker);
        public Task<string> GetILAMLApprovedCorpActDivBonusCollection(ILAMLCorpActionDividendBonusCollectionApproveDTO objILAMLApprovedCorpActDivBonusCollection, int CompanyID, int BranchID, string UserName);
        public Task<List<SLCorporateActionCashCollectionListDTO>> GetSLCorpActCashCollectionResult(int ProductID, int CorActDeclarationID, int CompanyID, int BranchID, string Maker);
    }
}
