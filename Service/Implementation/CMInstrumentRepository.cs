using Dapper;
using Model.DTOs.Instrument;
using Model.DTOs.UpdateLog;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class CMInstrumentRepository : ICMInstrumentRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public CMInstrumentRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        public async Task<string> ApproveInstrument(ApprovalInstrumentDto approve, string userName)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@InstrumentID", approve.InstrumentID);
            SpParameters.Add("@InstrumentType", approve.InstrumentType);
            SpParameters.Add("@IsApproved", approve.IsApproved);
            SpParameters.Add("@FeedbackRemark", approve.FeedbackRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
            CMInstrumentDTO PrevEquityInstrument = new CMInstrumentDTO();

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveInstrument", SpParameters);
        }
        public async Task<string> AddUpdate(CMInstrumentDTO entityDto, string userName)
        {
            try
            {
                #region Insert New Data

                if (entityDto.InstrumentID == 0)
                {
                    string sp = "CM_InsertCMInstrument";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                    SpParameters.Add("@SectorID", entityDto.SectorID);
                    SpParameters.Add("@CategoryID", entityDto.CategoryID);
                    SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                    SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                    SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                    SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                    SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                    SpParameters.Add("@InstrumentTypeID", 1); //for EquityInstrument
                    SpParameters.Add("@ISIN", entityDto.ISIN);
                    SpParameters.Add("@FaceValue", entityDto.FaceValue);
                    SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                    SpParameters.Add("@EPS", entityDto.EPS);
                    SpParameters.Add("@PERatio", entityDto.PERatio);
                    SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                    SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                    SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                    SpParameters.Add("@InstrumentStatus", entityDto.InstrumentStatus);
                    SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
                    SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_EI_CMExchangeInstrument"));

                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                    CMInstrumentDTO PrevEquityInstrument = new CMInstrumentDTO();

                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                    int InstrumentID = 0; //NEW INTRUMENTID

                    //saved successfully, now save the log
                    UpdateLog LogMaster = new UpdateLog();
                    List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                    if (result.ToLower().Contains("saved successfully"))
                    {
                        InstrumentID = Convert.ToInt32(result.ToString().Split('-').Last());

                        List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, new CMInstrumentDTO());

                        AllUpdatedproperties.Remove("ExchangeList");
                        AllUpdatedproperties.Remove("LastRecordDate");
                        AllUpdatedproperties.Remove("IssueDate");
                        AllUpdatedproperties.Remove("CategoryID");
                        AllUpdatedproperties.Remove("EPUDateInString");
                        AllUpdatedproperties.Remove("MaturityDateInString");
                        AllUpdatedproperties.Remove("OrganizationID");
                        AllUpdatedproperties.Remove("DepositoryID");
                        AllUpdatedproperties.Remove("DepositoryShortName");

                        //FOR CMInstrument
                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "CMInstrument",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                                PKID = InstrumentID
                            });
                        }

                        //FOR CMExchangeInstrument
                        foreach (var UpdateExchange in entityDto.ExchangeList)
                        {
                            InstrumentExchangeDto PrevExhange = new InstrumentExchangeDto();
                            AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                            AllUpdatedproperties.Remove("ExcInstrID");
                            AllUpdatedproperties.Remove("TradingPlatformID");
                            AllUpdatedproperties.Remove("ExchangeID");
                            AllUpdatedproperties.Remove("MarketID");

                            foreach (string PerpertyName in AllUpdatedproperties)
                            {
                                LogDetailList.Add(new UpdateLogDetailDto
                                {
                                    TableName = "CMExchangeInstrument",
                                    ColumnName = PerpertyName,
                                    PrevContent = "",
                                    UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString(),
                                    PKID = 0
                                });
                            }
                        }

                        _logOperation.SaveUpdateLog(LogDetailList, userName, InstrumentID,6);
                    }
                    
                    result = result.Split('-')[0];
                    return result;
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateCMInstrument";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                    SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                    SpParameters.Add("@SectorID", entityDto.SectorID);
                    SpParameters.Add("@CategoryID", entityDto.CategoryID);
                    SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                    SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                    SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                    SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                    SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                    SpParameters.Add("@InstrumentTypeID", 1); //for EquityInstrument
                    SpParameters.Add("@ISIN", entityDto.ISIN);
                    SpParameters.Add("@FaceValue", entityDto.FaceValue);
                    SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                    SpParameters.Add("@EPS", entityDto.EPS);
                    SpParameters.Add("@PERatio", entityDto.PERatio);
                    SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                    SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                    SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                    SpParameters.Add("@InstrumentStatus ", entityDto.InstrumentStatus);
                    SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
                    SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_EI_CMExchangeInstrument"));

                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                    CMInstrumentDTO PrevEquityInstrument =  GetById(Convert.ToInt32(entityDto.InstrumentID), userName);

                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                    //saved successfully, now save the log
                    List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                    if (result.ToLower().Contains("saved successfully"))
                    {
                        List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);
                        List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);

                        foreach (string PropertyName in TempUpdatedProperties)
                        {
                            if (PropertyName == "CategoryID")
                            {
                                var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                                if (Category != null)
                                {
                                    entityDto.CategoryName = Category.Where(c => c.typeCode == entityDto.CategoryID).FirstOrDefault().typeValue;
                                    AllUpdatedproperties.Add("CategoryName");
                                }
                            }
                            if (PropertyName == "DepositoryID")
                            {
                                var depository = _globalSettingService.GetTypeCodes("CMDepository", 0);
                                if (depository != null)
                                {
                                    entityDto.DepositoryCompanyName = depository.Where(c => c.typeCode == entityDto.DepositoryID).FirstOrDefault().typeValue;
                                    AllUpdatedproperties.Add("DepositoryCompanyName");
                                }
                            }

                        }

                        AllUpdatedproperties.Remove("ExchangeList");
                        AllUpdatedproperties.Remove("LastRecordDate");
                        AllUpdatedproperties.Remove("IssueDate");
                        AllUpdatedproperties.Remove("CategoryID");
                        AllUpdatedproperties.Remove("EPUDateInString");
                        AllUpdatedproperties.Remove("MaturityDateInString");
                        AllUpdatedproperties.Remove("OrganizationID");
                        AllUpdatedproperties.Remove("DepositoryID");
                        AllUpdatedproperties.Remove("DepositoryShortName");

                        //FOR CMInstrument
                        foreach (string PropertyName in AllUpdatedproperties)
                        {

                            var item = (new UpdateLogDetailDto
                            {
                                TableName = "CMInstrument",
                                ColumnName = PropertyName,
                                PrevContent = PrevEquityInstrument.GetType().GetProperty(PropertyName).GetValue(PrevEquityInstrument, null) == null ? "" : PrevEquityInstrument.GetType().GetProperty(PropertyName).GetValue(PrevEquityInstrument, null).ToString(),
                                UpdatedContent = entityDto.GetType().GetProperty(PropertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PropertyName).GetValue(entityDto, null).ToString(),
                                PKID = entityDto.InstrumentID
                            });

                            LogDetailList.Add(item);
                        }


                        //FOR CMExchangeInstrument
                        //FOR CMExchangeInstrument

                        List<InstrumentExchangeDto> AddedExchangeList = new List<InstrumentExchangeDto>();
                        List<InstrumentExchangeDto> UpdatedExchangeList = new List<InstrumentExchangeDto>();
                        List<InstrumentExchangeDto> DeletedExchangeList = new List<InstrumentExchangeDto>();

                        AddedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID == 0).ToList();
                        UpdatedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID > 0).ToList();

                        foreach (InstrumentExchangeDto ex in PrevEquityInstrument.ExchangeList)
                        {
                            if (entityDto.ExchangeList.Where(e => e.ExcInstrID == ex.ExcInstrID).ToList().Count == 0) DeletedExchangeList.Add(ex);
                        }


                        foreach (InstrumentExchangeDto UpdateExchange in AddedExchangeList)
                        {
                            var PrevExhange = new InstrumentExchangeDto();

                            AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                            AllUpdatedproperties.Remove("ExcInstrID");
                            AllUpdatedproperties.Remove("TradingPlatformID");
                            AllUpdatedproperties.Remove("ExchangeID");
                            AllUpdatedproperties.Remove("MarketID");

                            foreach (string PerpertyName in AllUpdatedproperties)
                            {
                                var item = new UpdateLogDetailDto();

                                item.TableName = "CMExchangeInstrument";
                                item.ColumnName = PerpertyName;
                                item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                                item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                                item.PKID = UpdateExchange.ExcInstrID;

                                LogDetailList.Add(item);
                            }
                        }

                        foreach (InstrumentExchangeDto UpdateExchange in UpdatedExchangeList)
                        {
                            InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                            if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                            AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                            AllUpdatedproperties.Remove("ExcInstrID");
                            AllUpdatedproperties.Remove("TradingPlatformID");
                            AllUpdatedproperties.Remove("ExchangeID");

                            if (AllUpdatedproperties.Where(c => c == "MarketID") != null)
                            {
                                var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                                if (CMMarket != null)
                                {
                                    UpdateExchange.MarketName = CMMarket.Where(c => c.typeCode == UpdateExchange.MarketID).FirstOrDefault().typeValue;
                                    AllUpdatedproperties.Add("MarketName");
                                    AllUpdatedproperties.Remove("MarketID");
                                }
                            }

                            foreach (string PerpertyName in AllUpdatedproperties)
                            {

                                var item = new UpdateLogDetailDto();

                                item.TableName = "CMExchangeInstrument";
                                item.ColumnName = PerpertyName;
                                item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                                item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                                item.PKID = UpdateExchange.ExcInstrID;

                                LogDetailList.Add(item);
                            }
                        }


                        foreach (InstrumentExchangeDto UpdateExchange in DeletedExchangeList)
                        {
                            InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                            if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                            AllUpdatedproperties = ObjectComparison.GetProperties(UpdateExchange);

                            AllUpdatedproperties.Remove("ExcInstrID");
                            AllUpdatedproperties.Remove("TradingPlatformID");
                            AllUpdatedproperties.Remove("ExchangeID");
                            AllUpdatedproperties.Remove("MarketID");
                            AllUpdatedproperties.Remove("CMMFInstrumentDetailID");

                            foreach (string PerpertyName in AllUpdatedproperties)
                            {
                                var item = new UpdateLogDetailDto();

                                item.TableName = "CMExchangeInstrument";
                                item.ColumnName = PerpertyName;
                                item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                                item.UpdatedContent = "";
                                item.PKID = PrevExhange.ExcInstrID;

                                LogDetailList.Add(item);
                            }
                        }


                        _logOperation.SaveUpdateLog(LogDetailList, userName, Convert.ToInt32(entityDto.InstrumentID),6);
                    }

                    return result;

                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> AddUpdateBondInstrument(CMBondInstrumentDto entityDto, string userName)
        {


            if (entityDto.InstrumentID == 0)
            {
                string sp = "CM_InsertBondInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@InstrumentTypeID", 2); // for BondInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                SpParameters.Add("@EPS", entityDto.EPS);
                SpParameters.Add("@PERatio", entityDto.PERatio);
                SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);

                SpParameters.Add("@BondInstDetailID", entityDto.BondInstDetailID);
                SpParameters.Add("@IssuerName", entityDto.IssuerName);
                SpParameters.Add("@CouponRate", entityDto.CouponRate);
                SpParameters.Add("@MarketYield", entityDto.MarketYield);
                SpParameters.Add("@TrusteeName", entityDto.TrusteeName);
                SpParameters.Add("@BondRating", entityDto.BondRating);
                SpParameters.Add("@CouponFrequency", entityDto.CouponFrequency);
                SpParameters.Add("@MaturityDate", entityDto.MaturityDate);

                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_BI_CMExchangeInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMInstrumentDTO PrevInstrument = new CMInstrumentDTO();

                var result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                int InstrumentID = 0; //NEW INTRUMENTID
                //saved successfully, now save the log
                UpdateLog LogMaster = new UpdateLog();
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                if (result.ToLower().Contains("saved successfully"))
                {
                    InstrumentID = Convert.ToInt32(result.Split('-').Last()); Convert.ToInt32(result.Split('-').Last());
                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, new CMBondInstrumentDto());

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");

                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = "",
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument
                    foreach (var UpdateExchange in entityDto.ExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = new InstrumentExchangeDto();
                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "CMExchangeInstrument",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString(),
                                PKID = 0
                            });
                        }
                    }

                    _logOperation.SaveUpdateLog(LogDetailList, userName, InstrumentID,6);
                }

                result = result.Split('-')[0];
                return result;
            }
            else
            {
                string sp = "CM_UpdateBondInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@InstrumentTypeID", 2); // for BondInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                SpParameters.Add("@EPS", entityDto.EPS);
                SpParameters.Add("@PERatio", entityDto.PERatio);
                SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);

                SpParameters.Add("@BondInstDetailID", entityDto.BondInstDetailID);
                SpParameters.Add("@IssuerName", entityDto.IssuerName);
                SpParameters.Add("@CouponRate", entityDto.CouponRate);
                SpParameters.Add("@MarketYield", entityDto.MarketYield);
                SpParameters.Add("@TrusteeName", entityDto.TrusteeName);
                SpParameters.Add("@BondRating", entityDto.BondRating);
                SpParameters.Add("@CouponFrequency", entityDto.CouponFrequency);
                SpParameters.Add("@MaturityDate", entityDto.MaturityDate);

                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_BI_CMExchangeInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMBondInstrumentDto PrevEquityInstrument = GetBondInstrumentById(Convert.ToInt32(entityDto.InstrumentID), userName).Result;

                string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                //saved successfully, now save the log
                UpdateLog LogMaster = new UpdateLog();
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();


                if (result.ToLower().Contains("saved successfully"))
                {
                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);
                    List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);

                    foreach (string PropertyName in TempUpdatedProperties)
                    {
                        if (PropertyName == "CategoryID")
                        {
                            var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                            if (Category != null)
                            {
                                entityDto.CategoryName = Category.Where(c => c.typeCode == entityDto.CategoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("CategoryName");
                            }
                        }
                        if (PropertyName == "DepositoryID")
                        {
                            var depository = _globalSettingService.GetTypeCodes("CMDepository", 0);
                            if (depository != null)
                            {
                                entityDto.DepositoryCompanyName = depository.Where(c => c.typeCode == entityDto.DepositoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("DepositoryCompanyName");
                            }
                        }

                    }

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");
                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = PrevEquityInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevEquityInstrument, null) == null ? "" : PrevEquityInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevEquityInstrument, null).ToString(),
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = entityDto.InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument

                    List<InstrumentExchangeDto> AddedExchangeList = new List<InstrumentExchangeDto>();
                    List<InstrumentExchangeDto> UpdatedExchangeList = new List<InstrumentExchangeDto>();
                    List<InstrumentExchangeDto> DeletedExchangeList = new List<InstrumentExchangeDto>();

                    AddedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID == 0).ToList();
                    UpdatedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID > 0).ToList();

                    foreach (InstrumentExchangeDto ex in PrevEquityInstrument.ExchangeList)
                    {
                        if (entityDto.ExchangeList.Where(e => e.ExcInstrID == ex.ExcInstrID).ToList().Count == 0) DeletedExchangeList.Add(ex);
                    }


                    foreach (InstrumentExchangeDto UpdateExchange in AddedExchangeList)
                    {
                        var PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }

                    foreach (InstrumentExchangeDto UpdateExchange in UpdatedExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");

                        if (AllUpdatedproperties.Where(c => c == "MarketID") != null)
                        {
                            var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                            if (CMMarket != null)
                            {
                                UpdateExchange.MarketName = CMMarket.Where(c => c.typeCode == UpdateExchange.MarketID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("MarketName");
                                AllUpdatedproperties.Remove("MarketID");
                            }
                        }
                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }


                    foreach (InstrumentExchangeDto UpdateExchange in DeletedExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetProperties(UpdateExchange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = "";
                            item.PKID = PrevExhange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }


                   _logOperation.SaveUpdateLog(LogDetailList, userName, Convert.ToInt32(entityDto.InstrumentID),6);
                }
                return result;
            }
        }

        public async Task<string> AddUpdateGsecInstrument(GsecInstrumentDto entityDto, string userName)
        {
            CMGSecInstDetailDto detailDto = new CMGSecInstDetailDto
            {
                InstrumentID = entityDto.InstrumentID,
                GSecInstDetailID = entityDto.GSecInstDetailID,
                //TotalOutstanding = entityDto.TotalOutstanding,
                IssueDate = entityDto.IssueDate,
                //IssuePrice = entityDto.IssuePrice,
                MaturityDate = entityDto.MaturityDate,
                CouponFrequency = entityDto.CouponFrequency,
                CouponRate = entityDto.CouponRate,
                //Yield = entityDto.Yield,
                //EPU = entityDto.EPU,
                //InformationDate = entityDto.InformationDate,
                //LastInterestPayoutDate = entityDto.LastInterestPayoutDate,
                AccruedInterest = entityDto.AccruedInterest

            };

            List<CMGSecInstDetailDto> detailDtos = new List<CMGSecInstDetailDto>();
            detailDtos.Add(detailDto);

            if (entityDto.InstrumentID == 0 || entityDto.InstrumentID == null)
            {
                string sp = "CM_InsertGsecInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                //SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                //SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                //SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                //SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                //SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@InstrumentTypeID", 4); // for GsecInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                //SpParameters.Add("@EPS", entityDto.EPS);
                //SpParameters.Add("@PERatio", entityDto.PERatio);
                //SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                //SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                //SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus ", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
                SpParameters.Add("@LastCouponPaymentDate", entityDto.LastCouponPaymentDate);
                SpParameters.Add("@NextCouponDate", entityDto.NextCouponDate);
                SpParameters.Add("@Type_CMGSecInstDetailList", ListtoDataTableConverter.ToDataTable(detailDtos).AsTableValuedParameter("Type_CMGSecInstDetail"));
                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_CMExchangeInstrument"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                GsecInstrumentDto PrevInstrument = new GsecInstrumentDto();

                var result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                int InstrumentID = 0; //NEW INTRUMENTID
                //saved successfully, now save the log
                UpdateLog LogMaster = new UpdateLog();
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                if (result.ToLower().Contains("saved successfully"))
                {

                    InstrumentID = Convert.ToInt32(result.Split('-').Last());

                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, new GsecInstrumentDto());

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");
                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = "",
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument
                    foreach (var UpdateExchange in entityDto.ExchangeList)
                    {
                        GsecInstrumentExchangeDto PrevExhange = new GsecInstrumentExchangeDto();
                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");
                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "CMExchangeInstrument",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString(),
                                PKID = 0
                            });
                        }
                    }

                   _logOperation.SaveUpdateLog(LogDetailList, userName, InstrumentID,6);
                }

                result = result.Split('-')[0];
                return result;
            }
            else
            {
                string sp = "CM_UpdateGsecInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                //SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                //SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                //SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                //SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                //SpParameters.Add("@NetAssetValue", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@InstrumentTypeID", 4); // for GsecInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                //SpParameters.Add("@EPS", entityDto.EPS);
                //SpParameters.Add("@PERatio", entityDto.PERatio);
                //SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                //SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                //SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus ", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
                SpParameters.Add("@LastCouponPaymentDate", entityDto.LastCouponPaymentDate);
                SpParameters.Add("@NextCouponDate", entityDto.NextCouponDate);
                SpParameters.Add("@Type_CMGSecInstDetailList", ListtoDataTableConverter.ToDataTable(detailDtos).AsTableValuedParameter("Type_CMGSecInstDetail"));
                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_CMExchangeInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                GsecInstrumentDto PrevInstrument = GetGsecInstrumentById(Convert.ToInt32(entityDto.InstrumentID), userName).Result;

                var result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                //saved successfully, now save the log
                UpdateLog LogMaster = new UpdateLog();
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                if (result.ToLower().Contains("saved successfully"))
                {
                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, PrevInstrument);
                    List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(entityDto, PrevInstrument);

                    foreach (string PropertyName in TempUpdatedProperties)
                    {
                        if (PropertyName == "CategoryID")
                        {
                            var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                            if (Category != null)
                            {
                                entityDto.CategoryName = Category.Where(c => c.typeCode == entityDto.CategoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("CategoryName");
                            }
                        }
                        if (PropertyName == "DepositoryID")
                        {
                            var depository = _globalSettingService.GetTypeCodes("CMDepository", 0);
                            if (depository != null)
                            {
                                entityDto.DepositoryCompanyName = depository.Where(c => c.typeCode == entityDto.DepositoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("DepositoryCompanyName");
                            }
                        }

                    }

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");

                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = PrevInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevInstrument, null) == null ? "" : PrevInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevInstrument, null).ToString(),
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = entityDto.InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument

                    List<GsecInstrumentExchangeDto> AddedExchangeList = new List<GsecInstrumentExchangeDto>();
                    List<GsecInstrumentExchangeDto> UpdatedExchangeList = new List<GsecInstrumentExchangeDto>();
                    List<GsecInstrumentExchangeDto> DeletedExchangeList = new List<GsecInstrumentExchangeDto>();

                    AddedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID == 0).ToList();
                    UpdatedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID > 0).ToList();

                    foreach (GsecInstrumentExchangeDto ex in PrevInstrument.ExchangeList)
                    {
                        if (entityDto.ExchangeList.Where(e => e.ExcInstrID == ex.ExcInstrID).ToList().Count == 0) DeletedExchangeList.Add(ex);
                    }


                    foreach (GsecInstrumentExchangeDto UpdateExchange in AddedExchangeList)
                    {
                        var PrevExhange = new GsecInstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }

                    foreach (GsecInstrumentExchangeDto UpdateExchange in UpdatedExchangeList)
                    {
                        GsecInstrumentExchangeDto PrevExhange = PrevInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new GsecInstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        if (AllUpdatedproperties.Where(c => c == "MarketID") != null)
                        {
                            var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                            if (CMMarket != null)
                            {
                                UpdateExchange.MarketName = CMMarket.Where(c => c.typeCode == UpdateExchange.MarketID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("MarketName");
                                AllUpdatedproperties.Remove("MarketID");
                            }
                        }

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }


                    foreach (GsecInstrumentExchangeDto UpdateExchange in DeletedExchangeList)
                    {
                        GsecInstrumentExchangeDto PrevExhange = PrevInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new GsecInstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetProperties(UpdateExchange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = "";
                            item.PKID = PrevExhange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }


                   _logOperation.SaveUpdateLog(LogDetailList, userName, Convert.ToInt32(entityDto.InstrumentID),6);
                }

                return result;
            }
        }

        public async Task<string> AddUpdateMutualFundInstrument(CMInstrumentDTO entityDto, string userName)
        {
            CMMFInstrumentDetailDto detailDto = new CMMFInstrumentDetailDto
            {
                CMMFInstrumentDetailID = entityDto.CMMFInstrumentDetailID,
                Tenor = entityDto.Tenor,
                IssueDate = entityDto.IssueDate,
                FundManager = entityDto.FundManager,
                Custody = entityDto.Custody,
                Trustee = entityDto.Custody,
                EPU = entityDto.EPU,
                EPUDate = entityDto.EPUDate,
                InstrumentID = entityDto.InstrumentID
            };

            List<CMMFInstrumentDetailDto> detailList = new List<CMMFInstrumentDetailDto>();
            detailList.Add(detailDto);

            if (entityDto.InstrumentID == 0)
            {
                string sp = "CM_InsertMutualFundInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@NetAssetValue", entityDto.NetAssetValue);
                SpParameters.Add("@InstrumentTypeID", 3); // for MutualFundInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                SpParameters.Add("@EPS", entityDto.EPS);
                SpParameters.Add("@PERatio", entityDto.PERatio);
                SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus ", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);
                SpParameters.Add("@CMMFInstrumentDetail", ListtoDataTableConverter.ToDataTable(detailList).AsTableValuedParameter("Type_CMMFInstrumentDetail"));

                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_MF_CMExchangeInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMInstrumentDTO PrevInstrument = new CMInstrumentDTO();

                var result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                //saved successfully, now save the log
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();
                int InstrumentID = Convert.ToInt32(result.Split('-').Last()); //NEW INTRUMENTID
                if (result.ToLower().Contains("saved successfully"))
                {
                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, new CMInstrumentDTO());

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");

                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = "",
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument
                    foreach (var UpdateExchange in entityDto.ExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = new InstrumentExchangeDto();
                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "CMExchangeInstrument",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString(),
                                PKID = 0
                            });
                        }
                    }

                    _logOperation.SaveUpdateLog(LogDetailList, userName, InstrumentID,6);
                }

                result = result.Split('-')[0];
                return result;
            }
            else
            {
                string sp = "CM_UpdateMutualFundInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@InstrumentID", entityDto.InstrumentID);
                SpParameters.Add("@InstrumentName", entityDto.InstrumentName);
                SpParameters.Add("@SectorID", entityDto.SectorID);
                SpParameters.Add("@CategoryID", entityDto.CategoryID);
                SpParameters.Add("@AuthorizedCapital", entityDto.AuthorizedCapital);
                SpParameters.Add("@PaidUpCapital", entityDto.PaidUpCapital);
                SpParameters.Add("@ReserveCapital", entityDto.ReserveCapital);
                SpParameters.Add("@NoOfOutstandingShares", entityDto.NoOfOutstandingShares);
                SpParameters.Add("@NetAssetValue", entityDto.NetAssetValue);
                SpParameters.Add("@InstrumentTypeID", 3); // for MutualFundInstrument
                SpParameters.Add("@ISIN", entityDto.ISIN);
                SpParameters.Add("@FaceValue", entityDto.FaceValue);
                SpParameters.Add("@MarketLotSize", entityDto.MarketLotSize);
                SpParameters.Add("@EPS", entityDto.EPS);
                SpParameters.Add("@PERatio", entityDto.PERatio);
                SpParameters.Add("@LastRecordDate", entityDto.LastRecordDate);
                SpParameters.Add("@OrganizationID", entityDto.OrganizationID);
                SpParameters.Add("@ListingStatus", entityDto.ListingStatus);
                SpParameters.Add("@InstrumentStatus ", entityDto.InstrumentStatus);
                SpParameters.Add("@DepositoryID", entityDto.DepositoryID);

                SpParameters.Add("@CMMFInstrumentDetail", ListtoDataTableConverter.ToDataTable(detailList).AsTableValuedParameter("Type_CMMFInstrumentDetail"));

                SpParameters.Add("@Type_CMExchangeInstrumentList", ListtoDataTableConverter.ToDataTable(entityDto.ExchangeList).AsTableValuedParameter("Type_MF_CMExchangeInstrument"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CMInstrumentDTO PrevEquityInstrument = GetMutualFundInstrumentById(Convert.ToInt32(entityDto.InstrumentID), userName).Result;

                string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                //saved successfully, now save the log
                UpdateLog LogMaster = new UpdateLog();
                List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                if (result.ToLower().Contains("saved successfully"))
                {
                    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);
                    List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(entityDto, PrevEquityInstrument);

                    foreach (string PropertyName in TempUpdatedProperties)
                    {
                        if (PropertyName == "CategoryID")
                        {
                            var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                            if (Category != null)
                            {
                                entityDto.CategoryName = Category.Where(c => c.typeCode == entityDto.CategoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("CategoryName");
                            }
                        }
                        if (PropertyName == "DepositoryID")
                        {
                            var depository = _globalSettingService.GetTypeCodes("CMDepository", 0);
                            if (depository != null)
                            {
                                entityDto.DepositoryCompanyName = depository.Where(c => c.typeCode == entityDto.DepositoryID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("DepositoryCompanyName");
                            }
                        }

                    }

                    AllUpdatedproperties.Remove("ExchangeList");
                    AllUpdatedproperties.Remove("LastRecordDate");
                    AllUpdatedproperties.Remove("IssueDate");
                    AllUpdatedproperties.Remove("CategoryID");
                    AllUpdatedproperties.Remove("EPUDateInString");
                    AllUpdatedproperties.Remove("MaturityDateInString");
                    AllUpdatedproperties.Remove("OrganizationID");
                    AllUpdatedproperties.Remove("DepositoryID");
                    AllUpdatedproperties.Remove("DepositoryShortName");

                    //FOR CMInstrument
                    foreach (string PerpertyName in AllUpdatedproperties)
                    {
                        LogDetailList.Add(new UpdateLogDetailDto
                        {
                            TableName = "CMInstrument",
                            ColumnName = PerpertyName,
                            PrevContent = PrevEquityInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevEquityInstrument, null) == null ? "" : PrevEquityInstrument.GetType().GetProperty(PerpertyName).GetValue(PrevEquityInstrument, null).ToString(),
                            UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                            PKID = entityDto.InstrumentID
                        });
                    }

                    //FOR CMExchangeInstrument

                    List<InstrumentExchangeDto> AddedExchangeList = new List<InstrumentExchangeDto>();
                    List<InstrumentExchangeDto> UpdatedExchangeList = new List<InstrumentExchangeDto>();
                    List<InstrumentExchangeDto> DeletedExchangeList = new List<InstrumentExchangeDto>();

                    AddedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID == 0).ToList();
                    UpdatedExchangeList = entityDto.ExchangeList.Where(e => e.ExcInstrID > 0).ToList();

                    foreach (InstrumentExchangeDto ex in PrevEquityInstrument.ExchangeList)
                    {
                        if (entityDto.ExchangeList.Where(e => e.ExcInstrID == ex.ExcInstrID).ToList().Count == 0) DeletedExchangeList.Add(ex);
                    }


                    foreach (InstrumentExchangeDto UpdateExchange in AddedExchangeList)
                    {
                        var PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }

                    foreach (InstrumentExchangeDto UpdateExchange in UpdatedExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateExchange, PrevExhange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");

                        if (AllUpdatedproperties.Where(c => c == "MarketID") != null)
                        {
                            var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                            if (CMMarket != null)
                            {
                                UpdateExchange.MarketName = CMMarket.Where(c => c.typeCode == UpdateExchange.MarketID).FirstOrDefault().typeValue;
                                AllUpdatedproperties.Add("MarketName");
                                AllUpdatedproperties.Remove("MarketID");
                            }
                        }

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null) == null ? "" : UpdateExchange.GetType().GetProperty(PerpertyName).GetValue(UpdateExchange, null).ToString();
                            item.PKID = UpdateExchange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }


                    foreach (InstrumentExchangeDto UpdateExchange in DeletedExchangeList)
                    {
                        InstrumentExchangeDto PrevExhange = PrevEquityInstrument.ExchangeList.Where(e => e.ExcInstrID == UpdateExchange.ExcInstrID).FirstOrDefault();

                        if (PrevExhange == null) PrevExhange = new InstrumentExchangeDto();

                        AllUpdatedproperties = ObjectComparison.GetProperties(UpdateExchange);

                        AllUpdatedproperties.Remove("ExcInstrID");
                        AllUpdatedproperties.Remove("TradingPlatformID");
                        AllUpdatedproperties.Remove("ExchangeID");
                        AllUpdatedproperties.Remove("MarketID");

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            var item = new UpdateLogDetailDto();

                            item.TableName = "CMExchangeInstrument";
                            item.ColumnName = PerpertyName;
                            item.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                            item.UpdatedContent = "";
                            item.PKID = PrevExhange.ExcInstrID;

                            LogDetailList.Add(item);
                        }
                    }

                    _logOperation.SaveUpdateLog(LogDetailList, userName, Convert.ToInt32(entityDto.InstrumentID),6);
                }
                return result;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CMInstrumentDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public Task<List<CMInstrumentDTO>> GetAllInstrumentList(string InstrumentType, int PageNo, int PerPage, string SearchKeyword, string ApprovalStatus)
        {           
            var values = new { 
                CompanyID = 1, 
                InstrumentType = InstrumentType, 
                PageNo = PageNo, 
                PerPage = PerPage,
                ApprovalStatus = ApprovalStatus,
                SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<CMInstrumentDTO>("[CM_ListCMInstrument]", values);
        }

        //EQUITY
        public CMInstrumentDTO GetById(int Id, string user)
        {
            CMInstrumentDTO Instrument = new CMInstrumentDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstrumentID", Id),
                new SqlParameter("@InstrumentTypeID",  1) //for equity
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMInstrument]", sqlParams);

            Instrument = CustomConvert.DataSetToList<CMInstrumentDTO>(DataSets.Tables[0]).First();

            Instrument.ExchangeList = CustomConvert.DataSetToList<InstrumentExchangeDto>(DataSets.Tables[1]);

            return Instrument;
        }

        public async Task<CMBondInstrumentDto> GetBondInstrumentById(int InstrumentID, string user)
        {
            CMBondInstrumentDto Instrument = new CMBondInstrumentDto();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstrumentID", InstrumentID),
                new SqlParameter("@InstrumentTypeID",  2) //for bond
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMInstrument]", sqlParams);

            Instrument = CustomConvert.DataSetToList<CMBondInstrumentDto>(DataSets.Tables[0]).First();

            Instrument.ExchangeList = CustomConvert.DataSetToList<InstrumentExchangeDto>(DataSets.Tables[1]);

            return Instrument;
        }

        public async Task<GsecInstrumentDto> GetGsecInstrumentById(int InstrumentID, string user)
        {

            GsecInstrumentDto Instrument = new GsecInstrumentDto();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstrumentID", InstrumentID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMGsecInstrument]", sqlParams);

            Instrument = CustomConvert.DataSetToList<GsecInstrumentDto>(DataSets.Tables[0]).First();
            Instrument.ExchangeList = CustomConvert.DataSetToList<GsecInstrumentExchangeDto>(DataSets.Tables[1]);
            return Instrument;
        }

        public async Task<CMInstrumentDTO> GetMutualFundInstrumentById(int InstrumentID, string user)
        {

            CMInstrumentDTO Instrument = new CMInstrumentDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstrumentID", InstrumentID),
                new SqlParameter("@InstrumentTypeID",  3) //for MF
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryCMInstrument]", sqlParams);

            Instrument = CustomConvert.DataSetToList<CMInstrumentDTO>(DataSets.Tables[0]).First();

            Instrument.ExchangeList = CustomConvert.DataSetToList<InstrumentExchangeDto>(DataSets.Tables[1]);

            return Instrument;
        }
    }
}
