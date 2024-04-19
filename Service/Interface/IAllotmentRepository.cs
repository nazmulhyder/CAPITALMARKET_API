using Model.DTOs.Allocation;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.OrderSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAllotmentRepository
    {
        public Task<string> InsertUpdateAllotmentIL(int CompanyID, int BranchID, string userName, AllotmentEntryDto entry);
        public Task<string> InsertUpdateAllotmentAML(int CompanyID, int BranchID, string userName, AMLAllotmentEntryDto entry);

        public Task<List<AllotmentGSecList>> GSecForAllotmentListIL(int CompanyID, int BranchID);
        public Task<List<AllotmentGSecList>> GSecForAllotmentListAML(int CompanyID, int BranchID);

        public Task<string> GSecAllotmentApprovalIL(string userName, int CompanyID, int branchID, GsecAllotmentApprovalDto approvalDto);
        public Task<string> GSecAllotmentApprovalAML(string userName, int CompanyID, int branchID, GsecAllotmentApprovalDto approvalDto);


        public Task<List<GSecAllotmentListDto>> ListGSecAllotmentIL(int CompanyID, int BranchID, FilterGSecAllotment filter);
        public Task<List<GSecAllotmentListDto>> ListGSecAllotmentAML(int CompanyID, int BranchID, FilterGSecAllotment filter);



        public Task<GetGSecAllotmentDto> GetGSecAllotmentIL(string UserName, int CompanyID, int BranchID, int OMIBuyID);
        public Task<GetGSecAllotmentDto> GetGSecAllotmentAML(string UserName, int CompanyID, int BranchID, int OMIBuyID);

        public Task<List<CouponCol_GsecInsHoldingDto>> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, int InstrumentID);
        public Task<List<CouponCol_GSecDto>> GsecListForCouponCollection(int CompanyID, int BranchID);

        public Task<string> InsertCouponColDeclarationIL(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry);
        public Task<string> InsertCouponColDeclarationAML(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry);
        public Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntriesIL(int CompanyID, int BranchID, int InstrumentID);
        public Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntriesAML(int CompanyID, int BranchID, int InstrumentID);

        public Task<string> InsertCouponCollectionIL(int CompanyID, int BranchID, string userName, CouponCollectionEntryDto couponCollection);
        public Task<string> InsertCouponCollectionAML(int CompanyID, int BranchID, string userName, CouponCollectionEntryDto couponCollection);

        public Task<List<CouponCol_approvalListDto>> ListCouponCollectionIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<List<CouponCol_approvalListDto>> ListCouponCollectionAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);

        public Task<string> CouponCollectionApprovalIL(string userName, int CompanyID, int branchID, CouponCollectionApprovalDto approvalDto);
        public Task<string> CouponCollectionApprovalAML(string userName, int CompanyID, int branchID, CouponCollectionApprovalDto approvalDto);


        public Task<List<CouponCol_getCollectedCoupon>> GetCollectionCouponForReversal(int CompanyID, int BranchID, int ProductID, string AccountNo, int instrumentID, string FromDate, string ToDate, int FundID);
       
        public Task<string> InsertCouponCollectionReversalIL(int CompanyID, int BranchID, string userName, CouponCol_reversalDto entry);
        public Task<string> InsertCouponCollectionReversalAML(int CompanyID, int BranchID, string userName, CouponCol_reversalDto entry);

        public  Task<List<CouponCol_reversalListDto>> ListCouponCollectionReversalIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<List<CouponCol_reversalListDto>> ListCouponCollectionReversalAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);

        public Task<string> CouponCollectionReversalApprovalIL(string userName, int CompanyID, int branchID, CouponCollectionReversalApprovalDto approvalDto);
        public Task<string> CouponCollectionReversalApprovalAML(string userName, int CompanyID, int branchID, CouponCollectionReversalApprovalDto approvalDto);

        public Task<List<GsecOffMktInsSaleHoldingDto>> GetAllOffMktSaleInsHolding(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID);
        public Task<string> InsertOffMktInsSaleIL(int CompanyID, int BranchID, string userName, GSecOffMktInsSaleDto entry);
        public Task<string> InsertOffMktInsSaleAML(int CompanyID, int BranchID, string userName, GSecOffMktInsSaleDto entry);

        public Task<string> OffMktSaleApprovalIL(string userName, int CompanyID, int branchID, OffMktSaleApprovalDto approvalDto);
        public Task<string> OffMktSaleApprovalAML(string userName, int CompanyID, int branchID, OffMktSaleApprovalDto approvalDto);

        public Task<List<ListGSecOffMktInsSaleDto>> ListGSecOffMktInsSaleIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<List<ListGSecOffMktInsSaleDto>> ListGSecOffMktInsSaleAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);

        public Task<object> GetRedemptionInsHoldingIL(int CompanyID, int BranchID, int InstrumentID);

        public Task<object> GetRedemptionInsHoldingAML(int CompanyID, int BranchID, int InstrumentID);

        public Task<string> InsertGSecRedemptionIL(string userName, int CompanyID, int branchID, InstrumentRedemption instrumentRedemption);
        public Task<string> InsertGSecRedemptionAML(string userName, int CompanyID, int branchID, InstrumentRedemptionAML instrumentRedemption);
        public Task<string> GSecInstrumentRedemptionApprovalIL(string userName, int CompanyID, int branchID, InstrumentRedemptionApprovalDto approvalDto);
        public Task<string> GSecInstrumentRedemptionApprovalAML(string userName, int CompanyID, int branchID, InstrumentRedemptionApprovalDto approvalDto);

        public Task<List<InsRed_GetList>> ListGSecInsRedemption(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType);
        public Task<List<GSecRedemption>> GSecForRedemptionListIL(int CompanyID, int BranchID, string ListType);
        public Task<List<GSecRedemption>> GSecForRedemptionListAML(int CompanyID, int BranchID, string ListType);
        public Task<object> GetRedemptionByIDIL(int CompanyID, int BranchID, int InstrumentID);
        public Task<object> GetRedemptionByIDAML(int CompanyID, int BranchID, int InstrumentID);

        public Task<object> TBillZCouponRedemptionListAML(int CompanyID, int BranchID, int FundID, string ListType, string FromDate, string ToDate);

        public string GetCleanPrice(int ComID, int BranchID, CalculateCleanPrice calculateCleanPrice);
        public Task<object> CalculateSettlementValue(int CompanyID, int BranchID, SettlementCalculate settlementCalculate);
    }
}
