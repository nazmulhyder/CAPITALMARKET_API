using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.UpdateLog
{
    public class UpdateLog
    {
        public Nullable<int> UpdateLogID { get; set; }

        public string UpdReferenceNo { get; set; }
        public Nullable<int> UpdateUnitID { get; set; }
        public Nullable<int> MODNo { get; set; }
        public string Maker { get; set; }
        public Nullable<DateTime> MakeDate { get; set; }

        public Nullable<int> ApprovalReqSetID { get; set; }
        public string DataReference { get; set; }
        public Nullable<int> DataKeyID { get; set; }
    }
    public class UpdateLogDetailDto
    {

        public Nullable<int> UpdateLogID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string PrevContent { get; set; }
        public string UpdatedContent { get; set; }

        public Nullable<int> PKID { get; set; }

    }
    public class UpdateLogPresentDetailDto
    {

        public Nullable<int> UpdateLogID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string PrevContent { get; set; }
        public string UpdatedContent { get; set; }

        public Nullable<int> PKID { get; set; }

        public string UpdateUnitName { get; set; }
        public string TableDisplayName { get; set; }
        public string ColumnDisplayName { get; set; }

    }

    public class UpdateLogMasterDto
    {
        public Nullable<int> IndexID { get; set; }

        public Nullable<int> MODNo { get; set; }

        public Nullable<int> UpdateLogID { get; set; }
        public string UpdReferenceNo { get; set; }
        public Nullable<int> ApprovalReqSetID { get; set; }
        public string DataReference { get; set; }
        public string UpdateUnitName { get; set; }

        public Nullable<DateTime> MakeDate { get; set; }
        public string Maker { get; set; }
        public string ApprovalStatus { get; set; }
        public string Approver { get; set; }
        public Nullable<DateTime> ApprovalDate { get; set; }
    }
}
