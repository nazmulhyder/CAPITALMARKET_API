using Microsoft.Extensions.DependencyInjection;
using Service.Implementation;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Mapper;

namespace Service
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ITestRepository, TestRepository>();        
            services.AddTransient<IService, Service.Implementation.Service>();
            services.AddTransient<IGlobalSettingService, GlobalSettingService>();
            services.AddTransient<IDBCommonOpService, DBCommonOpService>();
            services.AddTransient<ICMExchangeRepository, CMExchangeRepository>();
            services.AddTransient<IAssetManagerRepository, AssetManagerRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IBrokerRepository, BrokerRepository>();
            services.AddTransient<ICMMarketTypeRepository, CMMarketTypeRepository>();
            services.AddTransient<ICMInstrumentRepository, CMInstrumentRepository>();
            services.AddTransient<IUpdateLogRepository, UpdateLogRepository>();
            services.AddTransient<IInstrumentGroupRepository, InstrumentGroupRepository>();
            services.AddTransient<IDepositoryRepository, DepositoryRepository>();
            services.AddTransient<ICMTradingPlatformRepository, CMTradingPlatformRepository>();
            services.AddTransient<ISLTradeSettlementRuleRepository, SLTradeSettlementRuleRepository>();
            services.AddTransient<ISLTradeNettingRuleRepository, SLTradeNettingRuleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICMStockIndexRepository, CMStockIndexRepository>();
            services.AddTransient<ICMMerchantBankRepository, CMMerchantBankRepository>();
            services.AddAutoMapper(typeof(MapperInitializer));
            services.AddTransient<IBrokerageCommisionRepository, BrokerageCommisionRepository>();
            services.AddTransient<IBrokerageCommissionAMLRepository, BrokerageCommissionAMLRepository>();
            services.AddTransient<IPriceFileRepository, PriceFileRepository>();
            services.AddTransient<ITradeFileRepository, TradeFileRepository>();
            services.AddTransient<ITradeRestrictionRepository, TradeRestrictionRepository>();
            services.AddTransient<ITransactionDayRepository, TransactionDayRepository>();
            services.AddTransient<IApprovalRepository, ApprovalRepository>();
            services.AddTransient<IBrokerageCommisionAccountGroupRepository, BrokerageCommisionAccountGroupRepository>();
            services.AddTransient<IApprovalHistoryRepository, ApprovalHistoryRepository>();
            services.AddTransient<ISaleOrderRepository, SaleOrderRepository>();
            services.AddTransient<IBuyOrderRepository, BuyOrderRepository>();
            services.AddTransient<ITradeCorrectionRepository, TradeCorrectionRepository>();
            services.AddTransient<IAllocationRepository, AllocationRepository>();
            services.AddTransient<IDashboradRepository, DashboradRepository>();
            services.AddTransient<IOmnibusFileRepository, OmnibusFileRepository>();
            services.AddTransient<IWithdrawalRepository, WithdrawalRepository>();
            services.AddTransient<IFundCollectionRepository, FundCollectionRepository>();
            services.AddTransient<IIPORepository, IPORepository>();
            services.AddTransient<IInstallmentScheduleRepository, InstallmentScheduleRepository>();
            services.AddTransient<IEquityIncorporationRepository, EquityIncorporationRepository>();
            services.AddTransient<IChargesRepository, ChargesRepository>();
            services.AddTransient<ICorpActRepository, CorpActRepository>();
            services.AddTransient<IOrderSheetRepository, OrderSheetRepository>();
            services.AddTransient<ITradeFileExportRepository, TradeFileExportRepository>();
            services.AddTransient<ITradeSettlementRepository, TradeSettlementRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<ICDBLFileInformationRepository, CDBLFileInformationRepository>();
            services.AddTransient<IAllotmentRepository, AllotmentRepository>();
            services.AddTransient<IFAQRepository, FAQRepository>();
            services.AddTransient<IAccountingRepository, AccountingRepository>();
            services.AddTransient<IPerpetualBondRepository, PerpetualBondRepository>();
            services.AddTransient<IFDRRepository, FDRRepository>();
            services.AddTransient<IFundOperationRepository, FundOperationRepository>();
            services.AddTransient<ICorporateActionDividendRepository, CorporateActionDividendRepository>();
            services.AddTransient<IDematRepository, DematRepository>();
            services.AddTransient<IRematRepository, RematRepository>();
            services.AddTransient<IMarginRepository, MarginRepository>();
            services.AddTransient<ISecurityEliminationRepository, SecurityEliminationRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<ILockUnlockRepository, LockUnlockRepository>();
            services.AddTransient<ISellSurrenderRepository, SellSurrenderRepository>();
            services.AddTransient<IPhysicalInstrumentCollectionDeliveryRepository, PhysicalInstrumentCollectionDeliveryRepository>();
            services.AddTransient<IDividendRepository, DividendRepository>();
            services.AddTransient<IAccountingTradeSettlementRepository, AccountingTradeSettlementRepository>();
            services.AddTransient<IInsurancePremiumRepository, InsurancePremiumRepository>();
            services.AddTransient<IKYCRepository, KYCRepository>();
            services.AddTransient<IUnitFundCollectionDeliveryRepository, UnitFundCollectionDeliveryRepository>();
            services.AddTransient<IInstrumentConversionRepository, InstrumentConversionRepository>();
            services.AddTransient<ISettlementAccountRepository, SettlementAccountRepository>();
            services.AddTransient<IStockSplitRepository, StockSplitRepository>();
            services.AddTransient<IAuditInspectionRepository, AuditInspectionRepository>();
            services.AddTransient<IInternalFundTransferRepository, InternalFundTransferRepository>();
            services.AddTransient<IAccountSettlementRepository, AccountSettlementRepository>();
            services.AddTransient<INAVRepository, NAVRepository>();
            services.AddTransient<IVoucherTemplateRepository, VoucherTemplateRepository>();
        }
    }
}
