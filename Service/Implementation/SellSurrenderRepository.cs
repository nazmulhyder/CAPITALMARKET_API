using Dapper;
using Model.DTOs.Approval;
using Model.DTOs.CMExchange;
using Model.DTOs.FDR;
using Model.DTOs.MarginRequest;
using Model.DTOs.Reports;
using Model.DTOs.SellSurrender;
using Model.DTOs.UnitFundCollectionDelivery;
using Model.DTOs.UnitPurchase;
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
    public class SellSurrenderRepository : ISellSurrenderRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public SellSurrenderRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

		public async Task<object> ListUnapprovedUnitPurchaseDetail(int CompanyID, int BranchID, int ContractID)
		{
			SqlParameter[] Params = new SqlParameter[3];

			Params[0] = new SqlParameter("@CompanyID", CompanyID);
			Params[1] = new SqlParameter("@BranchID", BranchID);
			Params[2] = new SqlParameter("@ContractID", ContractID);
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListUnapprovedUnitPurchaseDetail_AML]", Params);

			return new
			{
				FundCollectionList = DataSets.Tables[0],
			};
		}

		public async Task<object> GetSellSurrenderMinimumUnitSetup(int CompanyID, int BranchID, string userName, int FundID, string AccountType)
        {
			var values = new { UserName = userName, CompanyID = CompanyID, BranchID = BranchID, FundID = FundID, AccountType = AccountType };

			return _dbCommonOperation.ReadSingleTable<SS_MinimumUnitSetupDto>("CM_GetMFSellSurrenderMinUnitSetupAML", values).Result.ToList().FirstOrDefault();
		}


		public async Task<string> InsertUpdateMinimumUnitSetup(int CompanyID, int BranchID, string userName, SS_MinimumUnitSetupDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateMFSellSurrenderMinUnitSetupAML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@SellMFUnitTransRestrictionID", entry.SellMFUnitTransRestrictionID == null ? 0: entry.SellMFUnitTransRestrictionID);
                SpParameters.Add("@FundID", entry.FundID);
                SpParameters.Add("@SurrenderMFUnitTransRestrictionID", entry.SurrenderMFUnitTransRestrictionID == null ? 0: entry.SurrenderMFUnitTransRestrictionID);
                SpParameters.Add("@AccountType", entry.AccountType);
                SpParameters.Add("@MinimumUnit", entry.MinimumUnit);
                SpParameters.Add("@minimumSurrenderUnit", entry.MinimumSurrenderUnit);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> InsertUpdateUnitIssueForSale(int CompanyID, int BranchID, string userName, SS_UnitIssueForSaleDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateMFSellSurrenderUnitIssueForSaleAML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@MFUnitInventoryID", entry.MFUnitInventoryID);
                SpParameters.Add("@FundID", entry.FundID);
                SpParameters.Add("@IssuedUnit", entry.IssuedUnit);
                SpParameters.Add("@AllocatedUnit", entry.AllocatedUnit);
                SpParameters.Add("@Status", entry.Status);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetAllUnitIssueForSaleByFund(int CompanyID, int BranchID, string UserName, int FundID)
        {
         
			SqlParameter[] sqlParams = new SqlParameter[]
		   {
				new SqlParameter("@CompanyID",  CompanyID),
				new SqlParameter("@BranchID",  BranchID),
				new SqlParameter("@FundID",  FundID),
		   };

            //return await _dbCommonOperation.ReadSingleTable<SS_UnitIssueForSaleDto>("CM_QueryMFSellSurrenderUnitIssueForSaleAML", values);
            var set = _dbCommonOperation.FindMultipleDataSetBySP("CM_QueryMFSellSurrenderUnitIssueForSaleAML", sqlParams);

            return set.Tables[0];
        }

        public async Task<string> InsertUpdateUnitPurchaseDetailSetup(int CompanyID, int BranchID, string userName, UnitPurchaseDto entry)
        {
            try
            {

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);

                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@UnitPrice", entry.CurrentNav);
                SpParameters.Add("@PurchaseUnit", entry.PurchaseUnit);
                SpParameters.Add("@SaleNo", entry.SaleNo);
                SpParameters.Add("@SalesRMID", entry.SalesRMID);
                SpParameters.Add("@SaleType", entry.SaleType);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertUpdateUnitPurchaseDetailAML", SpParameters);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetCustomerInfoForPurchase(int CompanyID, int BranchID, string UserName, int ContractID)
        {
			SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@UserName", UserName);
			Params[1] = new SqlParameter("@CompanyID", CompanyID);
			Params[2] = new SqlParameter("@BranchID", BranchID);
			Params[3] = new SqlParameter("@ContractID", ContractID);
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListFundCollectionForPurchaseAML]", Params);
            return DataSets.Tables[0];

        }

        public async Task<object> ListUnitPurchaseDetail(int CompanyID, int BranchID, SurrenderFilterDto purchaseFilter)
        {
            SqlParameter[] Params = new SqlParameter[7];

            Params[0] = new SqlParameter("@CompanyID", CompanyID);
            Params[1] = new SqlParameter("@BranchID", BranchID);
            Params[2] = new SqlParameter("@ProductID", purchaseFilter.ProductID);
            Params[3] = new SqlParameter("@DateFrom", Utility.DatetimeFormatter.DateFormat(purchaseFilter.FromDate));
            Params[4] = new SqlParameter("@DateTo", Utility.DatetimeFormatter.DateFormat(purchaseFilter.ToDate));
            Params[5] = new SqlParameter("@AccountNumber", purchaseFilter.AccountNumber);
            Params[6] = new SqlParameter("@ListType", purchaseFilter.ListType);
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListUnitPurchaseDetail_AML]", Params);

            return DataSets.Tables[0];
        }

        public async Task<object> GetQueryUnitPurchaseDetail(int CompanyID, int BranchID, string UserName,  int PurchaseDetailID)
        {
			SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@CompanyID", CompanyID);
			Params[1] = new SqlParameter("@BranchID", BranchID);
			Params[2] = new SqlParameter("@UserName", UserName);
			Params[3] = new SqlParameter("@PurchaseDetailID", PurchaseDetailID);
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[QueryUnitPurchaseDetail]", Params);

			return DataSets.Tables[0];
        }

        public async Task<string> UnitPurchaseDetailApproval(string userName, int CompanyID, int branchID, ApproveUnitPurchaseDetailDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@PurchaseDetailIDs", approvalDto.PurchaseDetailIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveUnitPurchaseDetailAML", SpParameters);
        }

        public async Task<object> ListUnitActivationRequestAML(int CompanyID, int BranchID, string UserName, int ProductID)
        {
			SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@CompanyID", CompanyID);
			Params[1] = new SqlParameter("@BranchID", BranchID);
			Params[2] = new SqlParameter("@UserName", UserName);
			Params[3] = new SqlParameter("@ProductID", ProductID);
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListUnitActivationRequestAML]", Params);

			return DataSets.Tables[0];

			//var values = new { CompanyID = CompanyID, BranchID = BranchID, UserName = UserName, ProductID = sS_Activation.ProductID.GetValueOrDefault(), PurchaseDateFrom = Utility.DatetimeFormatter.DateFormat(sS_Activation.PurchaseDateFrom), PurchaseDateTo = Utility.DatetimeFormatter.DateFormat(sS_Activation.PurchaseDateTo)};
            // return await _dbCommonOperation.ReadSingleTable<SS_UnitPurchaseDetailSetupDto>("ListUnitActivationRequestAML", values);
        }

        public async Task<object> InsertUnitActivation(int CompanyID, int BranchID, string userName, string PurchaseDetailIDs)
        {
            try
            {
                string sp = "CM_InsertUnitActivationAML";
			
				SqlParameter[] Params = new SqlParameter[4];
				Params[0] = new SqlParameter("@UserName", userName);
                Params[1] = new SqlParameter("@CompanyID", CompanyID);
                Params[2] = new SqlParameter("@BranchID", BranchID);
				Params[3] = new SqlParameter("@PurchaseDetailIDs", PurchaseDetailIDs);

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP(sp, Params);

				CDBLPayinFileDto fileData = new CDBLPayinFileDto();

				fileData = CustomConvert.DataSetToList<CDBLPayinFileDto>(DataSets.Tables[0]).ToList().FirstOrDefault();
                fileData.ContentList = CustomConvert.DataSetToList<CDBLPayinFileContentDto>(DataSets.Tables[1]).ToList();


				//var fileData = _service.sellSurrenderRepository.getAllCDBLData(CompanyID, BranchID, userName, PurchaseDetailIDs);

				

				StringBuilder srBuilder = new StringBuilder();
				srBuilder.Append(fileData.ControlRecord);
				srBuilder.Append(Environment.NewLine);
				srBuilder.Append(fileData.FirstLine);
				srBuilder.Append(Environment.NewLine);
				srBuilder.Append(fileData.SecondLine);
				srBuilder.Append(Environment.NewLine);
				srBuilder.Append(fileData.ThirdLine);
				foreach (var Item in fileData.ContentList)
				{
					srBuilder.Append(Environment.NewLine);
					srBuilder.Append(Item.FourthLine);

				}
				return new
				{
					FileName = fileData.FileName,
					FileContent = srBuilder.ToString()
				};


			}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public CDBLFileDataDto getAllCDBLData(int ComID,int BrnchID, string UserName, string PurchaseDetailIDs)
        //{
        //    CDBLFileDataDto cDBLFileData = new CDBLFileDataDto();

        //    SqlParameter[] sqlParams = new SqlParameter[]
        //    {
        //        new SqlParameter("@UserName", UserName),
        //        new SqlParameter("@CompanyID",  ComID),
        //        new SqlParameter("@BranchID",  BrnchID),
        //        new SqlParameter("@PurchaseDetailIDs",  PurchaseDetailIDs),
        //    };

        //    var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GenrateCDBLTransferTxtFileAML]", sqlParams);

        //    cDBLFileData.FileName = DataSets.Tables[0].Rows[0][0].ToString();
        //    cDBLFileData.FileContent_ControlRecord = DataSets.Tables[1].Rows[0][0].ToString();
        //    cDBLFileData.FileContent_Line1 = DataSets.Tables[2].Rows[0][0].ToString();
        //    cDBLFileData.FileContent_Line2 = DataSets.Tables[3].Rows[0][0].ToString(); ;
        //    cDBLFileData.FileContent_Line3 = DataSets.Tables[4].Rows[0][0].ToString();
        //    cDBLFileData.Content = CustomConvert.DataSetToList<ContenDto>(DataSets.Tables[5]).ToList();

        //    return cDBLFileData;
        //}

        public async Task<object> GetGetListOfUnitRequestActivationAML(int CompanyID, int BranchID, string UserName, int ProductID)
        {
			SqlParameter[] sqlParams = new SqlParameter[]
			{
				
				new SqlParameter("@CompanyID",  CompanyID),
				new SqlParameter("@BranchID",  BranchID),
				new SqlParameter("@UserName", UserName),
				new SqlParameter("@ProductID", ProductID),
			};

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[GetListOfUnitRequestActivationAML]", sqlParams);

            return DataSets.Tables[0];
			//var values = new { CompanyID = CompanyID, BranchID = BranchID, UserName = UserName, ProductID = ProductID };
			// return await _dbCommonOperation.ReadSingleTable<SS_UnitPurchaseDetailSetupDto>("GetListOfUnitRequestActivationAML", values);

		}

        public async Task<string> UpdateUnitActivation(int CompanyID, int BranchID, string userName, SS_UpdateActivateRequestDto sS_UpdateActivateReq)
        {
            try
            {
                string sp = "CM_UpdateUnitActivationAML";
                
                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UnitActivationIDs", sS_UpdateActivateReq.UnitActivationIDs);
                SpParameters.Add("@Action", sS_UpdateActivateReq.Action);
                SpParameters.Add("@CancelReason", sS_UpdateActivateReq.CancelReason);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> GetQueryUnitSurrenderAML(int CompanyID, int BranchID, string UserName, string AccNo, int ProductID)
        {
            SqlParameter[] Params = new SqlParameter[5];

            Params[0] = new SqlParameter("@CompanyID", CompanyID);
            Params[1] = new SqlParameter("@BranchID", BranchID);
            Params[2] = new SqlParameter("@ProductID", ProductID);
            Params[3] = new SqlParameter("@AccountNo", AccNo);
            Params[4] = new SqlParameter("@UserName", UserName);
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[QueryUnitSurrenderAML]", Params);

            return new
            {
                UnitSurrenderInfo = DataSets.Tables[0],
                ExitLoad = DataSets.Tables[1]
            };
        }
        public async Task<string> UpdateUnitSurrenderSetup(int CompanyID, int BranchID, string userName, SS_UnitSurrenderDto entry)
        {
            try
            {
                string sp = "CM_InsertUpdateUnitSurrenderAML";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@SurrenderDetailID", entry.SurrenderDetailID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@InstrumentID", entry.InstrumentID);
                SpParameters.Add("@SurrenderUnit", entry.SurrenderUnit);
                SpParameters.Add("@SurrenderPrice", entry.SurrenderPrice);
                SpParameters.Add("@ExitLoadValue", entry.ExitLoadValue);
                SpParameters.Add("@NetSurrenderValue", entry.NetSurrenderValue); 
                SpParameters.Add("@SurrenderNo", entry.SurrenderNo);
                SpParameters.Add("@RequestSource", entry.RequestSource);
                SpParameters.Add("@RetrievePurchaseDate", Utility.DatetimeFormatter.DateFormat(entry.RetrievePurchaseDate));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SS_UnitSurrenderDto>> ListUnitSurrenderAML(int CompanyID, int BranchID, string UserName, SurrenderFilterDto surrenderFilter)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, UserName = UserName, ProductID = surrenderFilter.ProductID,
                FromDate = Utility.DatetimeFormatter.DateFormat(surrenderFilter.FromDate),
                ToDate = Utility.DatetimeFormatter.DateFormat(surrenderFilter.ToDate),
                AccountNumber = surrenderFilter.AccountNumber,
                ListType = surrenderFilter.ListType
            };
            return await _dbCommonOperation.ReadSingleTable<SS_UnitSurrenderDto>("ListUnitSurrenderAML", values);
        }

        public async Task<string> UnitSurrenderApproval(string userName, int CompanyID, int branchID, ApproveUnitSurrenderDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@SurrenderDetailIDs", approvalDto.SurrenderDetailIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveUnitSurrenderAML", SpParameters);
        }

        public async Task<SS_UnitSurrenderDto> GetUnitSurrenderDetailAML(int CompanyID, int BranchID, string UserName, int SurrenderDetailID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, UserName = UserName, SurrenderDetailID = SurrenderDetailID };
            var result = await _dbCommonOperation.ReadSingleTable<SS_UnitSurrenderDto>("GetUnitSurrenderDetailAML", values);
            return result.FirstOrDefault();
        }

        public async Task<List<SS_UnitSurrenderDto>> GetAllUnitSurrenderApprovalListAML(int CompanyID, int BranchID, string UserName, int ProductID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, UserName = UserName , ProductID = ProductID };
            return await _dbCommonOperation.ReadSingleTable<SS_UnitSurrenderDto>("GetAllUnitSurrenderApprovalListAML", values);
        }

        public async Task<object> FundCollectionForPurchaseAML(int CompanyID, int BranchID, string UserName)
        {
            SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@UserName", UserName);
			Params[1] = new SqlParameter("@CompanyID", CompanyID);
            Params[2] = new SqlParameter("@BranchID", BranchID);
            Params[3] = new SqlParameter("@ContractID", "0");
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListFundCollectionForPurchaseAML]", Params);

            return DataSets.Tables[0];

		}

    
    }
}
