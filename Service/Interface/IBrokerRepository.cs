using Model.DTOs.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBrokerRepository : IGenericRepository<BrokerOrganisationDetailDto>
    {
        public Task<List<BrokerListDto>> GetAllBrokerList(int PageNo, int Perpage, string SearchKeyword);
    }
}
