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
  }
}
