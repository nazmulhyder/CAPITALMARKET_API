using Microsoft.AspNetCore.Http;
using Model.DTOs.FDR;
using Model.DTOs.InsurancePremium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IInsurancePremiumRepository
    {
        public Task<List<InsurancePremiumCollectionDto>> GetInsurancePremiumCollection(int CompanyID, int BranchID, string UserName, int FundID, string InstallmentDate);
        public Task<object> GenerateDDIFileAML(List<InsurancePremiumCollectionDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName);
        public Task<object> DDIFileUploadAML(IFormCollection data, string UserName);
        public Task<object> DDIFileListAML(string UserName, int CompanyID, int BranchID, string Status);
        public Task<object> DDIFileApproveAML(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus);
    }
}
