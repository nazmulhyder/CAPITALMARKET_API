using Microsoft.AspNetCore.Http;
using Model.DTOs.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuditInspectionRepository
    {
        #region Audit Inspection
        public Task<object> GetObservedClientAccountSL(string UserName, int CompanyID, int BranchID, string TradingCode, int InstrumentID,  string TradingDate);
        public Task<object> SaveAuditInspection(string UserName, int CompanyID, int BranchID, IFormCollection formData);
        public Task<object> GetAuditInspection(string UserName, int CompanyID, int BranchID, AuditSearchFilter filter);
        public Task<object> GetAuditInspectionById(int CompanyID, int BranchID, string userName, int ReferenceNo);
        #endregion Audit Inspection

        #region Requlatory Query Entry
        public Task<object> GetPrincipleAndJointApplicantSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNo);
        public Task<object> GetRequlatoryTWSSL(string UserName, int CompanyID, int BranchID, int ContractID, int InstrumentID,  string TradingDateFrom, string TradingDateTo);
        public Task<object> SaveRegulatory(string UserName, int CompanyID, int BranchID, IFormCollection formData);
        public Task<object> GetRegulatoryList(string UserName, int CompanyID, int BranchID, RequlatoryQuerySearchFilter filter);
        public Task<object> GetRegulatoryById(int CompanyID, int BranchID, string userName, int RegulatoryQueryID);
        #endregion Requlatory Query Entry
    }
}
