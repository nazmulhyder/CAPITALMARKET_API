using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.InstrumentConversion;
using Model.DTOs.LockUnlock;

namespace Service.Interface
{
    public interface IInstrumentConversionRepository
    {
        public Task<string> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID, string Maker);
        public InstrumentConversionDeclarationDTO CMInstrumentConversionListbyID(int InstConversionID, int CompanyID, int BranchID);
        public Task<List<InstrumentConversionDeclarationListDTO>> GetInstConversionDeclarationList(int CompanyID, int BranchID);
        public Task<string> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID, string UserName);
        public Task<List<CMInstrumentConversionDTO>> CMInstrumentConversionList(int InstConversionID, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCMInstrumentConversion(CMInstrumentConversionInsert entryIntrumentConversion, int CompanyID, int BranchID, string Maker);
        public Task<List<CMInstrumentConversion>> CMInstrumentConversionApprovalList(int InstConversionID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMApprovednstrumentConversion(CMInstrumentConversionApprove CMInstrumentConversionApprove, int CompanyID, int BranchID, string Maker);
        public Task<string> GetInstrumentConversionTotalCost(CMInstrumentConversionUpdateBaseQuantityDTO objInsConversionUpdate, int CompanyID, int BranchID, string Maker);
        public Task<string> InsSplitDeclaration(InstrumentSplitDeclarationDTO EntryISDDTO, int CompanyID, int BranchID, string Maker);
        public InstrumentSplitDeclarationDTO GetCMInstrumentSplitListbyID(int InstSplitedID, int CompanyID, int BranchID);
        public Task<List<InstrumentSplitDeclarationDTO>> GetInstSplitDeclarationList(int CompanyID, int BranchID);
        public Task<String> InstrumentSplitDeclarationApproved(InstrumentSplitDeclarationApprove InsSplitDeclarationList, int CompanyID, int BranchID, string UserName);
        public Task<List<CMInstrumentSplitDTO>> CMInstrumentSplitList(int InstSplitedID, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCMInstrumentSplit(CMInstrumentSplitInsert entryIntrumentSplit, int CompanyID, int BranchID, string Maker);
        public Task<List<CMInstrumentSplit>> CMInstrumentSplitApprovalList(int InstSplitedID, int CompanyID, int BranchID, string Maker);
        public Task<string> GetInstrumentSplittedQuantity(CMInstrumentSplitUpdateSpittedQuantityDTO objInsSplitUpdate, int CompanyID, int BranchID, string Maker);
        public Task<string> GetCMApprovednstrumentSplit(CMInstrumentSplitApprove objCMApprovedInstrumentSplit, int CompanyID, int BranchID, string Maker);
    }
}
