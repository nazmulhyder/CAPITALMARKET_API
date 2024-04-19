using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Right;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class CorporateActionDividendRepository : ICorporateActionDividendRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public CorporateActionDividendRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> InsertCorpActRightDividend(CorpActRightDeclarationDTO entryCorpActRightDeclaration, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdateCorporateActionDeclaration";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@CorpActDeclarationID", entryCorpActRightDeclaration.CorpActDeclarationID);
                SpParameters.Add("@InstrumentID", entryCorpActRightDeclaration.InstrumentID);
                SpParameters.Add("@DividendYear", entryCorpActRightDeclaration.DividendYear);
                SpParameters.Add("@IsInterim", entryCorpActRightDeclaration.IsInterim);
                //SpParameters.Add("@DeclareDate", entryCorpActRightDeclaration.DeclareDate);
                SpParameters.Add("@RecordDate", entryCorpActRightDeclaration.RecordDate);
                SpParameters.Add("@YearEndDate", entryCorpActRightDeclaration.YearEndDate);
                SpParameters.Add("@AGMDate", entryCorpActRightDeclaration.AGMDate);
                SpParameters.Add("@CashDivPercentage", entryCorpActRightDeclaration.CashDivPercentage);
                SpParameters.Add("@StockDivPercentage", entryCorpActRightDeclaration.StockDivPercentage);
                SpParameters.Add("@Maker", Maker);
                //SpParameters.Add("@Makedate", entryCorpActRightDeclaration.Makedate);
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

        public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivResult(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,

            };
            return _dbCommonOperation.ReadSingleTable<CorpActRightDeclarationListDTO>("[CM_ListCorpActionDividendResult]", values);
        }

        public Task<List<CorpActRightDeclarationListDTO>> GetAllCorpActDivDeclaration(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,

            };
            return _dbCommonOperation.ReadSingleTable<CorpActRightDeclarationListDTO>("[CM_ListCorpActionDividendDeclartion]", values);
        }

        public async Task<CorpActRightDeclarationListDTO> GetCorpActDividendListbyID(int CorpActDeclarationID)
        {
            var values = new { CorpActDeclarationID = CorpActDeclarationID };
            var res = await _dbCommonOperation.ReadSingleTable<CorpActRightDeclarationListDTO>("[CM_QueryCorporateActionDividend]", values);
            return res.FirstOrDefault();
        }

        public async Task<string> GetCorpActDividendApproval(CorpActDeclarationApproveDTO objCorpActDividendApproval, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_ApproveCorporateActionDividend";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@CorpActDeclarationID", objCorpActDividendApproval.CorpActDeclarationID);
                SpParameters.Add("@ApprovalRemark", objCorpActDividendApproval.ApprovalRemark);
                SpParameters.Add("@ApprovalStatus", objCorpActDividendApproval.ApprovalStatus);
                SpParameters.Add("@UserName", UserName);


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionClaimListDTO>> GetCorpActionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorpActDeclarationID = CorpActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionClaimListDTO>("[ILAMLListCorpActionBonusClaim]", values);
        }

        public async Task<string> ILAMLCorpActionClaim(ILAMLCorpActionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLInsertUpdateCorActBonusClaim";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@@ILAMLCorpActionBonusClaimList", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionClaim.ILAMLCorpActClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName,
                CorpActDeclarationID = CorpActDeclarationID

            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionClaimListApprovalDTO>("[ILAMLListCorpActBonusClaimApproval]", values);
        }

        public async Task<string> InsertCADBonusClaimApproval(ILAMLCorpActionClaimApprove entryBonusApproval, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMCorpActBonusClaimApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", entryBonusApproval.Status);
                SpParameters.Add("@ApprovalRemark", entryBonusApproval.ApprovalRemark);
                SpParameters.Add("@CorpActDeclarationID", entryBonusApproval.CorpActDeclarationID);
                SpParameters.Add("@ILAMLCorpActionBonusClaimListApproved", ListtoDataTableConverter.ToDataTable(entryBonusApproval.ILAMLCorpActClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimListApproved"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CorporateActionDividendDTO PrevEquityInstrument = new CorporateActionDividendDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetILAMLUpdateStockDivClaimApprove(ILAMLUpdateStockDivClaimApprove objILAMLUpdateStockDivClaimApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLUpdateCoprActBonusClaim";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@BonusEntitlement", objILAMLUpdateStockDivClaimApprove.BonusEntitlement);
                SpParameters.Add("@ILAMLCorpActionBonusClaimListApproved", ListtoDataTableConverter.ToDataTable(objILAMLUpdateStockDivClaimApprove.ILAMLCorpActClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimListApproved"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionCashClaimListDTO>> GetCorpActionCashClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorpActDeclarationID = CorpActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionCashClaimListDTO>("[ILAMLListCorpActionCashClaim]", values);
        }

        public async Task<string> ILAMLCorpActionCashClaim(ILAMLCorpActionCashClaim objILAMLCorpActionCashClaim, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLInsertUpdateCorporateActionCashClaim";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionCashClaim.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionCashClaimListDTO>> GetILAMLCashClaimApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                CorpActDeclarationID = CorpActDeclarationID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionCashClaimListDTO>("[ILAMLListCorpActionCashClaimApproval]", values);
        }

        public async Task<string> InsertCADBonusCashClaimApproval(ILAMLCorpActionCashClaim entryBonusCashApproval, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMCorpActionBonusCashClaimApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", entryBonusCashApproval.Status);
                SpParameters.Add("@ApprovalRemark", entryBonusCashApproval.ApprovalRemark);
                SpParameters.Add("@CorpActDeclarationID", entryBonusCashApproval.CorpActDeclarationID);
                SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(entryBonusCashApproval.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CorporateActionDividendDTO PrevEquityInstrument = new CorporateActionDividendDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetILAMLUpdateCashDivClaimApprove(ILAMLUpdateCashDivClaimApprove objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLUpdateCashDivClaimApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@NetAmount", objILAMLUpdateCashDivClaimApprove.NetAmount);
                SpParameters.Add("@ILAMLCorpActionCashClaimList", ListtoDataTableConverter.ToDataTable(objILAMLUpdateCashDivClaimApprove.ILAMLCorpActCashClaimList).AsTableValuedParameter("Type_ILAMLCorpActionCashClaimList"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionBonusFractionClaimListDTO>> GetCorpActionBonusFractionClaimList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorpActDeclarationID = CorpActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionBonusFractionClaimListDTO>("[ILAMLCorpActionBonusFractionClaimList]", values);
        }

        public async Task<string> ILAMLCorpActionBonusFractionClaim(ILAMLCorpActionBonusFractionClaim objILAMLCorpActionBonusFractionClaim, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLInsertUpdateCorActBonusFractionClaim";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ILAMLCorpActionBonusFractionClaim", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionBonusFractionClaim.ILAMLCorpActBonusFractionClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusFractionClaim"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionBonusFractionClaimListDTO>> ILAMLCorpActionBonusFractionClaimApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                CorActDeclarationID = CorActDeclarationID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionBonusFractionClaimListDTO>("[ILAMLListCorpActionBonusFractionClaimApproval]", values);
        }

        public async Task<string> GetILAMLUpdateBonusFractionClaimApprove(ILAMLCorpActionBonusFractionUpdate objILAMLUpdateCashDivClaimApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLUpdateBonusFractionClaimApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@BonusEntitlement", objILAMLUpdateCashDivClaimApprove.BonusEntitlement);
                SpParameters.Add("@ILAMLCorpActionBonusFractionClaim", ListtoDataTableConverter.ToDataTable(objILAMLUpdateCashDivClaimApprove.ILAMLCorpActBonusFractionClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusFractionClaim"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> InsertSLCorpActDivCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActDivCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertUpdateCorpActDivCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Status", entryCorpActDivCollection.Status);
                SpParameters.Add("@CorpActDeclarationID", entryCorpActDivCollection.CorpActDeclarationID);
                SpParameters.Add("@SLCorpActionDividendClaim", ListtoDataTableConverter.ToDataTable(entryCorpActDivCollection.SLCorpActCashClaimList).AsTableValuedParameter("Type_SLCorpActionDividendClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> InsertILAMLCorpActDividendBonusFractionClaimApproval(ILAMLCorpActionBonusFractionClaim entryBonusFractionApproval, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMCorpActionBonusFractionClaimApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", entryBonusFractionApproval.Status);
                SpParameters.Add("@ApprovalRemark", entryBonusFractionApproval.ApprovalRemark);
                SpParameters.Add("@CorpActDeclarationID", entryBonusFractionApproval.CorpActDeclarationID);
                SpParameters.Add("@ILAMLCorpActionBonusFractionClaim", ListtoDataTableConverter.ToDataTable(entryBonusFractionApproval.ILAMLCorpActBonusFractionClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusFractionClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CorporateActionDividendDTO PrevEquityInstrument = new CorporateActionDividendDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<SLCorporateActionCashCollectionListDTO>> GetAllCorpActCashCollectionResult(int CorActDeclarationID, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorActDeclarationID = CorActDeclarationID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SLCorporateActionCashCollectionListDTO>("[CMListCorpActionCashCollection]", values);
        }

        public Task<List<SLCorpActionDividendCollectionList>> GetSLCorpActionCashCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                CorActDeclarationID = CorActDeclarationID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SLCorpActionDividendCollectionList>("[CMListCorpActionCashCollectionApproval]", values);
        }

        public async Task<string> ILAMLCorpActionClaim(ILAMLCorpActionBonusFractionClaim objILAMLCorpActionClaim, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLInsertUpdateCorporateActionClaim";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@Status", objILAMLCorpActionClaim.Status);
                SpParameters.Add("@ApprovalRemark", objILAMLCorpActionClaim.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ILAMLCorpActionBonusClaimList", ListtoDataTableConverter.ToDataTable(objILAMLCorpActionClaim.ILAMLCorpActBonusFractionClaimList).AsTableValuedParameter("Type_ILAMLCorpActionBonusClaimList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //public Task<List<ILAMLCorpActionClaimListApprovalDTO>> GetILAMLClaimApprovalList(int CompanyID, int BranchID, string UserName)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        UserName = UserName

        //    };
        //    return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionClaimListApprovalDTO>("[ILAMLListCorpActBonusClaimApproval]", values);
        //}

        public async Task<string> GetSLCorpActDivCollectionApprove(SLCorporateActionCashCollectionApproveDTO objSLCorpActDivCollectionApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedCorpActDivCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objSLCorpActDivCollectionApprove.Status);
                SpParameters.Add("@ApprovalRemark", objSLCorpActDivCollectionApprove.ApprovalRemark);
                SpParameters.Add("@CorActDeclarationID", objSLCorpActDivCollectionApprove.CorpActDeclarationID);
                SpParameters.Add("@SLCorpActionDividendCollection", ListtoDataTableConverter.ToDataTable(objSLCorpActDivCollectionApprove.SLCorpActCashClaimApprove).AsTableValuedParameter("Type_SLCorpActionDividendCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CorporateActionDividendDTO PrevEquityInstrument = new CorporateActionDividendDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetSLUpdateNetAmountSLCorpActDivCollection(SLCorpActionDividendCollectionDTO objSLUpdateNetAmountSLCorpActDivCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMUpdateNetAmountCorpActDivCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@NetAmount", objSLUpdateNetAmountSLCorpActDivCollection.NetAmount);
                SpParameters.Add("@TaxAmount", objSLUpdateNetAmountSLCorpActDivCollection.TaxAmount);
                SpParameters.Add("@SLCorpActionDividendCollection", ListtoDataTableConverter.ToDataTable(objSLUpdateNetAmountSLCorpActDivCollection.SLCorpoActionDivdCollectionList).AsTableValuedParameter("Type_SLCorpActionDividendCollection"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<SLCorpActionBonusFractionCollectionListDTO>> GetSLCorpActionBonusFractionCollectionList(int CompanyID, int BranchID, int CorActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorActDeclarationID = CorActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<SLCorpActionBonusFractionCollectionListDTO>("[CMListCorpActBonusFrcCollection]", values);
        }

       

        public Task<List<ILAMLCorpActionCashClaimListApprovalDTO>> GetILAMLCashClaimApprovalList(int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionCashClaimListApprovalDTO>("[ILAMLCorpActionBonusCashClaimApprovalList]", values);
        }

        public async Task<string> InsertSLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertUpdateCorpActBonusFrcCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Status", entryCorpActBonusFractionCollection.Status);
                SpParameters.Add("@CorpActDeclarationID", entryCorpActBonusFractionCollection.CorpActDeclarationID);
                SpParameters.Add("@SLCorpActionBonusFractionClaim", ListtoDataTableConverter.ToDataTable(entryCorpActBonusFractionCollection.SLCorpActFractionClaimList).AsTableValuedParameter("Type_SLCorpActionBonusFractionClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<SLCorpActionBonusFractionCollectionApprovalListDTO>> GetSLCorpActionBonusFractionCollectionApprovalList(int CorActDeclarationID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                CorActDeclarationID = CorActDeclarationID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SLCorpActionBonusFractionCollectionApprovalListDTO>("[CMCorpActBonusFrcCollectionApproval]", values);
        }

        public async Task<string> GetSLUpdateNetAmountSLCorpActBonusFrcCollection(SLCorpActionDividendBonusFractionCollectionDTO objSLUpdateNetAmountSLCorpActBonusFrcCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMUpdateNetAmountCorpActBonusFrcCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@MarketPrice", objSLUpdateNetAmountSLCorpActBonusFrcCollection.MarketPrice);
                SpParameters.Add("@NetAmount", objSLUpdateNetAmountSLCorpActBonusFrcCollection.NetAmount);
                SpParameters.Add("@SLCorpActionBonusFractionCollection", ListtoDataTableConverter.ToDataTable(objSLUpdateNetAmountSLCorpActBonusFrcCollection.SLCorpoActionDivBonusFracCollectionList).AsTableValuedParameter("Type_CorpActionBonusFractionCollection"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetSLCorpActDivBonusFrcCollectionApprove(SLCorpActionDividendBonusFractionApproveDTO objSLCorpActDivBonusFrcCollectionApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMCorpActDivBonusFrcCollectionApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objSLCorpActDivBonusFrcCollectionApprove.Status);
                SpParameters.Add("@ApprovalRemark", objSLCorpActDivBonusFrcCollectionApprove.ApprovalRemark);
                SpParameters.Add("@CorpActDeclarationID", objSLCorpActDivBonusFrcCollectionApprove.CorpActDeclarationID);
                SpParameters.Add("@SLCorpActionBonusFractionCollection", ListtoDataTableConverter.ToDataTable(objSLCorpActDivBonusFrcCollectionApprove.SLCorpoActionDivBonusFracCollectionList).AsTableValuedParameter("Type_CorpActionBonusFractionCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CorporateActionDividendDTO PrevEquityInstrument = new CorporateActionDividendDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionBonusCollectionListDTO>> GetILAMLCorpActionBonusCollectionList(int CompanyID, int BranchID, int CorpActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                CorpActDeclarationID = CorpActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionBonusCollectionListDTO>("[ILAMLListCorpActBonusCollection]", values);
        }

        public async Task<string> InsertILAMLCorpActBonusCollection(ILAMLCorpActionBonusCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "ILAMLInsertUpdateCorActBonusCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Status", entryCorpActBonusCollection.Status);
                SpParameters.Add("@CorpActDeclarationID", entryCorpActBonusCollection.CorpActDeclarationID);
                SpParameters.Add("@ILAMLCorpActionBonusCollection", ListtoDataTableConverter.ToDataTable(entryCorpActBonusCollection.ILAMLCorpoActionDivBonusCollectionList).AsTableValuedParameter("Type_ILAMLCorpActionBonusCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ILAMLCorpActionBonusCollectionApprovalListDTO>> GetILAMLCorpActionBonusCollectionApprovalList(int CorpActDeclarationID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                CorpActDeclarationID = CorpActDeclarationID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<ILAMLCorpActionBonusCollectionApprovalListDTO>("[CMListCorpActionBonusCollectionApproval]", values);
        }

        public async Task<string> GetILAMLUpdateBonusCollectionApprove(ILAMLCorpActionBonusCollectionUpdateDTO objILAMLUpdateBonusCollectionApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "ILAMLUpdateCoprActBonusCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@BonusEntitlement", objILAMLUpdateBonusCollectionApprove.BonusEntitlement);
                SpParameters.Add("@ILAMLCorpActionBonusCollection", ListtoDataTableConverter.ToDataTable(objILAMLUpdateBonusCollectionApprove.ILAMLCorpoActionDivBonusCollectionUpList).AsTableValuedParameter("Type_ILAMLCorpActionBonusCollection"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetILAMLApprovedCorpActDivBonusCollection(ILAMLCorpActionDividendBonusCollectionApproveDTO objILAMLApprovedCorpActDivBonusCollection, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMCorpActBonusCollectionApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", objILAMLApprovedCorpActDivBonusCollection.Status);
                SpParameters.Add("@ApprovalRemark", objILAMLApprovedCorpActDivBonusCollection.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CorpActDeclarationID", objILAMLApprovedCorpActDivBonusCollection.CorpActDeclarationID);
                SpParameters.Add("@ILAMLCorpActionBonusCollection", ListtoDataTableConverter.ToDataTable(objILAMLApprovedCorpActDivBonusCollection.ILAMLCorpoActionDivBonusCollectionAList).AsTableValuedParameter("Type_ILAMLCorpActionBonusCollection"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<SLCorporateActionCashCollectionListDTO>> GetSLCorpActCashCollectionResult(int ProductID, int CorActDeclarationID, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                ProductID = ProductID,
                CorActDeclarationID = CorActDeclarationID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SLCorporateActionCashCollectionListDTO>("[SLListCorpActionCashCollection]", values);
        }
        public async Task<string> InsertSLCorpActBonusCollection(SLCorporateActionCashCollectionInsertDTO entryCorpActBonusCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "SLInsertUpdateCorpActDivCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CollectionStatus", entryCorpActBonusCollection.Status);
                SpParameters.Add("@ApprovalRemarks", entryCorpActBonusCollection.ApprovalRemark);

                SpParameters.Add("@SLCorpActionDividendClaim", ListtoDataTableConverter.ToDataTable(entryCorpActBonusCollection.SLCorpActCashClaimList).AsTableValuedParameter("Type_SLCorpActionDividendClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<SLCorpActionBonusFractionCollectionListDTO>> GetOnlySLCorpActionBonusFractionCollectionList(int ProductID, int CompanyID, int BranchID, int CorActDeclarationID, string Maker)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                ProductID = ProductID,
                CorActDeclarationID = CorActDeclarationID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<SLCorpActionBonusFractionCollectionListDTO>("[SLListCorpActBonusFrcCollection]", values);
        }



       

        public async Task<string> InsertOnlySLCorpActBonusFractionCollection(SLCorporateActionFractionCollectionInsertDTO entryCorpActBonusFractionCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "SLInsertUpdateCorpActBonusFrcCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CollectionStatus", entryCorpActBonusFractionCollection.Status);
                SpParameters.Add("@ApprovalRemarks", entryCorpActBonusFractionCollection.ApprovalRemark);
                SpParameters.Add("@SLCorpActionBonusFractionClaim", ListtoDataTableConverter.ToDataTable(entryCorpActBonusFractionCollection.SLCorpActFractionClaimList).AsTableValuedParameter("Type_SLCorpActionBonusFractionClaim"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }
}
