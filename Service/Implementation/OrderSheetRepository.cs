using AutoMapper;
using Dapper;
using Model.DTOs.OrderSheet;
using Model.DTOs.Withdrawal;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class OrderSheetRepository : IOrderSheetRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public OrderSheetRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

        public async Task<List<ListOrdersheetDTO>> ListOrdersheetSL(int CompanyID, int BranchID, int PageNo, int Perpage, string FilterType, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, FilterType = FilterType, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListOrdersheetDTO>("CM_ListOrdersheetSL", values);
        }
        public async Task<List<ListOrdersheetDTO>> ListOrdersheetIL(int CompanyID, int BranchID, int PageNo, int Perpage, string FilterType, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, FilterType = FilterType, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListOrdersheetDTO>("CM_ListOrdersheetIL", values);
        }


        public async Task<OrderSheetDTO> GetOrdersheetDetailsSL(string UserName, int CompanyID, int BranchID, int ContractID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ContractID  = ContractID };
            var result =  await _dbCommonOperation.ReadSingleTable<OrderSheetDTO>("CM_GetOrdersheetSL", values);
            return result.FirstOrDefault();
        }
        public async Task<OrderSheetDTO> GetOrdersheetDetailsIL(string UserName, int CompanyID, int BranchID, int ContractID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ContractID = ContractID };
            var result = await _dbCommonOperation.ReadSingleTable<OrderSheetDTO>("CM_GetOrdersheetIL", values);
            return result.FirstOrDefault();
        }

        public async Task<string> UpdateOrdersheetSL(int companyId, int branchID, string userName, OrderSheetDTO entityDto)
        {

            try
            {

                #region Insert New Data

                string sp = "CM_UpdateOrdersheetSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@DocInventoryID", entityDto.DocInventoryID);
                SpParameters.Add("@ContractID", entityDto.ContractID);              
                SpParameters.Add("@DocumentID", entityDto.DocumentID);
                SpParameters.Add("@SL", entityDto.SL);
                SpParameters.Add("@CollectedSheet", entityDto.CollectedSheet);
                SpParameters.Add("@RemainingSheet", entityDto.RemainingSheet);
                SpParameters.Add("@CurrentStatus", "Collected");
                SpParameters.Add("@CollectionDate", Utility.DatetimeFormatter.DateFormat(entityDto.CollectionDate));
                SpParameters.Add("@StatusDate", "");
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateOrdersheetIL(int companyId, int branchID, string userName, OrderSheetDTO entityDto)
        {

            try
            {

                #region Insert New Data

                string sp = "CM_UpdateOrdersheetIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@DocInventoryID", entityDto.DocInventoryID);
                SpParameters.Add("@ContractID", entityDto.ContractID);
                SpParameters.Add("@DocumentID", entityDto.DocumentID);
                SpParameters.Add("@SL", entityDto.SL);
                SpParameters.Add("@CollectedSheet", entityDto.CollectedSheet);
                SpParameters.Add("@RemainingSheet", entityDto.RemainingSheet);
                SpParameters.Add("@CurrentStatus", "Collected");
                SpParameters.Add("@CollectionDate", Utility.DatetimeFormatter.DateFormat(entityDto.CollectionDate));
                SpParameters.Add("@StatusDate", "");
                SpParameters.Add("@Remarks", entityDto.Remarks);
                SpParameters.Add("@StoreLocation", entityDto.StoreLocation);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> OrdersheetApprovalSL(string userName, int CompanyID, int branchID, OrdersheetApprovalDTO approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@DocInventoryIDs", approvalDto.DocInventoryIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ApproveFrom", approvalDto.ApproveFrom);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveOrdersheetSL", SpParameters);
        }
        public async Task<string> OrdersheetApprovalIL(string userName, int CompanyID, int branchID, OrdersheetApprovalDTO approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@DocInventoryIDs", approvalDto.DocInventoryIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
             SpParameters.Add("@ApproveFrom", approvalDto.ApproveFrom);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveOrdersheetIL", SpParameters);
        }

        public async Task<List<OrderSheetPrintDTO>> GetListOrdersheetPrintSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID,  ProductID = ProductID, AccountNumber = AccountNumber };
            return await _dbCommonOperation.ReadSingleTable<OrderSheetPrintDTO>("CM_ListOrdersheetPrintSL", values);
        }
        public async Task<List<OrderSheetPrintDTO>> GetListOrdersheetPrintIL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNumber = AccountNumber };
            return await _dbCommonOperation.ReadSingleTable<OrderSheetPrintDTO>("CM_ListOrdersheetPrintIL", values);
        }

        public string GetOrdersheetPrintStringSL(string UserName, int companyID, int BranchID, List<OrderSheetPrintDTO> orderSheets)
        {

            string sp = "CM_UpdateOrdersheetPrintStatusSL";

            var data = ListtoDataTableConverter.ToDataTable(orderSheets);

            DataTable MemberAccInfo = new DataTable();
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", companyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ContractID", orderSheets[0].ContractID);
            SpParameters.Add("@ordersheets", ListtoDataTableConverter.ToDataTable(orderSheets).AsTableValuedParameter("Type_OrdersheetPrint"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var res =  _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            string htmlContent = "<table style='width:100%;>";
            htmlContent += "<thead style='font-weight:bold'>";
            htmlContent += "<tr>";
            htmlContent += "<th>Name of Securities</th>";
            htmlContent += "<th>ISIN</th>";
            htmlContent += "<th>Exchange</th>";
            htmlContent += "<th>Buy/Sell</th>";
            htmlContent += "<th>Quantity</th>";
            htmlContent += "<th>Rate</th>";
            htmlContent += "</tr>";
            htmlContent += "</thead>";

            htmlContent += "<tbody>";
            foreach (var item in orderSheets)
            {
                htmlContent += "<tr>";
                htmlContent += "<td style='text-align:center'>" + item.SecurityCode + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.ISIN + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.ExchangeName + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.TransactionType + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.TotalQty + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.Rate + "</th>";
                htmlContent += "</tr>";
            }

            htmlContent += "</tbody>";
            htmlContent += "</table>";

            return htmlContent;
        }
        public string GetOrdersheetPrintStringIL(string UserName, int companyID, int BranchID, List<OrderSheetPrintDTO> orderSheets)
        {

            string sp = "CM_UpdateOrdersheetPrintStatusIL";

            var data = ListtoDataTableConverter.ToDataTable(orderSheets);

            DataTable MemberAccInfo = new DataTable();
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", companyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ContractID", orderSheets[0].ContractID);
            SpParameters.Add("@ordersheets", ListtoDataTableConverter.ToDataTable(orderSheets).AsTableValuedParameter("Type_OrdersheetPrint"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var res = _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            string htmlContent = "<table style='width:100%;>";
            htmlContent += "<thead style='font-weight:bold'>";
            htmlContent += "<tr>";
            htmlContent += "<th>Name of Securities</th>";
            htmlContent += "<th>ISIN</th>";
            htmlContent += "<th>Exchange</th>";
            htmlContent += "<th>Buy/Sell</th>";
            htmlContent += "<th>Quantity</th>";
            htmlContent += "<th>Rate</th>";
            htmlContent += "</tr>";
            htmlContent += "</thead>";

            htmlContent += "<tbody>";
            foreach (var item in orderSheets)
            {
                htmlContent += "<tr>";
                htmlContent += "<td style='text-align:center'>" + item.SecurityCode + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.ISIN + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.ExchangeName + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.TransactionType + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.TotalQty + "</th>";
                htmlContent += "<td style='text-align:center'>" + item.Rate + "</th>";
                htmlContent += "</tr>";
            }

            htmlContent += "</tbody>";
            htmlContent += "</table>";

            return htmlContent;
        }


        //public async Task<string> UpdateOrdersheetPrintStatusSL(int companyId, int branchID, string userName, int ContractID)
        //{
        //    try
        //    {

        //        #region Insert New Data

        //        string sp = "CM_UpdateOrdersheetPrintStatusSL";

        //        DataTable MemberAccInfo = new DataTable();
        //        DynamicParameters SpParameters = new DynamicParameters();
        //        SpParameters.Add("@UserName", userName);
        //        SpParameters.Add("@CompanyID", companyId); 
        //        SpParameters.Add("@BranchID", branchID);
        //        SpParameters.Add("@ContractID", ContractID);
        //        SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

        //        return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

        //        #endregion

        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<ReleasedOrdersheetDTO> GetAvailableOrdersheetReleaseSL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNumber = AccountNumber };
            var result = await _dbCommonOperation.ReadSingleTable<ReleasedOrdersheetDTO>("CM_GetAvailableOrdersheetReleaseSL", values);
            return result.FirstOrDefault();
        }
        public async Task<ReleasedOrdersheetDTO> GetAvailableOrdersheetReleaseIL(string UserName, int CompanyID, int BranchID, int ProductID, string AccountNumber)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNumber = AccountNumber };
            var result = await _dbCommonOperation.ReadSingleTable<ReleasedOrdersheetDTO>("CM_GetAvailableOrdersheetReleaseIL", values);
            return result.FirstOrDefault();
        }


        public async Task<string> UpdateOrdersheetReleaseStatusSL(int companyId, int branchID, string userName, OrdersheetReleasedDTO releasedOrdersheet)
        {
            try
            {

                #region Insert New Data

                string sp = "CM_UpdateOrdersheetReleaseStatusSL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@DocInventoryID", releasedOrdersheet.DocInventoryID);
                SpParameters.Add("@NoOfReleasedSheet", releasedOrdersheet.NoOfReleasedSheet);
                SpParameters.Add("@Remarks", releasedOrdersheet.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateOrdersheetReleaseStatusIL(int companyId, int branchID, string userName, OrdersheetReleasedDTO releasedOrdersheet)
        {
            try
            {

                #region Insert New Data

                string sp = "CM_UpdateOrdersheetReleaseStatusIL";

                DataTable MemberAccInfo = new DataTable();
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", companyId);
                SpParameters.Add("@DocInventoryID", releasedOrdersheet.DocInventoryID);
                SpParameters.Add("@NoOfReleasedSheet", releasedOrdersheet.NoOfReleasedSheet);
                SpParameters.Add("@Remarks", releasedOrdersheet.Remarks);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ZeroOrdersheetListDTO>> GetListZeroOrdersheetSL(string UserName, int CompanyID, int BranchID, int ProductID, int NoOfRemainingSheet)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, NoOfRemainingSheet = NoOfRemainingSheet };
            return await _dbCommonOperation.ReadSingleTable<ZeroOrdersheetListDTO>("CM_ListZeroOrdersheetSL", values);
        }
        public async Task<List<ZeroOrdersheetListDTO>> GetListZeroOrdersheetIL(string UserName, int CompanyID, int BranchID, int ProductID, int NoOfRemainingSheet)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, NoOfRemainingSheet = NoOfRemainingSheet };
            return await _dbCommonOperation.ReadSingleTable<ZeroOrdersheetListDTO>("CM_ListZeroOrdersheetIL", values);
        }


    }
}
