using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.Remat;
using Service.Implementation;

namespace Service.Interface
{
    public interface IRematRepository 
    {
        public Task<string> GetRematCollection(RematDTO objRematCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<RematListDTO>> GetRematInstrumentList(string TransactionDateFrom, string TransactionDateTo,string Status, int CompanyID, int BranchID, string Maker);
        public Task<string> GetUpdateRematInstrument(CMRematInstrumentUpdateMaster objUpdateRematInstrument, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMApprovedRematInstrument(CMApprovedRematInstrumentDTO objCMApprovedRematInstrument, int CompanyID, int BranchID, string Maker);
    }
}
