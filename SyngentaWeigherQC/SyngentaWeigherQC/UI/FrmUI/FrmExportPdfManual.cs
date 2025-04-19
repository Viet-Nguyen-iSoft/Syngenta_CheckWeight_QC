using DocumentFormat.OpenXml.VariantTypes;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    private List<DateTime> listDateInMonth = new List<DateTime>();
    private List<DateTime> listDateInWeek = new List<DateTime>();
    private void LoadDataDefault()
    {
      //Set value Current
      DateTime dt = DateTime.Now;
      yearCurrent = dt.Year;
      monthCurrent = dt.Month;

      weekInYears = AppCore.Ins.GetDaysInWeeks(yearCurrent);


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
      listDateInMonth =  AppCore.Ins.GetAllDaysInMonth(yearCurrent, monthCurrent);
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
      weekInYears = AppCore.Ins.GetDaysInWeeks(yearCurrent);
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
      listDateInMonth = AppCore.Ins.GetAllDaysInMonth(yearCurrent, monthCurrent);
      dtpFromMonth.Value = listDateInMonth.FirstOrDefault();
      dtpToMonth.Value = listDateInMonth.LastOrDefault();
    }

    public int GetIso8601WeekOfYear(DateTime time)
    {
      System.Globalization.Calendar calendar = System.Globalization.CultureInfo.InvariantCulture.Calendar;
      return calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }



    private void FrmExportPdfManual_Load(object sender, EventArgs e)
    {

    }

    private void btnExportMonth_Click(object sender, EventArgs e)
    {
      try
      {
        string pathLocal = AppCore.Ins._stationCurrent.PathReportLocal;
        string pathOneDrive = AppCore.Ins._stationCurrent.PathReportOneDrive;
        string nameStation = AppCore.Ins._stationCurrent.Name;
        FrmReportAutoPdf frmReportAutoPdfMonth = new FrmReportAutoPdf(yearCurrent, monthCurrent, weekCurrent, pathLocal, pathOneDrive, nameStation, isExportWeek:false);
        frmReportAutoPdfMonth.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }
    private void btnExportWeek_Click(object sender, EventArgs e)
    {
      try
      {
        string pathLocal = AppCore.Ins._stationCurrent.PathReportLocal;
        string pathOneDrive = AppCore.Ins._stationCurrent.PathReportOneDrive;
        string nameStation = AppCore.Ins._stationCurrent.Name;
        FrmReportAutoPdf frmReportAutoPdfMonth = new FrmReportAutoPdf(yearCurrent, monthCurrent, weekCurrent, pathLocal, pathOneDrive, nameStation, isExportMonth:false);
        frmReportAutoPdfMonth.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

   
    
  }
}
