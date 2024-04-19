using Dapper;
using Model.DTOs.Allocation;
using Model.DTOs.TradeRestriction;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class TradeRestrictionRepository : ITradeRestrictionRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TradeRestrictionRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        #region Trade Restriction [Product]

        public async Task<List<ListTradeRestrictionProductDto>> GetAllTradeRestrictionProduct(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionProductDto>("[CM_ListTradeRestrictionOnProduct]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TradeApprovalRestrictionProductDto> QueryTradeRestrictionProduct(int CompanyID, int BranchID, int ProductID, string UserName)
        {
            TradeApprovalRestrictionProductDto tradeRestrictionpProduct = new TradeApprovalRestrictionProductDto();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@ProductID", ProductID),
                new SqlParameter("@UserName",  UserName),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryTradeRestrictionOnProduct]", sqlParams);

            tradeRestrictionpProduct = CustomConvert.DataSetToList<TradeApprovalRestrictionProductDto>(DataSets.Tables[0]).FirstOrDefault();
            tradeRestrictionpProduct.productTransactionProfile = CustomConvert.DataSetToList<ProductTransactionProfileDto>(DataSets.Tables[1]).FirstOrDefault();
            tradeRestrictionpProduct.Buy_InsGrpsList = CustomConvert.DataSetToList<ProductTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpProduct.Sell_InsGrpsList = CustomConvert.DataSetToList<ProductTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpProduct.Buy_RestrictionInstruments = CustomConvert.DataSetToList<ProductRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpProduct.Sell_RestrictionInstruments = CustomConvert.DataSetToList<ProductRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpProduct.Sell_RestrictionSectors = CustomConvert.DataSetToList<ProductRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpProduct.Buy_RestrictionSectors = CustomConvert.DataSetToList<ProductRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpProduct.ApprovalDetail = CustomConvert.DataSetToList<ApprovalDto>(DataSets.Tables[5]).FirstOrDefault();

            return tradeRestrictionpProduct;
        }

        public async Task<string> AddUpdate(int CompanyID, int BranchID, TradeRestrictionpProductDto entityDto, string userName)
        {
            try
            {
                List<ProductTradeAllowedInsGrpDto> productTradeAllowedInsGrps = new List<ProductTradeAllowedInsGrpDto>();
                productTradeAllowedInsGrps.AddRange(entityDto.Buy_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());
                productTradeAllowedInsGrps.AddRange(entityDto.Sell_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());

                List<ProductRestrictionInsDto> productRestrictionInstruments = new List<ProductRestrictionInsDto>();
                productRestrictionInstruments.AddRange(entityDto.Buy_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());
                productRestrictionInstruments.AddRange(entityDto.Sell_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());

                List<ProductRestrictionSectorDto> productRestrictionSectors = new List<ProductRestrictionSectorDto>();
                productRestrictionSectors.AddRange(entityDto.Buy_RestrictionSectors.Where(x => x.SectorID > 0).ToList());
                productRestrictionSectors.AddRange(entityDto.Sell_RestrictionSectors.Where(x => x.SectorID > 0).ToList());

                #region Insert New Data

                if (entityDto.productTransactionProfile.ProdTransactionProfileID == 0 || entityDto.productTransactionProfile.ProdTransactionProfileID == null)
                {
                    string sp = "CM_InsertTradeRestrictionOnProduct";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@ProductID", entityDto.productTransactionProfile.ProductID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.productTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.productTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@IsFundPollingAllowed", entityDto.productTransactionProfile.IsFundPollingAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.productTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.productTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@productTradeAllowedInsGrps", ListtoDataTableConverter.ToDataTable(productTradeAllowedInsGrps).AsTableValuedParameter("Type_ProductTradeAllowedInsGrp"));
                    SpParameters.Add("@productRestrictionInstruments", ListtoDataTableConverter.ToDataTable(productRestrictionInstruments).AsTableValuedParameter("Type_ProductRestrictionIns"));
                    SpParameters.Add("@productRestrictionSectors", ListtoDataTableConverter.ToDataTable(productRestrictionSectors).AsTableValuedParameter("Type_ProductRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateTradeRestrictionOnProduct";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@ProdTransactionProfileID", entityDto.productTransactionProfile.ProdTransactionProfileID);
                    SpParameters.Add("@ProductID", entityDto.productTransactionProfile.ProductID);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.productTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.productTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@IsFundPollingAllowed", entityDto.productTransactionProfile.IsFundPollingAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.productTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.productTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@productTradeAllowedInsGrps", ListtoDataTableConverter.ToDataTable(productTradeAllowedInsGrps).AsTableValuedParameter("Type_ProductTradeAllowedInsGrp"));
                    SpParameters.Add("@productRestrictionInstruments", ListtoDataTableConverter.ToDataTable(productRestrictionInstruments).AsTableValuedParameter("Type_ProductRestrictionIns"));
                    SpParameters.Add("@productRestrictionSectors", ListtoDataTableConverter.ToDataTable(productRestrictionSectors).AsTableValuedParameter("Type_ProductRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListTradeRestrictionApprovalProductDto>> GetAllTradeRestrictionApprovalOnProduct(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionApprovalProductDto>("[CM_ApprovalTrdRestrictionListOnProduct]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Trade Restriction [Account Group]
        public async Task<List<ListTradeRestrictionAccGrpDto>> GetAllTradeRestrictionAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionAccGrpDto>("[CM_ListTradeRestrictionOnAccGrp]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddUpdateTradeRestrictionAccGrp(int CompanyID, int BranchID, TradeRestrictionAccGrpDto entityDto, string userName)
        {
            try
            {
                List<AccGrpTradeAllowedInsGrpDto> accGrpTradeAllowedInsGrps = new List<AccGrpTradeAllowedInsGrpDto>();
                accGrpTradeAllowedInsGrps.AddRange(entityDto.Buy_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());
                accGrpTradeAllowedInsGrps.AddRange(entityDto.Sell_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());

                List<AccGrpRestrictionInsDto> accGrpRestrictionInstruments = new List<AccGrpRestrictionInsDto>();
                accGrpRestrictionInstruments.AddRange(entityDto.Buy_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());
                accGrpRestrictionInstruments.AddRange(entityDto.Sell_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());

                List<AccGrpRestrictionSectorDto> accGrpRestrictionSectors = new List<AccGrpRestrictionSectorDto>();
                accGrpRestrictionSectors.AddRange(entityDto.Buy_RestrictionSectors.Where(x => x.SectorID > 0).ToList());
                accGrpRestrictionSectors.AddRange(entityDto.Sell_RestrictionSectors.Where(x => x.SectorID > 0).ToList());

                #region Insert New Data

                if (entityDto.AccGrpTransactionProfile.AccGrpTransactionProfileID == 0 || entityDto.AccGrpTransactionProfile.AccGrpTransactionProfileID == null)
                {
                    string sp = "CM_InsertTradeRestrictionOnAccGrp";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@AccountGroupID", entityDto.AccGrpTransactionProfile.AccountGroupID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.AccGrpTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.AccGrpTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.AccGrpTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.AccGrpTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@AccGrpTradeAllowedInsGrps", ListtoDataTableConverter.ToDataTable(accGrpTradeAllowedInsGrps).AsTableValuedParameter("Type_AccGrpTradeAllowedInsGrp"));
                    SpParameters.Add("@AccGrpRestrictionInstruments", ListtoDataTableConverter.ToDataTable(accGrpRestrictionInstruments).AsTableValuedParameter("Type_AccGrpTradeRestrictionIns"));
                    SpParameters.Add("@AccGrpRestrictionSectors", ListtoDataTableConverter.ToDataTable(accGrpRestrictionSectors).AsTableValuedParameter("Type_AccGrpRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateTradeRestrictionOnAccGrp";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@AccGrpTransactionProfileID", entityDto.AccGrpTransactionProfile.AccGrpTransactionProfileID);
                    SpParameters.Add("@AccountGroupID", entityDto.AccGrpTransactionProfile.AccountGroupID);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.AccGrpTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.AccGrpTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.AccGrpTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.AccGrpTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@AccGrpTradeAllowedInsGrps", ListtoDataTableConverter.ToDataTable(accGrpTradeAllowedInsGrps).AsTableValuedParameter("Type_AccGrpTradeAllowedInsGrp"));
                    SpParameters.Add("@AccGrpRestrictionInstruments", ListtoDataTableConverter.ToDataTable(accGrpRestrictionInstruments).AsTableValuedParameter("Type_AccGrpTradeRestrictionIns"));
                    SpParameters.Add("@AccGrpRestrictionSectors", ListtoDataTableConverter.ToDataTable(accGrpRestrictionSectors).AsTableValuedParameter("Type_AccGrpRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TradeApprovalRestrictionAccGrpDto> QueryTradeRestrictionAccGrp(int CompanyID, int BranchID, int AccountGroupID, string UserName)
        {
            TradeApprovalRestrictionAccGrpDto tradeRestrictionpAccGrp = new TradeApprovalRestrictionAccGrpDto();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@AccountGroupID", AccountGroupID),
                new SqlParameter("@UserName",  UserName),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryTradeRestrictionOnAccGrp]", sqlParams);

            tradeRestrictionpAccGrp = CustomConvert.DataSetToList<TradeApprovalRestrictionAccGrpDto>(DataSets.Tables[0]).FirstOrDefault();
            tradeRestrictionpAccGrp.AccGrpTransactionProfile = CustomConvert.DataSetToList<AccGrpTransactionProfileDto>(DataSets.Tables[1]).FirstOrDefault();
            tradeRestrictionpAccGrp.Buy_InsGrpsList = CustomConvert.DataSetToList<AccGrpTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpAccGrp.Sell_InsGrpsList = CustomConvert.DataSetToList<AccGrpTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpAccGrp.Buy_RestrictionInstruments = CustomConvert.DataSetToList<AccGrpRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpAccGrp.Sell_RestrictionInstruments = CustomConvert.DataSetToList<AccGrpRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpAccGrp.Buy_RestrictionSectors = CustomConvert.DataSetToList<AccGrpRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionpAccGrp.Sell_RestrictionSectors = CustomConvert.DataSetToList<AccGrpRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionpAccGrp.ApprovalDetail = CustomConvert.DataSetToList<ApprovalDto>(DataSets.Tables[5]).FirstOrDefault();

            return tradeRestrictionpAccGrp;
        }


        public async Task<List<ListTradeRestrictionApprovalAccGrpDto>> GetAllTradeRestrictionApprovalOnAccGrp(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionApprovalAccGrpDto>("[CM_ApprovalTrdRestrictionListOnAccGrp]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Trade Restriction[Account]
        public async Task<List<ListTradeRestrictionAccountDto>> GetAllTradeRestrictionAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionAccountDto>("[CM_ListTradeRestrictionOnAgreement]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddUpdateTradeRestrictionAccount(int CompanyID, int BranchID, TradeRestrictionAccountDto entityDto, string userName)
        {
            try
            {
                List<AccountTradeAllowedInsGrpDto> accountTradeAllowedInsGrps = new List<AccountTradeAllowedInsGrpDto>();
                accountTradeAllowedInsGrps.AddRange(entityDto.Buy_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());
                accountTradeAllowedInsGrps.AddRange(entityDto.Sell_InsGrpsList.Where(x => x.InstrumentGroupID > 0).ToList());

                List<AccountRestrictionInsDto> accountRestrictionInstruments = new List<AccountRestrictionInsDto>();
                accountRestrictionInstruments.AddRange(entityDto.Buy_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());
                accountRestrictionInstruments.AddRange(entityDto.Sell_RestrictionInstruments.Where(x => x.InstrumentID > 0).ToList());


                List<AccountRestrictionSectorDto> accRestrictionSectors = new List<AccountRestrictionSectorDto>();
                accRestrictionSectors.AddRange(entityDto.Buy_RestrictionSectors.Where(x => x.SectorID > 0).ToList());
                accRestrictionSectors.AddRange(entityDto.Sell_RestrictionSectors.Where(x => x.SectorID > 0).ToList());

                #region Insert New Data

                if (entityDto.AccountTransactionProfile.TransactionProfileID == 0 || entityDto.AccountTransactionProfile.TransactionProfileID == null)
                {
                    string sp = "CM_InsertTradeRestrictionOnAgreement";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@ContractID", entityDto.AccountTransactionProfile.ContractID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.AccountTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.AccountTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@IsFundPollingAllowed", entityDto.AccountTransactionProfile.IsFundPollingAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.AccountTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.AccountTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@AgrTradeAllowedInsGrp", ListtoDataTableConverter.ToDataTable(accountTradeAllowedInsGrps).AsTableValuedParameter("Type_AgrTradeAllowedInsGrp"));
                    SpParameters.Add("@AgrTradeIns", ListtoDataTableConverter.ToDataTable(accountRestrictionInstruments).AsTableValuedParameter("Type_AgrTradeIns"));
                    SpParameters.Add("@AgrRestrictionSectors", ListtoDataTableConverter.ToDataTable(accRestrictionSectors).AsTableValuedParameter("Type_AgrRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateTradeRestrictionOnAgreement";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@AgrTransactionProfileID", entityDto.AccountTransactionProfile.TransactionProfileID);
                    SpParameters.Add("@ContractID", entityDto.AccountTransactionProfile.ContractID);
                    SpParameters.Add("@CompanyID", CompanyID);
                    SpParameters.Add("@BranchID", BranchID);
                    SpParameters.Add("@IsBuyAllowed", entityDto.AccountTransactionProfile.IsBuyAllowed);
                    SpParameters.Add("@IsSellAllowed", entityDto.AccountTransactionProfile.IsSellAllowed);
                    SpParameters.Add("@IsFundPollingAllowed", entityDto.AccountTransactionProfile.IsFundPollingAllowed);
                    SpParameters.Add("@BuyRestrictionRemarks", entityDto.AccountTransactionProfile.BuyRestrictionRemarks);
                    SpParameters.Add("@SellRestrictionRemarks", entityDto.AccountTransactionProfile.SellRestrictionRemarks);
                    SpParameters.Add("@AgrTradeAllowedInsGrp", ListtoDataTableConverter.ToDataTable(accountTradeAllowedInsGrps).AsTableValuedParameter("Type_AgrTradeAllowedInsGrp"));
                    SpParameters.Add("@AgrTradeIns", ListtoDataTableConverter.ToDataTable(accountRestrictionInstruments).AsTableValuedParameter("Type_AgrTradeIns"));
                    SpParameters.Add("@AgrRestrictionSectors", ListtoDataTableConverter.ToDataTable(accRestrictionSectors).AsTableValuedParameter("Type_AgrRestrictionSecor"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                    SpParameters.Add("@UserName ", userName);
                    return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TradeRestrictionAccountDto> QueryTradeRestrictionAccount(int CompanyID, int BranchID, int ContractID, string UserName)
        {
            TradeRestrictionAccountDto tradeRestrictionAccount = new TradeRestrictionAccountDto();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@ContractID", ContractID),
                new SqlParameter("@UserName",  UserName),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryTradeRestrictionOnAgreement]", sqlParams);

            tradeRestrictionAccount = CustomConvert.DataSetToList<TradeRestrictionAccountDto>(DataSets.Tables[0]).FirstOrDefault();
            tradeRestrictionAccount.AccountTransactionProfile = CustomConvert.DataSetToList<AccountTransactionProfileDto>(DataSets.Tables[1]).FirstOrDefault();
            tradeRestrictionAccount.Buy_InsGrpsList = CustomConvert.DataSetToList<AccountTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionAccount.Sell_InsGrpsList = CustomConvert.DataSetToList<AccountTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionAccount.Buy_RestrictionInstruments = CustomConvert.DataSetToList<AccountRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionAccount.Sell_RestrictionInstruments = CustomConvert.DataSetToList<AccountRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeRestrictionAccount.Buy_RestrictionSectors = CustomConvert.DataSetToList<AccountRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeRestrictionAccount.Sell_RestrictionSectors = CustomConvert.DataSetToList<AccountRestrictionSectorDto>(DataSets.Tables[4]).Where(x => x.TransactionType.ToLower() == "sell").ToList();



            return tradeRestrictionAccount;
        }
        public async Task<TradeApprovalRestrictionAccountDto> QueryApprovalTradeRestrictionAccount(int CompanyID, int BranchID, int ContractID, string UserName)
        {
            TradeApprovalRestrictionAccountDto tradeApprovalRestrictionAccount = new TradeApprovalRestrictionAccountDto();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@ContractID", ContractID),
                new SqlParameter("@UserName",  UserName),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryApprovalTradeRestrictionOnAgreement]", sqlParams);

            tradeApprovalRestrictionAccount = CustomConvert.DataSetToList<TradeApprovalRestrictionAccountDto>(DataSets.Tables[0]).FirstOrDefault();
            tradeApprovalRestrictionAccount.AccountTransactionProfile = CustomConvert.DataSetToList<AccountTransactionProfileDto>(DataSets.Tables[1]).FirstOrDefault();
            tradeApprovalRestrictionAccount.Buy_InsGrpsList = CustomConvert.DataSetToList<AccountTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeApprovalRestrictionAccount.Sell_InsGrpsList = CustomConvert.DataSetToList<AccountTradeAllowedInsGrpDto>(DataSets.Tables[2]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeApprovalRestrictionAccount.Buy_RestrictionInstruments = CustomConvert.DataSetToList<AccountRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "buy").ToList();
            tradeApprovalRestrictionAccount.Sell_RestrictionInstruments = CustomConvert.DataSetToList<AccountRestrictionInsDto>(DataSets.Tables[3]).Where(x => x.TransactionType.ToLower() == "sell").ToList();
            tradeApprovalRestrictionAccount.ApprovalDetail = CustomConvert.DataSetToList<ApprovalDto>(DataSets.Tables[4]).FirstOrDefault();


            return tradeApprovalRestrictionAccount;
        }
        public async Task<List<ListTradeRestrictionApprovalAccountDto>> GetAllTradeRestrictionApprovalOnAccount(int CompanyID, int BranchID, int PageNo, int PerPage, string SearchKeyword)
        {
            try
            {
                var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = PerPage, SearchKeyword = SearchKeyword };
                return await _dbCommonOperation.ReadSingleTable<ListTradeRestrictionApprovalAccountDto>("[CM_ApprovalTrdRestrictionListOnAgreement]", values);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        public async Task<string> RestrictionApproval(string userName, int CompanyID, RestrictionApprovalDto approvalDto)
        {

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@userName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@RestrictionID", approvalDto.RestrictionID);
                SpParameters.Add("@RestrictionType", approvalDto.RestrictionType);
                SpParameters.Add("@IsApproved", approvalDto.IsApproved);
                SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveTradeRestriction", SpParameters);

        }

    }
}
