using Model.DTOs.TradingPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICMTradingPlatformRepository : IGenericRepository<CMTradingPlatformDTO>
    {
        public Task<List<CMTradingPlatformDTO>> GetCMTradingPlatformByExchangeID(int ExchangeID);
    }
}
