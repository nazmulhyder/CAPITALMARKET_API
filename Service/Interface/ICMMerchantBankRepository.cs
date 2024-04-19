using Model.DTOs.CMMerchantBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICMMerchantBankRepository : IGenericRepository<MerchantBankOraganisationDetailDto>
    {
        public Task<List<MerchantBankListDto>> GetCMMerchantBankList(int PageNo, int PerPage, string SearchKeyword);
    }
}
