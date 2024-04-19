using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Approval;
using Model.DTOs.Market;
using Model.DTOs.SaleOrder;
using Model.DTOs.TradeRestriction;
using Model.DTOs.Withdrawal;
using Newtonsoft.Json.Linq;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public WithdrawalRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        public async Task<string> AddUpdateWithdrawal(int companyId, int branchID, string userName, SLWithdrawalEntryDto entityDto)
        {
            try
            {
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                string sp = "";

                if (companyId == 4) //SL
                {
                    #region Insert New Data

                    //formatting date
                    entityDto.SLFinanceDisbursement.ProcessingDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursement.ProcessingDate);
                    entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate);

                    if (entityDto.SLFinanceDisbursement.DisbursementID == 0 || entityDto.SLFinanceDisbursement.DisbursementID == null)
                    {
                        sp = "CM_InsertWithdrawalSL";

                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@SLFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@SLFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion

                    #region Update Data
                    else
                    {
                        sp = "CM_UpdateWithdrawalSL";
                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@SLFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@SLFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion
                }

                if (companyId == 2) //AML
                {
                    #region Insert New Data

                    //formatting date
                    entityDto.SLFinanceDisbursement.ProcessingDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursement.ProcessingDate);
                    entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate);

                    if (entityDto.SLFinanceDisbursement.DisbursementID == 0 || entityDto.SLFinanceDisbursement.DisbursementID == null)
                    {
                        sp = "CM_InsertWithdrawalAML";

                        //DataTable MemberAccInfo = new DataTable();
                        //DynamicParameters SpParameters = new DynamicParameters();
                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@AMLFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@AMLFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion

                    #region Update Data
                    else
                    {
                        sp = "CM_UpdateWithdrawalAML";
                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@AMLFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@AMLFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion
                }

                if (companyId == 3) //IL
                {
                    #region Insert New Data

                    //formatting date
                    entityDto.SLFinanceDisbursement.ProcessingDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursement.ProcessingDate);
                    entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate = Utility.DatetimeFormatter.DateFormat(entityDto.SLFinanceDisbursementPaymentDetails.DisburseDate);

                    if (entityDto.SLFinanceDisbursement.DisbursementID == 0 || entityDto.SLFinanceDisbursement.DisbursementID == null)
                    {
                        sp = "CM_InsertWithdrawalIL";

                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@ILFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@ILFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion

                    #region Update Data
                    else
                    {
                        sp = "CM_UpdateWithdrawalIL";

                        SpParameters.Add("@UserName", userName);
                        SpParameters.Add("@CompanyID", companyId);
                        SpParameters.Add("@BranchID", branchID);
                        SpParameters.Add("@ILFinanceDisbursement", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursement).AsTableValuedParameter("Type_SLFinDisbursement"));
                        SpParameters.Add("@ILFinanceDisbursementDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLFinanceDisbursementPaymentDetails).AsTableValuedParameter("Type_SLFinanceDisbursementPaymentDetail"));
                        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                        //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    }
                    #endregion
                }

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetWithdrawalBankInfo(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            DataSet dataset = new DataSet();
            SqlParameter[] sqlParams = null;
            List<SLWithdrawalInvestorDocument> InvestorDocuments = new List<SLWithdrawalInvestorDocument>();

            if (CompanyID == 4) //SL
            {
                sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@ProductID", ProductID);
                sqlParams[2] = new SqlParameter("@AccountNumber", AccountNumber);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[4] = new SqlParameter("@BranchID", BranchID);

                dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_WithdrawalAccInfoDetailSL]", sqlParams);

                InvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[1]);

                for (int i = 0; i < InvestorDocuments.Count; i++)
                {
                    string docFilePath = InvestorDocuments[i].DocFilePath + InvestorDocuments[i].DocFileName;
                    if (File.Exists(docFilePath))
                    {
                        string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + InvestorDocuments[i].DocFileName;
                        string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                        if (!Directory.Exists(fileDirectory))
                            Directory.CreateDirectory(fileDirectory);
                        if (File.Exists(fileFullPath))
                            File.Delete(fileFullPath);
                        File.Copy(docFilePath, fileFullPath);
                        InvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + InvestorDocuments[i].DocFileName;
                    }
                }
            }
            if (CompanyID == 2) //AML
            {
                sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@ProductID", ProductID);
                sqlParams[2] = new SqlParameter("@AccountNumber", AccountNumber);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[4] = new SqlParameter("@BranchID", BranchID);

                dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_WithdrawalAccInfoDetailAML]", sqlParams);

                InvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[1]);

                for (int i = 0; i < InvestorDocuments.Count; i++)
                {
                    string docFilePath = InvestorDocuments[i].DocFilePath + InvestorDocuments[i].DocFileName;
                    if (File.Exists(docFilePath))
                    {
                        string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + InvestorDocuments[i].DocFileName;
                        string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                        if (!Directory.Exists(fileDirectory))
                            Directory.CreateDirectory(fileDirectory);
                        if (File.Exists(fileFullPath))
                            File.Delete(fileFullPath);
                        File.Copy(docFilePath, fileFullPath);
                        InvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + InvestorDocuments[i].DocFileName;
                    }
                }
            }
            if (CompanyID == 3) //IL
            {
                sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@ProductID", ProductID);
                sqlParams[2] = new SqlParameter("@AccountNumber", AccountNumber);
                sqlParams[3] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[4] = new SqlParameter("@BranchID", BranchID);

                dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_WithdrawalAccInfoDetailIL]", sqlParams);

                InvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[1]);

                for (int i = 0; i < InvestorDocuments.Count; i++)
                {
                    string docFilePath = InvestorDocuments[i].DocFilePath + InvestorDocuments[i].DocFileName;
                    if (File.Exists(docFilePath))
                    {
                        string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + InvestorDocuments[i].DocFileName;
                        string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                        if (!Directory.Exists(fileDirectory))
                            Directory.CreateDirectory(fileDirectory);
                        if (File.Exists(fileFullPath))
                            File.Delete(fileFullPath);
                        File.Copy(docFilePath, fileFullPath);
                        InvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + InvestorDocuments[i].DocFileName;
                    }
                }
            }


            var Result = new
            {
                InvestorAccountInfo = dataset.Tables[0],
                InvestorDocInfo = dataset.Tables[1],
                ErrorList = dataset.Tables[2],
                BankList = dataset.Tables[3]
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);
        }

        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalSL(int CompanyID, int BranchID, string userName, FilterWithdrawalDto filter)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID,  ListType = filter.ListType, UserName = userName, ContractID = filter.ContractID, ProductID = filter.ProductID, FromDate  = Utility.DatetimeFormatter.DateFormat(filter.FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(filter.ToDate) };
            return _dbCommonOperation.ReadSingleTable<SLWithdrawalInfoListDto>("CM_ListWithdrawalSL", values);
        }
        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalIL(int CompanyID, int BranchID, string userName, FilterWithdrawalDto filter)
        {

            var values = new { CompanyID = CompanyID, BranchID = BranchID, ListType = filter.ListType, UserName = userName, ContractID = filter.ContractID, ProductID = filter.ProductID, FromDate = Utility.DatetimeFormatter.DateFormat(filter.FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(filter.ToDate) };
            var data = _dbCommonOperation.ReadSingleTable<SLWithdrawalInfoListDto>("CM_ListWithdrawalIL", values);
            return data;
        }
        public Task<List<SLWithdrawalInfoListDto>> ListWithdrawalAML(int CompanyID, int BranchID,  string userName, FilterWithdrawalDto filter)
        {

            var values = new { CompanyID = CompanyID, BranchID = BranchID,  ListType = filter.ListType, UserName = userName, ContractID = filter.ContractID, ProductID = filter.ProductID, FromDate = Utility.DatetimeFormatter.DateFormat(filter.FromDate), ToDate = Utility.DatetimeFormatter.DateFormat(filter.ToDate) };
            return _dbCommonOperation.ReadSingleTable<SLWithdrawalInfoListDto>("CM_ListWithdrawalAML", values);
        }


        public async Task<SLGetWithdrawalInfo> GetWithdrawalDetailsSL(string UserName, int CompanyID, int BranchID, int DisburseID)
        {
            SLGetWithdrawalInfo sLGetWithdrawalInfo = new SLGetWithdrawalInfo();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@DisbursementID", DisburseID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetWithdrawalDetailSL]", sqlParams);

            ////sLGetWithdrawalInfo.DisbBankAccountID = Convert.ToInt16(dataset.Tables[0].Rows[0][0].ToString());
            sLGetWithdrawalInfo.slFinanceDisbursement = CustomConvert.DataSetToList<SLFinanceDisbursementDetailDto>(dataset.Tables[0]).FirstOrDefault();
            sLGetWithdrawalInfo.slFinanceDisbursementPaymentDetails = CustomConvert.DataSetToList<SLFinanceDisbursementPaymentInfoDto>(dataset.Tables[1]).FirstOrDefault();
            sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[2]).ToList();


            for (int i = 0; i < sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments.Count; i++)
            {
                string docFilePath = sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                if (File.Exists(docFilePath))
                {
                    string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                    string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                    if (!Directory.Exists(fileDirectory))
                        Directory.CreateDirectory(fileDirectory);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);
                    File.Copy(docFilePath, fileFullPath);
                    sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                }
            }

            return sLGetWithdrawalInfo;
        }
        public async Task<SLGetWithdrawalInfo> GetWithdrawalDetailsIL(string UserName, int CompanyID, int BranchID, int DisburseID)
        {
            SLGetWithdrawalInfo sLGetWithdrawalInfo = new SLGetWithdrawalInfo();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@DisbursementID", DisburseID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetWithdrawalDetailIL]", sqlParams);

            ////sLGetWithdrawalInfo.DisbBankAccountID = Convert.ToInt16(dataset.Tables[0].Rows[0][0].ToString());
            sLGetWithdrawalInfo.slFinanceDisbursement = CustomConvert.DataSetToList<SLFinanceDisbursementDetailDto>(dataset.Tables[0]).FirstOrDefault();
            sLGetWithdrawalInfo.slFinanceDisbursementPaymentDetails = CustomConvert.DataSetToList<SLFinanceDisbursementPaymentInfoDto>(dataset.Tables[1]).FirstOrDefault();
            sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[2]).ToList();


            for (int i = 0; i < sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments.Count; i++)
            {
                string docFilePath = sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                if (File.Exists(docFilePath))
                {
                    string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                    string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                    if (!Directory.Exists(fileDirectory))
                        Directory.CreateDirectory(fileDirectory);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);
                    File.Copy(docFilePath, fileFullPath);
                    sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                }
            }

            return sLGetWithdrawalInfo;
        }
        public async Task<SLGetWithdrawalInfo> GetWithdrawalDetailsAML(string UserName, int CompanyID, int BranchID, int DisburseID)
        {
            SLGetWithdrawalInfo sLGetWithdrawalInfo = new SLGetWithdrawalInfo();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@DisbursementID", DisburseID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_GetWithdrawalDetailAML]", sqlParams);

            ////sLGetWithdrawalInfo.DisbBankAccountID = Convert.ToInt16(dataset.Tables[0].Rows[0][0].ToString());
            sLGetWithdrawalInfo.slFinanceDisbursement = CustomConvert.DataSetToList<SLFinanceDisbursementDetailDto>(dataset.Tables[0]).FirstOrDefault();
            sLGetWithdrawalInfo.slFinanceDisbursementPaymentDetails = CustomConvert.DataSetToList<SLFinanceDisbursementPaymentInfoDto>(dataset.Tables[1]).FirstOrDefault();
            sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments = CustomConvert.DataSetToList<SLWithdrawalInvestorDocument>(dataset.Tables[2]).ToList();


            for (int i = 0; i < sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments.Count; i++)
            {
                string docFilePath = sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                if (File.Exists(docFilePath))
                {
                    string fileFullPath = Directory.GetCurrentDirectory() + "\\tmpImage\\" + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                    string fileDirectory = Directory.GetCurrentDirectory() + "\\tmpImage\\";
                    if (!Directory.Exists(fileDirectory))
                        Directory.CreateDirectory(fileDirectory);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);
                    File.Copy(docFilePath, fileFullPath);
                    sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFilePath = FilePath.GetFileUploadURL() + sLGetWithdrawalInfo.SLWithdrawalInvestorDocuments[i].DocFileName;
                }
            }

            return sLGetWithdrawalInfo;
        }

        public async Task<string> WithdrawalApprovalSL(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto)
        {

            DynamicParameters SpParameters = new DynamicParameters();


            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@DisbursementIDs", approvalDto.DisbursementIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_MultiApproveWithdrawalSL", SpParameters);

        }
        public async Task<string> WithdrawalApprovalIL(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto)
        {

            DynamicParameters SpParameters = new DynamicParameters();


            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@DisbursementIDs", approvalDto.DisbursementIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_MultiApproveWithdrawalIL", SpParameters);

        }
        public async Task<string> WithdrawalApprovalAML(string userName, int CompanyID, int branchID, WithdrawalApprovalDto approvalDto)
        {

            DynamicParameters SpParameters = new DynamicParameters();


            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@DisbursementIDs", approvalDto.DisbursementIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_MultiApproveWithdrawalAML", SpParameters);

        }


        public async Task<string> AddUpdatePrepareWithdrawalSL(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto)
        {
            try
            {
                #region Insert New Data

                //formatting date
                entityDto.InstrumentDate = Utility.DatetimeFormatter.DateFormat(entityDto.InstrumentDate);

                string sp = "CM_InsertPrepareWithdrawalSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", entityDto.DisbursementID);
                SpParameters.Add("@InstrumentType", entityDto.InstrumentType);
                SpParameters.Add("@InstrumentNmbr", entityDto.InstrumentNmbr);
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@InstrumentDate", entityDto.InstrumentDate);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@ProductID", entityDto.ProductID);
                SpParameters.Add("@BankAccountID", entityDto.BankAccountID);
                SpParameters.Add("@InstIssuedByIndexID", entityDto.InstIssuedByIndexID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<string> AddUpdatePrepareWithdrawalIL(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto)
        {
            try
            {
                #region Insert New Data

                //formatting date
                entityDto.InstrumentDate = Utility.DatetimeFormatter.DateFormat(entityDto.InstrumentDate);

                string sp = "CM_InsertPrepareWithdrawalIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", entityDto.DisbursementID);
                SpParameters.Add("@InstrumentType", entityDto.InstrumentType);
                SpParameters.Add("@InstrumentNmbr", entityDto.InstrumentNmbr);
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@InstrumentDate", entityDto.InstrumentDate);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@ProductID", entityDto.ProductID);
                SpParameters.Add("@BankAccountID", entityDto.BankAccountID);
                SpParameters.Add("@InstIssuedByIndexID", entityDto.InstIssuedByIndexID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<string> AddUpdatePrepareWithdrawalAML(int companyId, int branchID, string userName, SLPrepareWithdrawal entityDto)
        {
            try
            {
                #region Insert New Data

                //formatting date
                entityDto.InstrumentDate = Utility.DatetimeFormatter.DateFormat(entityDto.InstrumentDate);

                string sp = "CM_InsertPrepareWithdrawalAML";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", entityDto.DisbursementID);
                SpParameters.Add("@InstrumentType", entityDto.InstrumentType);
                SpParameters.Add("@InstrumentNmbr", entityDto.InstrumentNmbr);
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@InstrumentDate", entityDto.InstrumentDate);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@ProductID", entityDto.ProductID);
                SpParameters.Add("@BankAccountID", entityDto.BankAccountID);
                SpParameters.Add("@InstIssuedByIndexID", entityDto.InstIssuedByIndexID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<string> AddChequeLeafSL(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto)
        {

            try
            {

                // if duplicate cheque exists
                var values = new { DisBankAccId = entityDto.DisbBankAccountID, BranchID = entityDto.BranchID };
                List<ChequeLeavesListDto> allCheques = await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesSL]", values);

                //foreach (var item in allCheques)
                //{
                //    if (item.LastChequePrint == null && item.FromCheque )
                //}

                #region Insert New Data

                string sp = "CM_InsertChequeLeafSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@FromCheque", entityDto.FromCheque);
                SpParameters.Add("@ToCheque", entityDto.ToCheque);
                SpParameters.Add("@LastChequePrint", entityDto.LastChequePrint);
                SpParameters.Add("@DisbBankAccountID", entityDto.DisbBankAccountID);
                SpParameters.Add("@BranchID", entityDto.BranchID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> AddChequeLeafIL(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto)
        {

            try
            {

                // if duplicate cheque exists
                var values = new { DisBankAccId = entityDto.DisbBankAccountID, BranchID = entityDto.BranchID };
                List<ChequeLeavesListDto> allCheques = await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesIL]", values);

                //foreach (var item in allCheques)
                //{
                //    if (item.LastChequePrint == null && item.FromCheque )
                //}

                #region Insert New Data

                string sp = "CM_InsertChequeLeafIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@FromCheque", entityDto.FromCheque);
                SpParameters.Add("@ToCheque", entityDto.ToCheque);
                SpParameters.Add("@LastChequePrint", entityDto.LastChequePrint);
                SpParameters.Add("@DisbBankAccountID", entityDto.DisbBankAccountID);
                SpParameters.Add("@BranchID", entityDto.BranchID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> AddChequeLeafAML(int companyId, int branchID, string userName, SLLicenseeAccChequeBookDto entityDto)
        {

            try
            {

                // if duplicate cheque exists
                var values = new { DisBankAccId = entityDto.DisbBankAccountID, BranchID = entityDto.BranchID };
                List<ChequeLeavesListDto> allCheques = await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesAML]", values);

                //foreach (var item in allCheques)
                //{
                //    if (item.LastChequePrint == null && item.FromCheque )
                //}

                #region Insert New Data

                string sp = "CM_InsertChequeLeafAML";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@FromCheque", entityDto.FromCheque);
                SpParameters.Add("@ToCheque", entityDto.ToCheque);
                SpParameters.Add("@LastChequePrint", entityDto.LastChequePrint);
                SpParameters.Add("@DisbBankAccountID", entityDto.DisbBankAccountID);
                SpParameters.Add("@BranchID", entityDto.BranchID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ChequeLeavesListDto>> ChequeLeaveListSL(int companyId, int branchID, int DisBankAccId)
        {
            var values = new { DisBankAccId = DisBankAccId, BranchID = branchID };
            return await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesSL]", values);
        }
        public async Task<List<ChequeLeavesListDto>> ChequeLeaveListIL(int companyId, int branchID, int DisBankAccId)
        {
            var values = new { DisBankAccId = DisBankAccId, BranchID = branchID };
            return await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesIL]", values);
        }
        public async Task<List<ChequeLeavesListDto>> ChequeLeaveListAML(int companyId, int branchID, int DisBankAccId)
        {
            var values = new { DisBankAccId = DisBankAccId, BranchID = branchID };
            return await _dbCommonOperation.ReadSingleTable<ChequeLeavesListDto>("[CM_ListChequeLeavesAML]", values);
        }


        public async Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintSL(int CompanyID, int BranchID, int DisBankAccID,int ProductID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, DisBankAccID = DisBankAccID , ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListValidateAndPrintSLDto>("[CM_ListValidateAndPrintChequeSL]", values);
        }
        public async Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintIL(int CompanyID, int BranchID, int DisBankAccID, int ProductID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, DisBankAccID = DisBankAccID, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListValidateAndPrintSLDto>("[CM_ListValidateAndPrintChequeIL]", values);
        }
        public async Task<List<ListValidateAndPrintSLDto>> ListValidateAndPrintAML(int CompanyID, int BranchID, int DisBankAccID, int ProductID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, DisBankAccID = DisBankAccID, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListValidateAndPrintSLDto>("[CM_ListValidateAndPrintChequeAML]", values);
        }


        public async Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccSL(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListDisbursementBankAccountSL>("[CM_ListDisBankAccSL]", values);
        }
        public async Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccIL(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListDisbursementBankAccountSL>("[CM_ListDisBankAccIL]", values);
        }
        public async Task<List<ListDisbursementBankAccountSL>> ListDisbursementBankAccAML(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListDisbursementBankAccountSL>("[CM_ListDisBankAccAML]", values);
        }

        public async Task<string> UpdateInstrumentStatusSL(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID)
        {

            try
            {

                #region Update Instrument

                string sp = "CM_UpdateInstrumentStatusSL";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", DisbursementID);
                SpParameters.Add("@MonInstrumentID", MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateInstrumentStatusIL(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID)
        {

            try
            {

                #region Update Instrument

                string sp = "CM_UpdateInstrumentStatusIL";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", DisbursementID);
                SpParameters.Add("@MonInstrumentID", MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateInstrumentStatusAML(int companyId, int branchID, string userName, int DisbursementID, int MonInstrumentID)
        {

            try
            {

                #region Update Instrument

                string sp = "CM_UpdateInstrumentStatusAML";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", branchID);
                SpParameters.Add("@DisbursementID", DisbursementID);
                SpParameters.Add("@MonInstrumentID", MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<object> ListGenOnlineTransferSL(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@DisBankAccountId", DisBankAccountId);
            Params[1] = new SqlParameter("@PaymentMode", PaymentMode);
            Params[2] = new SqlParameter("@ProductID", ProductID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListGenOnlineTransferSL]", Params);

            return new
            {
                DataList = DataSets.Tables[0]
            };
        }
        public async Task<object> ListGenOnlineTransferIL(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@DisBankAccountId", DisBankAccountId);
            Params[1] = new SqlParameter("@PaymentMode", PaymentMode);
            Params[2] = new SqlParameter("@ProductID", ProductID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListGenOnlineTransferIL]", Params);

            return new
            {
                DataList = DataSets.Tables[0]
            };
        }
        public async Task<object> ListGenOnlineTransferAML(int companyId, int branchID, int DisBankAccountId, string PaymentMode, int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@DisBankAccountId", DisBankAccountId);
            Params[1] = new SqlParameter("@PaymentMode", PaymentMode);
            Params[2] = new SqlParameter("@ProductID", ProductID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListGenOnlineTransferAML]", Params);

            return new
            {
                DataList = DataSets.Tables[0]
            };
        }

        public async Task<object> UpdateOnlineGeneratedInstrumentsSL(string userName, int companyId, int branchID, string monInsIDs)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_UpdateOnlineGenInstrumentStatusSL";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInsIDs", monInsIDs);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

                //StringBuilder sb = new StringBuilder();

                //foreach (var item in listOnlineTransfers)
                //{
                //    sb.Append(item.ProductName + "-" + item.AccountNumber + "-"
                //        + item.DisburseAmount + "-"
                //        + item.DisburseDate + "-"
                //        + item.BankBranch + "-"
                //        + item.RoutingNo + "-"
                //        + item.BankName + "-"
                //        + item.BAName + "-"
                //        + item.BANumber + "-"
                //        + item.MobileNo);
                //}

                //string FileName = "Online Transfer Insturment-" + DateTime.Now.ToString("ddmmyyhhmm") + ".txt";

                ////saving the file in server path
                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\GeneratedOnlineTransferFiles\" + FileName))
                //{
                //    file.WriteLine(sb.ToString()); // "sb" is the StringBuilder
                //}

                //return new
                //{
                //    OnlineGeneratedInstrumentContent = sb.ToString(),
                //    OnlineGeneratedInstrumentFileName = FileName,
                //    StatusUpdate = result
                //};
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> UpdateOnlineGeneratedInstrumentsIL(string userName, int companyId, int branchID, string monInsIDs)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_UpdateOnlineGenInstrumentStatusIL";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInsIDs", monInsIDs);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

                //StringBuilder sb = new StringBuilder();

                //foreach (var item in listOnlineTransfers)
                //{
                //    sb.Append(item.ProductName + "-" + item.AccountNumber + "-"
                //        + item.DisburseAmount + "-"
                //        + item.DisburseDate + "-"
                //        + item.BankBranch + "-"
                //        + item.RoutingNo + "-"
                //        + item.BankName + "-"
                //        + item.BAName + "-"
                //        + item.BANumber + "-"
                //        + item.MobileNo);
                //}

                //string FileName = "Online Transfer Insturment-" + DateTime.Now.ToString("ddmmyyhhmm") + ".txt";

                ////saving the file in server path
                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\GeneratedOnlineTransferFiles\" + FileName))
                //{
                //    file.WriteLine(sb.ToString()); // "sb" is the StringBuilder
                //}

                //return new
                //{
                //    OnlineGeneratedInstrumentContent = sb.ToString(),
                //    OnlineGeneratedInstrumentFileName = FileName,
                //    StatusUpdate = result
                //};
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> UpdateOnlineGeneratedInstrumentsAML(string userName, int companyId, int branchID, string monInsIDs)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_UpdateOnlineGenInstrumentStatusAML";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInsIDs", monInsIDs);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

                //StringBuilder sb = new StringBuilder();

                //foreach (var item in listOnlineTransfers)
                //{
                //    sb.Append(item.ProductName + "-" + item.AccountNumber + "-"
                //        + item.DisburseAmount + "-"
                //        + item.DisburseDate + "-"
                //        + item.BankBranch + "-"
                //        + item.RoutingNo + "-"
                //        + item.BankName + "-"
                //        + item.BAName + "-"
                //        + item.BANumber + "-"
                //        + item.MobileNo);
                //}

                //string FileName = "Online Transfer Insturment-" + DateTime.Now.ToString("ddmmyyhhmm") + ".txt";

                ////saving the file in server path
                //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\GeneratedOnlineTransferFiles\" + FileName))
                //{
                //    file.WriteLine(sb.ToString()); // "sb" is the StringBuilder
                //}

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentSL(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrAccountNo = InstrumentOrAccountNo, ProductID= ProductID };
            return await _dbCommonOperation.ReadSingleTable<VoidPaymentInstrumentSL>("[CM_GetVoidPaymentInstrumentSL]", values);

        }
        public async Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentIL(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrAccountNo = InstrumentOrAccountNo, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<VoidPaymentInstrumentSL>("[CM_GetVoidPaymentInstrumentIL]", values);

        }
        public async Task<List<VoidPaymentInstrumentSL>> GetVoidPaymentInstrumentAML(int companyId, int branchID, string RetrievalType, string InstrumentOrAccountNo, int? ProductID)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrAccountNo = InstrumentOrAccountNo, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<VoidPaymentInstrumentSL>("[CM_GetVoidPaymentInstrumentAML]", values);

        }

        public async Task<string> AddVoidPaymentInstrumentSL(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_InsertVoidPaymentInstrumentSL";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@VoidOperationType", entityDto.VoidOperationType);
                SpParameters.Add("@VoidReason", entityDto.VoidReason);
                SpParameters.Add("@MonInstrumentID", entityDto.MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> AddVoidPaymentInstrumentIL(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_InsertVoidPaymentInstrumentIL";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@VoidOperationType", entityDto.VoidOperationType);
                SpParameters.Add("@VoidReason", entityDto.VoidReason);
                SpParameters.Add("@MonInstrumentID", entityDto.MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> AddVoidPaymentInstrumentAML(string userName, int companyId, int branchID, EntryVoidPaymentInstrumentSL entityDto)
        {
            try
            {

                #region Update Instrument

                string sp = "CM_InsertVoidPaymentInstrumentAML";
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@VoidOperationType", entityDto.VoidOperationType);
                SpParameters.Add("@VoidReason", entityDto.VoidReason);
                SpParameters.Add("@MonInstrumentID", entityDto.MonInstrumentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalSL(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType , ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListVoidPaymentInstrumentApproval>("[CM_ListVoidPaymentInstrumentSL]", values);
        }
        public async Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalIL(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListVoidPaymentInstrumentApproval>("[CM_ListVoidPaymentInstrumentIL]", values);
        }
        public async Task<List<ListVoidPaymentInstrumentApproval>> ListVoidPaymentInstrumentApprovalAML(int CompanyID, int branchID, int PageNo, int Perpage, string SearchKeyword, string ListType, int ProductID)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<ListVoidPaymentInstrumentApproval>("[CM_ListVoidPaymentInstrumentAML]", values);
        }

        public async Task<string> VoidInstrumentOrReversalApprovalSL(string userName, int CompanyID, int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@VoidOrWithdrawalInstrumentID", VoidOrWithdrawalInstrumentID);
            SpParameters.Add("@ApprovalType", ApprovalType);
            SpParameters.Add("@IsApproved", IsApproved);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveVoidOrWithdrawalInstrumentSL", SpParameters);
        }
        public async Task<string> VoidInstrumentOrReversalApprovalAML(string userName, int CompanyID, int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@VoidOrWithdrawalInstrumentID", VoidOrWithdrawalInstrumentID);
            SpParameters.Add("@ApprovalType", ApprovalType);
            SpParameters.Add("@IsApproved", IsApproved);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveVoidOrWithdrawalInstrumentAML", SpParameters);
        }
        public async Task<string> VoidInstrumentOrReversalApprovalIL(string userName, int CompanyID, int branchID, int VoidOrWithdrawalInstrumentID, string ApprovalType, bool IsApproved, string ApprovalRemark)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@VoidOrWithdrawalInstrumentID", VoidOrWithdrawalInstrumentID);
            SpParameters.Add("@ApprovalType", ApprovalType);
            SpParameters.Add("@IsApproved", IsApproved);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveVoidOrWithdrawalInstrumentIL", SpParameters);
        }


        public async Task<List<PostOnlineTransferSL>> GetPostOnlineTransferSL(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID)
        {
            var values = new { userName = userName, DisbursementBankAccID = DisbursementBankAccID, PaymentMode = PaymentMode , ProductID= ProductID };
            return await _dbCommonOperation.ReadSingleTable<PostOnlineTransferSL>("[CM_GetPostOnlineTransferSL]", values);
        }
        public async Task<List<PostOnlineTransferSL>> GetPostOnlineTransferIL(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID)
        {
            var values = new { userName = userName, DisbursementBankAccID = DisbursementBankAccID, PaymentMode = PaymentMode, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<PostOnlineTransferSL>("[CM_GetPostOnlineTransferIL]", values);
        }
        public async Task<List<PostOnlineTransferSL>> GetPostOnlineTransferAML(string userName, int companyId, int branchID, int DisbursementBankAccID, string PaymentMode, int ProductID)
        {
            var values = new { userName = userName, DisbursementBankAccID = DisbursementBankAccID, PaymentMode = PaymentMode, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<PostOnlineTransferSL>("[CM_GetPostOnlineTransferAML]", values);
        }

        public async Task<string> UpdatePostOnlineTransferSL(string userName, int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer)
        {
            try
            {
                string sp = "CM_UpdatePostOnlineTransferSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@OperationType", entryOnlineTransfer.OperationType);
                SpParameters.Add("@OnlineTransfers", ListtoDataTableConverter.ToDataTable(entryOnlineTransfer.postOnlineTransfers).AsTableValuedParameter("Type_PostOnlineTransferSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdatePostOnlineTransferIL(string userName, int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer)
        {
            try
            {
                string sp = "CM_UpdatePostOnlineTransferIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@OperationType", entryOnlineTransfer.OperationType);
                SpParameters.Add("@OnlineTransfers", ListtoDataTableConverter.ToDataTable(entryOnlineTransfer.postOnlineTransfers).AsTableValuedParameter("Type_PostOnlineTransferSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdatePostOnlineTransferAML(string userName, int CompanyID, int BranchID, EntryOnlineTransfer entryOnlineTransfer)
        {
            try
            {
                string sp = "CM_UpdatePostOnlineTransferAML";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@OperationType", entryOnlineTransfer.OperationType);
                SpParameters.Add("@OnlineTransfers", ListtoDataTableConverter.ToDataTable(entryOnlineTransfer.postOnlineTransfers).AsTableValuedParameter("Type_PostOnlineTransferSL"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ChequeClearInfo>> GetChequeClearInfoSL(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrDisbAccountNo = InstrumentOrDisbAccountNo };
            return await _dbCommonOperation.ReadSingleTable<ChequeClearInfo>("[CM_GetChequeClearInfoSL]", values);
        }
        public async Task<List<ChequeClearInfo>> GetChequeClearInfoIL(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrDisbAccountNo = InstrumentOrDisbAccountNo };
            return await _dbCommonOperation.ReadSingleTable<ChequeClearInfo>("[CM_GetChequeClearInfoIL]", values);
        }
        public async Task<List<ChequeClearInfo>> GetChequeClearInfoAML(string userName, int CompanyID, int BranchID, string RetrievalType, string InstrumentOrDisbAccountNo)
        {
            var values = new { RetrievalType = RetrievalType, InstrumentOrDisbAccountNo = InstrumentOrDisbAccountNo };
            return await _dbCommonOperation.ReadSingleTable<ChequeClearInfo>("[CM_GetChequeClearInfoAML]", values);
        }

        public async Task<string> UpdateChequeClearSL(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo)
        {

            try
            {
                string sp = "CM_UpdateClearChequeSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInstrumentID", chequeClearInfo.MonInstrumentID);
                SpParameters.Add("@PaymentDetailID", chequeClearInfo.PaymentDetailID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateChequeClearIL(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo)
        {

            try
            {
                string sp = "CM_UpdateClearChequeIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInstrumentID", chequeClearInfo.MonInstrumentID);
                SpParameters.Add("@PaymentDetailID", chequeClearInfo.PaymentDetailID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateChequeClearAML(string userName, int CompanyID, int BranchID, ChequeClearInfo chequeClearInfo)
        {

            try
            {
                string sp = "CM_UpdateClearChequeAML";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@MonInstrumentID", chequeClearInfo.MonInstrumentID);
                SpParameters.Add("@PaymentDetailID", chequeClearInfo.PaymentDetailID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ReleaseChequeLeafSL(string userName, int CompanyID, int BranchID, int ProductID, int DisbBankAccountID, long FromCheque, long ToCheque)
        {
            try
            {
                string sp = "CM_ReleaseChequeLeafSL";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ProductID", ProductID);
                SpParameters.Add("@BankAccountID", DisbBankAccountID);
                SpParameters.Add("@FromCheque", FromCheque);
                SpParameters.Add("@ToCheque", ToCheque);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ReleaseChequeLeafIL(string userName, int CompanyID, int BranchID, int ProductID, int DisbBankAccountID, long FromCheque, long ToCheque)
        {
            try
            {
                string sp = "CM_ReleaseChequeLeafIL";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ProductID", ProductID);
                SpParameters.Add("@BankAccountID", DisbBankAccountID);
                SpParameters.Add("@FromCheque", FromCheque);
                SpParameters.Add("@ToCheque", ToCheque);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> ReleaseChequeLeafAML(string userName, int CompanyID, int BranchID, int ProductID, int DisbBankAccountID, long FromCheque, long ToCheque)
        {
            try
            {
                string sp = "CM_ReleaseChequeLeafAML";
                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@ProductID", ProductID);
                SpParameters.Add("@BankAccountID", DisbBankAccountID);
                SpParameters.Add("@FromCheque", FromCheque);
                SpParameters.Add("@ToCheque", ToCheque);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafSL(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID)
        {
            var values = new { userName = userName, BankAccountID = BankAccountID, ProductID = ProductID, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListReleaseChequeLeafSL>("[CM_GetReleaseChequesSL]", values);
        }
        public async Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafIL(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID)
        {
            var values = new { userName = userName, BankAccountID = BankAccountID, ProductID = ProductID, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListReleaseChequeLeafSL>("[CM_GetReleaseChequesIL]", values);
        }
        public async Task<List<ListReleaseChequeLeafSL>> ListReleaseChequeLeafAML(string userName, int CompanyID, int BranchID, int ProductID, string BankAccountID)
        {
            var values = new { userName = userName, BankAccountID = BankAccountID, ProductID = ProductID, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListReleaseChequeLeafSL>("[CM_GetReleaseChequesAML]", values);
        }

        public async Task<object> FilterListWithdrawalSL(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter)
        {

            SqlParameter[] SpParameters = new SqlParameter[6];
            SpParameters[0] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[1] = new SqlParameter("@BranchID", BranchID);
            SpParameters[2] = new SqlParameter("@UserName", UserName);
            SpParameters[3] = new SqlParameter("@List_Type", filter.List_Type);
            SpParameters[4] = new SqlParameter("@WithdrawalFrom", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalFrom));
            SpParameters[5] = new SqlParameter("@WithdrawalTo", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalTo));

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_FilterListWithdrawalSL]", SpParameters);

            var Result = new
            {
                Data = dataset.Tables[0],

            };

            return await Task.FromResult(Result); ;

        }
        public async Task<object> FilterListWithdrawalIL(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter)
        {

            SqlParameter[] SpParameters = new SqlParameter[6];
            SpParameters[0] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[1] = new SqlParameter("@BranchID", BranchID);
            SpParameters[2] = new SqlParameter("@UserName", UserName);
            SpParameters[3] = new SqlParameter("@List_Type", filter.List_Type);
            SpParameters[4] = new SqlParameter("@WithdrawalFrom", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalFrom));
            SpParameters[5] = new SqlParameter("@WithdrawalTo", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalTo));

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_FilterListWithdrawalIL]", SpParameters);

            var Result = new
            {
                Data = dataset.Tables[0],

            };

            return await Task.FromResult(Result); ;

        }
        public async Task<object> FilterListWithdrawalAML(int CompanyID, int BranchID, string UserName, WithdrawalSearchFilterDto filter)
        {

            SqlParameter[] SpParameters = new SqlParameter[6];
            SpParameters[0] = new SqlParameter("@CompanyID", CompanyID);
            SpParameters[1] = new SqlParameter("@BranchID", BranchID);
            SpParameters[2] = new SqlParameter("@UserName", UserName);
            SpParameters[3] = new SqlParameter("@List_Type", filter.List_Type);
            SpParameters[4] = new SqlParameter("@WithdrawalFrom", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalFrom));
            SpParameters[5] = new SqlParameter("@WithdrawalTo", Utility.DatetimeFormatter.DateFormat(filter.WithdrawalTo));

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_FilterListWithdrawalAML]", SpParameters);

            var Result = new
            {
                Data = dataset.Tables[0],

            };

            return await Task.FromResult(Result); ;

        }

        public async Task<string> BulkPrepareInstrumentSL(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry)
        {
            string sp = "CM_BulkPrepareInstrumentSL";

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@DisbursementIDs", entry.DisbursementIDs);
            SpParameters.Add("@TransactionMode", entry.TransactionMode);
            SpParameters.Add("@PaymentBankAccountId", entry.PaymentBankAccountId);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }

        public async Task<string> BulkPrepareInstrumentIL(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry)
        {
            string sp = "CM_BulkPrepareInstrumentIL";

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@DisbursementIDs", entry.DisbursementIDs);
            SpParameters.Add("@TransactionMode", entry.TransactionMode);
            SpParameters.Add("@PaymentBankAccountId", entry.PaymentBankAccountId);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }

        public async Task<string> BulkPrepareInstrumentAML(string userName, int CompanyID, int BranchID, BulkPrepareInstrumentDto entry)
        {
            string sp = "CM_BulkPrepareInstrumentAML";

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@DisbursementIDs", entry.DisbursementIDs);
            SpParameters.Add("@TransactionMode", entry.TransactionMode);
            SpParameters.Add("@PaymentBankAccountId", entry.PaymentBankAccountId);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }

        public async Task<object> GetRecentSurrenderAML(int CompanyID, int BranchID, string UserName, int ContractID)
        {
            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@UserName", UserName);
            Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ContractID", ContractID);
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetRecentSurrenderAML]", Params);

            return DataSets.Tables[0];

        }

        public async Task<object> BulkInstrumentVoid(int CompanyID, int BranchID, string UserName, List<BulkInstrumentVoid> bulkInstrumentVoid)
        {
            //foreach (var account in accountList) { account.AccountNumber = account.AccountNumber.Trim(); }

            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@AccountList", ListtoDataTableConverter.ToDataTable(bulkInstrumentVoid));

           
            DataSet DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAccountVoidBulkAML]", sqlParams);


            //return DataSets.Tables[0];

            //string sp = "CM_InsertAccountVoidBulkAML";

            //DataTable MemberAccInfo = new DataTable();
            //DynamicParameters SpParameters = new DynamicParameters();
            //SpParameters.Add("@UserName", UserName);
            //SpParameters.Add("@CompanyID", CompanyID);
            //SpParameters.Add("@BranchID", BranchID);
            //SpParameters.Add("@AccountList", ListtoDataTableConverter.ToDataTable(bulkInstrumentVoid).AsTableValuedParameter("Type_AccountVoidBulk"));
            //SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);



            //return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            return DataSets.Tables[0];

        }

        public async Task<string> ApproveBulkInstrumentVoid(string userName, int CompanyID, int branchID, BulkInstrumentVoidApprovalDto approvalDto)
        {

            DynamicParameters SpParameters = new DynamicParameters();


            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@VoidOrWithdrawalInstrumentIDs", approvalDto.VoidOrWithdrawalInstrumentIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveVoidOrWithdrawalInstrumentBulkAML", SpParameters);

        }


    }
}


