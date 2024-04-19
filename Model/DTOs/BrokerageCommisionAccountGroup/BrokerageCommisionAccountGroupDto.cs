using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BrokerageCommisionAccountGroup
{

    public class BrokerageCommisionAccountGroupApproveDto
    {
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int AccountGroupID { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? ApprovalFeedback { get; set; }
        //public string? UserName { get; set; }
    }
    public class BrokerageCommisionAccountGroupDto
    {
        
            public string? CompanyID { get; set; }

            public string? CompanyName { get; set; }
            public Nullable<int> AccountGroupID { get; set; }
            public string? AccountGroupName { get; set; }
            public string? ProductName { get; set; }
            public string? ApprovalStatus { get; set; }

            public Nullable<int> ApprovalReqSetID { get; set; }
            
            public Nullable<int> BCEquityIncomeChargeID { get; set; }
        public Nullable<decimal> BCEquityPrevIncome { get; set; }
        public Nullable<decimal> BCEquityIncome { get; set; }
            public Nullable<Boolean> BCEquityIncomeHasSlab { get; set; }
        public Nullable<Boolean> BCEquityIncomeSlabModifed { get; set; }
        public Nullable<int> BCEquityPayableChargeID { get; set; }
        public Nullable<decimal> BCEquityPrevPayable { get; set; }
        public Nullable<decimal> BCEquityPayable { get; set; }
            public Nullable<Boolean> BCEquityPayableHasSlab { get; set; }
        public Nullable<Boolean> BCEquityPayableSlabModifed { get; set; }

        public Nullable<int> BCGsecIncomeChargeID { get; set; }
            public Nullable<decimal> BCGsecIncome { get; set; }
        public Nullable<decimal> BCGsecPrevIncome { get; set; }
        public Nullable<Boolean> BCGsecIncomeHasSlab { get; set; }
        public Nullable<Boolean> BCGsecIncomeSlabModifed { get; set; }
        public Nullable<int> BCGsecPayableChargeID { get; set; }
            public Nullable<decimal> BCGsecPayable { get; set; }
        public Nullable<decimal> BCGsecPrevPayable { get; set; }
        public Nullable<Boolean> BCGsecPayableHasSlab { get; set; }
        public Nullable<Boolean> BCGsecPayableSlabModifed { get; set; }
        public Nullable<int> BCMFIncomeChargeID { get; set; }
            public Nullable<decimal> BCMFIncome { get; set; }
        public Nullable<decimal> BCMFPrevIncome { get; set; }
        public Nullable<Boolean> BCMFIncomeHasSlab { get; set; }
        public Nullable<Boolean> BCMFIncomeSlabModifed { get; set; }
        public Nullable<int> BCMFPayableChargeID { get; set; }
            public Nullable<decimal> BCMFPayable { get; set; }
        public Nullable<decimal> BCMFPrevPayable { get; set; }
        public Nullable<Boolean> BCMFPayableHasSlab { get; set; }
        public Nullable<Boolean> BCMFPayableSlabModifed { get; set; }
        public Nullable<int> BCBondIncomeChargeID { get; set; }
            public Nullable<decimal> BCBondIncome { get; set; }
        public Nullable<decimal> BCBondPrevIncome { get; set; }
        public Nullable<Boolean> BCBondIncomeHasSlab { get; set; }
        public Nullable<Boolean> BCBondIncomeSlabModifed { get; set; }
        public Nullable<int> BCBondPayableChargeID { get; set; }
        public Nullable<decimal> BCBondPrevPayable { get; set; }
        public Nullable<decimal> BCBondPayable { get; set; }
            public Nullable<Boolean> BCBondPayableHasSlab { get; set; }
        public Nullable<Boolean> BCBondPayableSlabModifed { get; set; }
        public Nullable<int> TotalRowCount { get; set; }
        

    }

    public class BrokerageCommisionAccountGroupItemDto
    {

        public Nullable<int> CompanyID { get; set; }
       
        public string? ApprovalStatus { get; set; }
        public Nullable<int> AccountGroupID { get; set; }

        public string? AccountGroupName { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? AccountInclusion { get; set; }
        public string? SelectionField { get; set; }

        public string? SelectionValue { get; set; }

        public List<BrokerageCommisionAccountGroupItemRateDto>? ChargeList { get; set; }
        
    }

    public class BrokerageCommisionAccountGroupItemRateUpdateDto
    {
        public Nullable<int> AccountGroupChargeID { get; set; }
        public Nullable<int> ProductAttributeID { get; set; }
        public string? AttributeName { get; set; }
        public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<Boolean> HasSlab { get; set; }

    }
    public class BrokerageCommisionAccountGroupItemRateDto
    {
        public Nullable<int> AccountGroupChargeID { get; set; }
        public Nullable<int> ProductAttributeID { get; set; }
        public string? AttributeName { get; set; }
        public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public Nullable<Boolean> HasSlab { get; set; }
        public Nullable<Boolean> IsSlabModified { get; set; }
        public List<BrokerageCommisionAccountGroupItemSlabDto>? SlabList { get; set; } = null;
    }

    public class BrokerageCommisionAccountGroupItemSlabDto
    {
        public Nullable<int> SlabID { get; set; }
        public Nullable<int> SlNo { get; set; }
        public Nullable<decimal> MinValue { get; set; }
        public Nullable<decimal> PrevMinValue { get; set; }
        public Nullable<decimal> MaxValue { get; set; }
        public Nullable<decimal> PrevMaxValue { get; set; }
        public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public Nullable<int> AccountGroupChargeID { get; set; }

    }
}
