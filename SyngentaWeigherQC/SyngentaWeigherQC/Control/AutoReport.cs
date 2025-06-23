using DocumentFormat.OpenXml.Bibliography;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    public async void ReportAutoDailys(DateTime dateTime)
    {
      if (AppCore.Ins._listInforLine.Count > 0)
      {
        var dt_report = DatetimeHelper.GetRangeDateCurrent(dateTime);
        var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
        foreach (var line in lines)
        {
          var datalogs = await AppCore.Ins.LoadAllDatalogWeight(line.Id, dt_report.StartDate, dt_report.EndDate);
          List<DataReportExcel> dataReportExcels = AppCore.Ins.GenerateDataReport(datalogs);

          if (dataReportExcels != null)
          {
            if (dataReportExcels.Count > 0)
            {
              string[] pathReport = new string[3];

              //Yêu càu tạo các Folder Tháng trong Ngày
              //Tháng nào ?
              int month = dateTime.Month;

              HelperFolder_File.CreateFolderIfExits(line.PathReportLocal + $"\\Dailys\\Month{month}");
              HelperFolder_File.CreateFolderIfExits(line.PathReportOneDrive + $"\\Dailys\\Month{month}");

              pathReport[0] = line.PathReportLocal + $"\\Dailys\\Month{month}";
              pathReport[1] = line.PathReportOneDrive + $"\\Dailys\\Month{month}";
              foreach (var item in dataReportExcels)
              {
                ExcelHelper.ReportExcel(item, pathReport);
              }
            }
          }
        }
      }
    }

    public async void ReportAutoMonthly(int year, int month)
    {
      if (AppCore.Ins._listInforLine.Count > 0)
      {
        var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
        foreach (var line in lines)
        {
          var datalogWeights = await AppCore.Ins.LoadDataMonth(year, month, line.Id);
          if (datalogWeights?.Count() <= 0)
          {
            continue;
          }

          //Check  Folder
          string[] pathReport = new string[3];
          pathReport[0] = line.PathReportLocal + $"\\Months";
          pathReport[1] = line.PathReportOneDrive + $"\\Months";

          HelperFolder_File.CreateFolderIfExits(pathReport[0]);
          HelperFolder_File.CreateFolderIfExits(pathReport[1]);

          pathReport[0] += $"\\BÁO CÁO THÁNG {month}.pdf";
          pathReport[1] += $"\\BÁO CÁO THÁNG {month}.pdf";

          if (!HelperFolder_File.FileExits(pathReport[0]))
          {
            FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(datalogWeights, pathReport, $"BÁO CÁO THÁNG {month}");
            frmReportAutoPdf.ShowDialog();
          }
        }
      }
    }

    public async void ReportAutoWeekly(int year, int week)
    {
      if (AppCore.Ins._listInforLine.Count > 0)
      {
        var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
        foreach (var line in lines)
        {
          var datalogWeights = await AppCore.Ins.LoadDataWeek(year, week, line.Id);
          if (datalogWeights?.Count() <= 0)
          {
            continue;
          }

          //Check  Folder
          string[] pathReport = new string[3];
          pathReport[0] = line.PathReportLocal + $"\\Weeks";
          pathReport[1] = line.PathReportOneDrive + $"\\Weeks";

          HelperFolder_File.CreateFolderIfExits(pathReport[0]);
          HelperFolder_File.CreateFolderIfExits(pathReport[1]);

          pathReport[0] += $"\\BÁO CÁO TUẦN {week}.pdf";
          pathReport[1] += $"\\BÁO CÁO TUẦN {week}.pdf";

          if (!HelperFolder_File.FileExits(pathReport[0]))
          {
            FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(datalogWeights, pathReport, $"BÁO CÁO TUẦN {week}");
            frmReportAutoPdf.ShowDialog();
          }
        }
      }
    }
  }
}
