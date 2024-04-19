using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.UnitFundCollectionDelivery;

namespace Service.Interface
{
    public interface IUnitFundCollectionDeliveryRepository
    {
        public Task<string> GetUnitFundCollection(UnitFundCollectionDTO objUnitFundCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> GetApprovedUnitFundCollection(UnitFundCollectionApprove objApprovedUnitFundCollection, int CompanyID, int BranchID, string Maker);
        public Task<string> GetUnitFundDelivery(UnitFundDeliveryDTO objUnitFundDelivery, int CompanyID, int BranchID, string Maker);
        public Task<List<UnitFundCollectionListDTO>> GetUnitFundCollectionList(string Status, int CompanyID, int BranchID, string Maker);
        public UnitFundCollectionDTObyID CMUnitFundCollectionListByID(int UnitFundCollID, int CompanyID, int BranchID);
        public Task<List<UnitFundDeliveryListDTO>> GetUnitFundDeliveryList(string Status,int CompanyID, int BranchID, string Maker);
        public UnitFundDeliveryDTObyID GetCMUnitFundDeliveryListByID(int UnitFundDelID, int CompanyID, int BranchID);
        public Task<string> GetApprovedUnitFundDelivery(UnitFundDeliveryApprove objApprovedUnitFundDelivery, int CompanyID, int BranchID, string Maker);
    }
}
