using AutoMapper;
using Dapper;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Model.DTOs.AMLMF;
using Model.DTOs.CoA;
using Model.DTOs.FDR;
using Model.DTOs.FundOperation;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
	internal class FundOperationRepository : IFundOperationRepository
	{
		public readonly IDBCommonOpService _dbCommonOperation;
		
		public IMapper mapper;
		
		public FundOperationRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
		{
			_dbCommonOperation = dbCommonOperation;
			mapper = _mapper;
		}

		public async Task<object> AMLMFLockInInformationDetailForNewEntry(string UserName, int CompanyID, int BranchID, int FundID, string InvestmentType)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@InvestmentType", InvestmentType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFLockInInformationDetail]", sqlParams);

			var fund = CustomConvert.DataSetToList<AMLMFLockInInformationDto>(DataSets.Tables[0]).FirstOrDefault();
			fund.SlabList = CustomConvert.DataSetToList<AMLMFLockInInformationSlabDto>(DataSets.Tables[1]).ToList();

			return fund;
		}

		#region DUE_PAYEMENT

		public async Task<object> ApproveFundDuePayment(string UserName, int CompanyID, int BranchID, FundDuePaymenApproveDto data)
		{
			SqlParameter[] sqlParams = new SqlParameter[6];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ChrgDuePaymentIDs", data.ChrgDuePaymentIDs);
			sqlParams[4] = new SqlParameter("@ApprovalStatus", data.ApprovalStatus);
			sqlParams[5] = new SqlParameter("@Remark", data.Remark);


			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ApproveAMLMFDuePayment]", sqlParams);

			return DataSets.Tables[0].Rows[0][0];
		}

		public async Task<object> GetFundDuePaymentDetail(string UserName, int CompanyID, int BranchID, int ChrgDuePaymentID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ChrgDuePaymentID", ChrgDuePaymentID);


			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFDuePaymentDetail]", sqlParams);

			AMLMFDuePaymentDto duePayment = CustomConvert.DataSetToList<AMLMFDuePaymentDto>(DataSets.Tables[0]).FirstOrDefault();

			duePayment.ChargeTransactionList = CustomConvert.DataSetToList<AMLMFDuePaymentChargesDto>(DataSets.Tables[1]).ToList();

			return duePayment;
		}


		public async Task<object> GetFundDuePaymentList(string UserName, int CompanyID, int BranchID, int FundID, DateTime TenorStartDate, DateTime TenorEndDate, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[7];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@TenorStartDate", TenorStartDate);
			sqlParams[5] = new SqlParameter("@TenorEndDate", TenorEndDate);
			sqlParams[6] = new SqlParameter("@ListType", ListType);
		

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFDuepayment]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> SaveAMLMFDuePayment(string UserName, int CompanyID, int BranchID, AMLMFDuePaymentDto data)
		{
			string Summaryid = "";

			foreach(var item in data.ChargeTransactionList)
			{
				Summaryid = Summaryid + "," + item.Summaryid.ToString();
			}

			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@TenorStartDate", Utility.DatetimeFormatter.DateFormat(data.TenorStartDate));
			SpParameters.Add("@TenorEndDate", Utility.DatetimeFormatter.DateFormat(data.TenorEndDate));
			SpParameters.Add("@TotalCharge", data.TotalCharge);
			SpParameters.Add("@TotalTax", data.TotalTax);
			SpParameters.Add("@TotalVat", data.TotalVat);
			
			SpParameters.Add("@AgreementChargeID", data.AgreementChargeID);
			SpParameters.Add("@Summaryid", Summaryid);
			
			SpParameters.Add("@BankAccountID", data.BankAccountID);
			SpParameters.Add("@PaymentMethod", data.PaymentMode);
			
			SpParameters.Add("@FundID", data.FundID);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAMLMFDuePaymentDetail", SpParameters);
		}

		public async Task<object> GetFundDueListForPayment(string UserName, int CompanyID, int BranchID, int FundID, int AgreementChargeID, DateTime TenorStartDate, DateTime TenorEndDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[7];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@AgreementChargeID", AgreementChargeID);
			sqlParams[5] = new SqlParameter("@TenorStartDate", TenorStartDate);
			sqlParams[6] = new SqlParameter("@TenorStartEnd", TenorEndDate);

			
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFDueForpayment]", sqlParams);

			AMLMFDuePaymentDto aMLMFDuePaymentDto = new AMLMFDuePaymentDto
			{
				FundID= FundID,
				AgreementChargeID= AgreementChargeID,
				TenorStartDate= Utility.DatetimeFormatter.ConvertDatetimeToStringDDMMYYY(TenorStartDate),
				TenorEndDate= Utility.DatetimeFormatter.ConvertDatetimeToStringDDMMYYY(TenorEndDate) ,
				ChargeTransactionList = CustomConvert.DataSetToList<AMLMFDuePaymentChargesDto>(DataSets.Tables[0]).ToList()
		};

			return aMLMFDuePaymentDto;
		}
	
		
		#endregion DUE_PAYEMENT

		public async Task<object> GetFundChargeForDueAndAdvancePayment(string UserName, int CompanyID, int BranchID, int FundID, string ChargeMode)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@ChargeMode", ChargeMode);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFChargeForDueAndAdvancePayment]", sqlParams);

			return DataSets.Tables[0];
		}

		#region AdvancePayment

		public async Task<string> ApproveAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, AMLMFAdvPaymentApprovalDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@ChrgDuePaymentIDs", data.ChrgAdvPaymentIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAMLMFAdvancePayment", SpParameters);
		}

		public async Task<object> AMLMFAdvancePaymentDetail(string UserName, int CompanyID, int BranchID, int ChrgDuePaymentID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ChrgDuePaymentID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFAdvancePayment]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> ListAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, string listType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", listType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFAdvancePayment]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> SaveAMLMFAdvancePayment(string UserName, int CompanyID, int BranchID, AMLMFAdvPaymentDto data)
		{

		//	public decimal? ProjectedCharge { get; set; }
		//public decimal? ProjectedChargeTAX { get; set; }
		//public decimal? ProjectedChargeVAT { get; set; }
		//public decimal? TotalPayable { get; set; }
		//public decimal? NetPayable { get; set; }

		DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@LastChrgAdvPaymentID", data.LastChrgAdvPaymentID);
			SpParameters.Add("@AgreementChargeID", data.AgreementChargeID);
			SpParameters.Add("@TenorStartDate", Utility.DatetimeFormatter.DateFormat(data.TenorStartDate));
			SpParameters.Add("@TenorEndDate", Utility.DatetimeFormatter.DateFormat(data.TenorEndDate));
			SpParameters.Add("@TransactionDate", Utility.DatetimeFormatter.DateFormat(data.TransactionDate));
			SpParameters.Add("@NavAtStartDate", data.NavAtStartDate);
			SpParameters.Add("@Deviation", data.Deviation);
			SpParameters.Add("@ProjectedCharge", data.ProjectedCharge);
			SpParameters.Add("@NetPayable", data.NetPayable);
			SpParameters.Add("@TotalPayable", data.TotalPayable);
			SpParameters.Add("@ProjectedChargeTAX", data.ProjectedChargeTAX);
			SpParameters.Add("@ProjectedChargeVAT", data.ProjectedChargeVAT);
			SpParameters.Add("@BankAccountID", data.BankAccountID);
			SpParameters.Add("@PaymentMethod", data.PaymentMethod);
			SpParameters.Add("@InstrumentNumber", data.InstrumentNumber);
			SpParameters.Add("@NAVDate", Utility.DatetimeFormatter.ConvertStringToDatetime(data.NAVDate));

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAMLMFAdvancePayment", SpParameters);
		}

		public async Task<object> GetAMLMFAdvancePaymentProjectedCharge(string UserName, int CompanyID, int BranchID, int AgreementChargeID, DateTime TenorStartDate, DateTime TenorEndDate, DateTime NavDate, int FundID)
		{
			SqlParameter[] sqlParams = new SqlParameter[8];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AgreementChargeID", AgreementChargeID);
			sqlParams[4] = new SqlParameter("@TenorStartDate", TenorStartDate);
			sqlParams[5] = new SqlParameter("@TenorEndDate", TenorEndDate);
			sqlParams[6] = new SqlParameter("@NavDate", NavDate);
			sqlParams[7] = new SqlParameter("@FundID", FundID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_AMLMFAdvancePaymentProjectedCharge]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> GetFundAdvancePaymentDetail(string UserName, int CompanyID, int BranchID, int AgreementChargeID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AgreementChargeID", AgreementChargeID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_AMLMFAdvancePaymentDetail]", sqlParams);

			return DataSets.Tables[0];
		}

		#endregion

		public async Task<string> ApproveAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, AMLMFLockInInformationApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@LockInInformationIDs", data.LockInInformationIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemarks", data.ApprovalRemarks);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAMLMFLockInInformation", SpParameters);
		}

		public async Task<string> SaveAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, AMLMFLockInInformationDto data)
		{
			if(data.SlabList != null && data.SlabList.Count > 0)
			{
				data.HasSlab=true;
			}
			

			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@FundID", data.FundID);
			SpParameters.Add("@InvestmentType", data.InvestmentType);
			SpParameters.Add("@LockInDays", data.LockInDays);
			SpParameters.Add("@LockInInformationID", data.LockInInformationID);
			SpParameters.Add("@Remarks", data.Remarks);
			SpParameters.Add("@HasSlab", data.HasSlab);
			SpParameters.Add("@SlabList", ListtoDataTableConverter.ToDataTable(data.SlabList).AsTableValuedParameter("Type_AMLMFLockInInformationSlab"));
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAMLMFLockInInformation", SpParameters);
		}

		public async Task<object> AMLMFLockInInformationDetail(string UserName, int CompanyID, int BranchID, int LockInInformationID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", LockInInformationID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFLockInInformation]", sqlParams);

			AMLMFLockInInformationDto LockInfo = CustomConvert.DataSetToList<AMLMFLockInInformationDto>(DataSets.Tables[0]).FirstOrDefault();
			
			LockInfo.SlabList = CustomConvert.DataSetToList<AMLMFLockInInformationSlabDto>(DataSets.Tables[1]).ToList();

			return LockInfo;
		}

		public async Task<object> ListAMLMFLockInInformation(string UserName, int CompanyID, int BranchID, string listType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", listType);
			
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFLockInInformation]", sqlParams);

			return DataSets.Tables[0];
		}

		#region NAV_PROCESS
		
		public async Task<object> AMLMFNAVList(string UserName, int CompanyID, int BranchID, int FundID, DateTime DateFrom, DateTime DateTo)
		{
			SqlParameter[] sqlParams = new SqlParameter[6];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@FromDate", DateFrom);
			sqlParams[5] = new SqlParameter("@ToDate", DateTo);

			return _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListNAVatTransactionDateAMLMF]", sqlParams).Tables[0];
		}

		public async Task<object> GetAMLMFNAVatTransactionDate(string UserName, int CompanyID, int BranchID, int FundID, DateTime TransactionDate)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@TransactionDate", TransactionDate);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetAMLMFNAVatTransactionDate]", sqlParams);

			AMLMFNAVProcessingSummaryDto nav = CustomConvert.DataSetToList<AMLMFNAVProcessingSummaryDto>(DataSets.Tables[0]).FirstOrDefault();

			List< AMLMFNAVProcessingDetailDto > LegerHeadList = CustomConvert.DataSetToList<AMLMFNAVProcessingDetailDto>(DataSets.Tables[1]).ToList();


			
			SqlParameter[] sParams = new SqlParameter[4];
			sParams[0] = new SqlParameter("@UserName", UserName);
			sParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sParams[2] = new SqlParameter("@BranchID", BranchID);
			sParams[3] = new SqlParameter("@ListType", "approved");
			var DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAAML]", sParams);
			
			var AllCoAList = CustomConvert.DataSetToList<AMLMFNAVProcessingDetailTreeDto>(DataSet.Tables[0]).ToList();

			var DistinctLevel = AllCoAList.Select(o => o.AccLevel).Distinct().OrderByDescending(c => c);

			foreach (var level in DistinctLevel)
				foreach (var coa in AllCoAList.Where(c => c.AccLevel == level))
					coa.subRows = AllCoAList.Where(a => a.ParentGLCode == coa.GLCode).ToList();

			AllCoAList = AllCoAList.Where(c => c.ParentGLCode == null || c.ParentGLCode == "0").ToList();


			//CALCULATING LEAF TO ROOT TOTAL AMOUNT
			foreach (var level1 in AllCoAList)
			{
				foreach (var level2 in level1.subRows)
				{
					foreach (var level3 in level2.subRows)
					{
						foreach (var level4 in level3.subRows)
						{
							foreach (var level5 in level4.subRows)
							{
								var level5Head = LegerHeadList.Where(c => c.GLCode == level5.GLCode).FirstOrDefault();
								if (level5Head != null)
								{
									level5.LedgerHeadID = level5Head.LedgerHeadID;
									level5.PrevClosingBalance = level5Head.PrevClosingBalance;
									level5.BalanceChange = level5Head.BalanceChange;
									level5.ClosingBalance = level5Head.ClosingBalance;
								}
							}
							var level4Head = LegerHeadList.Where(c => c.GLCode == level4.GLCode).FirstOrDefault();
							if (level4Head != null)
							{
								level4.PrevClosingBalance = level4Head.PrevClosingBalance;
								level4.BalanceChange = level4Head.BalanceChange;
								level4.ClosingBalance = level4Head.ClosingBalance;
							}
							else
							{
								level4.PrevClosingBalance = level4.subRows.Sum(c => c.PrevClosingBalance);
								level4.BalanceChange = level4.subRows.Sum(c => c.BalanceChange);
								level4.ClosingBalance = level4.subRows.Sum(c => c.ClosingBalance);
							}
						}

						var level3Head = LegerHeadList.Where(c => c.GLCode == level3.GLCode).FirstOrDefault();
						if (level3Head != null)
						{
							level3.PrevClosingBalance = level3Head.PrevClosingBalance;
							level3.BalanceChange = level3Head.BalanceChange;
							level3.ClosingBalance = level3Head.ClosingBalance;
						}
						else
						{
							level3.PrevClosingBalance = level3.subRows.Sum(c => c.PrevClosingBalance);
							level3.BalanceChange = level3.subRows.Sum(c => c.BalanceChange);
							level3.ClosingBalance = level3.subRows.Sum(c => c.ClosingBalance);
						}
					}
					var level2Head = LegerHeadList.Where(c => c.GLCode == level2.GLCode).FirstOrDefault();
					if (level2Head != null)
					{
						level2.PrevClosingBalance = level2Head.PrevClosingBalance;
						level2.BalanceChange = level2Head.BalanceChange;
						level2.ClosingBalance = level2Head.ClosingBalance;
					}
					else
					{
						level2.PrevClosingBalance = level2.subRows.Sum(c => c.PrevClosingBalance);
						level2.BalanceChange = level2.subRows.Sum(c => c.BalanceChange);
						level2.ClosingBalance = level2.subRows.Sum(c => c.ClosingBalance);
					}
				}

				var level1Head = LegerHeadList.Where(c => c.GLCode == level1.GLCode).FirstOrDefault();
				if (level1Head != null)
				{
					level1.PrevClosingBalance = level1Head.PrevClosingBalance;
					level1.BalanceChange = level1Head.BalanceChange;
					level1.ClosingBalance = level1Head.ClosingBalance;
				}
				else
				{
					level1.PrevClosingBalance = level1.subRows.Sum(c => c.PrevClosingBalance);
					level1.BalanceChange = level1.subRows.Sum(c => c.BalanceChange);
					level1.ClosingBalance = level1.subRows.Sum(c => c.ClosingBalance);
				}
			}

			//FILTERING 0 CLOSING BALANCE
			foreach (var level1 in AllCoAList)
			{
				foreach (var level2 in level1.subRows)
				{
					foreach (var level3 in level2.subRows)
					{
						foreach (var level4 in level3.subRows)
						{
							level4.subRows = level4.subRows.Where(c => c.ClosingBalance != 0).ToList();
						}

						level3.subRows = level3.subRows.Where(c => c.ClosingBalance != 0).ToList();
					}

					level2.subRows = level2.subRows.Where(c => c.ClosingBalance != 0).ToList();
				}
				level1.subRows = level1.subRows.Where(c => c.ClosingBalance != 0).ToList();
			}

			nav.LedgerHeadList = AllCoAList;

			return nav;
		}

		public async Task<string> SaveAMLMFNAVatTransactionDate(string UserName, int CompanyID, int BranchID, int FundID, DateTime TransactionDate, DateTime EffectiveDateTo)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@FundID", FundID);
			SpParameters.Add("@TransactionDate", TransactionDate);
			SpParameters.Add("@EffectiveDateTo", EffectiveDateTo);

			
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ProcessNAVatTransactionDateAMLMF", SpParameters);
		}

		#endregion



		#region AMLMFAmortizationSetup

		public async Task<object> AMLMFAmortizationSetupDetail(string UserName, int CompanyID, int BranchID, int FundID, bool IsShariah)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);
			sqlParams[4] = new SqlParameter("@IsShariah", IsShariah);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_AMLMFAmortizationSetupDetail]", sqlParams);

			if (IsShariah)
			{
				List<AMLMFAmortizationSetupDto> setup = CustomConvert.DataSetToList<AMLMFAmortizationSetupDto>(DataSets.Tables[0]).ToList();

				List< AMLMFAmortizationSetuPdETAILDto > allDetail = CustomConvert.DataSetToList<AMLMFAmortizationSetuPdETAILDto>(DataSets.Tables[1]).ToList();

				foreach (var item in setup) item.DetailList = allDetail.Where(c => c.MFASetupID == item.MFASetupID).ToList();

				return setup;
			}
			else
			{
				AMLMFAmortizationSetupDto setup = CustomConvert.DataSetToList<AMLMFAmortizationSetupDto>(DataSets.Tables[0]).FirstOrDefault();

				setup.DetailList = CustomConvert.DataSetToList<AMLMFAmortizationSetuPdETAILDto>(DataSets.Tables[1]).ToList();

				return setup;
			}

			
		}

		public async Task<string> SaveAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, AMLMFAmortizationSetupDto data)
		{
			data.DetailList = data.DetailList.Where(c => c.Amount != 0).ToList();

			data.AmortizationStartDate = Utility.DatetimeFormatter.DateFormat(data.AmortizationStartDate);
			data.AmortizationEndDate = Utility.DatetimeFormatter.DateFormat(data.AmortizationEndDate);

			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@MFASetupID", data.MFASetupID == null ? 0 : data.MFASetupID);
			SpParameters.Add("@FundID", data.FundID );
			SpParameters.Add("@AmortizationStartDate", data.AmortizationStartDate);
			SpParameters.Add("@AmortizationEndDate", data.AmortizationEndDate);
			SpParameters.Add("@IsShariah", data.IsDSEShariahIndex);

			SpParameters.Add("@DetailList", ListtoDataTableConverter.ToDataTable(data.DetailList).AsTableValuedParameter("Type_AMLMFAmortizationSetupDetail"));


			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAMLMFAmortizationSetup", SpParameters);
		}

		public async Task<string> ApproveAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, AMLMFAmortizationSetupApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@MFASetupIDs", data.MFASetupIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
		
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAMLMFAmortizationSetup", SpParameters);
		}

		public async Task<object> ListAMLMFAmortizationSetup(string UserName, int CompanyID, int BranchID, string listtype)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", listtype);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFAmortizationSetup]", sqlParams);

			return DataSets.Tables[0];
		}

		#endregion AMLMFAmortizationSetup

		public async Task<object> ListAMLCCAAccountHead(string UserName, int CompanyID, int BranchID, string listtype)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", listtype);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLCCAAccountHead]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> ApproveAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, AMLMFBAInterestAdjustmentApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@IntAdjustmentIDs", data.IntAdjustmentIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			 
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAMLMFBAInterestAdjustment", SpParameters);
		}

		public async Task<object> AMLMFBAInterestAdjustmentDetail(string UserName, int CompanyID, int BranchID, int IntAdjustmentID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            
            sqlParams[3] = new SqlParameter("@ListTypeOrIntAdjustmentID", IntAdjustmentID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFBAInterestAdjustment]", sqlParams);

			return CustomConvert.DataSetToList<AMLMFBAInterestAdjustmentDto>(DataSets.Tables[0]).FirstOrDefault();
		}

		public async Task<object> ListAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, string ListType, int FundID)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@FundID", FundID);
            sqlParams[4] = new SqlParameter("@ListTypeOrIntAdjustmentID", ListType);
			
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFBAInterestAdjustment]", sqlParams);

			return DataSets.Tables[0];
		}
        public async Task<object> ListAMLMFBAInterestAdjustmentReversal(string UserName, int CompanyID, int BranchID, string ListType, int FundID)
        {
            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@FundID", FundID);
            sqlParams[4] = new SqlParameter("@ListTypeOrIntAdjustmentID", ListType);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMFBAInterestAdjustmentReversal]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<string> AMLMFBAInterestAdjustmentReversal(int CompanyID, int BranchID, string userName, AMLMFBAInterestAdjustmentReversalDto entry)
        {
            try
            {
                string sp = "AML_InsertUpdateBankInterestReversal";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReversalReason", entry.ReversalReason);
                SpParameters.Add("@IntCollectionID", entry.IntAdjustmentID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> AMLMFBAInterestAdjustmentReversalApproval(int CompanyID, int BranchID, string userName, AMLMFBAInterestAdjustmentReversalApprovalDto entry)
        {
            try
            {
                string sp = "AML_ApprovedBankInterestCollectionReversal";
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IntCollReversalID", entry.IntCollReversalID);
                SpParameters.Add("@ApprovalStatus", entry.ApprovalStatus);
                SpParameters.Add("@ApprovalRemark", entry.ApprovalRemark);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> saveAMLMFBAInterestAdjustment(string UserName, int CompanyID, int BranchID, AMLMFBAInterestAdjustmentDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@DateFrom", Utility.DatetimeFormatter.DateFormat(data.DateFrom));
			SpParameters.Add("@DateTo", Utility.DatetimeFormatter.DateFormat(data.DateTo));
			SpParameters.Add("@AdjustmentAmount", data.AdjustmentAmount);
			SpParameters.Add("@IsDebited", data.IsDebited);
			SpParameters.Add("@Reason", data.Reason);
			SpParameters.Add("@MFBankAccountID", data.MFBankAccountID);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_InsertAMLMFBAInterestAdjustment", SpParameters);
		}

		public async Task<string> CollectAccrualInterestBankAccountMFAML(string UserName, int CompanyID, int BranchID, int MFBankAccountID,  AMLIntrestAdjustmentCollectDto InterestData)
		{
			string MFBABalanceIDs = string.Empty;

			foreach(var item in InterestData.AMLIntrestAccualCollectDtoList) MFBABalanceIDs = MFBABalanceIDs + "," + item.MFBABalanceID;

			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@MFBABalanceIDs", MFBABalanceIDs);
			SpParameters.Add("@MFBankAccountID", MFBankAccountID);
            SpParameters.Add("@AccuredInterest", InterestData.AccruedInterest);
            SpParameters.Add("@AccuredAIT", InterestData.AccruedAIT);
            SpParameters.Add("@AdjustmentInterest", InterestData.AdjustmentInterest);
            SpParameters.Add("@AdjustmentAIT", InterestData.AdjustmentAIT);
            SpParameters.Add("@ExeciseDuty", InterestData.ExciseDuty);
            SpParameters.Add("@CollectionInterest", InterestData.CollecationInterest);
            SpParameters.Add("@CollectionDate", Utility.DatetimeFormatter.DateFormat(InterestData.CollectionDate));

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_CollectAccrualInterestOfBankAccountAMLMF", SpParameters);
		}

		public async Task<object> MFAccrualInterestList(string UserName, int CompanyID, int BranchID, AMLIntrestAccualList data)
		{
			SqlParameter[] sqlParams = new SqlParameter[6];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);

			sqlParams[3] = new SqlParameter("@MFBankAccountID", data.MFBankAccountID);
			sqlParams[4] = new SqlParameter("@FromDate", Utility.DatetimeFormatter.DateFormat(data.FromDate));
			sqlParams[5] = new SqlParameter("@ToDate", Utility.DatetimeFormatter.DateFormat(data.ToDate));

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccrualInterestOfBankAccountAMLMF]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<List<AMLFundDto>> MutualFundBankList(string UserName, int CompanyID, int BranchID)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_MFBankAccountList]", sqlParams);

			List<AMLFundDto> FundList = CustomConvert.DataSetToList<AMLFundDto>(DataSets.Tables[0]).ToList();
			List<AMLFundBankAccountDto> AMLFundBankAccountList = CustomConvert.DataSetToList<AMLFundBankAccountDto>(DataSets.Tables[1]).ToList();

			foreach (var item in FundList)
			{
				item.BankAccountList = AMLFundBankAccountList.Where(c => c.FundID == item.FundID).ToList();
			}

			return FundList;
		}

        public async Task<List<AMLFundMFDto>> MutualFundBankMFList(string UserName, int CompanyID, int BranchID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_MFBankAccountListbyMFBankAccountID]", sqlParams);

            List<AMLFundMFDto> FundList = CustomConvert.DataSetToList<AMLFundMFDto>(DataSets.Tables[0]).ToList();
            List<AMLFundBankAccountMFDto> AMLFundBankAccountList = CustomConvert.DataSetToList<AMLFundBankAccountMFDto>(DataSets.Tables[1]).ToList();

            foreach (var item in FundList)
            {
                item.BankAccountList = AMLFundBankAccountList.Where(c => c.FundID == item.FundID).ToList();
            }

            return FundList;
        }

        public async Task<object> ListMutualFundDetail(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLMutualFund]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> ApproveMutualFundDetail(string UserName, int CompanyID, int BranchID, ApproveAMLMutualFundDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@FundIDs", data.FundIDs);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAMLMutualFund", SpParameters);
		}

		public async Task<string> saveMutualFundDetail(string UserName, int CompanyID, int BranchID, AMLMutualFundDto data)
		{
			List<AMLMutualFundEntityDto> fundList = new List<AMLMutualFundEntityDto>();
			fundList.Add(JsonConvert.DeserializeObject<AMLMutualFundEntityDto>(JsonConvert.SerializeObject(data)));

			

			List<FundChargeSlabDto> SlabList = new List<FundChargeSlabDto>();

			foreach (var item in data.FundChargeList)
			{
				if(item.SlabList != null && item.SlabList.Count > 0)
				{
					item.HasSlab = true;
					item.ChargeAmount = 0;

					foreach (var slab in item?.SlabList)
					{
						if (slab.SlabID == null) slab.SlabID = 0;
						slab.ReferenceKey = item.ContractID.ToString() + item.AttributeID.ToString() + item.ProductAttributeID.ToString();
						SlabList.Add(slab);
					}
				}
				
			}

			; foreach (var bank in data.BankAccountList) 
			{
				bank.InterestStartDate = Utility.DatetimeFormatter.DateFormat(bank.InterestStartDate);
				
			}

			List<FundChargeEntityDto> ChargeList = JsonConvert.DeserializeObject<List<FundChargeEntityDto>>(JsonConvert.SerializeObject(data.FundChargeList));


			List<FundBankAccountTypeTableDto> TypeBankAccountList = data.BankAccountList.ConvertAll(x => new FundBankAccountTypeTableDto 
			{ 
				BankAccountName = x.BankAccountName,
				BankOrgBranchID = x.BankOrgBranchID,
				BankAccountNo = x.BankAccountNo,
				BankAccountType = x.BankAccountType,
				MFBankAccountID = x.MFBankAccountID,
				IsActive= x.IsActive,
				BalanceType = x.BalanceType,
				HasSlab = x.HasSlab,
				BankOrgID = x.BankOrgID,
				InterestStartDate =x.InterestStartDate,
				BankOrgName = x.BankOrgName,
				BranchName = x.BranchName,
				ControlHead =x.ControlHead,
				FundID = x.FundID,
				GLCode = x.GLCode,
				GLName = x.GLName,
				InterestRate = x.InterestRate,
				LedgerHeadID = x.LedgerHeadID,
				MakeDate = x.MakeDate,
				Maker=x.Maker,
				ProductID = x.ProductID,
				RoutingNo = x.RoutingNo,
				AIT=x.AIT,
				Modules=x.Modules,
			});


			
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			ChargeList = ChargeList.Where(c => c.ProductID == data.NonSipProductID).ToList();


			
			List<SalListDto> BankInterestSlabList = new List<SalListDto>();
			foreach (var item in data.BankAccountList)
			{
				var typeBankAcc = TypeBankAccountList.Where(c => c.MFBankAccountID == item.MFBankAccountID && c.BankAccountNo == item.BankAccountNo && c.BankAccountName == item.BankAccountName && c.BankOrgBranchID == item.BankOrgBranchID).FirstOrDefault();

				if (item.SlabList != null && item.SlabList.Count>0)
				{
					typeBankAcc.HasSlab = true;
					foreach (var slab in item?.SlabList)
					{
						item.InterestRate = 0;
						slab.ReferenceKey = item.BankAccountName + item.BankAccountNo;
						BankInterestSlabList.Add(slab);
			
					}
				}
				else { typeBankAcc.HasSlab = false; }
				
			}

			var fund = ListtoDataTableConverter.ToDataTable(fundList).AsTableValuedParameter("Type_FundAMLMutualFund");
			var BankAccountList = ListtoDataTableConverter.ToDataTable(TypeBankAccountList).AsTableValuedParameter("Type_FundAMLMutualFundBankAccount");
			var allcharge = ListtoDataTableConverter.ToDataTable(ChargeList).AsTableValuedParameter("Type_FundAMLMutualFundCharge");

			SpParameters.Add("@Fund", fund);
			SpParameters.Add("@FundBankAccountList", BankAccountList);
			SpParameters.Add("@FundChargetList", allcharge);
			SpParameters.Add("@FundChargetSlabList", ListtoDataTableConverter.ToDataTable(SlabList).AsTableValuedParameter("Type_AMLMutualFundChargeSlab"));
			SpParameters.Add("@InvesmentRuleList", ListtoDataTableConverter.ToDataTable(data.FundInvestmentRuleList).AsTableValuedParameter("Type_AMLInvesmentRule"));
			SpParameters.Add("@BankInterestSlabList", ListtoDataTableConverter.ToDataTable(BankInterestSlabList).AsTableValuedParameter("Type_AMLMFBankInterestSlab"));

			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAMLMutualFund", SpParameters);
		}

		public async Task<object> getMutualFundDetail(string UserName, int CompanyID, int BranchID, int FundID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FundID", FundID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_AMLMutualFundDetail]", sqlParams);

			AMLMutualFundDto Fund = CustomConvert.DataSetToList<AMLMutualFundDto>(DataSets.Tables[0]).FirstOrDefault();
			 Fund.ProductList = CustomConvert.DataSetToList<FundProductDto>(DataSets.Tables[1]).ToList();
			 Fund.BankAccountList = CustomConvert.DataSetToList<FundBankAccountDto>(DataSets.Tables[2]).ToList();
			 Fund.FundChargeList = CustomConvert.DataSetToList<FundChargeDto>(DataSets.Tables[3]).ToList();

			var slabList = CustomConvert.DataSetToList<FundChargeSlabDto>(DataSets.Tables[4]).ToList();

			foreach(var charge in Fund.FundChargeList)
			{
				charge.SlabList = slabList.Where(c => c.AgreementChargeID == charge.AgreementChargeID).ToList();
			}

			Fund.InvestmentRuleAll = CustomConvert.DataSetToList<FundInvestmentRuleDto>(DataSets.Tables[5]).ToList();

			Fund.FundInvestmentRuleList = CustomConvert.DataSetToList<FundInvestmentRuleDto>(DataSets.Tables[6]).ToList();

			Fund.LedgerHeadList = CustomConvert.DataSetToList<FundLedgerHeadDto>(DataSets.Tables[7]).ToList();

			List<SalListDto> AllSlabList = CustomConvert.DataSetToList<SalListDto>(DataSets.Tables[8]).ToList();

			foreach(var item in Fund.BankAccountList)
			{
				item.SlabList = AllSlabList.Where(c => c.MFBankAccountID == item.MFBankAccountID).ToList();
			}

			return Fund;
		}

		public async Task<string> ApproveMutualFund(string UserName, int CompanyID, int BranchID, MutualFundApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@FundIDs", data.FundIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveMutualFund", SpParameters);
		}

		public async Task<string> SaveMutualFund(string UserName, int CompanyID, int BranchID, MutualFundDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);

			SpParameters.Add("@FundID", data.FundID);
			SpParameters.Add("@FundName", data.FundName);
			SpParameters.Add("@ShortName", data.ShortName);
			SpParameters.Add("@FundType", data.FundType);
			SpParameters.Add("@LotSize", data.LotSize);
			SpParameters.Add("@FundStatus", data.FundStatus);
			SpParameters.Add("@FaceValue", data.FaceValue);
			SpParameters.Add("@ContractID", data.ContractID);
			SpParameters.Add("@InstrumentID", data.InstrumentID);

			SpParameters.Add("@SponsorOrgID", data.SponsorOrgID);
			SpParameters.Add("@CustodianID", data.CustodianID);
			SpParameters.Add("@TrusteeID", data.TrusteeID);
			SpParameters.Add("@SponsorContribution", data.SponsorContribution);
			SpParameters.Add("@PreIPOSubsFund", data.PreIPOSubsFund);
			SpParameters.Add("@IPOSubsFund", data.IPOSubsFund);
			SpParameters.Add("@NewIssuedFund", data.NewIssuedFund);
			SpParameters.Add("@RegistrationNo", data.RegistrationNo);
			SpParameters.Add("@TDRegistrationDate", data.TDRegistrationDate != null && data.TDRegistrationDate.Length == 0? null : Utility.DatetimeFormatter.DateFormat(data.TDRegistrationDate) );
			SpParameters.Add("@FormationDate", data.FormationDate != null && data.FormationDate.Length == 0 ? null :  Utility.DatetimeFormatter.DateFormat(data.FormationDate));
			SpParameters.Add("@FundSize", data.FundSize);
			SpParameters.Add("@IPOSubsStartDate", data.IPOSubsStartDate != null && data.IPOSubsStartDate.Length == 0 ? null : Utility.DatetimeFormatter.DateFormat(data.IPOSubsStartDate));
			SpParameters.Add("@IPOSubsEndDate", data.IPOSubsEndDate != null && data.IPOSubsEndDate.Length == 0 ? null : Utility.DatetimeFormatter.DateFormat(data.IPOSubsEndDate));
			
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveMutualFund", SpParameters);
		}

		public async Task<object> MutualFundDetail(string UserName, int CompanyID, int BranchID, int FundID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", FundID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListMutualFund]", sqlParams);


			return CustomConvert.DataSetToList<MutualFundDto>(DataSets.Tables[0]).FirstOrDefault(); ;
		}

		public async Task<object> ListMutualFund(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListMutualFund]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> CustodianInformationDetail(string UserName, int CompanyID, int BranchID, int organizationID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@Organization", organizationID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_getCustodianInformation]", sqlParams);

			var data = CustomConvert.DataSetToList<CustodianTrusteeDto>(DataSets.Tables[0]).FirstOrDefault();

			return data;
		}

		public async Task<object> CustodianInformationList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCustodianInformation]", sqlParams);

			var list = CustomConvert.DataSetToList<CustodianTrusteeDto>(DataSets.Tables[0]).ToList();

			return list;
		}

		public async Task<string> SaveCustodianInformation(string UserName, int CompanyID, int BranchID, CustodianTrusteeDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@OrganizationID", data.OrganizationID);
			SpParameters.Add("@ShortName", data.ShortName);
			SpParameters.Add("@CPName", data.CPName);
			SpParameters.Add("@CPMobileNo", data.CPMobileNo);
			SpParameters.Add("@CPEmailAddress", data.CPEmailAddress);
			SpParameters.Add("@DPCode", data.DPCode);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveCustodianInformation", SpParameters);
		}

		public async Task<object> TrusteeInformationList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListTrusteeInformation]", sqlParams);

			var list = CustomConvert.DataSetToList<CustodianTrusteeDto>(DataSets.Tables[0]).ToList();

			return list;
		}

		public async Task<object> TrusteeInformationDetail(string UserName, int CompanyID, int BranchID, int organizationID)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@Organization", organizationID);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_getTrusteeInformation]", sqlParams);

			var data = CustomConvert.DataSetToList<CustodianTrusteeDto>(DataSets.Tables[0]).FirstOrDefault();

			return data;
		}

		public async Task<string> SaveTrusteeInformation(string UserName, int CompanyID, int BranchID, CustodianTrusteeDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@OrganizationID", data.OrganizationID);
			SpParameters.Add("@ShortName", data.ShortName);
			SpParameters.Add("@CPName", data.CPName);
			SpParameters.Add("@CPMobileNo", data.CPMobileNo);
			SpParameters.Add("@CPEmailAddress", data.CPEmailAddress);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveTrusteeInformation", SpParameters);
		}

		public async Task<string> ApproveCustodianInformation(string UserName, int CompanyID, int BranchID, ApproveCustodianDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@CustodianIDs", data.CustodianIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCustodianInformation", SpParameters);
		}

		public async Task<string> ApproveTrusteeInformation(string UserName, int CompanyID, int BranchID, ApproveTrusteeDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@TrusteeIDs", data.TrusteeIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveTrusteeInformation", SpParameters);
		}

        public async Task<object> InterestandFeesAccuredInfo(string UserName,int CompanyID, int BranchID, int FundID)
        {
            

            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@FundID", FundID);



            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("AML_InterestandFeesAccuredInfo", sqlParams);
            // string FileDescription = DataSets.Tables[0].Rows[0][0].ToString();

            return new
            {
				MarketValue = DataSets.Tables[0],
				TBillList = DataSets.Tables[1],
				FDRList = DataSets.Tables[2],
				BankInterestList = DataSets.Tables[3],
				AmortizationList = DataSets.Tables[4],

				FeesList = DataSets.Tables[5],

			};

        }

        public async Task<string> GetInterestandFeesAccuredSaved(string UserName, int CompanyID, int BranchID, int FundID)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@FundID", FundID);

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("AML_InterestandFeesAccuredSaved", SpParameters);
        }
    }
}
