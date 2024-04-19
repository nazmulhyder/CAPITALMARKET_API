using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.SecurityElimination;

namespace Model.DTOs.InstrumentConversion
{
    public class InstrumentConversionDTO
    {
    }

    public class InstrumentConversionDeclarationDTO
    {
        public int? InstConversionID { get; set; }
        public int? BaseInstID { get; set; }
        public decimal? BaseRatio { get; set; }
        public decimal? ConvertedRatio { get; set; }
        public int? ConvertedInstID { get; set; }
        public string? DeclareDate { get; set; }
        public string? RecordDate { get; set; }
        public decimal? ConversionRatio { get; set; }
        public decimal? ConversionPercentage { get; set; }
        public string? IsContinue { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? Remarks { get; set; }
    }

    public class InstrumentConversionDeclarationApprove
    {
        public int? InstConversionID { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        //public List<InstrumentConversionDeclarationDTO>? cmInstrumentConversionList { get; set; }
    }

    public class InstrumentConversionDeclarationListDTO
    {
        public int? InstConversionID { get; set; }
        public int? BaseInstID { get; set; }
        public string? DeclareDate { get; set; }
        public string? RecordDate { get; set; }
        public string? ConversionRatio { get; set; }
        public decimal? ConversionPercentage { get; set; }
        public string? IsContinue { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? Remarks { get; set; }
        public string? BaseInstrumentName { get; set; }
        public string? ConvertedInstrumentName { get; set; }
        public string? EnableDisable { get; set; }

    }

    public class CMInstrumentConversionInsert
    {
        public int? InstConversionID { get; set; }
        public List<CMInstrumentConversion>? InstrumentConversionList { get; set; }
    }
    public class CMInstrumentConversion
    {
        public int? SL { get; set; }
        public int? ConversionDetailID { get; set; }
        public int? InstConversionID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? BaseInstID { get; set; }
        public string? BaseInstrument { get; set; }
        public decimal? BaseQuantity { get; set; }
        public decimal? BaseInstRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string? ConversionRatio { get; set; }
        public int? ConvertedInstID { get; set; }
        public string? ConvertedInstrument { get; set; }
        public decimal? ConvertedQuantity { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
        public decimal? ConvertedInstRate { get; set; }
        public decimal? BaseActualQuantity { get; set; }
    }

    public class CMInstrumentConversionDTO
    {
        public int? SL { get; set; }
        public int? InstConversionID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? BaseInstID { get; set; }
        public string? BaseInstrument { get; set; }
        public decimal? BaseQuantity { get; set; }
        public decimal? BaseInstRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string? ConversionRatio { get; set; }
        public int? ConvertedInstID { get; set; }
        public string? ConvertedInstrument { get; set; }
        public decimal? ConvertedQuantity { get; set; }
        public decimal? ConvertedInstRate { get; set; }
    }

    public class CMInstrumentConversionApprove
    {
        public int? InstConversionID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<CMInstrumentConversion>? InstrumentConversionApprovalList { get; set; }

    }

    public class CMInstrumentConversionUpdateBaseQuantityDTO
    {
        public decimal? BaseQuantity { get; set; }
        public List<CMInstrumentConversion>? InstrumentConversionBQUpdate { get; set; }
    }

    public class InstrumentSplitDeclarationDTO
    {
    public int? InstSplitedID { get; set; }
	public int? InstrumentID { get; set; }
    public string? InstrumentName { get; set; }
    public string? ConversionRatio { get; set; }
    public decimal? ConversionPercentage { get; set; }
    public decimal? BaseRatio { get; set; }
	public string? SplitedRatio { get; set; }
    public string? DeclareDate { get; set; }
    public string? RecordDate { get; set; }
    public string? Remarks { get; set;}
    public string? Maker { get; set; }
    public string? MakeDate { get; set; }
    public string? ApprovalStatus { get; set; }
    public int? ApprovalSetID { get; set; }
    public string? EnableDisable { get; set; }

    }

    public class InstrumentSplitDeclarationApprove
    {
        public int? InstSplitedID { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        //public List<InstrumentConversionDeclarationDTO>? cmInstrumentConversionList { get; set; }
    }

    public class CMInstrumentSplitDTO
    {
        public int? SL { get; set; }
        public int? InstSplitedID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? BaseQuantity { get; set; }
        public decimal? BaseInstRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string? ConversionRatio { get; set; }
        public int? ConvertedInstID { get; set; }
        public decimal? SplitedQuantity { get; set; }
        public decimal? SplitedQuantityRate { get; set; }
    }

    public class CMInstrumentSplitInsert
    {
        public int? InstSplitedID { get; set; }
        public List<CMInstrumentSplit>? InstrumentSplitList { get; set; }
    }

    public class CMInstrumentSplit
    {
        public int? SL { get; set; }
        public int? SplitedDetailID { get; set; }
        public int? InstConversionID { get; set; }
        public int? ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? InstrumentID { get; set; }
        public string? InstrumentName { get; set; }
        public decimal? BaseQuantity { get; set; }
        public decimal? BaseInstRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string? ConversionRatio { get; set; }
        public decimal? SplitedQuantity { get; set; }
        public decimal? SplitedQuantityRate { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
    }

    public class CMInstrumentSplitUpdateSpittedQuantityDTO
    {
        public decimal? SplitedQuantity { get; set; }
        public List<CMInstrumentSplit>? InstrumentSplittedQuantityUpdate { get; set; }
    }

    public class CMInstrumentSplitApprove
    {
        public int? InstSplitedID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<CMInstrumentSplit>? InstrumentSplitApprovalList { get; set; }

    }
}
