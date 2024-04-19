using Model.DTOs.BuyOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBuyOrderRepository
    {
        public Task<List<ListDPMProductPortfolioDto>> BuyOrderPortfolioListByProduct(string userName, int ProductID, int companyID, int BranchID);
        public Task<object> GetAllBuyOrderAccounts(int ProductID, int companyID, GenerateBuyOrderAccDto BuyOrderAccData);
        public Task<string> SaveBuyOrder(string userName, int companyID, int BranchID, SaveBuyOrderDto BuyOrder);
        public Task<List<ListBuyOrderAccWisePortfolioDto>> BuyOrderAcccountWisePortfolioList(string userName, int ProductID, string AccountNo, int companyID, int BranchID);
        public Task<string> SaveBuyOrderAccountWise(string userName, int CompanyID, int BranchID, SaveBuyOrderAccountWise saveOrderOrderAccountWise);
    }
}
