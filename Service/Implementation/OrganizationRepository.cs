using Model.DTOs.Broker;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class OrganizationRepository : IOrganizationRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public OrganizationRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public Task<string> AddUpdate(BrokerOrganisationListDto entity, string userName)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BrokerOrganisationListDto>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            return await _dbCommonOperation.ReadSingleTable<BrokerOrganisationListDto>("CM_BrokerOrganisationList", null);
        }

        public BrokerOrganisationListDto GetById(int Id, string user)
        {
            throw new NotImplementedException();
        }
    }
}
