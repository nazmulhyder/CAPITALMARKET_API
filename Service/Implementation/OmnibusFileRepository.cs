using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic.FileIO;
using Model.DTOs;
using Model.DTOs.ImportExportOmnibus;
using Model.DTOs.TradeFileUpload;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Implementation
{
    public class OmnibusFileRepository : IOmnibusFileRepository
    {
        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;
        public OmnibusFileRepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }

        #region AML IL
        public async Task<List<ExportFileBrokerDto>> BrokerList(int CompanyID, int BranchID)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBrokerForExportLimitFile]", sqlParams);

            return Utility.CustomConvert.DataSetToList<ExportFileBrokerDto>(DataSets.Tables[0]);
        }

        public async Task<object> GetExportedFiles(IFormCollection form, string UserName, string FileType, int CompanyID, int BranchID)
        {
            string ExportedPath = "";
            string ExportedPathExplorer = "";

            List<ExportFileDto> FileList = new List<ExportFileDto>();
            StringBuilder XMLSB = new StringBuilder();
            StringBuilder TextDSESB = new StringBuilder();
            StringBuilder TextCSESB = new StringBuilder();
            StringBuilder CtrlFile = new StringBuilder();

            int BrokerID = Convert.ToInt32(form["BrokerId"].ToString());
            string ExchangeID = form["ExchangeID"].ToString();
            string FileName = "";
            string FileExtension = "";
            string ProcessingMode = form["ProcessingMode"].ToString();
            string TradeDate = form["TransactionDate"].ToString();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CompanyID", CompanyID);
            param[1] = new SqlParameter("@BranchID", BranchID);
            var DSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListBrokerForExportLimitFile]", param);
            List<ExportFileBrokerDto> BrokerList = Utility.CustomConvert.DataSetToList<ExportFileBrokerDto>(DSets.Tables[0]);

            if (BrokerID > 0) BrokerList = BrokerList.Where(b => b.BrokerID == BrokerID).ToList();

            List<ILAMLCashLimitFileDto> CashLimitList = new List<ILAMLCashLimitFileDto>();
            List<ILAMLShareLimitFileDto> ShareLimitList = new List<ILAMLShareLimitFileDto>();

            SqlParameter[] sqlParams = new SqlParameter[11];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@BrokerID", BrokerID);
            sqlParams[4] = new SqlParameter("@ExchangeID", ExchangeID);
            sqlParams[5] = new SqlParameter("@FileName", FileName);
            sqlParams[6] = new SqlParameter("@FileExtension", FileExtension);
            sqlParams[7] = new SqlParameter("@FileType", FileType);
            sqlParams[8] = new SqlParameter("@ProcessingMode", ProcessingMode);
            sqlParams[9] = new SqlParameter("@TradeDate", TradeDate);
            sqlParams[10] = new SqlParameter("@ReturnMessage", "");
            sqlParams[10].Direction = ParameterDirection.Output;

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ExportLimitFile]", sqlParams);

            if(FileType.ToLower() == "cashlimit")
                CashLimitList = Utility.CustomConvert.DataSetToList<ILAMLCashLimitFileDto>(DataSets.Tables[0]);
            else
                ShareLimitList = Utility.CustomConvert.DataSetToList<ILAMLShareLimitFileDto>(DataSets.Tables[0]);


            if (FileType.ToLower() == "cashlimit")
            {
                foreach(var BrokerItem in BrokerList)
                {
                    FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Clients-BDS";
                    FileExtension = ".xml";

                    XMLSB.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    XMLSB.Append(Environment.NewLine);
                    XMLSB.Append("<Clients xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" mlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ProcessingMode=\"BatchInsertOrUpdate\" ");
                    XMLSB.Append(Environment.NewLine);

                    foreach (var item in CashLimitList.Where(c=>c.BrokerID == BrokerItem.BrokerID))
                    {
                        XMLSB.Append("<Limits>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<Cash>" + item.Purchasepower.ToString() + "</Cash>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("</Limits>");
                        XMLSB.Append(Environment.NewLine);
                        //DSE CASH
                        TextDSESB.Append(item.AccountNumber.Trim() + "~" + item.BOCode.Trim() + "~" + item.MemberName + "~" + "N~" + item.Purchasepower.ToString() + "~" + Convert.ToDateTime(Utility.DatetimeFormatter.DateFormat(TradeDate)).ToString("dd'-'MM'-'yyyy':'HH':'mm':'ss"));
                        TextDSESB.Append(Environment.NewLine);

                        //CSE CASH
                        TextCSESB.Append(item.AccountNumber.Trim() + "|" + item.Purchasepower.ToString() + "|" + DateTime.Now.ToString("yyyy'-'MM'-'dd"));
                        TextCSESB.Append(Environment.NewLine);
                    }

                    XMLSB.Append(Environment.NewLine);
                    XMLSB.Append("</Clients>");

                    FileList = new List<ExportFileDto>();

                    FileList.Add(new ExportFileDto
                    {
                        FileName = FileName,
                        FileExtention = FileExtension,
                        FileContent = XMLSB.ToString(),
                    });

                    if (ExchangeID == "6")
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "DSE Cash",
                            FileExtention = ".txt",
                            FileContent = TextDSESB.ToString(),
                        });
                    }
                    else if (ExchangeID == "7")
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "CSE Cash",
                            FileExtention = ".txt",
                            FileContent = TextCSESB.ToString(),
                        });
                    }
                    else
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "DSE Cash",
                            FileExtention = ".txt",
                            FileContent = TextDSESB.ToString(),
                        });

                        FileList.Add(new ExportFileDto
                        {
                            FileName = "CSE Cash",
                            FileExtention = ".txt",
                            FileContent = TextCSESB.ToString(),
                        });
                    }


                    //xml control file
                    CtrlFile.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    CtrlFile.Append(Environment.NewLine);
                    CtrlFile.Append("<Control xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Hash=\"" + CreateMD5(XMLSB.ToString()) + "/>");
                    CtrlFile.Append(Environment.NewLine);

                    //
                    FileList.Add(new ExportFileDto
                    {
                        FileName = FileName + "-ctrl",
                        FileExtention = ".xml",
                        FileContent = CtrlFile.ToString(),
                    });

                    //saving all files
                    foreach (var item in FileList)
                    {
                        StringBuilder FilePath = new StringBuilder();

                        FilePath.Append(@"\ExportedFiles\CashLimit\");
                        ExportedPath = "/ExportedFiles/CashLimit/";
                        ExportedPathExplorer = "\\192.168.115.17\\ExportedFiles\\CashLimit\\";

                        FilePath.Append(Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy"));
                        
                        ExportedPath = ExportedPath + Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
                        ExportedPathExplorer = ExportedPathExplorer + Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");

                        FilePath.Append(@"\");
                        FilePath.Append(BrokerItem.BrokerName.Trim());
                        FilePath.Append(@"\");
                        string Directory = FilePath.ToString();
                        

                        bool exists = System.IO.Directory.Exists(Directory);

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Directory);

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory + item.FileName + item.FileExtention))
                        {
                            file.WriteLine(item.FileContent); // "sb" is the StringBuilder
                        }

                    }

                }


            }

            else if(FileType.ToLower() == "sharelimit")
            {
                foreach(var BrokerItem in BrokerList)
                {
                    FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Positions-BDS";
                    FileExtension = ".xml";

                    XMLSB.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    XMLSB.Append(Environment.NewLine);
                    XMLSB.Append("<Clients xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" mlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ProcessingMode=\"BatchInsertOrUpdate\" ");
                    XMLSB.Append(Environment.NewLine);

                    foreach (var item in ShareLimitList)
                    {
                        XMLSB.Append("<InsertOne>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<SecurityCode><![CDATA[" + item.ScripName.Trim() + "]]></SecurityCode>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<ISIN><![CDATA[" + item.ISIN.Trim() + "]]></ISIN>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<Quantit>" + item.FreeQuantity.ToString() + "</Quantit>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("<TotalCost>" + item.TotalCost.ToString() + "</TotalCost>");
                        XMLSB.Append(Environment.NewLine);
                        XMLSB.Append("</InsertOne>");
                        XMLSB.Append(Environment.NewLine);


                        //DSE STOCK
                        TextDSESB.Append(item.ISIN.Trim() + "~" + item.ScripName.Trim() + "~" + item.BOCode + "~" + item.MemberName.ToString() + "~" + item.FreeQuantity + "~" + item.FreeQuantity + "~" + item.AccountNumber.Trim() + "~" + Convert.ToDateTime(Utility.DatetimeFormatter.DateFormat(TradeDate)).ToString("dd' 'MMM' 'yyyy"));
                        TextDSESB.Append(Environment.NewLine);

                        //CSE STOCK
                        TextCSESB.Append(item.AccountNumber.Trim() + "|" + item.BOCode.ToString() + "|" + item.ISIN + "~" + item.FreeQuantity + "~" + Convert.ToDateTime(Utility.DatetimeFormatter.DateFormat(TradeDate)).ToString("dd'-'MMM'-'yyyy"));
                        TextCSESB.Append(Environment.NewLine);
                    }

                    XMLSB.Append(Environment.NewLine);
                    XMLSB.Append("</Clients>");
                    XMLSB.Append(Environment.NewLine);

                    FileList = new List<ExportFileDto>();

                    FileList.Add(new ExportFileDto
                    {
                        FileName = FileName,
                        FileExtention = FileExtension,
                        FileContent = XMLSB.ToString(),
                    });

                    if (ExchangeID == "6")
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "DSE Stock",
                            FileExtention = ".txt",
                            FileContent = TextDSESB.ToString(),
                        });
                    }
                    else if (ExchangeID == "7")
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "CSE Stock",
                            FileExtention = ".txt",
                            FileContent = TextCSESB.ToString(),
                        });
                    }
                    else
                    {
                        FileList.Add(new ExportFileDto
                        {
                            FileName = "DSE Stock",
                            FileExtention = ".txt",
                            FileContent = TextDSESB.ToString(),
                        });

                        FileList.Add(new ExportFileDto
                        {
                            FileName = "CSE Stock",
                            FileExtention = ".txt",
                            FileContent = TextCSESB.ToString(),
                        });

                    }


                    //xml control file
                    CtrlFile.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    CtrlFile.Append(Environment.NewLine);
                    CtrlFile.Append("<Control xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Hash=\"" + CreateMD5(XMLSB.ToString()) + "/>");
                    CtrlFile.Append(Environment.NewLine);

                    //
                    FileList.Add(new ExportFileDto
                    {
                        FileName = FileName + "-ctrl",
                        FileExtention = ".xml",
                        FileContent = CtrlFile.ToString(),
                    });

                    //saving all files
                    foreach (var item in FileList)
                    {
                        StringBuilder FilePath = new StringBuilder();


                        FilePath.Append(@"\ExportedFiles\ShareLimit\");
                        ExportedPath = "/ExportedFiles/ShareLimit/";
                        ExportedPathExplorer = "\\192.168.115.17\\ExportedFiles\\ShareLimit\\";

                        FilePath.Append(Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy"));

                        ExportedPath = ExportedPath + Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
                        ExportedPathExplorer = ExportedPathExplorer + Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");

                        FilePath.Append(@"\");
                        FilePath.Append(BrokerItem.BrokerName.Trim());
                        FilePath.Append(@"\");
                        string Directory = FilePath.ToString();


                        bool exists = System.IO.Directory.Exists(Directory);

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Directory);

                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(Directory + item.FileName + item.FileExtention))
                        {
                            file.WriteLine(item.FileContent); // "sb" is the StringBuilder
                        }

                    }
                }

            }



            return new
            {
                ExportedPath = ExportedPath,
                ExportedPathExplorer = ExportedPathExplorer
            };
        }

        #endregion AML IL

        #region SL
        public async Task<object> SLLimitFileExport(IFormCollection formdata, int CompanyID, int BranchID, string Username)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder CtrlFile = new StringBuilder();
            string LimitType = formdata["LimitType"].ToString();
            string FileName = "";

            List<LimitFileDto> FileList = JsonConvert.DeserializeObject<List<LimitFileDto>>(formdata["FileList"]);

            string FileIDs = String.Join(",", FileList.Select(x => x.OmbLimitFileID.ToString()));

            string ExportType = formdata["ExportType"].ToString();
            string FTPServerAddress = formdata["FTPServerAddress"].ToString();
            string FTPServerUserName = formdata["FTPServerUserName"].ToString();
            string FTPServerUserPassword = formdata["FTPServerUserPassword"].ToString();
            string ServerFilePath = formdata["ServerFilePath"].ToString();
            string TradeDate = formdata["TransactionDate"].ToString();
           
            if (LimitType == "CashLimit")
            {
                FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Clients-ISL";

                SqlParameter[] sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@FileIDs", FileIDs);
                sqlParams[1] = new SqlParameter("@UserName", Username);
                sqlParams[2] = new SqlParameter("@ExchangeID", FileList[0].Exchangeid);
                sqlParams[3] = new SqlParameter("@FileName", FileName + ".xml");
                sqlParams[4] = new SqlParameter("@FileExtension", ".xml");
                //sqlParams[5] = new SqlParameter("@FileSizeInKB", 0.000);
                sqlParams[5] = new SqlParameter("@FileType", formdata["FileType"].ToString());
                sqlParams[6] = new SqlParameter("@ProcessingMode", formdata["ProcessingMode"].ToString());
                sqlParams[7] = new SqlParameter("@TradeDate", TradeDate);
                sqlParams[8] = new SqlParameter("@ReturnMessage", "");
                sqlParams[8].Direction = ParameterDirection.Output;

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_ExportCashLimitFile]", sqlParams);

                List<RegistrationTagDto> registrationTags = Utility.CustomConvert.DataSetToList<RegistrationTagDto>(DataSets.Tables[0]);
                List<BuySellLimitTagDto> buySellLimitTags = Utility.CustomConvert.DataSetToList<BuySellLimitTagDto>(DataSets.Tables[1]);
                List<MarketLimitTagDto> marketLimitTags = Utility.CustomConvert.DataSetToList<MarketLimitTagDto>(DataSets.Tables[2]);

               
                //xml header

                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("<Clients ProcessingMode=\"BatchInsertOrUpdate\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Clients.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                stringBuilder.Append(Environment.NewLine);
                //register tags
                foreach (var item in registrationTags.Where(r => r.Status == "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Register>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<DealerID>" + item.DealerID.Trim() + "</DealerID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<BOID>" + item.BOID.Trim() + "</BOID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<WithNetAdjustment>" + item.WithNetAdjustment + "</WithNetAdjustment>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Name><![CDATA[" + item.AccountName.Trim() + "]]></Name>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortName><![CDATA[]]></ShortName>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Address><![CDATA[" + item.Address.Trim() + "]]></Address>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Tel><![CDATA[" + item.Telephone.Trim() + "]]></Tel>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ICNo>" + item.ICNo + "</ICNo>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<AccountType><![CDATA[" + item.AccountType.Trim() + "]]></AccountType>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortSellingAllowed>" + item.ShortSellAllowing.Trim() + "</ShortSellingAllowed>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Register>");
                    stringBuilder.Append(Environment.NewLine);
                }


                stringBuilder.Append(Environment.NewLine);
                // client deactivate
                foreach (var item in registrationTags.Where(r => r.Status == "Closed"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
                // client suspend
                foreach (var item in registrationTags.Where(r => r.Status != "Closed" && r.Status != "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Sell_Suspend>Suspend</Sell_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Buy_Suspend>Suspend</Buy_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Remark></Remark>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);

                //limit tags
                foreach (var item in buySellLimitTags)
                {
                    stringBuilder.Append("<Limits>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.TradingCode.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    if(item.MarginLimit > 0)
                    {
                        stringBuilder.Append("<Margin>" + (item.MarginLimit).ToString() + "</Margin>");
                        stringBuilder.Append(Environment.NewLine);
                    }
                    stringBuilder.Append("<Cash>" + (item.PurchasePower + item.IncrementalPP).ToString() + "</Cash>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Limits>");
                    stringBuilder.Append(Environment.NewLine);
                }
                stringBuilder.Append(Environment.NewLine);
               
                //market tags
                foreach (var item in marketLimitTags)
                {
                    stringBuilder.Append("<LimitsWithMarket Market=\"" + item.ShortName + "\">");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.TradingCode.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    if(item.maxcapitalbuy != null)
                    {
                        stringBuilder.Append("<MaxCapitalBuy>" + (item.maxcapitalbuy).ToString() + "</MaxCapitalBuy>");
                        stringBuilder.Append(Environment.NewLine);
                    }

                    if (item.maxcapitalSell != null)
                    {
                        stringBuilder.Append("<MaxCapitalSell>" + (item.maxcapitalSell).ToString() + "</MaxCapitalSell>");
                        stringBuilder.Append(Environment.NewLine);
                    }

                    if (item.TotalTransaction != null)
                    {
                        stringBuilder.Append("<TotalTransaction>" + (item.TotalTransaction).ToString() + "</TotalTransaction>");
                        stringBuilder.Append(Environment.NewLine);
                    }

                    if (item.NetTransaction != null)
                    {
                        stringBuilder.Append("<NetTransaction>" + (item.NetTransaction).ToString() + "</NetTransaction>");
                        stringBuilder.Append(Environment.NewLine);
                    }

                    stringBuilder.Append("</LimitsWithMarket>");
                    stringBuilder.Append(Environment.NewLine);
                }
                stringBuilder.Append(Environment.NewLine);
                //xml close tag
                stringBuilder.Append("</Clients>");
                stringBuilder.Append(Environment.NewLine);


				string DateFolder = Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
				bool exists = System.IO.Directory.Exists(@"\ExportedFiles\OmnibusLimitFile\" + DateFolder + "\\");

				if (!exists)
				{
					System.IO.Directory.CreateDirectory(@"\ExportedFiles\OmnibusLimitFile\" + DateFolder + "\\");
				}

				//saving xml
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusLimitFile\" + DateFolder + "\\" + FileName + ".xml"))
                {
                    file.WriteLine(stringBuilder.ToString()); // "sb" is the StringBuilder
                }

                CtrlFile.Append("<Control Hash=\""+CreateMD5(stringBuilder.ToString())+"\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Control.xsd\" Method=\"MD5\" />");
                //xml control file
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusLimitFile\" + DateFolder + "\\" + FileName + "-ctrl" + ".xml"))
                {
                    file.WriteLine(CtrlFile.ToString()); 
                }
            }
            else if(LimitType == "ShareLimit") //FOR SHARE LIMIT/POSITION
            {
                SqlParameter[] sqlParams = new SqlParameter[9];

                sqlParams[0] = new SqlParameter("@FileIDs", FileIDs);
                sqlParams[1] = new SqlParameter("@UserName", Username);
                sqlParams[2] = new SqlParameter("@ExchangeID", FileList[0].Exchangeid);
                sqlParams[3] = new SqlParameter("@FileName", FileName + ".xml");
                sqlParams[4] = new SqlParameter("@FileExtension", ".xml");
                sqlParams[5] = new SqlParameter("@FileType", formdata["LimitType"].ToString());
                sqlParams[6] = new SqlParameter("@ProcessingMode", formdata["ProcessingMode"].ToString());
                sqlParams[7] = new SqlParameter("@TradeDate", TradeDate);
                sqlParams[8] = new SqlParameter("@ReturnMessage", "");
                sqlParams[8].Direction = ParameterDirection.Output;

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_ExportStockLimitFile]", sqlParams);

                List<SLStockLimitFileDto> SLStockLimitFile = Utility.CustomConvert.DataSetToList<SLStockLimitFileDto>(DataSets.Tables[0]);


                //xml header

                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("<<Positions ProcessingMode=\"IncrementQuantity\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Positions.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                stringBuilder.Append(Environment.NewLine);

                // <InsertOne>
                foreach (var item in SLStockLimitFile)
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<InsertOne>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.TradingCode.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<SecurityCode><![CDATA[" + item.ScriptsName.Trim() + "]]></SecurityCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ISIN><![CDATA[" + item.ISIN.Trim() + "]]></ISIN>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Quantit>"+item.SalableQuantity.ToString()+"</Quantit>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<TotalCost>"+item.TotalCost.ToString()+"</TotalCost>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<PositionType>" + item.PositionType.ToString()+ "</PositionType>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</InsertOne>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);

                //xml close tag
                stringBuilder.Append("</Positions>");
                stringBuilder.Append(Environment.NewLine);

                FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Positions-ISL";

				string DateFolder = Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
				bool exists = System.IO.Directory.Exists(@"\ExportedFiles\OmnibusShareLimitFile\" + DateFolder + "\\");

				if (!exists)
				{
					System.IO.Directory.CreateDirectory(@"\ExportedFiles\OmnibusShareLimitFile\" + DateFolder + "\\");
				}
				//saving xml
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusShareLimitFile\" + DateFolder + "\\" + FileName + ".xml"))
                {
                    file.WriteLine(stringBuilder.ToString()); // "sb" is the StringBuilder
                }

                CtrlFile.Append("<Control Hash=\"" + CreateMD5(stringBuilder.ToString()) + "\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Control.xsd\" Method=\"MD5\" />");
                //xml control file
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusShareLimitFile\" + DateFolder + "\\" + FileName + "-ctrl" + ".xml"))
                {
                    file.WriteLine(CtrlFile.ToString());
                }
            }
            
            else // REGISTRATION FILE
            {
                FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Clients-Registration-ISL";

                SqlParameter[] sqlParams = new SqlParameter[8];

                sqlParams[0] = new SqlParameter("@UserName", Username);
                sqlParams[1] = new SqlParameter("@ExchangeID", FileList[0].Exchangeid);
                sqlParams[2] = new SqlParameter("@FileName", FileName + ".xml");
                sqlParams[3] = new SqlParameter("@FileExtension", ".xml");
                sqlParams[4] = new SqlParameter("@FileType", formdata["LimitType"].ToString());
                sqlParams[5] = new SqlParameter("@ProcessingMode", formdata["ProcessingMode"].ToString());
                sqlParams[6] = new SqlParameter("@TradeDate", TradeDate);
                sqlParams[7] = new SqlParameter("@ReturnMessage", "");
                sqlParams[7].Direction = ParameterDirection.Output;

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_ExportClientRegistrationFile]", sqlParams);

                List<RegistrationTagDto> registrationTags = Utility.CustomConvert.DataSetToList<RegistrationTagDto>(DataSets.Tables[0]);
               

                //xml header

                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("<Clients ProcessingMode=\"BatchInsertOrUpdate\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Clients.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                stringBuilder.Append(Environment.NewLine);
                //register tags
                foreach (var item in registrationTags.Where(r => r.Status == "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Register>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<DealerID>" + item.DealerID.Trim() + "</DealerID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<BOID>" + item.BOID.Trim() + "</BOID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<WithNetAdjustment>" + item.WithNetAdjustment + "</WithNetAdjustment>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Name><![CDATA[" + item.AccountName.Trim() + "]]></Name>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortName><![CDATA[]]></ShortName>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Address><![CDATA[" + item.Address.Trim() + "]]></Address>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Tel><![CDATA[" + item.Telephone.Trim() + "]]></Tel>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ICNo>" + item.ICNo + "</ICNo>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<AccountType><![CDATA[" + item.AccountType.Trim() + "]]></AccountType>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortSellingAllowed>" + item.ShortSellAllowing.Trim() + "</ShortSellingAllowed>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Register>");
                    stringBuilder.Append(Environment.NewLine);
                }


                stringBuilder.Append(Environment.NewLine);
                // client deactivate
                foreach (var item in registrationTags.Where(r => r.Status == "Closed"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
                // client suspend
                foreach (var item in registrationTags.Where(r => r.Status != "Closed" && r.Status != "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Sell_Suspend>Suspend</Sell_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Buy_Suspend>Suspend</Buy_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Remark></Remark>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
                //xml close tag
                stringBuilder.Append("</Clients>");
                stringBuilder.Append(Environment.NewLine);

				string DateFolder = Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
				bool exists = System.IO.Directory.Exists(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\");

				if (!exists)
				{
					System.IO.Directory.CreateDirectory(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\");
				}
				//saving xml
				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\" + FileName + ".xml"))
                {
                    file.WriteLine(stringBuilder.ToString()); // "sb" is the StringBuilder
                }

                CtrlFile.Append("<Control Hash=\"" + CreateMD5(stringBuilder.ToString()) + "\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Control.xsd\" Method=\"MD5\" />");
                //xml control file
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\" + FileName + "-ctrl" + ".xml"))
                {
                    file.WriteLine(CtrlFile.ToString());
                }
            }
            return await Task.FromResult(new
            {
                FileName = FileName + ".xml",
                FileContent = stringBuilder.ToString(),
                Message = "File Exported Successfully",
                ControlFileName = FileName + "-ctrl" + ".xml",
                ControlFileFileContent = CtrlFile.ToString()
            });
        }

     
         public async Task<object> SLClientRegistrationFileExport(IFormCollection formdata, int CompanyID, int BranchID, string Username)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder CtrlFile = new StringBuilder();
            string LimitType = formdata["LimitType"].ToString();
            string FileName = "";

            string ExportType = formdata["ExportType"].ToString();
            string FTPServerAddress = formdata["FTPServerAddress"].ToString();
            string FTPServerUserName = formdata["FTPServerUserName"].ToString();
            string FTPServerUserPassword = formdata["FTPServerUserPassword"].ToString();
            string ServerFilePath = formdata["ServerFilePath"].ToString();
            string ClientType = formdata["ClientType"].ToString();

            string TradeDate = Utility.DatetimeFormatter.DateFormat(formdata["TransactionDate"].ToString());
            string FromDate = Utility.DatetimeFormatter.DateFormat(formdata["FromDate"].ToString()) ;
            string ToDate = Utility.DatetimeFormatter.DateFormat(formdata["ToDate"].ToString());
          
                FileName = DateTime.Now.ToString("yyyymmdd-hhmmss") + "-Clients-Registration-ISL";

                SqlParameter[] sqlParams = new SqlParameter[10];

                sqlParams[0] = new SqlParameter("@UserName", Username);
                sqlParams[1] = new SqlParameter("@ExchangeID", formdata["ExchangeID"].ToString());
                sqlParams[2] = new SqlParameter("@FileName", FileName + ".xml");
                sqlParams[3] = new SqlParameter("@FileExtension", ".xml");
                sqlParams[4] = new SqlParameter("@FileType", formdata["LimitType"].ToString());
                sqlParams[5] = new SqlParameter("@TradeDate", TradeDate);
                sqlParams[6] = new SqlParameter("@FromDate", FromDate);
                sqlParams[7] = new SqlParameter("@ToDate", ToDate);
                sqlParams[8] = new SqlParameter("@ClientType", ClientType);
                sqlParams[9] = new SqlParameter("@ReturnMessage", "");
                sqlParams[9].Direction = ParameterDirection.Output;

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_ExportClientRegistrationFile]", sqlParams);

                List<RegistrationTagDto> registrationTags = Utility.CustomConvert.DataSetToList<RegistrationTagDto>(DataSets.Tables[0]);
               

                //xml header

                stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("<Clients ProcessingMode=\"BatchInsertOrUpdate\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Clients.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                stringBuilder.Append(Environment.NewLine);
                //register tags
                foreach (var item in registrationTags.Where(r => r.Status == "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Register>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<DealerID>" + item.DealerID.Trim() + "</DealerID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<BOID>" + item.BOID.Trim() + "</BOID>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<WithNetAdjustment>" + item.WithNetAdjustment + "</WithNetAdjustment>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Name><![CDATA[" + item.AccountName.Trim() + "]]></Name>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortName><![CDATA[]]></ShortName>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Address><![CDATA[" + item.Address.Trim() + "]]></Address>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Tel><![CDATA[" + item.Telephone.Trim() + "]]></Tel>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ICNo>" + item.ICNo + "</ICNo>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<AccountType><![CDATA[" + item.AccountType.Trim() + "]]></AccountType>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ShortSellingAllowed>" + item.ShortSellAllowing.Trim() + "</ShortSellingAllowed>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Register>");
                    stringBuilder.Append(Environment.NewLine);
                }


                stringBuilder.Append(Environment.NewLine);
                // client deactivate
                foreach (var item in registrationTags.Where(r => r.Status == "Closed"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Deactivate>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
                // client suspend
                foreach (var item in registrationTags.Where(r => r.Status != "Closed" && r.Status != "Active"))
                {
                    //stringBuilder.Append(Environment.NewLine)
                    stringBuilder.Append("<Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<ClientCode>" + item.AccountNumber.Trim() + "</ClientCode>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Sell_Suspend>Suspend</Sell_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Buy_Suspend>Suspend</Buy_Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("<Remark></Remark>");
                    stringBuilder.Append(Environment.NewLine);
                    stringBuilder.Append("</Suspend>");
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(Environment.NewLine);
                //xml close tag
                stringBuilder.Append("</Clients>");
                stringBuilder.Append(Environment.NewLine);

			string DateFolder = Convert.ToDateTime(TradeDate).ToString("dd-MMM-yyyy");
			bool exists = System.IO.Directory.Exists(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\");

			if (!exists)
			{
				System.IO.Directory.CreateDirectory(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\");
			}
			//saving xml
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\" + FileName + ".xml"))
                {
                    file.WriteLine(stringBuilder.ToString()); // "sb" is the StringBuilder
                }

                CtrlFile.Append("<Control Hash=\"" + CreateMD5(stringBuilder.ToString()) + "\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xsi:noNamespaceSchemaLocation=\"Flextrade-BOS-Control.xsd\" Method=\"MD5\" />");
			//xml control file

			
          
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\OmnibusClientRegistrationFile\" + DateFolder + "\\" + FileName + "-ctrl" + ".xml"))
                {
                    file.WriteLine(CtrlFile.ToString());
                }
           
            return await Task.FromResult(new
            {
                FileName = FileName + ".xml",
                FileContent = stringBuilder.ToString(),
                Message = "File Exported Successfully",
                ControlFileName = FileName + "-ctrl" + ".xml",
                ControlFileFileContent = CtrlFile.ToString()
            });
        }

        public async Task<object> SL_ListExportClientRegistrationFile(string UserName, string FileType)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@FileType", FileType);
         
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_ListExportClientRegistrationFile]", sqlParams);

            return DataSets.Tables[0];
        }


        public async Task<object> GetOmnibusAccountList()
        {
            return await _dbCommonOperation.ReadSingleTable<object>("[dbo].[ListOmnibusAccounts]", null);
        }

        public async Task<object> GetOmnibusLimitFileList(int CompanyID,string FileType, DateTime TradeDate)
        {
            var values = new { CompanyID = CompanyID, TradeDate = TradeDate, FileType = FileType };
            return await _dbCommonOperation.ReadSingleTable<object>("[dbo].[CM_ListOmnibusLimitFile]", values);
        }

        public async Task<string> InsertOmnibusLimitFileText(IFormCollection formdata, int CompanyID, int BranchID, string UserName)
        {
            string FileContent = string.Empty;

            using (var reader = new StreamReader(formdata.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if(formdata["LimitType"].ToString() == "CashLimit")
            {
                List<OmnibusCashFileTextDto> DataList = new List<OmnibusCashFileTextDto>();

                foreach (string Line in FileLines.Where(l=>l.Length > 10).ToList())
                {
                    string[] Words = Line.Split("~");

                    DataList.Add(new OmnibusCashFileTextDto
                    {
                        AccountNumber = Words[0],
                        BOID = Words[1],
                        AccountName = Words[2],
                        AccountType = Words[3],
                        PurchasePower = Convert.ToDecimal(Words[4]),
                        Iselectronic = Words[5],
                        TradingDate = DateTime.ParseExact(Words[6].Split(":")[0], "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-mm-dd"),
                        //Convert.ToDateTime(Words[6].Split(":")[0])
                    });
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@SettelemnetAccount", formdata["OmnibusAccount"].ToString());
                SpParameters.Add("@ExchangeID", formdata["Exchange"].ToString());
                SpParameters.Add("@FileName", formdata.Files[0].FileName);
                SpParameters.Add("@FileExtension", Path.GetExtension(formdata.Files[0].FileName));
                SpParameters.Add("@FileSizeInKB", formdata.Files[0].Length / 1000);
                SpParameters.Add("@FileRow", DataList.Count);
                SpParameters.Add("@FileType", formdata["LimitType"].ToString());
                SpParameters.Add("@ProcessingMode", formdata["ProcessingMode"].ToString());
                SpParameters.Add("@TradeDate", formdata["TransactionDate"].ToString());

                SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_OmnibusCashFileText"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("[dbo].[SL_InsertOmnibusCashFileText]", SpParameters);
            }
            else
            {
                List<OmnibusShareFileTextDto> DataList = new List<OmnibusShareFileTextDto>();

                foreach (string Line in FileLines.Where(l => l.Length > 10).ToList())
                {
                    string[] Words = Line.Split("~");

                    string TradingDateString = Words[7].Split(":")[0];

                    DateTime TradingDate = DateTime.Now;

                    try
                    {
                        TradingDate = Convert.ToDateTime(TradingDateString);
                    }
                    catch { }

                    try
                    {
                        TradingDate = Convert.ToDateTime(DateTime.ParseExact(TradingDateString, "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-mm-dd"));

                    }
                    catch
                    {
                    }

                    DataList.Add(new OmnibusShareFileTextDto
                    {
                        ISIN = Words[0],
                        ScriptsName = Words[1],
                        BOID = Words[2],
                        AccountName = Words[3],
                        TotalQuantity = Convert.ToDecimal(Words[4]),
                        SalableQuantity = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        TradingDate = TradingDate.ToString("yyyy-M-d")
                    });
                }

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@SettelemnetAccount", formdata["OmnibusAccount"].ToString());
                SpParameters.Add("@ExchangeID", formdata["Exchange"].ToString());
                SpParameters.Add("@FileName", formdata.Files[0].FileName);
                SpParameters.Add("@FileExtension", Path.GetExtension(formdata.Files[0].FileName));
                SpParameters.Add("@FileSizeInKB", formdata.Files[0].Length / 1000);
                SpParameters.Add("@FileRow", DataList.Count);
                SpParameters.Add("@FileType", formdata["LimitType"].ToString());
                SpParameters.Add("@ProcessingMode", formdata["ProcessingMode"].ToString());
                SpParameters.Add("@TradeDate", formdata["TransactionDate"].ToString());
                SpParameters.Add("@TradeFileList", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_OmnibusShareFileText"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP("[dbo].[SL_InsertOmnibusShareFileText]", SpParameters);
            }
            
        }


        public async Task<object> InsertOmnibusLimitFileValidation(IFormCollection formdata, int CompanyID, int BranchID, string UserName)
        {
            string FileContent = string.Empty;

            using (var reader = new StreamReader(formdata.Files[0].OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            if (formdata["LimitType"].ToString() == "CashLimit")
            {
                List<OmnibusCashFileTextDto> DataList = new List<OmnibusCashFileTextDto>();

                foreach (string Line in FileLines.Where(l => l.Length > 10).ToList())
                {
                    string[] Words = Line.Split("~");

                    DataList.Add(new OmnibusCashFileTextDto
                    {
                        AccountNumber = Words[0],
                        BOID = Words[1],
                        AccountName = Words[2],
                        AccountType = Words[3],
                        PurchasePower = Convert.ToDecimal(Words[4]),
                        Iselectronic = Words[5],
                        TradingDate = DateTime.ParseExact(Words[6].Split(":")[0], "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-mm-dd"),
                        //Convert.ToDateTime(Words[6].Split(":")[0])
                    });
                }

                SqlParameter[] sqlParams = new SqlParameter[12];
              
                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@SettelemnetAccount", formdata["OmnibusAccount"].ToString());
                sqlParams[3] = new SqlParameter("@ExchangeID", formdata["Exchange"].ToString());
                sqlParams[4] = new SqlParameter("@FileName", formdata.Files[0].FileName);
                sqlParams[5] = new SqlParameter("@FileExtension", Path.GetExtension(formdata.Files[0].FileName));
                sqlParams[6] = new SqlParameter("@FileSizeInKB", formdata.Files[0].Length / 1000);
                sqlParams[7] = new SqlParameter("@FileRow", DataList.Count);
                sqlParams[8] = new SqlParameter("@FileType", formdata["LimitType"].ToString());
                sqlParams[9] = new SqlParameter("@ProcessingMode", formdata["ProcessingMode"].ToString());
                sqlParams[10] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(DataList));
               
                sqlParams[11] = new SqlParameter("@ReturnMessage", "");
                sqlParams[11].Direction = ParameterDirection.Output;

                var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_InsertOmnibusCashFileTextValidation]", sqlParams);

                var Result = new
                {
                    ValidationMessage = ValidationDataSets.Tables[7].Rows[0][0].ToString(),
                    NewAccountList = ValidationDataSets.Tables[0],
                    InactiveAccountList = ValidationDataSets.Tables[1],
                    NewBoList = ValidationDataSets.Tables[2],
                    BlankBoList = ValidationDataSets.Tables[3],
                    DuplicateAccountList = ValidationDataSets.Tables[4],
                    DuplicateBoList = ValidationDataSets.Tables[5],
                    newInActiveNonListedInsList = ValidationDataSets.Tables[6]
                    

                };
                return await Task.FromResult(Result);
            }
            else
            {
                List<OmnibusShareFileTextDto> DataList = new List<OmnibusShareFileTextDto>();

                foreach (string Line in FileLines.Where(l => l.Length > 10).ToList())
                {
                    string[] Words = Line.Split("~");

                    string TradingDateString = Words[7].Split(":")[0];

                    DateTime TradingDate = DateTime.Now;

                    try
                    {
                        TradingDate = Convert.ToDateTime(TradingDateString);
                    }
                    catch { }

                    try
                    {
                        TradingDate = Convert.ToDateTime(DateTime.ParseExact(TradingDateString, "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString("yyyy-mm-dd"));

                    }
                    catch
                    {
                    }

                    DataList.Add(new OmnibusShareFileTextDto
                    {
                        ISIN = Words[0],
                        ScriptsName = Words[1],
                        BOID = Words[2],
                        AccountName = Words[3],
                        TotalQuantity = Convert.ToDecimal(Words[4]),
                        SalableQuantity = Convert.ToDecimal(Words[5]),
                        AccountNumber = Words[6],
                        TradingDate = TradingDate.ToString("yyyy-M-d")
                    });
                }

                SqlParameter[] sqlParams = new SqlParameter[12];

                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[2] = new SqlParameter("@SettelemnetAccount", formdata["OmnibusAccount"].ToString());
                sqlParams[3] = new SqlParameter("@ExchangeID", formdata["Exchange"].ToString());
                sqlParams[4] = new SqlParameter("@FileName", formdata.Files[0].FileName);
                sqlParams[5] = new SqlParameter("@FileExtension", Path.GetExtension(formdata.Files[0].FileName));
                sqlParams[6] = new SqlParameter("@FileSizeInKB", formdata.Files[0].Length / 1000);
                sqlParams[7] = new SqlParameter("@FileRow", DataList.Count);
                sqlParams[8] = new SqlParameter("@FileType", formdata["LimitType"].ToString());
                sqlParams[9] = new SqlParameter("@ProcessingMode", formdata["ProcessingMode"].ToString());
                sqlParams[10] = new SqlParameter("@TradeFileList", ListtoDataTableConverter.ToDataTable(DataList));
                sqlParams[11] = new SqlParameter("@ReturnMessage", "");
                sqlParams[11].Direction = ParameterDirection.Output;

                var ValidationDataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[SL_InsertOmnibusShareFileTextValidation]", sqlParams);

                var Result = new
                {
                    ValidationMessage = ValidationDataSets.Tables[7].Rows[0][0].ToString(),
                    NewAccountList = ValidationDataSets.Tables[0],
                    InactiveAccountList = ValidationDataSets.Tables[1],
                    NewBoList = ValidationDataSets.Tables[2],
                    BlankBoList = ValidationDataSets.Tables[3],
                    DuplicateAccountList = ValidationDataSets.Tables[4],
                    DuplicateBoList = ValidationDataSets.Tables[5],
                    newInActiveNonListedInsList = ValidationDataSets.Tables[6]


                };

                return await Task.FromResult(Result);

            }

        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }

        #endregion SL
    }
}
