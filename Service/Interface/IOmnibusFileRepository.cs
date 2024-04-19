using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.ImportExportOmnibus;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOmnibusFileRepository
    {
        #region AML IL
        public Task<List<ExportFileBrokerDto>> BrokerList(int CompanyID, int BranchID);
        public Task<object> GetExportedFiles(IFormCollection form, string UserName, string FileType, int CompanyID, int BranchID);

        #endregion AML IL

        #region SL
        public Task<object> SLLimitFileExport(IFormCollection formdata, int CompanyID, int BranchID, string Username);
        public Task<object> SLClientRegistrationFileExport(IFormCollection formdata, int CompanyID, int BranchID, string Username);
        public Task<object> SL_ListExportClientRegistrationFile(string UserName, string FileType);
        public Task<object> GetOmnibusAccountList();
        public Task<object> GetOmnibusLimitFileList(int CompanyID, string FileType, DateTime TradeDate);
        public Task<string> InsertOmnibusLimitFileText(IFormCollection formdata,int CompanyID, int BranchID, string UserName);
        public Task<object> InsertOmnibusLimitFileValidation(IFormCollection formdata,int CompanyID, int BranchID, string UserName);

        #endregion SL
    }
}
