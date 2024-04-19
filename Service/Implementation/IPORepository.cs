using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.ImportExportOmnibus;
using Model.DTOs.Instrument;
using Model.DTOs.IPO;
using Model.DTOs.NAVFileUpload;
using Newtonsoft.Json;
using Service.Interface;
using Utility;

namespace Service.Implementation
{
    public class IPORepository : IIPORepository
    {

        private readonly IDBCommonOpService _dbCommonOperation;
        private readonly IUpdateLogRepository _logOperation;
        private readonly IGlobalSettingService _globalSettingService;

        public IPORepository(IDBCommonOpService dbCommonOperation, IUpdateLogRepository logOperation, IGlobalSettingService globalSettingService)
        {
            _dbCommonOperation = dbCommonOperation;
            _logOperation = logOperation;
            _globalSettingService = globalSettingService;
        }



        public async Task<string> InsertIPO(IPODTO? EntryIPODTO, int CompanyID, int BranchID, string UserName)
        {/*
            IPOMasterDTO dataIPOMasterDTO = JsonConvert.DeserializeObject<List<IPOMasterDTO>>(data["dataIPOMasterDTO"]).FirstOrDefault();
            List<IPODetailsDTO> datalistd = JsonConvert.DeserializeObject<List<IPODetailsDTO>>(data["dataIPODetailsDTO"]);
           
            List<IPOMasterDTO> datalistm = new List<IPOMasterDTO>();
            datalistm.Add(dataIPOMasterDTO);
           

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);

            SpParameters.Add("@CMIPOMaster", ListtoDataTableConverter.ToDataTable(datalistm).AsTableValuedParameter("Type_CMIPOMaster"));

            SpParameters.Add("@CMIPODetails", ListtoDataTableConverter.ToDataTable(datalistd).AsTableValuedParameter("Type_CMIPODetails"));

            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
            CMInstrumentDTO PrevEquityInstrument = new CMInstrumentDTO();

            return await _dbCommonOperation.InsertUpdateBySP("CMAddUpdateIPOSetting", SpParameters);
        */

            string result = string.Empty;

            try
            {

                foreach (var item in EntryIPODTO.IPODetailsList)
                {
                    item.SubscriptionOpeningDate = Utility.DatetimeFormatter.DateFormat(item.SubscriptionOpeningDate);
                    item.SubscriptionClosingDate = Utility.DatetimeFormatter.DateFormat(item.SubscriptionClosingDate);
                    item.CutOffDate = Utility.DatetimeFormatter.DateFormat(item.CutOffDate);
                }


                string sp = "CMInsertUpdateIPOSetting";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@IPOInstrumentID", EntryIPODTO.IPOInstrumentID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@InstrumentID", EntryIPODTO.InstrumentID);
                SpParameters.Add("@ScriptCode", EntryIPODTO.SecurityCode);
                SpParameters.Add("@IPOType", EntryIPODTO.IPOType);
                //SpParameters.Add("@ServicesCharge", EntryIPODTO.ServiceCharges);
                //SpParameters.Add("@ServicesChargeMode", EntryIPODTO.ServiceChargeMode);



                SpParameters.Add("@CMIPODetails", ListtoDataTableConverter.ToDataTable(EntryIPODTO.IPODetailsList).AsTableValuedParameter("Type_CMIPODetails"));


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<IPODTO>> GetAllIPOInstrument(int CompanyID, int BranchID)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,

            };
            return _dbCommonOperation.ReadSingleTable<IPODTO>("[CM_ListIPOInstrument]", values);
        }


