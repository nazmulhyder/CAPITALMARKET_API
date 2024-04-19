using Dapper;
using Model.DTOs.BuyOrder;
using Model.DTOs.BuyOrder;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class BuyOrderRepository : IBuyOrderRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public BuyOrderRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<object> GetAllBuyOrderAccounts(int companyID, int BranchID, GenerateBuyOrderAccDto BuyOrderAccData)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@ProductID", BuyOrderAccData.ProductID);
            sqlParams[0] = new SqlParameter("@companyID", companyID);
            sqlParams[0] = new SqlParameter("@BranchID", BranchID);
            sqlParams[1] = new SqlParameter("@AccountSelectionType", BuyOrderAccData.AccountSelectionType);
            sqlParams[2] = new SqlParameter("@AccountIds", BuyOrderAccData.AccountIds);
            sqlParams[3] = new SqlParameter("@IsNettingAllowed", BuyOrderAccData.IsNettingAllowed);
            sqlParams[4] = new SqlParameter("@DPMBuyOrderInstruments", ListtoDataTableConverter.ToDataTable(BuyOrderAccData.BuyOrders));


            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_ListBuyOrderAccounts]", sqlParams);

            var Result = new
            {
                BuyOrderAccounts = dataset.Tables[0],
                RestrictionMsg = dataset.Tables[1],
            };

            //return await Task.FromResult(CustomConvert.DataSetToList<BuyOrderAccountDto>());
            return await Task.FromResult(Result);

        }


        public async Task<List<ListDPMProductPortfolioDto>> BuyOrderPortfolioListByProduct(string userName, int ProductID, int companyID, int BranchID)
        {
            var values = new { userName = userName, ProductID = ProductID, companyID = companyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListDPMProductPortfolioDto>("[CM_ListBuyOrderDPMProductPortfolioIL]", values);
        }


        public Task<string> SaveBuyOrder(string userName, int companyID, int BranchID, SaveBuyOrderDto BuyOrder)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@CompanyID", companyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ProductID", BuyOrder.ProductID);
            SpParameters.Add("@ExchangeID", BuyOrder.ExchangeID);
            SpParameters.Add("@DPMBuyOrderInstruments", ListtoDataTableConverter.ToDataTable(BuyOrder.BuyOrders).AsTableValuedParameter("Type_DPMBuyOrderInstrument"));
            SpParameters.Add("@DPMBuyOrderInstrumentDetails", ListtoDataTableConverter.ToDataTable(BuyOrder.BuyOrderInstrumentDetails).AsTableValuedParameter("Type_DPMBuyOrderInstrumentDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMBuyOrder", SpParameters);
        }

        #region Account Wise Buy Order
        public async Task<List<ListBuyOrderAccWisePortfolioDto>> BuyOrderAcccountWisePortfolioList(string userName, int ProductID, string AccountNo, int companyID, int BranchID)
        {
            var values = new { userName = userName, AccountNo = AccountNo, ProductID = ProductID, companyID = companyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ListBuyOrderAccWisePortfolioDto>("[CM_ListBuyOrderAccountWisePortfolioIL]", values);
        }

        public Task<string> SaveBuyOrderAccountWise(string userName, int CompanyID, int BranchID, SaveBuyOrderAccountWise saveOrderOrderAccountWise)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ProductID", saveOrderOrderAccountWise.ProductID);
            //SpParameters.Add("@AccNo", saveOrderOrderAccountWise.AccountNo);
            SpParameters.Add("@ExchangeID", saveOrderOrderAccountWise.ExchangeID);
            SpParameters.Add("@DPMBuyOrderAccountWiseDetails", ListtoDataTableConverter.ToDataTable(saveOrderOrderAccountWise.OrderAccWiseList).AsTableValuedParameter("Type_DPMBuyOrderAccountWiseDetails"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMBuyOrderAccountWise", SpParameters);
        }
        #endregion
    }
}
