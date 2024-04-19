using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.Demat;
using Model.DTOs.InstrumentConversion;
using Model.DTOs.LockUnlock;
using Model.DTOs.SecurityElimination;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class InstrumentConversionRepository : IInstrumentConversionRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public InstrumentConversionRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMInsertUpdateInstrumentConversionDeclearation";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@InstConversionID", EntryICDDTO.InstConversionID);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@BaseInstID", EntryICDDTO.BaseInstID);
                SpParameters.Add("@ConvertedInstID", EntryICDDTO.ConvertedInstID);
                SpParameters.Add("@BaseRatio", EntryICDDTO.BaseRatio);
                SpParameters.Add("@ConvertedRatio", EntryICDDTO.ConvertedRatio);
                SpParameters.Add("@RecordDate", EntryICDDTO.RecordDate);
                SpParameters.Add("@IsContinue", EntryICDDTO.IsContinue);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Remarks", EntryICDDTO.Remarks);



                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                InstrumentConversionDeclarationDTO PrevEquityInstrument = new InstrumentConversionDeclarationDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public InstrumentConversionDeclarationDTO CMInstrumentConversionListbyID(int InstConversionID, int CompanyID, int BranchID)
        {
            InstrumentConversionDeclarationDTO Instrument = new InstrumentConversionDeclarationDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstConversionID", InstConversionID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryInstrumentConversionDeclaration]", sqlParams);

            Instrument = CustomConvert.DataSetToList<InstrumentConversionDeclarationDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public Task<List<InstrumentConversionDeclarationListDTO>> GetInstConversionDeclarationList(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<InstrumentConversionDeclarationListDTO>("[CM_ListInstrumentConversionDeclearation]", values);
        }

        public async Task<String> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_ApprovedInstrumentConversionDeclearation";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@InstConversionID", InsConvDeclarationList.InstConversionID);
                SpParameters.Add("@ApprovalStatus", InsConvDeclarationList.Status);
                SpParameters.Add("@ApprovalRemark", InsConvDeclarationList.ApprovalRemark);
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

        public Task<List<CMInstrumentConversionDTO>> CMInstrumentConversionList(int InstConversionID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstConversionID = InstConversionID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<CMInstrumentConversionDTO>("[CM_ListInstrumentConversion]", values);
        }

        public async Task<string> InsertCMInstrumentConversion(CMInstrumentConversionInsert entryIntrumentConversion, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdateInstrumentConversion";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstConversionID", entryIntrumentConversion.InstConversionID);
                SpParameters.Add("@CMInstrumentConversion", ListtoDataTableConverter.ToDataTable(entryIntrumentConversion.InstrumentConversionList).AsTableValuedParameter("Type_CMInstrumentConversion"));
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

        public Task<List<CMInstrumentConversion>> CMInstrumentConversionApprovalList(int InstConversionID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstConversionID = InstConversionID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<CMInstrumentConversion>("[CM_ListInstrumentConversionApproval]", values);
        }

        public async Task<string> GetCMApprovednstrumentConversion(CMInstrumentConversionApprove CMInstrumentConversionApprove, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InstrumentConversionApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstConversionID", CMInstrumentConversionApprove.InstConversionID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", CMInstrumentConversionApprove.Status);
                SpParameters.Add("@ApprovalRemark", CMInstrumentConversionApprove.ApprovalRemark);
                SpParameters.Add("@CMInstrumentConversion", ListtoDataTableConverter.ToDataTable(CMInstrumentConversionApprove.InstrumentConversionApprovalList).AsTableValuedParameter("Type_CMInstrumentConversion"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMInstrumentConversionApprove PrevEquityInstrument = new CMInstrumentConversionApprove();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetInstrumentConversionTotalCost(CMInstrumentConversionUpdateBaseQuantityDTO objInsConversionUpdate, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateInstrumentConversionBaseQuantity";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@BaseQuantity", objInsConversionUpdate.BaseQuantity);
                SpParameters.Add("@CMInstrumentConversion", ListtoDataTableConverter.ToDataTable(objInsConversionUpdate.InstrumentConversionBQUpdate).AsTableValuedParameter("Type_CMInstrumentConversion"));
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

        public async Task<string> InsSplitDeclaration(InstrumentSplitDeclarationDTO EntryISDDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMInsertUpdateInstrumentSplitedDeclearation";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@InstSplitedID", EntryISDDTO.InstSplitedID);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstrumentID", EntryISDDTO.InstrumentID);
                SpParameters.Add("@BaseRatio", EntryISDDTO.BaseRatio);
                SpParameters.Add("@SplitedRatio", EntryISDDTO.SplitedRatio);
                SpParameters.Add("@RecordDate", EntryISDDTO.RecordDate);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Remarks", EntryISDDTO.Remarks);



                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                InstrumentSplitDeclarationDTO PrevEquityInstrument = new InstrumentSplitDeclarationDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public InstrumentSplitDeclarationDTO GetCMInstrumentSplitListbyID(int InstSplitedID, int CompanyID, int BranchID)
        {
            InstrumentSplitDeclarationDTO Instrument = new InstrumentSplitDeclarationDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstSplitedID", InstSplitedID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryInstrumentSplitedDeclaration]", sqlParams);

            Instrument = CustomConvert.DataSetToList<InstrumentSplitDeclarationDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public Task<List<InstrumentSplitDeclarationDTO>> GetInstSplitDeclarationList(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<InstrumentSplitDeclarationDTO>("[CM_ListInstrumentSplitedDeclearation]", values);
        }

        public async Task<String> InstrumentSplitDeclarationApproved(InstrumentSplitDeclarationApprove InsSplitDeclarationList, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_ApprovedInstrumentSplittedDeclearation";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@InstSplitedID", InsSplitDeclarationList.InstSplitedID);
                SpParameters.Add("@ApprovalStatus", InsSplitDeclarationList.Status);
                SpParameters.Add("@ApprovalRemark", InsSplitDeclarationList.ApprovalRemark);
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

        public Task<List<CMInstrumentSplitDTO>> CMInstrumentSplitList(int InstSplitedID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstSplitedID = InstSplitedID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<CMInstrumentSplitDTO>("[CM_ListInstrumentSplitted]", values);
        }

        public async Task<string> InsertCMInstrumentSplit(CMInstrumentSplitInsert entryIntrumentSplit, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdateInstrumentSplited";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstSplitedID", entryIntrumentSplit.InstSplitedID);
                SpParameters.Add("@CMInstrumentSplited", ListtoDataTableConverter.ToDataTable(entryIntrumentSplit.InstrumentSplitList).AsTableValuedParameter("Type_CMInstrumentSplited"));
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

        public Task<List<CMInstrumentSplit>> CMInstrumentSplitApprovalList(int InstSplitedID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstSplitedID = InstSplitedID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<CMInstrumentSplit>("[CM_ListInstrumentSplittedApproval]", values);
        }

        public async Task<string> GetInstrumentSplittedQuantity(CMInstrumentSplitUpdateSpittedQuantityDTO objInsSplitUpdate, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateInstrumentSplittedQuantity";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@SplitedQuantity", objInsSplitUpdate.SplitedQuantity);
                SpParameters.Add("@CMInstrumentSplited", ListtoDataTableConverter.ToDataTable(objInsSplitUpdate.InstrumentSplittedQuantityUpdate).AsTableValuedParameter("Type_CMInstrumentSplited"));
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

        public async Task<string> GetCMApprovednstrumentSplit(CMInstrumentSplitApprove objCMApprovedInstrumentSplit, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InstrumentSplittedApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstSplitedID", objCMApprovedInstrumentSplit.InstSplitedID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objCMApprovedInstrumentSplit.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedInstrumentSplit.ApprovalRemark);
                SpParameters.Add("@CMInstrumentSplited", ListtoDataTableConverter.ToDataTable(objCMApprovedInstrumentSplit.InstrumentSplitApprovalList).AsTableValuedParameter("Type_CMInstrumentSplited"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMInstrumentSplitApprove PrevEquityInstrument = new CMInstrumentSplitApprove();

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
