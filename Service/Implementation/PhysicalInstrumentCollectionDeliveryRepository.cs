using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.Demat;
using Model.DTOs.LockUnlock;
using Model.DTOs.PhysicalInstrumentCollectionDelivery;
using Model.DTOs.SecurityElimination;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class PhysicalInstrumentCollectionDeliveryRepository: IPhysicalInstrumentCollectionDeliveryRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public PhysicalInstrumentCollectionDeliveryRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }
        public async Task<string> InsertCMPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryDTO entryPhysIntrumentCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdatePhyInstCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@PhyInstCollID", entryPhysIntrumentCollection.PhyInstCollID);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CollectionDate", entryPhysIntrumentCollection.CollectionDate);
                SpParameters.Add("@ContractID", entryPhysIntrumentCollection.ContractID);
                SpParameters.Add("@InstrumentID", entryPhysIntrumentCollection.InstrumentID);
                SpParameters.Add("@ReferenceNo", entryPhysIntrumentCollection.ReferenceNo);
                SpParameters.Add("@Quantity", entryPhysIntrumentCollection.Quantity);
                SpParameters.Add("@AverageRate", entryPhysIntrumentCollection.AverageRate);
                SpParameters.Add("@CertificateNo", entryPhysIntrumentCollection.CertificateNo);
                SpParameters.Add("@DestinationNoFrom", entryPhysIntrumentCollection.DestinationNoFrom);
                SpParameters.Add("@DestinationNoTo", entryPhysIntrumentCollection.DestinationNoTo);
                SpParameters.Add("@TotalCertificateNo", entryPhysIntrumentCollection.TotalCertificateNo);
                SpParameters.Add("@FolioNo", entryPhysIntrumentCollection.FolioNo);
                SpParameters.Add("@Remarks", entryPhysIntrumentCollection.Remarks);

                //SpParameters.Add("@CMPhysicalInstrumentCollection", ListtoDataTableConverter.ToDataTable(entryPhysIntrumentCollection.CMPhysicalInstrumentCollectionDeliveryList).AsTableValuedParameter("Type_CMPhysicalInstrumentCollection"));
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

        public Task<List<PhysicalInstrumentCollectionDeliveryDTO>> CMPhysicalInstrumentCollectionList(string Status,int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<PhysicalInstrumentCollectionDeliveryDTO>("[CM_ListPhysicalInstrumentCollection]", values);
        }

        public PhysicalInstrumentCollectionDeliveryDTO CMPhysicalInstrumentCollectionListbyID(int PhyInstCollID, int CompanyID, int BranchID)
        {
            PhysicalInstrumentCollectionDeliveryDTO Instrument = new PhysicalInstrumentCollectionDeliveryDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@PhyInstCollID", PhyInstCollID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryPhysicalInstrumentCollection]", sqlParams);

            Instrument = CustomConvert.DataSetToList<PhysicalInstrumentCollectionDeliveryDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<string> GetCMApprovedPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryApprove objCMApprovedPhysicalInstrumentCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_PhysicalInstrumentCollectionApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@ApprovalStatus", objCMApprovedPhysicalInstrumentCollection.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedPhysicalInstrumentCollection.ApprovalRemark);
                SpParameters.Add("@CMPhysicalInstrumentCollection", ListtoDataTableConverter.ToDataTable(objCMApprovedPhysicalInstrumentCollection.CMPhysicalInstrumentCollectionDeliveryApproveList).AsTableValuedParameter("Type_CMPhysicalInstrumentCollection"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                PhysicalInstrumentCollectionDeliveryDTO PrevEquityInstrument = new PhysicalInstrumentCollectionDeliveryDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> InsertCMPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryDTO entryPhysIntrumentDelivery, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdatePhyInstDelivery";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@PhyInstDelID", entryPhysIntrumentDelivery.PhyInstDelID);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@DeliveryDate", entryPhysIntrumentDelivery.DeliveryDate);
                SpParameters.Add("@ContractID", entryPhysIntrumentDelivery.ContractID);
                SpParameters.Add("@InstrumentID", entryPhysIntrumentDelivery.InstrumentID);
                SpParameters.Add("@ReferenceNo", entryPhysIntrumentDelivery.ReferenceNo);
                SpParameters.Add("@Quantity", entryPhysIntrumentDelivery.Quantity);
                SpParameters.Add("@AverageRate", entryPhysIntrumentDelivery.AverageRate);
                SpParameters.Add("@CertificateNo", entryPhysIntrumentDelivery.CertificateNo);
                SpParameters.Add("@DestinationNoFrom", entryPhysIntrumentDelivery.DestinationNoFrom);
                SpParameters.Add("@DestinationNoTo", entryPhysIntrumentDelivery.DestinationNoTo);
                SpParameters.Add("@TotalCertificateNo", entryPhysIntrumentDelivery.TotalCertificateNo);
                SpParameters.Add("@FolioNo", entryPhysIntrumentDelivery.FolioNo);
                SpParameters.Add("@Remarks", entryPhysIntrumentDelivery.Remarks);

                //SpParameters.Add("@CMPhysicalInstrumentDelivery", ListtoDataTableConverter.ToDataTable(entryPhysIntrumentDelivery.CMPhysicalInstrumentDeliveryList).AsTableValuedParameter("Type_CMPhysicalInstrumentDelivery"));
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

        public Task<List<PhysicalInstrumentDeliveryDTO>> CMPhysicalInstrumentDeliveryList(string Status,int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<PhysicalInstrumentDeliveryDTO>("[CM_ListPhysicalInstrumentDelivery]", values);
        }

        public PhysicalInstrumentDeliveryDTO CMPhysicalInstrumentDeliveryListbyID(int PhyInstDelID, int CompanyID, int BranchID)
        {
            PhysicalInstrumentDeliveryDTO Instrument = new PhysicalInstrumentDeliveryDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@PhyInstDelID", PhyInstDelID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryPhysicalInstrumentDelivery]", sqlParams);

            Instrument = CustomConvert.DataSetToList<PhysicalInstrumentDeliveryDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<string> GetCMApprovedPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryApprove objCMApprovedPhysicalInstrumentDelivery, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_PhysicalInstrumentDeliveryApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@ApprovalStatus", objCMApprovedPhysicalInstrumentDelivery.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedPhysicalInstrumentDelivery.ApprovalRemark);
                SpParameters.Add("@CMPhysicalInstrumentDelivery", ListtoDataTableConverter.ToDataTable(objCMApprovedPhysicalInstrumentDelivery.CMPhysicalInstrumentDeliveryApproveList).AsTableValuedParameter("Type_CMPhysicalInstrumentDelivery"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                PhysicalInstrumentCollectionDeliveryDTO PrevEquityInstrument = new PhysicalInstrumentCollectionDeliveryDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //public async Task<string> GetUnitFundCollection(UnitFundCollectionDTO objUnitFundCollection, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "CM_InsertUpdateUnitFundCollection";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@UnitFundCollID", objUnitFundCollection.UnitFundCollID);
        //        SpParameters.Add("@InstrumentLedgerID", objUnitFundCollection.InstrumentLedgerID);
        //        SpParameters.Add("@ContractID ", objUnitFundCollection.ContractID);
        //        SpParameters.Add("@InstrumentID ", objUnitFundCollection.InstrumentID);
        //        SpParameters.Add("@Quantity ", objUnitFundCollection.Quantity);
        //        SpParameters.Add("@Rate ", objUnitFundCollection.Rate);
        //        SpParameters.Add("@Remarks ", objUnitFundCollection.Remarks);
        //        SpParameters.Add("@Maker", Maker);
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

        //public Task<List<UnitFundCollectionListDTO>> GetUnitFundCollectionList(int CompanyID, int BranchID, string Maker)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        Maker = Maker
        //    };
        //    return _dbCommonOperation.ReadSingleTable<UnitFundCollectionListDTO>("[CM_ListUnitFundCollection]", values);
        //}


        //public async Task<string> GetApprovedUnitFundCollection(UnitFundCollectionApprove objApprovedUnitFundCollection, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {

        //        string sp = "CM_UnitFundCollectionApproval";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@Maker", Maker); 
        //        //SpParameters.Add("@UnitFundCollID", objApprovedUnitFundCollection.UnitFundCollID);
        //        SpParameters.Add("@ApprovalStatus", objApprovedUnitFundCollection.Status);
        //        SpParameters.Add("@ApprovalRemark", objApprovedUnitFundCollection.ApprovalRemark);
        //        SpParameters.Add("@CMUnitFundCollection", ListtoDataTableConverter.ToDataTable(objApprovedUnitFundCollection.CMUnitFundCollectionApproveList).AsTableValuedParameter("Type_CMUnitFundCollection"));
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        UnitFundCollectionListDTO PrevEquityInstrument = new UnitFundCollectionListDTO();

        //        result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        //public async Task<string> GetUnitFundDelivery(UnitFundDeliveryDTO objUnitFundDelivery, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {


        //        string sp = "CM_InsertUpdateUnitFundDelivery";

        //        DynamicParameters SpParameters = new DynamicParameters();


        //        SpParameters.Add("@UnitFundDelID", objUnitFundDelivery.UnitFundDelID);
        //        SpParameters.Add("@InstrumentLedgerID", objUnitFundDelivery.InstrumentLedgerID);
        //        SpParameters.Add("@ContractID ", objUnitFundDelivery.ContractID);
        //        SpParameters.Add("@InstrumentID ", objUnitFundDelivery.InstrumentID);
        //        SpParameters.Add("@Quantity ", objUnitFundDelivery.Quantity);
        //        SpParameters.Add("@Rate ", objUnitFundDelivery.Rate);
        //        SpParameters.Add("@Remarks ", objUnitFundDelivery.Remarks);
        //        SpParameters.Add("@Maker", Maker);
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



        //public Task<List<UnitFundDeliveryListDTO>> GetUnitFundDeliveryList(int CompanyID, int BranchID, string Maker)
        //{
        //    var values = new
        //    {
        //        CompanyID = CompanyID,
        //        BranchID = BranchID,
        //        Maker = Maker
        //    };
        //    return _dbCommonOperation.ReadSingleTable<UnitFundDeliveryListDTO>("[CM_ListUnitFundDelivery]", values);
        //}

        //public UnitFundDeliveryDTObyID GetCMUnitFundDeliveryListByID(int UnitFundDelID, int CompanyID, int BranchID)
        //{
        //    UnitFundDeliveryDTObyID Instrument = new UnitFundDeliveryDTObyID();

        //    SqlParameter[] sqlParams = new SqlParameter[]
        //   {
        //        new SqlParameter("@UnitFundDelID", UnitFundDelID),
        //        new SqlParameter("@CompanyID", CompanyID),
        //        new SqlParameter("@BranchID", BranchID)
        //   };
        //    var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryUnitFundDelivery]", sqlParams);

        //    Instrument = CustomConvert.DataSetToList<UnitFundDeliveryDTObyID>(DataSets.Tables[0]).First();

        //    return Instrument;
        //}

        //public async Task<string> GetApprovedUnitFundDelivery(UnitFundDeliveryApprove objApprovedUnitFundDelivery, int CompanyID, int BranchID, string Maker)
        //{
        //    string result = string.Empty;

        //    try
        //    {

        //        string sp = "CM_UnitFundDeliveryApproval";

        //        DynamicParameters SpParameters = new DynamicParameters();

        //        SpParameters.Add("@CompanyID", CompanyID);
        //        SpParameters.Add("@BranchID", BranchID);
        //        SpParameters.Add("@Maker", Maker);
        //        //SpParameters.Add("@UnitFundCollID", objApprovedUnitFundCollection.UnitFundCollID);
        //        SpParameters.Add("@ApprovalStatus", objApprovedUnitFundDelivery.Status);
        //        SpParameters.Add("@ApprovalRemark", objApprovedUnitFundDelivery.ApprovalRemark);
        //        SpParameters.Add("@CMUnitFundDelivery", ListtoDataTableConverter.ToDataTable(objApprovedUnitFundDelivery.CMUnitFundDeliveryApproveList).AsTableValuedParameter("Type_CMUnitFundDelivery"));
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
        //        UnitFundCollectionListDTO PrevEquityInstrument = new UnitFundCollectionListDTO();

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
