using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.UnitFundCollectionDelivery;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class UnitFundCollectionDeliveryRepository : IUnitFundCollectionDeliveryRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public UnitFundCollectionDeliveryRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> GetUnitFundCollection(UnitFundCollectionDTO objUnitFundCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_InsertUpdateUnitFundCollection";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@UnitFundCollID", objUnitFundCollection.UnitFundCollID);
                SpParameters.Add("@InstrumentLedgerID", objUnitFundCollection.InstrumentLedgerID);
                SpParameters.Add("@ContractID ", objUnitFundCollection.ContractID);
                SpParameters.Add("@InstrumentID ", objUnitFundCollection.InstrumentID);
                SpParameters.Add("@Quantity ", objUnitFundCollection.Quantity);
                SpParameters.Add("@Rate ", objUnitFundCollection.Rate);
                SpParameters.Add("@Remarks ", objUnitFundCollection.Remarks);
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

        public Task<List<UnitFundCollectionListDTO>> GetUnitFundCollectionList(string Status,int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<UnitFundCollectionListDTO>("[CM_ListUnitFundCollection]", values);
        }

        public UnitFundCollectionDTObyID CMUnitFundCollectionListByID(int UnitFundCollID, int CompanyID, int BranchID)
        {
            UnitFundCollectionDTObyID Instrument = new UnitFundCollectionDTObyID();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UnitFundCollID", UnitFundCollID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryUnitFundCollection]", sqlParams);

            Instrument = CustomConvert.DataSetToList<UnitFundCollectionDTObyID>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<string> GetApprovedUnitFundCollection(UnitFundCollectionApprove objApprovedUnitFundCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_UnitFundCollectionApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                //SpParameters.Add("@UnitFundCollID", objApprovedUnitFundCollection.UnitFundCollID);
                SpParameters.Add("@ApprovalStatus", objApprovedUnitFundCollection.Status);
                SpParameters.Add("@ApprovalRemark", objApprovedUnitFundCollection.ApprovalRemark);
                SpParameters.Add("@CMUnitFundCollection", ListtoDataTableConverter.ToDataTable(objApprovedUnitFundCollection.CMUnitFundCollectionApproveList).AsTableValuedParameter("Type_CMUnitFundCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                UnitFundCollectionListDTO PrevEquityInstrument = new UnitFundCollectionListDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetUnitFundDelivery(UnitFundDeliveryDTO objUnitFundDelivery, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_InsertUpdateUnitFundDelivery";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@UnitFundDelID", objUnitFundDelivery.UnitFundDelID);
                SpParameters.Add("@InstrumentLedgerID", objUnitFundDelivery.InstrumentLedgerID);
                SpParameters.Add("@ContractID ", objUnitFundDelivery.ContractID);
                SpParameters.Add("@InstrumentID ", objUnitFundDelivery.InstrumentID);
                SpParameters.Add("@Quantity ", objUnitFundDelivery.Quantity);
                SpParameters.Add("@Rate ", objUnitFundDelivery.Rate);
                SpParameters.Add("@Remarks ", objUnitFundDelivery.Remarks);
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



        public Task<List<UnitFundDeliveryListDTO>> GetUnitFundDeliveryList(string Status,int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<UnitFundDeliveryListDTO>("[CM_ListUnitFundDelivery]", values);
        }

        public UnitFundDeliveryDTObyID GetCMUnitFundDeliveryListByID(int UnitFundDelID, int CompanyID, int BranchID)
        {
            UnitFundDeliveryDTObyID Instrument = new UnitFundDeliveryDTObyID();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@UnitFundDelID", UnitFundDelID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryUnitFundDelivery]", sqlParams);

            Instrument = CustomConvert.DataSetToList<UnitFundDeliveryDTObyID>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<string> GetApprovedUnitFundDelivery(UnitFundDeliveryApprove objApprovedUnitFundDelivery, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_UnitFundDeliveryApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                //SpParameters.Add("@UnitFundCollID", objApprovedUnitFundCollection.UnitFundCollID);
                SpParameters.Add("@ApprovalStatus", objApprovedUnitFundDelivery.Status);
                SpParameters.Add("@ApprovalRemark", objApprovedUnitFundDelivery.ApprovalRemark);
                SpParameters.Add("@CMUnitFundDelivery", ListtoDataTableConverter.ToDataTable(objApprovedUnitFundDelivery.CMUnitFundDeliveryApproveList).AsTableValuedParameter("Type_CMUnitFundDelivery"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                UnitFundCollectionListDTO PrevEquityInstrument = new UnitFundCollectionListDTO();

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
