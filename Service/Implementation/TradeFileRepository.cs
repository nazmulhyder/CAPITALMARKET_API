using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.SqlServer.Server;
using Model.DTOs.PriceFileUpload;
using Model.DTOs.TradeDataCorrection;
using Model.DTOs.TradeFileUpload;
using Model.DTOs.TradeRestriction;
using Newtonsoft.Json;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Utility;

namespace Service.Implementation
{
    public class TradeFileRepository : ITradeFileRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TradeFileRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<object> ShortSaleList(string UserName, int CompanyID, int ProductID, DateTime TradeDate)
        {

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@TradeDate", TradeDate);
            sqlParams[3] = new SqlParameter("@ProductID", ProductID);
        
            var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListShortSale]", sqlParams);

        
            return await Task.FromResult(ValidationDataSets.Tables[0]);
        }

        #region SLTradeFile

        public async Task<List<SLTradeFileStatusDto>> TradeFileStatusSL(string UserName, DateTime TradeDate)
        {
            var values = new { UserName = UserName, TradeDate = TradeDate };

            return await _dbCommonOperation.ReadSingleTable<SLTradeFileStatusDto>("[dbo].[CM_ListTradeFileSL]", values);
        }

        public async Task<string> TradeReversalRequestSL(string UserName, TradeFileReversalRequest data)
        {
            string IDs = string.Join(",", data.TradeFiles.Select(f => f.TradeFileID));

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@RequestRemark", data.ApprovalRemark);
            SpParameters.Add("@TradeFileIDs", IDs);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertTradeReversalRequestSL", SpParameters);
        }

        public async Task<string> ApproveTradeFileReversalSL(string UserName, ApproveTradeFileReversalRequest data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@ApproveRemark", data.ApprovalRemark);
            SpParameters.Add("@ExchangeID", data.TradeFileID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveTradeFileReversalSL", SpParameters);
        }


        public async Task<object> SLTradeFileProcessingValidation(IFormFile formData, string UserName, int CompanyID, int BranchID, string strTradeDate, string ExcName)
        {
            try
            {
                if(ExcName == "DSE" && Path.GetExtension(formData.FileName) == ".xml") //DSE normal trade 
                {
                    TradeData trades = new TradeData();

                    using (MemoryStream str = new MemoryStream())
                    {
                        await formData.CopyToAsync(str);
                        str.Position = 0;
                        var xml = XDocument.Load(str);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xml.ToString());
                        string jsonText = JsonConvert.SerializeXmlNode(doc);
                        jsonText = Regex.Replace(jsonText, "[@]", string.Empty);
                        trades = JsonConvert.DeserializeObject<TradeData>(jsonText);

                    }

                    SqlParameter[] sqlParams = new SqlParameter[7];
                    sqlParams[0] = new SqlParameter("@UserName", UserName);
                    sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                    sqlParams[3] = new SqlParameter("@TradeDate", strTradeDate);
                    sqlParams[4] = new SqlParameter("@ExchangeName", ExcName);
                    sqlParams[5] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(trades.Trades.Detail));
                    sqlParams[6] = new SqlParameter("@ReturnMessage", "");
                    sqlParams[6].Direction = ParameterDirection.Output;

                    var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertSLTradeFileValidation]", sqlParams);

                    var Result = new
                    {
                        TradeDateValidation = ValidationDataSets.Tables[6].Rows[0][0].ToString(),
                        newTradeDealerList = ValidationDataSets.Tables[0],
                        newAccountList = ValidationDataSets.Tables[1],
                        InactiveAccountList = ValidationDataSets.Tables[2],
                        newInstrumentList = ValidationDataSets.Tables[3],
                        newScrip = ValidationDataSets.Tables[4],
                        newInActiveNonListedInsList = ValidationDataSets.Tables[5]

                    };
                    return await Task.FromResult(Result);
                }

                else if (ExcName == "DSE" && Path.GetExtension(formData.FileName) == ".csv") // DSE Gsec trade 
                {
                    List<TradeDetail> FileRows = new List<TradeDetail>();

                    using (var fileStream = formData.OpenReadStream())
                    using (var reader = new StreamReader(fileStream))
                    {
                        string row;
                        int counter = 0;
                        while ((row = reader.ReadLine()) != null)
                        {
                            if (counter != 0)
                            {
                                row = row.Trim();
                                string[] SplitCells = row.Split(",");

                                FileRows.Add(new TradeDetail
                                {
                                    TradeNo = SplitCells[0],
                                    RefOrderID = SplitCells[1],
                                    CompulsorySpot = SplitCells[2],
                                    Time = SplitCells[5],
                                    BOID = SplitCells[6],
                                    TraderDealerID = SplitCells[7],
                                    SecurityCode = SplitCells[12],
                                    Price = SplitCells[13],
                                    Board = SplitCells[14],
                                    AssetClass = SplitCells[15],
                                    Quantity = SplitCells[16],
                                    Value = SplitCells[17],
                                    SettlementValue = SplitCells[18],
                                    ClientCode = SplitCells[36],
                                    Yield = SplitCells[41],
                                    AccruedInterest = SplitCells[42]
                                });
                                counter++;

                            }

                        }
                    }

                    SqlParameter[] sqlParams = new SqlParameter[7];
                    sqlParams[0] = new SqlParameter("@UserName", UserName);
                    sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                    sqlParams[3] = new SqlParameter("@TradeDate", strTradeDate);
                    sqlParams[4] = new SqlParameter("@ExchangeName", ExcName);
                    sqlParams[5] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileRows));
                    sqlParams[6] = new SqlParameter("@ReturnMessage", "");
                    sqlParams[6].Direction = ParameterDirection.Output;

                    var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertSLTradeFileValidation]", sqlParams);

                    var Result = new
                    {
                        TradeDateValidation = ValidationDataSets.Tables[6].Rows[0][0].ToString(),
                        newTradeDealerList = ValidationDataSets.Tables[0],
                        newAccountList = ValidationDataSets.Tables[1],
                        InactiveAccountList = ValidationDataSets.Tables[2],
                        newInstrumentList = ValidationDataSets.Tables[3],
                        newScrip = ValidationDataSets.Tables[4],
                        newInActiveNonListedInsList = ValidationDataSets.Tables[5]

                    };
                    return await Task.FromResult(Result);
                }

                else if(ExcName == "CSE")
                {
                    List<TradeDetail> TradeDataList = new List<TradeDetail>();

                    string FileContent = string.Empty;

                    using (var reader = new StreamReader(formData.OpenReadStream()))
                    {
                        FileContent = await reader.ReadToEndAsync();
                    }

                    if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

                    string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    foreach (string Line in FileLines)
                    {
                        string[] Words = Line.Split("|");
                        
                        TradeDataList.Add(new TradeDetail
                        {
                            TraderDealerID = Words[0],
                            ScripCode= Words[1],
                            SecurityCode= Words[2],
                            Side= Words[3],
                            Quantity = Words[4],
                            Price = Words[5],
                            ClientCode = Words[6],
                            HowlaNo= Words[9],
                            Date = DateTime.ParseExact(Words[10], "dd/mm/yyyy", CultureInfo.InvariantCulture).ToString("yyyymmdd"),
                            Time = Words[11],
                            ClientType = Words[14]
                        });
                    }

                    SqlParameter[] sqlParams = new SqlParameter[7];
                    sqlParams[0] = new SqlParameter("@UserName", UserName);
                    sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                    sqlParams[2] = new SqlParameter("@BranchID", BranchID);
                    sqlParams[3] = new SqlParameter("@TradeDate", strTradeDate);
                    sqlParams[4] = new SqlParameter("@ExchangeName", ExcName);
                    sqlParams[5] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(TradeDataList));
                    sqlParams[6] = new SqlParameter("@ReturnMessage", "");
                    sqlParams[6].Direction = ParameterDirection.Output;

                    var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertSLTradeFileValidation]", sqlParams);
                    var Result = new
                    {
                        TradeDateValidation = ValidationDataSets.Tables[6].Rows[0][0].ToString(),
                        newTradeDealerList = ValidationDataSets.Tables[0],
                        newAccountList = ValidationDataSets.Tables[1],
                        InactiveAccountList = ValidationDataSets.Tables[2],
                        newInstrumentList = ValidationDataSets.Tables[3],
                        newScrip = ValidationDataSets.Tables[4],
                        newInActiveNonListedInsList = ValidationDataSets.Tables[5]

                    };

                    return await Task.FromResult(Result);
                }

                return await Task.FromResult("");
            }
            catch(SqlException ex)
            {
                return ex.Message;
            }
        }


        public async Task<object> SLTradeFileComparisonFromFTP(string UserName, int CompanyID, int BranchID)
        {

            #region PREVIOUS DATA

            //var FileList = FtpUtility.FileList("ftp://192.168.114.108:8021/", UserName, "111@@@idlc");

            //if (FileList.Count() > 0 && FileList.Where(f => f.Contains(".xml")).ToList().Count == 1)
            //{
            //    string Content = FtpUtility.FileContent("ftp://192.168.114.108:8021/" + FileList.Where(f => f.Contains(".xml")).FirstOrDefault(), UserName, "111@@@idlc");

            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(Content);
            //    string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            //    json = Regex.Replace(json, "[@]", string.Empty);
            //    priceFileFTPDto priceFileDto = JsonConvert.DeserializeObject<priceFileFTPDto>(json);

            //    List<FTPTicker> DataList = new List<FTPTicker>();

            //    foreach (var item in priceFileDto.EODTickers.Ticker) DataList.Add(item);


            //    SqlParameter[] sqlParams = new SqlParameter[]
            //{
            //    new SqlParameter("@UserName", UserName),
            //    new SqlParameter("@CompanyID", CompanyID),
            //    new SqlParameter("@BranchID", BranchID),
            //    new SqlParameter("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(DataList)),
            //    new SqlParameter("@ReturnMessage", "")
            //};

            //    var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListComparePriceFileValidation]", sqlParams);

            //    var ComparisonDataSet = _dbCommonOperation.FindMultipleDataSetBySP("CM_ListComparePriceFile", sqlParams);


            //    List<Attributes> NewInstrumentList = CustomConvert.DataSetToList<Attributes>(ValidationDataSets.Tables[0]);
            //    List<dynamic> NewCategoryList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[1]);
            //    List<dynamic> CompareResult = CustomConvert.DataSetToList<dynamic>(ComparisonDataSet.Tables[0]);


            //    var Result = new
            //    {
            //        FileDataList = priceFileDto.EODTickers,
            //        NewInstrumentList = NewInstrumentList.Where(c => c.Sector != null).ToList(),
            //        NewCategoryList = NewCategoryList,
            //        CompareResult = CompareResult
            //    };
            //    return await Task.FromResult(Result);
            //}
            #endregion

            return await Task.FromResult("");
        }


        public async Task<string> SLTradeFileUpload(IFormCollection formData, string UserName)
        {

            if (formData.Files.Count == 0) return "No File Uploaded.";

            if(formData["ExchangeName"].ToString() == "DSE" && Path.GetExtension(formData.Files[0].FileName) == ".xml") //dse normal file
            {
                TradeData trades = new TradeData();

                string jsonText = "";
                using (MemoryStream str = new MemoryStream())
                {
                    await formData.Files[0].CopyToAsync(str);
                    str.Position = 0;
                    var xml = XDocument.Load(str);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml.ToString());
                    jsonText = JsonConvert.SerializeXmlNode(doc);
                    jsonText = Regex.Replace(jsonText, "[@]", string.Empty);
                    trades = JsonConvert.DeserializeObject<TradeData>(jsonText);
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
                SpParameters.Add("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
                SpParameters.Add("@ExchangeName", formData["ExchangeName"].ToString());
                SpParameters.Add("@TradeFileName", formData.Files[0].FileName);
                SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
                SpParameters.Add("@FileSizeInKB", formData.Files[0].Length / 1000);
                SpParameters.Add("@TradeDate", formData["TradeDate"].ToString());
                SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(trades.Trades.Detail).AsTableValuedParameter("Type_TradeFileAttribute"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertSLTradeFile]", SpParameters);
            }
            
            else if (formData["ExchangeName"].ToString() == "DSE" && Path.GetExtension(formData.Files[0].FileName) == ".csv") // DSE gsec trade
            {

                List<TradeDetail> FileRows = new List<TradeDetail>();

                using (var fileStream = formData.Files[0].OpenReadStream())
                using (var reader = new StreamReader(fileStream))
                {
                    string row;
                    int counter = 0;
                    while ((row = reader.ReadLine()) != null)
                    {
                        if (counter != 0)
                        {
                            row = row.Trim();
                            string[] SplitCells = row.Split(",");

                            if(SplitCells[6].Length > 5) // BUY HOWLA EXIST

                            FileRows.Add(new TradeDetail
                            {
                                Action = "EXEC",
                                Status ="PF",
                                ISIN ="",
                                AssetClass = "GS",
                                OrderID = SplitCells[1], // BUY ORDER NO
                                RefOrderID = SplitCells[0],
                                Side = "B",
                                BOID = SplitCells[6], // BUY BOID
                                SecurityCode = SplitCells[12],
                                Board = SplitCells[14],
                                Quantity = SplitCells[16],
                                Price = SplitCells[13],
                                Value = SplitCells[17],
                                Category = "A",
                                CompulsorySpot = SplitCells[2] == "FALSE" ? "N" : "Y",
                                ClientCode = SplitCells[36], // Buy Investor ID
                                TraderDealerID = SplitCells[8], // Buy Trade Delar
                                SettlementValue = SplitCells[18],
                                Yield = SplitCells[41],
                                AccruedInterest = SplitCells[42],

                            });

                            if (SplitCells[7].Length > 5) // SELL HOWLA EXIST

                                FileRows.Add(new TradeDetail
                                {
                                    Action = "EXEC",
                                    Status = "PF",
                                    ISIN = "",
                                    AssetClass = "GS",
                                    OrderID = SplitCells[3], // BUY ORDER NO
                                    RefOrderID = SplitCells[0],
                                    Side = "S",
                                    BOID = SplitCells[7], // SELL BOID
                                    SecurityCode = SplitCells[12],
                                    Board = SplitCells[14],
                                    Quantity = SplitCells[16],
                                    Price = SplitCells[13],
                                    Value = SplitCells[17],
                                    Category = "A",
                                    CompulsorySpot = SplitCells[2] == "FALSE" ? "N" : "Y",
                                    ClientCode = SplitCells[37], // Sell Investor ID
                                    TraderDealerID = SplitCells[9], // Sell Trade Delar
                                    SettlementValue = SplitCells[18],
                                    Yield = SplitCells[41],
                                    AccruedInterest = SplitCells[42],
                                });
                        }

                        counter++;

                    }
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
                SpParameters.Add("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
                SpParameters.Add("@ExchangeName", formData["ExchangeName"].ToString());
                SpParameters.Add("@TradeFileName", formData.Files[0].FileName);
                SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
                SpParameters.Add("@FileSizeInKB", formData.Files[0].Length / 1000);
                SpParameters.Add("@TradeDate", formData["TradeDate"].ToString());
                SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileRows).AsTableValuedParameter("Type_TradeFileAttribute"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertSLTradeFile]", SpParameters);
            }
            else
            {
                List<TradeDetail> TradeDataList = new List<TradeDetail>();

                string FileContent = string.Empty;

                using (var reader = new StreamReader(formData.Files[0].OpenReadStream()))
                {
                    FileContent = await reader.ReadToEndAsync();
                }

                if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

                string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string Line in FileLines)
                {
                    string[] Words = Line.Split("|");

                    TradeDataList.Add(new TradeDetail
                    {
                        TraderDealerID = Words[0],
                        ScripCode = Words[1],
                        SecurityCode = Words[2],
                        Side = Words[3],
                        Quantity = Words[4],
                        Price = Words[5],
                        ClientCode = Words[6],
                        HowlaNo = Words[9],
                        Date = DateTime.ParseExact(Words[10], "dd/mm/yyyy", CultureInfo.InvariantCulture).ToString("yyyymmdd"),
                        Time = Words[11],
                        ClientType = Words[14]
                    });
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
                SpParameters.Add("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
                SpParameters.Add("@ExchangeName", formData["ExchangeName"].ToString());
                SpParameters.Add("@TradeFileName", formData.Files[0].FileName);
                SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
                SpParameters.Add("@FileSizeInKB", formData.Files[0].Length / 1000);
                SpParameters.Add("@TradeDate", formData["TradeDate"].ToString());
                SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(TradeDataList).AsTableValuedParameter("Type_TradeFileAttribute"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertSLTradeFile]", SpParameters);
            }
            
        }

      

        public async Task<List<SLTradeSummaryDto>> SLTradeSummary(int CompanyID,DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, TradeDate = TradeDate };
            return await _dbCommonOperation.ReadSingleTable<SLTradeSummaryDto>("[dbo].[CM_ListSLTradeSummary]", values);
        }

        public async Task<string> ApproveSLTradeSummaryProductWise(string UserName, int CompanyID,int BrokerID, DateTime TradeDate, int ProductID,string ApprovalRemark)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BrokerID", BrokerID);
            SpParameters.Add("@ProductID", ProductID);
            SpParameters.Add("@TradeDate", TradeDate);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            await _dbCommonOperation.ExecuteSP("[dbo].[CM_ApproveSLTradeSummary]", SpParameters);
            string ReturnMessage = SpParameters.Get<string>("ReturnMessage");
            return await Task.FromResult(ReturnMessage);
        }
        #endregion SLTradeFile

        #region ILTradeFile


        public async Task<List<SLTradeFileStatusDto>> TradeFileStatusIL(string UserName, DateTime TradeDate)
        {
            var values = new { UserName = UserName, TradeDate = TradeDate };

            return await _dbCommonOperation.ReadSingleTable<SLTradeFileStatusDto>("[dbo].[CM_ListTradeFileIL]", values);
        }
        public async Task<string> TradeReversalRequestIL(string UserName, TradeFileReversalRequest data)
        {
            string IDs = string.Join(",", data.TradeFiles.Select(f => f.TradeFileID));

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@RequestRemark", data.ApprovalRemark);
            SpParameters.Add("@TradeFileIDs", IDs);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertTradeReversalRequestIL", SpParameters);
        }

        public async Task<string> ApproveTradeFileReversalIL(string UserName, ApproveTradeFileReversalRequest data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@ApproveRemark", data.ApprovalRemark);
            SpParameters.Add("@TradeFileID", data.TradeFileID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveTradeFileReversalIL", SpParameters);
        }

        public async Task<object> ILPanelBrokerList(string UserName, int CompanyID)
        {
            SqlParameter[] SpParameters = new SqlParameter[2];
            SpParameters[0] = new SqlParameter("@UserName", UserName);
            SpParameters[1] = new SqlParameter("@CompanyID", CompanyID);

            var DataSets =  _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListPanelBroker]", SpParameters);

            var Result = new
            {
                BrokerList = DataSets.Tables[0]
            };

            return await Task.FromResult(Result);
        }

        public async Task<object> ILTradeFileUploadValidation(IFormCollection formData, string UserName)
        {
            string FileContent = string.Empty; 
            List< ILTradeFileDetailDto > FileDetails = new List< ILTradeFileDetailDto >();

            using (var reader = new StreamReader(formData.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            if (formData["ExchangeName"].ToString() == "DSE")
            {
                foreach (string Line in FileLines)
                {
                     if (Line.Length == 0) continue;

                    string[] Words = Line.Split("~");
                    
                    if (Words.Length < 3) throw new Exception("Wrong File Selected.");


                    DateTime dd = DateTime.Parse(Words[7].Split("-")[2] + "-" + Words[7].Split("-")[1] + "-" + Words[7].Split("-")[0]);

						//DateTime.ParseExact(Words[7], "dd-mm-yyyy", CultureInfo.InvariantCulture);
                 

                    FileDetails.Add(new ILTradeFileDetailDto
                    {
                        OrderRefNo = Words[0],
                        SecurityCode = Words[1],
                        ISIN = Words[2],
                        TraderCode = Words[3],
                        TradeType = Words[4],
                        Quantity = Convert.ToDecimal(Words[5]),
                        Rate = Convert.ToDecimal(Words[6]),
                        TradeDateTime = dd,
                        //DateTime.ParseExact(Words[7], "dd-mm-yyyy", null),
                        Market = Words[9],
                        Status = Words[10],
                        HowlaType = Words[11],
                        ForeginFlag = Words[12],
                        AccountNumber = Words[13],
                        BOID = Words[14],
                        HowlaRefNo = Words[15],
                        CompolsarySpot = Words[16],
                        InstrumentCategory = Words[17]
                    });
                }

            }
            else
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("|");

                    if (Words.Length < 3) throw new Exception("Wrong File Selected.");


                    DateTime dd = new DateTime();

                    if(Words[12].ToString().Contains("-"))
                    {
						dd = DateTime.Parse(Words[12].Split("-")[2] + "-" + Words[12].Split("-")[1] + "-" + Words[12].Split("-")[0]);

					}
                    else
                    {
   						dd = DateTime.Parse(Words[12].Split("/")[2] + "-" + Words[12].Split("/")[1] + "-" + Words[12].Split("/")[0]);
					}


					FileDetails.Add(new ILTradeFileDetailDto
                    {
                        TraderCode = Words[0],
                        OrderRefNo = Words[1],
                        SecurityCode = Words[2],
                        TradeType = Words[3],
                        
                        Quantity = Convert.ToDecimal(Words[4]),
                        Rate = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        HowlaRefNo = Words[9],
                        TradeDateTime = dd

                    });
                }

            }

            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
            sqlParams[2] = new SqlParameter("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
            sqlParams[3] = new SqlParameter("@BrokerID", JsonConvert.DeserializeObject(formData["Broker"]));
            sqlParams[4] = new SqlParameter("@TradeDate", formData["TradeDate"].ToString());
            sqlParams[5] = new SqlParameter("@ExchangeName", formData["ExchangeName"].ToString());
            sqlParams[6] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileDetails));

            var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertILTradeFileValidation]", sqlParams);

            var Result = new
            {
                ValidationMessage = ValidationDataSets.Tables[0]?.Rows[0]?[0].ToString(),
                NewAccounts = ValidationDataSets.Tables[1],
                InActiveAccounts = ValidationDataSets.Tables[2],
                NewInstruments = ValidationDataSets.Tables[3],
                InActiveInstruments = ValidationDataSets.Tables[4]

            };

            return await Task.FromResult(Result);

        }

        public async Task<string> ILTradeFileUpload(IFormCollection formData, string UserName)
        {
            string FileContent = string.Empty;
            List<ILTradeFileDetailDto> FileDetails = new List<ILTradeFileDetailDto>();

            using (var reader = new StreamReader(formData.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (formData["ExchangeName"].ToString() == "DSE")
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("~");

                    if (Words.Length < 3) throw new Exception("Wrong File Selected.");
                  
                    DateTime dd = DateTime.ParseExact(Words[7], "dd-mm-yyyy", CultureInfo.InvariantCulture);
                    FileDetails.Add(new ILTradeFileDetailDto
                    {
                        OrderRefNo = Words[0],
                        SecurityCode = Words[1],
                        ISIN = Words[2],
                        TraderCode = Words[3],
                        TradeType = Words[4],
                        Quantity = Convert.ToDecimal(Words[5]),
                        Rate = Convert.ToDecimal(Words[6]),
                        TradeDateTime = dd,
                        Market = Words[9],
                        Status = Words[10],
                        HowlaType = Words[11],
                        ForeginFlag = Words[12],
                        AccountNumber = Words[13],
                        BOID = Words[14],
                        HowlaRefNo = Words[15],
                        CompolsarySpot = Words[16],
                        InstrumentCategory = Words[17]
                    });
                }

            }
            else
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("|");

                    if (Words.Length < 3) throw new Exception("Wrong File Selected.");

                    DateTime dd = new DateTime();

					if (Words[12].ToString().Contains("-"))
					{
						dd = DateTime.Parse(Words[12].Split("-")[2] + "-" + Words[12].Split("-")[1] + "-" + Words[12].Split("-")[0]);

					}
					else
					{
						dd = DateTime.Parse(Words[12].Split("/")[2] + "-" + Words[12].Split("/")[1] + "-" + Words[12].Split("/")[0]);
					}
					
                    FileDetails.Add(new ILTradeFileDetailDto
                    {
                        TraderCode = Words[0],
                        OrderRefNo = Words[1],
                        SecurityCode = Words[2],
                        TradeType = Words[3],

                        Quantity = Convert.ToDecimal(Words[4]),
                        Rate = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        HowlaRefNo = Words[9],
                        TradeDateTime = dd

                    });
                }

            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
            SpParameters.Add("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
            SpParameters.Add("@BrokerID", JsonConvert.DeserializeObject(formData["Broker"]));
            SpParameters.Add("@ExchangeName", formData["ExchangeName"].ToString());
            SpParameters.Add("@TradeFileName", formData.Files[0].FileName);
            SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
            SpParameters.Add("@FileSizeInKB", formData.Files[0].Length);
            SpParameters.Add("@TradeDate", formData["TradeDate"].ToString());
            SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileDetails).AsTableValuedParameter("Type_ILTradeFileDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertILTradeFile]", SpParameters);
        }


        public async Task<List<ILTradeSummaryDto>> ILTradeSummary(int CompanyID, DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, TradeDate = TradeDate };
            return await _dbCommonOperation.ReadSingleTable<ILTradeSummaryDto>("[dbo].[CM_ListILTradeSummary]", values);
        }

        public async Task<object> ApproveILTradeSummaryProductWise(string UserName, int CompanyID, int BrokerID, DateTime TradeDate, int ProductID, string ApprovalRemark)
        {

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BrokerID", BrokerID);
            sqlParams[3] = new SqlParameter("@ProductID", ProductID);
            sqlParams[4] = new SqlParameter("@TradeDate", TradeDate);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListILTradeSummaryApproveValidation]", sqlParams);

            if (DataSets.Tables[0].Rows.Count > 0 || DataSets.Tables[1].Rows.Count > 0)
            {
                var Result = new
                {
                    AllocationPendingList = DataSets.Tables[0],
                    ShotSaleList = DataSets.Tables[1]
                };

                return await Task.FromResult(Result);
            }
           


            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BrokerID", BrokerID);
            SpParameters.Add("@ProductID", ProductID);
            SpParameters.Add("@TradeDate", TradeDate);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            await _dbCommonOperation.ExecuteSP("[dbo].[CM_ApproveILTradeSummary]", SpParameters);
            string ReturnMessage = SpParameters.Get<string>("ReturnMessage");
            return await Task.FromResult(ReturnMessage);
        }

        public async Task<object> GetNonMarginTradeDataIL(string UserName, int CompanyID, DateTime TradeDate)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, TradeDate = TradeDate };

            List<NonMarginTradeDataDto> data = await _dbCommonOperation.ReadSingleTable<NonMarginTradeDataDto>("[dbo].[CM_ListNonMarginTradeDataIL]", values);

            var Products = data.GroupBy(p => p.ProductID)
            .Select(grp => grp.FirstOrDefault()).ToList();

            var DataList = new
            {
                ProductList = Products,
                TradeDataList = data

            };

            return DataList;
        }

        public async Task<string> InsertNonMarginTradeDataIL(string UserName, int CompanyID, int BranchID, List<NonMarginTradeDataDto> data)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@TradeData", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_NonMerginTradeData"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertNonMarginTradeDataIL]", SpParameters);
        }

        public async Task<object> GetListOverBuyIL(string UserName, int CompanyID,int BranchID, DateTime TradeDate)
        {
            var values = new { Transaction_Date = TradeDate, CompanyID = CompanyID, BranchID = BranchID, UserName = UserName };

            var data = await _dbCommonOperation.ReadSingleTable<ListOverBuyILDto>("[dbo].[CM_ListOverBuyIL]", values);

            var Products = data.GroupBy(p => p.ProductID)
            .Select(grp => grp.FirstOrDefault()).ToList();

            var DataList = new
            {
                ProductList = Products,
                TradeDataList = data

            };

            return DataList;
        }

        public async Task<string> InsertListOverBuyIL(string UserName, int CompanyID, int BranchID, List<ListOverBuyILDto> data)
        {

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@TradeData", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_OverBuyTradeData"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertOverBuyTradeDataIL]", SpParameters);
        }

        #endregion ILTradeFile

        #region AML

        public async Task<List<SLTradeFileStatusDto>> TradeFileStatusAML(string UserName, DateTime TradeDate)
        {
            var values = new { UserName = UserName, TradeDate = TradeDate };

            return await _dbCommonOperation.ReadSingleTable<SLTradeFileStatusDto>("[dbo].[CM_ListTradeFileAML]", values);
        }
        
        public async Task<string> TradeReversalRequestAML(string UserName, TradeFileReversalRequest data)
        {
            string IDs = string.Join(",", data.TradeFiles.Select(f => f.TradeFileID));

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@RequestRemark", data.ApprovalRemark);
            SpParameters.Add("@TradeFileIDs", IDs);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertTradeReversalRequestAML", SpParameters);
        }

        public async Task<string> ApproveTradeFileReversalAML(string UserName, ApproveTradeFileReversalRequest data)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", data.CompanyID);
            SpParameters.Add("@BranchID", data.BranchID);
            SpParameters.Add("@ApproveRemark", data.ApprovalRemark);
            SpParameters.Add("@TradeFileID", data.TradeFileID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveTradeFileReversalAML", SpParameters);
        }

        public async Task<object> AMLPanelBrokerList(string UserName, int CompanyID)
        {
            SqlParameter[] SpParameters = new SqlParameter[2];
            SpParameters[0] = new SqlParameter("@UserName", UserName);
            SpParameters[1] = new SqlParameter("@CompanyID", CompanyID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLBroker]", SpParameters);

            var Result = new
            {
                BrokerList = DataSets.Tables[0]
            };

            return await Task.FromResult(Result);
        }
        
        public async Task<object> AMLTradeFileUploadValidation(IFormCollection formData, string UserName)
        {
            string FileContent = string.Empty;
            List<ILTradeFileDetailDto> FileDetails = new List<ILTradeFileDetailDto>();

            using (var reader = new StreamReader(formData.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);


            if (formData["ExchangeName"].ToString() == "DSE")
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("~");

					//DateTime dd = DateTime.Parse(Words[7].Split("-")[2] + "-" + Words[7].Split("-")[1] + "-" + Words[7].Split("-")[0]);

					DateTime dd = new DateTime();

					if (Words[7].ToString().Contains("-"))
					{
						dd = DateTime.Parse(Words[7].Split("-")[2] + "-" + Words[7].Split("-")[1] + "-" + Words[7].Split("-")[0]);

					}
					else
					{
						dd = DateTime.Parse(Words[7].Split("/")[2] + "-" + Words[7].Split("/")[1] + "-" + Words[7].Split("/")[0]);
					}

					//DateTime dd = DateTime.ParseExact(Words[7], "dd-mm-yyyy", CultureInfo.InvariantCulture);
					// DateTime dd = Convert.ToDateTime(DateTime.Parse(Words[7]));

					FileDetails.Add(new ILTradeFileDetailDto
                    {
                        OrderRefNo = Words[0],
                        SecurityCode = Words[1],
                        ISIN = Words[2],
                        TraderCode = Words[3],
                        TradeType = Words[4],
                        Quantity = Convert.ToDecimal(Words[5]),
                        Rate = Convert.ToDecimal(Words[6]),
                        TradeDateTime = dd,
                        Market = Words[9],
                        Status = Words[10],
                        HowlaType = Words[11],
                        ForeginFlag = Words[12],
                        AccountNumber = Words[13],
                        BOID = Words[14],
                        HowlaRefNo = Words[15],
                        CompolsarySpot = Words[16],
                        InstrumentCategory = Words[17]
                    });
                }

            }
            else
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("|");
					DateTime dd = new DateTime();

					if (Words[10].ToString().Contains("-"))
					{
						dd = DateTime.Parse(Words[10].Split("-")[2] + "-" + Words[10].Split("-")[1] + "-" + Words[10].Split("-")[0]);

					}
					else
					{
						dd = DateTime.Parse(Words[10].Split("/")[2] + "-" + Words[10].Split("/")[1] + "-" + Words[10].Split("/")[0]);
					}
					//DateTime dd = Convert.ToDateTime(DateTime.Parse(Words[7]));
					FileDetails.Add(new ILTradeFileDetailDto
                    {
                        TraderCode = Words[0],
                        OrderRefNo = Words[1],
                        SecurityCode = Words[2],
                        TradeType = Words[3],

                        Quantity = Convert.ToDecimal(Words[4]),
                        Rate = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        HowlaRefNo = Words[9],
                        TradeDateTime = dd

                    });
                }

            }

            SqlParameter[] sqlParams = new SqlParameter[7];
            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
            sqlParams[2] = new SqlParameter("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
            sqlParams[3] = new SqlParameter("@BrokerID", JsonConvert.DeserializeObject(formData["Broker"]));
            sqlParams[4] = new SqlParameter("@TradeDate", formData["TradeDate"].ToString());
            sqlParams[5] = new SqlParameter("@ExchangeName", formData["ExchangeName"].ToString());
            sqlParams[6] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileDetails));

            var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_InsertAMLTradeFileValidation]", sqlParams);

            var Result = new
            {
                ValidationMessage = ValidationDataSets.Tables[0].Rows[0][0].ToString(),
                NewAccounts = ValidationDataSets.Tables[1],
                InActiveAccounts = ValidationDataSets.Tables[2],
                NewInstruments = ValidationDataSets.Tables[3],
                InActiveInstruments = ValidationDataSets.Tables[4]

            };

            return await Task.FromResult(Result);

        }

        public async Task<string> AMLTradeFileUpload(IFormCollection formData, string UserName)
        {
            string FileContent = string.Empty;
            List<ILTradeFileDetailDto> FileDetails = new List<ILTradeFileDetailDto>();

            using (var reader = new StreamReader(formData.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (formData["ExchangeName"].ToString() == "DSE")
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("~");

					DateTime dd = new DateTime();

					if (Words[7].ToString().Contains("-"))
					{
						dd = DateTime.Parse(Words[7].Split("-")[2] + "-" + Words[7].Split("-")[1] + "-" + Words[7].Split("-")[0]);

					}
					else
					{
						dd = DateTime.Parse(Words[7].Split("/")[2] + "-" + Words[7].Split("/")[1] + "-" + Words[7].Split("/")[0]);
					}
					//DateTime dd = Convert.ToDateTime(DateTime.Parse(Words[7]));

					FileDetails.Add(new ILTradeFileDetailDto
                    {
                        OrderRefNo = Words[0],
                        SecurityCode = Words[1],
                        ISIN = Words[2],
                        TraderCode = Words[3],
                        TradeType = Words[4],
                        Quantity = Convert.ToDecimal(Words[5]),
                        Rate = Convert.ToDecimal(Words[6]),
                        TradeDateTime = dd,
                        Market = Words[9],
                        Status = Words[10],
                        HowlaType = Words[11],
                        ForeginFlag = Words[12],
                        AccountNumber = Words[13],
                        BOID = Words[14],
                        HowlaRefNo = Words[15],
                        CompolsarySpot = Words[16],
                        InstrumentCategory = Words[17]
                    });
                }

            }
            else
            {
                foreach (string Line in FileLines)
                {
                    if (Line.Length == 0) continue;

                    string[] Words = Line.Split("|");
					DateTime dd = new DateTime();

					if (Words[10].ToString().Contains("-"))
					{
						dd = DateTime.Parse(Words[10].Split("-")[2] + "-" + Words[10].Split("-")[1] + "-" + Words[10].Split("-")[0]);

					}
					else
					{
						dd = DateTime.Parse(Words[10].Split("/")[2] + "-" + Words[10].Split("/")[1] + "-" + Words[10].Split("/")[0]);
					}
					// DateTime dd = Convert.ToDateTime(DateTime.Parse(Words[7]));
					FileDetails.Add(new ILTradeFileDetailDto
                    {
                        TraderCode = Words[0],
                        OrderRefNo = Words[1],
                        SecurityCode = Words[2],
                        TradeType = Words[3],

                        Quantity = Convert.ToDecimal(Words[4]),
                        Rate = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        HowlaRefNo = Words[9],
                        TradeDateTime = dd

                    });
                }

            }

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", JsonConvert.DeserializeObject(formData["CompanyId"]));
            SpParameters.Add("@BranchID", JsonConvert.DeserializeObject(formData["BranchId"]));
            SpParameters.Add("@BrokerID", JsonConvert.DeserializeObject(formData["Broker"]));
            SpParameters.Add("@ExchangeName", formData["ExchangeName"].ToString());
            SpParameters.Add("@TradeFileName", formData.Files[0].FileName);
            SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
            SpParameters.Add("@FileSizeInKB", formData.Files[0].Length);
            SpParameters.Add("@TradeDate", formData["TradeDate"].ToString());
            SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(FileDetails).AsTableValuedParameter("Type_ILTradeFileDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertAMLTradeFile]", SpParameters);
        }
       

        //public async Task<List<SLTradeSummaryDto>> AMLTradeSummary(int CompanyID, DateTime TradeDate)
        //{
        //    var values = new { CompanyID = CompanyID, TradeDate = TradeDate };

        //    return await _dbCommonOperation.ReadSingleTable<SLTradeSummaryDto>("[dbo].[CM_ListAMLTradeSummary]", values);
        //}

        public async Task<List<AMLProductDto>> AMLTradeSummary(int CompanyID, DateTime TradeDate)
        {
          
            SqlParameter[] sqlParams = new SqlParameter[]
          {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@TradeDate", TradeDate),
          };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListAMLTradeSummary]", sqlParams);

            List<AMLProductDto> AMLProducts = CustomConvert.DataSetToList<AMLProductDto>(DataSets.Tables[0]).ToList();
            List<AMLTradeDetailDto> AMLTradeDetails = CustomConvert.DataSetToList<AMLTradeDetailDto>(DataSets.Tables[1]).ToList();
            List<AMLBrokerTradeDto> AMLBrokerTrades = CustomConvert.DataSetToList<AMLBrokerTradeDto>(DataSets.Tables[2]).ToList();


            foreach(var item in AMLProducts)
            {
                if(item.ProductID == 0)
                {
                    item.AMLTradeDetailList = AMLTradeDetails;
                    foreach (var allitem in item.AMLTradeDetailList) allitem.AMLBrokerTradeList = AMLBrokerTrades.Where(b => b.BrokerID == allitem.BrokerID).ToList();
                }
                
                else
                {
                    item.AMLTradeDetailList = AMLTradeDetails.Where(c => c.ProductID == item.ProductID).ToList();
                    foreach (var amlitem in item.AMLTradeDetailList)
                    {
                        amlitem.AMLBrokerTradeList = AMLBrokerTrades.Where(a => a.ProductID == item.ProductID && a.BrokerID == amlitem.BrokerID).ToList();
                    }
                }
            }


            return AMLProducts;
        }
       
        public async Task<string> ApproveAMLTradeSummaryProductWise(string UserName, int CompanyID, int BrokerID, DateTime TradeDate, int ProductID, string ApprovalRemark)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BrokerID", BrokerID);
            SpParameters.Add("@ProductID", ProductID);
            SpParameters.Add("@TradeDate", TradeDate);
            SpParameters.Add("@ApprovalRemark", ApprovalRemark);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            await _dbCommonOperation.ExecuteSP("[dbo].[CM_ApproveAMLTradeSummary]", SpParameters);
            string ReturnMessage = SpParameters.Get<string>("ReturnMessage");
            return await Task.FromResult(ReturnMessage);
        }

        #endregion AML
    }
}
