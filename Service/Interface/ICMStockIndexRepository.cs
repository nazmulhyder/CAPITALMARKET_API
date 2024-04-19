using Model.DTOs.CMStockIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICMStockIndexRepository : IGenericRepository<CMStockIndexDTO>
    {
        public Task<List<CMStockIndexDTO>> GetCMStockIndexByExchangeID(int ID);
    }
}
