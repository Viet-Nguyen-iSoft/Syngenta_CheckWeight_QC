using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmExportPdfManual : Form
  {
    public FrmExportPdfManual()
    {
      InitializeComponent();
      LoadDataDefault();
    }

    private Dictionary<int, List<DateTime>> weekInYears = new Dictionary<int, List<DateTime>>();
    private int weekCurrent = 0;
    private int monthCurrent = 0;
    private int yearCurrent = 0;
    private int lineId = 0;
    private List<DateTime> listDateInMonth = new List<DateTime>();
    private List<DateTime> listDateInWeek = new List<DateTime>();

    private void FrmExportPdfManual_Load(object sender, EventArgs e)
    {
      CbbLines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();

      var lineChoose = cbbLine.SelectedItem as InforLine;
      lineId = (lineChoose != null) ? lineChoose.Id : 0;
    }

    private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
    {
      var lineChoose = cbbLine.SelectedItem as InforLine;
      lineId = (lineChoose != null) ? lineChoose.Id : 0;
    }

    public List<InforLine> CbbLines
    {
      set
      {
        this.cbbLine.DataSource = value;
        this.cbbLine.DisplayMember = "Name";
      }
    }


    private void LoadDataDefault()
    {
      //Set value Current
      DateTime dt = DateTime.Now;
      yearCurrent = dt.Year;
      monthCurrent = dt.Month;

      weekInYears = DatetimeHelper.GetDaysInWeeks(yearCurrent);


      //Year
      for (int i = 2020; i <= 2120; i++)
      {
        this.cbbYearMonth.Items.Add(i);
        this.cbbYearWeek.Items.Add(i);
      }

      //Month
      for (int i = 1; i <= 12; i++)
      {
        this.cbbMonth.Items.Add(i);
      }

      foreach (var item in weekInYears.Keys.ToList())
      {
        this.cbbWeek.Items.Add(item);
      }

      this.cbbYearMonth.SelectedItem = yearCurrent;
      this.cbbYearWeek.SelectedItem = yearCurrent;
      this.cbbMonth.SelectedItem = monthCurrent;

      weekCurrent = GetIso8601WeekOfYear(dt);
      this.cbbWeek.SelectedItem = weekCurrent;

      //Tháng
      listDateInMonth = DatetimeHelper.GetAllDaysInMonth(yearCurrent, monthCurrent);
      dtpFromMonth.Value = listDateInMonth.FirstOrDefault();
      dtpToMonth.Value = listDateInMonth.LastOrDefault();

      //Tuần
      listDateInWeek = weekInYears.Where(x => x.Key == weekCurrent).FirstOrDefault().Value;
      dtpFromWeek.Value = listDateInWeek.FirstOrDefault();
      dtpToWeek.Value = listDateInWeek.LastOrDefault();

      //Sự kiện
      cbbMonth.SelectedIndexChanged += CbbMonth_SelectedIndexChanged;
      cbbYearMonth.SelectedIndexChanged += CbbMonth_SelectedIndexChanged;

      cbbYearWeek.SelectedIndexChanged += CbbWeek_SelectedIndexChanged;
      cbbWeek.SelectedIndexChanged += CbbWeek_SelectedIndexChanged;
    }

    private void CbbWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
      weekInYears = DatetimeHelper.GetDaysInWeeks(yearCurrent);
      int.TryParse(this.cbbWeek.Text, out weekCurrent);
      //Tuần
      listDateInWeek = weekInYears.Where(x => x.Key == weekCurrent).FirstOrDefault().Value;
      dtpFromWeek.Value = listDateInWeek.FirstOrDefault();
      dtpToWeek.Value = listDateInWeek.LastOrDefault();
    }

    private void CbbMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
      int.TryParse(this.cbbMonth.Text, out monthCurrent);

      //Tháng
      listDateInMonth = DatetimeHelper.GetAllDaysInMonth(yearCurrent, monthCurrent);
      dtpFromMonth.Value = listDateInMonth.FirstOrDefault();
      dtpToMonth.Value = listDateInMonth.LastOrDefault();
    }

    public int GetIso8601WeekOfYear(DateTime time)
    {
      System.Globalization.Calendar calendar = System.Globalization.CultureInfo.InvariantCulture.Calendar;
      return calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    private async void btnExportMonth_Click(object sender, EventArgs e)
    {
      try
      {
        var datalogWeights = await AppCore.Ins.LoadDataMonth(yearCurrent, monthCurrent, lineId);


        if (datalogWeights?.Count() <= 0)
        {
          new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Warning);
          return;
        }

        string titleReport = $"BÁO CÁO THÁNG {monthCurrent}";

        string[] pathReport = new string[3];
        using (var saveFD = new SaveFileDialog())
        {
          saveFD.Filter = "Excel|*.xlsx|All files|*.*";
          saveFD.Title = "Save report to excel file";
          saveFD.FileName = $"{titleReport}.pdf";

          DialogResult dialogResult = saveFD.ShowDialog();
          if (dialogResult == DialogResult.OK) pathReport[0] = saveFD.FileName; //lay duong dan luu file
          else return; //huy report neu chon cancel
        }

        FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(datalogWeights, pathReport, titleReport);
        frmReportAutoPdf.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }
    private async void btnExportWeek_Click(object sender, EventArgs e)
    {
      try
      {
        var datalogWeights = await AppCore.Ins.LoadDataWeek(yearCurrent, weekCurrent, lineId);

        if (datalogWeights?.Count() <= 0)
        {
          new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Warning);
          return;
        }

        string titleReport = $"BÁO CÁO TUẦN {monthCurrent}";

        string[] pathReport = new string[3];
        using (var saveFD = new SaveFileDialog())
        {
          saveFD.Filter = "Excel|*.xlsx|All files|*.*";
          saveFD.Title = "Save report to excel file";
          saveFD.FileName = $"{titleReport}.pdf";

          DialogResult dialogResult = saveFD.ShowDialog();
          if (dialogResult == DialogResult.OK) pathReport[0] = saveFD.FileName;
          else return;
        }

        FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(datalogWeights, pathReport, titleReport);
        frmReportAutoPdf.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }


  }
}
