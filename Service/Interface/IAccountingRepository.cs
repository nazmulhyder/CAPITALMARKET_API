using Microsoft.AspNetCore.Http;
using Model.DTOs.Accounting;
using Model.DTOs.Approval;
using Model.DTOs.Charges;
using Model.DTOs.CoA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountingRepository
	{
        public Task<object> GetBulkVoucherValidation(string UserName, int CompanyID, int BranchID, List<VoucherBulkDto> vouchers);

        public Task<object> ListAccLedgerHeadForVoucherFilter(string UserName, int CompanyID, int BranchID);

		public Task<string> ApproveAccAgreement(string UserName, int CompanyID, int BranchID, AccAgreementApproveDto data);

		public Task<string> saveAgreement(string UserName, int CompanyID, int BranchID, bool IsFundAccount, AccCifListDto data);

		public Task<List<AccCifListDto>> ListCif(string UserName, int CompanyID, int BranchID, string SearchKeyword);

		public Task<object> GetGenAgreement(string UserName, int CompanyID, int BranchID, int ContractID, bool IsFundAccount);

		public Task<object> ListGenAgreement(string UserName, int CompanyID, int BranchID, bool IsFundAccount, string ListType);

		public Task<object> Listproduct(string UserName, int CompanyID, int BranchID, bool IsCompanyAsProductShow);

		public Task<string> SaveReverseAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherDto data);

		public Task<object> ApproveAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherApproveDto data);
        
        public Task<object> GetAccVoucher(string UserName, int CompanyID, int BranchID, string VoucherRefNo);

		public Task<object> ListAccVoucher(string UserName, int CompanyID, int BranchID, AccVoucherListParameterDto data);
        
        public Task<string> SaveAccVoucher(string UserName, int CompanyID, int BranchID, string IssueType, bool IsPartyVoucher, AccVoucherDto data);
        
        public Task<object> GetLedgerHeadsForAccVoucher(string UserName, int CompanyID, int BranchID, string AccountNumber, string VoucherType);

        public Task<object> SearchAccount(string UserName, int CompanyID, int BranchID, string data);

		public Task<string> ApproveVoucherPostingDateChange(string UserName, int CompanyID, int BranchID, ApproveVoucherPostingDateChangeDto data);

        public Task<string> SaveVoucherPostingDateChange(string UserName, int CompanyID, int BranchID, VoucherPostingDateChangeDto data);

        public Task<object> GetVoucherPostingDate(string UserName, int CompanyID, int BranchID);

        public Task<object> GetVoucherPostingDateChangeList(string UserName, int CompanyID, int BranchID, string ListType);

        public Task<string> ApproveAccLedgerHead(string UserName, int CompanyID, int BranchID, ApproveAccLedgerHead data);

		public Task<object> ListAccLedgerHeadAll(string UserName, int CompanyID, int BranchID, string ListType);

		public Task<string> SaveUpdateAccLedgerHead(string UserName, int CompanyID, int BranchID, AccLedgerHeadDto data);

		public Task<string> SaveUpdateAccLedgerHeadBulk(string UserName, int CompanyID, int BranchID,int ProductID, List<AccLedgerHeadBulkDto> data);

		public Task<List<CoAReverseDto>> ListAccLedgerHead(string UserName, int CompanyID, int BranchID, int ProductID);
       
        public Task<string> ApproveCoA(string UserName, int CompanyID, int BranchID, ApproveCoA data);

        public Task<object> SaveUpdateCoA(string UserName, int CompanyID, int BranchID, CoADto data);

        public Task<object> GetCoAList(string UserName, int CompanyID, int BranchID, string ListType);
        
        public Task<object> AllCoAList(string UserName, int CompanyID, int BranchID, string ListType);
        
    }
}
