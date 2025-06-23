using DocumentFormat.OpenXml.Wordprocessing;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Models.NewFolder1;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmReportAutoPdf : Form
  {
    public List<DatalogWeight> _datalogWeights = new List<DatalogWeight>();
    public string[] _pathReport = new string[3];

    public FrmReportAutoPdf()
    {
      InitializeComponent();
    }

    public FrmReportAutoPdf(List<DatalogWeight> datalogWeights, string[] pathReport, string titleReport) : this()
    {
      _datalogWeights = datalogWeights;
      _pathReport = pathReport;

      lbTitleReport.Text = titleReport;
    }

    private void ReportAutoPdf_Load(object sender, EventArgs e)
    {
      timer1.Start();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      timer1.Stop();
      ReportStart();
    }

    private void ReportStart()
    {
      try
      {
        if (_datalogWeights.Count <= 0) return;

        LoadDataShowUI(_datalogWeights);

        //Detail Excel
        var dataReportExcels = AppCore.Ins.GenerateDataReport(_datalogWeights);

        if (dataReportExcels.Count() > 0)
        {
          //Đi từng ngày
          foreach (var dataReportExcel in dataReportExcels)
          {
            //Đi từng Loại Ca
            foreach (var data_by_shift in dataReportExcel.DataByDates)
            {
              //Đi từng sp
              foreach (var data_by_product in data_by_shift.DataByProducts)
              {
                var product = data_by_product.Production;
                var datas = data_by_product.DataByProductionByShifts;
                var lastRecord = datas.LastOrDefault().DatalogWeights.FirstOrDefault();


                //List Datalog Weight
                var datalogWeight = data_by_product.DataByProductionByShifts.SelectMany(x => x.DatalogWeights).ToList();

                //Tạo Tab
                TabPage newTabPage = new TabPage();
                newTabPage.Text = $"{data_by_product.Production.Name}";

                // 
                ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct()
                {
                  NameLine = lastRecord?.InforLine?.Name,
                  ModeTare = eNumHelper.GetDescription(lastRecord?.eModeTare),
                  ProductName = product.Name,
                  PackSize = product.PackSize,
                  Standard = product.StandardFinal,
                  Upper = product.UpperLimitFinal,
                  Lower = product.LowerLimitFinal,
                  NameShiftLeader = lastRecord.ShiftLeader?.UserName,
                };

                ////ChartLine
                var dataChartLine = AppCore.Ins.CvtDatalogWeightToChartLine(datalogWeight, product);

                //Data Table
                var data_table = AppCore.Ins.ConvertToDTOList(datalogWeight);

                UcReport ucReport = new UcReport();
                //ucReport.Name = $"SP{cntTab}";
                ucReport.Dock = DockStyle.Fill;
                ucReport.SetTitle($"{data_by_product.Production.Name} ({lastRecord.CreatedAt.ToString("yyyy-MM-dd")})");
                ucReport.SetInfor(excelClassInforProduct);
                ucReport.StatisticalProductUI(data_by_product);
                ucReport.SetChartHistogram(datalogWeight, product);
                ucReport.SetChartLine(dataChartLine);
                ucReport.SetDataTable(data_table, product);

                newTabPage.Controls.Add(ucReport);
                tabControl1.TabPages.Add(newTabPage);
              }
            }
          }
        }

      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
      finally
      {
        if (_pathReport.Length > 0)
        {
          foreach (var item in _pathReport)
          {
            if (item != null)
            {
              ExportToPdf(item);
            }
          }
        }
        
        this.Close();
      }
    }


    private void LoadDataShowUI(List<DatalogWeight> datalogWeights)
    {
      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogWeights);
      var consumptionCharts = AppCore.Ins.CvtDataReportExcelToConsumptionChart(dataReportExcels);
      var consumptionTable = AppCore.Ins.CvtDataReportExcelToConsumptionTable(dataReportExcels);

      SetDataChart(consumptionCharts);
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
      Dictionary<string, double> groupDataTopSigma = new Dictionary<string, double>();

      Dictionary<string, double> groupDataAverageError = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageLoss = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageCpk = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageStdev = new Dictionary<string, double>();
      Dictionary<string, double> groupDataAverageSigma = new Dictionary<string, double>();

      //Đi từng sản phẩm
      foreach (var item in consumptionV2s)
      {
        double topError = item.DetailConsumptions.Max(x => x.Error);
        double topLoss = item.DetailConsumptions.Max(x => x.Loss);
        double topCpk = item.DetailConsumptions.Max(x => x.Cpk);
        double topStdev = item.DetailConsumptions.Max(x => x.Stdev);
        double topSigma = item.DetailConsumptions.Max(x => x.Sigma);

        groupDataTopError.Add(item.Production.Name, topError);
        groupDataTopLoss.Add(item.Production.Name, topLoss);
        groupDataTopCpk.Add(item.Production.Name, topCpk);
        groupDataTopStdev.Add(item.Production.Name, topStdev);
        groupDataTopSigma.Add(item.Production.Name, topSigma);

        double averageError = item.DetailConsumptions.Average(x => x.Error);
        double averageLoss = item.DetailConsumptions.Average(x => x.Loss);
        double averageCpk = item.DetailConsumptions.Average(x => x.Cpk);
        double averageStdev = item.DetailConsumptions.Average(x => x.Stdev);
        double averageSigma = item.DetailConsumptions.Average(x => x.Sigma);

        groupDataAverageError.Add(item.Production.Name, averageError);
        groupDataAverageLoss.Add(item.Production.Name, averageLoss);
        groupDataAverageCpk.Add(item.Production.Name, averageCpk);
        groupDataAverageStdev.Add(item.Production.Name, averageStdev);
        groupDataAverageSigma.Add(item.Production.Name, averageSigma);
      }

      //Dãy trên
      this.chartTopError.SetDataChart(groupDataTopError);
      this.chartTopLoss.SetDataChart(groupDataTopLoss);
      this.chartTopCpk.SetDataChart(groupDataTopCpk);
      this.chartTopStdev.SetDataChart(groupDataTopStdev);
      this.chartTopSigma.SetDataChart(groupDataTopSigma);


      //Dãy dưới
      this.chartAvgError.SetDataChart(groupDataAverageError);
      this.chartAvgLoss.SetDataChart(groupDataAverageLoss);
      this.chartAvgCpk.SetDataChart(groupDataAverageCpk);
      this.chartAvgStdev.SetDataChart(groupDataAverageStdev);
      this.chartAvgSigma.SetDataChart(groupDataAverageSigma);

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

      this.chartPie.SetDataChart(totalSample, totalLower, totalOver);
      this.sumary.SetData(consumptionV2s.Count(), totalSample, totalLower + totalOver, totalOver);
    }



    private int _year = 0;
    private int _month = 0;
    private int _week = 0;

    private string _pathReportLocalMonth = "";
    private string _pathReportOndriveMonth = "";

    private string _pathReportLocalWeek = "";
    private string _pathReportOndriveWeek = "";

    private bool _permitExportMonth = true;
    private bool _permitExportWeek = true;
    public FrmReportAutoPdf(int year, int month, int week, string PathReportLocal, string PathReportOneDrive, string nameStation, bool isExportMonth = true, bool isExportWeek = true)
    {
      InitializeComponent();
      this.WindowState = FormWindowState.Maximized;
      this.StartPosition = FormStartPosition.CenterScreen;

      SetTitleChart();

      _year = year;
      _month = month;
      _week = week;

      string fileNameMonth = $"\\Months\\Bao_cao_tong_hop_{nameStation}_Thang{month}.pdf";
      string fileNameWeek = $"\\Weeks\\Bao_cao_tong_hop_{nameStation}_Tuan{week}.pdf";

      _pathReportLocalMonth = PathReportLocal + $"{fileNameMonth}";
      _pathReportOndriveMonth = PathReportOneDrive + $"{fileNameMonth}";

      _pathReportLocalWeek = PathReportLocal + $"{fileNameWeek}";
      _pathReportOndriveWeek = PathReportOneDrive + $"{fileNameWeek}";

      _permitExportMonth = isExportMonth;
      _permitExportWeek = isExportWeek;
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

    private void ExportToPdf(string pathReport)
    {
      // Tạo tài liệu PDF mới
      PdfDocument document = new PdfDocument();

      // Lặp qua từng TabPage trong TabControl
      foreach (TabPage tabPage in tabControl1.TabPages)
      {
        // Chụp ảnh TabPage
        Bitmap bitmap = CaptureTabPage(tabPage);

        // Lưu Bitmap vào tệp tạm thời
        string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bmp");
        bitmap.Save(tempImagePath);

        // Thêm một trang mới vào tài liệu PDF
        PdfPage page = document.AddPage();
        page.Width = 842;  // Kích thước chiều rộng của A4 trong landscape (842px)
        page.Height = 595; // Kích thước chiều cao của A4 trong landscape (595px)
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Tải hình ảnh từ tệp tạm thời
        XImage img = XImage.FromFile(tempImagePath);

        // Vẽ hình ảnh lên trang PDF
        gfx.DrawImage(img, 0, 0, page.Width, page.Height);

        // Xóa tệp tạm thời
        File.Delete(tempImagePath);
      }

      // Lưu tài liệu PDF vào đường dẫn
      document.Save(pathReport);
    }

    private Bitmap CaptureTabPage(TabPage tabPage)
    {
      // Chụp hình ảnh của TabPage và chuyển thành Bitmap
      Bitmap bitmap = new Bitmap(tabPage.Width, tabPage.Height);
      tabPage.DrawToBitmap(bitmap, new Rectangle(0, 0, tabPage.Width, tabPage.Height));
      return bitmap;
    }

    private void SetTitleChart()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetTitleChart();
        }));
        return;
      }

      //Month
      //Set Title Chart 
      this.chartTopError.SetTilte = "Top trung bình % mẫu lỗi";
      this.chartTopLoss.SetTilte = "Top trung bình % hao hụt";
      this.chartTopCpk.SetTilte = "Top trung bình Cpk";
      this.chartTopStdev.SetTilte = "Top trung bình Stdev";

      this.chartTopError.IsVisible(false);
      this.chartTopLoss.IsVisible(false);
      this.chartTopCpk.IsVisible(false);
      this.chartTopStdev.IsVisible(false);

      //Set Title Chart 
      this.chartAvgError.SetTilte = "Biểu đồ trung bình % mẫu lỗi";
      this.chartAvgLoss.SetTilte = "Biểu đồ trung bình % hao hụt";
      this.chartAvgCpk.SetTilte = "Biểu đồ trung bình Cpk";
      this.chartAvgStdev.SetTilte = "Biểu đồ trung bình Stdev";
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

    private List<DateTime> GetAllDaysInWeek(int year, int week)
    {
      List<DateTime> daysOfWeek = new List<DateTime>();
      DateTime jan1 = new DateTime(year, 1, 1);

      // Tìm ngày đầu tiên của tuần đầu tiên trong năm
      int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
      DateTime firstMonday = jan1.AddDays(daysOffset);

      // Tính toán ngày bắt đầu của tuần được chỉ định
      int daysToAdd = (week - 1) * 7;
      DateTime startDate = firstMonday.AddDays(daysToAdd);

      // Thêm các ngày của tuần vào danh sách
      for (int i = 0; i < 7; i++)
      {
        daysOfWeek.Add(startDate.AddDays(i));
      }

      return daysOfWeek;
    }


    private void ClearPage()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearPage();
        }));
        return;
      }

      for (int i = tabControl1.TabPages.Count - 1; i > 0; i--)
      {
        tabControl1.TabPages.RemoveAt(i);
      }

      //Dãy trên
      this.chartTopError.SetDataChart(null);
      this.chartTopLoss.SetDataChart(null);
      this.chartTopCpk.SetDataChart(null);
      this.chartTopStdev.SetDataChart(null);

      //Dãy dưới
      this.chartAvgError.SetDataChart(null, $"Tháng 0");
      this.chartAvgLoss.SetDataChart(null, $"Tháng 0");
      this.chartAvgCpk.SetDataChart(null, $"Tháng 0");
      this.chartAvgStdev.SetDataChart(null, $"Tháng 0");

      UpdateUIChartPie(chartPie, null);
    }


  }
}
