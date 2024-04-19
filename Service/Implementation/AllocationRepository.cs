using AutoMapper;
using Dapper;
using DocumentFormat.OpenXml.ExtendedProperties;
using Model.DTOs.Allocation;
using Model.DTOs.AssetManager;
using Model.DTOs.SaleOrder;
using Model.DTOs.User;
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
    public class AllocationRepository : IAllocationRepository
    {

        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public AllocationRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        #region Sale Order Allocation

        public async Task<List<SaleAllocationBatchInfoDto>> GetAllocationSaleOrderBatchInfo(string userName, int CompanyID, int BranchID)
		{
            var values = new { UserName = userName, CompanyID= CompanyID, BranchID= BranchID };
            return await _dbCommonOperation.ReadSingleTable<SaleAllocationBatchInfoDto>("[ListBatchInfosOfSaleOrder]", values);
        }


        public async Task<List<SaleOrderTradeDto>> GetAllSaleOrderTrade(string userName, int CompanyID, int BranchID, int SaleOrderID)
        {
            var values = new { UserName = userName,CompnayID= CompanyID, BranchID= BranchID, SaleOrderID = SaleOrderID };
            return await _dbCommonOperation.ReadSingleTable<SaleOrderTradeDto>("[SaleOrderOfTradeData]", values);
        }


        //public async Task<List<AccountSaleOrderAllocationDto>> GetAllSaleOrderAllocationAccounts(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades)
        //{
        //    SqlParameter[] sqlParams = new SqlParameter[3];
        //    sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
        //    sqlParams[1] = new SqlParameter("@BranchID", BranchID);
        //    sqlParams[2] = new SqlParameter("@SaleOrderAllocationTrades", ListtoDataTableConverter.ToDataTable(SaleOrderTrades));
        //    var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[ListSaleOrderAllocationAccounts]", sqlParams);

        //    return await Task.FromResult(CustomConvert.DataSetToList<AccountSaleOrderAllocationDto>(dataset.Tables[0]));
        //}

        public async Task<object> GetAllSaleOrderAllocationAccounts(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades)
        {

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);
            sqlParams[2] = new SqlParameter("@SaleOrderAllocationTrades", ListtoDataTableConverter.ToDataTable(SaleOrderTrades));
            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[ListSaleOrderAllocationAccounts]", sqlParams);

            var tmpAccountSaleOrderAllocations = CustomConvert.DataSetToList<AccountSaleOrderAllocation_v2_Dto>(dataset.Tables[0]);

            #region Distribute fractional SaleOrder Allocation

            bool exeStatus  = false;
            //decimal totalOrderQty = tmpAccountSaleOrderAllocations.Select(x => x.OrderQty.GetValueOrDefault()).Sum();
            decimal tmpAvailableExecutedQty = tmpAccountSaleOrderAllocations.Count()>0 ? tmpAccountSaleOrderAllocations[0].availableExecutedQty.GetValueOrDefault() : 0;
            decimal tmpTotalAllocatedOrderQty = tmpAccountSaleOrderAllocations.Count() > 0 ? tmpAccountSaleOrderAllocations[0].totalAllocatedOrderQty.GetValueOrDefault() : 0;
            decimal totalRequiredQty = tmpAccountSaleOrderAllocations.Count() > 0 ? tmpAccountSaleOrderAllocations.Select(x => x.ReqOrderQty.GetValueOrDefault()).Sum() :0;


            while (tmpAvailableExecutedQty>0 && totalRequiredQty > 0)
            foreach (var Item in tmpAccountSaleOrderAllocations)
            {
                exeStatus = false;
                decimal tmpAllocationQty = 0;

                if (Item.AllocationType.ToLower() == "pro-data-basis")
                {
                    if (Item.ReqOrderQty >= 1 && Item.availableExecutedQty > 0 && Item.AllocatedQty < Item.OrderQty)
                    {

                        //tmpAllocationQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
                        tmpAllocationQty = 1;
                        Item.AllocatedQty += tmpAllocationQty;
                        Item.ReqOrderQty -= tmpAllocationQty;
                        Item.totalAllocatedOrderQty += tmpAllocationQty;
                        Item.availableExecutedQty -= tmpAllocationQty;
                        totalRequiredQty -= tmpAllocationQty;
                        exeStatus = true;
                    }

                    // update full list for this item
                    if (exeStatus)
                    {
                        foreach (var updateItem in tmpAccountSaleOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
                        {
                            updateItem.availableExecutedQty = Item.availableExecutedQty;
                            updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
                                //updateItem.totalExecutedQty = Item.totalExecutedQty;
                            tmpAvailableExecutedQty = updateItem.availableExecutedQty.GetValueOrDefault();
                        }
                    }
                }

                if (Item.AllocationType.ToLower() == "fill-basis")
                {
                    exeStatus = false;
                    decimal tmpAllocatedQty = 0;
                    if (Item.ReqOrderQty > 0 && Item.availableExecutedQty > 0 && Item.totalAllocatedOrderQty < Item.totalExecutedQty)
                    //&& Item.ReqOrderQty < Item.availableExecutedQty && (Item.AllocatedQty + Item.ReqOrderQty) <= Item.OrderQty)
                    {

                        tmpAllocatedQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
                        Item.AllocatedQty = tmpAllocatedQty;
                        Item.ReqOrderQty -= tmpAllocatedQty;
                        Item.totalAllocatedOrderQty += tmpAllocatedQty;
                        Item.availableExecutedQty = Item.availableExecutedQty - tmpAllocatedQty;
                            totalRequiredQty -= tmpAllocatedQty;
                            exeStatus = true;
                    }

                    // update full list for this item
                    if (exeStatus)
                    {
                        foreach (var updateItem in tmpAccountSaleOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
                        {
                            updateItem.availableExecutedQty = Item.availableExecutedQty;
                            updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
                                //updateItem.totalExecutedQty = Item.totalExecutedQty;
                            tmpAvailableExecutedQty = updateItem.availableExecutedQty.GetValueOrDefault();
                        }
                    }
                }
            }

            //List<AccountSaleOrderAllocationDto> result = Mapper.Map<List<AccountSaleOrderAllocation_v2_Dto>>(tmpAccountSaleOrderAllocations);
            var data  = mapper.Map<List<AccountSaleOrderAllocationDto>>(tmpAccountSaleOrderAllocations);
            #endregion

            var Result = new
            {
                Allocations = data,
                RestrictionMsg = dataset.Tables[1],
            };

            return await Task.FromResult(Result);
        }

        //public async Task<List<AccountSaleOrderAllocation_v2_Dto>> GetAllSaleOrderAllocationAccounts_v3(int CompanyID, int BranchID, List<SaleOrderTradeDto> SaleOrderTrades)
        //{


        //    SqlParameter[] sqlParams = new SqlParameter[3];
        //    sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
        //    sqlParams[1] = new SqlParameter("@BranchID", BranchID);
        //    sqlParams[2] = new SqlParameter("@SaleOrderAllocationTrades", ListtoDataTableConverter.ToDataTable(SaleOrderTrades));
        //    var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[ListSaleOrderAllocationAccounts_v2]", sqlParams);

        //    var tmpAccountSaleOrderAllocations = CustomConvert.DataSetToList<AccountSaleOrderAllocation_v2_Dto>(dataset.Tables[0]);

        //    #region Distribute fractional SaleOrder Allocation

        //    bool exeStatus = false;
        //    int tmpInstrumentID = 0;
        //    foreach (var Item in tmpAccountSaleOrderAllocations)
        //    {
        //        exeStatus = false;
        //        decimal tmpAllocationQty = 0;

        //        if (Item.AllocationType.ToLower() == "pro-data-basis")
        //        {
        //            if (Item.ReqOrderQty >= 1 && Item.availableExecutedQty > 0 && Item.AllocatedQty < Item.OrderQty)
        //            {

        //                tmpAllocationQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
        //                Item.AllocatedQty += tmpAllocationQty;
        //                Item.ReqOrderQty -= tmpAllocationQty;
        //                Item.totalAllocatedOrderQty += tmpAllocationQty;
        //                Item.availableExecutedQty -= tmpAllocationQty;
        //                exeStatus = true;
        //            }

        //            // update full list for this item
        //            if (exeStatus)
        //            {
        //                foreach (var updateItem in tmpAccountSaleOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
        //                {
        //                    updateItem.availableExecutedQty = Item.availableExecutedQty;
        //                    updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
        //                    //updateItem.totalExecutedQty = Item.totalExecutedQty;
        //                }
        //            }
        //        }

        //        if (Item.AllocationType.ToLower() == "fill-basis")
        //        {
        //            exeStatus = false;
        //            decimal tmpAllocatedQty = 0;
        //            if (Item.ReqOrderQty > 0 && Item.availableExecutedQty > 0  && Item.totalAllocatedOrderQty < Item.totalExecutedQty)
        //                //&& Item.ReqOrderQty < Item.availableExecutedQty && (Item.AllocatedQty + Item.ReqOrderQty) <= Item.OrderQty)
        //            {

        //                tmpAllocatedQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
        //                Item.AllocatedQty = tmpAllocatedQty;
        //                Item.ReqOrderQty -= tmpAllocatedQty;
        //                Item.totalAllocatedOrderQty += tmpAllocatedQty;
        //                Item.availableExecutedQty = Item.availableExecutedQty - tmpAllocatedQty;
        //                exeStatus = true;
        //            }

        //            // update full list for this item
        //            if (exeStatus)
        //            {
        //                foreach (var updateItem in tmpAccountSaleOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
        //                {
        //                    updateItem.availableExecutedQty = Item.availableExecutedQty;
        //                    updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
        //                    //updateItem.totalExecutedQty = Item.totalExecutedQty;
        //                }
        //            }
        //        }
        //    }

        //    //List<AccountSaleOrderAllocationDto> result = Mapper.Map<List<AccountSaleOrderAllocation_v2_Dto>>(tmpAccountSaleOrderAllocations);
        //    var data = mapper.Map<List<AccountSaleOrderAllocationDto>>(tmpAccountSaleOrderAllocations);
        //    #endregion
        //    return await Task.FromResult(tmpAccountSaleOrderAllocations);
        //}

        public Task<string> SaveSaleOrderAllocation(int CompanyID, int BranchID, string userName, SaveSaleOrderAllcoationDto SaleOrderAllocation)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@SaleOrderID", SaleOrderAllocation.SaleOrderID);
            SpParameters.Add("@DPMSaleOrderAllocationInstruments", ListtoDataTableConverter.ToDataTable(SaleOrderAllocation.SaleOrderTrades).AsTableValuedParameter("Type_DPMSaleOrderAllocationInstrument"));
            SpParameters.Add("@DPMSaleOrderAllocationInstrumentDetails", ListtoDataTableConverter.ToDataTable(SaleOrderAllocation.SaleOrderAllocationAccounts).AsTableValuedParameter("Type_DPMSaleOrderAllocationInstrumentDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMSaleOrderAllocation", SpParameters);
        }

        #endregion

        #region Buy Order Allocation
        public async Task<List<BuyAllocationBatchInfoDto>> GetAllocationBuyOrderBatchInfo(string userName, int CompanyID, int BranchID)
        {
            var values = new { UserName = userName, CompanyID= CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<BuyAllocationBatchInfoDto>("[ListBatchInfosOfBuyOrder]", values);
        }

        public async Task<List<BuyOrderTradeDto>> GetAllBuyOrderTrade(string userName, int CompanyID, int BranchID, int BuyOrderID)
        {
            var values = new { UserName = userName, CompanyID = CompanyID, BranchID= BranchID, BuyOrderID = BuyOrderID };
            return await _dbCommonOperation.ReadSingleTable<BuyOrderTradeDto>("[BuyOrderOfTradeData]", values);
        }

        //public async Task<List<AccountBuyOrderAllocationDto>> GetAllBuyOrderAllocationAccounts(int CompanyID, int BranchID, List<BuyOrderTradeDto> BuyOrderTrades)
        //{
        //    SqlParameter[] sqlParams = new SqlParameter[3];
        //    sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
        //    sqlParams[1] = new SqlParameter("@BranchID", BranchID);
        //    sqlParams[2] = new SqlParameter("@BuyOrderAllocationTrades", ListtoDataTableConverter.ToDataTable(BuyOrderTrades));
        //    var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[ListBuyOrderAllocationAccounts]", sqlParams);

        //    return await Task.FromResult(CustomConvert.DataSetToList<AccountBuyOrderAllocationDto>(dataset.Tables[0]));
        //}


        public async Task<object> GetAllBuyOrderAllocationAccounts(int CompanyID, int BranchID, List<BuyOrderTradeDto> BuyOrderTrades)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);
            sqlParams[2] = new SqlParameter("@BuyOrderAllocationTrades", ListtoDataTableConverter.ToDataTable(BuyOrderTrades));
            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[ListBuyOrderAllocationAccounts]", sqlParams);

            #region Distribute fractional SaleOrder Allocation

            bool exeStatus = false;
            int tmpInstrumentID = 0;
            var tmpAccountBuyOrderAllocations = CustomConvert.DataSetToList<AccountBuyOrderAllocation_v2_Dto>(dataset.Tables[0]);
            decimal tmpAvailableExecutedQty = tmpAccountBuyOrderAllocations.Count() > 0 ? tmpAccountBuyOrderAllocations[0].availableExecutedQty.GetValueOrDefault() : 0;
            decimal tmpTotalAllocatedOrderQty = tmpAccountBuyOrderAllocations.Count() > 0 ? tmpAccountBuyOrderAllocations[0].totalAllocatedOrderQty.GetValueOrDefault() : 0;
            decimal totalRequiredQty = tmpAccountBuyOrderAllocations.Count() > 0 ? tmpAccountBuyOrderAllocations.Select(x => x.ReqOrderQty.GetValueOrDefault()).Sum() : 0;


            while (tmpAvailableExecutedQty > 0 && totalRequiredQty > 0)
                foreach (var Item in tmpAccountBuyOrderAllocations)
                {
                    exeStatus = false;

                    decimal tmpAllocationQty = 0;

                    if (Item.AllocationType.ToLower() == "pro-data-basis")
                    {
                        if (Item.ReqOrderQty >= 1 && Item.availableExecutedQty > 0 && Item.AllocatedQty < Item.OrderQty)
                        {

                            //tmpAllocationQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
                            tmpAllocationQty = 1;
                            Item.AllocatedQty += tmpAllocationQty;
                            Item.ReqOrderQty -= tmpAllocationQty;
                            Item.totalAllocatedOrderQty += tmpAllocationQty;
                            Item.availableExecutedQty -= tmpAllocationQty;
                            totalRequiredQty -= tmpAllocationQty;
                            exeStatus = true;
                        }

                        // update full list for this item
                        if (exeStatus)
                        {
                            foreach (var updateItem in tmpAccountBuyOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
                            {
                                updateItem.availableExecutedQty = Item.availableExecutedQty;
                                updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
                                //updateItem.totalExecutedQty = Item.totalExecutedQty;
                                tmpAvailableExecutedQty = updateItem.availableExecutedQty.GetValueOrDefault();
                            }
                        }
                    }

                    if (Item.AllocationType.ToLower() == "fill-basis")
                    {
                        exeStatus = false;
                        decimal tmpAllocatedQty = 0;
                        if (Item.ReqOrderQty > 0 && Item.availableExecutedQty > 0 && Item.totalAllocatedOrderQty < Item.totalExecutedQty)
                        //&& Item.ReqOrderQty < Item.availableExecutedQty && (Item.AllocatedQty + Item.ReqOrderQty) <= Item.OrderQty)
                        {

                            tmpAllocatedQty = Item.availableExecutedQty >= Item.ReqOrderQty.GetValueOrDefault() ? Item.ReqOrderQty.GetValueOrDefault() : Item.availableExecutedQty.GetValueOrDefault();
                            Item.AllocatedQty = tmpAllocatedQty;
                            Item.ReqOrderQty -= tmpAllocatedQty;
                            Item.totalAllocatedOrderQty += tmpAllocatedQty;
                            Item.availableExecutedQty = Item.availableExecutedQty - tmpAllocatedQty;
                            totalRequiredQty -= tmpAllocationQty;
                            exeStatus = true;
                        }

                        // update full list for this item
                        if (exeStatus)
                        {
                            foreach (var updateItem in tmpAccountBuyOrderAllocations.Where(x => x.InstrumentID == Item.InstrumentID.GetValueOrDefault()))
                            {
                                updateItem.availableExecutedQty = Item.availableExecutedQty;
                                updateItem.totalAllocatedOrderQty = Item.totalAllocatedOrderQty;
                                //updateItem.totalExecutedQty = Item.totalExecutedQty;
                                tmpAvailableExecutedQty = updateItem.availableExecutedQty.GetValueOrDefault();
                            }
                        }
                    }
                }

            //List<AccountBuyOrderAllocationDto> result = mapper.Map<List<AccountBuyOrderAllocationDto>>(tmpAccountSaleOrderAllocations);
            var data = mapper.Map<List<AccountBuyOrderAllocationDto>>(tmpAccountBuyOrderAllocations);
            #endregion
            //return await Task.FromResult(data);
            var Result = new
            {
                Allocations = data,
                RestrictionMsg = dataset.Tables[1],
            };

            return await Task.FromResult(Result);
        }

        public Task<string> SaveBuyOrderAllocation(int CompanyID, int BranchID, string userName, SaveBuyOrderAllcoationDto BuyOrderAllocation)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@BuyOrderID", BuyOrderAllocation.BuyOrderID);
            SpParameters.Add("@DPMBuyOrderAllocationInstruments", ListtoDataTableConverter.ToDataTable(BuyOrderAllocation.BuyOrderTrades).AsTableValuedParameter("Type_DPMBuyOrderAllocationInstrument"));
            SpParameters.Add("@DPMBuyOrderAllocationInstrumentDetails", ListtoDataTableConverter.ToDataTable(BuyOrderAllocation.BuyOrderAllocationAccounts).AsTableValuedParameter("Type_DPMBuyOrderAllocationInstrumentDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return _dbCommonOperation.InsertUpdateBySP("CM_InsertDPMBuyOrderAllocation", SpParameters);
        }

        #endregion

        #region Sale Order Approval

        public async Task<string> AllocationApproval(string userName, AllocationApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@AllocationID", approvalDto.AllocationID);
            SpParameters.Add("@AllocationType", approvalDto.AllocationType);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveILTradeAllocation", SpParameters);
        }

        public async Task<List<SaleAllocationApprovalListDto>> GetAllSaleOrderAllocation(string userName, int CompanyID, int BranchID)
        {
            var values = new { UserName = userName,CompnayID= CompanyID, BranchID= BranchID };
            return await _dbCommonOperation.ReadSingleTable<SaleAllocationApprovalListDto>("[CM_SaleOrderAllocationApproval]", values);
        }
        #endregion

        #region Buy Order Approval
        public async Task<List<BuyAllocationApprovalListDto>> GetAllBuyOrderAllocation(string userName, int CompanyID, int BranchID)
        {
            var values = new { UserName = userName, CompanyID = CompanyID , BranchID=BranchID};
            return await _dbCommonOperation.ReadSingleTable<BuyAllocationApprovalListDto>("[CM_BuyOrderAllocationApproval]", values);
        }

        public async Task<List<SaleAllocationApprovalDetailListDto>> GetAllSaleOrderAllocationDetails(int SaleAllocationID)
        {
            var values = new { SaleAllocationID = SaleAllocationID };
            return await _dbCommonOperation.ReadSingleTable<SaleAllocationApprovalDetailListDto>("[CM_SaleOrderAllocationApprovalDetail]", values);
        }

        public async Task<List<BuyAllocationApprovalDetailListDto>> GetAllBuyOrderAllocationDetails(int BuyAllocationID)
        {
            var values = new { BuyAllocationID = BuyAllocationID };
            return await _dbCommonOperation.ReadSingleTable<BuyAllocationApprovalDetailListDto>("[CM_BuyOrderAllocationApprovalDetail]", values);
        }

        public Task<string> DeleteSellBuyOrderByBatch(string userName, int CompanyID, int BranchID, int OrderID, string OrderType)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", userName);
            SpParameters.Add("@OrderID", OrderID);
            SpParameters.Add("@OrderType", OrderType);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            return _dbCommonOperation.InsertUpdateBySP("CM_SaleBuyOrderRemove", SpParameters);
        }


        #endregion
    }
}
