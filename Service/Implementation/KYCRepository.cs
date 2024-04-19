using Dapper;
using Model.DTOs.Allotment;
using Model.DTOs.Approval;
using Model.DTOs.InsurancePremium;
using Model.DTOs.KYC;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.DTOs.KYC.CompleteKYCDto;

namespace Service.Implementation
{
    public class KYCRepository : IKYCRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public KYCRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }


        #region IDLC-SL

        public async Task<KYCDto> KYCAccountInfoSL(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ContractID = ContractID, ProductID = ProductID };
            var data = await _dbCommonOperation.ReadSingleTable<KYCDto>("CM_GetKYCInfoSL", values);
            return data.FirstOrDefault();
        }

        public async Task<List<PendingKYCDto>> PendingKYCSL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<PendingKYCDto>("CM_GetPendingKYCSL", values);
        }

        public async Task<List<CompleteKYCDto>> CompleteKYCSL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<CompleteKYCDto>("CM_GetCompleteKYCSL", values);
        }

        public async Task<List<ReviewKYCDto>> ReviewComScreenKYCSL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ReviewKYCDto>("CM_ReviewComScrApprovalListKYCSL", values);
        }

        public async Task<List<ApprovalKYCDto>> ListApprovalKYCSL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ApprovalKYCDto>("CM_GetApprovalListKYCSL", values);
        }

        public async Task<List<CodesKYCDto>> CodesKYC(int CompanyID, int BranchID, string UserName, string TypeName, string TypeID)
        {
            var values = new { TypeName = TypeName , TypeID = TypeID };
            return await _dbCommonOperation.ReadSingleTable<CodesKYCDto>("CodesKYC", values);
        }

        public async Task<string> InsertUpdateKYCSL(int CompanyID, int BranchID, string userName, KYCDto entry, string ActionType)
        {
            try
            {
                string sp = "CM_InsertUpdateKYCSL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@SLAgrKYCID", entry.SLAgrKYCID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@SourceOfFund", entry.SourceOfFund);
                SpParameters.Add("@SOFJustification", entry.SOFJustification);
                SpParameters.Add("@OwnerDetail", entry.OwnerDetail);
                SpParameters.Add("@ProfessionOrBusinessType", entry.ProfessionOrBusinessType);
                SpParameters.Add("@ScoreProforBT", entry.ScoreProforBT);
                SpParameters.Add("@NetWorth", entry.NetWorth);
                SpParameters.Add("@ScoreNetworth", entry.ScoreNetworth);
                SpParameters.Add("@ACOpeningMode", entry.ACOpeningMode);
                SpParameters.Add("@ScoreACOpeningMode", entry.ScoreACOpeningMode);
                SpParameters.Add("@ExptValMonthlyTransaction", entry.ExptValMonthlyTransaction);
                SpParameters.Add("@ScoreExptValMT", entry.ScoreExptValMT);
                SpParameters.Add("@ExptNoOfMonthlyTransaction", entry.ExptNoOfMonthlyTransaction);
                SpParameters.Add("@ScoreExptNumMT", entry.ScoreExptNumMT);
                SpParameters.Add("@ExptValCashMT", entry.ExptValCashMT);
                SpParameters.Add("@ExptValMCT", entry.ExptValMCT);
                SpParameters.Add("@ScoreExptValMCT", entry.ScoreExptValMCT); 
                SpParameters.Add("@ExpNumMCT", entry.ExpNumMCT);
                SpParameters.Add("@ScoreExpNumMCT", entry.ScoreExpNumMCT);
                SpParameters.Add("@TPSNumOfMDT", entry.TPSNumOfMDT);
                SpParameters.Add("@TPSNumOfMWT", entry.TPSNumOfMWT);
                SpParameters.Add("@TPSMaxSizeOfDT", entry.TPSMaxSizeOfDT);
                SpParameters.Add("@TPSMaxSizeOfWT", entry.TPSMaxSizeOfWT);
                SpParameters.Add("@ORARiskLevel", entry.ORARiskLevel);
                SpParameters.Add("@ORARiskRating", entry.ORARiskRating);
                SpParameters.Add("@ORADueDiligence", entry.ORADueDiligence);
                SpParameters.Add("@TotalScore", entry.TotalScore);
                SpParameters.Add("@Comments", entry.Comments);
                SpParameters.Add("@IsCustomerAddressVerified", entry.IsCustomerAddressVerified);
                SpParameters.Add("@AddressVerificationNotes", entry.AddressVerificationNotes);
                SpParameters.Add("@IsPEP", entry.IsPEP);
                SpParameters.Add("@PEPIsApprovalTaken", entry.ScoreExptNumMT);
                SpParameters.Add("@PEPSourceOfWealth", entry.PEPSourceOfWealth);
                SpParameters.Add("@PEPSOWComments", entry.PEPSOWComments);
                SpParameters.Add("@IsFtoFInterviewTaken", entry.IsFtoFInterviewTaken);
                SpParameters.Add("@TypeOfVisa", entry.TypeOfVisa);
                SpParameters.Add("@RefreeID", entry.RefreeID);
                SpParameters.Add("@HasDirectorshipOfCompany", entry.HasDirectorshipOfCompany.GetValueOrDefault());
                SpParameters.Add("@CompanyNameOrRemarks", entry.CompanyNameOrRemarks);
                SpParameters.Add("@ActionType", ActionType);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> KYCApproval(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@AgrKYCIDs", approvalDto.AgrKYCIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@UserType", approvalDto.UserType);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveKYC", SpParameters);
        }
        #endregion IDLC-SL

        #region IDLC-IL
        public async Task<List<PendingKYCDto>> PendingKYCIL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<PendingKYCDto>("CM_GetPendingKYCIL", values);

        }

        public async Task<List<CompleteKYCDto>> CompleteKYCIL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<CompleteKYCDto>("CM_GetCompleteKYCIL", values);
        }

        public async Task<string> InsertUpdateKYCIL(int CompanyID, int BranchID, string userName, KYCILDto entry, string ActionType)
        {
            try
            {
                string sp = "CM_InsertUpdateKYCIL";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@ILAgrKYCID", entry.ILAgrKYCID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@AddressVerifiedByID", entry.AddressVerifiedByID);
                SpParameters.Add("@IsHighRiskBT", entry.IsHighRiskBT);
                SpParameters.Add("@SourceOfFundID", entry.SourceOfFundID);
                SpParameters.Add("@DepositMatchWithProfile", entry.DepositMatchWithProfile);
                SpParameters.Add("@SourceOfAddFund", entry.SourceOfAddFund);              
                SpParameters.Add("@HowSOFVerified", entry.HowSOFVerified);
                SpParameters.Add("@IsPEP", entry.IsPEP);
                SpParameters.Add("@RiskGrading", entry.RiskGrading);
                SpParameters.Add("@ApplicantType", entry.ApplicantType);
                SpParameters.Add("@ActionType", ActionType);
                SpParameters.Add("@IdenDocTypeID", entry.IdenDocTypeID);
                SpParameters.Add("@OccDocID", entry.OccDocID);
                SpParameters.Add("@BusinessTypeID", entry.BusinessTypeID);
                SpParameters.Add("@VatRegistrationNo", entry.VatRegistrationNo);
                SpParameters.Add("@CommentsOnClientProfile", entry.CommentsOnClientProfile);
                SpParameters.Add("@ShareholderInformation", entry.ShareholderInformation);
                SpParameters.Add("@Comments", entry.Comments);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<KYCILDto> KYCAccountInfoIL(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ContractID = ContractID, ProductID = ProductID };
            var data = await _dbCommonOperation.ReadSingleTable<KYCILDto>("CM_GetKYCInfoIL", values);
            return data.FirstOrDefault();
        }

        public async Task<List<ApprovalILKYCDto>> ListApprovalKYCIL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ApprovalILKYCDto>("CM_GetApprovalListKYCIL", values);
        }

        public async Task<string> KYCApprovalIL(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@AgrKYCIDs", approvalDto.AgrKYCIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@UserType", approvalDto.UserType);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveKYCIL", SpParameters);
        }

        public async Task<List<ReviewILKYCDto>> ReviewComScreenKYCIL(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ReviewILKYCDto>("CM_ReviewComScrApprovalListKYCIL", values);
        }

        #endregion IDLC-IL

        #region IDLC-AML
        public async Task<List<PendingKYCDto>> PendingKYCAML(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<PendingKYCDto>("CM_GetPendingKYCAML", values);

        }

        public async Task<List<CompleteKYCDto>> CompleteKYCAML(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<CompleteKYCDto>("CM_GetCompleteKYCAML", values);
        }

        public async Task<List<CompleteKYCDto>> AllKYCAML(int CompanyID, int BranchID, string UserName, string Status)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, Status = Status };
            return await _dbCommonOperation.ReadSingleTable<CompleteKYCDto>("CM_GetAllKYCAML", values);
        }

        public async Task<string> InsertUpdateKYCAML(int CompanyID, int BranchID, string userName, KYCAMLDto entry, string ActionType)
        {
            try
            {
                string sp = "CM_InsertUpdateKYCAML";

                #region Insert New Data

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@AMLAgrKYCID", entry.AMLAgrKYCID);
                SpParameters.Add("@ContractID", entry.ContractID);
                SpParameters.Add("@AddressVerifiedByID", entry.AddressVerifiedByID);
                SpParameters.Add("@IsHighRiskBT", entry.IsHighRiskBT);
                SpParameters.Add("@SourceOfFundID", entry.SourceOfFundID);
                SpParameters.Add("@DepositMatchWithProfile", entry.DepositMatchWithProfile);
                SpParameters.Add("@SourceOfAddFund", entry.SourceOfAddFund);
                SpParameters.Add("@HowSOFVerified", entry.HowSOFVerified);
                SpParameters.Add("@IsPEP", entry.IsPEP);
                SpParameters.Add("@RiskGrading", entry.RiskGrading);
                SpParameters.Add("@ApplicantType", entry.ApplicantType);
                SpParameters.Add("@ActionType", ActionType);
                SpParameters.Add("@IdenDocTypeID", entry.IdenDocTypeID);
                SpParameters.Add("@OccDocID", entry.OccDocID);
                SpParameters.Add("@BusinessTypeID", entry.BusinessTypeID);
                SpParameters.Add("@VatRegistrationNo", entry.VatRegistrationNo);
                SpParameters.Add("@CommentsOnClientProfile", entry.CommentsOnClientProfile);
                SpParameters.Add("@ShareholderInformation", entry.ShareholderInformation);
                SpParameters.Add("@Comments", entry.Comments);
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                return await _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);

                #endregion

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<KYCAMLDto> KYCAccountInfoAML(int CompanyID, int BranchID, string UserName, int ContractID, int ProductID)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID, ContractID = ContractID, ProductID = ProductID };
            var data = await _dbCommonOperation.ReadSingleTable<KYCAMLDto>("CM_GetKYCInfoAML", values);
            return data.FirstOrDefault();
        }

        public async Task<List<ApprovalAMLKYCDto>> ListApprovalKYCAML(int CompanyID, int BranchID, string UserName)
        {
            var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
            return await _dbCommonOperation.ReadSingleTable<ApprovalAMLKYCDto>("CM_GetApprovalListKYCAML", values);
        }

        public async Task<string> KYCApprovalAML(string userName, int CompanyID, int branchID, KYCApprovalDto approvalDto)
        {
            DynamicParameters SpParameters = new DynamicParameters();

            SpParameters.Add("@userName", userName);
            SpParameters.Add("@CompanyID", CompanyID);
            SpParameters.Add("@BranchID", branchID);
            SpParameters.Add("@AgrKYCIDs", approvalDto.AgrKYCIDs);
            SpParameters.Add("@IsApproved", approvalDto.IsApproved);
            SpParameters.Add("@ApprovalRemark", approvalDto.ApprovalRemark);
            SpParameters.Add("@UserType", approvalDto.UserType);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveKYCAML", SpParameters);
        }

        //public async Task<List<ReviewAMLKYCDto>> ReviewComScreenKYCAML(int CompanyID, int BranchID, string UserName)
        //{
        //    var values = new { UserName = UserName, CompanyID = CompanyID, BranchID = BranchID };
        //    return await _dbCommonOperation.ReadSingleTable<ReviewAMLKYCDto>("CM_ReviewComScrApprovalListKYCAML", values);
        //}

        #endregion IDLC-AML

    }
}
