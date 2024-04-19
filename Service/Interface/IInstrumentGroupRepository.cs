using Model.DTOs.Allocation;
using Model.DTOs.InstrumentGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IInstrumentGroupRepository : IGenericRepository<InstrumentGroupDto>
    {
        public Task<string> AddUpdateInsGrp(InstrumentGroupDto data, string userName,int CompanyID, int BranchID);
        public Task<List<InstrumentDropdownDto>> InstrumentDropdown();
        public Task<List<InstrumentGroupDropdownDto>> InstrumentGroupDropdown();
        public Task<List<InstrumentByInstrumentGrpDto>> InstrumentByInsGrpIds(string InsGrpIds);
        public Task<string> InstrumentGroupApproval(string userName, InstrumentGroupApprovalDto approvalDto);
        public Task<List<InstrumentGroupDto>> GetAllInstrumentGrp(string UserName, int CompanyID, int BranchID,int PageNo, int Perpage, string SearchKeyword);
        public Task<List<InstrumentGroupApprovalListDto>> GetAllInstrumentGrpApproval(string UserName, int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword);
    }
}
