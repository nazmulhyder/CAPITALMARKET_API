using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.IPO;
using Model.DTOs.LockUnlock;
using Model.DTOs.Remat;
using Service.Implementation;

namespace Service.Interface
{
    public interface ILockUnlockRepository
    {
        //public Task<string> GetRematCollection(RematDTO objRematCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<LockUnlockInstrumentListDTO>> GetLockUnlockInstrumentList( int CompanyID, int BranchID, int ContractID, string LockUnlockStatus, string Maker);
        public Task<string> PostCMApprovedLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID, string Maker);
        public Task<string> PostCMUpdateLockingQuantity(CMLockInstrumentDTO objCMLockInstrumentDTO, int CompanyID, int BranchID, string Maker);
        public Task<List<LockInstrumentListDTO>> GetLockingApprovalList(string LockInstrumentStatus, int CompanyID, int BranchID, string Maker);
        public Task<string> PostLockingInstruemnt(LockInstrumentListDTO objLockInstrumentListDTO, int CompanyID, int BranchID, string Maker);
        public Task<string> PostUnlockingInstruemnt(UnlockInstrumentListDTO objUnlockInstrumentListDTO, int CompanyID, int BranchID, string Maker);
        public Task<List<UnlockInstrumentListDTO>> GetUnlockingApprovalList(string UnlockInstrumentStatus, int CompanyID, int BranchID, string Maker);
        public Task<string> PostCMUpdateUnlockingQuantity(CMUnLockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID, string Maker);
        public Task<string> PostCMApprovedUnlockingQuantity(CMUnlockInstrumentDTO objCMUnlockInstrumentDTO, int CompanyID, int BranchID, string Maker);
        public LockInstrumentListDTO CMLockingInstrumentListbyID(int LockingId, int CompanyID, int BranchID);
        public UnlockInstrumentListDTO CMUnlockingInstrumentListbyID(int UnLockingId, int CompanyID, int BranchID);
        //public Task<string> InsConversionDeclaration(InstrumentConversionDeclarationDTO EntryICDDTO, int CompanyID, int BranchID, string Maker);
        //public InstrumentConversionDeclarationDTO CMInstrumentConversionListbyID(int InstConversionID, int CompanyID, int BranchID);
        //public Task<List<InstrumentConversionDeclarationListDTO>> GetInstConversionDeclarationList(int CompanyID, int BranchID);
        //public Task<string> InstrumentDeclarationApproved(InstrumentConversionDeclarationApprove InsConvDeclarationList, int CompanyID, int BranchID, string UserName);
    }
}
