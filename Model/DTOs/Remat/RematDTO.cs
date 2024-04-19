using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.Remat
{
    public class RematDTO
    {
        public int? RematInstrumentID { get; set; }
        public int? InstrumentLedgerID { get; set; }
        public int? ContractID { get; set; }
        public int? InstrumentID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public string? Remarks { get; set; }
    }

    public class RematListDTO
    {
        public int? SL { get; set; }
        public int? RematInstrumentID { get; set; }
        public int? RematFileDetailID { get; set; }
        public int? CDBLFileID { get; set; }
        public int? ProductID { get; set; }
        public String? ProductName { get; set; }
        public String? AccountNumber { get; set; }
        public String? BOID { get; set; }
        public String? MemberName { get; set; }
        public String? ISIN { get; set; }
        public String? ISINShortName { get; set; }
        public decimal? AcceptedQuantity { get; set; }
        public decimal? AveragePrice { get; set; }
        public String? ApprovalStatus { get; set; }
        public String? TransactionDate { get; set; }
        public String? Maker { get; set; }
        public String? MakeDate { get; set; }
        public String? EnableDisable { get; set; }
    }

    public class CMRematInstrumentUpdateMaster
    {
        public Nullable<decimal> AveragePrice { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public List<RematListDTO>? CMRematInstrumentUpdateList { get; set; }
    }

    public class CMApprovedRematInstrumentDTO
    {
        public String? Status { get; set; }
        public String? ApprovalRemark { get; set; }
        public List<RematListDTO>? CMRematInstrumentApproveList { get; set; }

    }
}
