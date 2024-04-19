using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.IPO;

namespace Model.DTOs
{
    public class CDBLFileInformation
{
	public string? TransactionDate { get; set; }
    public Nullable<int> CDBLFileInfoID { get; set; }

    public string? FileDescription { get; set; }
    public string? FileName { get; set; }
    public Nullable<int> NoofFile { get; set; }
    public Nullable<int> ProcessedNoofFile { get; set; }
    public String? EnableDisable { get; set; }
    }

    public class CDBLListDTO
    {
        public int? SerialNo { get; set; }
        public int? IPOFileDetailID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? CurrentBalance { get; set; }
        public decimal? LockinBalance { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovedStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
        public String? EnableDisable { get; set; }
    }

    public class CMCDBLFileProcessDTO
    {
        public String? ApprovalRemark { get; set; }
        public String? Status { get; set; }
       

        public List<CMCDBLFileProcessList>? CDBLFileProcessList { get; set; }
        //public List<CMCDBLValidationFile>? CMCDBLValidationFileList { get; set; }
        public List<CMCDBLFileMissingInstrument>? CMCDBLFileMissingInstrumentList { get; set; }
        public List<CMCDBLFileMissingAccountInfo>? CMCDBLFileMissingAccountInfoList { get; set; }

    }
    //public class CMCDBLValidationFile
    //{
    //    public int? CDBLFileID { get; set; }
    //    public String? FileDescription { get; set; }

    //}

    public class CMCDBLFileProcessList
    {
        public int? IPOFileDetailID { get; set; }
        public int? CDBLFileID { get; set; } 

    }
    public class CMCDBLFileMissingInstrument
    {
        public int? SerialNo { get; set; }
        public String? TransactionDate { get; set; }
        public String? CompanyName { get; set; }
        public String? ISIN { get; set; }
        public String? Remarks { get; set; }
        public int? CDBLFileId { get; set; }
    }
    public class CMCDBLFileMissingAccountInfo
    {
        public int? SerialNo { get; set; }
        public String? TransactionDate { get; set; }
        public String? BOCode { get; set; }
        public String? AccountNumber { get; set; }
        public String? Name { get; set; }
        public String? Remarks { get; set; }
        public int? CDBLFileId { get; set; }
    }

    public class CMCDBLUpdateIPOFiledataMaster
    {
        public decimal? AveragePrice { get; set; }
        public List<CMCDBLUpdateIPOFiledataDTO>? CMCDBLUpdateList { get; set; }
    }

    public class CMCDBLUpdateIPOFiledataProcess
    {
        public int? IPOInstrumentID { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        public List<CMCDBLUpdateIPOFiledataDTO>? CMCDBLUpdateList { get; set; }
    }

    public class CMCDBLUpdateIPOFiledataDTO
    {
        public int? SerialNo { get; set; }
        public int? IPOFileDetailID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? CurrentBalance { get; set; }
        public decimal? LockinBalance { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovedStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
        
    }

    public class CDBLRightsListDTO
    {
        public int? RightsCollectionID { get; set; }
        public int? BonusRightsFileDetailID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? FreeBalance { get; set; }
        public decimal? LockinBalance { get; set; }
        public decimal? FreezQuantity { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? CreditedQuantity { get; set; }   
        public decimal? AveragePrice { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
        public String? EnableDisable { get; set; }
    }

    public class CMCDBLUpdateRightsMaster
    {
        public decimal? AveragePrice { get; set; }
        public List<CDBLRightsUpdateDTO>? CMCDBLUpdateList { get; set; }
    }

    public class CMCDBLRightsProcess
    {
        public int? RightSettingID { get; set; }
        public string? Status { get; set; }
        public string? ApprovalRemark { get; set; }
        public List<CDBLRightsUpdateDTO>? CMCDBLUpdateList { get; set; }
    }

    public class CDBLRightsUpdateDTO
    {
        public int? RightsCollectionID { get; set; }
        public int? BonusRightsFileDetailID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? FreeBalance { get; set; }
        public decimal? LockinBalance { get; set; }
        public decimal? FreezQuantity { get; set; }
        public decimal? BlockQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
    }

    public class CDBLTransferListDTO
    {
        public int? SL { get; set; }
        public int? TransferTransmissionFileDetailID { get; set; }
        public int? TransTmisionInstrumentID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
        public String? Maker { get; set; }
        public String? MakeDate { get; set; }
        public string? FileDate { get; set; }
        public String? EnableDisable { get; set; }
       
    }

    public class SLTransferTransmissionInstrument
    {
        public int? SL { get; set; }
        public int? TransferTransmissionFileDetailID { get; set; }
        public int? TransTmisionInstrumentID { get; set; }
        public int? CDBLFileID { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? SequenceNumber { get; set; }
    }

    public class SLTransferTransmissionInstrumentMaster
    {
        public Nullable<decimal> AveragePrice { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public List<SLTransferTransmissionInstrument>? CMCDBLTransferTransUpdateList { get; set; }
    }

    public class SLApprovedTransferTransmissionInstrumentDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<SLTransferTransmissionInstrument>? CMCDBLTransferTransUpdateList { get; set; }

    }

    public class CMCDBLTransferTransmissionDTO
    {

        public int? TransTmisionInstrumentID { get; set; }
        public int? ProductID { get; set; }
        public int? CDBLFileInfoID { get; set; }
        public String? TransactionDate { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public String? Remarks { get; set; }
        public String? CollectionDelivery { get; set; }
        public String? ApprovalStatus { get; set; }
        public int? ApprovalSetID { get; set; }
        public int? InstrumentLedgerID { get; set; }
        public int? TransferTransmissionFileDetailID { get; set; }

    }

    public class CDBLInstrumentListID
    {
       public int? InstrumentID { get; set; }
       public decimal? TotalQuantity { get; set; }
       public decimal? FreeQuantity { get; set;}
        public decimal? ReceivableQuantity { get; set; }

        public decimal? FreezeQuantity { get; set; }
        public decimal? LockinQuantity { get; set; }
        public decimal? BlockQuantity { get; set;}
       public decimal? PledgeQuantity { get; set; }
       
    }

    public class AMLBankAccountDTO
    {
        public int? FundID { get; set; }
        public string? FundName { get; set; }
        public int? MFBankAccountID { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountName { get;set; }
    }

    public class InstrumentDto
    {
        public int SerialNo { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        
       
    }
}
