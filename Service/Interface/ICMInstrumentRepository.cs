using Model.DTOs.Instrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICMInstrumentRepository : IGenericRepository<CMInstrumentDTO>
    {
        public Task<List<CMInstrumentDTO>> GetAllInstrumentList(string InstrumentType, int PageNo, int PerPage, string SearchKeyword, string ApprovalStatus);
        public Task<CMBondInstrumentDto> GetBondInstrumentById(int InstrumentID, string user);
        public Task<CMInstrumentDTO> GetMutualFundInstrumentById(int InstrumentID, string user);
        public Task<GsecInstrumentDto> GetGsecInstrumentById(int InstrumentID, string user);
        public Task<string> AddUpdateMutualFundInstrument(CMInstrumentDTO entityDto, string userName);
        public Task<string> AddUpdateBondInstrument(CMBondInstrumentDto entityDto, string userName);
        public Task<string> AddUpdateGsecInstrument(GsecInstrumentDto entityDto, string userName);

        public Task<string> ApproveInstrument(ApprovalInstrumentDto approve, string userName);
    }
}
