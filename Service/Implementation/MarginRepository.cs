using Dapper;
using Microsoft.AspNetCore.Http.Features;
using Model.DTOs.Broker;
using Model.DTOs.Document;
using Model.DTOs.FDR;
using Model.DTOs.MarginRequest;
using Model.DTOs.TradeRestriction;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utility;
using static Model.DTOs.KYC.CompleteKYCDto;

namespace Service.Implementation
{
    public class MarginRepository : IMarginRepository
    {

        public readonly IDBCommonOpService _dbCommonOperation;
        public MarginRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<MarginRequestClientInfo> GetClientInfoForMarginRequest(int CompanyID, int BranchID, string UserName, int ProductID, string AccountNo)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ProductID = ProductID, AccountNo = AccountNo };
            var result = await _dbCommonOperation.ReadSingleTable<MarginRequestClientInfo>("CM_ClientInfoForMarginReqSL", values); ;
            return result.FirstOrDefault();
        }

        public async Task<GetMarginRquestDto> InsertUpdateMarginRequest(int CompanyID, int BranchID, string userName, MarginRquestDto entry)
        {
            try
            {
                DataTable documents = new DataTable();
                documents = ListtoDataTableConverter.ToDataTable<EntryDocumentDto>(entry.documents);

                foreach (var item in entry.documents)
                {
                    item.docCollectionDate = Utility.DatetimeFormatter.DateFormat(item.docCollectionDate);
                    item.expectedDate = Utility.DatetimeFormatter.DateFormat(item.expectedDate);
                }

                string sp = "CM_InsertUpdateMarginRequestSL"; 
                int MarginReqID;
                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@MarginRequestID", entry.MarginRequestID);
                SpParameters.Add("@AppraisalInfoID", entry.AppraisalInfoID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@ClientName", entry.ClientName);
                SpParameters.Add("@Address", entry.Address);
                SpParameters.Add("@Equity", entry.Equity);
                SpParameters.Add("@FirstDepositClrDate", Utility.DatetimeFormatter.DateFormat(entry.FirstDepositClrDate));
                SpParameters.Add("@CurrYearTurnOver", entry.CurrYearTurnOver);
                SpParameters.Add("@RealizedGain", entry.RealizedGain);
                SpParameters.Add("@UnrealizedGain", entry.UnrealizedGain);
                SpParameters.Add("@Education", entry.Education);
                SpParameters.Add("@Subject", entry.Subject);
                SpParameters.Add("@Profession", entry.Profession);
                SpParameters.Add("@Designation", entry.Designation);
                SpParameters.Add("@CompanyName", entry.CompanyName);
                SpParameters.Add("@SMExperiance", entry.smExperiance);
                SpParameters.Add("@TargetNetWorth", entry.TargetNetWorth);
                SpParameters.Add("@ClientType", entry.ClientType);
                SpParameters.Add("@CurrTWRR", entry.CurrTWRR); // time waited return
                SpParameters.Add("@ProposedIntRate", entry.ProposedIntRate);
                SpParameters.Add("@ProposedLoanRatio", entry.ProposedLoanRatio);
                SpParameters.Add("@RequestStatus", entry.RequestStatus);
                SpParameters.Add("@ReviewRemarks", entry.ReviewRemarks);
                SpParameters.Add("@CompleteRemarks", entry.CompleteRemarks);
                SpParameters.Add("@Operation", entry.Operation);
                SpParameters.Add("@BusinessDescription", entry.businessDescription);
                SpParameters.Add("@EducationDescription", entry.educationDescription);
                SpParameters.Add("@NetworthDescription", entry.networthDescription);
                SpParameters.Add("@MarginClinkedClients", ListtoDataTableConverter.ToDataTable(entry.marginClinkedClients).AsTableValuedParameter("Type_MarginClinkedClient"));
                SpParameters.Add("@Documents", documents.AsTableValuedParameter("Type_Document"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                var resutrnMsg = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        
                MarginReqID = entry.MarginRequestID.GetValueOrDefault() == 0 ? Convert.ToInt32(resutrnMsg.ToString().Split('-').Last()) : entry.MarginRequestID.GetValueOrDefault();

                return await GetMarginRequestById(CompanyID, BranchID,userName,MarginReqID);

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListMarginRequests_ViewtStatusDto>> ListViewMarginRequestStatus(int CompanyID, int BranchID, int PageNo, int Perpage, string SearchKeyword, string ListType)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, PageNo = PageNo, PerPage = Perpage, SearchKeyword = SearchKeyword, ListType = ListType };
            return await _dbCommonOperation.ReadSingleTable<ListMarginRequests_ViewtStatusDto>("CM_ListMarginRequest_ViewStatus", values);
        }

        public async Task<List<ListMarginRequests_ViewtStatusDto>> ListViewCompletedMarginRequestStatus(int CompanyID, int BranchID, FilterDto filter)
        {
            var values = new { CompanyID = CompanyID, BranchID = BranchID, FilterType = filter.FilterType, FilterFrom = filter.FilterFrom, FilterTo = filter.FilterTo };
            return await _dbCommonOperation.ReadSingleTable<ListMarginRequests_ViewtStatusDto>("CM_ListCompletedMarginRequest", values);
        }

        public async Task<GetMarginRquestDto> GetMarginRequestById(int CompanyID, int BranchID, string UserName, int MarginReqID)
        {
            GetMarginRquestDto marginRquest = new GetMarginRquestDto();


            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@MarginRquestID", MarginReqID)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryMarginRequestSL]", sqlParams);

            marginRquest = CustomConvert.DataSetToList<GetMarginRquestDto>(DataSets.Tables[0]).First();

            marginRquest.marginClinkedClients = CustomConvert.DataSetToList<MarginClinkedClientDto>(DataSets.Tables[1]);

            marginRquest.MerginReqValidations = CustomConvert.DataSetToList<MerginReqValidation>(DataSets.Tables[2]);

            marginRquest.documents = CustomConvert.DataSetToList<DocumentDto>(DataSets.Tables[3]);

            for (int i = 0; i < marginRquest.documents.Count; i++)
            {
                string docFilePath = marginRquest.documents[i].docFilePath + marginRquest.documents[i].docFilePath;
                if (File.Exists(docFilePath))
                {
                    string fileFullPath = Directory.GetCurrentDirectory() + "\\MarginRequestFiles\\" + marginRquest.documents[i].docFilePath;
                    string fileDirectory = Directory.GetCurrentDirectory() + "\\MarginRequestFiles\\";
                    if (!Directory.Exists(fileDirectory))
                        Directory.CreateDirectory(fileDirectory);
                    if (File.Exists(fileFullPath))
                        File.Delete(fileFullPath);
                    File.Copy(docFilePath, fileFullPath);
                    marginRquest.documents[i].docFilePath = FilePath.GetFileUploadURL() +marginRquest.documents[i].docFilePath;
                }
            }

            return marginRquest;
        }

        public async Task<MarginMonitoringSMSEmailDto> ListMarginMonitoring(int CompanyID, int BranchID, string UserName)
        {
            MarginMonitoringSMSEmailDto marginMonitoringSMSEmail = new MarginMonitoringSMSEmailDto();


            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[1] = new SqlParameter("@BranchID", BranchID);


            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CM_ListMarginMonitoring]", sqlParams);


            marginMonitoringSMSEmail.MarginCallZoneList = CustomConvert.DataSetToList<MarginMonitoringDto>(dataset.Tables[0]).ToList();
            marginMonitoringSMSEmail.ForcedSellZoneList = CustomConvert.DataSetToList<MarginMonitoringDto>(dataset.Tables[1]).ToList();


            string sp = "CM_InsertMarginMonitoring";

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@MarginCallZoneList", ListtoDataTableConverter.ToDataTable(marginMonitoringSMSEmail.MarginCallZoneList).AsTableValuedParameter("Type_MarginMonitoringDetail"));
            SpParameters.Add("@ForcedSellZoneList", ListtoDataTableConverter.ToDataTable(marginMonitoringSMSEmail.ForcedSellZoneList).AsTableValuedParameter("Type_MarginMonitoringDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var resutrnMsg = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

            return marginMonitoringSMSEmail;

        }

        public async Task<string> MarginMonitoringSMSEmailSent(int CompanyID, int BranchID, string UserName ,MarginMonitoringSMSEmailDto marginMonitoringSMSEmail)
        {
             List<SendSMSDto> MarginCallSMSList = new List<SendSMSDto>();
             List<SendSMSDto> ForcedSellSMSList = new List<SendSMSDto>();
            
             List<SendEmailDto> MarginCallEmailList = new List<SendEmailDto>();
             List<SendEmailDto> ForcedSellEmailList = new List<SendEmailDto>();


            // SMS Sent List from Margin Call Zone
            foreach (var Item in marginMonitoringSMSEmail.MarginCallZoneList.Where(c => c.IsSMSSent == true && !String.IsNullOrWhiteSpace(c.MobileNo)).ToList())
            {
                var item = new SendSMSDto();
                item.SMSText = "TEXT SMS FROM IDLCSL";
                item.EventName = "Margin Call";
                item.RecipientMobileNo = "01830055496";
                item.DataKeyID = Item.ContractID;
                MarginCallSMSList.Add(item);
                Utility.SMSManager.SendSMS("01675824789", item.SMSText);
            }

            // Email Sent List from Margin Call Zone
            foreach (var Item in marginMonitoringSMSEmail.MarginCallZoneList.Where(c => c.IsEmailSent == true && !String.IsNullOrWhiteSpace(c.EmailAddress)).ToList())
            {
                var item = new SendEmailDto();
                item.RecipientEmailNo = "TEXT EMAIL FROM IDLCSL";
                item.EventName = "Margin Call";
                item.RecipientEmailNo = item.RecipientEmailNo;
                item.DataKeyID = Item.ContractID;
                MarginCallEmailList.Add(item);
                Utility.EmailManager.SendEmail("nhyder.cse131.uiu@gmail.com", item.EmailText, "Margin Monitoring Mail");
            }


            // SMS Sent List from Forced Sell Zone
            foreach (var Item in marginMonitoringSMSEmail.ForcedSellZoneList.Where(c => c.IsSMSSent == true && !String.IsNullOrWhiteSpace(c.MobileNo)).ToList())
            {
                var item = new SendSMSDto();
                item.SMSText = "TEXT SMS FROM IDLCSL";
                item.EventName = "Forced Sell";
                item.RecipientMobileNo = item.RecipientMobileNo;
                item.DataKeyID = Item.ContractID;
                ForcedSellSMSList.Add(item);
                Utility.SMSManager.SendSMS("01675824789", item.SMSText);

            }

            // Email Sent List from Forced Sell Zone
            foreach (var Item in marginMonitoringSMSEmail.ForcedSellZoneList.Where(c => c.IsEmailSent == true && !String.IsNullOrWhiteSpace(c.EmailAddress)).ToList())
            {
                var item = new SendEmailDto();
                item.RecipientEmailNo = "TEXT EMAIL FROM IDLCSL";
                item.EventName = "Forced Sell";
                item.RecipientEmailNo = item.RecipientEmailNo;
                item.DataKeyID = Item.ContractID;
                //Utility.EmailManager.SendEmail(Item.EmailAddress, item.EmailText, "Margin Monitoring Mail");
                ForcedSellEmailList.Add(item);
            }

            //

            string sp = "CM_MarginMonitoring_SentSMSEmailRecord"; 

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@MarginCallSMSList",   ListtoDataTableConverter.ToDataTable(MarginCallSMSList).AsTableValuedParameter("Type_SentSMS"));
            SpParameters.Add("@MarginCallEmailList", ListtoDataTableConverter.ToDataTable(MarginCallEmailList).AsTableValuedParameter("Type_SentEmail"));
            SpParameters.Add("@ForcedSellSMSList",   ListtoDataTableConverter.ToDataTable(ForcedSellSMSList).AsTableValuedParameter("Type_SentSMS"));
            SpParameters.Add("@ForcedSellEmailList", ListtoDataTableConverter.ToDataTable(ForcedSellEmailList).AsTableValuedParameter("Type_SentEmail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }

        public async Task<List<CodesMarginRequestDto>> CodesMarginRequest(int CompanyID, int BranchID, string UserName, string TypeName)
        {
            var values = new { TypeName = TypeName };
            return await _dbCommonOperation.ReadSingleTable<CodesMarginRequestDto>("CodesMarginRequest", values);
        }
        public async Task<List<CodesMarginJson>> AllCodesMargin(int CompanyID, int BranchID, string UserName, int MarginReqID)
        {
            AllCodesMarginDto resDto = new AllCodesMarginDto();

            List<CodesMarginJson> codesMarginJsons = new List<CodesMarginJson>();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@MarginReqID", MarginReqID);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CodesMarginRequestJson]", sqlParams);

            resDto.CodesMrReqEducation = CustomConvert.DataSetToList<CodesMarginRequestDto>(dataset.Tables[0]).ToList();
            resDto.CodesMrStockMktExp = CustomConvert.DataSetToList<CodesMarginRequestDto>(dataset.Tables[1]).ToList();
            resDto.CodesMrReqProfession = CustomConvert.DataSetToList<CodesMarginRequestDto>(dataset.Tables[2]).ToList();
            resDto.CodesMrPerceptionNetWorth = CustomConvert.DataSetToList<CodesMarginRequestDto>(dataset.Tables[3]).ToList();
            var data = CustomConvert.DataSetToList<AllCodesMarginDto>(dataset.Tables[4]).FirstOrDefault();
            resDto.Education = data.Education;
            resDto.TargetNetWorth = data.TargetNetWorth;
            resDto.SMExperiance = data.SMExperiance;
            resDto.Profession = data.Profession;
            resDto.eduScore = data.eduScore;
            resDto.smEScore = data.smEScore;
            resDto.professionScore = data.professionScore; 
            resDto.netWorthScore = data.netWorthScore;
            resDto.TotalScore = data.TotalScore;

            int i = 0;
            foreach (var item in resDto.CodesMrReqEducation)
            {
               
                var _item = new CodesMarginJson();
                _item.Category =  "Educational background";
                _item.Details = item.typeValue;
                _item.Score = item.Score.ToString();
                _item.Mark = data.eduScore.ToString();
                _item.Remark = data.TargetNetWorth;
                _item.TotalScore = data.TotalScore.ToString();
                _item.RowSpan = i ==0 ? resDto.CodesMrReqEducation.Count() : 0;
                codesMarginJsons.Add(_item);
                i++;
            }

            i = 0;
            foreach (var item in resDto.CodesMrStockMktExp)
            {
               

                var _item = new CodesMarginJson();
                _item.Category = "Stock market experience";
                _item.Details = item.typeValue;
                _item.Score = item.Score.ToString();
                _item.Mark = data.smEScore.ToString();
                _item.Remark = data.SMExperiance;
                _item.TotalScore = data.TotalScore.ToString();
                _item.RowSpan = i == 0 ? resDto.CodesMrStockMktExp.Count() : 0;
                codesMarginJsons.Add(_item);
                i++;
            }

            i = 0;
            foreach (var item in resDto.CodesMrReqProfession)
            {
                
                var _item = new CodesMarginJson();
                _item.Category = "Profession";
                _item.Details = item.typeValue;
                _item.Score = item.Score.ToString();
                _item.Mark = data.professionScore.ToString();
                _item.Remark = data.Profession;
                _item.TotalScore = data.TotalScore.ToString();
                _item.RowSpan = i == 0 ? resDto.CodesMrReqProfession.Count() : 0;
                codesMarginJsons.Add(_item);
                i++;
            }

            i = 0;
            foreach (var item in resDto.CodesMrPerceptionNetWorth)
            {
             
                var _item = new CodesMarginJson();
                _item.Category = "Perception of net worth";
                _item.Details = item.typeValue;
                _item.Score = item.Score.ToString();
                _item.Mark = data.netWorthScore.ToString();
                _item.Remark = data.TargetNetWorth;
                _item.TotalScore = data.TotalScore.ToString();
                _item.RowSpan = i == 0 ? resDto.CodesMrPerceptionNetWorth.Count() : 0;
                codesMarginJsons.Add(_item);
                i++;
            }


            return codesMarginJsons;
        }

    }
}
