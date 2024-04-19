using Dapper;
using Microsoft.AspNetCore.Http;
using Model.DTOs.FDR;
using Model.DTOs.FundCollection;
using Model.DTOs.InsurancePremium;
using Newtonsoft.Json;
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
    public class InsurancePremiumRepository : IInsurancePremiumRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public InsurancePremiumRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }

        public async Task<List<InsurancePremiumCollectionDto>> GetInsurancePremiumCollection(int CompanyID, int BranchID, string UserName, int FundID, string InstallmentDate)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, FundID = FundID, InstallmentDate = Utility.DatetimeFormatter.DateFormat(InstallmentDate) };
            return await _dbCommonOperation.ReadSingleTable<InsurancePremiumCollectionDto>("CM_GetInsrurancePremiumCollectionAML", values);
        }

        public async Task<object> GenerateDDIFileAML(List<InsurancePremiumCollectionDto> data, int BanckAccountID, int CompanyID, int BranchID, string UserName)
        {
            string InstallmentIDs = string.Empty;
            foreach (InsurancePremiumCollectionDto schedule in data)
                InstallmentIDs = InstallmentIDs + schedule.InstallmentID.ToString() + ",";

            SqlParameter[] sqlParams = new SqlParameter[5];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@BankAccountID", BanckAccountID);
            sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[3] = new SqlParameter("@BranchID", BranchID);
            sqlParams[4] = new SqlParameter("@InstallmentIDs", InstallmentIDs);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[ListDDIFileContentAML]", sqlParams);

            DDIFileheadTagDto HeadTag = CustomConvert.DataSetToList<DDIFileheadTagDto>(DataSets.Tables[0]).FirstOrDefault();

            List<DDIFileContentTagDto> Trnasactions = CustomConvert.DataSetToList<DDIFileContentTagDto>(DataSets.Tables[1]);

            DDIFileTailTagDto TailTag = CustomConvert.DataSetToList<DDIFileTailTagDto>(DataSets.Tables[2]).FirstOrDefault();

            StringBuilder OtherBankFileContent = new StringBuilder();
            StringBuilder SameBankFileContent = new StringBuilder();


            // txt file name
            string DateTimeName = DateTime.Now.ToString("ddmmyyhhmm");

            string SameBankDDIFileName = HeadTag.OrganizationName + "_" + "SB_" + DateTimeName + ".txt";
            string OtherBankDDIFileName = HeadTag.OrganizationName + "_" + "OB_" + DateTimeName + ".txt";

            //OTHER BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == false).Count() > 0)
            {
                OtherBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                OtherBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == false))
                {
                    OtherBankFileContent.Append(item.TransactionType + "," + item.RoutingNo + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    OtherBankFileContent.Append(Environment.NewLine);
                }

                OtherBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");


                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + OtherBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }

            //SAME BANK ACCOUNT DDI
            if (Trnasactions.Where(t => t.IsSameBank == true).Count() > 0)
            {
                SameBankFileContent.Append(HeadTag.HeadTag + "," + HeadTag.HeadNumber + "," + HeadTag.AccountNumber);
                SameBankFileContent.Append(Environment.NewLine);

                foreach (var item in Trnasactions.Where(t => t.IsSameBank == true))
                {
                    SameBankFileContent.Append(item.TransactionType + "," + item.BankCode + "," + "," + item.BankAccountNumber + ","
                        + item.MemberName + "," + item.InstrumentNumber + "," + item.Amount + "," + item.InstrumentDate + "," + item.CollectionType + ","
                        + ",,,,,,,,," + item.InstrumentNumber);
                    SameBankFileContent.Append(Environment.NewLine);
                }

                SameBankFileContent.Append(TailTag.TailTag + "," + TailTag.TransactionCount + "," + "0.0" + "," + TailTag.TotalAmount + "," + "0.0");

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"\ExportedFiles\" + SameBankDDIFileName))
                {
                    file.WriteLine(OtherBankFileContent.ToString()); // "sb" is the StringBuilder
                }
            }


            return new
            {
                SameBankFileName = SameBankDDIFileName,
                SamneBankFileContent = SameBankFileContent.ToString(),
                OtherBankFileName = OtherBankDDIFileName,
                OtherBankFileContent = OtherBankFileContent.ToString()
            };
        }

        public async Task<object> DDIFileUploadAML(IFormCollection data, string UserName)
        {
            try
            {
                List<DDIFileTransactionDto> TransactionList = JsonConvert.DeserializeObject<List<DDIFileTransactionDto>>(data["Transaction"]);
                //List<DDIFileReturnDto> ReturnList = JsonConvert.DeserializeObject<List<DDIFileReturnDto>>(data["Return"]);

                string FileName = data["FileName"];
                string ExecutionType = data["ExecutionType"];
                int FileSize = Convert.ToInt32(data["FileSize"]);
                string FileExtension = Path.GetExtension(FileName);
                int CompanyID = Convert.ToInt32(data["CompanyID"]);
                int BranchID = Convert.ToInt32(data["BranchID"]);
                int BankAccountID = Convert.ToInt32(data["BankAccountID"]);

                SqlParameter[] sqlParams = new SqlParameter[10];

                sqlParams[0] = new SqlParameter("@UserName", UserName);
                sqlParams[1] = new SqlParameter("@BankAccountID", BankAccountID);
                sqlParams[2] = new SqlParameter("@CompanyID", CompanyID);
                sqlParams[3] = new SqlParameter("@BranchID", BranchID);
                sqlParams[4] = new SqlParameter("@FileName", FileName);
                sqlParams[5] = new SqlParameter("@FileSize", FileSize);
                sqlParams[6] = new SqlParameter("@FileExtension", FileExtension);
                sqlParams[7] = new SqlParameter("@ExecutionType", ExecutionType);
                sqlParams[8] = new SqlParameter("@TransactionList", ListtoDataTableConverter.ToDataTable(TransactionList));
                //sqlParams[9] = new SqlParameter("@ReturnTransactionList", ListtoDataTableConverter.ToDataTable(ReturnList));

                var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileUploadValidationAML]", sqlParams);

                return DataSets.Tables[0].Rows[0][0].ToString();


            }
            catch (Exception ex) { return null; };
        }

        public async Task<object> DDIFileListAML(string UserName, int CompanyID, int BranchID, string Status)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];

            sqlParams[0] = new SqlParameter("@UserName", UserName);
            sqlParams[1] = new SqlParameter("@CompanyID", CompanyID);
            sqlParams[2] = new SqlParameter("@BranchID", BranchID);
            sqlParams[3] = new SqlParameter("@Status", Status);

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[DDIFileListAML]", sqlParams);

            List<DDIFileDto> FileList = new List<DDIFileDto>();

            FileList = CustomConvert.DataSetToList<DDIFileDto>(DataSets.Tables[0]);

            List<DDIFileTransactionTblDto> TransactionList = CustomConvert.DataSetToList<DDIFileTransactionTblDto>(DataSets.Tables[1]);

            List<DDIFileReturnTblDto> ReturnTransactionList = CustomConvert.DataSetToList<DDIFileReturnTblDto>(DataSets.Tables[2]);

            foreach (var item in FileList)
            {
                item.TransactionList = TransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
                item.ReturnList = ReturnTransactionList.Where(c => c.DDIFileID == item.DDIFileID).ToList();
            }

            return FileList;
        }

        public async Task<object> DDIFileApproveAML(string UserName, int CompanyID, int BranchID, int DDIFileID, string ApproveStatus)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@userName", UserName);
            SpParameters.Add("@DDIFileID", DDIFileID);
            SpParameters.Add("@ApproveStatus", ApproveStatus);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("ApproveDDIFileAML", SpParameters);
        }


    }
}
