using Model.DTOs.AssetManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAssetManagerRepository : IGenericRepository<CMAssetManagerOrganisationDetailDTO>
    {
        public Task<string> UpdateAccountTradingCodes(List<AMLBrokerDto> data, string userName, int BrokerID);
        public Task<List<AMLBrokerDto>> GetAllAccountTradingCodes(int BrokerID);
        public Task<List<AssetManagerListDto>> GetAllAssetManager(int PageNo, int Perpage, string SearchKeyword);
    }

}
