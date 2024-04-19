using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs;
using Model.DTOs.MarginRequest;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class Service : IService
    {
        public Service(
            ICMExchangeRepository _CMExchanges,
            ITestRepository _Tests,
            IAssetManagerRepository _CMAssetManagers,
            IOrganizationRepository _Organizations,
            IBrokerRepository _Brokers,
            ICMMarketTypeRepository _CMMarketTypes,
            ICMInstrumentRepository _CMInstruments,
            IUpdateLogRepository _Logs,
            IInstrumentGroupRepository _InstrumentGroups,
            IDepositoryRepository _Depositories,
            ICMTradingPlatformRepository _TradingPlatforms,
            ISLTradeSettlementRuleRepository _TradeSettlementRules,
            ISLTradeNettingRuleRepository _TradeNettingRules,
            IUserRepository _Users,
            ICMStockIndexRepository _Indexes,
            ICMMerchantBankRepository _MerchantBanks,
            IBrokerageCommisionRepository brokerageCommision,
            IBrokerageCommissionAMLRepository _brokerageCommisionAML,
            IPriceFileRepository _priceFileRepository,
            ITradeFileRepository _TradeFile,
            ITransactionDayRepository _TransactionDay,
            ITradeRestrictionRepository _TradeRestrictions,
            IApprovalRepository _Approvals,
            IBrokerageCommisionAccountGroupRepository _brokerageCommisionAccountGroup,
            IApprovalHistoryRepository _ApprovalHistory,
            ISaleOrderRepository _SaleOrder,
            ITradeCorrectionRepository _tradeCorrection,
            IBuyOrderRepository _BuyOrder,
            IAllocationRepository _Allocation,
            IDashboradRepository _Dashboard,
            IOmnibusFileRepository _omnibusFileRepository,
            IFundCollectionRepository _fundCollectionRepository,
            IWithdrawalRepository _withdrawalRepository,
            IIPORepository _iIPORepository,
            IInstallmentScheduleRepository _installmentSchedule,
            IEquityIncorporationRepository _equityIncorporationRepository,
            IChargesRepository _chargesRepository,
            IOrderSheetRepository _orderSheetRepository,
            ICorpActRepository _corpActRepository,
            ITradeFileExportRepository _tradeFileExportRepository,
            ITradeSettlementRepository _tradeSettlementRepository,
            IReportRepository _reportRepository,
            ICDBLFileInformationRepository _CDBLFileInformation,
            IAllotmentRepository _AllotmentRepository,
            IFAQRepository _FAQRepository,
			IAccountingRepository _coARepository,
            IPerpetualBondRepository _perpetualBond,
            IFDRRepository _fDRRepository,
			IFundOperationRepository _fundOperationRepository,
            ICorporateActionDividendRepository _corporateActionDividend,
            IDematRepository _dematRepository,
            IRematRepository _rematRepository,
            IMarginRepository _marginRepository,
            ISecurityEliminationRepository _securityEliminationRepository,
            IDocumentRepository _documentRepository,
            ILockUnlockRepository _lockUnlockRepository,
            ISellSurrenderRepository _sellSurrenderRepository,
            IPhysicalInstrumentCollectionDeliveryRepository _physicalInstrumentCollectionDelivery,
			IAccountingTradeSettlementRepository _accountingTradeSettlementRepository,
            IDividendRepository _dividentRepository,
            IInsurancePremiumRepository _insurancePremiumRepository,
            IKYCRepository _kYCRepository,
            IUnitFundCollectionDeliveryRepository _unitFund,
            IInstrumentConversionRepository _instrumentConversion,
			ISettlementAccountRepository _settlementAccountRepository,
            IStockSplitRepository _stockSplitRepository,
            IAuditInspectionRepository _auditInspectionRepository,
            IInternalFundTransferRepository _internalFundTransferRepository,
			IAccountSettlementRepository _accountSettlementRepository,
			INAVRepository _navRepository,
			IVoucherTemplateRepository _voucherTemplateRepository


			)
        {
            CMExchanges = _CMExchanges;
            CMAssetManagers = _CMAssetManagers;
            Organizations = _Organizations;
            Brokers = _Brokers;
            CMMarketTypes = _CMMarketTypes;
            CMInstruments = _CMInstruments;
            Logs = _Logs;
            Tests = _Tests;
            InstrumentGroups = _InstrumentGroups;
            Depositories = _Depositories;
            CMTradingPlatforms = _TradingPlatforms;
            TradeSettlementRules = _TradeSettlementRules;
            TradeNettingRules = _TradeNettingRules;
            Users = _Users;
            Indexes = _Indexes;
            MerchantBanks = _MerchantBanks;
            this.brokerageCommision = brokerageCommision;
            brokerageCommisionAML = _brokerageCommisionAML;
            priceFile = _priceFileRepository;
            TradeFile = _TradeFile;
            TradeRestrictions = _TradeRestrictions;
            ApprovalHistory = _ApprovalHistory;
            SaleOrder = _SaleOrder;
            tradeCorrection = _tradeCorrection;
            BuyOrder = _BuyOrder;
            brokerageCommisionAccountGroup = _brokerageCommisionAccountGroup;
            TransactionDay = _TransactionDay;
            Allocation = _Allocation;
            Approvals = _Approvals;
            Dashborad = _Dashboard;
            omnibusFileRepository = _omnibusFileRepository;
            fundCollectionRepository = _fundCollectionRepository;
            WithdrawalRepository = _withdrawalRepository;
            iPORepository = _iIPORepository;
            InstallmentSchedule = _installmentSchedule;
            equityIncorporationRepository = _equityIncorporationRepository;
            chargesRepository = _chargesRepository;
            corpActRepository = _corpActRepository;
            orderSheetRepository = _orderSheetRepository;
            tradeFileExportRepository = _tradeFileExportRepository;
            tradeSettlementRepository = _tradeSettlementRepository;
            reportRepository = _reportRepository;
            CDBLFileInformation = _CDBLFileInformation;
            AllotmentRepository = _AllotmentRepository;
            FAQRepository = _FAQRepository;
            coARepository = _coARepository;
            perpetualBondRepository = _perpetualBond;
            fundOperationRepository = _fundOperationRepository;
            fDRRepository = _fDRRepository;
            corporateActionDividend = _corporateActionDividend;
            dematRepository = _dematRepository;
            rematRepository = _rematRepository;
            marginRepository = _marginRepository;
            securityEliminationRepository = _securityEliminationRepository;
            documentRepository = _documentRepository;
            lockUnlockRepository = _lockUnlockRepository;
            sellSurrenderRepository = _sellSurrenderRepository;
            physicalInstrumentCollectionDelivery = _physicalInstrumentCollectionDelivery;
            divident = _dividentRepository;
            accountingTradeSettlementRepository = _accountingTradeSettlementRepository;
            insurancePremiumRepository = _insurancePremiumRepository;
            kYCRepository = _kYCRepository;
            unitFund = _unitFund;
            instrumentConversion = _instrumentConversion;
            settlementAccountRepository = _settlementAccountRepository;
            stockSplitRepository = _stockSplitRepository;
            auditInspectionRepository = _auditInspectionRepository;
            internalFundTransferRepository = _internalFundTransferRepository;
            accountSettlementRepository = _accountSettlementRepository;
            navRepository = _navRepository;
            voucherTemplateRepository = _voucherTemplateRepository;
        }

        public ITestRepository Tests { get; }
        public ICMExchangeRepository CMExchanges { get; }
        public IAssetManagerRepository CMAssetManagers { get; }
        public IOrganizationRepository Organizations { get; }
        public IBrokerRepository Brokers { get; }
        //public IBrokerListRepository BrokerLists { get; }
        public ICMMarketTypeRepository CMMarketTypes { get; }
        public ICMInstrumentRepository CMInstruments { get; }
        public IUpdateLogRepository Logs { get; }
        public IInstrumentGroupRepository InstrumentGroups { get; }
        public IDepositoryRepository Depositories { get; }
        public ICMTradingPlatformRepository CMTradingPlatforms { get; }
        public ISLTradeSettlementRuleRepository TradeSettlementRules { get; }
        public ISLTradeNettingRuleRepository TradeNettingRules { get; }
        public IUserRepository Users { get; }
        public ICMStockIndexRepository Indexes { get; }
        public ICMMerchantBankRepository MerchantBanks { get; }

        public IBrokerageCommisionRepository brokerageCommision { get; }

        public IBrokerageCommissionAMLRepository brokerageCommisionAML { get; }
        public IPriceFileRepository priceFile { get; }

        public ITradeFileRepository TradeFile { get; }
        public ITradeRestrictionRepository TradeRestrictions { get; }
        public ITransactionDayRepository TransactionDay { get; }

        public IApprovalRepository Approvals { get; }

        public IBrokerageCommisionAccountGroupRepository brokerageCommisionAccountGroup { get; }

        public IApprovalHistoryRepository ApprovalHistory { get; }

        public ISaleOrderRepository SaleOrder { get; }

        public ITradeCorrectionRepository tradeCorrection { get; }

        public IBuyOrderRepository BuyOrder { get; }

        public IAllocationRepository Allocation { get; }
        public IDashboradRepository Dashborad { get; }
        public IOmnibusFileRepository omnibusFileRepository { get; }
        public IFundCollectionRepository fundCollectionRepository { get; }


        public IWithdrawalRepository WithdrawalRepository { get; }
        public IIPORepository iPORepository { get; }

        public IInstallmentScheduleRepository InstallmentSchedule { get; }

        public IEquityIncorporationRepository equityIncorporationRepository { get; }
        public IChargesRepository chargesRepository { get; }
        public ICorpActRepository corpActRepository { get; }

        public ITradeFileExportRepository tradeFileExportRepository { get; }

        public IOrderSheetRepository orderSheetRepository { get; }
        public ITradeSettlementRepository tradeSettlementRepository { get; }
        public IReportRepository reportRepository { get; }
        public ICDBLFileInformationRepository CDBLFileInformation { get; }

        public IAllotmentRepository AllotmentRepository { get; }

        public IFAQRepository FAQRepository { get; }
        public IAccountingRepository coARepository { get; }
        public IPerpetualBondRepository perpetualBondRepository { get; }
        public IFundOperationRepository fundOperationRepository { get; }
        public IFDRRepository fDRRepository { get; }
        public ICorporateActionDividendRepository corporateActionDividend { get; }
        public IDematRepository dematRepository { get; }

        public IRematRepository rematRepository { get; }

        public IMarginRepository marginRepository { get; }
        public ISecurityEliminationRepository securityEliminationRepository { get; }

        public IDocumentRepository documentRepository { get; }
        public ILockUnlockRepository lockUnlockRepository { get; }

        public ISellSurrenderRepository sellSurrenderRepository { get; }
        public IPhysicalInstrumentCollectionDeliveryRepository physicalInstrumentCollectionDelivery { get; }

        public IDividendRepository divident { get; }
        public IAccountingTradeSettlementRepository accountingTradeSettlementRepository { get; }

        public IInsurancePremiumRepository insurancePremiumRepository { get; }

        public IKYCRepository kYCRepository { get; }
        public IUnitFundCollectionDeliveryRepository unitFund { get; }
        public IInstrumentConversionRepository instrumentConversion { get; }
        public ISettlementAccountRepository settlementAccountRepository { get; }

        public IStockSplitRepository stockSplitRepository { get; }

        public IAuditInspectionRepository auditInspectionRepository { get; }

        public IInternalFundTransferRepository internalFundTransferRepository { get; }
        public IAccountSettlementRepository accountSettlementRepository { get; }
        public INAVRepository navRepository { get; }
        public IVoucherTemplateRepository voucherTemplateRepository { get; }
	}


    public class GlobalSettingService : IGlobalSettingService
    {
        public readonly IConfiguration _configuration;
        public GlobalSettingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TransactionDateStatusDTO GetTransactionDateStatus(string UserName, int CompanyID, int BranchID)
        {

            TransactionDateStatusDTO transaction = new TransactionDateStatusDTO();

            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {
               
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName);
                parameters.Add("@CompanyID", CompanyID);
                parameters.Add("@BranchID", BranchID);

                try
                {
                    transaction = db.Query<TransactionDateStatusDTO>("[CM_GetTransactionDate]", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            }
            return transaction;
        }
        
        public List<TypeCodeDTO> GetTypeCodes(string typeName, int pTypeID = 0)
        {
            SqlConnection con = new SqlConnection();
            List<TypeCodeDTO> typeCodesList = new List<TypeCodeDTO>();
            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {
                string sql = "[dbo].[LstTypeCodes]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TypeName", typeName);
                if (pTypeID > 0)
                    parameters.Add("@PTypeId", pTypeID);
                try
                {
                    typeCodesList = db.Query<TypeCodeDTO>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                }
            }
            return typeCodesList;
        }

        public List<TypeCodebyCompanyDTO> GetTypeCodesCompanyWise(int CompanyID, int BranchID, string TypeName, int InstructionParam =0)
        {
            SqlConnection con = new SqlConnection();
            List<TypeCodebyCompanyDTO> typeCodesList = new List<TypeCodebyCompanyDTO>();
            using (var db = new SqlConnection(_configuration["DevConnectionStrings:ConnString"]))
            {
                string sql = "[dbo].[LstTypeCodesCompanyWise]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@TypeName", TypeName);
                parameters.Add("@CompanyID", CompanyID);
                parameters.Add("@BranchID", BranchID);
                if (InstructionParam >0)
                parameters.Add("@InstructionParam", InstructionParam);
                try
                {
                    typeCodesList = db.Query<TypeCodebyCompanyDTO>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                }
            }
            return typeCodesList;
        }

        
    }
}
