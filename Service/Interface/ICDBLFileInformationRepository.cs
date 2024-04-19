using Microsoft.AspNetCore.Http;
using Model.DTOs;
using Model.DTOs.Approval;
using Model.DTOs.Charges;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.InstrumentGroup;
using Model.DTOs.IPO;
using Model.DTOs.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICDBLFileInformationRepository
    {
        public Task<object> getCDBLFileInformationList(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, string CDBLFilePath);

        public Task<object> ProcessCDBLFile(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data);
        public Task<List<CDBLListDTO>> GetCDBLIPOFileDataList(int IPOInstrumentID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCDBLUpdateIPOFiledata(CMCDBLUpdateIPOFiledataMaster objCDBLUpdateIPOFiledata, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCDBLFileProcess(CMCDBLUpdateIPOFiledataProcess objGetCDBLFileProcess, int CompanyID, int BranchID, string Maker);
        public Task<List<CDBLRightsListDTO>> GetCDBLRightsFileApprovalList(int RightSettingID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCDBLUpdateRightsData(CMCDBLUpdateRightsMaster objCDBLUpdateRightsData, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCDBLRightsProcess(CMCDBLRightsProcess objCDBLRightsProcess, int CompanyID, int BranchID, string Maker);
        public Task<List<CDBLTransferListDTO>> getCMCDBLTransferTransmissionApprovalList(string TransactionDateFrom, string TransactionDateTo, string Status,int CDBLFileInfoID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCDBLUpdatePriceTransferTransmissionInstrument(SLTransferTransmissionInstrumentMaster objSLApprovedTransferTransmissionInstrument, int CompanyID, int BranchID, string Maker);
        public Task<string> GetSLApprovedTransferTransmissionInstrument(SLApprovedTransferTransmissionInstrumentDTO objSLApprovedTransferTransmissionInstrument, int CompanyID, int BranchID, string Maker);
        public Task<string> TransferTransmissionInstrument(CMCDBLTransferTransmissionDTO? entryTransferTransmissionInstrument, int CompanyID, int BranchID, string UserName);
        public CDBLInstrumentListID InstrumentInfoListbyID(int InstrumentID, int ContractID, string TransactionDate, int CompanyID, int BranchID);
        public Task<object> ValidateCDBLFile(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data);
        public Task<List<AMLBankAccountDTO>> GetAMLMFBankAccountList(int FundID, string UserName);
        public Task<List<InstrumentDto>> GetInstrumentInfobyTypeCodeList(int TypeCode,string AccountNumber,int ProductID, int CompanyID, int BranchID, string UserName);
    }
}
