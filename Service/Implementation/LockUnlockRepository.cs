using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs.Demat;
using Model.DTOs.IPO;
using Model.DTOs.LockUnlock;
using Model.DTOs.Remat;
using Model.DTOs.SecurityElimination;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class LockUnlockRepository : ILockUnlockRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        public LockUnlockRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }
        public Task<List<LockUnlockInstrumentListDTO>> GetLockUnlockInstrumentList( int CompanyID, int BranchID, int ContractID, string LockUnlockStatus, string Maker)
        {
            var values = new
            {
                
                CompanyID = CompanyID,
                BranchID = BranchID,
                ContractID= ContractID,
                LockUnlockStatus=LockUnlockStatus,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<LockUnlockInstrumentListDTO>("[CMListLockUnlockInstrument]", values);
        }
        public async Task<string> PostLockingInstruemnt(LockInstrumentListDTO objLockInstrumentListDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {
                string sp = "CM_InsertUpdateLockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@LockingId", objLockInstrumentListDTO.LockingId);
                SpParameters.Add("@ContractID ", objLockInstrumentListDTO.ContractID);
                SpParameters.Add("@InstrumentID ", objLockInstrumentListDTO.InstrumentID);
                SpParameters.Add("@Quantity ", objLockInstrumentListDTO.Quantity);
                SpParameters.Add("@EffectiveDate ", objLockInstrumentListDTO.EffectiveDate);
                SpParameters.Add("@LockingType ", objLockInstrumentListDTO.LockingType);
                SpParameters.Add("@Remarks ", objLockInstrumentListDTO.Remarks);
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
        public Task<List<LockInstrumentListDTO>> GetLockingApprovalList( string LockInstrumentStatus, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                LockInstrumentStatus = LockInstrumentStatus,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<LockInstrumentListDTO>("[CM_ListLockingApproval]", values);
        }
        public async Task<string> PostCMUpdateLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateLockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Quantity", objCMLockInstrumentDTO.Quantity);
                SpParameters.Add("@CMLockingInstrument", ListtoDataTableConverter.ToDataTable(objCMLockInstrumentDTO.CMLockInstrumentListDTO).AsTableValuedParameter("Type_CMLockingInstrument"));
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
        public async Task<string> PostCMApprovedLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedLockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objCMLockInstrumentDTO.Status);
                SpParameters.Add("@ApprovalRemark", objCMLockInstrumentDTO.ApprovalRemark);
                SpParameters.Add("@CMLockingInstrument", ListtoDataTableConverter.ToDataTable(objCMLockInstrumentDTO.CMLockInstrumentListDTO).AsTableValuedParameter("Type_CMLockingInstrument"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                DematDTO PrevEquityInstrument = new DematDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> PostUnlockingInstruemnt(UnlockInstrumentListDTO objUnlockInstrumentListDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {
                string sp = "CM_InsertUpdateUnlockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UnLockingId", objUnlockInstrumentListDTO.UnLockingId);
                SpParameters.Add("@ContractID ", objUnlockInstrumentListDTO.ContractID);
                SpParameters.Add("@InstrumentID ", objUnlockInstrumentListDTO.InstrumentID);
                SpParameters.Add("@Quantity ", objUnlockInstrumentListDTO.Quantity);
                SpParameters.Add("@EffectiveDate ", objUnlockInstrumentListDTO.EffectiveDate);
                SpParameters.Add("@UnLockingType ", objUnlockInstrumentListDTO.UnLockingType);
                SpParameters.Add("@Remarks ", objUnlockInstrumentListDTO.Remarks);
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

        public Task<List<UnlockInstrumentListDTO>> GetUnlockingApprovalList(string UnlockInstrumentStatus, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                UnlockInstrumentStatus = UnlockInstrumentStatus,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<UnlockInstrumentListDTO>("[CM_ListUnLockingApproval]", values);
        }

        public async Task<string> PostCMUpdateUnlockingQuantity(CMUnLockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateUnLockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Quantity", objCMUnlockInstrumentDTO.Quantity);
                SpParameters.Add("@CMUnlockingInstrument", ListtoDataTableConverter.ToDataTable(objCMUnlockInstrumentDTO.CMUnlockInstrumentListDTO).AsTableValuedParameter("Type_CMUnlockingInstrument"));
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

        public async Task<string> PostCMApprovedUnlockingQuantity(CMUnlockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedUnLockingInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objCMUnlockInstrumentDTO.Status);
                SpParameters.Add("@ApprovalRemark", objCMUnlockInstrumentDTO.ApprovalRemark);
                SpParameters.Add("@CMUnlockingInstrument", ListtoDataTableConverter.ToDataTable(objCMUnlockInstrumentDTO.CMUnlockInstrumentListDTO).AsTableValuedParameter("Type_CMUnlockingInstrument"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                LockInstrumentListDTO PrevEquityInstrument = new LockInstrumentListDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public LockInstrumentListDTO CMLockingInstrumentListbyID(int LockingId, int CompanyID, int BranchID)
        {
            LockInstrumentListDTO Instrument = new LockInstrumentListDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@LockingId", LockingId),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_LockingInstrumentbyID]", sqlParams);

            Instrument = CustomConvert.DataSetToList<LockInstrumentListDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public UnlockInstrumentListDTO CMUnlockingInstrumentListbyID(int UnLockingId, int CompanyID, int BranchID)
        {
            UnlockInstrumentListDTO Instrument = new UnlockInstrumentListDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UnLockingId", UnLockingId),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_UnLockingInstrumentbyID]", sqlParams);

            Instrument = CustomConvert.DataSetToList<UnlockInstrumentListDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        //public async Task<string> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "CMInsertUpdateInstrumentConversionDeclearation";

        //        DynamicParameters SpParameters = new DynamicParameters();
        //        SpParameters.Add("@InstConversionID", EntryICDDTO.InstConversionID);
        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@BaseInstID", EntryICDDTO.BaseInstID);
        //        SpParameters.Add("@ConvertedInstID", EntryICDDTO.ConvertedInstID);
        //        SpParameters.Add("@BaseRatio", EntryICDDTO.BaseRatio);
        //        SpParameters.Add("@ConvertedRatio", EntryICDDTO.ConvertedRatio);
        //        SpParameters.Add("@RecordDate", EntryICDDTO.RecordDate);
        //        SpParameters.Add("@IsContinue", EntryICDDTO.IsContinue);
        //        SpParameters.Add("@Maker", Maker);
        //        SpParameters.Add("@Remarks", EntryICDDTO.Remarks);



        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        LockUnlockInstrumentListDTO PrevEquityInstrument = new LockUnlockInstrumentListDTO();

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public InstrumentConversionDeclarationDTO CMInstrumentConversionListbyID(int InstConversionID, int CompanyID, int BranchID)
        //{
        //    InstrumentConversionDeclarationDTO Instrument = new InstrumentConversionDeclarationDTO();

        //    SqlParameter[] sqlParams = new SqlParameter[]
        //   {
        //        new SqlParameter("@InstConversionID", InstConversionID),
        //        new SqlParameter("@CompanyID", CompanyID),
        //        new SqlParameter("@BranchID", BranchID)
        //   };
        //    var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryInstrumentConversionDeclaration]", sqlParams);

        //    Instrument = CustomConvert.DataSetToList<InstrumentConversionDeclarationDTO>(DataSets.Tables[0]).First();

        //    return Instrument;
        //}

        //public Task<List<InstrumentConversionDeclarationListDTO>> GetInstConversionDeclarationList(int CompanyID, int BranchID)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID
        //    };
        //    return _dbCommonOperation.ReadSingleTable<InstrumentConversionDeclarationListDTO>("[CM_ListInstrumentConversionDeclearation]", values);
        //}

        //public async Task<String> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID, string UserName)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "CM_ApprovedInstrumentConversionDeclearation";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@InstConversionID", InsConvDeclarationList.InstConversionID);
        //        SpParameters.Add("@ApprovalStatus", InsConvDeclarationList.Status);
        //        SpParameters.Add("@ApprovalRemark", InsConvDeclarationList.ApprovalRemark);
        //        SpParameters.Add("@UserName", UserName);

        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

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
