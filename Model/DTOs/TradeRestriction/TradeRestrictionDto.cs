using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TradeRestriction
{

    public class RestrictionApprovalDto
    {
        public int RestrictionID { get; set; }
        public string RestrictionType { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovalRemark { get; set; }
    }

    #region Trade Restriction [Product]
    public class TradeRestrictionpProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public ProductTransactionProfileDto productTransactionProfile { get; set; }
        public List<ProductTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<ProductTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<ProductRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<ProductRestrictionInsDto> Sell_RestrictionInstruments { get; set; }
        public List<ProductRestrictionSectorDto> Sell_RestrictionSectors { get; set; }
        public List<ProductRestrictionSectorDto> Buy_RestrictionSectors { get; set; }


    }

    public class TradeApprovalRestrictionProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public ProductTransactionProfileDto productTransactionProfile { get; set; }
        public List<ProductTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<ProductTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<ProductRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<ProductRestrictionInsDto> Sell_RestrictionInstruments { get; set; }
        public List<ProductRestrictionSectorDto> Sell_RestrictionSectors { get; set; }
        public List<ProductRestrictionSectorDto> Buy_RestrictionSectors { get; set; }
        public ApprovalDto ApprovalDetail { get; set; }


    }

    public class ProductTransactionProfileDto
    {
        public int? ProdTransactionProfileID { get; set; }
        public bool IsBuyAllowed { get; set; }
        public bool IsSellAllowed { get; set; }
        public bool IsFundPollingAllowed { get; set; }
        public int ProductID { get; set; }
        public string? BuyRestrictionRemarks { get; set; }
        public string? SellRestrictionRemarks { get; set; }

    }

    public class ProductTradeAllowedInsGrpDto
    { 
       public int ProdAllowedGroupID { get; set; }
       public string TransactionType { get; set; }
       public int ProdTransactionProfileID { get; set;}
       public int InstrumentGroupID { get; set; }
       public string GroupName { get; set; }
       public string GroupDetail { get; set; }

    }

    public class ProductRestrictionInsDto
    {
        public int ProdRestrictedInstrumentID { get; set; }
        public string TransactionType { get; set; }
        public int InstrumentID { get; set; }
        public int ProdTransactionProfileID { get; set; }
        public string InstrumentName { get; set; }
        public decimal? PERatio { get; set; }
        public string? Remarks { get; set; }

    }

    public class ProductRestrictionSectorDto
    {
        public int ProdRestrictedSectorID { get; set; }
        public string TransactionType { get; set; }
        public int SectorID { get; set; }
        public int ProdTransactionProfileID { get; set; }
        public string SectorName { get; set; }
        public string? Remarks { get; set; }

    }

    public class ListTradeRestrictionProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string BuyRestriction { get; set; }
        public string SellRestriction { get; set; }
        public int TotalRowCount { get; set; }
        public string ApprovalStatus { get; set; }
    }

    public class ListTradeRestrictionApprovalProductDto
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? BuyRestriction { get; set; }
        public string? SellRestriction { get; set; }
        public int? TotalRowCount { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? TransactionProfileID { get; set; }
        public string? Maker { get; set; }
        public DateTime? MakeDate { get; set; }
    }

    #endregion

    #region Trade Restriction [Account Group]
    public class ListTradeRestrictionAccGrpDto
    {
        public int AccountGroupID { get; set; }
        public string AccountGroupName { get; set; }
        public string BuyRestriction { get; set; }
        public string SellRestriction { get; set; }
        public int TotalRowCount { get; set; }
        public string ApprovalStatus { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }

    }


    public class AccGrpTransactionProfileDto
    {
        public int? AccGrpTransactionProfileID { get; set; }
        public bool IsBuyAllowed { get; set; }
        public bool IsSellAllowed { get; set; }
        //public bool IsFundPollingAllowed { get; set; }
        public int AccountGroupID { get; set; }
        public string? BuyRestrictionRemarks { get; set; }
        public string? SellRestrictionRemarks { get; set; }

    }

    public class AccGrpTradeAllowedInsGrpDto
    {
        public int AccGrpAllowedGroupID { get; set; }
        public string TransactionType { get; set; }
        public int AccGrpTransactionProfileID { get; set; }
        public int InstrumentGroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDetail { get; set; }

    }

    public class AccGrpRestrictionInsDto
    {
        public int AccGrpRestrictedInstrumentID { get; set; }
        public string TransactionType { get; set; }
        public int InstrumentID { get; set; }
        public int AccGrpTransactionProfileID { get; set; }
        public string InstrumentName { get; set; }
        public string? Remarks { get; set; }

    }

    public class AccGrpRestrictionSectorDto
    {
        public int AccGrpRestrictedSectorID { get; set; }
        public string TransactionType { get; set; }
        public int SectorID { get; set; }
        public int AccGrpTransactionProfileID { get; set; }
        public string SectorName { get; set; }
        public string? Remarks { get; set; }

    }

    public class ListTradeRestrictionApprovalAccGrpDto
    {
        public int? AccountGroupID { get; set; }
        public string? AccountGroupName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string? BuyRestriction { get; set; }
        public string? SellRestriction { get; set; }
        public int? TotalRowCount { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? TransactionProfileID { get; set; }
        public string? Maker { get; set; }
        public DateTime? MakeDate { get; set; }
    }

    public class TradeRestrictionAccGrpDto
    {
        public int AccountGroupID { get; set; }
        public string AccountGroupName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public AccGrpTransactionProfileDto AccGrpTransactionProfile { get; set; }
        public List<AccGrpTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<AccGrpTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<AccGrpRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<AccGrpRestrictionInsDto> Sell_RestrictionInstruments { get; set; }

        public List<AccGrpRestrictionSectorDto> Buy_RestrictionSectors { get; set; }
        public List<AccGrpRestrictionSectorDto> Sell_RestrictionSectors { get; set; }

    }

    public class TradeApprovalRestrictionAccGrpDto
    {
        public int AccountGroupID { get; set; }
        public string AccountGroupName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public AccGrpTransactionProfileDto AccGrpTransactionProfile { get; set; }
        public List<AccGrpTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<AccGrpTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<AccGrpRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<AccGrpRestrictionInsDto> Sell_RestrictionInstruments { get; set; }
        public List<AccGrpRestrictionSectorDto> Buy_RestrictionSectors { get; set; }
        public List<AccGrpRestrictionSectorDto> Sell_RestrictionSectors { get; set; }
        public ApprovalDto ApprovalDetail { get; set; }

    }



    #endregion

    #region Trade Restriction [Account]
    public class ListTradeRestrictionAccountDto
    {
        public int? ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? MemberName { get; set; }
        public string? MemberCode { get; set; }
        public string? BuyRestriction { get; set; }
        public string? SellRestriction { get; set; }
        public int? TotalRowCount { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? AccountGroupID { get; set; }
        public string? AccountGroupName { get; set; }
    }


    public class AccountTransactionProfileDto
    {
        public int? TransactionProfileID { get; set; }
        public bool IsBuyAllowed { get; set; }
        public bool IsSellAllowed { get; set; }
        public bool IsFundPollingAllowed { get; set; }
        public int ContractID { get; set; }
        public string? BuyRestrictionRemarks { get; set; }
        public string? SellRestrictionRemarks { get; set; }

    }

    public class AccountTradeAllowedInsGrpDto
    {
        public int AgrAllowedGroupID { get; set; }
        public string TransactionType { get; set; }
        public int TransactionProfileID { get; set; }
        public int InstrumentGroupID { get; set; }
        public string? GroupName { get; set; }
        public string? GroupDetail { get; set; }

    }

    public class AccountRestrictionInsDto
    {
        public int AgrRestrictedInstrumentID { get; set; }
        public string TransactionType { get; set; }
        public int TransactionProfileID { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        
        public string? Remarks { get; set; }
       
    }

    public class AccountRestrictionSectorDto
    {
        public int AgrRestrictedSectorID { get; set; }
        public string TransactionType { get; set; }
        public int SectorID { get; set; }
        public int AgrTransactionProfileID { get; set; }
        public string SectorName { get; set; }
        public string? Remarks { get; set; }

    }

    public class ListTradeRestrictionApprovalAccountDto
    {
        public int? ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? MemberName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? AccountGroupID { get; set; }
        public string? AccountGroupName { get; set; }
        public string? BuyRestriction { get; set; }
        public string? SellRestriction { get; set; }
        public int? TotalRowCount { get; set; }
        public string? ApprovalStatus { get; set; }
        public int TransactionProfileID { get; set; }
        public string? Maker { get; set; }
        public DateTime? MakeDate { get; set; }
    }

    public class TradeRestrictionAccountDto
    {
        public int ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? MemberName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? AccountGroupID { get; set; }
        public string? AccountGroupName { get; set; }
        public AccountTransactionProfileDto AccountTransactionProfile { get; set; }
        public List<AccountTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<AccountTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<AccountRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<AccountRestrictionInsDto> Sell_RestrictionInstruments { get; set; }
        public List<AccountRestrictionSectorDto> Buy_RestrictionSectors { get; set; }
        public List<AccountRestrictionSectorDto> Sell_RestrictionSectors { get; set; }


    }
    public class TradeApprovalRestrictionAccountDto
    {
        public int ContractID { get; set; }
        public string? AccountNo { get; set; }
        public string? MemberName { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? AccountGroupID { get; set; }
        public string? AccountGroupName { get; set; }
        public AccountTransactionProfileDto AccountTransactionProfile { get; set; }
        public List<AccountTradeAllowedInsGrpDto> Buy_InsGrpsList { get; set; }
        public List<AccountTradeAllowedInsGrpDto> Sell_InsGrpsList { get; set; }
        public List<AccountRestrictionInsDto> Buy_RestrictionInstruments { get; set; }
        public List<AccountRestrictionInsDto> Sell_RestrictionInstruments { get; set; }
        public ApprovalDto ApprovalDetail { get; set; }


    }
    #endregion

    #region Approval DTO
    public class ApprovalDto
    {
        public int ID { get; set; }
        public string? ApprovalStatus { get; set; }
        public int? ApprovalReqSetID { get; set; }
        public int? ApprovalReqID { get; set; }
        public int? RequiredLevel { get; set; }
        public int? CurrentLevel { get; set; }
        public string? Maker { get; set; }
        public DateTime? MakeDate { get; set; }
        public int? ApprovalTypeCode { get; set; }

    }
    #endregion
}
