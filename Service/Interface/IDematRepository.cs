using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs;
using Model.DTOs.Demat;

namespace Service.Interface
{
    public interface IDematRepository
    {
        public Task<string> GetDematCollection(DematDTO objDematCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<DematListDTO>> GetDematCollectionList(string TransactionDateFrom, string TransactionDateTo,string Status, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMUpdateDematCollection(CMDematCollectionUpdateMaster objCMUpdateDematCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMApprovedDematCollection(CMApprovedDematCollectionDTO objCMApprovedDematCollection, int CompanyID, int BranchID, string Maker);
        
    }

}
