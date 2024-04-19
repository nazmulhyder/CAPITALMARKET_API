using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTOs.TransactionDay;

namespace Service.Interface
{
    public interface ITransactionDayRepository
    {
        public Task<List<TransactionDayDto>> GetWeekDays(int CompanyID, int BranchID);
        public Task<string> UpdateTransactionDay(int CompanyID, int BranchID,List<UpdateTransactionDayDto> transactionDays, string userName);
        public Task<List<CalendarDayDto>> GetCalendarDaysList(int CompanyID, int BranchID,string Month, int Year);
        public Task<string> UpdateHoliDay(int CompanyID, int BranchID,List<CalendarDayDto> holiDays, string userName);

    }
}
