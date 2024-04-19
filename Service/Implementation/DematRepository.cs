using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs;
using Model.DTOs.Demat;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
   public class DematRepository : IDematRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        public DematRepository( IDBCommonOpService dbCommonOperation, IConfiguration configuration, IUpdateLogRepository logOperation,IGlobalSettingService globalSettingService )
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> GetDematCollection(DematDTO objDematCollection,int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_InsertUpdateDematCollection";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@DematInstrumentID", objDematCollection.DematInstrumentID);
                SpParameters.Add("@InstrumentLedgerID", objDematCollection.InstrumentLedgerID);
                SpParameters.Add("@ContractID ", objDematCollection.ContractID);
                SpParameters.Add("@InstrumentID ", objDematCollection.InstrumentID);
                SpParameters.Add("@Quantity ", objDematCollection.Quantity);
                SpParameters.Add("@Rate ", objDematCollection.Rate);
                SpParameters.Add("@Remarks ", objDematCollection.Remarks);
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

        public Task<List<DematListDTO>> GetDematCollectionList(string TransactionDateFrom, string TransactionDateTo,string Status, int CompanyID, int BranchID, string Maker)
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
            return _dbCommonOperation.ReadSingleTable<DematListDTO>("[CM_ListDematCollection]", values);
        }

        public async Task<string> GetCMUpdateDematCollection(CMDematCollectionUpdateMaster objCMUpdateDematCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateDematCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AveragePrice", objCMUpdateDematCollection.AveragePrice);
                SpParameters.Add("@Quantity", objCMUpdateDematCollection.Quantity);
                SpParameters.Add("@CMDematCollection", ListtoDataTableConverter.ToDataTable(objCMUpdateDematCollection.CMDematCollectionUpdateList).AsTableValuedParameter("Type_CMDematCollection"));
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

        public async Task<string> GetCMApprovedDematCollection(CMApprovedDematCollectionDTO objCMApprovedDematCollection, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedDematCollection";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objCMApprovedDematCollection.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedDematCollection.ApprovalRemark);
                SpParameters.Add("@CMDematCollection", ListtoDataTableConverter.ToDataTable(objCMApprovedDematCollection.CMDematCollectionUpdateList).AsTableValuedParameter("Type_CMDematCollection"));
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
