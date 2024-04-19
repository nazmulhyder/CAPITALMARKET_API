using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SellSurrender
{
    public class SS_MinimumUnitSetupDto
    {
		public Nullable<int> SellMFUnitTransRestrictionID { get; set; }

		public Nullable<int> SurrenderMFUnitTransRestrictionID { get; set; }
		public Nullable<int> FundID { get; set; }
		public string? AccountType { get; set; }
		public Nullable<int> MinimumUnit { get; set; }
		public Nullable<int> MinimumSurrenderUnit { get; set; }

	}

    public class SS_UnitIssueForSaleDto
    {
        public int MFUnitInventoryID { get; set; }
        public int? FundID { get; set; }
        public int? IssuedUnit { get; set; }
        public int? AllocatedUnit { get; set; }
        public string? Status { get; set; }
        public string? Maker { get; set; }
        public string? MakeDate { get; set; }
    }
}
