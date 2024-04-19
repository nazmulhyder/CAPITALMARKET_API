using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.PhysicalInstrumentCollectionDelivery;

namespace Service.Interface
{
    public interface IPhysicalInstrumentCollectionDeliveryRepository
    {
        public Task<string> InsertCMPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryDTO entryPhysIntrumentCollection, int CompanyID, int BranchID, string Maker);
        public Task<List<PhysicalInstrumentCollectionDeliveryDTO>> CMPhysicalInstrumentCollectionList(string Status,int CompanyID, int BranchID, string Maker);
        public PhysicalInstrumentCollectionDeliveryDTO CMPhysicalInstrumentCollectionListbyID(int PhyInstCollID, int CompanyID, int BranchID);
        public Task<string> GetCMApprovedPhysicalInstrumentCollection(PhysicalInstrumentCollectionDeliveryApprove objCMApprovedPhysicalInstrumentCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> InsertCMPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryDTO entryIntrumentDelivery, int CompanyID, int BranchID, string Maker);
        public Task<List<PhysicalInstrumentDeliveryDTO>> CMPhysicalInstrumentDeliveryList(string Status,int CompanyID, int BranchID, string Maker);
        public PhysicalInstrumentDeliveryDTO CMPhysicalInstrumentDeliveryListbyID(int PhyInstDelID, int CompanyID, int BranchID);
        public Task<string> GetCMApprovedPhysicalInstrumentDelivery(PhysicalInstrumentDeliveryApprove objCMApprovedPhysicalInstrumentDelivery, int CompanyID, int BranchID, string Maker);
        //public Task<string> GetUnitFundCollection(UnitFundCollectionDTO objUnitFundCollection, int CompanyID, int BranchID, string Maker);
        //public Task<string> GetApprovedUnitFundCollection(UnitFundCollectionApprove objApprovedUnitFundCollection, int CompanyID, int BranchID, string Maker);
        //public Task<string> GetUnitFundDelivery(UnitFundDeliveryDTO objUnitFundDelivery, int CompanyID, int BranchID, string Maker);
        //public Task<List<UnitFundDeliveryListDTO>> GetUnitFundDeliveryList(int CompanyID, int BranchID, string Maker);
        //public UnitFundDeliveryDTObyID GetCMUnitFundDeliveryListByID(int UnitFundDelID, int CompanyID, int BranchID);
        //public Task<string> GetApprovedUnitFundDelivery(UnitFundDeliveryApprove objApprovedUnitFundDelivery, int CompanyID, int BranchID, string Maker);

    }
}
