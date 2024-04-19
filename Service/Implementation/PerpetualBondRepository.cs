using Dapper;
using Model.DTOs.Allocation;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.PerpetualBond;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class PerpetualBondRepository : IPerpetualBondRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public PerpetualBondRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<List<PB_ActiveBondInstrument>> PBActiveBondInstrumentList(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<PB_ActiveBondInstrument>("CM_PB_ListBondInsIL", values);
        }

        public async Task<List<CouponCol_declarationDto>> LastThreeDeclaretionEntries(int CompanyID, int BranchID, int InstrumentID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_declarationDto>("CM_ListPerpetualBondLast_3_Declaration_IL", values);
        }

        public async Task<string> InsertCouponColDeclaration(int CompanyID, int BranchID, string userName, CouponCol_declarationDto entry)
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
                SpParameters.Add("@TenorStartDate", Utility.DatetimeFormatter.DateFormat(entry.TenorStartDate));
                SpParameters.Add("@TenorEndDate", Utility.DatetimeFormatter.DateFormat(entry.TenorEndDate));
                SpParameters.Add("@Year",  entry.Year);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<BondNewDeclaredInstrument>> BondNewDeclaredInstrument(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<BondNewDeclaredInstrument>("CM_PB_ListBondNewDeclaredInstrumentIL", values);
        }
        public async Task<List<PerpetualBond>> GetPerpetualBondHoldings(int CompanyID, int BranchID, int InstrumentID, int ProductID, int DeclarationID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID, ProductID = ProductID, DeclarationID = DeclarationID };
            return await _dbCommonOperation.ReadSingleTable<PerpetualBond>("CM_PB_GetAllHoldingForBondInstrument", values);
        }


        public async Task<object> GetPerpetualBondHoldings_Claim(int CompanyID, int BranchID, int InstrumentID, string Year, int DeclarationID)
        {
              SqlParameter[] sqlParams = new SqlParameter[]
              {
                    new SqlParameter("@CompanyID", CompanyID),
                    new SqlParameter("@BranchID", BranchID),
                    new SqlParameter("@InstrumentID", InstrumentID),
                    new SqlParameter("@Year", Year),
                    new SqlParameter("@DeclarationID", DeclarationID)
              };
            //return await _dbCommonOperation.ReadSingleTable<PerpetualBondClaim>("CM_PB_GetAllHoldingForBondInstrument_Claim", values);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_PB_GetAllHoldingForBondInstrument_Claim]", sqlParams);

            var Result = new
            {
                PB_List = DataSets.Tables[0],
            };

            return await Task.FromResult(Result);
        }


        public async Task<string> InsertPerpetualBondClaim(int CompanyID, int BranchID, string userName, List<PerpetualBond> perpetualBonds, int DeclarationID, int ProductID)
        {
            try
            {
                string sp = "CM_InsertPerpetualInstCollectionClaimIL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DeclarationID", DeclarationID);
                SpParameters.Add("@ProductID", ProductID);
                SpParameters.Add("@PerpetualBonds", ListtoDataTableConverter.ToDataTable(perpetualBonds).AsTableValuedParameter("Type_PerpetualBond"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> InsertPerpetualBond(int CompanyID, int BranchID, string userName, List<PerpetualBondClaim> perpetualBonds)
        {
            try
            {
                string sp = "CM_InsertPerpetualBondIL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@PerpetualBonds", ListtoDataTableConverter.ToDataTable(perpetualBonds).AsTableValuedParameter("Type_PerpetualBondClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ListPerpetualBond>> ListPerpetualBond(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListPerpetualBond>("CM_ListPerpetualBondIL", values);
        }

        public async Task<string> PerpetualBondApproval(string userName, int CompanyID, int branchID, PerpetualBondApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@InstCollectionIDs", approvalDto.IntCollectionIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApprovePerpetualBond", SpParameters);
        }

        public async Task<string> PerpetualBondDeclarationApproval(string userName, int CompanyID, int branchID, PerpetualBondDeclarationApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@DeclarationIDs", approvalDto.DeclarationIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApprovePerpetualBondDeclaration", SpParameters);
        }

        public async Task<List<PerpetualBondDeclarationDto>> ListPerpetualBondDeclaration(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<PerpetualBondDeclarationDto>("CM_ListPerpetualBondDeclarationIL", values);
        }

        public async Task<List<PerpetualBondForReversalDto>> ListPerpetualBondForReversal(int CompanyID, int BranchID, int ProductID, string AccountNo, int InstrumentID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo, InstrumentID = InstrumentID };
            return await _dbCommonOperation.ReadSingleTable<PerpetualBondForReversalDto>("CM_PB_GetAllPerpetualBondForReversal", values);
        }

        public async Task<string> InsertPerpetualBondReversal(int CompanyID, int BranchID, string userName, List<PerpetualBondReversalDto>  perpetualBondReversals)
        {
            try
            {
                string sp = "CM_InsertPerpetualBondReversalIL";

                foreach(var oitem in perpetualBondReversals)
                {
                    oitem.IntCollectionDate = Utility.DatetimeFormatter.DateFormat(oitem.IntCollectionDate);
                }

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@PerpetualBondsReversal", ListtoDataTableConverter.ToDataTable(perpetualBondReversals).AsTableValuedParameter("Type_PerpetualBondReversal"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PerpetualBondReversalApproval(string userName, int CompanyID, int branchID, PerpetualBondReversalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@IntCollReversalIDs", approvalDto.IntCollReversalIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApprovePerpetualBondReversal", SpParameters);
        }

        public async Task<List<CouponCol_reversalListDto>> ListPerpetualBondReversal(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<CouponCol_reversalListDto>("CM_ListPerpetualReversalIL", values);
        }

        public async Task<object> PerpetualBondClaimList(int CompanyID, int BranchID, string Year)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@Year", Year)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_PerpetualBond_List]", sqlParams);

            var Result = new
            {
                PB_List = DataSets.Tables[0],
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> FilterDeclarationList(int CompanyID, int BranchID, int InstrumentID, string Year)
        {
            //var values = new { CompanyID = CompanyID, BranchID = BranchID, InstrumentID = InstrumentID };
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@InstrumentID", InstrumentID),
                new SqlParameter("@Year", Year)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_PerpetualBondCollectionDeclaration_List]", sqlParams);

            var Result = new
            {
                PB_List = DataSets.Tables[0],
            };

            return await Task.FromResult(Result);
        }


    }
}
