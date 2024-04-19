using Dapper;
using Model.DTOs.ApprovalHistory;
using Model.DTOs.UpdateLog;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class ApprovalHistoryRepository : IApprovalHistoryRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public ApprovalHistoryRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<List<ApprovalHistoryDTO>> getApprovalHistoryList(int approvalTypeCodeId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@approvalTypeCode", approvalTypeCodeId);

                var approvalHistoryList = await _dbCommonOperation.ReadSingleTable<ApprovalHistoryDTO>("[dbo].[ApprovalHistory]", parameters);

                return approvalHistoryList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
