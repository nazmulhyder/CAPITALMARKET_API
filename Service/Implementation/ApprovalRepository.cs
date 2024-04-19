using Dapper;
using Model.DTOs.Approval;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Service.Implementation
{
    public class ApprovalRepository : IApprovalRepository
    {
        public readonly IConfiguration _configuration;
        public readonly IDBCommonOpService _dbCommonOperation;
        public ApprovalRepository(IConfiguration configuration, IDBCommonOpService dbCommonOperation)
        {
            _configuration = configuration;
            _dbCommonOperation = dbCommonOperation;
        }

        public bool UpdateApprovalStatus(int approvalReqSetId, ApprovalDetail approvalDetl)
        {
            bool returnVal = false;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ApprovalReqSetID", approvalReqSetId);
            parameters.Add("@ApprovalStatus", approvalDetl.approvalStatus);
            parameters.Add("@ApproverRemarks", approvalDetl.approverRemark);
            parameters.Add("@Approver", approvalDetl.approver);

            //var result = _dbCommonOperation.InsertUpdateBySP("dbo.UpdateApprovalRequest", parameters);

            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {
                string sql = "dbo.UpdateApprovalRequest";
                try
                {
                    var result = db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                    returnVal = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return returnVal;
        }
    }
}
