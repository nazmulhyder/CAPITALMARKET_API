using Dapper;
using Microsoft.AspNetCore.Http;
using Model.DTOs.PriceFileUpload;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Utility;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Service.Implementation
{
    public class PriceFileRepository : IPriceFileRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public PriceFileRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<string> SaveNewInstrumentCategory(string CategoryName)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@CategoryName", CategoryName);
           
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_InsertInstrumentCategory", SpParameters);
        }
        public async Task<object> PriceFileUpload(IFormCollection formData, string UserName)
        {
            bool IsGsec = Convert.ToBoolean(formData["GSec"]);

            if (formData["priceFileType"] == "ManualUpload" && formData["exchange"] == "DSE" && IsGsec == false)
            {
                string fileString = string.Empty;

                if (formData.Files.Count > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        formData.Files[0].CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        fileString = System.Text.Encoding.UTF8.GetString(fileBytes);
                    }

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(fileString);
                    string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                    json = Regex.Replace(json, "[@]", string.Empty);
                    priceFileFTPDto priceFileDto = JsonConvert.DeserializeObject<priceFileFTPDto>(json);
                    List<FTPTicker> DataList = new List<FTPTicker>();
                    foreach (var item in priceFileDto.EODTickers.Ticker) DataList.Add(item);


                    DynamicParameters SpParameters = new DynamicParameters();

                    SpParameters.Add("@UserName", UserName);
                    SpParameters.Add("@ExchangeName", formData["exchange"].ToString());
                    SpParameters.Add("@IsGsec", formData["GSec"].ToString());
                    SpParameters.Add("@TradingDate", formData["tradingDate"].ToString());
                    SpParameters.Add("@FileName", formData.Files[0].FileName);

                    SpParameters.Add("@FileExtension", Path.GetExtension(formData.Files[0].FileName));
                    SpParameters.Add("@FileSizeInKB", formData.Files[0].Length / 1000);
                    SpParameters.Add("@Status", formData["priceFileType"].ToString());

                    SpParameters.Add("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_PriceFileDataList"));
                    SpParameters.Add("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(new List<GsecPriceFileDto>()).AsTableValuedParameter("Type_PriceFileDataListGsec"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                    return await _dbCommonOperation.InsertUpdateBySP("CM_InsertPriceFile", SpParameters);
                }
                else return await Task.FromResult("No File Uploded.");

            }

            else if (formData["priceFileType"] == "FtpUpload" && formData["exchange"] == "DSE" && IsGsec == false)
            {
                var FileList = FtpUtility.FileList("ftp://192.168.114.108:8021/", "Shourav", "111@@@idlc");

                if (FileList.Count() == 0 && FileList.Where(f => f.FileName.Contains(".xml")).ToList().Count == 0)
                {
                    return await Task.FromResult("No XML file found in FTP directory");
                }
                else
                {
                    var FileDefination = FileList.Where(f => f.FileName.Contains(".xml")).FirstOrDefault();

                    string Content = FtpUtility.FileContent("ftp://192.168.114.108:8021/" + FileDefination.FileName, "Shourav", "111@@@idlc");

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Content);
                    string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                    json = Regex.Replace(json, "[@]", string.Empty);
                    priceFileFTPDto priceFileDto = JsonConvert.DeserializeObject<priceFileFTPDto>(json);

                    List<FTPTicker> DataList = new List<FTPTicker>();

                    foreach (var item in priceFileDto.EODTickers.Ticker) DataList.Add(item);


                    DynamicParameters SpParameters = new DynamicParameters();

                    SpParameters.Add("@UserName", UserName);
                    SpParameters.Add("@ExchangeName", formData["exchange"].ToString());
                    SpParameters.Add("@IsGsec", formData["GSec"].ToString());
                    SpParameters.Add("@TradingDate", formData["tradingDate"].ToString());
                    SpParameters.Add("@FileName", FileDefination.FileName);

                    SpParameters.Add("@FileExtension", Path.GetExtension(FileDefination.FileName));
                    SpParameters.Add("@FileSizeInKB", FileDefination.FileSizeInKB);
                    SpParameters.Add("@Status", formData["priceFileType"].ToString());

                    SpParameters.Add("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_PriceFileDataList"));
                    SpParameters.Add("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(new List<GsecPriceFileDto>()).AsTableValuedParameter("Type_PriceFileDataListGsec"));
                    SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);


                    return await _dbCommonOperation.InsertUpdateBySP("CM_InsertPriceFile", SpParameters);
                }
            }
            
            else if (formData["priceFileType"] == "ManualUpload" && formData["exchange"] == "DSE" && IsGsec == true)
            {

                var GsecPriceFileDataList = JsonConvert.DeserializeObject<List<GsecPriceFileDto>>(formData["priceFile"]);

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@ExchangeName", formData["exchange"].ToString());
                SpParameters.Add("@IsGsec", IsGsec);
                SpParameters.Add("@TradingDate", formData["tradingDate"].ToString());
                SpParameters.Add("@FileName", formData["fileName"].ToString());

                SpParameters.Add("@FileExtension", Path.GetExtension(formData["fileName"].ToString()));
                SpParameters.Add("@FileSizeInKB", Convert.ToInt32(formData["fileSize"]) / 1000);
                SpParameters.Add("@Status", formData["priceFileType"].ToString());

                SpParameters.Add("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(new List<FTPTicker>()).AsTableValuedParameter("Type_PriceFileDataList"));
                SpParameters.Add("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(GsecPriceFileDataList).AsTableValuedParameter("Type_PriceFileDataListGsec"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertPriceFile", SpParameters);
            }

            else if (formData["priceFileType"] == "ManualUpload" && formData["exchange"] == "CSE")
            {
                CSEPriceFileDto FileData = JsonConvert.DeserializeObject<CSEPriceFileDto>(formData["priceFile"]);

                List<GsecPriceFileDto> DataList = new List<GsecPriceFileDto>();

                foreach(var item in FileData.td)
                {
                    DataList.Add(new GsecPriceFileDto
                    {
                        ClosePrice = item.ClosePrice.Remove(item.ClosePrice.Length - 2),
                        Security = item.ScripCd.Trim(),
                        SecurityName = item.ScripName.Trim(),
                        SecurityShortName = item.ScripId.Trim(),
                        SessionName = item.slNo.Trim()

                    });
                }

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@ExchangeName", formData["exchange"].ToString());
                SpParameters.Add("@IsGsec", IsGsec);
                SpParameters.Add("@TradingDate", formData["tradingDate"].ToString());
                SpParameters.Add("@FileName", formData["fileName"].ToString());

                SpParameters.Add("@FileExtension", Path.GetExtension(formData["fileName"].ToString()));
                SpParameters.Add("@FileSizeInKB", Convert.ToInt32(formData["fileSize"]) / 1000);
                SpParameters.Add("@Status", formData["priceFileType"].ToString());

                SpParameters.Add("@PriceFileDataList", ListtoDataTableConverter.ToDataTable((new List<FTPTicker>())).AsTableValuedParameter("Type_PriceFileDataList"));
                SpParameters.Add("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_PriceFileDataListGsec"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("CM_InsertPriceFile", SpParameters);
            }
           
            return await Task.FromResult("");

        }
        public async Task<object> PriceFileComparisonFromFTP(string UserName, int CompanyID, int BranchID)
        {

            var FileList = FtpUtility.FileList("ftp://192.168.114.118:8021/", "Shourav", "111@@@idlc");

            if (FileList.Count() == 0 && FileList.Where(f => f.FileName.Contains(".xml")).ToList().Count == 0)
            {
                return await Task.FromResult("No XML file found in FTP directory");
            }
            else
            {
                string FileName = FileList.Where(f => f.FileName.Contains(".xml")).FirstOrDefault().FileName;

                string Content = FtpUtility.FileContent("ftp://192.168.114.118:8021/" + FileName, "Shourav", "111@@@idlc");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Content);
                string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                json = Regex.Replace(json, "[@]", string.Empty);
                priceFileFTPDto priceFileDto = JsonConvert.DeserializeObject<priceFileFTPDto>(json);

                List<FTPTicker> DataList = new List<FTPTicker>();

                foreach (var item in priceFileDto.EODTickers.Ticker) DataList.Add(item);


                SqlParameter[] sqlParams = new SqlParameter[]
                {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@IsGsec", false),
                new SqlParameter("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(DataList)),
                new SqlParameter("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(new List<GsecPriceFileDto>())),
                new SqlParameter("@ReturnMessage", "")
                };

                var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListComparePriceFileValidation]", sqlParams);

                var ComparisonDataSet = _dbCommonOperation.FindMultipleDataSetBySP("CM_ListComparePriceFile", sqlParams);


                List<Attributes> NewInstrumentList = CustomConvert.DataSetToList<Attributes>(ValidationDataSets.Tables[0]);
                List<dynamic> NewCategoryList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[1]);
                List<dynamic> CompareResult = CustomConvert.DataSetToList<dynamic>(ComparisonDataSet.Tables[0]);


                var Result = new
                {
                    FileDataList = priceFileDto.EODTickers,
                    NewInstrumentList = NewInstrumentList.Where(c => c.Sector != null).ToList(),
                    NewCategoryList = NewCategoryList,
                    CompareResult = CompareResult
                };
                return await Task.FromResult(Result);
            }

            return await Task.FromResult("");
        }
       
        public async Task<object> PriceFileComparison(IFormCollection formData,string UserName, int CompanyID, int BranchID)
        {
            
            if (Convert.ToBoolean(JsonConvert.DeserializeObject(formData["GSec"])))
            {
                var GsecPriceFileDataList = JsonConvert.DeserializeObject<List<GsecPriceFileDto>>(formData["priceFile"]);

                GsecPriceFileDataList = GsecPriceFileDataList.Where(s => s.Security != null && s.Security.Length > 3).ToList();
                                SqlParameter[] sqlParams = new SqlParameter[]
                {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@IsGsec", true),
                new SqlParameter("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(new List<Attributes>())),
                new SqlParameter("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(GsecPriceFileDataList)),
                new SqlParameter("@ReturnMessage", "")
                };

              
                var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListComparePriceFileValidation]", sqlParams);

                var Result = new
                {
                    TradeDateValidation = ValidationDataSets.Tables[6].Rows[0][0].ToString(),
                    NewInstrumentList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[0]),
                    NewCategoryList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[1]),
                    CompareResult = new
                    {
                        CategoryChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[2]),
                        SpotInstrumentList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[3]),
                        AssetClassChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[4]),
                        SectorChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[5])
                    }
                };
                return await Task.FromResult(Result);
            }
            else
            {
                priceFileDto priceFileDto = JsonSerializer.Deserialize<priceFileDto>(formData["priceFile"]);

                List<Attributes> DataList = new List<Attributes>();

                foreach (var item in priceFileDto.EODTickers.Ticker) DataList.Add(item._attributes);


                SqlParameter[] sqlParams = new SqlParameter[]
                {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@IsGsec", false),
                new SqlParameter("@PriceFileDataList", ListtoDataTableConverter.ToDataTable(DataList)),
                 new SqlParameter("@PriceFileGsecDataList", ListtoDataTableConverter.ToDataTable(new List<GsecPriceFileDto>())),
                new SqlParameter("@ReturnMessage", "")
                };

                

                var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListComparePriceFileValidation]", sqlParams);

                var Result = new
                {
                    TradeDateValidation = ValidationDataSets.Tables[6].Rows[0][0].ToString(),
                    NewInstrumentList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[0]),
                    NewCategoryList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[1]),
                    CompareResult = new
                    {
                        CategoryChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[2]),
                        SpotInstrumentList = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[3]),
                        AssetClassChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[4]),
                        SectorChange = CustomConvert.DataSetToList<dynamic>(ValidationDataSets.Tables[5])
                    }
                };
                return await Task.FromResult(Result);
            }

            

        }

     
        public async Task<List<ClosingPriceFileListDto>> ClosingPriceFileList(int CompanyID, DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, TradeDate = TradeDate };
            return await _dbCommonOperation.ReadSingleTable<ClosingPriceFileListDto>("[dbo].[CM_ListClosingPriceFile]", values);
        }
    }
}
