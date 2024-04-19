using Dapper;
using Model.DTOs.BrokerageCommisionAccountGroup;
using Model.DTOs.InstrumentGroup;
using Model.DTOs.TransactionDay;
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
    public class TransactionDayRepository : ITransactionDayRepository
    {
        public readonly IDBCommonOpService _dbCommonOperation;
        public TransactionDayRepository(IDBCommonOpService dbCommonOperation)
        {
            _dbCommonOperation = dbCommonOperation;
        }
       
        public Task<List<TransactionDayDto>> GetWeekDays(int CompanyID, int BranchID)
        {

            var values = new { CompanyID=CompanyID,BranchID=BranchID };
            return _dbCommonOperation.ReadSingleTable<TransactionDayDto>("dbo.QueryWeekDays",values);        
        }


        public Task<string> UpdateTransactionDay(int CompanyID, int BranchID,List<UpdateTransactionDayDto> transactionDays, string userName)
        {
            string sp = "UpdateWeekDay";

            DynamicParameters SpParameters = new DynamicParameters();
			SpParameters.Add("@CompanyID", CompanyID);
			SpParameters.Add("@BranchID", BranchID);
            SpParameters.Add("@UserName", userName);
            SpParameters.Add("@TransactionDays", ListtoDataTableConverter.ToDataTable(transactionDays).AsTableValuedParameter("Type_WeekDay"));
            SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            return _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
        }

       
        public Task<List<CalendarDayDto>> GetCalendarDaysList(int CompanyID, int BranchID,string Month, int Year)
        {
            SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CompanyID", CompanyID),
                new SqlParameter("@BranchID", BranchID),
                new SqlParameter("@Month", Month),
                new SqlParameter("@Year", Year),
               
            };
            var DataSets = _dbCommonOperation.FindMultipleDataSetBySP("ListCalendarDays", sqlParams);

            List<CalendarDayDto> days = CustomConvert.DataSetToList<CalendarDayDto>(DataSets.Tables[0]).ToList();

            return Task.FromResult(days);
        }

   
        public Task<string> UpdateHoliDay(int CompanyID, int BranchID, List<CalendarDayDto> holiDays, string userName)
        {

            List<UpdateHoliDayDto> updateHolidays = new List<UpdateHoliDayDto>();
            List<UpdateExceptionDayDto> updateEceptions = new List<UpdateExceptionDayDto>();
            foreach (var item in holiDays)
            {
                if (item.isHoliday == 1)
                    updateHolidays.Add
                     (
                       new UpdateHoliDayDto 
                       {
                           HolidayID = item.DateID.GetValueOrDefault(),
                           Date = item.Date.GetValueOrDefault(),
                           Purpose = item.HolidayPurpose,
                           DateID = item.DateID.GetValueOrDefault() 

                       }
                     );

                if (item.isExceptionday == 1) 
                    updateEceptions.Add(
                       new UpdateExceptionDayDto
                       {
                           ExceptionDayID = item.DateID.GetValueOrDefault(),
                           Date = item.Date.GetValueOrDefault(),
                           Purpose = item.ExceptiondayPurpose,
                           IsDepositAllowed = item.IsDepositAllowed.GetValueOrDefault(),
                           IsWithdrawalAllowd = item.IsWithdrawalAllowd.GetValueOrDefault(),
                           IsDSETradeAllowed = item.IsDSETradeAllowed.GetValueOrDefault(),
                           IsCSETradeAllowed = item.IsCSETradeAllowed.GetValueOrDefault(),
                           IsDSESettlementAllowed = item.IsDSESettlementAllowed.GetValueOrDefault(),
                           IsCSESettlementAllowed = item.IsCSESettlementAllowed.GetValueOrDefault(),
                           DateID = item.DateID.GetValueOrDefault()
                       }
                     );
            }


             Task<string> returnMsg = null;
            if (updateHolidays.Count()>0)
            {
                string sp = "UpdateHoliDay";

                DynamicParameters SpParameters = new DynamicParameters();
                SpParameters.Add("@CompanyID", CompanyID);
                SpParameters.Add("@BranchID", BranchID);
                SpParameters.Add("@UserName", userName);
                SpParameters.Add("@CalendarDays", ListtoDataTableConverter.ToDataTable(updateHolidays).AsTableValuedParameter("Type_HoliDay"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                returnMsg = _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }

            if (updateEceptions.Count() > 0)
            {
                string sp = "UpdateExceptionDay";

                DynamicParameters SpParameters = new DynamicParameters();
				SpParameters.Add("@CompanyID", CompanyID);
				SpParameters.Add("@BranchID", BranchID);
				SpParameters.Add("@UserName", userName);
                SpParameters.Add("@@ExceptionDays", ListtoDataTableConverter.ToDataTable(updateEceptions).AsTableValuedParameter("Type_ExceptionDay"));
                SpParameters.Add("@ReturnMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
                returnMsg = _dbCommonOperation.InsertUpdateBySP(sp, SpParameters);
            }

            return returnMsg;
        }

    
   }
}
