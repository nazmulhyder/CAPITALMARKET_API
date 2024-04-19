using Dapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using IronXL;
using Model.DTOs.Allocation;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.Depository;
using Model.DTOs.OrderSheet;
using Model.DTOs.PriceFileUpload;
using Model.DTOs.TradeRestriction;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
    public class AllotmentRepository : IAllotmentRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public AllotmentRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<string> GSecAllotmentApprovalIL(string userName, int CompanyID, int branchID, GsecAllotmentApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@OMIBuyIDs", approvalDto.OMIBuyIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveGSecAllotmentIL", SpParameters);
        }

        public async Task<string> GSecAllotmentApprovalAML(string userName, int CompanyID, int branchID, GsecAllotmentApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@OMIBuyIDs", approvalDto.OMIBuyIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveGSecAllotmentAML", SpParameters);
        }

        public async Task<List<AllotmentGSecList>> GSecForAllotmentListIL(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<AllotmentGSecList>("CM_ListGsecForAllotmentIL", values);
        }
        public async Task<List<AllotmentGSecList>> GSecForAllotmentListAML(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<AllotmentGSecList>("CM_ListGsecForAllotmentAML", values);
        }

        public Task<string> InsertUpdateAllotmentIL(int CompanyID, int BranchID, string userName, AllotmentEntryDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateGSecInsAllotmentIL";

                #region Insert New Data

                if (entry.OMIBuyID == 0 || entry.OMIBuyID == null)
                {


                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@OMIBuyID", entry.OMIBuyID);
                    SpParameters.Add("@ContractID", entry.ContractID);
                    SpParameters.Add("@InstrumentID", entry.InstrumentID);
                    SpParameters.Add("@Operation", entry.Operation);
                    SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                    SpParameters.Add("@UnitPrice", entry.UnitPrice);
                    SpParameters.Add("@Yield", entry.Yield);
                    SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                    SpParameters.Add("@AuctionNo", entry.AuctionNo);
                    SpParameters.Add("@SettlementAmount", entry.SettlementAmount);
                    SpParameters.Add("@SettlementDate", Utility.DatetimeFormatter.DateFormat(entry.SettlementDate));
                    SpParameters.Add("@CleanPrice", entry.CleanPrice);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@OMIBuyID", entry.OMIBuyID);
                    SpParameters.Add("@ContractID", entry.ContractID);
                    SpParameters.Add("@InstrumentID", entry.InstrumentID);
                    SpParameters.Add("@Operation", entry.Operation);
                    SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                    SpParameters.Add("@UnitPrice", entry.UnitPrice);
                    SpParameters.Add("@Yield", entry.Yield);
                    SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                    SpParameters.Add("@AuctionNo", entry.AuctionNo);
                    SpParameters.Add("@SettlementAmount", entry.SettlementAmount);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> InsertUpdateAllotmentAML(int CompanyID, int BranchID, string userName, AMLAllotmentEntryDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateGSecInsAllotmentAML";

                #region Insert New Data

                if (entry.OMIBuyID == 0 || entry.OMIBuyID == null)
                {


                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@OMIBuyID", entry.OMIBuyID);
                    SpParameters.Add("@ContractID", entry.ContractID);
                    SpParameters.Add("@InstrumentID", entry.InstrumentID);
                    SpParameters.Add("@Operation", entry.Operation);
                    SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                    SpParameters.Add("@UnitPrice", entry.UnitPrice);
                    SpParameters.Add("@Yield", entry.Yield);
                    SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                    SpParameters.Add("@AuctionNo", entry.AuctionNo);
                    SpParameters.Add("@SettlementAmount", entry.SettlementAmount);
                    SpParameters.Add("@FundID", entry.FundID);
                    SpParameters.Add("@BankAccountID", entry.BankAccountID);
                    SpParameters.Add("@FaceValue", entry.FaceValue);
                    SpParameters.Add("@NoOfDays", entry.NoOfDays);
                    SpParameters.Add("@IssueDate", Utility.DatetimeFormatter.DateFormat(entry.IssueDate));
                    SpParameters.Add("@MaturityDate", Utility.DatetimeFormatter.DateFormat(entry.MaturityDate));
                    SpParameters.Add("@IsTBond", entry.IsTBond);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@OMIBuyID", entry.OMIBuyID);
                    SpParameters.Add("@ContractID", entry.ContractID);
                    SpParameters.Add("@InstrumentID", entry.InstrumentID);
                    SpParameters.Add("@Operation", entry.Operation);
                    SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                    SpParameters.Add("@UnitPrice", entry.UnitPrice);
                    SpParameters.Add("@Yield", entry.Yield);
                    SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                    SpParameters.Add("@AuctionNo", entry.AuctionNo);
                    SpParameters.Add("@SettlementAmount", entry.SettlementAmount);
                    SpParameters.Add("@FundID", entry.FundID);
                    SpParameters.Add("@BankAccountID", entry.BankAccountID);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GSecAllotmentListDto>> ListGSecAllotmentIL(int CompanyID, int BranchID, FilterGSecAllotment filter)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID,  ListType = filter.ListType, InstrumentType = filter.InstrumentType, FundID = filter.FundID, FromDate  = Utility.DatetimeFormatter.DateFormat(filter.FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(filter.ToDate) };
            return await _dbCommonOperation.ReadSingleTable<GSecAllotmentListDto>("CM_ListGSecAllotmentIL", values);
        }

        public async Task<List<GSecAllotmentListDto>> ListGSecAllotmentAML(int CompanyID, int BranchID, FilterGSecAllotment filter)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ListType = filter.ListType, InstrumentType = filter.InstrumentType, FundID = filter.FundID, FromDate = Utility.DatetimeFormatter.DateFormat(filter.FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(filter.ToDate) };
            return await _dbCommonOperation.ReadSingleTable<GSecAllotmentListDto>("CM_ListGSecAllotmentAML", values);
        }


        public async Task<GetGSecAllotmentDto> GetGSecAllotmentIL(string UserName, int CompanyID, int BranchID, int OMIBuyID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, OMIBuyID = OMIBuyID };
            var result = await _dbCommonOperation.ReadSingleTable<GetGSecAllotmentDto>("CM_GetGSecAllotmentIL", values);
            return result.FirstOrDefault();
        }
        public async Task<GetGSecAllotmentDto> GetGSecAllotmentAML(string UserName, int CompanyID, int BranchID, int OMIBuyID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, OMIBuyID = OMIBuyID };
            var result = await _dbCommonOperation.ReadSingleTable<GetGSecAllotmentDto>("CM_GetGSecAllotmentAML", values);
            return result.FirstOrDefault();
        }


        public async Task<List<CouponCol_GsecInsHoldingDto>> GetAllGSecInstrumentHolding(int CompanyID, int BranchID, int InstrumentID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_GsecInsHoldingDto>("CM_GetAllGSecInstrumentHolding", values);

        }

        public async Task<List<CouponCol_GSecDto>> GsecListForCouponCollection(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_GSecDto>("CM_ListCouponCollectionGsecIL", values);
        }

        public async Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntriesIL(int CompanyID, int BranchID, int InstrumentID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_declarationDto>("CM_ListCouponColLast_3_Declaration_IL", values);
        }

        public async Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntriesAML(int CompanyID, int BranchID, int InstrumentID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_declarationDto>("CM_ListCouponColLast_3_Declaration_AML", values);
        }

        public async Task<string> InsertCouponColDeclarationIL(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry)
        {
            try
            {
                string sp = "CM_InsertCouponCollectionDeclarationIL";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DeclarationID", entry.DeclarationID);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@InterestRate", entry.InterestRate);
                SpParameters.Add("@RecordDate", Utility.DatetimeFormatter.DateFormat(entry.RecordDate));
                SpParameters.Add("@TenorStartDate", string.IsNullOrEmpty(entry.TenorStartDate) ? null : Utility.DatetimeFormatter.DateFormat(entry.TenorStartDate));
                SpParameters.Add("@TenorEndDate", string.IsNullOrEmpty(entry.TenorEndDate) ? null : Utility.DatetimeFormatter.DateFormat(entry.TenorEndDate));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> InsertCouponColDeclarationAML(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry)
        {
            try
            {
                string sp = "CM_InsertCouponCollectionDeclarationAML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DeclarationID", entry.DeclarationID);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@InterestRate", entry.InterestRate);
                SpParameters.Add("@RecordDate", Utility.DatetimeFormatter.DateFormat(entry.RecordDate));
                SpParameters.Add("@TenorStartDate", string.IsNullOrEmpty(entry.TenorStartDate) ? null : Utility.DatetimeFormatter.DateFormat(entry.TenorStartDate));
                SpParameters.Add("@TenorEndDate", string.IsNullOrEmpty(entry.TenorEndDate) ? null : Utility.DatetimeFormatter.DateFormat(entry.TenorEndDate));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> InsertCouponCollectionIL(int CompanyID, int BranchID, string userName, CouponCollectionEntryDto couponCollection)
        {
            try
            {
                string sp = "CM_InsertCouponCollectionIL";

                #region Insert New Data

                foreach(var oitem in couponCollection.CouponCollections)
                {
                    oitem.InstCollectionDate =  string.IsNullOrEmpty(oitem.InstCollectionDate)?  Utility.DatetimeFormatter.DateFormat(oitem.InstCollectionDate) : "";
                    oitem.TransactionDate = string.IsNullOrEmpty(oitem.TransactionDate) ? Utility.DatetimeFormatter.DateFormat(oitem.TransactionDate) : "";
                    oitem.MakeDate = string.IsNullOrEmpty(oitem.MakeDate) ? Utility.DatetimeFormatter.DateFormat(oitem.MakeDate) : "";
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DeclarationID", couponCollection.DeclarationID);
                SpParameters.Add("@BankAccountID", couponCollection.BankAccountID);
                SpParameters.Add("@FundID", couponCollection.FundID);
                SpParameters.Add("@InstrumentNumber", couponCollection.InstrumentNumber);
                //SpParameters.Add("@InstColletionID", entry.InstColletionID);
                //SpParameters.Add("@ContractID", entry.ContractID);
                //SpParameters.Add("@InstrumentID", entry.InstrumentID);
                //SpParameters.Add("@InterestAmount", entry.InterestAmount);
                //SpParameters.Add("@AIT", entry.AIT);
                //SpParameters.Add("@CollectionAmount", entry.CollectionAmount);
                //SpParameters.Add("@Remarks", entry.Remarks);
                //SpParameters.Add("@DeclarationID", entry.DeclarationID);
                //SpParameters.Add("@BankAccountID", entry.BankAccountID);
                //SpParameters.Add("@InstrumentNumber", entry.InstrumentNumber);
                SpParameters.Add("@InstallmentCollections", ListtoDataTableConverter.ToDataTable(couponCollection.CouponCollections).AsTableValuedParameter("Type_GsecCouponCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> InsertCouponCollectionAML(int CompanyID, int BranchID, string userName, CouponCollectionEntryDto couponCollection)
        {
            {
                try
                {
                    string sp = "CM_InsertCouponCollectionAML";

                    #region Insert New Data

                    foreach (var oitem in couponCollection.CouponCollections)
                    {
                        oitem.InstCollectionDate = string.IsNullOrEmpty(oitem.InstCollectionDate) ? Utility.DatetimeFormatter.DateFormat(oitem.InstCollectionDate) : "";
                        oitem.TransactionDate = string.IsNullOrEmpty(oitem.TransactionDate) ? Utility.DatetimeFormatter.DateFormat(oitem.TransactionDate) : "";
                        oitem.MakeDate = string.IsNullOrEmpty(oitem.MakeDate) ? Utility.DatetimeFormatter.DateFormat(oitem.MakeDate) : "";
                    }

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@DeclarationID", couponCollection.DeclarationID);
                    SpParameters.Add("@BankAccountID", couponCollection.BankAccountID);
                    SpParameters.Add("@FundID", couponCollection.FundID);
                    SpParameters.Add("@InstrumentNumber", couponCollection.InstrumentNumber);
                    //SpParameters.Add("@InstColletionID", entry.InstColletionID);
                    //SpParameters.Add("@ContractID", entry.ContractID);
                    //SpParameters.Add("@InstrumentID", entry.InstrumentID);
                    //SpParameters.Add("@InterestAmount", entry.InterestAmount);
                    //SpParameters.Add("@AIT", entry.AIT);
                    //SpParameters.Add("@CollectionAmount", entry.CollectionAmount);
                    //SpParameters.Add("@Remarks", entry.Remarks);
                    //SpParameters.Add("@BankAccountID", entry.BankAccountID);
                    SpParameters.Add("@InstallmentCollections", ListtoDataTableConverter.ToDataTable(couponCollection.CouponCollections).AsTableValuedParameter("Type_GsecCouponCollection"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<CouponCol_approvalListDto>> ListCouponCollectionIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_approvalListDto>("CM_ListCouponCollectionIL", values);
        }
        public async Task<List<CouponCol_approvalListDto>> ListCouponCollectionAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_approvalListDto>("CM_ListCouponCollectionAML", values);
        }

        public async Task<string> CouponCollectionApprovalIL(string userName, int CompanyID, int branchID, CouponCollectionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@InstColletionID", approvalDto.InstColletionID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCouponCollectionIL", SpParameters);
        }
        public async Task<string> CouponCollectionApprovalAML(string userName, int CompanyID, int branchID, CouponCollectionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@InstColletionID", approvalDto.InstColletionID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCouponCollectionAML", SpParameters);
        }

        public async Task<List<CouponCol_getCollectedCoupon>> GetCollectionCouponForReversal(int CompanyID, int BranchID, int ProductID, string AccountNo, int instrumentID, string FromDate, string ToDate, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo, instrumentID = instrumentID, FromDate = Utility.DatetimeFormatter.DateFormat(FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(ToDate), FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_getCollectedCoupon>("CM_GetCollectedCouponForReversalIL", values);
        }

        public async Task<string> InsertCouponCollectionReversalIL(int CompanyID, int BranchID, string userName, CouponCol_reversalDto entry)
        {
            try
            {
                string sp = "CM_InsertCouponCollectionReversalIL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IntCollReversalID", 0);
                SpParameters.Add("@ReversalReason", entry.ReversalReason);
                SpParameters.Add("@TransactionID", entry.TransactionID);
                SpParameters.Add("@InstCollectionID", entry.InstCollectionID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> InsertCouponCollectionReversalAML(int CompanyID, int BranchID, string userName, CouponCol_reversalDto entry)
        {
            try
            {
                string sp = "CM_InsertCouponCollectionReversalAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IntCollReversalID", 0);
                SpParameters.Add("@ReversalReason", entry.ReversalReason);
                SpParameters.Add("@TransactionID", entry.TransactionID);
                SpParameters.Add("@InstCollectionID", entry.InstCollectionID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CouponCol_reversalListDto>> ListCouponCollectionReversalIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_reversalListDto>("CM_ListCouponCollectionReversalIL", values);
        }

        public async Task<List<CouponCol_reversalListDto>> ListCouponCollectionReversalAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_reversalListDto>("CM_ListCouponCollectionReversalAML", values);
        }

        public async Task<string> CouponCollectionReversalApprovalIL(string userName, int CompanyID, int branchID, CouponCollectionReversalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollReversalID", approvalDto.IntCollReversalID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCouponCollectionReversalIL", SpParameters);
        }
        public async Task<string> CouponCollectionReversalApprovalAML(string userName, int CompanyID, int branchID, CouponCollectionReversalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollReversalID", approvalDto.IntCollReversalID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCouponCollectionReversalAML", SpParameters);
        }

        public async Task<List<GsecOffMktInsSaleHoldingDto>> GetAllOffMktSaleInsHolding(int CompanyID, int BranchID, int ProductID, string AccountNo, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<GsecOffMktInsSaleHoldingDto>("CM_GetAllOffMktSaleInsHolding", values);
        }

        public async Task<string> InsertOffMktInsSaleIL(int CompanyID, int BranchID, string userName, GSecOffMktInsSaleDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateOffMktSaleIL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@OMISaleID", 0);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@Operation", "Sale");
                SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                //SpParameters.Add("@Amount", entry.Amount);
                SpParameters.Add("@Remarks", entry.Remarks);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@CapitalGain", entry.capitalGain);
                SpParameters.Add("@Yield", entry.Yield);
                SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                SpParameters.Add("@CleanPrice", entry.CleanPrice);
                SpParameters.Add("@SettlementDate", Utility.DatetimeFormatter.DateFormat(entry.SettlementDate));
                SpParameters.Add("@SettlementValue", entry.SettlementValue);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> InsertOffMktInsSaleAML(int CompanyID, int BranchID, string userName, GSecOffMktInsSaleDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateOffMktSaleAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@OMISaleID", 0);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@Operation", "Sale");
                SpParameters.Add("@NoOfUnit", entry.NoOfUnit);
                //SpParameters.Add("@Amount", entry.Amount);
                SpParameters.Add("@Remarks", entry.Remarks);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@BankAccountID", entry.BankAccountID);
                SpParameters.Add("@CapitalGain", entry.capitalGain);
                SpParameters.Add("@Yield", entry.Yield);
                SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                SpParameters.Add("@CleanPrice", entry.CleanPrice);
                SpParameters.Add("@SettlementDate", Utility.DatetimeFormatter.DateFormat(entry.SettlementDate));
                SpParameters.Add("@SettlementValue", entry.SettlementValue);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> OffMktSaleApprovalIL(string userName, int CompanyID, int branchID, OffMktSaleApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@OMISaleID", approvalDto.OMISaleID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveOffMktSaleIL", SpParameters);
        }

        public async Task<string> OffMktSaleApprovalAML(string userName, int CompanyID, int branchID, OffMktSaleApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@OMISaleID", approvalDto.OMISaleID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveOffMktSaleAML", SpParameters);
        }

        public async Task<List<ListGSecOffMktInsSaleDto>> ListGSecOffMktInsSaleIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListGSecOffMktInsSaleDto>("CM_ListOffMktSaleIL", values);
        }

        public async Task<List<ListGSecOffMktInsSaleDto>> ListGSecOffMktInsSaleAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListGSecOffMktInsSaleDto>("CM_ListOffMktSaleAML", values);
        }

        public async Task<object> GetRedemptionInsHoldingIL(int CompanyID, int BranchID, int InstrumentID)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@InstrumentID", InstrumentID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetInstrumentRedemptionIL]", sqlParams);

            var data1 = CustomConvert.DataSetToList<InsRed_getHoldings>(DataSets.Tables[0]).ToList();
            var data2 = CustomConvert.DataSetToList<InsRed_getHoldingsSummary>(DataSets.Tables[1]).FirstOrDefault();

            //return await _dbCommonOperation.ReadSingleTable<InsRed_getHoldings>("CM_GetInstrumentRedemptionIL", values);
            var Result = new
            {
                holdings = data1,
                summary = data2
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> GetRedemptionInsHoldingAML(int CompanyID, int BranchID, int InstrumentID)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@InstrumentID", InstrumentID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetInstrumentRedemptionAML]", sqlParams);

            var data1 = CustomConvert.DataSetToList<InsRed_getHoldings>(DataSets.Tables[0]).ToList();
            var data2 = CustomConvert.DataSetToList<InsRed_getHoldingsSummary>(DataSets.Tables[1]).FirstOrDefault();
            var data3 = CustomConvert.DataSetToList<MFBankList>(DataSets.Tables[2]).ToList();

            foreach (var item in data1)
            {
                var bankList = data3.Where(x => x.FundID == item.FundID).ToList();
                item.bankLists.AddRange(bankList);
            }

            //return await _dbCommonOperation.ReadSingleTable<InsRed_getHoldings>("CM_GetInstrumentRedemptionIL", values);
            var Result = new
            {
                holdings = data1,
                summary = data2
            };

            return await Task.FromResult(Result);
        }

        public async Task<string> InsertGSecRedemptionIL(string userName, int CompanyID, int branchID, InstrumentRedemption instrumentRedemption)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@InstRedemptionID", instrumentRedemption.InstRedemptionID);
            SpParameters.Add("@InstrumentID", instrumentRedemption.InstrumentID);
            SpParameters.Add("@InstrumentRedemptionDetails", ListtoDataTableConverter.ToDataTable(instrumentRedemption.instrumentRedemptionDetails).AsTableValuedParameter("Type_GSecRedemptionDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertGSecRedemptionIL", SpParameters);
        }

        public async Task<string> InsertGSecRedemptionAML(string userName, int CompanyID, int branchID, InstrumentRedemptionAML instrumentRedemption)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@InstRedemptionID", instrumentRedemption.InstRedemptionID);
            SpParameters.Add("@InstrumentID", instrumentRedemption.InstrumentID);
            SpParameters.Add("@InstrumentRedemptionDetails", ListtoDataTableConverter.ToDataTable(instrumentRedemption.instrumentRedemptionDetails).AsTableValuedParameter("Type_GSecRedemptionDetailAML"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertGSecRedemptionAML", SpParameters);
        }

        public async Task<string> GSecInstrumentRedemptionApprovalIL(string userName, int CompanyID, int branchID, InstrumentRedemptionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@InstRedemptionIDs", approvalDto.InstRedemptionIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveGSecInstrumentRedemtionIL", SpParameters);
        }
        public async Task<string> GSecInstrumentRedemptionApprovalAML(string userName, int CompanyID, int branchID, InstrumentRedemptionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@InstRedemptionIDs", approvalDto.InstRedemptionIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveGSecInstrumentRedemtionAML", SpParameters);
        }

        public async Task<List<InsRed_GetList>> ListGSecInsRedemption(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<InsRed_GetList>("CM_ListGSecInsRedemptionIL", values);
        }

        public async Task<List<GSecRedemption>> GSecForRedemptionListIL(int CompanyID, int BranchID, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<GSecRedemption>("CM_ListGsecForRedemptionIL", values);
        }
        public async Task<List<GSecRedemption>> GSecForRedemptionListAML(int CompanyID, int BranchID, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<GSecRedemption>("CM_ListGsecForRedemptionAML", values);
        }

        public async Task<object> GetRedemptionByIDIL(int CompanyID, int BranchID, int InstrumentID)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@InstrumentID", InstrumentID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetGsecRedemptionByIDIL]", sqlParams);

            var data1 = CustomConvert.DataSetToList<InsRed_getHoldings>(DataSets.Tables[0]).ToList();
            var data2 = CustomConvert.DataSetToList<InsRed_getHoldingsSummary>(DataSets.Tables[1]).FirstOrDefault();

            //return await _dbCommonOperation.ReadSingleTable<InsRed_getHoldings>("CM_GetInstrumentRedemptionIL", values);
            var Result = new
            {
                holdings = data1,
                summary = data2
            };

            return await Task.FromResult(Result);
        }
        public async Task<object> GetRedemptionByIDAML(int CompanyID, int BranchID, int InstrumentID)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@InstrumentID", InstrumentID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetGsecRedemptionByIDAML]", sqlParams);

            var data1 = CustomConvert.DataSetToList<InsRed_getHoldings>(DataSets.Tables[0]).ToList();
            var data2 = CustomConvert.DataSetToList<InsRed_getHoldingsSummary>(DataSets.Tables[1]).FirstOrDefault();

            //return await _dbCommonOperation.ReadSingleTable<InsRed_getHoldings>("CM_GetInstrumentRedemptionIL", values);
            var Result = new
            {
                holdings = data1,
                summary = data2
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> TBillZCouponRedemptionListAML(int CompanyID, int BranchID, int FundID, string ListType, string FromDate, string ToDate)
        {
           SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@FundID", FundID),
                new SqlParameter("@ListType", ListType),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate)
           };

             var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ApprovalTBillZCouponAML]", sqlParams);

            var Result = new
            {
                ApprovalData = DataSets.Tables[0]
            };

            return await Task.FromResult(Result);
        }

        public string GetCleanPrice(int CompanyID, int BranchID, CalculateCleanPrice calculateCleanPrice)
        {
           try
            {
                double mEffectiveRate; string textOutput = null;

                mEffectiveRate = 0;
                string pExcelPath;

                pExcelPath = AppDomain.CurrentDomain.BaseDirectory;
                pExcelPath = pExcelPath + "TBondValueCalculation.xlsx";

                WorkBook workbook = WorkBook.Load(pExcelPath);
                WorkSheet sheet = workbook.WorkSheets.First();
                WorkSheet ws = workbook.GetWorkSheet("Calculation");

                ws["D7"].Value = calculateCleanPrice.SettlmentDate.GetValueOrDefault().ToString("MM/dd/yyyy");
                ws["D8"].Value = 2.2;
                ws["D10"].Value = 2.2;
                ws["D11"].Value = 1.2;
                ws["D12"].Value = 50000;
                //ws["D14"].Formula = "=ROUND(PRICE(D7,D8,D10,D11,100,2,1),4)";
                //foreach (var cell in sheet["D6:D15"])
                //{
                //    cell.Text= cell.Text;
                //    Console.WriteLine("Cell {0} has value '{1}'", cell.AddressString, cell.Text);
                //}

                return sheet["D14"].ToString();

            }

            catch (Exception ex)
            {
                throw ex;
            }

            
        }


        public async Task<object> SettlementCalculator(int CompanyID, int BranchID, SettlementCalculate settlementCalculate)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@InstrumentID", settlementCalculate.InstrumentID),
                new SqlParameter("@SettlementDate", Utility.DatetimeFormatter.DateFormat(settlementCalculate.SettlementDate)),
                new SqlParameter("@Yield", settlementCalculate.Yield),
                new SqlParameter("@NoOfUnit", settlementCalculate.NoOfUnit)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SP_CALCULATE_SETTLEMENT]", sqlParams);

            var Result = new
            {
                SettlementData = DataSets.Tables[0]
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> CalculateSettlementValue(int CompanyID, int BranchID, SettlementCalculate settlementCalculate)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@InstrumentID", settlementCalculate.InstrumentID),
                new SqlParameter("@SettlementDate", Utility.DatetimeFormatter.DateFormat(settlementCalculate.SettlementDate)),
                new SqlParameter("@Yield", settlementCalculate.Yield),
                new SqlParameter("@NoOfUnit", settlementCalculate.NoOfUnit)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SP_CALCULATE_SETTLEMENT]", sqlParams);

            var Result = new
            {
                SettlementData = DataSets.Tables[0]
            };

            return await Task.FromResult(Result);
        }
    }
}
