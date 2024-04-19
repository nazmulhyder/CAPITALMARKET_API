using Model.DTOs.Demat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.LockUnlock
{
    public class LockUnlockInstrumentListDTO
    {
     
        public int? InstrumentID { get; set; }
        public String? InstrumentName { get; set; }

    }
    public class LockInstrumentListDTO
    {

        public int? Sl { get; set; }

        public int? LockingId { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }

        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }

        public int? InstrumentID { get; set; }

        public String? InstrumentName { get; set; }
        public Decimal? Quantity { get; set; }
        public String? TransactionDate { get; set; }

        public String? EffectiveDate { get; set; }

        public String? LockingSource { get; set; }

        public int? LockingType { get; set; }

        public String? LockingTypeName { get; set; }
        public String? ApprovalStatus { get; set; }
        
        public String? Remarks { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public String? EnableDisable { get; set; }

        public Decimal? TotalQuantity { get; set; }
        public Decimal? FreeQuantity { get; set; }
        public Decimal? BlockQuantity { get; set; }
        public Decimal? FreezeQuantity { get; set; }
        public Decimal? LockinQuantity { get; set; }
        public Decimal? PledgeQuantity { get; set; }

        public Decimal? ReceivableQuantity { get; set; }


    }
    public class CMLockInstrumentDTO
    {
     
        public decimal? Quantity { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        public List<LockInstrumentListDTO>? CMLockInstrumentListDTO { get; set; }
    }

    //public class InstrumentConversionDeclarationDTO
    //{
    //    public int? InstConversionID { get; set; }
    //    public int? BaseInstID { get; set; }
    //    public decimal? BaseRatio { get; set; }
    //    public decimal? ConvertedRatio { get; set; }
    //    public int? ConvertedInstID { get; set; }
    //    public string? DeclareDate { get; set; }
    //    public string? RecordDate { get; set; }
    //    public decimal? ConversionRatio { get; set; }
    //    public decimal? ConversionPercentage { get; set;}
    //    public string? IsContinue { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public string? Remarks { get; set; }
    //}

    //public class InstrumentConversionDeclarationApprove
    //{
    //    public int? InstConversionID { get; set; }
    //    public string? Status { get; set; }
    //    public string? ApprovalRemark { get; set; }
    //    //public List<InstrumentConversionDeclarationDTO>? cmInstrumentConversionList { get; set; }
    //}

    //public class InstrumentConversionDeclarationListDTO
    //{
    //    public int? InstConversionID { get; set; }
    //    public int? BaseInstID { get; set; }
    //    public string? DeclareDate { get; set; }
    //    public string? RecordDate { get; set; }
    //    public string? ConversionRatio { get; set; }
    //    public decimal? ConversionPercentage { get; set; }
    //    public string? IsContinue { get; set; }
    //    public string? ApprovalStatus { get; set; }
    //    public int? ApprovalSetID { get; set; }
    //    public string? Maker { get; set; }
    //    public string? MakeDate { get; set; }
    //    public string? Remarks { get; set; }
    //    public string? BaseInstrumentName { get; set; }
    //    public string? ConvertedInstrumentName { get; set; }
    //    public string? EnableDisable { get; set; }

    //}

    public class UnlockInstrumentListDTO
    {

        public int? Sl { get; set; }

        public int? UnLockingId { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }

        public int? ContractID { get; set; }
        public String? AccountNumber { get; set; }
        public String? MemberName { get; set; }

        public int? InstrumentID { get; set; }

        public String? InstrumentName { get; set; }
        public Decimal? Quantity { get; set; }
        public String? TransactionDate { get; set; }

        public String? EffectiveDate { get; set; }

        public String? UnLockingSource { get; set; }

        public int? UnLockingType { get; set; }

        public String? UnLockingTypeName { get; set; }
        public String? ApprovalStatus { get; set; }

        public String? Remarks { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public String? EnableDisable { get; set; }

        public Decimal? TotalQuantity { get; set; }
        public Decimal? FreeQuantity { get; set; }
        public Decimal? BlockQuantity { get; set; }
        public Decimal? FreezeQuantity { get; set; }
        public Decimal? LockinQuantity { get; set; }
        public Decimal? PledgeQuantity { get; set; }

        public Decimal? ReceivableQuantity { get; set; }


    }

    public class CMUnLockInstrumentDTO
    {

        public decimal? Quantity { get; set; }
        public string? ApprovalRemark { get; set; }
        public List<UnlockInstrumentListDTO>? CMUnlockInstrumentListDTO { get; set; }
    }

    public class CMUnlockInstrumentDTO
    {

        public decimal? Quantity { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        public List<UnlockInstrumentListDTO>? CMUnlockInstrumentListDTO { get; set; }
    }

}
