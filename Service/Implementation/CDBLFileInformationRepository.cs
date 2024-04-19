using Dapper;
using Microsoft.Extensions.Configuration;
using Model.DTOs;
using Model.DTOs.Charges;
using Model.DTOs.CorporateActionDividend;
using Model.DTOs.InstrumentGroup;
using Model.DTOs.IPO;
using Model.DTOs.TradeFileUpload;
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
    public class CDBLFileInformationRepository : ICDBLFileInformationRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public readonly IConfiguration _configuration;
        public CDBLFileInformationRepository(IDBCommonOpService dbCommonOperation, IConfiguration configuration)
        {
            _dbCommonOperation = dbCommonOperation;
            _configuration = configuration;
        }
        public async Task<object> getCDBLFileInformationList(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, string CDBLFilePath)
        {
            string path = _configuration["DocumentFilePath:CDBLFilePath"].ToString();

            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
            sqlParams[1] = new SqlParameter("@TransactionDateTo", TransactionDateTo);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@Maker", UserName);
            sqlParams[5] = new SqlParameter("@CDBLFilePath", path);

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CMCDBLFileInformation]", sqlParams);

            var InfoList = CustomConvert.DataSetToList<CDBLFileInformation>(dataset.Tables[0]).ToList();

            return new
            {
                FileInfoList = InfoList,
                CDBLFilePath = _configuration["DocumentFilePath:CDBLFilePath"].ToString()
            };
        }

        public async Task<object> ProcessCDBLFile(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data)
        {
           string CDBLFileInfoIDs = string.Empty;

            foreach (var item in data) CDBLFileInfoIDs = CDBLFileInfoIDs + "," + item.CDBLFileInfoID;

            SqlParameter[] sqlParams = new SqlParameter[7];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
            sqlParams[4] = new SqlParameter("@TransactionDateTo", TransactionDateTo);
            sqlParams[5] = new SqlParameter("@CDBLFilePath", _configuration["DocumentFilePath:CDBLFilePath"].ToString());
            sqlParams[6] = new SqlParameter("@CDBLFileList", ListtoDataTableConverter.ToDataTable(data));
            //sqlParams[6] = new SqlParameter("@CDBLFileInfoIDs", CDBLFileInfoIDs);

            //sqlParams[7] = new SqlParameter("@ReturnMessage", null);
            //sqlParams[7].Direction = ParameterDirection.Output;


            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("CMCDBLFileUpload", sqlParams);
           // string FileDescription = DataSets.Tables[0].Rows[0][0].ToString();

            return new
            {
                //FileDescription= FileDescription,
                CMCDBLValidationFileList= DataSets.Tables[0],
                CMCDBLFileMissingInstrumentList = DataSets.Tables[1],
                CMCDBLFileMissingAccountInfoList = DataSets.Tables[2],

            };

        }

        public Task<List<CDBLListDTO>> GetCDBLIPOFileDataList(int IPOInstrumentID, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                IPOInstrumentID = IPOInstrumentID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<CDBLListDTO>("[CMCDBLListIPOFiledata]", values);
        }

        public async Task<string> GetCDBLUpdateIPOFiledata(CMCDBLUpdateIPOFiledataMaster objCDBLUpdateIPOFiledata, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMCDBLUpdateIPOFiledata";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AveragePrice", objCDBLUpdateIPOFiledata.AveragePrice);
                SpParameters.Add("@CMCDBLListIPOFileDetails", ListtoDataTableConverter.ToDataTable(objCDBLUpdateIPOFiledata.CMCDBLUpdateList).AsTableValuedParameter("Type_CMCDBLListIPOFileDetails"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetCDBLFileProcess(CMCDBLUpdateIPOFiledataProcess objGetCDBLFileProcess, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMCDBLIPOFiledataProcess";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@IPOInstrumentID", objGetCDBLFileProcess.IPOInstrumentID);
                SpParameters.Add("@Status", objGetCDBLFileProcess.Status);
                SpParameters.Add("@ApprovalRemark", objGetCDBLFileProcess.ApprovalRemark);
                SpParameters.Add("@CMCDBLListIPOFileDetails", ListtoDataTableConverter.ToDataTable(objGetCDBLFileProcess.CMCDBLUpdateList).AsTableValuedParameter("Type_CMCDBLListIPOFileDetails"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<CDBLRightsListDTO>> GetCDBLRightsFileApprovalList(int RightSettingID, int CompanyID, int BranchID, string Maker)
        {
            var values = new
            {
                RightSettingID = RightSettingID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<CDBLRightsListDTO>("[CMCDBLRightsFileApprovalList]", values);
        }

        public async Task<string> GetCDBLUpdateRightsData(CMCDBLUpdateRightsMaster objCDBLUpdateRightsData, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMCDBLUpdateRightsData";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AveragePrice", objCDBLUpdateRightsData.AveragePrice);
                SpParameters.Add("@CMCDBLListRightsDetails", ListtoDataTableConverter.ToDataTable(objCDBLUpdateRightsData.CMCDBLUpdateList).AsTableValuedParameter("Type_CMCDBLListRightsDetails"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetCDBLRightsProcess(CMCDBLRightsProcess objCDBLRightsProcess, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "CMCDBLRightsProcess";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@RightSettingID", objCDBLRightsProcess.RightSettingID);
                SpParameters.Add("@Status", objCDBLRightsProcess.Status);
                SpParameters.Add("@ApprovalRemark", objCDBLRightsProcess.ApprovalRemark);
                SpParameters.Add("@CMCDBLListRightsDetails", ListtoDataTableConverter.ToDataTable(objCDBLRightsProcess.CMCDBLUpdateList).AsTableValuedParameter("Type_CMCDBLListRightsDetails"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Task<List<CDBLTransferListDTO>> getCMCDBLTransferTransmissionApprovalList(string TransactionDateFrom, string TransactionDateTo, string Status, int CompanyID, int BranchID, int CDBLFileInfoID,  string Maker)
        {
            var values = new
            {
                TransactionDateFrom = TransactionDateFrom,
                TransactionDateTo = TransactionDateTo,
                Status = Status,
                CDBLFileInfoID = CDBLFileInfoID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                Maker = Maker
            };
            return _dbCommonOperation.ReadSingleTable<CDBLTransferListDTO>("[CMCDBLTransferTransmissionApprovalList]", values);
        }

        public async Task<string> GetCDBLUpdatePriceTransferTransmissionInstrument(SLTransferTransmissionInstrumentMaster objCDBLUpdatePriceTransferTransmissionInstrument, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {


                string sp = "SLUpdatePriceTransferTransmissionInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Quantity", objCDBLUpdatePriceTransferTransmissionInstrument.Quantity); 
                SpParameters.Add("@AveragePrice", objCDBLUpdatePriceTransferTransmissionInstrument.AveragePrice);
                SpParameters.Add("@SLTransferTransmissionInstrument", ListtoDataTableConverter.ToDataTable(objCDBLUpdatePriceTransferTransmissionInstrument.CMCDBLTransferTransUpdateList).AsTableValuedParameter("Type_SLTransferTransmissionInstrument"));
                SpParameters.Add("@Maker", Maker);

                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> GetSLApprovedTransferTransmissionInstrument(SLApprovedTransferTransmissionInstrumentDTO objSLApprovedTransferTransmissionInstrument, int CompanyID, int BranchID, string Maker)
        {
            string result = string.Empty;

            try
            {

                string sp = "CMApprovedTransferTransmissionInstrument";

                DynamicParameters SpParameters = new DynamicParameters();

                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@Maker", Maker);
                SpParameters.Add("@Status", objSLApprovedTransferTransmissionInstrument.Status);
                SpParameters.Add("@ApprovalRemark", objSLApprovedTransferTransmissionInstrument.ApprovalRemark);
                SpParameters.Add("@SLTransferTransmissionInstrument", ListtoDataTableConverter.ToDataTable(objSLApprovedTransferTransmissionInstrument.CMCDBLTransferTransUpdateList).AsTableValuedParameter("Type_SLTransferTransmissionInstrument"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CDBLFileInformation PrevEquityInstrument = new CDBLFileInformation();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public async Task<string> TransferTransmissionInstrument(CMCDBLTransferTransmissionDTO? entryTransferTransmissionInstrument, int CompanyID, int BranchID, string UserName)
        {
            string result = string.Empty;

            try
            {



                string sp = "CMInsertupdateTransferTransmissionInstrument";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@TransTmisionInstrumentID", entryTransferTransmissionInstrument.TransTmisionInstrumentID);
                SpParameters.Add("@ProductID", entryTransferTransmissionInstrument.ProductID);
                SpParameters.Add("@ContractID", entryTransferTransmissionInstrument.ContractID);
                SpParameters.Add("@CDBLFileInfoID", entryTransferTransmissionInstrument.CDBLFileInfoID);
                SpParameters.Add("@InstrumentID", entryTransferTransmissionInstrument.InstrumentID);
                SpParameters.Add("@Quantity", entryTransferTransmissionInstrument.Quantity);
                SpParameters.Add("@Rate", entryTransferTransmissionInstrument.Rate);
                SpParameters.Add("@Remarks", entryTransferTransmissionInstrument.Remarks);
                SpParameters.Add("@Maker", UserName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);


                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                //GETTING PREVIOUS DATA BEFORE SAVE/UPDATE NEW DATA
                CDBLFileInformation PrevEquityInstrument = new CDBLFileInformation();

                result = await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public CDBLInstrumentListID InstrumentInfoListbyID(int InstrumentID, int ContractID, string TransactionDate, int CompanyID, int BranchID)
        {
            CDBLInstrumentListID Instrument = new CDBLInstrumentListID();

            SqlParameter[] sqlParams = new SqlParameter[]
           {
                new SqlParameter("@InstrumentID", InstrumentID),
                new SqlParameter("@ContractID", ContractID),
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@TransactionDate", TransactionDate)
           };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QueryInstrumentInfobyContractIDandInstrumentID]", sqlParams);

            Instrument = CustomConvert.DataSetToList<CDBLInstrumentListID>(DataSets.Tables[0]).First();

            return Instrument;
        }

        public async Task<object> ValidateCDBLFile(string UserName, int CompanyID, int BranchID, string TransactionDateFrom, string TransactionDateTo, List<CDBLFileInformation> data)
        {
            string path = _configuration["DocumentFilePath:CDBLFilePath"].ToString();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@TransactionDateFrom", TransactionDateFrom);
            sqlParams[1] = new SqlParameter("@TransactionDateTo", TransactionDateTo);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@Maker", UserName);
            

            var dataset = _dbCommonOperation.FindMultipleDataSetBySP("[CMCDBLFileInformation]", sqlParams);

            var InfoList = CustomConvert.DataSetToList<CDBLFileInformation>(dataset.Tables[0]).ToList();

            return new
            {
                FileInfoList = InfoList,
                CDBLFilePath = _configuration["DocumentFilePath:CDBLFilePath"].ToString()
            };
        }

        public Task<List<AMLBankAccountDTO>> GetAMLMFBankAccountList(int FundID, string UserName)
        {
            var values = new
            {
                FundID = FundID,
                UserName = UserName
            };
            return _dbCommonOperation.ReadSingleTable<AMLBankAccountDTO>("[AML_MFBankAccountList]", values);
        }

        public Task<List<InstrumentDto>> GetInstrumentInfobyTypeCodeList(int TypeCode, string AccountNumber, int ProductID, int CompanyID, int BranchID, string UserName)
        {
            var values = new
            {
                TypeCode = TypeCode,
                AccountNumber = AccountNumber,
                ProductID = ProductID,
                CompanyID = CompanyID,
                BranchID = BranchID,
                UserName = UserName
            };
            return _dbCommonOperation.ReadSingleTable<InstrumentDto>("[CM_QueryInstrumentInfobyTypeCode]", values);
        }
    }
}
