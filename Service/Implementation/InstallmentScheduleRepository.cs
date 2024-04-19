using AutoMapper;
using Dapper;
using Model.DTOs.InstallmentSchedule;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class InstallmentScheduleRepository : IInstallmentScheduleRepository
    {

        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public InstallmentScheduleRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        public async Task<string> AddInstallmentScheduleSL(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto)
        {
            string sp = "";
            DataTable MemberAccInfo = new DataTable();
            DynamicParameters SpParameters = new DynamicParameters();

            if (entityDto.InstallmentID == 0 || entityDto.InstallmentID == null)
            {
                sp = "CM_InsertUpdateInstallmentScheduleSL";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

               
            }

            else
            {
                sp = "CM_InsertUpdateInstallmentScheduleSL";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            }

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }
        public async Task<string> AddInstallmentScheduleIL(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto)
        {
            string sp = "";
            DataTable MemberAccInfo = new DataTable();
            DynamicParameters SpParameters = new DynamicParameters();

            if (entityDto.InstallmentID == 0 || entityDto.InstallmentID == null)
            {
                sp = "CM_InsertUpdateInstallmentScheduleIL";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


            }

            else
            {
                sp = "CM_InsertUpdateInstallmentScheduleIL";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            }

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }
        public async Task<string> AddInstallmentScheduleAML(int companyId, int branchID, string userName, GenInstallmentSchedule entityDto)
        {
            string sp = "";
            DataTable MemberAccInfo = new DataTable();
            DynamicParameters SpParameters = new DynamicParameters();

            if (entityDto.InstallmentID == 0 || entityDto.InstallmentID == null)
            {
                sp = "CM_InsertUpdateInstallmentScheduleAML";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


            }

            else
            {
                sp = "CM_InsertUpdateInstallmentScheduleAML";
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@BranchID", companyId);
                SpParameters.Add("@InstallmentID", entityDto.InstallmentID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DueDate", Utility.DatetimeFormatter.DateFormat(entityDto.DueDate));
                SpParameters.Add("@Amount", entityDto.Amount);
                SpParameters.Add("@Status", entityDto.Status);
                SpParameters.Add("@CollectionMode", entityDto.CollectionMode);
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            }

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }
    }
}
