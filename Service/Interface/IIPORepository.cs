using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Instrument;
using Model.DTOs.IPO;
using Model.DTOs.NAVFileUpload;
using Model.DTOs.Right;

namespace Service.Interface
{
   
    public interface IIPORepository
    {
        public Task<string> InsertIPO(IPODTO EntryIPODTO , int CompanyID, int BranchID, string UserName );
        //public Task<string> InsertIPO1(IPODetailsDTO data, int CompanyID, int BranchID, string UserName );
        public Task<List<IPODTO>> GetAllIPOInstrument(int CompanyID, int BranchID); //
        public IPODTO GetIPOInstrumentById(int IPOInstrumentID); //
        public object GetIPOAccountInfo(int ProductId, string AccountNumber, int CompanyID, int BranchID); //

        public IPOInstrumentInfo GetIPOInstrumentInfo(IPOInvestorInfo EntryIPOInvestorInfo, int CompanyID, int BranchID); //
        public Task<string> InsertIPOApplication(IPOApplication entryIPOInvestorInstrumentInfo,int CompanyID,int BranchID, string UserName); //
        public Task<List<IPOOrderApproved>> GetIPOApplicationOrderList(int IPOInstrumentID, string Status, int CompanyID, int BranchID); //
        public IPOApplicationListID IPOApplicationListbyID(int IPOApplicationID, int CompanyID, int BranchID);
        public Task<List<IPODTO>> IPOInstrumentListforApproval(int CompanyID);
        public Task<string> GetIPOInstrumentApproval(IPOInstrumentApproval objIPOInstrumentApproval, string UserName);
        public Task<string> IPOApplicationApproved(IPOApproval IPOOrderApprovedList, int CompanyID, int BranchID, string UserName);
        public Task<string> IPOApplicationApprovedbyID(IPOApproval IPOOrderApprovedList,int IPOApplicationID, int CompanyID, int BranchID, string UserName);
        //public IPOApplicationListID IPOApplicationListsbyID(int IPOApplicationID);
        // IIPORepository StartO
        public Task<List<IPOBulkApproved>> GetIPOApplicationBulkList(int ProductID, int IPOInstrumentID, string Maker, int CompanyID, int BranchID); //
        public Task<string> InsertIPOBulk(IPOBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID, string UserName); //

        public Task<string> InsertIPOResultFileText(IFormFile formdata, int IPOInstrumentID, int CompanyID, int BranchID, string UserName);
        public Task<List<IPOListResultforAllocationRefund>> GetResultforAllocationRefundList(int IPOInstrumentID, string Maker, int CompanyID, int BranchID); //
        public  Task<string> InsertIPOResult(IPOResultMaster entryIPOResult, int CompanyID, int BranchID, string UserName);
        public Task<List<IPOReversalDTO>> GetIPOReversalList(int CompanyID, int BranchID, string UserName, string IPOType);
        public Task<string> InsertReversalApproval(IPOReversalMaster entryIPOReversal, int CompanyID, int BranchID, string UserName);
        public Task<List<IPOBookBuildingDTO>> GetIPOBookBuildingList( int IPOInstrumentID);
        public Task<string> InsertApplicationSubscription(IPOBookBuildingAppSubscriptionIPO entryIPOAppSubscription, string Maker, int CompanyID, int BranchID); //
        public IPOBookBuildingAllotmentDTO GetIPOBookBuildingAllotmentInfo(int IPOInstrumentID, int ContractID, int CompanyID, int BranchID); //
        public Task<string> InsertApplicationAllotMent(IPOBookBuildingAllotmentInsDTO entryIPOBookBuildingAllotment, string Maker,int CompanyID, int BranchID); //
        public Task<List<IPOBookBuildingAllotmentList>> GetIPOSubsCriptionforApprovalList(int IPOInstrumentID, string Status, int CompanyID, int BranchID); //
        public SubscriptionListByIdDTO GetIPOSubscriptionListbyID(int SubscriptionID, int CompanyID, int BranchID); //
        public Task<string> ApproveBookBuildingRefundMaster(BookBuildingRefundMaster entryBookBuildingRefundMaster, string UserName, int CompanyID, int BranchID); //
        public Task<List<AMLCollection>> GetAMLIPOCollectionList(int IPOInstrumentID,string IPOType, int CompanyID, int BranchID);
        public Task<string> AMLCollectionApproveRequest(AMLCollectionMaster AMLCollectionReq, int CompanyID, int BranchID, string Maker);
        public Task<List<AMLCollectionApprovalList>> GetInstrumentCollectionlList(int IPOInsturmentID,string IPOType, int CompanyID, int BranchID, string UserName);
        public Task<String> AMLIPOInstrumentApproved(AMLCollectionApproved AMLIPOInstrumentList, int CompanyID, int BranchID, string Maker);
        public Task<object> IPOFileGenerate(int IPOInstrumentID, int CompanyID, int BranchID, string UserName);
        public IPODTO GetIPOInstrumentSetupInfo(int IPOInstrumentID, string IPOType, int CompanyID, int BranchID);
        public Task<List<IPOOrderApproved>> GetIPOApplicationWaitingSubmissionList(int IPOInstrumentID, string Status, int CompanyID, int BranchID);
        public Task<List<IPOOrderApproved>> GetIPOSubmittedApplicationList(int IPOInstrumentID, string Status, int CompanyID, int BranchID);
        public Task<object> GetIPOApplicationFileValidation(string UserName, int CompanyID, int BranchID, List<IPOApplicationFileDetailDto> data);
        public Task<object> SaveIPOApplicationFile(string UserName, int CompanyID, int BranchID,int InstrumentID, IFormCollection data);
        public Task<List<IPOApplicationEligibleList>> GetIPOEligibleInvestorList(int IPOInsSettingID,string EnableDisable, int CompanyID, int BranchID, string UserName);
        public Task<string> InsertEligibleIPOBulk(IPOEligibleBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID, string UserName);
        public Task<List<IPOBookBuildingAllotmentList>> GetAMLIPOSubsCriptionList(int IPOInstrumentID, int CompanyID, int BranchID);
        public Task<String> AMLIPOSubscriptionCollectionApproved(AMLCollectionApprovedDTO AMLIPOInstrumentList, int CompanyID, int BranchID, string Maker);
        public Task<List<ListTransferAmountDTO>> GetCMListTransferfundamountbyIPORights(string TransactionType, int CompanyID, int BranchID);
        public Task<List<AMLBankAccountInfoDTO>> GetAMLBankAccountInformationByID(int MFBankAccountID, int CompanyID, int BranchID);
        public Task<List<ListTransferDTO>> GetFundTransferToIssuerList(string TransactionType, int InstrumentID, int CompanyID, int BranchID);
        public Task<String> SLILSaveFundTransferToIssuer(SLILFundTransferToIssuerDTO SLILFundTransfer, int CompanyID, int BranchID, string Maker);
        public Task<string> ApproveBookBuildingRefund(BookBuildingRefundMaster entryBookBuildingRefundMaster, string UserName, int CompanyID, int BranchID);
        // IIPORepository End

    }
}
