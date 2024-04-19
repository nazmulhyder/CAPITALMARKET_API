using Microsoft.AspNetCore.Http;
using Model.DTOs.AccountSettlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
	public interface IAccountSettlementRepository
	{
		public Task<object> BulkUploadAccountClosureUpload(string UserName, int CompanyID, int BranchID, List<AccountClosureReqDto> accountList);
		public Task<object> BulkUploadAccountClosureValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> accountList);

		public Task<object> ExecuteAccountClosure(string UserName, int CompanyID, int BranchID,string ApprovalStatus, List<AccountClosureReqDto> data);

		public Task<object> ListAccountClosure(string UserName, int CompanyID, int BranchID, string listtype);

		public Task<object> saveAccountClosure(string UserName, int CompanyID, int BranchID, IFormCollection data);

		public Task<object> GetAccountClosureDetail(string UserName, int CompanyID, int BranchID, string AccountNo, int AgrClosureReqID);



		public Task<object> ListAccountSuspensionWithdrawalRequest(string UserName, int CompanyID, int BranchID, string listtype);

		public Task<object> saveAccountSuspensionWithdrawalRequest(string UserName, int CompanyID, int BranchID, IFormCollection data);

		public Task<object> ApproveAccountSuspensionWithdrawal(string UserName, int CompanyID, int BranchID, AccountSuspensionWithdrawalApprovalDto data);

		public Task<object> BulkUploadAccountSuspensionWithdrawalValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data);

		public Task<object> BulkUploadAccountSuspensionWithdrawal(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data);

		public Task<object> ListAccountSuspensionForWithdrawal(string UserName, int CompanyID, int BranchID);



		public Task<object> GetAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, string AccountNo, int AgrSuspensionID);

		public Task<object> SaveUpdateAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, IFormCollection data);

		public Task<object> BulkUploadAccountSuspensionValidation(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data);

		public Task<object> BulkUploadAccountSuspension(string UserName, int CompanyID, int BranchID, List<AccountSuspensionBulkDto> data);
		
		public Task<object> ListAccountSuspensionSL(string UserName, int CompanyID, int BranchID, string listtype);

		public Task<object> ApproveAccountSuspensionDetail(string UserName, int CompanyID, int BranchID, AccountSuspensionApprovalDto data);

	}
}
