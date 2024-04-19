using Model.DTOs;
using Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IService
    {
        ITestRepository Tests { get; }
        ICMExchangeRepository CMExchanges { get; }
        IAssetManagerRepository CMAssetManagers { get; }
        IOrganizationRepository Organizations { get; }
        IBrokerRepository Brokers { get; }
        ICMMarketTypeRepository CMMarketTypes { get; }
        ICMInstrumentRepository CMInstruments { get; }
        IInstrumentGroupRepository InstrumentGroups { get; }
        IUpdateLogRepository Logs { get; }
        IDepositoryRepository Depositories { get; }
        ICMTradingPlatformRepository CMTradingPlatforms { get; }
        ISLTradeSettlementRuleRepository TradeSettlementRules { get; }
        ISLTradeNettingRuleRepository TradeNettingRules { get; }
        IUserRepository Users { get; }
        ICMStockIndexRepository Indexes { get; }
        ICMMerchantBankRepository MerchantBanks { get; }
        IBrokerageCommisionRepository brokerageCommision { get; }
        IBrokerageCommissionAMLRepository brokerageCommisionAML { get; }
        IPriceFileRepository priceFile { get; }
        ITradeFileRepository TradeFile { get; }
        ITradeRestrictionRepository TradeRestrictions { get; }
        ITransactionDayRepository TransactionDay { get; }
        IApprovalRepository Approvals { get; }
        IBrokerageCommisionAccountGroupRepository brokerageCommisionAccountGroup { get; }
        IApprovalHistoryRepository ApprovalHistory { get; }
        ISaleOrderRepository SaleOrder { get; }
        IBuyOrderRepository BuyOrder { get; }
        ITradeCorrectionRepository tradeCorrection { get; }
        IAllocationRepository Allocation { get; }
        IDashboradRepository Dashborad { get; }
        IOmnibusFileRepository omnibusFileRepository { get; }
        IWithdrawalRepository WithdrawalRepository { get; }
        IFundCollectionRepository fundCollectionRepository { get; }
        IIPORepository iPORepository { get; }
        IInstallmentScheduleRepository InstallmentSchedule { get; }
        IEquityIncorporationRepository equityIncorporationRepository { get; }
        IChargesRepository chargesRepository { get; }
        ICorpActRepository corpActRepository { get; }
        ITradeFileExportRepository tradeFileExportRepository { get; }
        IOrderSheetRepository orderSheetRepository { get; }
        ITradeSettlementRepository tradeSettlementRepository { get; }
        IReportRepository reportRepository { get; }
        ICDBLFileInformationRepository CDBLFileInformation { get; }
        IAllotmentRepository AllotmentRepository { get; }
        IFAQRepository FAQRepository { get; }
		IAccountingRepository coARepository { get; }
        IPerpetualBondRepository perpetualBondRepository { get; }
        IFDRRepository fDRRepository { get; }
		IFundOperationRepository fundOperationRepository { get; }
        ICorporateActionDividendRepository corporateActionDividend { get; }
        IDematRepository dematRepository { get; }
        IRematRepository rematRepository { get; }
        IMarginRepository marginRepository { get; }
        ISecurityEliminationRepository securityEliminationRepository { get; }
        IDocumentRepository documentRepository { get; }
        ILockUnlockRepository lockUnlockRepository { get; }
        ISellSurrenderRepository sellSurrenderRepository { get; }
        IPhysicalInstrumentCollectionDeliveryRepository physicalInstrumentCollectionDelivery { get; }
        IDividendRepository divident { get; }
		IAccountingTradeSettlementRepository accountingTradeSettlementRepository { get; }
        IInsurancePremiumRepository insurancePremiumRepository { get; } 
        IKYCRepository   kYCRepository { get; }
        IUnitFundCollectionDeliveryRepository unitFund { get; }
        IInstrumentConversionRepository instrumentConversion { get; }
		ISettlementAccountRepository settlementAccountRepository { get; }
        IStockSplitRepository stockSplitRepository { get; }
        IAuditInspectionRepository auditInspectionRepository { get; }
        IInternalFundTransferRepository internalFundTransferRepository { get; }
		IAccountSettlementRepository accountSettlementRepository { get; }
		INAVRepository navRepository { get; }
		IVoucherTemplateRepository voucherTemplateRepository { get; }
	}


    public interface IGlobalSettingService
    {
        public List<TypeCodeDTO> GetTypeCodes(string typeName, int pTypeID = 0);
        public List<TypeCodebyCompanyDTO> GetTypeCodesCompanyWise(int CompanyID, int BranchID, string TypeName, int InstructionParam = 0);

        public TransactionDateStatusDTO GetTransactionDateStatus(string UserName,int CompanyID, int BranchID);
    }


}
