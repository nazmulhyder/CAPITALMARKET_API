using AutoMapper;
using Dapper;
using Model.DTOs.Accounting;
using Model.DTOs.Allocation;
using Model.DTOs.AssetManager;
using Model.DTOs.Charges;
using Model.DTOs.CoA;
using Model.DTOs.SaleOrder;
using Model.DTOs.User;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service.Implementation
{
    public class AccountingRepository : IAccountingRepository
	{
        public readonly IDBCommonOpService _dbCommonOperation;
        public IMapper mapper;
        public AccountingRepository(IDBCommonOpService dbCommonOperation, IMapper _mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            mapper = _mapper;
        }

		public async Task<object> ListAccLedgerHeadForVoucherFilter(string UserName, int CompanyID, int BranchID)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
		
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadForVoucherFilter]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> ApproveAccAgreement(string UserName, int CompanyID, int BranchID, AccAgreementApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@ContractIDs", data.ContractIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApproveStatus", data.ApproveStatus);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID == 4)
				return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccAgreementSL", SpParameters);
			else if(CompanyID == 3)
				return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccAgreementIL", SpParameters);
			else
				return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccAgreementAML", SpParameters);
		}

		public async Task<object> GetGenAgreement(string UserName, int CompanyID, int BranchID, int ContractID, bool IsFundAccount)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ContractID);
			sqlParams[4] = new SqlParameter("@IsFundAccount", IsFundAccount);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAgreementForAdmin]", sqlParams);

			var agreement = CustomConvert.DataSetToList<AccAgreementDetailDto>(DataSets.Tables[0]).FirstOrDefault();
			
			if(agreement!= null)
			{
				SqlParameter[] Params = new SqlParameter[4];

				Params[0] = new SqlParameter("@UserName", UserName);
				Params[1] = new SqlParameter("@CompanyID", CompanyID);
				Params[2] = new SqlParameter("@BranchID", BranchID);
				Params[3] = new SqlParameter("@CIFOrName", agreement.MemberCode);

				var Sets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCIFForAdmin]", Params);

				agreement.BankList = CustomConvert.DataSetToList<AccCifBranchDto>(Sets.Tables[1]).ToList();

			}

			return agreement;
		}

		public async Task<object> ListGenAgreement(string UserName, int CompanyID, int BranchID, bool IsFundAccount, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);
			sqlParams[4] = new SqlParameter("@IsFundAccount", IsFundAccount);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAgreementForAdmin]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> saveAgreement(string UserName, int CompanyID, int BranchID, bool IsFundAccount, AccCifListDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@ContractID", data.ContractID == null ? "0" : data.ContractID);
			SpParameters.Add("@IndexID", data.IndexID == null ? "0" : data.IndexID);
			SpParameters.Add("@ProductID", data.ProductID);
			SpParameters.Add("@AccountNumber", data.AccountNumber);
			SpParameters.Add("@BankAccountID", data.BankAccountID);
			SpParameters.Add("@IsFundAccount", IsFundAccount);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAccGrnAgreementAdmin", SpParameters);
		}

		public async Task<List<AccCifListDto>> ListCif(string UserName, int CompanyID, int BranchID, string SearchKeyword)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@CIFOrName", SearchKeyword);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCIFForAdmin]", sqlParams);

			List<AccCifListDto> list = CustomConvert.DataSetToList<AccCifListDto>(DataSets.Tables[0]).ToList();
			List<AccCifBranchDto> Banklist = CustomConvert.DataSetToList<AccCifBranchDto>(DataSets.Tables[1]).ToList();

			foreach(var item in list)
			{
				item.BankList = Banklist.Where(c=>c.IndexID == item.IndexID).ToList();
			}

			return list;

		}

		public async Task<object> Listproduct(string UserName, int CompanyID, int BranchID, bool IsCompanyAsProductShow)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@IsCompanyAsProductShow", IsCompanyAsProductShow);
			
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListProductForAccounting]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> SaveReverseAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@VoucherDate", Utility.DatetimeFormatter.DateFormat(data.VoucherDate));
			SpParameters.Add("@IssueDate", Utility.DatetimeFormatter.DateFormat(data.IssueDate));
			SpParameters.Add("@VoucherType", data.VoucherType);
			SpParameters.Add("@VoucherID", data.VoucherID);
			SpParameters.Add("@InstrumentType", data.InstrumentType);
			SpParameters.Add("@InstrumentNo", data.InstrumentNo);
			SpParameters.Add("@VoucherNote", data.VoucherNote);
			SpParameters.Add("@LedgerList", ListtoDataTableConverter.ToDataTable(data.LedgerList).AsTableValuedParameter("Type_AccLedger"));
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID == 4)
				return await _dbCommonOperation.InsertUpdateBySP("CM_SaveReverseAccVoucherSL", SpParameters);
			else if(CompanyID == 3)
				return await _dbCommonOperation.InsertUpdateBySP("CM_SaveReverseAccVoucherIL", SpParameters);
			else
				return await _dbCommonOperation.InsertUpdateBySP("CM_SaveReverseAccVoucherAML", SpParameters);
		}

		public async Task<object> ApproveAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherApproveDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@VoucherIDs", data.VoucherIDs);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ApproveStatus", data.ApproveStatus);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccVoucherSL", SpParameters);
			else if(CompanyID==3) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccVoucherIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccVoucherAML", SpParameters);
		}

		public async Task<object> GetAccVoucher(string UserName, int CompanyID, int BranchID, string VoucherRefNo)
		{
			SqlParameter[] sqlParams = new SqlParameter[15];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FromVoucherDate", "");
			sqlParams[4] = new SqlParameter("@ToVoucherDate", "");
			sqlParams[5] = new SqlParameter("@VoucherType", "all");
			sqlParams[6] = new SqlParameter("@ApprovalStatus", "all");
			sqlParams[7] = new SqlParameter("@VoucherNote", "");
			sqlParams[8] = new SqlParameter("@VoucherIssuer", "");
			sqlParams[9] = new SqlParameter("@VoucherRefNo", VoucherRefNo);
			
			sqlParams[10] = new SqlParameter("@EntryType","all");
			sqlParams[11] = new SqlParameter("@LedgerHeadID", 0);
			sqlParams[12] = new SqlParameter("@IsDetail", true);
			sqlParams[13] = new SqlParameter("@AccountNumber", "");
			sqlParams[14] = new SqlParameter("@ProductID", "0");

			DataSet DataSets = new DataSet();
			if(CompanyID==4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherSL]", sqlParams);
			else if(CompanyID==3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherIL]", sqlParams);
			else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherAML]", sqlParams);

			SingleAccVoucherDto Voucher = CustomConvert.DataSetToList<SingleAccVoucherDto>(DataSets.Tables[0]).FirstOrDefault();
			
			List<SingleAccLedgerDto> LedgerList = CustomConvert.DataSetToList<SingleAccLedgerDto>(DataSets.Tables[1]).ToList();

			Voucher.LedgerList = LedgerList.Where(c => c.VoucherID == Voucher.VoucherID).ToList();

			//foreach(var ledger in Voucher.LedgerList)
			//{
			//	SqlParameter[] Params = new SqlParameter[5];

			//	Params[0] = new SqlParameter("@UserName", UserName);
			//	Params[1] = new SqlParameter("@CompanyID", CompanyID);
			//	Params[2] = new SqlParameter("@BranchID", BranchID);
			//	Params[3] = new SqlParameter("@AccountNumber", ledger.AccountNumber);
			//	Params[4] = new SqlParameter("@VoucherType", Voucher.VoucherType);

			//	DataSet Sets = new DataSet();
			//	if(CompanyID==4) Sets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsSL]", Params);
			//	else if(CompanyID==3) Sets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsIL]", Params);
			//	else Sets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsAML]", Params);

			//	ledger.LedgerHeadList = Sets.Tables[0];


			//	SqlParameter[] sql = new SqlParameter[5];

			//	sql[0] = new SqlParameter("@UserName", UserName);
			//	sql[1] = new SqlParameter("@CompanyID", CompanyID);
			//	sql[2] = new SqlParameter("@BranchID", BranchID);
			//	sql[3] = new SqlParameter("@ProductID", "0");
			//	sql[4] = new SqlParameter("@AccountNumber", ledger.AccountNumber);

			//	var DSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSearch]", sql);

			//	ledger.AccountList = DSets.Tables[0];
			//}

			return Voucher;
		}

		public async Task<object> ListAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherListParameterDto data)
		{
			SqlParameter[] sqlParams = new SqlParameter[15];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@FromVoucherDate", Utility.DatetimeFormatter.DateFormat(data.FromVoucherDate));
			sqlParams[4] = new SqlParameter("@ToVoucherDate", Utility.DatetimeFormatter.DateFormat(data.ToVoucherDate));
			sqlParams[5] = new SqlParameter("@VoucherType", data.VoucherType);
			sqlParams[6] = new SqlParameter("@ApprovalStatus", data.ApprovalStatus);
			sqlParams[7] = new SqlParameter("@VoucherNote", data.VoucherNote);
			sqlParams[8] = new SqlParameter("@VoucherIssuer",data.VoucherIssuer);
			sqlParams[9] = new SqlParameter("@VoucherRefNo", data.VoucherRefNo);
			sqlParams[10] = new SqlParameter("@EntryType", data.EntryType);
			sqlParams[11] = new SqlParameter("@LedgerHeadID", data.LedgerHeadID == null? 0 :data.LedgerHeadID);
			sqlParams[12] = new SqlParameter("@IsDetail", false);
			sqlParams[13] = new SqlParameter("@AccountNumber", data.AccountNumber);
			sqlParams[14] = new SqlParameter("@ProductID",data.ProductID);

			DataSet DataSets = new DataSet();

			if(CompanyID==4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherSL]", sqlParams);
			else if(CompanyID==3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherIL]", sqlParams);
			else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccVoucherAML]", sqlParams);

			//List<AccVoucherDto> VoucherList = CustomConvert.DataSetToList<AccVoucherDto>(DataSets.Tables[0]).ToList();
			//List<AccLedgerDto> LedgerList = CustomConvert.DataSetToList<AccLedgerDto>(DataSets.Tables[1]).ToList();

			//foreach(var Voucher in VoucherList) Voucher.LedgerList = LedgerList.Where(c=>c.VoucherID == Voucher.VoucherID).ToList();

			return DataSets.Tables[0];
		}

		public async Task<object> GetBulkVoucherValidation(string UserName, int CompanyID, int BranchID, List<VoucherBulkDto> vouchers)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@LedgerList", ListtoDataTableConverter.ToDataTable(vouchers));

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CM_BulkVoucherValidation", sqlParams);

			List<SingleAccLedgerDto> LedgerList = CustomConvert.DataSetToList<SingleAccLedgerDto>(DataSets.Tables[0]).ToList();

			return LedgerList;
		}

		public async Task<string> SaveAccVoucher(string UserName, int CompanyID, int BranchID, string IssueType,bool IsPartyVoucher, AccVoucherDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@FundContractID", data.ContractID);
			SpParameters.Add("@IsPartyVoucher", IsPartyVoucher);
			SpParameters.Add("@VoucherDate", Utility.DatetimeFormatter.DateFormat(data.VoucherDate));
			SpParameters.Add("@IssueDate", Utility.DatetimeFormatter.DateFormat(data.IssueDate));
			SpParameters.Add("@VoucherType", data.VoucherType);
			SpParameters.Add("@IssueType", IssueType);
			SpParameters.Add("@VoucherNote", data.VoucherNote);
			SpParameters.Add("@InstrumentType", data.InstrumentType);
			SpParameters.Add("@InstrumentNo", data.InstrumentNo);
			SpParameters.Add("@IsPrepareInstrument", data.IsPrepareInstrument);
			SpParameters.Add("@LedgerList", ListtoDataTableConverter.ToDataTable(data.LedgerList).AsTableValuedParameter("Type_AccLedger"));
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAccVoucherSL", SpParameters);
			else if(CompanyID==3) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAccVoucherIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_SaveAccVoucherAML", SpParameters);
		}

		public async Task<object> SearchAccount(string UserName, int CompanyID, int BranchID, string AccountNumber)
		{
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ProductID", "0");
			sqlParams[4] = new SqlParameter("@AccountNumber", AccountNumber);

			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccountSearchForVoucher]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> GetLedgerHeadsForAccVoucher(string UserName, int CompanyID, int BranchID, string AccountNumber, string VoucherType)
		{
			
			SqlParameter[] sqlParams = new SqlParameter[5];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@AccountNumber", AccountNumber);
			sqlParams[4] = new SqlParameter("@VoucherType", VoucherType);

			DataSet DataSets = new DataSet();
			 if(CompanyID==4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsSL]", sqlParams);
			 else if(CompanyID==3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsIL]", sqlParams);
			 else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_GetLedgerHeadsAML]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> ApproveVoucherPostingDateChange(string UserName, int CompanyID, int BranchID, ApproveVoucherPostingDateChangeDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@PostingDateID", data.PostingDateID);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveVoucherDateChange", SpParameters);
		}

		public async Task<string> SaveVoucherPostingDateChange(string UserName, int CompanyID, int BranchID, VoucherPostingDateChangeDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@TransactionDayID", data.TransactionDayID);
			SpParameters.Add("@ChangedPostingDate", Utility.DatetimeFormatter.DateFormat(data.VoucherPostingDate));
			SpParameters.Add("@ChangeRemark", data.ChangeRemark);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveVoucherDateChangeSL", SpParameters);
			else if(CompanyID==3) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveVoucherDateChangeIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_SaveVoucherDateChangeAML", SpParameters);
		}

		public async Task<object> GetVoucherPostingDateChangeList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);
		
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_VoucherDateChangeList]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<object> GetVoucherPostingDate(string UserName, int CompanyID, int BranchID)
		{
			SqlParameter[] sqlParams = new SqlParameter[3];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
		
			var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_VoucherDate]", sqlParams);

			var list = CustomConvert.DataSetToList<VoucherPostingDateChangeDto>(DataSets.Tables[0]).ToList();

			return list.FirstOrDefault();
		}

		public async Task<string> ApproveAccLedgerHead(string UserName, int CompanyID, int BranchID, ApproveAccLedgerHead data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@LedgerHeadIDs", data.LedgerHeadIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccLedgerHeadSL", SpParameters);
			else if(CompanyID==3) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccLedgerHeadIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveAccLedgerHeadAML", SpParameters);
		}

		public async Task<object> ListAccLedgerHeadAll(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			DataSet DataSets = new DataSet();
			if(CompanyID == 4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadAllSL]", sqlParams);
			else if(CompanyID == 3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadAllIL]", sqlParams);
			else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadAllAML]", sqlParams);

			return DataSets.Tables[0];
		}

		public async Task<string> SaveUpdateAccLedgerHeadBulk(string UserName, int CompanyID, int BranchID,int ProductID, List<AccLedgerHeadBulkDto> datalist)
		{
			foreach(var data in datalist)
			{
				DynamicParameters SpParameters = new DynamicParameters();

				SpParameters.Add("@UserName", UserName);
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);
				SpParameters.Add("@COAID", data.COAID);
				SpParameters.Add("@ProductID", ProductID);
				SpParameters.Add("@IsActive", data.IsActive);
				SpParameters.Add("@EnableJournalVoucher", data.EnableJournalVoucher);
				SpParameters.Add("@EnablePaymentVoucher", data.EnablePaymentVoucher);
				SpParameters.Add("@EnableCollectionVoucher", data.EnableCollectionVoucher);
				SpParameters.Add("@EnableBalanceCheck", data.EnableBalanceCheck);
				SpParameters.Add("@EnableBankAdjustmentVoucher", data.EnableBankAdjustmentVoucher);
				SpParameters.Add("@EnableNAVProcess", data.EnableNAVProcess);
				SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				if (CompanyID == 4)  await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadBulkSL", SpParameters);
				else if (CompanyID == 3)  await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadBulkIL", SpParameters);
				else await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadBulkAML", SpParameters);
			}

			return await Task.FromResult("Ledger head has been saved");

		}


		public async Task<string> SaveUpdateAccLedgerHead(string UserName, int CompanyID, int BranchID, AccLedgerHeadDto data)
		{
			DynamicParameters SpParameters = new DynamicParameters();

			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@COAID", data.COAID);
			SpParameters.Add("@ProductID", data.ProductID);
			SpParameters.Add("@IsActive", data.IsActive);
			SpParameters.Add("@EnableJournalVoucher", data.EnableJournalVoucher);
			SpParameters.Add("@EnablePaymentVoucher", data.EnablePaymentVoucher);
			SpParameters.Add("@EnableCollectionVoucher", data.EnableCollectionVoucher);
			SpParameters.Add("@EnableBalanceCheck", data.EnableBalanceCheck);
			SpParameters.Add("@EnableBankAdjustmentVoucher", data.EnableBankAdjustmentVoucher);
			SpParameters.Add("@EnableNAVProcess", data.EnableNAVProcess);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadSL", SpParameters);
			else if(CompanyID==3) return await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_SaveUpdateAccLedgerHeadAML", SpParameters);
		}

		public async Task<List<CoAReverseDto>> ListAccLedgerHead(string UserName, int CompanyID, int BranchID, int ProductID)
		{
			SqlParameter[] Params = new SqlParameter[4];

			Params[0] = new SqlParameter("@UserName", UserName);
			Params[1] = new SqlParameter("@CompanyID", CompanyID);
			Params[2] = new SqlParameter("@BranchID", BranchID);
			Params[3] = new SqlParameter("@ListType", "approved");

			DataSet DataSet = new DataSet();

			if(CompanyID == 4) DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoASL]", Params);
			else if(CompanyID == 3) DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAIL]", Params);
			else DataSet = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAAML]", Params);

			var AllCoAList = CustomConvert.DataSetToList<CoAReverseDto>(DataSet.Tables[0]).ToList().OrderByDescending(c=>c.GLCode).ToList();

			foreach (var child5 in AllCoAList)
			{
				if (child5.ParentGLCode != null)
				{
					child5.ParentCoA = AllCoAList.Where(c => c.GLCode == child5.ParentGLCode).FirstOrDefault();

					if (child5?.ParentCoA?.ParentGLCode != null)
					{
						child5.ParentCoA.ParentCoA = AllCoAList.Where(c => c.GLCode == child5.ParentCoA.ParentGLCode).FirstOrDefault();

						if (child5?.ParentCoA?.ParentCoA?.ParentGLCode != null)
						{
							child5.ParentCoA.ParentCoA.ParentCoA = AllCoAList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();

							if (child5?.ParentCoA?.ParentCoA?.ParentCoA?.ParentGLCode != null)
							{
								child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA = AllCoAList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();

								if (child5?.ParentCoA?.ParentCoA?.ParentCoA?.ParentCoA?.ParentGLCode != null)
								{
									child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA = AllCoAList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();
								}
								else
								{
									child5.IsLeafNode = true;
								}
							}
							else
							{
								child5.IsLeafNode = true;
							}
						}
						else
						{
							child5.IsLeafNode = true;
						}
					}
					else
					{
						child5.IsLeafNode = true;
					}
				}
			}

			foreach (var item in AllCoAList)
			{
				if (item.ParentCoA != null)
				{
					item.ParentCoATree.Add(item.ParentCoA.label);

					if (item.ParentCoA.ParentCoA != null)
					{

						item.ParentCoATree.Add(item.ParentCoA.ParentCoA.label);

						if (item.ParentCoA.ParentCoA.ParentCoA != null)
						{
							item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.label);

							if (item.ParentCoA.ParentCoA.ParentCoA.ParentCoA != null)
							{
								item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.label);

								if (item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA != null)
								{
									item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA.label);
								}
							}
						}
					}
				}
			}

			foreach (var item in AllCoAList)
			{
				item.ParentCoATree.Reverse();
			}

			SqlParameter[] sqlParams = new SqlParameter[2];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@ProductID", ProductID);

			DataSet DataSets = new DataSet();

			 if(CompanyID ==4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadSL]", sqlParams);
			 else if(CompanyID==3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadIL]", sqlParams);
			 else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAccLedgerHeadAML]", sqlParams);

			var AllLedgerList = CustomConvert.DataSetToList<AccLedgerHeadDto>(DataSets.Tables[0]).ToList();


			foreach (var item in AllLedgerList)
			{
				var coaItem = AllCoAList.Where(c => c.COAID.ToString() == item.COAID).FirstOrDefault();

				if(coaItem != null)
				{
					coaItem.ApprovalStatus = item.ApprovalStatus;
					coaItem.ApprovalSetID = item.ApprovalReqSetID;
					coaItem.IsSelectedInLedger = true;
					coaItem.EnablePaymentVoucher = item.EnablePaymentVoucher;
					coaItem.EnableBalanceCheck = item.EnableBalanceCheck;
					coaItem.EnableCollectionVoucher = item.EnableCollectionVoucher;
					coaItem.EnableJournalVoucher = item.EnableJournalVoucher;
					coaItem.EnableNAVProcess = item.EnableNAVProcess;
					coaItem.EnableBankAdjustmentVoucher = item.EnableBankAdjustmentVoucher;
					coaItem.ProductID = item.ProductID;
				}

			}

			//reseting parrent coa
			foreach (var item in AllCoAList) item.ParentCoA = null;

			return AllCoAList;
		}

		public async Task<string> ApproveCoA(string UserName, int CompanyID, int BranchID, ApproveCoA data)
		{
			DynamicParameters SpParameters = new DynamicParameters();
			
			SpParameters.Add("@UserName", UserName);
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
			SpParameters.Add("@COAIDs", data.COAIDs);
			SpParameters.Add("@ApprovalStatus", data.ApprovalStatus);
			SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
			SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

			if(CompanyID==4) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCoASL", SpParameters);
			else if(CompanyID == 3) return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCoAIL", SpParameters);
			else return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveCoAAML", SpParameters);
		}

		public async Task<object> SaveUpdateCoA(string UserName, int CompanyID, int BranchID, CoADto data)
		{
			if(data.COAID == 0)
			{
				DynamicParameters SpParameters = new DynamicParameters();
				SpParameters.Add("@COAID", data.COAID);
				SpParameters.Add("@UserName", UserName);
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);
				SpParameters.Add("@GLName", data.GLName);
				SpParameters.Add("@ParentGLCode", data.ParentGLCode);
				SpParameters.Add("@BalanceType", data.BalanceType);
				SpParameters.Add("@AccLevel", data.AccLevel);
				SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

				if(CompanyID ==4) await _dbCommonOperation.InsertUpdateBySP("CM_SaveCoASL", SpParameters);
				else if(CompanyID==3) await _dbCommonOperation.InsertUpdateBySP("CM_SaveCoAIL", SpParameters);
				else await _dbCommonOperation.InsertUpdateBySP("CM_SaveCoAAML", SpParameters);
			}
			else
			{
				DynamicParameters SpParameters = new DynamicParameters();
				SpParameters.Add("@COAID", data.COAID);
				SpParameters.Add("@UserName", UserName);
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);
				SpParameters.Add("@GLName", data.GLName);
				SpParameters.Add("@ParentGLCode", data.ParentGLCode);
				SpParameters.Add("@BalanceType", data.BalanceType);
				SpParameters.Add("@AccLevel", data.AccLevel);
				SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


				if (CompanyID == 4) await _dbCommonOperation.InsertUpdateBySP("CM_UpdateCoASL", SpParameters);
				else if (CompanyID == 3) await _dbCommonOperation.InsertUpdateBySP("CM_UpdateCoAIL", SpParameters);
				else await _dbCommonOperation.InsertUpdateBySP("CM_UpdateCoAAML", SpParameters);

				//await _dbCommonOperation.InsertUpdateBySP("CM_UpdateCoA", SpParameters);
			}

			return await GetCoAList(UserName, CompanyID, BranchID, "All");
		}

		public async Task<object> GetCoAList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			return GetAllCoAList(UserName, CompanyID, BranchID, ListType);
		}

		private object GetAllCoAList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", ListType);

			DataSet DataSets = new DataSet();

			if (CompanyID == 4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoASL]", sqlParams);
			else if (CompanyID == 3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAIL]", sqlParams);
			else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAAML]", sqlParams);



			var AllCoAList = CustomConvert.DataSetToList<CoADto>(DataSets.Tables[0]).ToList();

			var DistinctLevel = AllCoAList.Select(o => o.AccLevel).Distinct().OrderByDescending(c => c);

			foreach (var level in DistinctLevel)
				foreach (var coa in AllCoAList.Where(c => c.AccLevel == level))
					coa.nodes = AllCoAList.Where(a => a.ParentGLCode == coa.GLCode).ToList();

			AllCoAList = AllCoAList.Where(c => c.ParentGLCode == null || c.ParentGLCode == "0").ToList();


			return new
			{
				CoAList = AllCoAList,
				AllCoAList = DataSets.Tables[0]
			};
		}

		public async Task<object> AllCoAList(string UserName, int CompanyID, int BranchID, string ListType)
		{
			SqlParameter[] sqlParams = new SqlParameter[4];

			sqlParams[0] = new SqlParameter("@UserName", UserName);
			sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
			sqlParams[2] = new SqlParameter("@BranchID", BranchID);
			sqlParams[3] = new SqlParameter("@ListType", "all");

			DataSet DataSets = new DataSet();
			
			if(CompanyID == 4) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoASL]", sqlParams);
			else if(CompanyID == 3) DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAIL]", sqlParams);
			else DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListCoAAML]", sqlParams);

			var AllList = CustomConvert.DataSetToList<CoAReverseDto>(DataSets.Tables[0]).ToList();

			var UnApprovedList = AllList.Where(c=>c.ApprovalStatus.ToLower() == ListType.ToLower()).ToList();

			foreach (var child5 in UnApprovedList)
			{
				if (child5.ParentGLCode != null)
				{
					child5.ParentCoA = AllList.Where(c => c.GLCode == child5.ParentGLCode).FirstOrDefault();
					
					if (child5.ParentCoA.ParentGLCode != null)
					{
						child5.ParentCoA.ParentCoA = AllList.Where(c => c.GLCode == child5.ParentCoA.ParentGLCode).FirstOrDefault();

						if(child5.ParentCoA.ParentCoA.ParentGLCode != null)
						{
							child5.ParentCoA.ParentCoA.ParentCoA = AllList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();

							if(child5.ParentCoA.ParentCoA.ParentCoA.ParentGLCode != null)
							{
								child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA = AllList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();

								if(child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentGLCode != null )
								{
									child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA = AllList.Where(c => c.GLCode == child5.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentGLCode).FirstOrDefault();
								}
								else
								{
									child5.IsLeafNode = true;
								}
							}
							else
							{
								child5.IsLeafNode = true;
							}
						}
						else
						{
							child5.IsLeafNode = true;
						}
					}
				}
			}

			foreach(var item in UnApprovedList)
			{
				if(item.ParentCoA != null)
				{
					item.ParentCoATree.Add(item.ParentCoA.label);

					if(item.ParentCoA.ParentCoA != null){

						item.ParentCoATree.Add(item.ParentCoA.ParentCoA.label);

						if(item.ParentCoA.ParentCoA.ParentCoA != null)
						{
							item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.label);

							if(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA != null)
							{
								item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.label);

								if(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA != null)
								{
									item.ParentCoATree.Add(item.ParentCoA.ParentCoA.ParentCoA.ParentCoA.ParentCoA.label);
								}
							}
						}
					}
				}
			}

			foreach (var item in UnApprovedList)
			{
				item.ParentCoATree.Reverse();
			}

				return new { CoAList = UnApprovedList };
		}

		

		
	}
}
