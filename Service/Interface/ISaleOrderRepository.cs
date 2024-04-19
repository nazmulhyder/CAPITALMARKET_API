using Model.DTOs.Broker;
using Model.DTOs.BuyOrder;
using Model.DTOs.SaleOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISaleOrderRepository
    {
        public Task<List<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>> SaleOrderPortfolioListByProduct(string userName, int ProductID, int companyID, int BranchID);
        public Task<object> GetAllSaleOrderAccounts(int companyID, int BranchID,GenerateSaleOrderAccDto SaleOrderAccData);
        public Task<string> SaveSaleOrder(string userName,SaveSaleOrderDto SaleOrder);
        public Task<List<ListSaleOrderAccWisePortfolioDto>> SaleOrderAcccountWisePortfolioList(string userName, int ProductID,string AccountNo, int companyID, int BranchID);
        public Task<object> SaveSaleOrderAccountWise(string userName, int companyID, int BranchID, SaveSellOrderAccountWise saveOrderOrderAccountWise);
        public Task<List<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>> AllInstrumentPortfolio(string userName, int companyID, int BranchID, int ProductID);

    }
}
