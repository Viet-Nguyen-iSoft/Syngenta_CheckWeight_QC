using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class DatetimeHelper
  {
    public static (DateTime StartDate, DateTime EndDate) RangeDateByMonth(int year, int month)
    {
      DateTime startDate = new DateTime(year, month, 1);
      DateTime endDate = startDate.AddMonths(1).AddDays(-1);
      return (startDate, endDate);
    }

    public static (DateTime StartDate, DateTime EndDate) GetRangeDateCurrent(DateTime dateTime)
    {
      var dt_current = dateTime;
      var hour_current = dt_current.Hour;

      if (hour_current >= 0 && hour_current < 6)
      {
        DateTime startDate = dt_current.AddDays(-1).Date + new TimeSpan(6, 0, 0);
        DateTime endDate = dt_current.Date + new TimeSpan(5, 59, 59);
        return (startDate, endDate);
      }
      else
      {
        DateTime startDate = dt_current.Date + new TimeSpan(6, 0, 0);
        DateTime endDate = dt_current.AddDays(1).Date + new TimeSpan(5, 59, 59);
        return (startDate, endDate);
      }
    }
  }
}
