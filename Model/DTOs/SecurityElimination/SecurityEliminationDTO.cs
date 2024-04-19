using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.Demat;

namespace Model.DTOs.SecurityElimination
{
    public class SecurityEliminationDTO
    {
        public int? SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? ReceivableQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? FreeQuantity { get; set; }
        public decimal? Rate { get;set; }
        public decimal? MarketValue { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public string? EnableDisable { get; set; }
    }

    public class SecurityEliminationInsertDTO
    {
       public List<SecuirtyEliminationMaster>? CMSecurityEliminationList { get; set; }
    }

    public class SecuirtyEliminationMaster
    {
        public int? SL { get; set; }
        public String? AccountNumber { get; set; }
        public String? AccountName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? ReceivableQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? FreeQuantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? MarketValue { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? EleminatiionInstFileDetailID { get; set; }
        public int? InstrumentLedgerID { get; set; }
        public int? EleminationInstrumentID { get; set; }
        public String? Maker { get; set; }
        public string? MakeDate { get; set; }
        public string? EnableDisable { get; set; }
    }

    public class CMSecurityInstrumentEliminationApproveDTO
    {
        public int? InstrumentID { get; set; }
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<SecuirtyEliminationMaster>? CMSecurityEliminationApprove { get; set; }
    }

    public class CMSecurityEliminationUpdateMaster
    {
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public List<SecuirtyEliminationMaster>? CMSecurityEliminationUpdateList { get; set; }
    }

    //public class CMInstrumentConversionDTO 
    //{ 
    //    public int? SL { get; set;}
    //    public int? InstConversionID { get; set; }
    //    public int? ContractID { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? AccountName { get; set;}
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set;}
    //    public int? BaseInstID { get; set; }
    //    public string? BaseInstrument { get; set;}
    //    public decimal? BaseQuantity { get; set; }
    //    public decimal? BaseInstRate { get; set; }
    //    public decimal? TotalCost { get; set; }
    //    public string? ConversionRatio { get; set;}
    //    public int? ConvertedInstID { get; set; }
    //    public string? ConvertedInstrument { get;set; }
    //    public decimal? ConvertedQuantity { get; set; }
    //    public decimal? ConvertedInstRate { get;set; }
    //}

    //public class CMInstrumentConversion
    //{
    //    public int? SL { get; set; }
    //    public int? ConversionDetailID { get; set; }
    //    public int? InstConversionID { get; set; }
    //    public int? ContractID { get; set; }
    //    public string? AccountNumber { get; set; }
    //    public string? AccountName { get; set; }
    //    public int? ProductID { get; set; }
    //    public string? ProductName { get; set; }
    //    public int? BaseInstID { get; set; }
    //    public string? BaseInstrument { get; set; }
    //    public decimal? BaseQuantity { get; set; }
    //    public decimal? BaseInstRate { get; set; }
    //    public decimal? TotalCost { get; set; }
    //    public string? ConversionRatio { get; set; }
    //    public int? ConvertedInstID { get; set; }
    //    public string? ConvertedInstrument { get; set; }
    //    public decimal? ConvertedQuantity { get; set; }
    //    public decimal? ConvertedInstRate { get; set; }
    //}

    //public class CMInstrumentConversionInsert
    //{
    //    public int? InstConversionID { get; set; }
    //    public List<CMInstrumentConversion>? InstrumentConversionList { get; set; }
    //}


}
