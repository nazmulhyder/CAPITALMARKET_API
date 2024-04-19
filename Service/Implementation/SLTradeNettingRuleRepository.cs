using AutoMapper;
using Dapper;
using Model.DTOs.TradingPlatform;
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
    public class SLTradeNettingRuleRepository : ISLTradeNettingRuleRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        private IMapper _mapper;

        public SLTradeNettingRuleRepository(IMapper mapper, IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
            _mapper = mapper;
        }

        public async Task<string> AddUpdate(SLTradeNettingRuleSetDTO entityDto, string userName)
        {
            try
            {

                #region Insert New Data

                if (entityDto.NettingRuleSetID == 0 || entityDto.NettingRuleSetID == null)
                {
                    string sp = "CM_InsertSLTradeNettingRuleSet";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@NettingRuleSetName", entityDto.NettingRuleSetName);
                    SpParameters.Add("@IsNettingAllowed", entityDto.IsNettingAllowed);
                    SpParameters.Add("@NettingType", entityDto.NettingType);
                    SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                    //SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@SLTradeNettingRuleDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLTradeNettingRuleSetDetails).AsTableValuedParameter("Type_SLTradeNettingRuleSetDetail"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                    #region log for Netting [Save]
                    int NettingRuleID = Convert.ToInt32(result.ToString().Split('-').Last()); //NEW Netting Setup

                    UpdateLog LogMaster = new UpdateLog();
                    List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();

                    if (result.ToLower().Contains("saved successfully"))
                    {
                        List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, new SLTradeNettingRuleSetDTO());

                        AllUpdatedproperties.Remove("SLTradeNettingRuleSetDetails");


                        if (AllUpdatedproperties.Where(c => c == "ExchangeID").Count()>0)
                        {
                            var CMExchange = _globalSettingService.GetTypeCodes("CMExchange", 0);
                            if (CMExchange != null)
                            {
                                entityDto.ExchangeName = CMExchange.Where(c => c.typeCode == entityDto.ExchangeID).FirstOrDefault().typeValue;
                                entityDto.ExchangeShortName = CMExchange.Where(c => c.typeCode == entityDto.ExchangeID).FirstOrDefault().typeShortName;
                                AllUpdatedproperties.Add("ExchangeName");
                                AllUpdatedproperties.Add("ExchangeShortName");
                                //AllUpdatedproperties.Remove("MarketID");
                                //AllUpdatedproperties.Remove("MarketShortName");
                            }
                        }

                        foreach (string PerpertyName in AllUpdatedproperties)
                        {
                            LogDetailList.Add(new UpdateLogDetailDto
                            {
                                TableName = "SLTradeNettingRuleSet",
                                ColumnName = PerpertyName,
                                PrevContent = "",
                                UpdatedContent = entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PerpertyName).GetValue(entityDto, null).ToString(),
                                PKID = NettingRuleID
                            });
                        }

                        //FOR new SLTradeNettingRuleSetDetails 
                        foreach (var UpdateNettingDetail in entityDto.SLTradeNettingRuleSetDetails)
                        {
                            SLTradeNettingRuleSetDetailDTO PrevNettingDetail = new SLTradeNettingRuleSetDetailDTO();
                            AllUpdatedproperties = ObjectComparison.GetChangedProperties(UpdateNettingDetail, PrevNettingDetail);

                            AllUpdatedproperties.Remove("NettingRuleSetDetailID");
                            AllUpdatedproperties.Remove("NettingRuleSetID");
                            //AllUpdatedproperties.Remove("MarketID");
                            //AllUpdatedproperties.Remove("MarketID");


                            if (AllUpdatedproperties.Where(c => c == "CategoryID").Count() > 0)
                            {
                                var CMCategory = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                                if (CMCategory != null)
                                {
                                    UpdateNettingDetail.Category = CMCategory.Where(c => c.typeCode == UpdateNettingDetail.CategoryID).FirstOrDefault().typeValue;
                                    AllUpdatedproperties.Add("Category");
                                }
                            }

                            if (AllUpdatedproperties.Where(c => c == "MarketID").Count() > 0)
                            {
                                var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                                if (CMMarket != null)
                                {
                                    UpdateNettingDetail.MarketName = CMMarket.Where(c => c.typeCode == UpdateNettingDetail.MarketID).FirstOrDefault().typeValue;
                                    AllUpdatedproperties.Add("MarketName");
                                    AllUpdatedproperties.Remove("MarketShortName");
                                }
                            }

                            foreach (string PerpertyName in AllUpdatedproperties)
                            {
                                LogDetailList.Add(new UpdateLogDetailDto
                                {
                                    TableName = "SLTradeNettingRuleSetDetail",
                                    ColumnName = PerpertyName,
                                    PrevContent = "",
                                    UpdatedContent = UpdateNettingDetail.GetType().GetProperty(PerpertyName).GetValue(UpdateNettingDetail, null) == null ? "" : UpdateNettingDetail.GetType().GetProperty(PerpertyName).GetValue(UpdateNettingDetail, null).ToString(),
                                    PKID = 0
                                });
                            }
                        }

                        _logOperation.SaveUpdateLog(LogDetailList, userName, NettingRuleID,8);
                    }
                    #endregion log for Netting [Save]

                    return result;
                }
                #endregion

                #region Update Data
                else
                {
                    string sp = "CM_UpdatetSLTradeNettingRuleSet";

                    DynamicParameters SpParameters = new DynamicParameters();
                    SpParameters.Add("@NettingRuleSetID", entityDto.NettingRuleSetID);
                    SpParameters.Add("@NettingRuleSetName", entityDto.NettingRuleSetName);
                    SpParameters.Add("@IsNettingAllowed", entityDto.IsNettingAllowed);
                    SpParameters.Add("@NettingType", entityDto.NettingType);
                    SpParameters.Add("@ExchangeID", entityDto.ExchangeID);
                    //SpParameters.Add("@UserName", userName);
                    SpParameters.Add("@SLTradeNettingRuleDetail", ListtoDataTableConverter.ToDataTable(entityDto.SLTradeNettingRuleSetDetails).AsTableValuedParameter("Type_SLTradeNettingRuleSetDetail"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    string result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


                    #region log for netting [update]
                    int NettingRuleID = Convert.ToInt32(result.ToString().Split('-').Last()); //update Netting
                    SLTradeNettingRuleSetDTO prevTradeSettlementRuleDTO = GetById(NettingRuleID, userName);
                    List<UpdateLogDetailDto> LogDetailList = new List<UpdateLogDetailDto>();
                    
                    if (result.ToLower().Contains("updated successfully"))
                    {
                        List<string> AllUpdatedproperties = ObjectComparison.GetChangedProperties(entityDto, prevTradeSettlementRuleDTO);
                        List<string> TempUpdatedProperties = ObjectComparison.GetChangedProperties(entityDto, prevTradeSettlementRuleDTO);

                        AllUpdatedproperties.Remove("SLTradeNettingRuleSetDetails");


                        if (AllUpdatedproperties.Where(c => c == "ExchangeID").Count()>0)
                        {
                            var CMExchange = _globalSettingService.GetTypeCodes("CMExchange", 0);
                            if (CMExchange != null)
                            {
                                entityDto.ExchangeName = CMExchange.Where(c => c.typeCode == entityDto.ExchangeID).FirstOrDefault().typeValue;
                                entityDto.ExchangeShortName = CMExchange.Where(c => c.typeCode == entityDto.ExchangeID).FirstOrDefault().typeShortName;
                                AllUpdatedproperties.Add("ExchangeName");
                                AllUpdatedproperties.Add("ExchangeShortName");
                                //AllUpdatedproperties.Remove("MarketID");
                                //AllUpdatedproperties.Remove("MarketShortName");
                            }
                        }


                        //FOR CMInstrument
                        foreach (string PropertyName in AllUpdatedproperties)
                        {

                            var item = (new UpdateLogDetailDto
                            {
                                TableName = "SLTradeNettingRuleSet",
                                ColumnName = PropertyName,
                                PrevContent = prevTradeSettlementRuleDTO.GetType().GetProperty(PropertyName).GetValue(prevTradeSettlementRuleDTO, null) == null ? "" : prevTradeSettlementRuleDTO.GetType().GetProperty(PropertyName).GetValue(prevTradeSettlementRuleDTO, null).ToString(),
                                UpdatedContent = entityDto.GetType().GetProperty(PropertyName).GetValue(entityDto, null) == null ? "" : entityDto.GetType().GetProperty(PropertyName).GetValue(entityDto, null).ToString(),
                                PKID = NettingRuleID
                            });

                            LogDetailList.Add(item);

                        }

                            //for detail
                            List<SLTradeNettingRuleSetDetailDTO> AddedNettingDetailList = new List<SLTradeNettingRuleSetDetailDTO>();
                            List<SLTradeNettingRuleSetDetailDTO> UpdatedNettingDetailList = new List<SLTradeNettingRuleSetDetailDTO>();
                            List<SLTradeNettingRuleSetDetailDTO> DeletedNettingDetailList = new List<SLTradeNettingRuleSetDetailDTO>();

                            AddedNettingDetailList = entityDto.SLTradeNettingRuleSetDetails.Where(e => e.NettingRuleSetDetailID == 0).ToList();
                            UpdatedNettingDetailList = entityDto.SLTradeNettingRuleSetDetails.Where(e => e.NettingRuleSetDetailID > 0).ToList();

                            foreach (SLTradeNettingRuleSetDetailDTO ex in prevTradeSettlementRuleDTO.SLTradeNettingRuleSetDetails)
                            {
                                if (entityDto.SLTradeNettingRuleSetDetails.Where(e => e.NettingRuleSetDetailID == ex.NettingRuleSetDetailID).ToList().Count == 0) DeletedNettingDetailList.Add(ex);
                            }

                            foreach (SLTradeNettingRuleSetDetailDTO addedNettingDetail in AddedNettingDetailList)
                            {
                                var PrevExhange = new SLTradeNettingRuleSetDetailDTO();

                                AllUpdatedproperties = ObjectComparison.GetChangedProperties(addedNettingDetail, PrevExhange);

                                //AllUpdatedproperties.Remove("ExcInstrID");
                                //AllUpdatedproperties.Remove("TradingPlatformID");
                                //AllUpdatedproperties.Remove("ExchangeID");
                                //AllUpdatedproperties.Remove("MarketID");

                                if (AllUpdatedproperties.Where(c => c == "CategoryID").Count() > 0)
                                {
                                    var CMCategory = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                                    if (CMCategory != null)
                                    {
                                        addedNettingDetail.Category = CMCategory.Where(c => c.typeCode == addedNettingDetail.CategoryID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("Category");
                                    }
                                }

                                if (AllUpdatedproperties.Where(c => c == "MarketID").Count() > 0)
                                {
                                    var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                                    if (CMMarket != null)
                                    {
                                        addedNettingDetail.MarketName = CMMarket.Where(c => c.typeCode == addedNettingDetail.MarketID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("MarketName");
                                        AllUpdatedproperties.Remove("MarketShortName");
                                    }
                                }


                                foreach (string PerpertyName in AllUpdatedproperties)
                                {
                                    var itemLogDetail = new UpdateLogDetailDto();

                                    itemLogDetail.TableName = "SLTradeNettingRuleSetDetail";
                                    itemLogDetail.ColumnName = PerpertyName;
                                    itemLogDetail.PrevContent = PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null) == null ? "" : PrevExhange.GetType().GetProperty(PerpertyName).GetValue(PrevExhange, null).ToString();
                                    itemLogDetail.UpdatedContent = addedNettingDetail.GetType().GetProperty(PerpertyName).GetValue(addedNettingDetail, null) == null ? "" : addedNettingDetail.GetType().GetProperty(PerpertyName).GetValue(addedNettingDetail, null).ToString();
                                    itemLogDetail.PKID = addedNettingDetail.NettingRuleSetDetailID;
                                    LogDetailList.Add(itemLogDetail);
                                }
                            }

                            foreach (SLTradeNettingRuleSetDetailDTO ItemNettingDetail in UpdatedNettingDetailList)
                            {
                                SLTradeNettingRuleSetDetailDTO PrevNettingDetail = prevTradeSettlementRuleDTO.SLTradeNettingRuleSetDetails.Where(e => e.NettingRuleSetDetailID == ItemNettingDetail.NettingRuleSetDetailID).FirstOrDefault();

                                if (PrevNettingDetail == null) PrevNettingDetail = new SLTradeNettingRuleSetDetailDTO();

                                AllUpdatedproperties = ObjectComparison.GetChangedProperties(ItemNettingDetail, PrevNettingDetail);

                                //AllUpdatedproperties.Remove("ExcInstrID");
                                //AllUpdatedproperties.Remove("TradingPlatformID");
                                //AllUpdatedproperties.Remove("ExchangeID");


                                if (AllUpdatedproperties.Where(c => c == "CategoryID").Count() > 0)
                                {
                                    var CMCategory = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                                    if (CMCategory != null)
                                    {
                                        ItemNettingDetail.Category = CMCategory.Where(c => c.typeCode == ItemNettingDetail.CategoryID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("Category");
                                    }
                                }

                                if (AllUpdatedproperties.Where(c => c == "MarketID").Count() > 0)
                                {
                                    var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                                    if (CMMarket != null)
                                    {
                                        ItemNettingDetail.MarketName = CMMarket.Where(c => c.typeCode == ItemNettingDetail.MarketID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("MarketName");
                                        AllUpdatedproperties.Remove("MarketShortName");
                                    }
                                }

                                foreach (string PerpertyName in AllUpdatedproperties)
                                {
                                    var itemLogDetail = new UpdateLogDetailDto();

                                    itemLogDetail.TableName = "SLTradeNettingRuleSetDetail";
                                    itemLogDetail.ColumnName = PerpertyName;
                                    itemLogDetail.PrevContent = PrevNettingDetail.GetType().GetProperty(PerpertyName).GetValue(PrevNettingDetail, null) == null ? "" : PrevNettingDetail.GetType().GetProperty(PerpertyName).GetValue(PrevNettingDetail, null).ToString();
                                    itemLogDetail.UpdatedContent = ItemNettingDetail.GetType().GetProperty(PerpertyName).GetValue(ItemNettingDetail, null) == null ? "" : ItemNettingDetail.GetType().GetProperty(PerpertyName).GetValue(ItemNettingDetail, null).ToString();
                                    itemLogDetail.PKID = ItemNettingDetail.NettingRuleSetDetailID;
                                    LogDetailList.Add(itemLogDetail);
                                }


                            }

                            foreach (SLTradeNettingRuleSetDetailDTO ItemDeletedNetting in DeletedNettingDetailList)
                            {
                                SLTradeNettingRuleSetDetailDTO PrevNettingDetail = prevTradeSettlementRuleDTO.SLTradeNettingRuleSetDetails.Where(e => e.NettingRuleSetDetailID == ItemDeletedNetting.NettingRuleSetDetailID).FirstOrDefault();

                                if (PrevNettingDetail == null) PrevNettingDetail = new SLTradeNettingRuleSetDetailDTO();

                                AllUpdatedproperties = ObjectComparison.GetProperties(ItemDeletedNetting);

                                //AllUpdatedproperties.Remove("ExcInstrID");
                                //AllUpdatedproperties.Remove("TradingPlatformID");
                                //AllUpdatedproperties.Remove("ExchangeID");
                                //AllUpdatedproperties.Remove("MarketID");
                                //AllUpdatedproperties.Remove("CMMFInstrumentDetailID");


                                if (AllUpdatedproperties.Where(c => c == "CategoryID").Count() > 0)
                                {
                                    var CMCategory = _globalSettingService.GetTypeCodes("CMCodesCategory", 0);
                                    if (CMCategory != null)
                                    {
                                        ItemDeletedNetting.Category = CMCategory.Where(c => c.typeCode == ItemDeletedNetting.CategoryID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("Category");
                                    }
                                }

                                if (AllUpdatedproperties.Where(c => c == "MarketID").Count() > 0)
                                {
                                    var CMMarket = _globalSettingService.GetTypeCodes("CMMarket", 0);
                                    if (CMMarket != null)
                                    {
                                        ItemDeletedNetting.MarketName = CMMarket.Where(c => c.typeCode == ItemDeletedNetting.MarketID).FirstOrDefault().typeValue;
                                        AllUpdatedproperties.Add("MarketName");
                                        AllUpdatedproperties.Remove("MarketShortName");
                                    }
                                }

                                foreach (string PerpertyName in AllUpdatedproperties)
                                {
                                    var itemNettingDetail = new UpdateLogDetailDto();

                                    itemNettingDetail.TableName = "SLTradeNettingRuleSetDetail";
                                    itemNettingDetail.ColumnName = PerpertyName;
                                    itemNettingDetail.PrevContent = PrevNettingDetail.GetType().GetProperty(PerpertyName).GetValue(PrevNettingDetail, null) == null ? "" : PrevNettingDetail.GetType().GetProperty(PerpertyName).GetValue(PrevNettingDetail, null).ToString();
                                    itemNettingDetail.UpdatedContent = "";
                                    itemNettingDetail.PKID = ItemDeletedNetting.NettingRuleSetDetailID;
                                    LogDetailList.Add(itemNettingDetail);
                                }
                            }

                            _logOperation.SaveUpdateLog(LogDetailList, userName, Convert.ToInt32(entityDto.NettingRuleSetID),8);
             

                    }

                    #endregion log for netting [update]


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

        public Task<List<SLTradeNettingRuleSetDTO>> GetAll(int PageNo, int Perpage, string SearchKeyword)
        {
            var values = new { CompanyID = 1, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword };
            return _dbCommonOperation.ReadSingleTable<SLTradeNettingRuleSetDTO>("[CM_ListSLTradeNettingRuleSet]", values);
        }

        public SLTradeNettingRuleSetDTO GetById(int Id, string user)
        {
            SLTradeNettingRuleSetDTO slTradeNettingRuleSetDTO = new SLTradeNettingRuleSetDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@NettingRuleSetID", Id),
                new SqlParameter("@UserName",  user),
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QuerySLTradeNettingRuleSet]", sqlParams);

            slTradeNettingRuleSetDTO = DataSets.Tables[0].Rows.Count > 0 ?  CustomConvert.DataSetToList<SLTradeNettingRuleSetDTO>(DataSets.Tables[0]).First() : new SLTradeNettingRuleSetDTO();
            slTradeNettingRuleSetDTO.SLTradeNettingRuleSetDetails = DataSets.Tables[1].Rows.Count > 0 ?  CustomConvert.DataSetToList<SLTradeNettingRuleSetDetailDTO>(DataSets.Tables[1]) : new List<SLTradeNettingRuleSetDetailDTO>();

            return slTradeNettingRuleSetDTO;
        }
    }
}
