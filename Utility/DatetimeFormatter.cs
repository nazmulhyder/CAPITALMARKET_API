using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class DatetimeFormatter
    {
        public static string DateFormat(string dateString)
        {
            if (dateString != null && dateString.Contains("/"))
            {
                var formated = dateString.Split("/")[2] + "-" + dateString.Split("/")[1] + "-" + dateString.Split("/")[0];
                return formated;
            }
            
            else return dateString;
        }
        public static DateTime ConvertStringToDatetime(string dateString)
        {
            return Convert.ToDateTime(dateString,
            System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
        }

        public static string ConvertDatetimeToString(DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy");
        }

		public static string ConvertDatetimeToStringDDMMYYY(DateTime dateTime)
		{
			return dateTime.ToString("dd/MM/yyyy");
		}
	}
}
