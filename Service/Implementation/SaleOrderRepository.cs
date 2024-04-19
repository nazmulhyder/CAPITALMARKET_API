using Dapper;
using Model.DTOs.Broker;
using Model.DTOs.BuyOrder;
using Model.DTOs.PriceFileUpload;
using Model.DTOs.SaleOrder;
using Model.DTOs.TradeFileUpload;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class SaleOrderRepository : ISaleOrderRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public SaleOrderRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<object> GetAllSaleOrderAccounts(int companyID, int BranchID,GenerateSaleOrderAccDto SaleOrderAccData)
        {
            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@ProductID", SaleOrderAccData.ProductID);
            sqlParams[1] = new SqlParameter("@companyID", companyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@AccountSelectionType", SaleOrderAccData.AccountSelectionType);
            sqlParams[4] = new SqlParameter("@AccountIds", SaleOrderAccData.AccountIds);
            sqlParams[5] = new SqlParameter("@DPMSaleOrderInstruments", ListtoDataTableConverter.ToDataTable(SaleOrderAccData.saleOrders));

            var dataset =   _dbCommonOperation.FindMultipleDataSetBySP("[CM_ListSaleOrderAccounts]", sqlParams);

            var Result = new
            {
                SaleOrderAccounts = dataset.Tables[0],
                RestrictionMsg = dataset.Tables[1],
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<SaleOrderAccountDto>(dataset.Tables[0]));
            return await Task.FromResult(Result);

        }

        public async Task<List<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>> SaleOrderPortfolioListByProduct(string userName, int ProductID, int companyID, int BranchID)
        {
            var values = new { userName = userName, ProductID = ProductID, companyID = companyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>("[CM_ListSaleOrderDPMProductPortfolioIL]", values);
        }


        public Task<string> SaveSaleOrder(string userName,SaveSaleOrderDto SaleOrder)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@ProductID", SaleOrder.ProductID);
            SpParameters.Add("@ExchangeID", SaleOrder.ExchangeID);
            SpParameters.Add("@DPMSaleOrderInstruments", ListtoDataTableConverter.ToDataTable(SaleOrder.SaleOrderInstrument).AsTableValuedParameter("Type_DPMSaleOrderInstrument"));
            SpParameters.Add("@DPMSaleOrderInstrumentDetails", ListtoDataTableConverter.ToDataTable(SaleOrder.SaleOrderInstrumentDetail).AsTableValuedParameter("Type_DPMSaleOrderInstrumentDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMSaleOrder", SpParameters);
        }

        #region Account Wise Sale Order
        public async Task<List<ListSaleOrderAccWisePortfolioDto>> SaleOrderAcccountWisePortfolioList(string userName, int ProductID, string AccountNo, int companyID, int BranchID)
        {
            var values = new { userName = userName, AccountNo = AccountNo, ProductID = ProductID, companyID = companyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListSaleOrderAccWisePortfolioDto>("[CM_ListSaleOrderAccountWisePortfolioIL]", values);
        }

        public async Task<object> SaveSaleOrderAccountWise(string userName, int companyID, int BranchID, SaveSellOrderAccountWise saveOrderOrderAccountWise)
        {

            // check validation
            var resturnMsg = CheckAccountWiseValidation(userName, saveOrderOrderAccountWise);
            var result = "";

            if (resturnMsg.Tables[0].Rows.Count <= 0)
            {
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@companyID", companyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ProductID", saveOrderOrderAccountWise.ProductID);
                SpParameters.Add("@ExchangeID", saveOrderOrderAccountWise.ExchangeID);
                SpParameters.Add("@DPMSaleOrderAccountWiseDetails", ListtoDataTableConverter.ToDataTable(saveOrderOrderAccountWise.OrderAccWiseList).AsTableValuedParameter("Type_DPMSaleOrderAccountWiseDetails"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                result = await _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMSaleOrderAccountWise", SpParameters);
            }
          
            var Results = new
            {
                Status = result,
                RestrictionMsg = resturnMsg.Tables[0],
            };

            return await Task.FromResult(Results);
        }

        public async Task<List<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>> AllInstrumentPortfolio(string userName, int companyID, int BranchID, int ProductID)
        {
            var values = new { userName = userName, companyID = companyID, BranchID = BranchID, ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<Model.DTOs.SaleOrder.ListDPMProductPortfolioDto>("[CM_ListOrderInstrumentPortfolioIL]", values);
        }

        public DataSet CheckAccountWiseValidation(string userName, SaveSellOrderAccountWise saveOrderOrderAccountWise)
        {

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", userName);
            sqlParams[1] = new SqlParameter("@ProductID", saveOrderOrderAccountWise.ProductID);
            sqlParams[2] = new SqlParameter("@ExchangeID", saveOrderOrderAccountWise.ExchangeID);
            sqlParams[3] = new SqlParameter("@DPMSaleOrderAccountWiseDetails", ListtoDataTableConverter.ToDataTable(saveOrderOrderAccountWise.OrderAccWiseList));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertDPMSaleOrderAccountWiseValidation]", sqlParams);

           
            return DataSets;

        }

        #endregion
    }
}
