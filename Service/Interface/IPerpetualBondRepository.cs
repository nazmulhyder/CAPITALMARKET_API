using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.PerpetualBond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPerpetualBondRepository
    {
        public Task<List<PB_ActiveBondInstrument>> PBActiveBondInstrumentList(int CompanyID, int BranchID);
        public Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntries(int CompanyID, int BranchID, int InstrumentID);
        public Task<string> InsertCouponColDeclaration(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry);
        public Task<List<BondNewDeclaredInstrument>> BondNewDeclaredInstrument(int CompanyID, int BranchID);
        public Task<List<PerpetualBond>> GetPerpetualBondHoldings(int CompanyID, int BranchID, int InstrumentID, int ProductID, int DeclarationID);
        public Task<object> GetPerpetualBondHoldings_Claim(int CompanyID, int BranchID, int InstrumentID, string Year, int DeclarationID);
        public Task<string> InsertPerpetualBond(int CompanyID, int BranchID, string userName, List<PerpetualBondClaim> perpetualBonds);
        public Task<string> InsertPerpetualBondClaim(int CompanyID, int BranchID, string userName, List<PerpetualBond> perpetualBonds, int DeclarationID, int ProductID);

        public Task<List<ListPerpetualBond>> ListPerpetualBond(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<string> PerpetualBondApproval(string userName, int CompanyID, int branchID, PerpetualBondApprovalDto approvalDto);
        public Task<string> PerpetualBondDeclarationApproval(string userName, int CompanyID, int branchID, PerpetualBondDeclarationApprovalDto approvalDto);
        public Task<List<PerpetualBondDeclarationDto>> ListPerpetualBondDeclaration(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public  Task<List<PerpetualBondForReversalDto>> ListPerpetualBondForReversal(int CompanyID, int BranchID, int ProductID, string AccountNo, int InstrumentID);
        public Task<string> InsertPerpetualBondReversal(int CompanyID, int BranchID, string userName, List<PerpetualBondReversalDto> perpetualBondReversals);
        public Task<string> PerpetualBondReversalApproval(string userName, int CompanyID, int branchID, PerpetualBondReversalApprovalDto approvalDto);
        public Task<List<CouponCol_reversalListDto>> ListPerpetualBondReversal(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<object> PerpetualBondClaimList(int CompanyID, int BranchID, string Year);
        public Task<object> FilterDeclarationList(int CompanyID, int BranchID, int InstrumentID, string Year);
    }
}
