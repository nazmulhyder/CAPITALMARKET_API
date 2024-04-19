using Dapper;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.Divident;
using Model.DTOs.SellSurrender;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class DividendRepository : IDividendRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public DividendRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }


        public async Task<string> InsertUpdateDevidendDeclaration(int CompanyID, int BranchID, string userName, DividendDisbursementDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDividendDeclarationAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@MFDividendDecID", entry.MFDividendDecID);
                SpParameters.Add("@PeriodFrom", Utility.DatetimeFormatter.DateFormat(entry.PeriodFrom));
                SpParameters.Add("@PeriodTo", Utility.DatetimeFormatter.DateFormat(entry.PeriodTo));
                SpParameters.Add("@RecordDate", Utility.DatetimeFormatter.DateFormat(entry.RecordDate));
                SpParameters.Add("@DividendRate", entry.DividendRate);
                SpParameters.Add("@TotalDividendPayable", entry.TotalDividendPayable);
                SpParameters.Add("@Status", entry.Status);
                SpParameters.Add("@FundID", entry.FundID);
                SpParameters.Add("@NonTaxAmtInPercetageInd", entry.NonTaxAmtInPercetageInd);
                SpParameters.Add("@NonTaxAmtInPercetageOrg", entry.NonTaxAmtInPercetageOrg);
                SpParameters.Add("@NonTaxAmtInAmountInd", entry.NonTaxAmtInAmountInd);
                SpParameters.Add("@NonTaxAmtInAmountOrg", entry.NonTaxAmtInAmountOrg);
                SpParameters.Add("@DeclarationDate", Utility.DatetimeFormatter.DateFormat(entry.DeclarationDate));
                SpParameters.Add("@PaidupCapital", entry.PaidupCapital);
                SpParameters.Add("@Nav", entry.Nav);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public async Task<object> CalculateTotalDividendPayable(int CompanyID, int BranchID, string UserName, DivCalculationParamDto divCalculationParam)
        {
            SqlParameter[] Params = new SqlParameter[7];

            Params[0] = new SqlParameter("@CompanyID", CompanyID);
            Params[1] = new SqlParameter("@BranchID", BranchID);
            Params[2] = new SqlParameter("@UserName", UserName);
            Params[3] = new SqlParameter("@FundID", divCalculationParam.FundID);
            Params[4] = new SqlParameter("@divPercentage", divCalculationParam.divPercentage);
            Params[5] = new SqlParameter("@RecordDate", Utility.DatetimeFormatter.DateFormat(divCalculationParam.RecordDate));
            Params[6] = new SqlParameter("@PaidupCapital", divCalculationParam.PaidupCapital);
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_CalculateTotalDividendAML]", Params);

            return new
            {
                CalculateDivident = DataSets.Tables[0],
                Nav = DataSets.Tables[1]
            };
        }

        public async Task<List<DividendDisbursementDto>> ListDividendAML(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType , FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<DividendDisbursementDto>("CM_ListDividendAML", values);
        }

        public async Task<string> DividendApprovalAML(string userName, int CompanyID, int branchID, DividendApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@MFDividendDecID", approvalDto.MFDividendDecID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDividendAML", SpParameters);
        }


        //cash dividend distribution

        public async Task<List<DividendDisbursementDto>> CashDividendInfoListAML(int CompanyID, int BranchID, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<DividendDisbursementDto>("CM_GetCashDividendInfoListAML", values);
        }

        public async Task<object> CashDividendDistributionListAML(int CompanyID, int BranchID, int MFDividendDecID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);
            sqlParams[2] = new SqlParameter("@MFDividendDecID", MFDividendDecID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_CashDividendDistributionListAML]", sqlParams);

            var Result = new
            {
                DividentAllocationList = dataset.Tables[0],
            };

            return await Task.FromResult(Result);
        }
        public async Task<string> InsertUpdateCashDividendDistribution(int CompanyID, int BranchID, string userName, List<CashDividendDistribution> cashDividendDistributions)
        {
            try
            {
                string sp = "CM_InsertUpdateCashDividendDistributionAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DividendDisbursementCash", ListtoDataTableConverter.ToDataTable(cashDividendDistributions).AsTableValuedParameter("Type_DividendDisbursementCash"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<object> GetCashDividendApprovalAML(int CompanyID, int BranchID, string userName, int FundID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@FundID", FundID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetCashDividendApprovalAML]", sqlParams);

            var Result = new
            {
                AMLMF_DividendDec = dataset.Tables[0],
                //AMLMF_DividendEntitlement = dataset.Tables[1],
                //AMLMF_DividentSummary = dataset.Tables[2]
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);

        }

        public async Task<string> CashDividentDistributionApproval(string userName, int CompanyID, int branchID, CashDividentDistributionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@MFCashDivDistIDs", approvalDto.MFCashDivDistIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCashDividendDistributionAML", SpParameters);
        }


        //stock dividend distribution
        public async Task<List<DividendDisbursementDto>> StockDividendInfoListAML(int CompanyID, int BranchID, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<DividendDisbursementDto>("CM_GetStockDividendInfoListAML", values);
        }
        public async Task<List<CIPDividendDistribution>> StockDividendDistributionListAML(int CompanyID, int BranchID, string MFDividendDecIDs)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, MFDividendDecIDs = MFDividendDecIDs };
            return await _dbCommonOperation.ReadSingleTable<CIPDividendDistribution>("CM_StockDividendDistributionListAML", values);
        }
        public async Task<string> InsertUpdateStockDividendDistribution(int CompanyID, int BranchID, string userName, List<CIPDividendDistribution> cipDividendDistributions)
        {
            try
            {
                string sp = "CM_InsertUpdateStockDividendDistributionAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DividendDisbursementCIP", ListtoDataTableConverter.ToDataTable(cipDividendDistributions).AsTableValuedParameter("Type_DividendDisbursementCIP"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<object> GetCIPDividendApprovalAML(int CompanyID, int BranchID, string userName, int FundID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@FundID", FundID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetCIPDividendApprovalAML]", sqlParams);

            var Result = new
            {
                AMLMF_DividendDec = dataset.Tables[0]
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);

        }

        public async Task<object> GetCashDivByRecord(int CompanyID, int BranchID, string userName, int DeclarationID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@DeclarationID", DeclarationID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetCashDivByRecordAML]", sqlParams);

            var Result = new
            {
                //AMLMF_DividendDec = dataset.Tables[0],
                AMLMF_DividendEntitlement = dataset.Tables[0],
                AMLMF_DividentSummary = dataset.Tables[1]
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);

        }

        public async Task<string> StockDividentDistributionApproval(string userName, int CompanyID, int branchID, StockDividentDistributionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@MFCIPDivDistIDs", approvalDto.MFCIPDivDistIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveStockDividendDistributionAML", SpParameters);
        }

        public async Task<object> GetGLBalance(int CompanyID, int BranchID, int FundID, string GLCode)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);
            sqlParams[2] = new SqlParameter("@FundID", FundID);
            sqlParams[3] = new SqlParameter("@GLCode", GLCode);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_Get_GLBalance]", sqlParams);

            var Result = new
            {
                PaidUpCapital = dataset.Tables[0],
            };

            return await Task.FromResult(Result);

        }

        public async Task<object> GetStockDivByRecord(int CompanyID, int BranchID, string userName, int DeclarationID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@DeclarationID", DeclarationID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetStockDivByRecordAML]", sqlParams);

            var Result = new
            {
                //AMLMF_DividendDec = dataset.Tables[0],
                AMLMF_DividendEntitlement = dataset.Tables[0],
                AMLMF_DividentSummary = dataset.Tables[1]
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);

        }

        public async Task<object> DividentAllotcationDistribution(int CompanyID, int BranchID,  int DivDeclarationID, string PayoutType,  decimal Nav )
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);
            sqlParams[2] = new SqlParameter("@MFDividendDecID", DivDeclarationID);
            sqlParams[3] = new SqlParameter("@PayoutType", PayoutType);
            sqlParams[4] = new SqlParameter("@Nav", Nav);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_DividendAllocation]", sqlParams);

            var Result = new
            {
                DividentAllocationList = dataset.Tables[0],
            };

            return await Task.FromResult(Result);

        }

    }
}
