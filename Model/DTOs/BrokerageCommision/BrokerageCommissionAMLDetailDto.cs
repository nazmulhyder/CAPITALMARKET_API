using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.BrokerageCommision
{
    public class BrokerageCommissionAMLBasicDto
    {
        public Nullable<int> ContractID { get; set; }
        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }
       
    }

    public class BrokerageCommissionAMLDetailDto
    {
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> AMCBrokerID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public string? BrokerName { get; set; }
        public string? TradingCode { get; set; }

        public List<BrokerageCommissionAMLDetailChargeDto>? ChargeList { get; set; }
    }
    public class BrokerageCommissionAMLDetailChargeDto
    {
        public Nullable<int> AgrBrokerChargeID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public Nullable<int> ProductAttributeID { get; set; }
        public Nullable<int> AttributeID { get; set; }
        public string? AttributeName { get; set; }
        public string? ValueType { get; set; }

        public Nullable<decimal> ChargeAmount { get; set; }
        public string? DerivedFrom { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public string? ApprovalStatus { get; set; }
        public Nullable<Boolean> HasSlab { get; set; }

        public Nullable<decimal> ProductSettingChargeAmount { get; set; }
        public Nullable<decimal> AccountGroupLevelChargeAmount { get; set; }
        public List<BrokerageCommissionAMLDetailChargeSlabDto> SlabList { get; set; }
    }

    public class BrokerageCommissionAMLDetailChargeDtoForUpdate
    {
        public Nullable<int> AgrBrokerChargeID { get; set; }
        public Nullable<int> ContractID { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public Nullable<int> ProductAttributeID { get; set; }
        public Nullable<int> AttributeID { get; set; }
        public string? AttributeName { get; set; }
        public string? ValueType { get; set; }

        public Nullable<decimal> ChargeAmount { get; set; }
        public string? DerivedFrom { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public string? ApprovalStatus { get; set; }
        public Nullable<Boolean> HasSlab { get; set; }
    }
    public class BrokerageCommissionAMLDetailChargeSlabDto
    {
        public Nullable<int> SlabID { get; set; }
        public Nullable<int> SlNo { get; set; }
        public Nullable<decimal> MinValue { get; set; }
        public Nullable<decimal> PrevMinValue { get; set; }
        public Nullable<decimal> MaxValue { get; set; }
        public Nullable<decimal> PrevMaxValue { get; set; }

        public Nullable<decimal> ChargeAmount { get; set; }
        public Nullable<decimal> PrevChargeAmount { get; set; }
        public Nullable<int> AgrBrokerChargeID { get; set; }
        public Nullable<Boolean> IsDeleted { get; set; }

    }



    public class UpdateBrokerageCommissionAMLDetailListDto
    {
        //public string AccountNumber { get; set; }
        //public int ContractID { get; set; }
        //public int ProductID { get; set; }
        //public string ProductName { get; set; }
        //public int BrokerID { get; set; }
        //public string BrokerName { get; set; }
        public int AgrBrokerChargeID { get; set; }
        public decimal ChargeAmount { get; set; }
        public string DerivedFrom { get; set; }
        //public int ProductAttributeID { get; set; }
        //public string InstrumentName { get; set; }
        public bool? HasSlab { get; set; } = true;
    }
}
