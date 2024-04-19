using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Interface;
using Model.DTOs;
using System.ComponentModel.Design;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace Service.Implementation
{
    public class DocumentRepository : IDocumentRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        public DocumentRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
        }

        public async Task<string> UpdateDocumentDetail(int memberDocumentID, string docuentPath, string docFileExtension, string userName, int companyID = 0, int comBranchID = 0)
        {
            string fileName = "";
            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberDocumentID", memberDocumentID);
                parameters.Add("@DocFileExtension", docFileExtension);
                parameters.Add("@DocumentPath", docuentPath);
                parameters.Add("@UserName", userName);
                parameters.Add("@CompanyID", companyID);
                parameters.Add("@ComBranchID", comBranchID);
                parameters.Add("@DocFileName", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                string sql = "dbo.UpdateDocumentDetail";
                try
                {
                    db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
                    fileName = parameters.Get<string>("DocFileName");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return fileName;
            }

        }

        public async Task<string> UpdateAgreementDocumentDetail(int agreementDocumentID, string docuentPath, string docFileExtension, string userName, int companyID = 0, int comBranchID = 0)
        {
            var fileName = "";

            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AgrDocumentID", agreementDocumentID);
                parameters.Add("@DocFileExtension", docFileExtension);
                parameters.Add("@DocumentPath", docuentPath);
                parameters.Add("@UserName", userName);
                parameters.Add("@CompanyID", companyID);
                parameters.Add("@ComBranchID", comBranchID);
                parameters.Add("@DocFileName", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                string sql = "dbo.UpdateAgreementDocumentDetail";
                try
                {
                    db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
                    fileName = parameters.Get<string>("DocFileName");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return fileName;
        }
    }
}
