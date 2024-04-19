using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.SecurityElimination;

namespace Model.DTOs.PhysicalInstrumentCollectionDelivery
{
    public class PhysicalInstrumentCollectionDeliveryDTO
    {
        public int? SL { get; set; }
        public int? PhyInstCollID { get; set; }
        public string? CollectionDate { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? ReferenceNo { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? AverageRate { get; set; }
        public string? CertificateNo { get; set; }
        public string? DestinationNoFrom { get; set; }
        public string? DestinationNoTo { get; set; }
        public string? TotalCertificateNo { get; set; }
        public string? FolioNo { get; set; }
        public int? InstrumentLedgerID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? Remarks { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate{ get; set; }

        public decimal? AvailableBalance { get; set; }
        public string? BOID { get; set; }
        public string? InvestorCategory { get; set; }

    }

    public class PhysicalInstrumentDeliveryDTO
    {
        public int? SL { get; set; }
        public int? PhyInstDelID { get; set; }
        public string? DeliveryDate { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public string? ReferenceNo { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? AverageRate { get; set; }
        public string? CertificateNo { get; set; }
        public string? DestinationNoFrom { get; set; }
        public string? DestinationNoTo { get; set; }
        public string? TotalCertificateNo { get; set; }
        public string? FolioNo { get; set; }
        public int? InstrumentLedgerID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? Remarks { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }

        public decimal? AvailableBalance { get; set; }
        public string? BOID { get; set; }
        public string? InvestorCategory { get; set; }
    }

    public class PhysicalInstrumentCollectionDeliveryInsertDTO
    {
        public List<PhysicalInstrumentDeliveryDTO>? CMPhysicalInstrumentCollectionDeliveryList { get; set; }
    }

    public class PhysicalInstrumentDeliveryInsertDTO
    {
        public List<PhysicalInstrumentDeliveryDTO>? CMPhysicalInstrumentDeliveryList { get; set; }
    }

    public class PhysicalInstrumentCollectionDeliveryApprove
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<PhysicalInstrumentCollectionDeliveryDTO>? CMPhysicalInstrumentCollectionDeliveryApproveList { get; set; }
    }

    public class PhysicalInstrumentDeliveryApprove
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<PhysicalInstrumentDeliveryDTO>? CMPhysicalInstrumentDeliveryApproveList { get; set; }
    }

    //public class UnitFundCollectionDTO
    //{
    //    public int? UnitFundCollID { get; set; }
    //    public int? InstrumentLedgerID { get; set; }
    //    public int? ContractID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public string? Remarks { get; set; }
    //}

    //public class UnitFundCollectionListDTO
    //{
    //    public int? SL { get; set; }
    //    public int? UnitFundCollID { get; set; }
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? BOID { get; set; }
    //    public string? MemberName { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public string? TransactionDate { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public string? InstrumentName { get; set; }
    //    public string? Remarks { get; set; }
    //    public string? EnableDisable { get; set; }
              
    //}

    //public class UnitFundCollectionDTObyID
    //{
    //    public int? SL { get; set; }
    //    public int? UnitFundCollID { get; set; }
    //    public int? ContractID { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? MemberName { get; set; }
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public string? InstrumentName { get; set; }
    //    public decimal? TotalQuantity { get; set; }
    //    public decimal? FreeQuantity { get; set; }
    //    public decimal? BlockQuantity { get; set; }
    //    public decimal? ReceivableQuantity { get; set; }
    //    public decimal? FreezeQuantity { get; set; }
    //    public decimal? LockinQuantity { get; set; }
    //    public decimal? PledgeQuantity { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public int? InstrumentLedgerID { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public string? Remarks { get; set; }
    //    public string? Maker { get; set; }
    //    public string? MakeDate { get; set; }
    //    public string? BOID { get; set; }
    //    public decimal? AvailableBalance { get; set; }
    //    public string? InvestorCategory { get; set; }
    //}

    //public class UnitFundCollectionApprove
    //{
    //    //public int? UnitFundCollID { get; set; }
    //    public String? Status { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public List<UnitFundCollectionListDTO>? CMUnitFundCollectionApproveList { get; set; }
    //}

    //public class UnitFundDeliveryDTO
    //{
    //    public int? UnitFundDelID { get; set; }
    //    public int? InstrumentLedgerID { get; set; }
    //    public int? ContractID { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public string? Remarks { get; set; }
    //}

    //public class UnitFundDeliveryListDTO
    //{
    //    public int? SL { get; set; }
    //    public int? UnitFundDelID { get; set; }
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? BOID { get; set; }
    //    public string? MemberName { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public string? TransactionDate { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public string? InstrumentName { get; set; }
    //    public string? Remarks { get; set; }
    //    public string? EnableDisable { get; set; }
    //}

    //public class UnitFundDeliveryDTObyID
    //{
    //    public int? SL { get; set; }
    //    public int? UnitFundDelID { get; set; }
    //    public int? ContractID { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? MemberName { get; set; }
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set; }
    //    public int? InstrumentID { get; set; }
    //    public string? InstrumentName { get; set; }
    //    public decimal? TotalQuantity { get; set; }
    //    public decimal? FreeQuantity { get; set; }
    //    public decimal? BlockQuantity { get; set; }
    //    public decimal? ReceivableQuantity { get; set; }
    //    public decimal? FreezeQuantity { get; set; }
    //    public decimal? LockinQuantity { get; set; }
    //    public decimal? PledgeQuantity { get; set; }
    //    public decimal? Quantity { get; set; }
    //    public decimal? Rate { get; set; }
    //    public int? InstrumentLedgerID { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public string? Remarks { get; set; }
    //    public string? Maker { get; set; }
    //    public string? MakeDate { get; set; }
    //    public string? BOID { get; set; }
    //    public decimal? AvailableBalance { get; set; }
    //    public string? InvestorCategory { get; set; }
    //}

    //public class UnitFundDeliveryApprove
    //{
    //    //public int? UnitFundCollID { get; set; }
    //    public String? Status { get; set; }
    //    public String? ApprovalRemark { get; set; }
    //    public List<UnitFundDeliveryListDTO>? CMUnitFundDeliveryApproveList { get; set; }
    //}
}
