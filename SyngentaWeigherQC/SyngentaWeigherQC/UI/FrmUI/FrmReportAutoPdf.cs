using DocumentFormat.OpenXml.Bibliography;
using Irony.Parsing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Rectangle = iTextSharp.text.Rectangle;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmReportAutoPdf : Form
  {
    public FrmReportAutoPdf()
    {
      InitializeComponent();
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
    public FrmReportAutoPdf(int year, int month, int week, string PathReportLocal, string PathReportOneDrive, string nameStation, bool isExportMonth=true, bool isExportWeek = true)
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


    private void ReportAutoPdf_Load(object sender, EventArgs e)
    {
      timer1.Start();
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

    private void ReportStart()
    {
      try
      {
        if (_permitExportWeek)
        {
          List<DateTime> listDaysOfWeek = GetAllDaysInWeek(_year, _week);

          //Nếu đã có file report thì không làm gì hết
          if (!File.Exists(_pathReportLocalWeek) || !File.Exists(_pathReportOndriveWeek))
          {
            //SetTitleReport(eReportConsumption.Week);
            LoadDataReportAuToMonth(listDaysOfWeek, _week, eReportConsumption.Week);
            SetDataProductDetail(dataReport);

            if (!File.Exists(_pathReportLocalWeek))
            {
              ExportToPdf(_pathReportLocalWeek);
            }

            if (!File.Exists(_pathReportOndriveWeek))
            {
              ExportToPdf(_pathReportOndriveWeek);
            }
          }
        }  
        

        if (_permitExportMonth)
        {
          ClearPage();
          List<DateTime> listDaysOfMonth = GetAllDaysInMonth(_year, _month);
          if (!File.Exists(_pathReportLocalMonth) || !File.Exists(_pathReportOndriveMonth))
          {
            //SetTitleReport(eReportConsumption.Month);
            LoadDataReportAuToMonth(listDaysOfMonth, _month, eReportConsumption.Month);
            SetDataProductDetail(dataReport);

            if (!File.Exists(_pathReportLocalMonth))
            {
              ExportToPdf(_pathReportLocalMonth);
            }

            if (!File.Exists(_pathReportOndriveMonth))
            {
              ExportToPdf(_pathReportOndriveMonth);
            }
          }
        }  

      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
      finally { this.Close(); }
    }


    private List<DataReportByDate> dataReport = new List<DataReportByDate>();
    private async void LoadDataReportAuToMonth(List<DateTime> listDays, int numberMonthOrWeek, eReportConsumption eReportConsumption)
    {
      
    }

    


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
    private ChartLineData ProcessingDataChartLine(List<DatalogWeight> datalogs, List<Sample> samples, Models.Production productions)
    {
      if (datalogs?.Count<=0 || samples?.Count<=0) return null;

      List<double> averageRaw = new List<double>();
      double averageTotal = Math.Round(samples.Where(x=>x.isHasValue==true).Average(x => x.Value), 2);
      datalogs = datalogs.OrderBy(x => x.Id).ToList();
      foreach (DatalogWeight datalog in datalogs)
      {
        double average = samples.Where(x => x.DatalogId == datalog.Id && x.isHasValue==true).Average(x => x.Value);
        averageRaw.Add(Math.Round(average, 2));
      }
      ChartLineData chartLineData = new ChartLineData()
      {
        Min = productions.LowerLimitFinal,
        Max = productions.UpperLimitFinal,
        Target = productions.StandardFinal,
        Average = averageTotal,
        AverageRaw = averageRaw,
      };
      return chartLineData;
    }

    private StatisticalData DataProcessing(List<DatalogWeight> datalogs, List<Sample> samples)
    {
      return null;
      //if (datalogs?.Count <= 0 || samples?.Count <= 0) return null;

      //DatalogWeight datalogFirst = datalogs.FirstOrDefault();
      //List<double> valueSamples = samples?.Where(x => x.isHasValue == true).Select(x => x.Value).ToList();
      //Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == datalogFirst.ProductId).FirstOrDefault();

      //var numbersOver = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value > productions.UpperLimitFinal).Count() : 0;
      //var numbersLower = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value < productions.LowerLimitFinal).Count() : 0;

      //double stdev = AppCore.Ins.Stdev(valueSamples);
      //double average = Math.Round(valueSamples.Average(), 2);
      //double target = productions.StandardFinal;

      //string result = (average >= target) ? "ĐẠT" : "KHÔNG ĐẠT";

      //int totalSamples = samples.Count();
      //int numberSamplesOver = (int)samples?.Where(x => x.Value > productions.UpperLimitFinal).Count();
      //int numberSamplesLower = (int)samples?.Where(x => x.Value < productions.LowerLimitFinal).Count();

      //double rateError = (totalSamples != 0) ? (((numberSamplesOver + numberSamplesLower) * 100) / (totalSamples)) : 0;
      //rateError = Math.Round(rateError, 2);

      //double rateLoss = (average > target) ? Math.Round((((average - target) * 100) / target), 2) : 0;

      //StatisticalData statisticalData = new StatisticalData()
      //{
      //  Shift = datalogFirst.ShiftId,
      //  Stdev = stdev,
      //  Average = average,
      //  Target = productions.StandardFinal,
      //  Result = result,
      //  TotalSample = valueSamples.Count(),
      //  NumberSampleOver = numberSamplesOver,
      //  NumberSampleLower = numberSamplesLower,
      //  RateError = rateError,
      //  RateLoss = rateLoss,
      //};

      //return statisticalData;
    }


    private async Task<DataReportByDate> LoadDataProductByDate(DateTime dt_DB)
    {
      return null;
      ////Load tất cả Data ngày đó
      //List<DatalogWeight> allDatalogs = await AppCore.Ins.LoadAllDatalogs(dt_DB);
      //List<Sample> allSamples = await AppCore.Ins.LoadAllSamplesContainZero(dt_DB);

      //if (allDatalogs == null) return null;
      //if (allDatalogs.Count == 0) return null;

      //if (allDatalogs.Count > 0)
      //{
      //  //Các datalog theo sản phẩm
      //  Dictionary<int, List<DatalogWeight>> groupDatalogs = allDatalogs
      //    .GroupBy(x => x.ProductId)
      //    .ToDictionary(group => group.Key, group => group.ToList());

      //  List<DataDateGroupByProduct> dataByDates = new List<DataDateGroupByProduct>();
      //  foreach (var item in groupDatalogs)
      //  {
      //    List<DatalogWeight> listDatalog = item.Value.ToList();
      //    if (listDatalog.Count > 0)
      //    {
      //      int productId = listDatalog.FirstOrDefault().ProductId;
      //      List<int> listValueDatalogId = listDatalog.Select(x => x.Id).ToList();
      //      List<Sample> samples = AppCore.Ins.GetDataSampleByListIdDatalog(allSamples, listValueDatalogId);

      //      DataDateGroupByProduct dataDateGroupByProduct = new DataDateGroupByProduct()
      //      {
      //        ProductId = productId,
      //        Datalogs = listDatalog,
      //        Samples = samples,
      //      };
      //      dataByDates.Add(dataDateGroupByProduct);
      //    }
      //  }

      //  if (dataByDates.Count > 0)
      //  {
      //    DataReportByDate dataReportByDate = new DataReportByDate()
      //    {
      //      DateTime = dt_DB,
      //      DataDateGroupByProducts = dataByDates
      //    };

      //    return dataReportByDate;
      //  }
      //  else
      //  {
      //    return null;
      //  }
      //}
      //else
      //{
      //  return null;
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

    private void ExportToPdf(string pathReport)
    {
      try
      {
        // Kích thước của trang PDF (kích thước A4 nằm ngang)
        float pageWidth = PageSize.A4.Rotate().Width;
        float pageHeight = PageSize.A4.Rotate().Height;

        int numberTag = tabControl1.TabCount;
        // Tạo một tài liệu PDF
        using (Document document = new Document(new Rectangle(pageWidth, pageHeight)))
        {
          // Tạo một PdfWriter để ghi tài liệu vào một tệp
          using (FileStream fileStream = new FileStream(pathReport, FileMode.Create))
          {
            using (PdfWriter writer = PdfWriter.GetInstance(document, fileStream))
            {
              // Mở Document để bắt đầu ghi
              document.Open();
              for (int tageStart = 0; tageStart < numberTag; tageStart++)
              {
                tabControl1.SelectedIndex = tageStart;
                // Chụp ảnh của TableLayoutPanel
                //UcReport userControl = (UserControl)tabControl1.TabIndex[2];
                TabPage tabPage = tabControl1.TabPages[tageStart];
                Bitmap bitmapN = RenderControlToBitmap(tabPage);

                // Tạo đối tượng Image từ Bitmap
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmapN, System.Drawing.Imaging.ImageFormat.Bmp);
                image.ScaleAbsolute(PageSize.A4.Height, PageSize.A4.Width);
                image.SetAbsolutePosition(0, 0);

                // Vẽ ảnh lên trang PDF
                document.NewPage();
                document.Add(image);
              }

              document.Close();
            }
          }
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
      
    }


    private Bitmap RenderControlToBitmap(System.Windows.Forms.Control control)
    {
      Bitmap bitmap = new Bitmap(control.Width, control.Height);
      //control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, (int)PageSize.A4.Height, (int)PageSize.A4.Width));
      control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, 2500, 2500));
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

    private void timer1_Tick(object sender, EventArgs e)
    {
      ReportStart();
    }
  }
}
