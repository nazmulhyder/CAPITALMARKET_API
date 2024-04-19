using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Reports
{
    public class MenuWiseReportDto
    {
        public Nullable<int> MenuID { get; set; }

        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string ModuleID { get; set; }
        public string ParentMenuID { get; set; }
        public string MenuLevel { get; set; }

        public Nullable<int> OrderID { get; set; }

        public List<MenuWiseReportParameterDto> ParameterList { get; set; }
    }

    public class MenuWiseReportParameterDto
    {
        public Nullable<int> ReportParameterID { get; set; }

        public Nullable<int> MenuID { get; set; }
        public Nullable<int> ParameterTypeCode { get; set; }
        public string ParameterCaption { get; set; }
        public string ParameterType { get; set; }
        public string ParameterDataType { get; set; }

        public Nullable<int> ParameterOrder { get; set; }
        public string ParameterData { get; set; }
        public string DropdownData { get; set; }
    }

    public class TradeDataDto
    {
        
        public Nullable<int> ContractID { get; set; }

        public string? AccountNumber { get; set; }
        public string? MemberName { get; set; }
        public string? MemberType { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string? ProductName { get; set; }

        public string? ISIN { get; set; }
        public string? InstrumentName { get; set; }
        public Nullable<decimal> TradeQuantity { get; set; }
        public Nullable<decimal> CommissionRate { get; set; }
        public Nullable<decimal> BrokerageCommission { get; set; }

        public Nullable<decimal> TradeAmount { get; set; }
        public Nullable<decimal> Howla { get; set; }
        public Nullable<decimal> Laga { get; set; }
        public Nullable<decimal> AIT { get; set; }
        public Nullable<DateTime> TradeDate { get; set; }

        public Nullable<DateTime> ShareSettlementDate { get; set; }
        public Nullable<int> MarketID { get; set; }
        public string? MarketName { get; set; }
        public string? BankOrgName { get; set; }
        public string? BankAccountNumber { get; set; }

        public Nullable<int> ExchangeID { get; set; }
        public string? ExchangeName { get; set; }
        public string? ExchangeShortName { get; set; }
        public string? TradeType { get; set; }
    }

}
