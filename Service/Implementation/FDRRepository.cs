using Dapper;
using Model.DTOs.Allocation;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.FDR;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class FDRRepository : IFDRRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public FDRRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

      
        public async Task<BalanceInfoDto> FDR_GetClientAbailableBalanceIL_AML(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, int FundID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo, FundID= FundID };
            var result = await _dbCommonOperation.ReadSingleTable<BalanceInfoDto>("CM_FDR_BalanceInfoIL_AML", values); ;
            return result.FirstOrDefault();
        }

        public async Task<string> InsertUpdateDepositAccountOpening(int CompanyID, int BranchID, string userName, DepositOpeningDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDepositAccountOpeningIL_AML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DepositOpenDetlID", entry.DepositOpenDetlID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@OpeningDate", Utility.DatetimeFormatter.DateFormat(entry.OpeningDate));
                SpParameters.Add("@OpeningAmount",  entry.OpeningAmount);
                SpParameters.Add("@BankFIOrgID", entry.BankFIOrgID);
                SpParameters.Add("@DepositProductName", entry.DepositProductName);
                SpParameters.Add("@DepositID", entry.DepositID);
                SpParameters.Add("@DepositACCNo", entry.DepositACCNo);
                SpParameters.Add("@DepositAmount", entry.DepositAmount);
                SpParameters.Add("@DepositTerm", entry.DepositTerm);
                SpParameters.Add("@MaturityDate", Utility.DatetimeFormatter.DateFormat(entry.MaturityDate));
                SpParameters.Add("@InterestRate", entry.InterestRate); 
                SpParameters.Add("@InterestAmount", entry.InterestAmount);
                SpParameters.Add("@TermInDays", entry.TermInDays);
                SpParameters.Add("@IntPayOutPeriodicity", entry.IntPayOutPeriodicity);
                SpParameters.Add("@IsAutoRenewed", entry.IsAutoRenewed); 
                SpParameters.Add("@FundID", entry.FundID);
                SpParameters.Add("@BankAccountID", entry.BankAccountID);
                SpParameters.Add("@MaturedValue", entry.MaturedValue);
                SpParameters.Add("@AIT", entry.AIT);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DepositOpeningDto>> ListDepositBankAccountOpening(int CompanyID, int BranchID, string FromDate, string ToDate, string SearchKeyword, string ListType, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, FromDate = FromDate, ToDate = ToDate, SearchKeyword = SearchKeyword, ListType = ListType , FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<DepositOpeningDto>("CM_ListDepositAccountOpeningIL_AML", values);
        }

        public async Task<string> DepositBankAccountOpeningApprovalIL(string userName, int CompanyID, int branchID, DepositAccountOpeningApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@DepositOpenDetlIDs", approvalDto.DepositOpenDetlIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositBankAccountOpening", SpParameters);
        }

        public async Task<string> DepositBankAccountOpeningApprovalAML(string userName, int CompanyID, int branchID, DepositAccountOpeningApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@DepositOpenDetlIDs", approvalDto.DepositOpenDetlIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositBankAccountOpeningAML", SpParameters);
        }


        public async Task<List<InterestCollectionInfoDto>> InterestCollectionInfo(int CompanyID, int BranchID, string UserName , int ProductID, string AccountNo, int FundID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID,  ProductID = ProductID, AccountNo = AccountNo, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<InterestCollectionInfoDto>("CM_FDR_GetDepositCollectionIL_AML", values);
        }



        public async Task<string> InsertUpdateDepositInterestCollection(int CompanyID, int BranchID, string userName, InterestCollectionDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDepositInterestCollectionIL_AML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID); 
                SpParameters.Add("@IntCollectionID", 0);
                SpParameters.Add("@IntCollectionDate", Utility.DatetimeFormatter.DateFormat(entry.IntCollectionDate));               
                SpParameters.Add("@InterestAmount", entry.InterestAmount);
                SpParameters.Add("@AIT", entry.AIT);
                SpParameters.Add("@IntCollectionAmount", entry.IntCollectionAmount);
                SpParameters.Add("@DepositID", entry.DepositID);
                SpParameters.Add("@BankAccountID", entry.BankAccountID);
                SpParameters.Add("@ExciseDuty", entry.ExciseDuty);
                SpParameters.Add("@AdjustmentInterest", entry.AdjustmentInterest);
                SpParameters.Add("@AdjustmentAIT", entry.AdjustmentAIT);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListInterestCollectionDto>> ListDepositInterestCollection(int CompanyID, int BranchID, string DateFrom, string DateTo, string SearchKeyword, string ListType, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, DateFrom = DateFrom, DateTo = DateTo, SearchKeyword = SearchKeyword, ListType = ListType, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<ListInterestCollectionDto>("CM_ListDepositInterestCollectionIL_AML", values);
        }

        public async Task<string> DepositIntCollectionApprovalIL(string userName, int CompanyID, int branchID, DepositIntCollectionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollectionIDs", approvalDto.IntCollectionIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestCollectionIL_AML", SpParameters);
        }

        public async Task<string> DepositIntCollectionApprovalAML(string userName, int CompanyID, int branchID, DepositIntCollectionApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollectionIDs", approvalDto.IntCollectionIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestCollectionAML", SpParameters);
        }



        // reversal


        public async Task<List<DepositIntCollectionReversalInfoDto>> GetInterestCollectionInfoForReversal(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, string DepositAccNo, int FundID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo, DepositAccNo = DepositAccNo, FundID = FundID };
            return await _dbCommonOperation.ReadSingleTable<DepositIntCollectionReversalInfoDto>("CM_FDR_GetIntCollectionReversalIL_AML", values);
        }

        public async Task<string> InsertUpdateDepositInterestCollectionReversal(int CompanyID, int BranchID, string userName, DepositInterestCollectionReversalDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDepositInterestCollectionReversalIL_AML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IntCollReversalID", 0);
                SpParameters.Add("@ReversalReason", entry.ReversalReason);
                SpParameters.Add("@IntCollectionID", entry.IntCollectionID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListInterestCollectionReversalDto>> ListDepositInterestCollectionReversalIL(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListInterestCollectionReversalDto>("CM_ListDepositInterestCollectionReversalIL_AML", values);
        }

        public async Task<string> DepositIntCollectionReversalApprovalIL(string userName, int CompanyID, int branchID, DepositInterestReversalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollReversalID", approvalDto.IntCollReversalID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestCollectionReversalIL_AML", SpParameters);
        }

        public async Task<string> DepositIntCollectionReversalApprovalAML(string userName, int CompanyID, int branchID, DepositInterestReversalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@IntCollReversalID", approvalDto.IntCollReversalID);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestCollectionReversalAML", SpParameters);
        }

        // encashment

        public async Task<List<DepositInterestInfoDto>> GetDepositInterestInfoForEncashment(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo, int FundID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo , FundID  = FundID };
            return await _dbCommonOperation.ReadSingleTable<DepositInterestInfoDto>("CM_FDR_GetDepositInterestForEncashmentlIL_AML", values);
        }

        public async Task<string> InsertUpdateDepositInterestEncashment(int CompanyID, int BranchID, string userName, DepositEncashmentDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDepositInterestEncashmentIL_AML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@EncashmentID", 0);
                SpParameters.Add("@PrincipalAmount", entry.PrincipalAmount);
                SpParameters.Add("@InterestAmount", entry.InterestAmount);
                SpParameters.Add("@AIT", entry.AIT);
                SpParameters.Add("@TotalCharge", entry.TotalCharge);
                SpParameters.Add("@EncashmentDate", Utility.DatetimeFormatter.DateFormat(entry.EncashmentDate));
                SpParameters.Add("@EncashmentAmount", entry.EncashmentAmount);
                SpParameters.Add("@DepositID", entry.DepositID);
                SpParameters.Add("@ExciseDuty", entry.ExciseDuty);
                SpParameters.Add("@PrematureCharge", entry.PrematureCharge);
                SpParameters.Add("@BankAccountID", entry.BankAccountID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListDepositEncashmentDto>> ListDepositInterestEncashmentIL_AML(int CompanyID, int BranchID, string FromDate, string ToDate, string SearchKeyword, string ListType, int FundID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, FromDate = FromDate, ToDate = ToDate, SearchKeyword = SearchKeyword, ListType = ListType, FundID= FundID };
            return await _dbCommonOperation.ReadSingleTable<ListDepositEncashmentDto>("CM_ListDepositInterestEncashmentIL_AML", values);
        }

        public async Task<string> DepositIntEncashmentApprovalIL(string userName, int CompanyID, int branchID, DepositInterestEncashmentApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@EncashmentIDs", approvalDto.EncashmentIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestEncashmentIL_AML", SpParameters);
        }
        public async Task<string> DepositIntEncashmentApprovalAML(string userName, int CompanyID, int branchID, DepositInterestEncashmentApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@EncashmentIDs", approvalDto.EncashmentIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestEncashmentAML", SpParameters);
        }


        //re-newal
        public async Task<string> InsertUpdateDepositAccountRenewalAML(int CompanyID, int BranchID, string userName, DepositInstrumentRenewal entry)
        {
            try
            {
                string sp = "CM_InsertUpdateDepositRenewalAML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@RenewalDepositAmount", entry.RenewalDepositAmount);
                SpParameters.Add("@DepositID", entry.DepositID);
                SpParameters.Add("@RenewalInterestRate", entry.RenewalInterestRate);
                SpParameters.Add("@TermInDays", entry.TermInDays);
                SpParameters.Add("@RenewalIssueDate", Utility.DatetimeFormatter.DateFormat(entry.RenewalIssueDate));
                SpParameters.Add("@RenewalMaturityDate", Utility.DatetimeFormatter.DateFormat(entry.RenewalMaturityDate));
                SpParameters.Add("@AIT", entry.RenewalAIT);
                SpParameters.Add("@ExciseDuty", entry.RenewalExciseDuty);
                SpParameters.Add("@AdjInterest", entry.AdjustmentInterest);
                SpParameters.Add("@AdjAITRate", entry.AITRate);
                SpParameters.Add("@AccruedInterest", entry.AccruedInterest);
                SpParameters.Add("@AccruedAIT", entry.AccruedAIT);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //renewal
        public async Task<string> DepositAccountRenewalApprovalAML(string userName, int CompanyID, int branchID, DepositInterestRenewalApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@DepositRenewalIDs", approvalDto.DepositRenewalIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveDepositInterestRenewalAML", SpParameters);
        }

        public async Task<object> GetRenewalListAML(int CompanyID, int BranchID, string userName, int FundID)
        {
              SqlParameter[] sqlParams = new SqlParameter[]
              {
                  new SqlParameter("@CompanyID", CompanyID),
                  new SqlParameter("@BranchID", BranchID),
                  new SqlParameter("@FundID", FundID)
              };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetRenewalListAML]", sqlParams);

            var Result = new
            {
                RenewalList = DataSets.Tables[0],
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> RenewalListAML(int CompanyID, int BranchID, string userName, string FromDate, string ToDate, int FundID, string ListType)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                  new SqlParameter("@CompanyID", CompanyID),
                  new SqlParameter("@BranchID", BranchID),
                  new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate),
                  new SqlParameter("@ListType", ListType),
                  new SqlParameter("@FundID", FundID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_RenewalListAML]", sqlParams);

            var Result = new
            {
                RenewalList = DataSets.Tables[0],
            };

            return await Task.FromResult(Result);
        }

    }
}
