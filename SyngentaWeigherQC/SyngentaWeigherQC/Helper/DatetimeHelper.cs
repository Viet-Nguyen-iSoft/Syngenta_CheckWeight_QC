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
      // Ngày đầu tiên của tháng, 6h sáng
      DateTime startTime = new DateTime(year, month, 1, 6, 0, 0);

      // Ngày đầu tiên của tháng tiếp theo, 6h sáng (ngày kết thúc)
      DateTime endTime = (month == 12)
          ? new DateTime(year + 1, 1, 1, 6, 0, 0)
          : new DateTime(year, month + 1, 1, 6, 0, 0);

      return (startTime, endTime);
    }

    public static (DateTime StartDate, DateTime EndDate) RangeDateByWeek(int year, int week)
    {
      // Tìm ngày đầu tiên của năm
      DateTime jan1 = new DateTime(year, 1, 1);

      // ISO 8601: Thứ 2 là ngày đầu tuần, tuần 1 chứa ngày thứ 4 đầu tiên của năm
      int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
      DateTime firstThursday = jan1.AddDays(daysOffset);

      // Tuần đầu tiên có chứa Thursday => tuần 1 bắt đầu từ Monday trước đó
      var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
      int firstWeek = cal.GetWeekOfYear(firstThursday, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

      // Lùi về Monday của tuần đầu tiên
      DateTime firstWeekStart = firstThursday.AddDays(-3);

      // Tính ngày bắt đầu tuần cần tìm
      DateTime startTime = firstWeekStart.AddDays((week - 1) * 7).Date.AddHours(6);
      DateTime endTime = startTime.AddDays(7); // đến 6h sáng thứ 2 tuần kế

      return (startTime, endTime);
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

    public static Dictionary<int, List<DateTime>> GetDaysInWeeks(int year)
    {
      Dictionary<int, List<DateTime>> daysInWeeks = new Dictionary<int, List<DateTime>>();

      // Tính ngày đầu tiên của năm
      DateTime firstDayOfYear = new DateTime(year, 1, 1);
      // Tính ngày cuối cùng của năm
      DateTime lastDayOfYear = new DateTime(year, 12, 31);

      int week = 1; // Khởi tạo số tuần ban đầu
      List<DateTime> daysInWeek = new List<DateTime>(); // Danh sách ngày trong tuần
                                                        // Lặp qua từ ngày đầu tiên đến ngày cuối cùng của năm
      for (DateTime date = firstDayOfYear; date <= lastDayOfYear; date = date.AddDays(1))
      {
        // Kiểm tra nếu ngày hiện tại là ngày đầu tiên của một tuần mới
        if (date.DayOfWeek == DayOfWeek.Monday)
        {
          if (daysInWeek.Count > 0)
          {
            daysInWeeks.Add(week, daysInWeek); // Thêm danh sách ngày trong tuần vào Dictionary
            week++; // Tăng số tuần lên 1
            daysInWeek = new List<DateTime>(); // Tạo danh sách mới cho tuần tiếp theo
          }
        }
        daysInWeek.Add(date); // Thêm ngày vào danh sách của tuần
      }

      // Thêm danh sách ngày trong tuần cuối cùng của năm
      if (daysInWeek.Count > 0)
      {
        daysInWeeks.Add(week, daysInWeek);
      }
      return daysInWeeks;
    }
    public static List<DateTime> GetAllDaysInMonth(int year, int month)
    {
      List<DateTime> listOfDaysInMonth = new List<DateTime>();
      int daysInMonth = DateTime.DaysInMonth(year, month);
      for (int day = 1; day <= daysInMonth; day++)
      {
        listOfDaysInMonth.Add(new DateTime(year, month, day));
      }
      return listOfDaysInMonth;
    }

  }
}
