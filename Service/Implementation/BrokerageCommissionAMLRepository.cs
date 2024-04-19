using AutoMapper;
using Dapper;
using Model.DTOs.BrokerageCommision;
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
    public class BrokerageCommissionAMLRepository : IBrokerageCommissionAMLRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        private IMapper _mapper;
        public BrokerageCommissionAMLRepository(IDBCommonOpService dbCommonOperation, IMapper mapper)
        {
            _dbCommonOperation = dbCommonOperation;
            _mapper = mapper;
        }
        public async Task<object> GetAMLBrokerageCommisionByContractID(int ContractID, string UserName)
        {
           
            List<BrokerageCommissionAMLDetailDto> BrokerList = new List<BrokerageCommissionAMLDetailDto>();


            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@ContractID", ContractID),
                new SqlParameter("@UserName", UserName)
            };

            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("[dbo].[CM_QryBrokerageCommisionAML]", sqlParams);

            BrokerageCommissionAMLBasicDto BasicInfo = CustomConvert.DataSetToList<BrokerageCommissionAMLBasicDto>(DataSets.Tables[0]).FirstOrDefault();
            BrokerList = CustomConvert.DataSetToList<BrokerageCommissionAMLDetailDto>(DataSets.Tables[1]);
            List<BrokerageCommissionAMLDetailChargeDto > ALlCharge= CustomConvert.DataSetToList<BrokerageCommissionAMLDetailChargeDto>(DataSets.Tables[2]);
            List<BrokerageCommissionAMLDetailChargeSlabDto> AllAlabs = CustomConvert.DataSetToList<BrokerageCommissionAMLDetailChargeSlabDto>(DataSets.Tables[3]);

            foreach(var broker in BrokerList)
            {
                List<BrokerageCommissionAMLDetailChargeDto> ChargeList = ALlCharge.Where(c => c.BrokerID == broker.BrokerID).ToList();
                foreach(var charge in ChargeList) charge.SlabList = AllAlabs.Where(s=>s.AgrBrokerChargeID == charge.AgrBrokerChargeID).ToList();
                broker.ChargeList = ChargeList;
            }

            var result = new
            {
                BasicInfo = BasicInfo,
                BrokerList = BrokerList.Where(t => t.TradingCode != null && t.TradingCode != "").ToList()
            };

            return result;
        }


        public async Task<List<BrokerageCommissionAMLDto>> GetBrokerageCommisionList(int CompanyID, string UserName, string ApprovalStatus)
        {

            var values = new { CompanyID = CompanyID, UserName = UserName, ApprovalStatus = ApprovalStatus };
            //return await _dbCommonOperation.ReadSingleTable<BrokerageCommissionAMLDto>("[CM_ListBrokerageCommissionAML]", values);
            return await _dbCommonOperation.ReadSingleTable<BrokerageCommissionAMLDto>("[CM_ListBrokerageCommissionAML]", values);

        }

        public async Task<string> UpdateAMLBrokerageCommission(List<BrokerageCommissionAMLDetailDto> BrokerList, string userName, int CompanyID, int BranchID)
        {

            List<BrokerageCommissionAMLDetailChargeDto> ChargeList = new List<BrokerageCommissionAMLDetailChargeDto>();
            List<BrokerageCommissionAMLDetailChargeDtoForUpdate> ChargeListForUpdate = new List<BrokerageCommissionAMLDetailChargeDtoForUpdate>();
            List< BrokerageCommissionAMLDetailChargeSlabDto > SlabList = new List<BrokerageCommissionAMLDetailChargeSlabDto>();

            foreach(var item in BrokerList)
            {
                ChargeList.AddRange(item.ChargeList);
                foreach(var chargeItem in item.ChargeList) SlabList.AddRange(chargeItem.SlabList);
            }

            foreach(var item in ChargeList)
                ChargeListForUpdate.Add(JsonConvert.DeserializeObject<BrokerageCommissionAMLDetailChargeDtoForUpdate>(JsonConvert.SerializeObject(item)));

            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@UserName", userName, dbType: DbType.String);
            SpParameters.Add("@CompanyId", CompanyID, dbType: DbType.Int32);
            SpParameters.Add("@BranchId", BranchID, dbType: DbType.Int32);
            SpParameters.Add("@ChargeList", ListtoDataTableConverter.ToDataTable(ChargeListForUpdate).AsTableValuedParameter("Type_AMLBrokerageCommissionDetail"));
            SpParameters.Add("@SlabList", ListtoDataTableConverter.ToDataTable(SlabList).AsTableValuedParameter("Type_GenAgreementBrokerChargeSlab"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            var result = await _dbCommonOperation.InsertUpdateBySP("CM_UpdateBrokerageCommissionAML", SpParameters);

            return result;
          
        }

        public async Task<string> ApproveAMLBrokerageCommision(BrokerageCommisionApproveDto approval, string UserName)
        {
            DynamicParameters SpParameters = new DynamicParameters();
            SpParameters.Add("@ContractID", approval.ContractID);
            SpParameters.Add("@UserName", UserName);
            SpParameters.Add("@CompanyID", approval.CompanyID);
            SpParameters.Add("@BranchID", approval.BranchID);
            SpParameters.Add("@ApprovalStatus", approval.ApprovalStatus);
            SpParameters.Add("@ApprovalFeedback", approval.ApprovalFeedback);
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            return await _dbCommonOperation.InsertUpdateBySP("CM_ApproveBrokerageCommisionAML", SpParameters);
        }


    }
}
