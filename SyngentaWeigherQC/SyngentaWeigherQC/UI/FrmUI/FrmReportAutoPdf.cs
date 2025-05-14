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
    public FrmReportAutoPdf()
    {
      InitializeComponent();
    }

    public List<DatalogWeight> _datalogWeights = new List<DatalogWeight>();
    public string _pathReport = string.Empty;

    public FrmReportAutoPdf(List<DatalogWeight> datalogWeights, string pathReport, string titleReport) : this()
    {
      _datalogWeights = datalogWeights;
      _pathReport = pathReport;
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
                  NameShiftLeader = lastRecord.ShiftLeader.UserName,
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
        //this.Close(); 
      }
    }

    private void timer_export_Tick(object sender, EventArgs e)
    {
      //ExportToPdf(_pathReport);
      //ExportTabControlToPdf(tabControl1, _pathReport);
    }

    private void label9_Click(object sender, EventArgs e)
    {
      ExportToPdf(_pathReport);
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
      this.ucChartV1_avgSampleErrorMonthCurrent.SetDataChart(groupDataTopError);
      this.ucChartV1_avgSampleLossMonthCurrent.SetDataChart(groupDataTopLoss);
      this.ucChartV1_avgCpkMonthCurrent.SetDataChart(groupDataTopCpk);
      this.ucChartV1_avgStdevMonthCurrent.SetDataChart(groupDataTopStdev);


      //Dãy dưới
      this.ucChartV2_avgSampleErrorMonthCurrent.SetDataChart(groupDataAverageError);
      this.ucChartV2_avgSampleLossMonthCurrent.SetDataChart(groupDataAverageLoss);
      this.ucChartV2_avgCpkMonthCurrent.SetDataChart(groupDataAverageCpk);
      this.ucChartV2_avgStdevMonthCurrent.SetDataChart(groupDataAverageStdev);

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

      this.ucChartPieMothCurrent.SetDataChart(totalSample, totalLower, totalOver);


      this.ucSumaryMothCurrent.SetData(consumptionV2s.Count(), totalSample, totalLower + totalOver, totalOver);
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




    private void SetTitleReport(eReportConsumption eReportConsumption)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetTitleReport(eReportConsumption);
        }));
        return;
      }

      label9.Text = eReportConsumption == eReportConsumption.Month ?
        "ĐANG XUẤT BÁO CÁO THEO THÁNG TỰ ĐỘNG. VUI LÒNG CHỜ ĐỢI TRONG GIÂY LÁT ..." :
        "ĐANG XUẤT BÁO CÁO THEO TUẦN TỰ ĐỘNG. VUI LÒNG CHỜ ĐỢI TRONG GIÂY LÁT ...";
    }



    private List<DataReportByDate> dataReport = new List<DataReportByDate>();





    private void SetDataProductDetail(List<DataReportByDate> reportDataWeek)
    {
      //if (this.InvokeRequired)
      //{
      //  this.Invoke(new Action(() =>
      //  {
      //    SetDataProductDetail(reportDataWeek);
      //  }));
      //  return;
      //}

      //if (reportDataWeek.Count <= 0) return;

      //int cntTab = 0;
      ////Đi qua các Data theo ngày
      //foreach (var item in reportDataWeek)
      //{
      //  //Các sản phẩm ngày hôm đó
      //  DateTime dt_Report = item.DateTime;
      //  List<DataDateGroupByProduct> dataReportByDate = item.DataDateGroupByProducts.ToList();

      //  if (dataReportByDate.Count > 0)
      //  {
      //    //Đi từng sản phẩm
      //    foreach (var dataDateGroupByProduct in dataReportByDate)
      //    {
      //      List<DatalogWeight> datalogs = dataDateGroupByProduct.Datalogs.ToList();
      //      List<Sample> samples = dataDateGroupByProduct.Samples.ToList();

      //      if (datalogs.Count > 0)
      //      {
      //        DatalogWeight datalogLast = datalogs?.LastOrDefault();

      //        Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(s => s.Id == datalogLast.ProductId).FirstOrDefault();
      //        double maxProduct = productions.UpperLimitFinal;
      //        double target = productions.StandardFinal;
      //        double minProduct = productions.LowerLimitFinal;

      //        string nameUser = AppCore.Ins._listShiftLeader?.Where(x => x.Id == datalogLast.UserId).Select(x => x.UserName).FirstOrDefault();
      //        //Tạo class thông tin sp
      //        ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct()
      //        {
      //          NameLine = AppCore.Ins._stationCurrent.Name,
      //          ModeTare = (datalogLast.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn",
      //          ProductName = productions.Name,
      //          Standard = target,
      //          Upper = maxProduct,
      //          Lower = minProduct,
      //          NameShiftLeader = nameUser,
      //        };



      //        //Tách thành các Shift
      //        List<StatisticalData> statisticalDatas = new List<StatisticalData>();

      //        var listDatalogByShift = datalogs.GroupBy(x => x.ShiftId).OrderBy(x => x.Key).ToList();
      //        foreach (var data in listDatalogByShift)
      //        {
      //          List<int> listValueDatalogId = data.Select(d => d.Id).ToList();
      //          List<Sample> samplesChid = AppCore.Ins.GetDataSampleByListIdDatalog(samples, listValueDatalogId);


      //          //SetDataTable(item.ToList(), samplesChid, productions);

      //          StatisticalData statisticalData1 = DataProcessing(data.ToList(), samplesChid);
      //          if (statisticalData1 != null) statisticalDatas.Add(statisticalData1);

      //          //DataAnalysis(datalogsCurrentBtn);
      //        }

      //        ChartLineData chartLineData = ProcessingDataChartLine(datalogs, samples, productions);
      //        double averageTotal_Product = statisticalDatas.Select(x => x.Average).Average();

      //        //Tạo Tab
      //        string titleReport = $"BÁO CÁO SẢN PHẨM: {productions.Name} - NGÀY: {dt_Report.ToString("dd/MM/yyyy")}";

      //        TabPage newTabPage = new TabPage();
      //        newTabPage.Text = $"SP {++cntTab} trong tuần hiện tại";

      //        UcReport ucReport = new UcReport();
      //        ucReport.Name = $"SP{cntTab}";
      //        ucReport.Dock = DockStyle.Fill;

      //        ucReport.SetDataInfor(excelClassInforProduct, titleReport);

      //        ucReport.StatisticalProductUI(statisticalDatas);
      //        ucReport.SetDataTable(datalogs, samples);
      //        ucReport.SetChartHistogram(samples, productions);
      //        ucReport.SetChartLine(chartLineData);
      //        ucReport.SetResultFinal(averageTotal_Product >= target);

      //        newTabPage.Controls.Add(ucReport);
      //        tabControl1.TabPages.Add(newTabPage);
      //      }
      //    }
      //  }

      //}

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

    //private void ExportToPdf(string pathReport)
    //{
    //  try
    //  {
    //    // Kích thước của trang PDF (kích thước A4 nằm ngang)
    //    float pageWidth = PageSize.A4.Rotate().Width;
    //    float pageHeight = PageSize.A4.Rotate().Height;

    //    int numberTag = tabControl1.TabCount;
    //    // Tạo một tài liệu PDF
    //    using (Document document = new Document(new Rectangle(pageWidth, pageHeight)))
    //    {
    //      // Tạo một PdfWriter để ghi tài liệu vào một tệp
    //      using (FileStream fileStream = new FileStream(pathReport, FileMode.Create))
    //      {
    //        using (PdfWriter writer = PdfWriter.GetInstance(document, fileStream))
    //        {
    //          // Mở Document để bắt đầu ghi
    //          document.Open();
    //          for (int tageStart = 0; tageStart < numberTag; tageStart++)
    //          {
    //            tabControl1.SelectedIndex = tageStart;
    //            // Chụp ảnh của TableLayoutPanel
    //            //UcReport userControl = (UserControl)tabControl1.TabIndex[2];
    //            TabPage tabPage = tabControl1.TabPages[tageStart];
    //            Bitmap bitmapN = RenderControlToBitmap(tabPage);

    //            // Tạo đối tượng Image từ Bitmap
    //            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmapN, System.Drawing.Imaging.ImageFormat.Bmp);
    //            image.ScaleAbsolute(PageSize.A4.Height, PageSize.A4.Width);
    //            image.SetAbsolutePosition(0, 0);

    //            // Vẽ ảnh lên trang PDF
    //            document.NewPage();
    //            document.Add(image);
    //          }

    //          document.Close();
    //        }
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    LoggerHelper.LogErrorToFileLog(ex);
    //  }

    //}

    //public void ExportTabControlToPdf(TabControl tabControl, string pdfPath)
    //{
    //  PdfDocument pdf = new PdfDocument();

    //  // Lưu tab hiện tại để phục hồi sau khi chụp xong
    //  int originalIndex = tabControl.SelectedIndex;

    //  for (int i = 0; i < tabControl.TabPages.Count; i++)
    //  {
    //    tabControl.SelectedIndex = i;
    //    Application.DoEvents(); // Bắt buộc phải cập nhật UI

    //    Bitmap bmp = new Bitmap(tabControl.TabPages[i].Width, tabControl.TabPages[i].Height);
    //    tabControl.TabPages[i].DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height));

    //    PdfPage page = pdf.AddPage();
    //    page.Width = XUnit.FromPoint(bmp.Width);
    //    page.Height = XUnit.FromPoint(bmp.Height);

    //    using (XGraphics gfx = XGraphics.FromPdfPage(page))
    //    {
    //      using (XImage image = XImage.FromGdiPlusImage(bmp))
    //      {
    //        gfx.DrawImage(image, 0, 0);
    //      }
    //    }
    //  }

    //  // Khôi phục lại tab ban đầu
    //  tabControl.SelectedIndex = originalIndex;

    //  pdf.Save(pdfPath);
    //  MessageBox.Show("Xuất PDF thành công!");
    //}

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
      this.ucChartV1_avgSampleErrorMonthCurrent.SetTilte = "Top trung bình % mẫu lỗi";
      this.ucChartV1_avgSampleLossMonthCurrent.SetTilte = "Top trung bình % hao hụt";
      this.ucChartV1_avgCpkMonthCurrent.SetTilte = "Top trung bình Cpk";
      this.ucChartV1_avgStdevMonthCurrent.SetTilte = "Top trung bình Stdev";

      this.ucChartV1_avgSampleErrorMonthCurrent.IsVisible(false);
      this.ucChartV1_avgSampleLossMonthCurrent.IsVisible(false);
      this.ucChartV1_avgCpkMonthCurrent.IsVisible(false);
      this.ucChartV1_avgStdevMonthCurrent.IsVisible(false);

      //Set Title Chart 
      this.ucChartV2_avgSampleErrorMonthCurrent.SetTilte = "Biểu đồ trung bình % mẫu lỗi";
      this.ucChartV2_avgSampleLossMonthCurrent.SetTilte = "Biểu đồ trung bình % hao hụt";
      this.ucChartV2_avgCpkMonthCurrent.SetTilte = "Biểu đồ trung bình Cpk";
      this.ucChartV2_avgStdevMonthCurrent.SetTilte = "Biểu đồ trung bình Stdev";
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


  }
}
