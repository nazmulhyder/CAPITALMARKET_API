using Microsoft.AspNetCore.Http;
using Model.DTOs.EquityIncorporation;
using Model.DTOs.FundCollection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFundCollectionRepository
    {
        public Task<object> GetFundCollectionModeList(string UserName, int CompanyID, int BranchID);

        public Task<object> GetChequeDishonorReasonList(string UserName, int CompanyID, int BranchID);
		
		public Task<List<ProductDto>> GetProductList(string UserName, int CompanyID);
        public Task<object> GetFundCollectionList(string UserName, FundCollectionListDto filterData);
        public Task<object> GetFundCollectionListIL(string UserName, FundCollectionListDto filterData);
        public Task<object> GetFundCollectionListAML(string UserName, FundCollectionListDto filterData);
        public Task<object> GetAgreementInfo(int CompanyID, int ProductID, string AccountNumber);
        public Task<string> AddUpdateFundCollection(FundCollectionDto data, string UserName, int CompanyID, int BranchID);
        public Task<string> AddUpdateFundCollectionAML(FundCollectionDto data, string UserName, int CompanyID, int BranchID);
        public Task<string> AddUpdateFundCollectionIL(FundCollectionDto data, string UserName, int CompanyID, int BranchID);
        public Task<string> ApproveFundCollection(string UserName, FundCollectionApprovalDto approveData, int CompanyID);
        public Task<string> ApproveFundCollectionAML(string UserName, FundCollectionApprovalDto approveData, int CompanyID);
        public Task<string> ApproveFundCollectionIL(string UserName, FundCollectionApprovalDto approveData, int CompanyID);
        public Task<object> GetBankAccountList(int CompanyID, int ProductID, int FundID,string stringSetupLevel);
        public Task<FundCollectionDto> GetFundCollectionDetail(int CollectionInfoID, int CompanyID);
        public Task<FundCollectionDto> GetFundCollectionDetailAML(int CollectionInfoID, int CompanyID);
        public Task<FundCollectionDto> GetFundCollectionDetailIL(int CollectionInfoID, int CompanyID);
        public Task<object> BankStatementUpload(IFormCollection data, string UserName);
        public Task<object> BankStatementUploadAML(IFormCollection data, string UserName);
        public Task<object> BankStatementUploadIL(IFormCollection data, string UserName);
        public Task<object> UploadedStatement(string date, int CompanyID, int BranchID, string UserName);
        public Task<object> UploadedStatementIL(string date, int CompanyID, int BranchID, string UserName);
        public Task<object> UploadedStatementAML(string date, int CompanyID, int BranchID, string UserName);
        public Task<object> InstallmentCollectionListForScheduleTagging(string username, int CompanyID, string ListType, string AccountNumber);
        public Task<object> InstallmentCollectionListForScheduleTaggingIL(string username, int CompanyID, string ListType, string AccountNumber);
        public Task<object> InstallmentCollectionListForScheduleTaggingAML(string username, int CompanyID, string ListType, string AccountNumber);
        public Task<string> SaveScheduleInstallmentTagIL(string UserName, List<InstallmentScheduleDto> Installmentlist, int MoninstrumentID, int CompanyID, int BranchID);
        public Task<string> SaveScheduleInstallmentTagAML(string UserName, List<InstallmentScheduleDto> Installmentlist, int MoninstrumentID, int CompanyID, int BranchID);
        public Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagList(string UserName, int CompanyID, string ListType);
        public Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagListAML(string UserName, int CompanyID, string ListType);
        public Task<List<SchedulenstrumentDto>> GetScheduleInstallmentTagListIL(string UserName, int CompanyID, string ListType);
        public Task<string> ApproveScheduleInstallmentTagList(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID);
        public Task<string> ApproveScheduleInstallmentTagListAML(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID);
        public Task<string> ApproveScheduleInstallmentTagListIL(string UserName, ScheduleInstallmentTagApprovalDto data, int CompanyID);


        public Task<object> GetScheduleInstallmentListForDDIFileAML(string UserName, int CompanyID, int BranchID, int ProductID, string ListType, string ScheduleDueDateFrom, string ScheduleDueDateTo);
        public Task<object> GetScheduleInstallmentListForDDIFileIL(string UserName, int CompanyID, int BranchID, int ProductID, string ListType, string ScheduleDueDateFrom, string ScheduleDueDateTo);
        public Task<object> GenerateDDIFile(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName);
        public Task<object> GenerateDDIFileAML(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName);
        public Task<object> GenerateDDIFileIL(List<ScheduleListForDDIFileDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName);

        public Task<object> GetScheduleInstallmentListFor2ndDDIFile(string UserName, int CompanyID, int BranchID, string ScheduleDueDate);
        public Task<object> GetScheduleInstallmentListFor2ndDDIFileAML(string UserName, int CompanyID, int BranchID, int ProductID, string ScheduleDueDateFrom, string ScheduleDueDateTo);
        public Task<object> GetScheduleInstallmentListFor2ndDDIFileIL(string UserName, int CompanyID, int BranchID, int ProductID, string ScheduleDueDateFrom, string ScheduleDueDateTo);
        public Task<string> UnlockFor2ndDDIFile(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID);
        public Task<string> UnlockFor2ndDDIFileAML(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID);
        public Task<string> UnlockFor2ndDDIFileIL(List<ScheduleListForDDIFileDto> data, string UserName, int CompanyID, int BranchID);

     
        public Task<object> DDIFileUpload(IFormCollection data, string UserName);


        public Task<object> DDIFileList(string UserName, int CompanyID, int BranchID, string Status);
        public Task<object> DDIFileListAML(string UserName, int CompanyID, int BranchID, string Status, string FromDate, string ToDate, int ProductID);
        public Task<object> DDIFileListIL(string UserName, int CompanyID, int BranchID, string Status, string FromDate, string ToDate, int ProductID);

        public Task<object> DDIFileApprove(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus);
        public Task<object> DDIFileApproveAML(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus);
        public Task<object> DDIFileApproveIL(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus);

       

    }
}
