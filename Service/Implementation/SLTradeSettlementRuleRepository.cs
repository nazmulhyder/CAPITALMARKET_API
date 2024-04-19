using AutoMapper;
using Dapper;
using Model.DTOs.TradingPlatform;
using Model.DTOs.UpdateLog;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class SLTradeSettlementRuleRepository : ISLTradeSettlementRuleRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        private IMapper _mapper;

        public SLTradeSettlementRuleRepository(IMapper mapper,IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
            _mapper = mapper;
        }

        public async Task<string> AddUpdate(SLTradeSettlementRuleDTO dtodata, string userName)
        {
            try
            {
                #region Insert New Data

                if (dtodata.SettlementRuleID == 0 || dtodata.SettlementRuleID == null)
                {
                    string sp = "CM_InsertSLTradeSettlementRule";
                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@InstrumentTypeID", dtodata.InstrumentTypeID);
                    SpParameters.Add("@ExchangeID", dtodata.ExchangeID);
                    SpParameters.Add("@TradingPlatformID", dtodata.TradingPlatformID);
                    SpParameters.Add("@MarketID", dtodata.MarketID);
                    SpParameters.Add("@CategoryID", dtodata.CategoryID);
                    SpParameters.Add("@PurInstSettleAt", dtodata.PurInstSettleAt);
                    SpParameters.Add("@PurFundPaymentAt", dtodata.PurFundPaymentAt);
                    SpParameters.Add("@SellInstSettleAt", dtodata.SellInstSettleAt);
                    SpParameters.Add("@SellFundRcvAt", dtodata.SellFundRcvAt);
                    SpParameters.Add("@Remarks", dtodata.Remark);
                    SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                    #region log for Settlement [Save]
                    int SettlementID = Convert.ToInt32(result.ToString().Split('-').Last()); //NEW Settlement

                    UpdateLog LogMaster = new UpdateLog();
                    List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                    if (result.ToLower().Contains("saved successfully"))
                    {
                        List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(dtodata, new SLTradeSettlementRuleDTO());

                        //AllUpdatedproperties.Remove("ExchangeList");
                        //AllUpdatedproperties.Remove("LastRecordDate");
                        //AllUpdatedproperties.Remove("IssueDate");
                        //AllUpdatedproperties.Remove("CategoryID");
                        //AllUpdatedproperties.Remove("EPUDateInString");
                        //AllUpdatedproperties.Remove("MaturityDateInString");
                        //AllUpdatedproperties.Remove("OrganizationID");
                        //AllUpdatedproperties.Remove("DepositoryID");
                        //AllUpdatedproperties.Remove("DepositoryShortName");

                        //if (AllUpdatedproperties.Where(c => c == "MarketID").Count() > 0)
                        //{
                        //    var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                        //    if (CMMarket != null)
                        //    {
                        //        dtodata.MarketName = CMMarket.Where(c => c.typeCode == ItemNettingDetail.MarketID).FirstOrDefault().typeValue;
                        //        AllUpdatedproperties.Add("MarketName");
                        //    }
                        //}

                        //FOR Trade Settlement Setup
                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "SLTradeSettlementRule",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = dtodata.GetType().GetProperty(PerpertyName).GetValue(dtodata, null) == null ? "" : dtodata.GetType().GetProperty(PerpertyName).GetValue(dtodata, null).ToString(),
                                PKID = SettlementID
                            });
                        }

                        _logOperation.SaveUpdateLog(LogDetailList, userName, SettlementID, 7);

                    }
                    #endregion log for Settlement [Save]

                    return result;
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdateSLTradeSettlementRule";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@SettlementRuleID", dtodata.SettlementRuleID);
                    SpParameters.Add("@InstrumentTypeID", dtodata.InstrumentTypeID);
                    SpParameters.Add("@ExchangeID", dtodata.ExchangeID);
                    SpParameters.Add("@TradingPlatformID", dtodata.TradingPlatformID);
                    SpParameters.Add("@MarketID", dtodata.MarketID);
                    SpParameters.Add("@CategoryID", dtodata.CategoryID);
                    SpParameters.Add("@PurInstSettleAt", dtodata.PurInstSettleAt);
                    SpParameters.Add("@PurFundPaymentAt", dtodata.PurFundPaymentAt);
                    SpParameters.Add("@SellInstSettleAt", dtodata.SellInstSettleAt);
                    SpParameters.Add("@SellFundRcvAt", dtodata.SellFundRcvAt);
                    SpParameters.Add("@Remarks", dtodata.Remark);
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                    SLTradeSettlementRuleEditDTO PrevSLTradeSettlementRule = await GetSLTradeSettlementRuleById(Convert.ToInt32(dtodata.SettlementRuleID), userName);
                    SLTradeSettlementRuleDTO prevTradeSettlementRuleDTO = _mapper.Map<SLTradeSettlementRuleDTO>(PrevSLTradeSettlementRule);
                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
                    int SettlementID = Convert.ToInt32(result.ToString().Split('-').Last()); //update Settlement

                    #region log for Settlement [Update]
                    //List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                    //if (result.ToLower().Contains("updated successfully"))
                    //{

                    //    //var data = _mapper.Map<SLTradeSettlementRuleEditDTO>(dtodata);

                    //    List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(dtodata, prevTradeSettlementRuleDTO);
                    //    List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(dtodata, prevTradeSettlementRuleDTO);

                       
                    //    foreach (string PropertyName in TempUpdatedProperties)
                    //    {
                    //        if (PropertyName == "CategoryID")
                    //        {
                    //            var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                    //            if (Category != null)
                    //            {
                    //                PrevSLTradeSettlementRule.Category = Category.Where(c => c.typeCode == dtodata.CategoryID).FirstOrDefault().typeValue;
                    //                AllUpdatedproperties.Add("CategoryName");
                    //            }
                    //        }

                    //        //if (PropertyName == "MarketID")
                    //        //{
                    //        //    var Category = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                    //        //    if (Category != null)
                    //        //    {
                    //        //        PrevSLTradeSettlementRule.Category = Category.Where(c => c.typeCode == dtodata.CategoryID).FirstOrDefault().typeValue;
                    //        //        AllUpdatedproperties.Add("CategoryName");
                    //        //    }
                    //        //}

                    //    }

                    //    //FOR CMInstrument
                    //    foreach (string PropertyName in AllUpdatedproperties)
                    //    {

                    //        var item = (new UpdateLogDetailDto
                    //        {
                    //            TableName = "SLTradeSettlementRule",
                    //            ColumnName = PropertyName,
                    //            PrevContent = PrevSLTradeSettlementRule.GetType().GetProperty(PropertyName).GetValue(PrevSLTradeSettlementRule, null) == null ? "" : PrevSLTradeSettlementRule.GetType().GetProperty(PropertyName).GetValue(PrevSLTradeSettlementRule, null).ToString(),
                    //            UpdatedContent = dtodata.GetType().GetProperty(PropertyName).GetValue(dtodata, null) == null ? "" : dtodata.GetType().GetProperty(PropertyName).GetValue(dtodata, null).ToString(),
                    //            PKID = dtodata.SettlementRuleID
                    //        });

                    //        LogDetailList.Add(item);
                    //    }

                    //     _logOperation.SaveUpdateLog(LogDetailList, userName, SettlementID,7);


                    //}

                    #endregion log for Settlement [Update]
                 
                    return result;
                }
                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SLTradeSettlementRuleDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            throw new NotImplementedException();
        }

        public Task<List<SLTradeSettlementRuleEditDTO>> GetAllSLTradeSettlementRule(int CompanyID, int BranchID)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID };
            return _dbCommonOperation.ReadSingleTable<SLTradeSettlementRuleEditDTO>("[CM_ListSLTradeSettlementRule]", values);
        }

        public SLTradeSettlementRuleDTO GetById(int Id, string user)
        {
            throw new NotImplementedException();
        }

        public async Task<SLTradeSettlementRuleEditDTO> GetSLTradeSettlementRuleById(int SLTradeSettlementRuleID, string user)
        {
            List<SLTradeSettlementRuleEditDTO> results =  new List<SLTradeSettlementRuleEditDTO>();
            var values = new { SettlementRuleID = SLTradeSettlementRuleID, UserName = user };
            results = await _dbCommonOperation.ReadSingleTable<SLTradeSettlementRuleEditDTO>("[CM_QuerySLTradeSettlementRule]", values);
            return results.FirstOrDefault();

        }

        public string setInstumentTypeName(int instrumentTypeId)
        {
            switch(instrumentTypeId)
            {
                case 1:
                    return "Equity";

                case 2:
                    return "Bond";

                case 3:
                    return "Mutual Fund";
   

                case 4:
                    return "G Sec";

                default: 
                    return "";


            }  
        }
    }
}
