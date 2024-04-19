using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Demat;
using Model.DTOs.SecurityElimination;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class SecurityEliminationRepository : ISecurityEliminationRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public SecurityEliminationRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }
        public Task<List<SecurityEliminationDTO>> CMSecurityEliminationList(int InstrumentID, int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstrumentID = InstrumentID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SecurityEliminationDTO>("[CM_ListSecurityElimination]", values);
        }

        public async Task<string> InsertCMSecurityElimination(SecurityEliminationInsertDTO entrySecurityELimination, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_InsertUpdateSecuirtyElimination";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CMSecurityElimination", ListtoDataTableConverter.ToDataTable(entrySecurityELimination.CMSecurityEliminationList).AsTableValuedParameter("Type_CMSecurityElimination"));
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

        public Task<List<SecuirtyEliminationMaster>> CMSecurityEliminationApprovalList(int InstrumentID, string Status,int CompanyID, int BranchID, string Maker)
        {

            var values = new
            {
                InstrumentID = InstrumentID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker

            };
            return _dbCommonOperation.ReadSingleTable<SecuirtyEliminationMaster>("[CM_ListSecurityInstrumentEliminationApproval]", values);
        }

        public async Task<string> GetCMApprovedInstrumentElimination(CMSecurityInstrumentEliminationApproveDTO objCMApprovedInstrumentElimination, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_SecurityInstrumentEliminationApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@@InstrumentID", objCMApprovedInstrumentElimination.InstrumentID);
                SpParameters.Add("@ApprovalStatus", objCMApprovedInstrumentElimination.Status);
                SpParameters.Add("@ApprovalRemark", objCMApprovedInstrumentElimination.ApprovalRemark);
                SpParameters.Add("@CMSecurityElimination", ListtoDataTableConverter.ToDataTable(objCMApprovedInstrumentElimination.CMSecurityEliminationApprove).AsTableValuedParameter("Type_CMSecurityElimination"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                SecurityEliminationDTO PrevEquityInstrument = new SecurityEliminationDTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetCMUpdateSecurityElimination(CMSecurityEliminationUpdateMaster objCMUpdateSecurityElimination, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_UpdateSecurityElimination";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Rate", objCMUpdateSecurityElimination.Rate);
                SpParameters.Add("@Amount", objCMUpdateSecurityElimination.Amount);
                SpParameters.Add("@CMSecurityElimination", ListtoDataTableConverter.ToDataTable(objCMUpdateSecurityElimination.CMSecurityEliminationUpdateList).AsTableValuedParameter("Type_CMSecurityElimination"));
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



    }
}
