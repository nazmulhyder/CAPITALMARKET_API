using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs;
using Model.DTOs.IPO;
using Model.DTOs.Right;
using Model.DTOs.Withdrawal;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class CorpActRepository: ICorpActRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public CorpActRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> InsertRightInstrumentSetting(RightDTO entryRightDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;
            try
            {

                string sp = "CM_InsertUpdateRightInstrumentSetting";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@RightInstSettingID", entryRightDTO.RightInstSettingID);
                SpParameters.Add("@InstrumentId", entryRightDTO.InstrumentID);
                SpParameters.Add("@DeclareDate", entryRightDTO.DeclareDate);
                SpParameters.Add("@RecordDate", entryRightDTO.RecordDate);
                SpParameters.Add("@FaceValue", entryRightDTO.FaceValue);
                SpParameters.Add("@Premium", entryRightDTO.Premium);
                SpParameters.Add("@Rights", entryRightDTO.Rights);
                SpParameters.Add("@Holdings", entryRightDTO.Holdings);
                SpParameters.Add("@SubscriptionOpenDate", entryRightDTO.SubscriptionOpenDate);
                SpParameters.Add("@SubscriptionClosedDate", entryRightDTO.SubscriptionClosedDate);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@ServiceCharge", entryRightDTO.ServiceCharge);
                SpParameters.Add("@SrvChargeIsPercentage", String.IsNullOrWhiteSpace(entryRightDTO.SrvChargeIsPercentage)? null : Convert.ToBoolean(entryRightDTO.SrvChargeIsPercentage));
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<RightDTO>> GetAllCorpActResult(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,

            };
            return _dbCommonOperation.ReadSingleTable<RightDTO>("[CM_ListCorpActionResult]", values);
        }

        public async Task<CorpActDetailsDTO> GetCorpActRightListbyID(int RightInstSettingID)
        {
            var values = new { RightInstSettingID = RightInstSettingID };
            var res= await _dbCommonOperation.ReadSingleTable<CorpActDetailsDTO>("[CM_QueryCorporateActionRight]", values);
            return res.FirstOrDefault();
        }

        public async Task<string> GetCorpActApproval(RightApproveDTO objCorpActRightApproval, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_ApproveCorporateActionRight";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@RightInstSettingID", objCorpActRightApproval.RightInstSettingID);
                SpParameters.Add("@ApprovalRemark", objCorpActRightApproval.ApprovalRemark);
                SpParameters.Add("@ApprovalStatus", objCorpActRightApproval.ApprovalStatus);
                SpParameters.Add("@UserName", UserName);


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public InstrumentApplicationRights GetInstrumentInformationforRightsApplication(int RightInstSettingID, int CompanyID, int BranchID)
        {
            InstrumentApplicationRights Instrument = new InstrumentApplicationRights();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@RightInstSettingID", RightInstSettingID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)

           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListInstrumentInformationforRightsApplication]", sqlParams);

            Instrument = CustomConvert.DataSetToList<InstrumentApplicationRights>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public object GetInvestorInformationforRightsApplication(int RightInstSettingID, int ProductID, string AccountNumber, int CompanyID, int BranchID)
        {
            InvestorInformationRights Instrument = new InvestorInformationRights();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@ProductID", ProductID),
                new SqlParameter("@AccountNumber", AccountNumber),
                  new SqlParameter("@RightInstSettingID", RightInstSettingID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)

           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListInvestorInformationforRightsApplication]", sqlParams);

            Instrument = CustomConvert.DataSetToList<InvestorInformationRights>(DataSets.Tables[0]).First();
            List<InvestorInformationRightsDetails> ObjCorpActInvestorInfo = Utility.CustomConvert.DataSetToList<InvestorInformationRightsDetails>(DataSets.Tables[1]);

            var result = new
            {
                Instrument = Instrument,
                ObjCorpActInvestorInfo = ObjCorpActInvestorInfo
            };
            return result;
        }

        public async Task<string> InsertRightApplication(RightApplicatonDTO entryRightAppDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertupdateRightsApplication";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@RightsApplicationId", entryRightAppDTO.RightsApplicationID);
                SpParameters.Add("@RightInstSettingID", entryRightAppDTO.RightInstSettingID);
                SpParameters.Add("@ProductID", entryRightAppDTO.ProductID);
                SpParameters.Add("@ContractID", entryRightAppDTO.ContractID);
                SpParameters.Add("@Quantity", entryRightAppDTO.AppliedQuantity);
                SpParameters.Add("@Rate", entryRightAppDTO.Rate);
                SpParameters.Add("@ServiceCharge", entryRightAppDTO.ServiceCharge);
                SpParameters.Add("@MFBankAccountID", entryRightAppDTO.MFBankAccountID); 
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public Task<List<RightsApplicationforApprovalDTO>> GetRightsApplicationOrderList(int RightInstSettingID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                RightInstSettingID = RightInstSettingID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<RightsApplicationforApprovalDTO>("[CMListRightsApplicationforApproval]", values);
        }

        public async Task<RightApplicatonListDTO> GetRightsApplicationListbyID(int RightsApplicationID, int CompanyID, int BranchID)
        {
            var values = new 
            { 
                RightsApplicationID = RightsApplicationID,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            var res = await _dbCommonOperation.ReadSingleTable<RightApplicatonListDTO>("[CM_QueryRightsApplicationListbyID]", values);
            return res.FirstOrDefault();
        }

        public Task<List<RightsApplicationforApprovalDTO>> GetRightsApplicationforApproval(int RightInstSettingID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                RightInstSettingID = RightInstSettingID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<RightsApplicationforApprovalDTO>("[CMListRightsApplicationforApproval]", values);
        }

        public async Task<string> RightsApplicationApproved(RightsApplication RightApplicatonListApproved, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMApprovedRightsApplication";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@Status", RightApplicatonListApproved.Status);
                SpParameters.Add("@ApprovalRemark", RightApplicatonListApproved.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CMRightsApproval", ListtoDataTableConverter.ToDataTable(RightApplicatonListApproved.corporateApprovedList).AsTableValuedParameter("Type_CMRightsApproval"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<RightsReversalDTO>> GetRightsReversalList(int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<RightsReversalDTO>("[RightsReversalApprovalList]", values);
        }

        public async Task<string> InsertCARReversalApproval(RightsReversalMaster entryRightsReversal, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "RightsReversalApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", entryRightsReversal.Status);
                SpParameters.Add("@ApprovalRemark", entryRightsReversal.ApprovalRemark);
                SpParameters.Add("@RightsReverseID", entryRightsReversal.RightsReverseID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                RightDTO PrevEquityInstrument = new RightDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<RightsBulkApproved>> GetRightsApplicationBulkList(int ProductID, int RightInstSettingID, string Maker, int CompanyID, int BranchID)
        {
            var values = new
            {
                RightInstSettingID = RightInstSettingID,
                ProductID = ProductID,
                Maker = Maker,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<RightsBulkApproved>("[ListBulkInsertRightsApplication]", values);
        }

        public async Task<string> InsertCARightBulk(RightsBulkMaster entryRightsBulk, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertupdateBulkRightsApplication ";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@ProductID", entryRightsBulk.ProductID);
                SpParameters.Add("@RightsInstrumentID", entryRightsBulk.RightsInstrumentID);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);

                SpParameters.Add("@CMRightsBulkDetails", ListtoDataTableConverter.ToDataTable(entryRightsBulk.RightsBulkInsertList).AsTableValuedParameter("Type_CMBulkRightsDetails"));
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                RightDTO PrevEquityInstrument = new RightDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<RightsCollection>> GetAMLCARInstrumentCollectionList(int RightInstSettingID, int CompanyID, int BranchID)
        {
            var values = new
            {
                RightInstSettingID = RightInstSettingID,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<RightsCollection>("[ILAML_CARQueryInstrumentCollection]", values);
        }

        public async Task<string> AMLCorpActRightInstrumentApproveRequest(AMLRightsCollectionMaster? AMLCorpActRightCollectionReq, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "AML_ApprovedCorpActionRightInsturment";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@@AMLCorpActRightInstrument", ListtoDataTableConverter.ToDataTable(AMLCorpActRightCollectionReq.corpActRightInstrumentCollectionList).AsTableValuedParameter("Type_AMLCorpActRightInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<AMLCorpActRightCollectionApprovalList>> GetCorpActRightCollectionApprovalList(int RightInstSettingID,int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                RightInstSettingID = RightInstSettingID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<AMLCorpActRightCollectionApprovalList>("[CorpActRightCollectionApprovalList]", values);
        }

        public async Task<String> AMLCorpActRightInstrumentApproved(AMLCorpActRightCollectionApproved AMLCorpActRightInstrumentList, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAML_RightsCollectionApproved";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ApprovalRemark", AMLCorpActRightInstrumentList.ApprovalRemark);
                SpParameters.Add("@Status", AMLCorpActRightInstrumentList.Status);
                SpParameters.Add("@RightInstSettingID", AMLCorpActRightInstrumentList.RightInstSettingID);
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@AMLCorpActRightInstrument", ListtoDataTableConverter.ToDataTable(AMLCorpActRightInstrumentList.AMLCorpActRightCollectionApprovedList).AsTableValuedParameter("Type_AMLCorpActRightInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<RightDTO>> GetAllCorpActDeclarationApproval(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,

            };
            return _dbCommonOperation.ReadSingleTable<RightDTO>("[CM_ListCorpActionDeclarationApproval]", values);
        }

        public async Task<string> CARApplicationApprovedbyID(RightsApplication data, int RightsApplicationID, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMApprovedRightsApplicationByID";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@Status", data.Status);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@RightsApplicationID", RightsApplicationID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //public async Task<string> InsertCorpActRightDividend(CorpActRightDeclarationDTO entryCorpActRightDeclaration, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {

        //        string sp = "CM_InsertUpdateCorporateActionDeclaration";

        //        DynamicParameters SpParameters = new DynamicParameters();
        //        SpParameters.Add("@CorpActDeclarationID", entryCorpActRightDeclaration.CorpActDeclarationID);
        //        SpParameters.Add("@InstrumentID", entryCorpActRightDeclaration.InstrumentID);
        //        SpParameters.Add("@DividendYear", entryCorpActRightDeclaration.DividendYear);
        //        SpParameters.Add("@IsInterim", entryCorpActRightDeclaration.IsInterim);
        //        SpParameters.Add("@DeclareDate", entryCorpActRightDeclaration.DeclareDate);
        //        SpParameters.Add("@RecordDate", entryCorpActRightDeclaration.RecordDate);
        //        SpParameters.Add("@AGMDate", entryCorpActRightDeclaration.AGMDate);
        //        SpParameters.Add("@CashDivPercentage", entryCorpActRightDeclaration.CashDivPercentage);
        //        SpParameters.Add("@StockDivPercentage", entryCorpActRightDeclaration.StockDivPercentage);
        //        SpParameters.Add("@Maker", Maker);
        //        //SpParameters.Add("@Makedate", entryCorpActRightDeclaration.Makedate);
        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivResult(int CompanyID, int BranchID)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,

        //    };
        //    return _dbCommonOperation.ReadSingleTable<CorpActRightDeclarationListDTO>("[CM_ListCorpActionDividendResult]", values);
        //}

        //public async Task<CorpActRightDeclarationListDTO> GetCorpActDividendListbyID(int CorpActDeclarationID)
        //{
        //    var values = new { CorpActDeclarationID = CorpActDeclarationID };
        //    var res = await _dbCommonOperation.ReadSingleTable<CorpActRightDeclarationListDTO>("[CM_QueryCorporateActionDividend]", values);
        //    return res.FirstOrDefault();
        //}

        //public async Task<string> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "CM_ApproveCorporateActionDividend";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@CorpActDeclarationID", objCorpActDividendApproval.CorpActDeclarationID);
        //        SpParameters.Add("@ApprovalRemark", objCorpActDividendApproval.ApprovalRemark);
        //        SpParameters.Add("@ApprovalStatus", objCorpActDividendApproval.ApprovalStatus);
        //        SpParameters.Add("@UserName", UserName);


        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public Task<List<ILAMLCorpActionClaimListDTO>> GetCorpActionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        CorpActDeclarationID = CorpActDeclarationID,
        //        Maker = Maker
        //    };
        //    return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionClaimListDTO>("[ILAMLListCorpActionBonusClaim]", values);
        //}

        //public async Task<string> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "ILAMLInsertUpdateCorActBonusClaim";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@Maker", UserName);
        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@@ILAMLCorpActionBonusClaimList", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionClaim.ILAMLCorpActClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimList"));

        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        UserName = UserName,
        //        CorpActDeclarationID = CorpActDeclarationID

        //    };
        //    return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionClaimListApprovalDTO>("[ILAMLListCorpActBonusClaimApproval]", values);
        //}

        //public async Task<string> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {

        //        string sp = "CMCorpActBonusClaimApproval";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@UserName", UserName);
        //        SpParameters.Add("@Status", entryBonusApproval.Status);
        //        SpParameters.Add("@ApprovalRemark", entryBonusApproval.ApprovalRemark);
        //        SpParameters.Add("@CorpActDeclarationID", entryBonusApproval.CorpActDeclarationID);
        //        SpParameters.Add("@ILAMLCorpActionBonusClaimListApproved", ListtoDataTableConverter.ToDataTable(entryBonusApproval.ILAMLCorpActClaimAList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimListApproved"));
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        RightDTO PrevEquityInstrument = new RightDTO();

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public async Task<string> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "ILAMLUpdateCoprActBonusClaim";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@BonusEntitlement", objILAMLUpdateStockDivClaimApprove.BonusEntitlement);
        //        SpParameters.Add("@ILAMLCorpActionBonusClaimListApproved", ListtoDataTableConverter.ToDataTable(objILAMLUpdateStockDivClaimApprove.ILAMLCorpActClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimListApproved"));
        //        SpParameters.Add("@Maker", Maker);

        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public Task<List<ILAMLCorpActionCashClaimListDTO>> GetCorpActionCashClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        CorpActDeclarationID = CorpActDeclarationID,
        //        Maker = Maker
        //    };
        //    return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionCashClaimListDTO>("[ILAMLListCorpActionCashClaim]", values);
        //}

        //public async Task<string> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "ILAMLInsertUpdateCorporateActionCashClaim";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@Maker", UserName);
        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionCashClaim.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));

        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public Task<List<ILAMLCorpActionCashClaimListDTO>> GetILAMLCashClaimApprovalList(int CorpActDeclarationID ,int CompanyID, int BranchID, string UserName)
        //{
        //    var values = new
        //    {
        //        CorpActDeclarationID = CorpActDeclarationID,
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        UserName = UserName

        //    };
        //    return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionCashClaimListDTO>("[ILAMLListCorpActionCashClaimApproval]", values);
        //}

        //public async Task<string> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {

        //        string sp = "CMCorpActionBonusCashClaimApproval";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@UserName", UserName);
        //        SpParameters.Add("@Status", entryBonusCashApproval.Status);
        //        SpParameters.Add("@ApprovalRemark", entryBonusCashApproval.ApprovalRemark);
        //        SpParameters.Add("@CorpActDeclarationID", entryBonusCashApproval.CorpActDeclarationID);
        //        SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(entryBonusCashApproval.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        RightDTO PrevEquityInstrument = new RightDTO();

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public async Task<string> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "ILAMLUpdateCashDivClaimApproval";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@NetAmount", objILAMLUpdateCashDivClaimApprove.NetAmount);
        //        SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(objILAMLUpdateCashDivClaimApprove.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));
        //        SpParameters.Add("@Maker", Maker);

        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}
    }
}
