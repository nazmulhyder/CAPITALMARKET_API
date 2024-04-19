using Model.DTOs.InstrumentGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.TransactionDay
{
    public class TransactionDayDto
    {
        public int? DayID { get; set; }
        public string? Day { get; set; }
        public bool? IsWorkingDay { get; set; }
        public string? Maker { get; set; }
        public DateTime? MakeDate { get; set; }
       
    }

    public class UpdateTransactionDayDto
    {
        public int? DayID { get; set; }
        public string? Day { get; set; }
        public bool? IsWorkingDay { get; set; }
    }


    public class CalendarDayDto
    {
        public int? DateID { get; set; }

        public DateTime? Date { get; set; }
        public string? Day { get; set; }
        public Boolean? IsWorkingDay { get; set; }
        public int? isHoliday { get; set; }
        public string? HolidayPurpose { get; set; }

        public int? isExceptionday { get; set; }
        public string? ExceptiondayPurpose { get; set; }
        public Boolean? IsDepositAllowed { get; set; }
        public Boolean? IsWithdrawalAllowd { get; set; }
        public Boolean? IsDSETradeAllowed { get; set; }

        public Boolean? IsDSESettlementAllowed { get; set; }
        public Boolean? IsCSETradeAllowed { get; set; }
        public Boolean? IsCSESettlementAllowed { get; set; }
    }

    public class UpdateHoliDayDto
    {
        public int HolidayID { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public int DateID { get; set; }
    }

    public class UpdateExceptionDayDto
    {
        public int ExceptionDayID { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public Boolean? IsDepositAllowed { get; set; }
        public Boolean? IsWithdrawalAllowd { get; set; }
        public Boolean? IsDSETradeAllowed { get; set; }
        public Boolean? IsCSETradeAllowed { get; set; }
        public Boolean? IsDSESettlementAllowed { get; set; }      
        public Boolean? IsCSESettlementAllowed { get; set; }
        public int DateID { get; set; }
    }
}



