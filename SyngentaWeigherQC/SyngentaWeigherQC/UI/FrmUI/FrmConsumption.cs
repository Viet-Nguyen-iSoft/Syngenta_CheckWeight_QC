using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Models.NewFolder1;
using SyngentaWeigherQC.UI.Filter;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;


namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSynthetic : Form
  {
    public FrmSynthetic()
    {
      InitializeComponent();
      SetTitleChart();
    }
    #region Singleton parttern
    private static FrmSynthetic _Instance = null;
    public static FrmSynthetic Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSynthetic();
        }
        return _Instance;
      }
    }
    #endregion

    #region Init Chart
    private void SetTitleChart()
    {
      //Home
      //Set Title Chart 
      this.chartErrorHome.SetTilte = "Top trung bình % mẫu lỗi";
      this.chartLossHome.SetTilte = "Top trung bình % hao hụt";
      this.chartCpkHome.SetTilte = "Top trung bình Cpk";
      this.chartStdevHome.SetTilte = "Top trung bình Stdev";

      //Set Title Chart 
      this.chartavgErrorHome.SetTilte = "Biểu đồ trung bình % mẫu lỗi";
      this.chartavgLossHome.SetTilte = "Biểu đồ trung bình % hao hụt";
      this.chartavgCpkHome.SetTilte = "Biểu đồ trung bình Cpk";
      this.chartavgStdevHome.SetTilte = "Biểu đồ trung bình Stdev";

      //Month
      //Set Title Chart 
      this.ucChartV1_avgSampleErrorMonthCurrent.SetTilte = "Top trung bình % mẫu lỗi";
      this.ucChartV1_avgSampleLossMonthCurrent.SetTilte = "Top trung bình % hao hụt";
      this.ucChartV1_avgCpkMonthCurrent.SetTilte = "Top trung bình Cpk";
      this.ucChartV1_avgStdevMonthCurrent.SetTilte = "Top trung bình Stdev";

      //Set Title Chart 
      this.ucChartV2_avgSampleErrorMonthCurrent.SetTilte = "Biểu đồ trung bình % mẫu lỗi";
      this.ucChartV2_avgSampleLossMonthCurrent.SetTilte = "Biểu đồ trung bình % hao hụt";
      this.ucChartV2_avgCpkMonthCurrent.SetTilte = "Biểu đồ trung bình Cpk";
      this.ucChartV2_avgStdevMonthCurrent.SetTilte = "Biểu đồ trung bình Stdev";

      //Week
      //Set Title Chart 
      this.Chart1WeekCurrentError.SetTilte = "Top trung bình % mẫu lỗi";
      this.Chart1WeekCurrentLoss.SetTilte = "Top trung bình % hao hụt";
      this.Chart1WeekCurrentCpk.SetTilte = "Top trung bình Cpk";
      this.Chart1WeekCurrentStdev.SetTilte = "Top trung bình Stdev";

      //Set Title Chart 
      this.Chart2WeekCurrentError.SetTilte = "Biểu đồ trung bình % mẫu lỗi";
      this.Chart2WeekCurrentLoss.SetTilte = "Biểu đồ trung bình % hao hụt";
      this.Chart2WeekCurrentCpk.SetTilte = "Biểu đồ trung bình Cpk";
      this.Chart2WeekCurrentStdev.SetTilte = "Biểu đồ trung bình Stdev";

    }

    #endregion

    public List<InforLine> CbbLines
    {
      set
      {
        this.cbbLine.DataSource = value;
        this.cbbLine.DisplayMember = "Name";
      }
    }

    private int year_current = 0;
    private int month_current = 0;
    private int weekCurrent = 0;
    private int _lineId = 0;
    private List<DatalogWeight> _weightList = new List<DatalogWeight>();
    private async void FrmConsumption_Load(object sender, EventArgs e)
    {
      CbbLines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();

      // Lấy tuần tháng năm hiện tại
      DateTime dt_curent = DateTime.Now;
      month_current = dt_curent.Month;
      year_current = dt_curent.Year;

      CultureInfo cul = CultureInfo.CurrentCulture;
      weekCurrent = cul.Calendar.GetWeekOfYear(dt_curent, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

      var lineChoose = cbbLine.SelectedItem as InforLine;
      _lineId = (lineChoose != null) ? lineChoose.Id : 0;

      //Dữ liệu tháng hiện tại
      month_current = 4;
      _weightList = await LoadDataMonth(year_current, month_current, _lineId);
      LoadDataShowUI(_weightList);
    }

    private void LoadDataShowUI(List<DatalogWeight> datalogWeights)
    {
      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogWeights);
      var consumptionCharts = AppCore.Ins.CvtDataReportExcelToConsumptionChart(dataReportExcels);
      var consumptionTable = AppCore.Ins.CvtDataReportExcelToConsumptionTable(dataReportExcels);

      SetDataChart(consumptionCharts);
      SetDataTable(consumptionTable);
    }


    #region Month
    private void btnMonth_Click(object sender, EventArgs e)
    {
      FrmFilterMonth frmFilterMonth = new FrmFilterMonth();
      frmFilterMonth.OnSendMonthChoose += FrmFilterMonth_OnSendMonthChoose;
      frmFilterMonth.Location = new System.Drawing.Point(10, 500);
      frmFilterMonth.ShowDialog();
    }
    private async void FrmFilterMonth_OnSendMonthChoose(List<DateTime> listDate)
    {
      if (listDate.Count > 0)
      {
        var from = listDate.FirstOrDefault();
        var to = listDate.LastOrDefault();
        _weightList = await LoadDataByRangeDate(from, to, _lineId);
        LoadDataShowUI(_weightList);
      }
    }
    #endregion

    #region Week
    private void btnWeek_Click(object sender, EventArgs e)
    {
      FrmFilterWeek frmFilterWeek = new FrmFilterWeek();
      frmFilterWeek.OnSendWeekChoose += FrmFilterWeek_OnSendWeekChoose;
      frmFilterWeek.Location = new System.Drawing.Point(100, 210);
      frmFilterWeek.ShowDialog();
    }

    private async void FrmFilterWeek_OnSendWeekChoose(List<DateTime> listDate)
    {
      if (listDate.Count > 0)
      {
        var from = listDate.FirstOrDefault();
        var to = listDate.LastOrDefault();
        _weightList = await LoadDataByRangeDate(from, to, _lineId);
        LoadDataShowUI(_weightList);
      }
    }
    #endregion


    #region Day
    private void btnDay_Click(object sender, EventArgs e)
    {
      FrmFilterDate frmFilterDate = new FrmFilterDate(monthCurrent);
      frmFilterDate.OnSendDateChoose += FrmFilterDate_OnSendDateChoose;
      frmFilterDate.Location = new System.Drawing.Point(200, 320);
      frmFilterDate.ShowDialog();
    }

    private async void FrmFilterDate_OnSendDateChoose(List<DateTime> listDate)
    {
      if (listDate.Count > 0)
      {
        var from = listDate.FirstOrDefault();
        var to = listDate.LastOrDefault();
        _weightList = await LoadDataByRangeDate(from, to, _lineId);
        LoadDataShowUI(_weightList);
      }
    }

    #endregion

    private void btnLoad_Click(object sender, EventArgs e)
    {

    }

    private void btnExportPdf_Click(object sender, EventArgs e)
    {
      string path = "";
      using (var saveFD = new SaveFileDialog())
      {
        saveFD.Filter = "Excel|*.xlsx|All files|*.*";
        saveFD.Title = "Save report to excel file";
        saveFD.FileName = $"Báo cáo tổng hợp  {DateTime.Now.ToString("dd_MM_yyyy")}  At {DateTime.Now.ToString("HH_mm")}.pdf";

        DialogResult dialogResult = saveFD.ShowDialog();
        if (dialogResult == DialogResult.OK) path = saveFD.FileName; //lay duong dan luu file
        else return; //huy report neu chon cancel
      }

      FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(_weightList, path, "Test Report");
      frmReportAutoPdf.ShowDialog();

      //using (var saveFD = new SaveFileDialog())
      //{
      //  saveFD.Filter = "Excel|*.xlsx|All files|*.*";
      //  saveFD.Title = "Save report to excel file";
      //  saveFD.FileName = $"Báo cáo tổng hợp  {DateTime.Now.ToString("dd_MM_yyyy")}  At {DateTime.Now.ToString("HH_mm")}.pdf";

      //  DialogResult dialogResult = saveFD.ShowDialog();
      //  if (dialogResult == DialogResult.OK) path = saveFD.FileName; //lay duong dan luu file
      //  else return; //huy report neu chon cancel
      //}

      ////this.lbTitleHome.Visible = true;
      ////this.tablelayoutFilter.Visible = false;

      ////ExportToPdf(path);
      //tabControl1.SelectedIndex = 0;

      //this.lbTitleHome.Visible = false;
      //this.tablelayoutFilter.Visible = true;
    }


    private async Task<List<DatalogWeight>> LoadDataMonth(int year, int month, int lineId)
    {
      var rangeDate = DatetimeHelper.RangeDateByMonth(year, month);
      return await AppCore.Ins.LoadAllDatalogWeight(lineId, rangeDate.StartDate, rangeDate.EndDate);
    }
    private async Task<List<DatalogWeight>> LoadDataByRangeDate(DateTime from, DateTime to, int lineId)
    {
      return await AppCore.Ins.LoadAllDatalogWeight(lineId, from, to);
    }



    private void SetDataChart(List<ConsumptionChart> consumptionV2s)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart(consumptionV2s);
        }));
        return;
      }

      Dictionary<string, double> groupDataTopError = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopLoss = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopCpk = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopStdev = new Dictionary<string, double>();

      Dictionary<string, double> groupDataAverageError = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageLoss = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageCpk = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageStdev = new Dictionary<string, double>();

      //Đi từng sản phẩm
      foreach (var item in consumptionV2s)
      {
        double topError = item.DetailConsumptions.Max(x => x.Error);
        double topLoss = item.DetailConsumptions.Max(x => x.Loss);
        double topCpk = item.DetailConsumptions.Max(x => x.Cpk);
        double topStdev = item.DetailConsumptions.Max(x => x.Stdev);

        groupDataTopError.Add(item.Production.Name, topError);
        groupDataTopLoss.Add(item.Production.Name, topLoss);
        groupDataTopCpk.Add(item.Production.Name, topCpk);
        groupDataTopStdev.Add(item.Production.Name, topStdev);

        double averageError = item.DetailConsumptions.Average(x => x.Error);
        double averageLoss = item.DetailConsumptions.Average(x => x.Loss);
        double averageCpk = item.DetailConsumptions.Average(x => x.Cpk);
        double averageStdev = item.DetailConsumptions.Average(x => x.Stdev);

        groupDataAverageError.Add(item.Production.Name, averageError);
        groupDataAverageLoss.Add(item.Production.Name, averageLoss);
        groupDataAverageCpk.Add(item.Production.Name, averageCpk);
        groupDataAverageStdev.Add(item.Production.Name, averageStdev);
      }

      //Dãy trên
      this.chartErrorHome.SetDataChart(groupDataTopError);
      this.chartLossHome.SetDataChart(groupDataTopLoss);
      this.chartCpkHome.SetDataChart(groupDataTopCpk);
      this.chartStdevHome.SetDataChart(groupDataTopStdev);


      //Dãy dưới
      this.chartavgErrorHome.SetDataChart(groupDataAverageError);
      this.chartavgLossHome.SetDataChart(groupDataAverageLoss);
      this.chartavgCpkHome.SetDataChart(groupDataAverageCpk);
      this.chartavgStdevHome.SetDataChart(groupDataAverageStdev);

      //Chart Pie
      var totalSample = (int)consumptionV2s
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalSample);
      var totalOver = (int)consumptionV2s
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalOver);
      var totalLower = (int)consumptionV2s
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalLower);

      ChartPieHome.SetDataChart(totalSample, totalLower, totalOver);
    }




    private void SetDataTable(List<ConsumptionTable> consumptions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(consumptions);
        }));
        return;
      }

      dataGridViewTableHome.Rows.Clear();
      if (consumptions.Count > 0)
      {
        foreach (ConsumptionTable consumption in consumptions)
        {
          int row = dataGridViewTableHome.Rows.Add();
          dataGridViewTableHome.Rows[row].Cells["Column1"].Value = consumption.Shift;
          dataGridViewTableHome.Rows[row].Cells["Column3"].Value = consumption.DateTime;
          dataGridViewTableHome.Rows[row].Cells["Column4"].Value = consumption.ProductionName;
          dataGridViewTableHome.Rows[row].Cells["Column5"].Value = consumption.AverageMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column6"].Value = consumption.MinMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column7"].Value = consumption.MaxMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column8"].Value = consumption.Target;
          dataGridViewTableHome.Rows[row].Cells["Column9"].Value = consumption.LowerProduct;
          dataGridViewTableHome.Rows[row].Cells["Column10"].Value = consumption.UpperProduct;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Value = consumption.Evaluate;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Style.BackColor = (consumption.Evaluate == "ĐẠT") ? Color.Green : Color.Red;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Style.ForeColor = Color.White;
        }
      }
    }



    private List<DateTime> GetAllDaysInMonth(int year, int month)
    {
      List<DateTime> listOfDaysInMonth = new List<DateTime>();
      int daysInMonth = DateTime.DaysInMonth(year, month);
      for (int day = 1; day <= daysInMonth; day++)
      {
        listOfDaysInMonth.Add(new DateTime(year, month, day));
      }
      return listOfDaysInMonth;
    }




    //Home
    private List<DateTime> listDateHome = new List<DateTime>();
    private int monthCurrent = DateTime.Now.Month;




    private List<DataReportByDate> reportDataWeek = new List<DataReportByDate>();





    private void DataHome(List<DateTime> listDays)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          DataHome(listDays);
        }));
        return;
      }


    }



    private void UpdateUIChartPie(UcChartPie ucChartPie, DataChartReport dataChartReport)
    {
      int total = 0;
      int over = 0;
      int lower = 0;
      if (dataChartReport != null)
      {
        total = dataChartReport.NumberSampleTotal;
        over = dataChartReport.NumberSampleUpper;
        lower = dataChartReport.NumberSampleLower;
      }
      ucChartPie.SetDataChart(total, lower, over);
    }


    private List<Sample> GetSampleFromDatalogId(List<Sample> samples, List<int> datalogId)
    {
      if (samples == null || datalogId == null) return null;

      List<Sample> listSample = new List<Sample>();

      if (datalogId.Count > 0)
      {
        for (int i = 0; i < datalogId.Count; i++)
        {
          List<Sample> samplesChid = samples?.Where(x => x.DatalogId == datalogId[i]).ToList();
          if (samplesChid != null)
          {
            if (samplesChid.Count > 0)
              listSample.AddRange(samplesChid);
          }
        }
      }
      return listSample;
    }







    private List<DateTime> GetAllDaysInWeek(int year, int weekNumber)
    {
      List<DateTime> daysOfWeek = new List<DateTime>();
      DateTime jan1 = new DateTime(year, 1, 1);

      // Tìm ngày đầu tiên của tuần đầu tiên trong năm
      int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
      DateTime firstMonday = jan1.AddDays(daysOffset);

      // Tính toán ngày bắt đầu của tuần được chỉ định
      int daysToAdd = (weekNumber - 1) * 7;
      DateTime startDate = firstMonday.AddDays(daysToAdd);

      // Thêm các ngày của tuần vào danh sách
      for (int i = 0; i < 7; i++)
      {
        daysOfWeek.Add(startDate.AddDays(i));
      }

      return daysOfWeek;
    }




    private Bitmap RenderControlToBitmap(System.Windows.Forms.Control control)
    {
      Bitmap bitmap = new Bitmap(control.Width, control.Height);
      //control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, (int)PageSize.A4.Height, (int)PageSize.A4.Width));
      control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, 2500, 2500));
      return bitmap;
    }












    private List<Shift> listShift = new List<Shift>();
    private List<int> listCodeShiftFilter = new List<int>();
    private void btnShift_Click(object sender, EventArgs e)
    {
      listShift = AppCore.Ins._listShift.OrderBy(x => x.CodeShift).ToList();

      List<string> listShiftName = listShift.Select(x => x.Name).ToList();
      FrmFilterShift frmFilterShift = new FrmFilterShift(listShiftName);
      frmFilterShift.OnSendShiftChoose += FrmFilterShift_OnSendShiftChoose;
      frmFilterShift.Location = new System.Drawing.Point(300, 510);
      frmFilterShift.ShowDialog();
    }

    private void FrmFilterShift_OnSendShiftChoose(List<string> listDate)
    {
      listCodeShiftFilter = new List<int>();
      if (listDate.Count > 0 && listShift.Count > 0)
      {
        foreach (var item in listDate)
        {
          int code = listShift.Where(x => x.Name == item).Select(x => x.CodeShift).FirstOrDefault();
          listCodeShiftFilter.Add(code);
        }
      }

      //FilterDataPageHome(listDateHome);
      DataHome(listDateHome);
    }

    private void ClearHome()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearHome();
        }));
        return;
      }

      //Dãy trên
      this.chartErrorHome.SetDataChart(null);
      this.chartLossHome.SetDataChart(null);
      this.chartCpkHome.SetDataChart(null);
      this.chartStdevHome.SetDataChart(null);

      //Dãy dưới
      this.chartavgErrorHome.SetDataChart(null, $"Tháng 0");
      this.chartavgLossHome.SetDataChart(null, $"Tháng 0");
      this.chartavgCpkHome.SetDataChart(null, $"Tháng 0");
      this.chartavgStdevHome.SetDataChart(null, $"Tháng 0");

      this.ChartPieHome.SetDataChart(0, 0, 0);

      this.dataGridViewTableHome.Rows.Clear();
    }

    private void ClearMonth()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearMonth();
        }));
        return;
      }

      //Dãy trên
      this.ucChartV1_avgSampleErrorMonthCurrent.SetDataChart(null);
      this.ucChartV1_avgSampleLossMonthCurrent.SetDataChart(null);
      this.ucChartV1_avgCpkMonthCurrent.SetDataChart(null);
      this.ucChartV1_avgStdevMonthCurrent.SetDataChart(null);

      //Dãy dưới
      this.ucChartV2_avgSampleErrorMonthCurrent.SetDataChart(null, $"Tháng 0");
      this.ucChartV2_avgSampleLossMonthCurrent.SetDataChart(null, $"Tháng 0");
      this.ucChartV2_avgCpkMonthCurrent.SetDataChart(null, $"Tháng 0");
      this.ucChartV2_avgStdevMonthCurrent.SetDataChart(null, $"Tháng 0");

      UpdateUIChartPie(ucChartPieMothCurrent, null);
    }

    private void ClearWeek()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearWeek();
        }));
        return;
      }

      //Dãy trên
      this.Chart1WeekCurrentError.SetDataChart(null);
      this.Chart1WeekCurrentLoss.SetDataChart(null);
      this.Chart1WeekCurrentCpk.SetDataChart(null);
      this.Chart1WeekCurrentStdev.SetDataChart(null);

      //Dãy dưới
      this.Chart2WeekCurrentError.SetDataChart(null, $"Tuần  0");
      this.Chart2WeekCurrentLoss.SetDataChart(null, $"Tuần 0");
      this.Chart2WeekCurrentCpk.SetDataChart(null, $"Tuần 0");
      this.Chart2WeekCurrentStdev.SetDataChart(null, $"Tuần 0");

      UpdateUIChartPie(Chart2PieWeekCurrent, null);
    }




    //private void ExportToPdf(string pathReport)
    //{
    //  // Kích thước của trang PDF (kích thước A4 nằm ngang)
    //  float pageWidth = PageSize.A4.Rotate().Width;
    //  float pageHeight = PageSize.A4.Rotate().Height;

    //  int numberTag = tabControl1.TabCount;
    //  // Tạo một tài liệu PDF
    //  using (Document document = new Document(new Rectangle(pageWidth, pageHeight)))
    //  {
    //    // Tạo một PdfWriter để ghi tài liệu vào một tệp
    //    using (FileStream fileStream = new FileStream(pathReport, FileMode.Create))
    //    {
    //      using (PdfWriter writer = PdfWriter.GetInstance(document, fileStream))
    //      {
    //        // Mở Document để bắt đầu ghi
    //        document.Open();
    //        int tageStart = 0;
    //        for (; tageStart < numberTag; tageStart++)
    //        {
    //          tabControl1.SelectedIndex = tageStart;
    //          // Chụp ảnh của TableLayoutPanel
    //          //UcReport userControl = (UserControl)tabControl1.TabIndex[2];
    //          TabPage tabPage = tabControl1.TabPages[tageStart];
    //          Bitmap bitmapN = RenderControlToBitmap(tabPage);

    //          // Tạo đối tượng Image từ Bitmap
    //          iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmapN, System.Drawing.Imaging.ImageFormat.Bmp);
    //          image.ScaleAbsolute(PageSize.A4.Height, PageSize.A4.Width);
    //          image.SetAbsolutePosition(0, 0);

    //          // Vẽ ảnh lên trang PDF
    //          document.NewPage();
    //          document.Add(image);
    //        }

    //        document.Close();
    //      }
    //    }
    //  }
    //}




    private void btnExxportPdfByWeek_Click(object sender, EventArgs e)
    {
      FrmExportPdfManual frmFilterWeek = new FrmExportPdfManual();
      frmFilterWeek.ShowDialog();
    }


  }
}
