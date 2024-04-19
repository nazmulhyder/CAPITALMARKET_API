using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs.Demat;
using Model.DTOs.Remat;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class RematRepository : IRematRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        public RematRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> GetRematCollection(RematDTO objRematCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "[CM_InsertUpdateRematInstrument]";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@RematInstrumentID", objRematCollection.RematInstrumentID);
                SpParameters.Add("@InstrumentLedgerID", objRematCollection.InstrumentLedgerID);
                SpParameters.Add("@ContractID ", objRematCollection.ContractID);
                SpParameters.Add("@InstrumentID ", objRematCollection.InstrumentID);
                SpParameters.Add("@Quantity ", objRematCollection.Quantity);
                SpParameters.Add("@Rate ", objRematCollection.Rate);
                SpParameters.Add("@Remarks ", objRematCollection.Remarks);
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

        public Task<List<RematListDTO>> GetRematInstrumentList(string TransactionDateFrom, string TransactionDateTo,string Status, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                TransactionDateFrom = TransactionDateFrom,
                TransactionDateTo = TransactionDateTo,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<RematListDTO>("[CM_ListRematCollection]", values);
        }

        public async Task<string> GetUpdateRematInstrument(CMRematInstrumentUpdateMaster objUpdateRematInstrument, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateRematInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AveragePrice", objUpdateRematInstrument.AveragePrice);
                SpParameters.Add("@Quantity", objUpdateRematInstrument.Quantity);
                SpParameters.Add("@CMRematInstrument", ListtoDataTableConverter.ToDataTable(objUpdateRematInstrument.CMRematInstrumentUpdateList).AsTableValuedParameter("Type_CMRematInstrument"));
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

        public async Task<string> GetCMApprovedRematInstrument(CMApprovedRematInstrumentDTO objCMApprovedRematInstrument, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedRematInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objCMApprovedRematInstrument.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedRematInstrument.ApprovalRemark);
                SpParameters.Add("@CMRematInstrument", ListtoDataTableConverter.ToDataTable(objCMApprovedRematInstrument.CMRematInstrumentApproveList).AsTableValuedParameter("Type_CMRematInstrument"));
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
    }
}
