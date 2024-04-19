using Microsoft.AspNetCore.Http;
using Model.DTOs.PriceFileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPriceFileRepository
    {
        public Task<string> SaveNewInstrumentCategory(string CategoryName);
        public Task<object> PriceFileComparison(IFormCollection formData,string UserName, int CompanyID, int BranchID);
        public Task<object> PriceFileComparisonFromFTP(string UserName, int CompanyID, int BranchID);

        public Task<object> PriceFileUpload(IFormCollection formData, string UserName);

        public Task<List<ClosingPriceFileListDto>> ClosingPriceFileList(int CompanyID, DateTime TradeDate);

    }
}
