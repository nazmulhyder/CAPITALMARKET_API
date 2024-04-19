using Model.DTOs.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICMMarketTypeRepository : IGenericRepository<CMMarketTypeDTO>
    {
        public Task<CMMarketTypeListDTO> GetCMMarketTypeById(int MarketTypeID, string user);
        public Task<List<CMMarketTypeDTO>> GetByTradingPlatformID(int TradingPlatformID);
        public Task<List<CMMarketTypeListDTO>> GetAllMarketType(int PageNo, int Perpage, string SearchKeyword);
    }
}