        public IPODTO GetIPOInstrumentById(int IPOInstrumentID)
        {
            IPODTO Instrument = new IPODTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@IPOInstrumentID",  IPOInstrumentID),
                //new SqlParameter("@CompanyID", CompanyID),
                //new SqlParameter("@BranchID", BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryIPOInstrument]", sqlParams);

            Instrument = CustomConvert.DataSetToList<IPODTO>(DataSets.Tables[0]).First();

            Instrument.IPODetailsList = CustomConvert.DataSetToList<IPODetailsDTO>(DataSets.Tables[1]);

            return Instrument;
        }
        public object GetIPOAccountInfo(int ProductId, string AccountNumber, int CompanyID, int BranchID)
        {
            IPOInvestorInfo Instrument = new IPOInvestorInfo();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@ProductID", ProductId),
                new SqlParameter("@AccountNumber", AccountNumber),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)

           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListInvestorInformationforIPOApplication]", sqlParams);

            Instrument = CustomConvert.DataSetToList<IPOInvestorInfo>(DataSets.Tables[0]).First();
            List<IPOInvestorIPOInfo> ObjIPOInvestorIPOInfo = Utility.CustomConvert.DataSetToList<IPOInvestorIPOInfo>(DataSets.Tables[1]);
            List<InstrumentDTO> ObjInstrumentInfo = Utility.CustomConvert.DataSetToList<InstrumentDTO>(DataSets.Tables[2]);
            List<UnlockInstrumentDTO> ObjUnLockInstrumentInfo = Utility.CustomConvert.DataSetToList<UnlockInstrumentDTO>(DataSets.Tables[3]);
            var result = new
            {
                Instrument = Instrument,
                ObjIPOInvestorIPOInfo = ObjIPOInvestorIPOInfo,
                ObjInstrumentInfo = ObjInstrumentInfo,
                ObjUnlockInstrumentInfo = ObjUnLockInstrumentInfo,
                ObjPhysicalInstrumentDeliveryInfo = DataSets.Tables[4],
                ObjUnitFundDeliveryInfo = DataSets.Tables[5],
                ObjRematInstrumentInfo = DataSets.Tables[6],

            };
            return result;
        }
        public IPOInstrumentInfo GetIPOInstrumentInfo(IPOInvestorInfo? EntryIPOInvestorInfo, int CompanyID, int BranchID)
        {
            IPOInstrumentInfo Instrument = new IPOInstrumentInfo();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@AccountType", EntryIPOInvestorInfo.InvestorCategory),
                new SqlParameter("@IpoInstrumentID", EntryIPOInvestorInfo.IpoInstrumentID),
                new SqlParameter("@ContractID", EntryIPOInvestorInfo.ContractID),
                new SqlParameter("@AppliedAmount", EntryIPOInvestorInfo.AppliedAmount),
                new SqlParameter("@CompanyID",CompanyID),
                new SqlParameter("@BranchID",BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListInstrumentInformationforIPOApplication]", sqlParams);

            Instrument = CustomConvert.DataSetToList<IPOInstrumentInfo>(DataSets.Tables[0]).First();



            return Instrument;
        }
        public async Task<string> InsertIPOApplication(IPOApplication? entryIPOApplication, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                //foreach (var item in EntryIPODTO.IPODetailsList)
                //{
                //    item.SubscriptionOpeningDate = Utility.DatetimeFormatter.DateFormat(item.SubscriptionOpeningDate);
                //    item.SubscriptionClosingDate = Utility.DatetimeFormatter.DateFormat(item.SubscriptionClosingDate);
                //    item.CutOffDate = Utility.DatetimeFormatter.DateFormat(item.CutOffDate);
                //}


                string sp = "CMInsertupdateIPOApplication";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@ProductID", entryIPOApplication.ProductID);
                SpParameters.Add("@ContractID", entryIPOApplication.ContractID);
                SpParameters.Add("@IPOInstrumentdetailId", entryIPOApplication.IPOInstrumentdetailId);
                SpParameters.Add("@IPOInstrumentID", entryIPOApplication.IPOInstrumentID);
                SpParameters.Add("@IPOApplicationID", entryIPOApplication.IPOApplicationID);
                SpParameters.Add("@NoOfshare", entryIPOApplication.NoOfshare);
                SpParameters.Add("@AppliedAmount", entryIPOApplication.AppliedAmount);
                SpParameters.Add("@IPOsourceID", 1);
                SpParameters.Add("@IPOChargeAmount", entryIPOApplication.ServiceCharge);
                SpParameters.Add("@TotalApplicationAmount", entryIPOApplication.TotalApplicationAmount);
                SpParameters.Add("@Rate", entryIPOApplication.Rate);
                SpParameters.Add("@MFBankAccountID", entryIPOApplication.MFBankAccountID);
                SpParameters.Add("@Remarks", entryIPOApplication.Remarks);
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<IPODTO>> IPOInstrumentListforApproval(int CompanyID)
        {
            var values = new
            {
                CompanyID = CompanyID

            };
            return _dbCommonOperation.ReadSingleTable<IPODTO>("[CM_IPOInstrumentApprovalList]", values);
        }
        public Task<List<IPOOrderApproved>> GetIPOApplicationOrderList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOOrderApproved>("[CMListIPOApplicationforApproval]", values);
        }


        public async Task<string> IPOApplicationApproved(IPOApproval data, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMApprovedIPOApplication";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@Status", data.Status);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@CMIPOApproval", ListtoDataTableConverter.ToDataTable(data.IPOOrderApprovedList).AsTableValuedParameter("Type_CMIPOApproval"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<string> IPOApplicationApprovedbyID(IPOApproval data, int IPOApplicationID, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMApprovedIPOApplicationbyID";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@Status", data.Status);
                SpParameters.Add("@ApprovalRemark", data.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOApplicationID", IPOApplicationID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<string> GetIPOInstrumentApproval(IPOInstrumentApproval objIPOInstrumentApproval, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "CM_ApproveIPOInstrument";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@IPOInstrumentid", objIPOInstrumentApproval.IPOInstrumentID);
                SpParameters.Add("@ApprovalRemark", objIPOInstrumentApproval.ApprovalRemark);
                SpParameters.Add("@IsApproved", objIPOInstrumentApproval.Status);
                SpParameters.Add("@UserName", UserName);


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public IPOApplicationListID IPOApplicationListbyID(int IPOApplicationID, int CompanyID, int BranchID)
        {
            IPOApplicationListID Instrument = new IPOApplicationListID();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@IPOApplicationID", IPOApplicationID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryIPOApplicationListbyID]", sqlParams);

            Instrument = CustomConvert.DataSetToList<IPOApplicationListID>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public Task<List<IPOBulkApproved>> GetIPOApplicationBulkList(int ProductID, int IPOInstrumentID, string Maker, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                ProductID = ProductID,
                Maker = Maker,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOBulkApproved>("[ListBulkInsertIPOApplication]", values);
        }

        public async Task<string> InsertIPOBulk(IPOBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertupdateBulkIPOApplication ";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@ProductID", entryIPOBulk.ProductID);
                SpParameters.Add("@IPOInstrumentID", entryIPOBulk.IPOInstrumentID);
                SpParameters.Add("@NoOfshare", 0);
                SpParameters.Add("@AppliedAmount", entryIPOBulk.AppliedAmount);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);

                SpParameters.Add("@CMIPOBulkDetails", ListtoDataTableConverter.ToDataTable(entryIPOBulk.IPOBulkInsertList).AsTableValuedParameter("Type_CMBulkIPODetails"));
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public async Task<string> InsertIPOResultFileText(IFormFile formdata, int IPOInstrumentID, int CompanyID, int BranchID, string UserName)
        {
            string FileContent = string.Empty;
            string Result = string.Empty;

            using (var reader = new StreamReader(formdata.OpenReadStream()))
            {
                FileContent = await reader.ReadToEndAsync();
            }

            if (FileContent == string.Empty) return await Task.FromResult("No content found in file");

            string[] FileLines = FileContent.Split("\n");


            List<IPOResultDTO> DataList = new List<IPOResultDTO>();

            foreach (string Line in FileLines.Where(l => l.Length > 10).ToList())
            {
                string[] Words = Line.Split("~");

                DataList.Add(new IPOResultDTO
                {
                    TrecNo = Words[0],
                    DPID = Words[1],
                    AccountNumber = Words[2],
                    AccountName = Words[3],
                    BOID = Words[4],
                    InvestorCategory = Words[5],
                    ScriptsName = Words[6],
                    NoofShare = Convert.ToDecimal(Words[7]),
                    Currency = Words[8],
                    AppliedAmount = Convert.ToDecimal(Words[9]),
                    AllotedShare = Convert.ToDecimal(Words[10]),
                    FinedAmount = Convert.ToDecimal(Words[11]),
                    RefundAmount = Convert.ToDecimal(Words[12]),
                    Remarks = Words[13],

                });
            }



            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@IPOInstrumentID", IPOInstrumentID);
            SpParameters.Add("@FileName", "");
            SpParameters.Add("@IPOResultData", ListtoDataTableConverter.ToDataTable(DataList).AsTableValuedParameter("Type_IPOResultData"));



            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CMInsertUpdateIPOResult]", SpParameters);

        }
        public Task<List<IPOListResultforAllocationRefund>> GetResultforAllocationRefundList(int IPOInstrumentID, string Maker, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInsturmentID = IPOInstrumentID,
                Maker = Maker,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOListResultforAllocationRefund>("[CMListIPOResultforAllocationRefund]", values);
        }

        public async Task<string> InsertIPOResult(IPOResultMaster entryIPOResult, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMIPOAllocationRefund";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@IPOInstrumentID", entryIPOResult.IPOInstrumentID);
                SpParameters.Add("@Status", entryIPOResult.Status);
                SpParameters.Add("@ApprovalRemark", entryIPOResult.ApprovalRemark);
                SpParameters.Add("IPOResultList", ListtoDataTableConverter.ToDataTable(entryIPOResult.IPOResultInsertList).AsTableValuedParameter("Type_IPOResultList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public Task<List<IPOReversalDTO>> GetIPOReversalList(int CompanyID, int BranchID, string UserName, string IPOType)
        {
            var values = new
            {
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName,
                IPOType = IPOType

            };
            return _dbCommonOperation.ReadSingleTable<IPOReversalDTO>("[IPOReversalApprovalList]", values);
        }

        public async Task<string> InsertReversalApproval(IPOReversalMaster entryIPOReversal, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "IPOReversalApproval";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@Status", entryIPOReversal.Status);
                SpParameters.Add("@ApprovalRemark", entryIPOReversal.ApprovalRemark);
                SpParameters.Add("@ipoReverseID", entryIPOReversal.IpoReverseID);

                //SpParameters.Add("IPOResultList", ListtoDataTableConverter.ToDataTable(entryIPOReversal.IPOReversalApproveList).AsTableValuedParameter("Type_IPOReversalApproval"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<IPOBookBuildingDTO>> GetIPOBookBuildingList(int IPOInstrumentID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,

            };
            return _dbCommonOperation.ReadSingleTable<IPOBookBuildingDTO>("[CM_ListBookBuildingApplication]", values);
        }
        public async Task<string> InsertApplicationSubscription(IPOBookBuildingAppSubscriptionIPO entryIPOAppSubscription, String Maker, int CompanyID, int BranchID)
        {
            string result = string.Empty;

            try
            {

                string sp = "CM_BookbuildingApplicationSubscripton";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@IPOApplicationId", entryIPOAppSubscription.IPOApplicationId);
                SpParameters.Add("@NoOfShare", entryIPOAppSubscription.NoOfshare);
                SpParameters.Add("@Rate", entryIPOAppSubscription.Rate);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public IPOBookBuildingAllotmentDTO GetIPOBookBuildingAllotmentInfo(int IPOInstrumentID, int ContractID, int CompanyID, int BranchID)
        {
            IPOBookBuildingAllotmentDTO Instrument = new IPOBookBuildingAllotmentDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@IPOInstrumentID",IPOInstrumentID),
                new SqlParameter("@ContractID",ContractID),
                new SqlParameter("@CompanyID",CompanyID),
                new SqlParameter("@BranchID",BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_IPOBookBuildingAllotment]", sqlParams);

            Instrument = CustomConvert.DataSetToList<IPOBookBuildingAllotmentDTO>(DataSets.Tables[0]).First();



            return Instrument;
        }

        public async Task<string> InsertApplicationAllotMent(IPOBookBuildingAllotmentInsDTO entryIPOBookBuildingAllotment, String Maker, int CompanyID, int BranchID)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMIPOinsertupdateSubscription";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@SubscriptionID", entryIPOBookBuildingAllotment.SubscriptionID);
                SpParameters.Add("@AppliedAmount", entryIPOBookBuildingAllotment.AppliedAmount);
                SpParameters.Add("@Quantity", entryIPOBookBuildingAllotment.Quantity);
                SpParameters.Add("@Rate", entryIPOBookBuildingAllotment.Rate);
                SpParameters.Add("@IPOApplicationId", entryIPOBookBuildingAllotment.IPOApplicationId);
                SpParameters.Add("@MFBankAccountID ", entryIPOBookBuildingAllotment.MFBankAccountID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<IPOBookBuildingAllotmentList>> GetIPOSubsCriptionforApprovalList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOBookBuildingAllotmentList>("[CMListIPOSubsCriptionforApproval]", values);
        }

        public SubscriptionListByIdDTO GetIPOSubscriptionListbyID(int SubscriptionID, int CompanyID, int BranchID)
        {
            SubscriptionListByIdDTO Instrument = new SubscriptionListByIdDTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@SubscriptionID", SubscriptionID),
                new SqlParameter("@CompanyID",CompanyID),
                new SqlParameter("BranchID",BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryIPOSubscriptionListbyID]", sqlParams);

            Instrument = CustomConvert.DataSetToList<SubscriptionListByIdDTO>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<string> ApproveBookBuildingRefundMaster(BookBuildingRefundMaster entryBookBuildingRefundMaster, string UserName, int CompanyID, int BranchID)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMIPOAllocationRefundBB";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@IPOInstrumentID", entryBookBuildingRefundMaster.IPOInstrumentID);
                SpParameters.Add("@Status", entryBookBuildingRefundMaster.Status);
                SpParameters.Add("@ApprovalRemark", entryBookBuildingRefundMaster.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOAllocationList", ListtoDataTableConverter.ToDataTable(entryBookBuildingRefundMaster.objIPOBookBuildingAllotmentList).AsTableValuedParameter("Type_IPOAllocationList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<AMLCollection>> GetAMLIPOCollectionList(int IPOInstrumentID,string IPOType, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                IPOType = IPOType,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<AMLCollection>("[AML_ListIPOCollection]", values);
        }

        public async Task<string> AMLCollectionApproveRequest(AMLCollectionMaster? AMLCollectionReq, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {


                string sp = "AML_ApprovedIPOCollection";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@IPOType", AMLCollectionReq.IPOType);
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AMLCollectionApproval", ListtoDataTableConverter.ToDataTable(AMLCollectionReq.ipoInstrumentCollectionList).AsTableValuedParameter("Type_AMLCollectionApproval"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<AMLCollectionApprovalList>> GetInstrumentCollectionlList(int IPOInsturmentID,string IPOType, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                IPOInsturmentID = IPOInsturmentID,
                IPOType = IPOType,
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<AMLCollectionApprovalList>("[AML_ListIPOCollectionApproval]", values);
        }

        public async Task<String> AMLIPOInstrumentApproved(AMLCollectionApproved AMLIPOInstrumentList, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "AML_IPOApprovalCollection";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOInstrumentID", AMLIPOInstrumentList.IPOInstrumentID);
                SpParameters.Add("@IPOType", AMLIPOInstrumentList.IPOType);
                SpParameters.Add("@Status", AMLIPOInstrumentList.Status);
                SpParameters.Add("@ApprovalRemark", AMLIPOInstrumentList.ApprovalRemark);
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@AMLIPOInstrumentApproved", ListtoDataTableConverter.ToDataTable(AMLIPOInstrumentList.AMLCollectionApprovedList).AsTableValuedParameter("Type_AMLIPOInstrumentApproved"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<object> IPOFileGenerate(int IPOInstrumentID, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                UserName = UserName,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            var datalist = _dbCommonOperation.ReadSingleTable<IPOFileGenerateIssuer>("[CM_IPOFileGenerateToIssuer]", values).Result.ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var item in datalist)
            {
                sb.Append(item.Code1 + "~" + item.Code2 + "~" + item.AccountNumber + "~" + item.MemberName + "~" + item.BOID + "~" + item.AccountType + "~" + item.Currency + "~" + item.AppliedAmount + "~" + item.CompanyShortName);
                sb.Append(Environment.NewLine);

            };
            return new
            {
                FileName = "IPO File.txt",
                FileContent = sb.ToString()

            };


        }

        //public Task<List<IPOInstrumentSetupList>> GetIPOInstrumentSetupInfo(int IPOInstrumentID, string IPOType, int CompanyID, int BranchID)
        //{
        //    var values = new
        //    {
        //        IPOInstrumentID = IPOInstrumentID,
        //        IPOType = IPOType,
        //        CompanyID = CompanyID,
        //        BranchID = BranchID
        //    };
        //    return _dbCommonOperation.ReadSingleTable<IPOInstrumentSetupList>("[CM_QueryIPOInstrumentSetup]", values);
        //}

        public IPODTO GetIPOInstrumentSetupInfo(int IPOInstrumentID, string IPOType, int CompanyID, int BranchID)
        {
            IPODTO Instrument = new IPODTO();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@IPOInstrumentID",  IPOInstrumentID),
               new SqlParameter("@IPOType", IPOType),
               new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryIPOInstrumentSetup]", sqlParams);


            Instrument = CustomConvert.DataSetToList<IPODTO>(DataSets.Tables[0]).FirstOrDefault();
            if (Instrument != null)
                Instrument.IPODetailsList = CustomConvert.DataSetToList<IPODetailsDTO>(DataSets.Tables[1]);

            return Instrument;
        }

        public Task<List<IPOOrderApproved>> GetIPOApplicationWaitingSubmissionList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOOrderApproved>("[CMListIPOSubmittedApplicationforApproval]", values);
        }

        public Task<List<IPOOrderApproved>> GetIPOSubmittedApplicationList(int IPOInstrumentID, string Status, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                Status = Status,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOOrderApproved>("[CMListIPOSubmittedApplications]", values);
        }

        public async Task<object> GetIPOApplicationFileValidation(string UserName, int CompanyID, int BranchID, List<IPOApplicationFileDetailDto> data)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            //sqlParams[3] = new SqlParameter("@InstrumentID", InstrumentID);
            sqlParams[3] = new SqlParameter("@FileDetails", ListtoDataTableConverter.ToDataTable(data));

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_ListIPOApplicationFileValidation]", sqlParams);

            return DataSets.Tables[0];
        }

        public async Task<object> SaveIPOApplicationFile(string UserName, int CompanyID, int BranchID, int InstrumentID, IFormCollection form)
        {
            List<IPOApplicationFileDetailDto> data = JsonConvert.DeserializeObject<List<IPOApplicationFileDetailDto>>(form["IpoApplicationFileData"]);

            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@FileName", form.Files[0].FileName.ToString());
            SpParameters.Add("@IPOInsSettingID", InstrumentID);
            SpParameters.Add("@FileDetails", ListtoDataTableConverter.ToDataTable(data).AsTableValuedParameter("Type_IPOApplicationFileDetail"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            return await _dbCommonOperation.InsertUpdateBySP("[dbo].[CM_InsertIPOApplicationFile]", SpParameters);
            
            //return DataSets.Tables[0].Rows[0][0].ToString();
        }

        public Task<List<IPOApplicationEligibleList>> GetIPOEligibleInvestorList(int IPOInsSettingID, string EnableDisable, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                IPOInsSettingID = IPOInsSettingID,
                EnableDisable = EnableDisable,
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName

            };
            return _dbCommonOperation.ReadSingleTable<IPOApplicationEligibleList>("[CM_ListEligibleInvestor]", values);
        }

        public async Task<string> InsertEligibleIPOBulk(IPOEligibleBulkInsertMaster entryIPOBulk, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMInsertupdateEligibleBulkIPOApplication";

                DynamicParameters SpParameters = new DynamicParameters();



                SpParameters.Add("@IPOInstrumentID", entryIPOBulk.IPOInstrumentID);

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);

                SpParameters.Add("@CMIPOBulkDetails", ListtoDataTableConverter.ToDataTable(entryIPOBulk.IPOBulkInsertList).AsTableValuedParameter("Type_CMEligibleIPODetails"));
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<IPOBookBuildingAllotmentList>> GetAMLIPOSubsCriptionList(int IPOInstrumentID, int CompanyID, int BranchID)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<IPOBookBuildingAllotmentList>("[AMListIPOSubsCriptionforApproval]", values);
        }

        public async Task<String> AMLIPOSubscriptionCollectionApproved(AMLCollectionApprovedDTO AMLIPOInstrumentList, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "AML_IPOApprovalCollection";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOInstrumentID", AMLIPOInstrumentList.rightInstSettingID);
                SpParameters.Add("@Status", AMLIPOInstrumentList.Status);
                SpParameters.Add("@ApprovalRemark", AMLIPOInstrumentList.ApprovalRemark);
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@AMLIPOInstrumentApproved", ListtoDataTableConverter.ToDataTable(AMLIPOInstrumentList.amlCorpActRightCollectionApprovedList).AsTableValuedParameter("Type_AMLIPOInstrumentApproved"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<ListTransferAmountDTO>> GetCMListTransferfundamountbyIPORights(string TransactionType, int CompanyID, int BranchID)
        {
            var values = new
            {
                TransactionType = TransactionType,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<ListTransferAmountDTO>("[CMListTransferfundamountbyIPORights]", values);
        }

        public Task<List<AMLBankAccountInfoDTO>> GetAMLBankAccountInformationByID(int MFBankAccountID, int CompanyID, int BranchID)
        {
            var values = new
            {
                MFBankAccountID = MFBankAccountID,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<AMLBankAccountInfoDTO>("[AML_BankAccountInformation]", values);
        }

        public Task<List<ListTransferDTO>> GetFundTransferToIssuerList(string TransactionType, int InstrumentID, int CompanyID, int BranchID)
        {
            var values = new
            {
                TransactionType = TransactionType,
                InstrumentID = InstrumentID,
                CompanyID = CompanyID,
                BranchID = BranchID
            };
            return _dbCommonOperation.ReadSingleTable<ListTransferDTO>("[CM_FundTransferToIssuerList]", values);
        }

        public async Task<String> SLILSaveFundTransferToIssuer(SLILFundTransferToIssuerDTO SLILFundTransfer, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "SLILSaveFundTransferToIssuer";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("InstrumentID", SLILFundTransfer.InstrumentID);
                SpParameters.Add("@IPOType", SLILFundTransfer.IPOType);
                SpParameters.Add("@DepositBankAccountID ", SLILFundTransfer.DepositBankAccountID);
                SpParameters.Add("@TotalApplicationAmount", SLILFundTransfer.TotalApplicationAmount);
                //SpParameters.Add("@ApprovalRemark", SLILFundTransferList.ApprovalRemark);
                SpParameters.Add("@Maker", Maker);

                //SpParameters.Add("@AMLIPOInstrumentApproved", ListtoDataTableConverter.ToDataTable(SLILFundTransferList.SLILFundTransferApprovedList).AsTableValuedParameter("Type_AMLIPOInstrumentApproved"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                //IPOOrderApproved PrevEquityInstrument = new IPOOrderApproved();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> ApproveBookBuildingRefund(BookBuildingRefundMaster entryBookBuildingRefundMaster, string UserName, int CompanyID, int BranchID)
        {
            string result = string.Empty;

            try
            {

                string sp = "IL_IPOAllocationRefundBB";

                DynamicParameters SpParameters = new DynamicParameters();


                SpParameters.Add("@UserName", UserName);
                SpParameters.Add("@IPOInstrumentID", entryBookBuildingRefundMaster.IPOInstrumentID);
                SpParameters.Add("@Status", entryBookBuildingRefundMaster.Status);
                SpParameters.Add("@ApprovalRemark", entryBookBuildingRefundMaster.ApprovalRemark);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOAllocationList", ListtoDataTableConverter.ToDataTable(entryBookBuildingRefundMaster.objIPOBookBuildingAllotmentList).AsTableValuedParameter("Type_IPOAllocationList"));

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                IPODTO PrevEquityInstrument = new IPODTO();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
    
}
