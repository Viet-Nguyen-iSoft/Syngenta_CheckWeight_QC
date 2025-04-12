using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SixLabors.Fonts;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.Filter;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Color = System.Drawing.Color;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;
using Rectangle = iTextSharp.text.Rectangle;


namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmConsumption : Form
  {
    public FrmConsumption()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmConsumption _Instance = null;
    public static FrmConsumption Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmConsumption();
        }
        return _Instance;
      }
    }
    #endregion



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


    private int year = 0;
    private int month = 0;

    //Home
    private List<DateTime> listDateHome = new List<DateTime>();
    private int monthCurrent = DateTime.Now.Month;
    private int weekCurrent = 0;

    private void FrmConsumption_Load(object sender, EventArgs e)
    {
      SetTitleChart();

      // Lấy ngày đầu tiên của tuần
      DateTime dt_curent = DateTime.Now;
      month = dt_curent.Month;
      year = dt_curent.Year;

      CultureInfo cul = CultureInfo.CurrentCulture;
      int week = cul.Calendar.GetWeekOfYear(dt_curent, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
      weekCurrent = week;

      List<DateTime> listDaysOfMonth = GetAllDaysInMonth(year, month);
      List<DateTime> listDaysOfWeek = GetAllDaysInWeek(year, week);

      listDateHome = listDaysOfMonth;

      DataHome(listDaysOfMonth);
      DataMonthCurrent(listDaysOfMonth,month, eReportConsumption.Month);
      DataMonthCurrent(listDaysOfWeek, weekCurrent, eReportConsumption.Week);

      SetDataProductDetail(reportDataWeek);
    }

    private List<DataReportByDate> reportDataWeek = new List<DataReportByDate>();

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


    
    private async void DataHome(List<DateTime> listDays)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          DataHome(listDays);
        }));
        return;
      }

      try
      {
        //Đi từng DB
        //Data theo từng ngày
        List<DataReportByDate> dataReportByDates = new List<DataReportByDate>();
        for (int i = 0; i < listDays.Count; i++)
        {
          string fileDB = Application.StartupPath + $"\\Database\\{listDays[i].ToString("yyyyMMdd")}_data_log.sqlite";
          if (!File.Exists(fileDB)) continue;

          DataReportByDate report = await LoadDataProductByDate(listDays[i], listCodeShiftFilter);

          if (report != null)
          {
            if (report.DataDateGroupByProducts.Count > 0)
              dataReportByDates.Add(report);
          }
        }


        List<GroupProductData> groupProductDatas = new List<GroupProductData>();
        if (dataReportByDates.Count > 0)
        {
          List<Consumption> listConsumption = new List<Consumption>();
          int stt = 1;
          //Phan tichs các sp theo từng ngày
          foreach (var item in dataReportByDates)
          {
            //Các product trong ngày
            List<DataDateGroupByProduct> dataDateGroupByProducts = item.DataDateGroupByProducts;

            if (dataDateGroupByProducts.Count > 0)
            {
              //Đi từng Product mỗi ngày
              foreach (var listDataProduct in dataDateGroupByProducts)
              {
                List<Datalog> datalogs = listDataProduct.Datalogs;
                List<Sample> samples = listDataProduct.Samples;
                List<int> listValueDatalogsId = new List<int>();
                List<double> listValueSamples = new List<double>();

                //Đi từng Shift
                if (datalogs.Count > 0)
                {
                  var datalogGroupByShifts = datalogs.GroupBy(x => x.ShiftId).ToList();

                  foreach (var listDatalogByShift in datalogGroupByShifts)
                  {
                    datalogs = listDatalogByShift.ToList();
                    listValueDatalogsId = datalogs.Select(x => x.Id).ToList();
                    List<Sample> samplesChid = AppCore.Ins.GetDataSampleByListIdDatalog(samples, listValueDatalogsId);

                    List<Sample> samplesChid_NoZero = samplesChid.Where(x => x.isEnable == true && x.isHasValue).ToList();
                    listValueSamples = samplesChid_NoZero.Select(x => x.Value).ToList();

                    Datalog datalogLast = datalogs.LastOrDefault();
                    Models.Production production = AppCore.Ins._listAllProductsContainIsDelete.Where(x => x.Id == datalogLast.ProductId).FirstOrDefault();

                    if (production == null) return;

                    string nameShift = AppCore.Ins._listShift?.Where(x => x.CodeShift == datalogLast.ShiftId)
                                                              .Select(x => x.Name).FirstOrDefault();
                    string date = ((DateTime)datalogLast.CreatedAt).ToString("dd-MM-yyyy");
                    string nameProduct = production.Name;

                    double avgMeasure = Math.Round(listValueSamples.Average(), 2);
                    double maxMeasure = listValueSamples.Max();
                    double minMeasure = listValueSamples.Min();

                    double targetProduct = production.StandardFinal;
                    double maxProduct = production.UpperLimitFinal;
                    double minProduct = production.LowerLimitFinal;

                    Consumption consumption = new Consumption()
                    {
                      ProductId = datalogLast.ProductId,
                      Shift = nameShift,
                      STT = stt++,
                      DateTime = date,
                      Production = nameProduct,
                      AverageMeasure = avgMeasure,
                      MaxMeasure = maxMeasure,
                      MinMeasure = minMeasure,
                      Target = targetProduct,
                      UpperProduct = maxProduct,
                      LowerProduct = minProduct,
                      Evaluate = (avgMeasure >= targetProduct) ? "PASS" : "FAIL",

                      //Datalogs = datalogs,
                      SamplesValue = listValueSamples,
                    };

                    listConsumption.Add(consumption);
                  }
                }
              }
            }
          }

          //Gộp listConsumption theo Product 
          var listConsumptionsByProductId= listConsumption.GroupBy(x=>x.ProductId).ToList();
          DataChartReport dataChartReport = new DataChartReport();

          if (listConsumptionsByProductId.Count>0)
          {
            Dictionary<string, double> D_Cpk = new Dictionary<string, double>();
            Dictionary<string, double> D_Stdev = new Dictionary<string, double>();
            Dictionary<string, double> D_RateError = new Dictionary<string, double>();
            Dictionary<string, double> D_RateLoss = new Dictionary<string, double>();

            int numbersSample = 0;
            int numberSampleOver = 0;
            int numberSampleLower = 0;

            foreach (var item in listConsumptionsByProductId)
            {
              var data = item.ToList();
              if (data.Count>0)
              {
                int productId = data.FirstOrDefault().ProductId;
                Models.Production productionValue = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == productId)?.FirstOrDefault();
                if (productionValue == null) continue;

                string productionName = productionValue.Name + $" [{productId}]";

                List<double> allSamplesValue = data.SelectMany(x => x.SamplesValue).ToList();
                double stdevValue = AppCore.Ins.Stdev(allSamplesValue);
                double averageValue = allSamplesValue.Average();
                //double minValue = allSamplesValue.Min();
                //double maxValue = allSamplesValue.Max();

                //double Cp_Value = (stdevValue != 0) ? ((maxValue - minValue) / (6 * stdevValue)) : 0;
                //double Cpk_H_Value = (stdevValue != 0) ? ((maxValue - averageValue) / (3 * stdevValue)) : 0;
                //double Cpk_L_Value = (stdevValue != 0) ? ((averageValue - minValue) / (3 * stdevValue)) : 0;
                double Cpk_H_Value = (stdevValue != 0) ? ((productionValue.UpperLimitFinal - averageValue) / (3 * stdevValue)) : 0;
                double Cpk_L_Value = (stdevValue != 0) ? ((averageValue - productionValue.LowerLimitFinal) / (3 * stdevValue)) : 0;
                double Cpk_Value = Math.Min(Cpk_H_Value, Cpk_L_Value);

                int numberSamples = allSamplesValue.Count();
                int numbersOver = allSamplesValue.Count(x => x > productionValue.UpperLimitFinal);
                int numbersLower= allSamplesValue.Count(x => x < productionValue.LowerLimitFinal);

                double rateError = (numberSamples != 0) ? ((double)((numbersLower) * 100) / (double)(numberSamples)) : 0;
                //double rateLoss = (averageValue > productionValue.StandardFinal) ? Math.Round((((averageValue - productionValue.StandardFinal) * 100) / productionValue.StandardFinal), 2) : 0;
                double rateLoss = (numberSamples != 0) ? ((double)((numbersOver) * 100) / (double)(numberSamples)) : 0;

                D_Cpk.Add(productionName, Cpk_Value);
                D_Stdev.Add(productionName, stdevValue);
                D_RateError.Add(productionName, rateError);
                D_RateLoss.Add(productionName, rateLoss);

                numbersSample += numberSamples;
                numberSampleOver += numbersOver;
                numberSampleLower+= numbersLower;
              }  
            }



            dataChartReport = new DataChartReport()
            {
              Stdev = D_Stdev,
              Cpk = D_Cpk,
              RateError = D_RateError,
              RateLoss = D_RateLoss,
              NumberSampleTotal = numbersSample,
              NumberSampleLower = numberSampleLower,
              NumberSampleUpper = numberSampleOver
            };

          }

          //Consumption
          UpdateUITable(listConsumption);
          UpdateUIChartPie(ChartPieHome, dataChartReport);

          //Dãy trên
          this.chartErrorHome.SetDataChart(dataChartReport.RateError);
          this.chartLossHome.SetDataChart(dataChartReport.RateLoss);
          this.chartCpkHome.SetDataChart(dataChartReport.Cpk);
          this.chartStdevHome.SetDataChart(dataChartReport.Stdev);

          //Dãy dưới
          this.chartavgErrorHome.SetDataChart(dataChartReport.RateError, $"Trung bình");
          this.chartavgLossHome.SetDataChart(dataChartReport.RateLoss, $"Trung bình");
          this.chartavgCpkHome.SetDataChart(dataChartReport.Cpk, $"Trung bình");
          this.chartavgStdevHome.SetDataChart(dataChartReport.Stdev, $"Trung bình");

        }
        else
        {
          ClearHome();
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }


    private async void DataMonthCurrent(List<DateTime> listDays,int numberMonthOrWeek, eReportConsumption eReportConsumption)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          DataMonthCurrent(listDays, numberMonthOrWeek, eReportConsumption);
        }));
        return;
      }

      try
      {
        //Đi từng DB
        //Data theo từng ngày
        List<DataReportByDate> dataReportByDates = new List<DataReportByDate>();
        for (int i = 0; i < listDays.Count; i++)
        {
          string fileDB = Application.StartupPath + $"\\Database\\{listDays[i].ToString("yyyyMMdd")}_data_log.sqlite";
          if (!File.Exists(fileDB)) continue;

          DataReportByDate report = await LoadDataProductByDate(listDays[i], listCodeShiftFilter);

          if (report != null)
          {
            if (report.DataDateGroupByProducts.Count > 0)
              dataReportByDates.Add(report);
          }
        }

        if (eReportConsumption == eReportConsumption.Week)
        {
          reportDataWeek = dataReportByDates;
        }

        List<GroupProductData> groupProductDatas = new List<GroupProductData>();
        if (dataReportByDates.Count > 0)
        {
          List<Consumption> listConsumption = new List<Consumption>();
          int stt = 1;
          //Phan tichs các sp theo từng ngày
          foreach (var item in dataReportByDates)
          {
            //Các product trong ngày
            List<DataDateGroupByProduct> dataDateGroupByProducts = item.DataDateGroupByProducts;

            if (dataDateGroupByProducts.Count > 0)
            {
              //Đi từng Product mỗi ngày
              foreach (var listDataProduct in dataDateGroupByProducts)
              {
                List<Datalog> datalogs = listDataProduct.Datalogs;
                List<Sample> samples = listDataProduct.Samples;
                List<int> listValueDatalogsId = new List<int>();
                List<double> listValueSamples = new List<double>();

                //Đi từng Shift
                if (datalogs.Count > 0)
                {
                  var datalogGroupByShifts = datalogs.GroupBy(x => x.ShiftId).ToList();

                  foreach (var listDatalogByShift in datalogGroupByShifts)
                  {
                    datalogs = listDatalogByShift.ToList();
                    listValueDatalogsId = datalogs.Select(x => x.Id).ToList();
                    List<Sample> samplesChid = AppCore.Ins.GetDataSampleByListIdDatalog(samples, listValueDatalogsId);

                    List<Sample> samplesChid_NoZero = samplesChid.Where(x => x.isEnable == true && x.isHasValue).ToList();
                    listValueSamples = samplesChid_NoZero.Select(x => x.Value).ToList();

                    Datalog datalogLast = datalogs.LastOrDefault();
                    Models.Production production = AppCore.Ins._listAllProductsContainIsDelete.Where(x => x.Id == datalogLast.ProductId).FirstOrDefault();

                    string nameShift = AppCore.Ins._listShift?.Where(x => x.CodeShift == datalogLast.ShiftId)
                                                              .Select(x => x.Name).FirstOrDefault();
                    string date = ((DateTime)datalogLast.CreatedAt).ToString("dd-MM-yyyy");
                    string nameProduct = production.Name;

                    double avgMeasure = Math.Round(listValueSamples.Average(), 2);
                    double maxMeasure = listValueSamples.Max();
                    double minMeasure = listValueSamples.Min();

                    double targetProduct = production.StandardFinal;
                    double maxProduct = production.UpperLimitFinal;
                    double minProduct = production.LowerLimitFinal;

                    Consumption consumption = new Consumption()
                    {
                      ProductId = datalogLast.ProductId,
                      Shift = nameShift,
                      STT = stt++,
                      DateTime = date,
                      Production = nameProduct,
                      AverageMeasure = avgMeasure,
                      MaxMeasure = maxMeasure,
                      MinMeasure = minMeasure,
                      Target = targetProduct,
                      UpperProduct = maxProduct,
                      LowerProduct = minProduct,
                      Evaluate = (avgMeasure >= targetProduct) ? "PASS" : "FAIL",

                      //Datalogs = datalogs,
                      SamplesValue = listValueSamples,
                    };

                    listConsumption.Add(consumption);
                  }
                }
              }
            }
          }


          //Gộp listConsumption theo Product 
          var listConsumptionsByProductId = listConsumption.GroupBy(x => x.ProductId).ToList();
          DataChartReport dataChartReport = new DataChartReport();

          if (listConsumptionsByProductId.Count > 0)
          {
            Dictionary<string, double> D_Cpk = new Dictionary<string, double>();
            Dictionary<string, double> D_Stdev = new Dictionary<string, double>();
            Dictionary<string, double> D_RateError = new Dictionary<string, double>();
            Dictionary<string, double> D_RateLoss = new Dictionary<string, double>();

            int numbersSample = 0;
            int numberSampleOver = 0;
            int numberSampleLower = 0;

            foreach (var item in listConsumptionsByProductId)
            {
              var data = item.ToList();
              if (data.Count > 0)
              {
                int productId = data.FirstOrDefault().ProductId;
                Models.Production productionValue = AppCore.Ins._listAllProductsContainIsDelete.Where(x => x.Id == productId).FirstOrDefault();
                string productionName = productionValue.Name + $" [{productId}]";

                List<double> allSamplesValue = data.SelectMany(x => x.SamplesValue).ToList();
                double stdevValue = AppCore.Ins.Stdev(allSamplesValue);
                double averageValue = allSamplesValue.Average();
                double minValue = allSamplesValue.Min();
                double maxValue = allSamplesValue.Max();

                //double Cp_Value = (stdevValue != 0) ? ((maxValue - minValue) / (6 * stdevValue)) : 0;
                //double Cpk_H_Value = (stdevValue != 0) ? ((maxValue - averageValue) / (3 * stdevValue)) : 0;
                //double Cpk_L_Value = (stdevValue != 0) ? ((averageValue - minValue) / (3 * stdevValue)) : 0;
                double Cpk_H_Value = (stdevValue != 0) ? ((productionValue.UpperLimitFinal - averageValue) / (3 * stdevValue)) : 0;
                double Cpk_L_Value = (stdevValue != 0) ? ((averageValue - productionValue.LowerLimitFinal) / (3 * stdevValue)) : 0;
                double Cpk_Value = Math.Min(Cpk_H_Value, Cpk_L_Value);

                int numberSamples = allSamplesValue.Count();
                int numbersOver = allSamplesValue.Count(x => x > productionValue.UpperLimitFinal);
                int numbersLower = allSamplesValue.Count(x => x < productionValue.LowerLimitFinal);

                double rateError = (numberSamples != 0) ? ((double)((numbersLower) * 100) / (double)(numberSamples)) : 0;
                //double rateLoss = (averageValue > productionValue.StandardFinal) ? Math.Round((((averageValue - productionValue.StandardFinal) * 100) / productionValue.StandardFinal), 2) : 0;
                double rateLoss = (numberSamples != 0) ? ((double)((numbersOver) * 100) / (double)(numberSamples)) : 0;

                D_Cpk.Add(productionName, Cpk_Value);
                D_Stdev.Add(productionName, stdevValue);
                D_RateError.Add(productionName, rateError);
                D_RateLoss.Add(productionName, rateLoss);

                numbersSample += numberSamples;
                numberSampleOver += numbersOver;
                numberSampleLower += numbersLower;
              }
            }

            dataChartReport = new DataChartReport()
            {
              Stdev = D_Stdev,
              Cpk = D_Cpk,
              RateError = D_RateError,
              RateLoss = D_RateLoss,
              NumberSampleTotal = numbersSample,
              NumberSampleLower = numberSampleLower,
              NumberSampleUpper = numberSampleOver
            };

          }

          if (eReportConsumption == eReportConsumption.Month)
          {
            //Dãy trên
            this.ucChartV1_avgSampleErrorMonthCurrent.SetDataChart(dataChartReport.RateError);
            this.ucChartV1_avgSampleLossMonthCurrent.SetDataChart(dataChartReport.RateLoss);
            this.ucChartV1_avgCpkMonthCurrent.SetDataChart(dataChartReport.Cpk);
            this.ucChartV1_avgStdevMonthCurrent.SetDataChart(dataChartReport.Stdev);

            //Dãy dưới
            this.ucChartV2_avgSampleErrorMonthCurrent.SetDataChart(dataChartReport.RateError, $"Tháng {numberMonthOrWeek}");
            this.ucChartV2_avgSampleLossMonthCurrent.SetDataChart(dataChartReport.RateLoss, $"Tháng {numberMonthOrWeek}");
            this.ucChartV2_avgCpkMonthCurrent.SetDataChart(dataChartReport.Cpk, $"Tháng {numberMonthOrWeek}");
            this.ucChartV2_avgStdevMonthCurrent.SetDataChart(dataChartReport.Stdev, $"Tháng {numberMonthOrWeek}");

            UpdateUIChartPie(ucChartPieMothCurrent, dataChartReport);

            this.ucSumaryMothCurrent.SetData(listConsumptionsByProductId.Count, dataChartReport.NumberSampleTotal, dataChartReport.NumberSampleLower, dataChartReport.NumberSampleUpper);

            this.lbTitleMonth.Text = $"BÁO CÁO TỔNG HỢP THÁNG {numberMonthOrWeek.ToString("00")}";
          }
          else
          {
            //Dãy trên
            this.Chart1WeekCurrentError.SetDataChart(dataChartReport.RateError);
            this.Chart1WeekCurrentLoss.SetDataChart(dataChartReport.RateLoss);
            this.Chart1WeekCurrentCpk.SetDataChart(dataChartReport.Cpk);
            this.Chart1WeekCurrentStdev.SetDataChart(dataChartReport.Stdev);

            //Dãy dưới
            this.Chart2WeekCurrentError.SetDataChart(dataChartReport.RateError, $"Tuần {numberMonthOrWeek}");
            this.Chart2WeekCurrentLoss.SetDataChart(dataChartReport.RateLoss, $"Tuần {numberMonthOrWeek}");
            this.Chart2WeekCurrentCpk.SetDataChart(dataChartReport.Cpk, $"Tuần {numberMonthOrWeek}");
            this.Chart2WeekCurrentStdev.SetDataChart(dataChartReport.Stdev, $"Tuần {numberMonthOrWeek}");

            UpdateUIChartPie(Chart2PieWeekCurrent, dataChartReport);
            
            this.SumaryWeekCurrent.SetData(listConsumptionsByProductId.Count, dataChartReport.NumberSampleTotal, dataChartReport.NumberSampleLower, dataChartReport.NumberSampleUpper);

            this.lbTitleReportWeek.Text = $"BÁO CÁO TỔNG HỢP TUẦN {numberMonthOrWeek.ToString("00")}";
          }
        }
        else
        {
          if (eReportConsumption == eReportConsumption.Month)
          {
            this.lbTitleMonth.Text = $"BÁO CÁO TỔNG HỢP THÁNG {numberMonthOrWeek.ToString("00")}";
            ClearMonth();
          }
         
          else if (eReportConsumption == eReportConsumption.Week)
          {
            this.lbTitleReportWeek.Text = $"BÁO CÁO TỔNG HỢP TUẦN {numberMonthOrWeek.ToString("00")}";
            ClearWeek();
          }  
            
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }

    }

    private void UpdateUIChartPie(UcChartPie ucChartPie,DataChartReport dataChartReport)
    {
      int total = 0;
      int over = 0;
      int lower = 0;
      if (dataChartReport!=null)
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

      if (datalogId.Count>0)
      {
        for (int i = 0; i < datalogId.Count; i++)
        {
          List<Sample> samplesChid = samples?.Where(x => x.DatalogId == datalogId[i]).ToList();
          if (samplesChid!=null)
          {
            if (samplesChid.Count > 0)
              listSample.AddRange(samplesChid);
          }  
        }
      }  
      return listSample;
    }


    private void UpdateUITable(List<Consumption> consumptions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateUITable(consumptions);
        }));
        return;
      }

      dataGridViewTableHome.Rows.Clear();
      if (consumptions.Count>0)
      {
        foreach (Consumption consumption in consumptions)
        {
          int row = dataGridViewTableHome.Rows.Add();
          dataGridViewTableHome.Rows[row].Cells["Column1"].Value = consumption.Shift;
          dataGridViewTableHome.Rows[row].Cells["Column2"].Value = consumption.STT;
          dataGridViewTableHome.Rows[row].Cells["Column3"].Value = consumption.DateTime;
          dataGridViewTableHome.Rows[row].Cells["Column4"].Value = consumption.Production;
          dataGridViewTableHome.Rows[row].Cells["Column5"].Value = consumption.AverageMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column6"].Value = consumption.MinMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column7"].Value = consumption.MaxMeasure;
          dataGridViewTableHome.Rows[row].Cells["Column8"].Value = consumption.Target;
          dataGridViewTableHome.Rows[row].Cells["Column9"].Value = consumption.LowerProduct;
          dataGridViewTableHome.Rows[row].Cells["Column10"].Value = consumption.UpperProduct;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Value = consumption.Evaluate;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Style.BackColor = (consumption.Evaluate=="PASS")? Color.Green:Color.Red;
          dataGridViewTableHome.Rows[row].Cells["Column11"].Style.ForeColor = Color.White;
        }  
      }  
    }


    //private void SetDataDetail()
    //{
    //  if (this.InvokeRequired)
    //  {
    //    this.Invoke(new Action(() =>
    //    {
    //      SetDataDetail();
    //    }));
    //    return;
    //  }

    //  int cnt = 0;
    //  DateTime dt_Report = DateTime.Now;
    //  if (reportDataWeek.Count > 0)
    //  {
    //    //ĐI report từng ngày
    //    for (int i = 0; i < reportDataWeek.Count; i++)
    //    {
    //      dt_Report = reportDataWeek[i].DateTime;
    //      List<GroupProductData> groupProductData = reportDataWeek[i].GroupProductDatas;
    //      if (groupProductData.Count > 0)
    //      {
    //        List<Datalog> datalogs = new List<Datalog>();
    //        List<Sample> samples = new List<Sample>();
    //        for (int j = 0; j < groupProductData.Count; j++)
    //        {
    //          datalogs.AddRange(groupProductData[j].Datalogs);
    //          samples.AddRange(groupProductData[j].Samples);
    //        }

    //        if (datalogs.Count > 0)
    //        {
    //          var groupedByFGs = datalogs.GroupBy(x => x.ProductId);

    //          foreach (var item in groupedByFGs)
    //          {
    //            List<Datalog> listDatalogsByProductId = item.ToList();
    //            Datalog datalogLast = listDatalogsByProductId?.LastOrDefault();

    //            Productions productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(s => s.Id == datalogLast.ProductId).FirstOrDefault();
    //            double uppper_product_shift = productions.PacksizeUpperLimitFinal;
    //            double standard_product_shift = productions.PacksizeStandardFinal;
    //            double lower_product_shift = productions.PacksizeLowerLimitFinal;

    //            string nameUser = AppCore.Ins._listUsers?.Where(x => x.Id == datalogLast.UserId).Select(x => x.UserName).FirstOrDefault();
    //            //Tạo class thông tin sp
    //            ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct()
    //            {
    //              NameLine = AppCore.Ins._stationCurrent.Name,
    //              ModeTare = (datalogLast.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn",
    //              ProductName = productions.Name,
    //              PackSize = productions.StandardCapacity,
    //              Standard = productions.PacksizeStandardFinal,
    //              Upper = productions.PacksizeUpperLimitFinal,
    //              Lower = productions.PacksizeLowerLimitFinal,
    //              NameShiftLeader = nameUser,
    //            };

    //            //Lấy hết sample tương ứng
    //            List<int> listGroupId = item.ToList()?.Select(x => x.Id).ToList();
    //            List<Sample> sampleDatalogs = new List<Sample>();
    //            foreach (var idDatalog in listGroupId)
    //            {
    //              List<Sample> sample = samples?.Where(x => x.DatalogId == idDatalog && x.isEnable == true && x.isHasValue == true).ToList();
    //              if (sample.Count > 0)
    //                sampleDatalogs.AddRange(sample);
    //            }


    //            //Phần tách riêng tính toán theo ca
    //            List<StatisticalData> statisticalDatas = new List<StatisticalData>();
    //            var groupedDatalogByShift = listDatalogsByProductId.GroupBy(x => x.ShiftId).ToList();
    //            foreach (var group in groupedDatalogByShift)
    //            {
    //              List<Datalog> listDatalogs_Chid = group.ToList();
    //              List<Sample> listSamples_Chid = new List<Sample>();
    //              foreach (var item2 in listDatalogs_Chid)
    //              {
    //                List<Sample> samples_chid = sampleDatalogs?.Where(x => x.DatalogId == item2.Id).ToList();
    //                if (samples_chid.Count > 0)
    //                  listSamples_Chid.AddRange(samples_chid);
    //              }


    //              //Lấy 1 datalog cuối phân tích
    //              Datalog datalogLast_Chid = listDatalogs_Chid.LastOrDefault();

    //              int total = (int)listSamples_Chid.Count;
    //              int numbersUpper = (int)((listSamples_Chid != null) ? listSamples_Chid?.Where(x => x.Value > uppper_product_shift).Count() : 0);
    //              int numbersLower = (int)((listSamples_Chid != null) ? listSamples_Chid?.Where(x => x.Value < lower_product_shift).Count() : 0);
    //              double averageSample = (listSamples_Chid != null) ? listSamples_Chid.Average(x=>x.Value) : 0;
    //              double stdev = (listSamples_Chid != null) ? AppCore.Ins.Stdev(listSamples_Chid.Select(x => x.Value).ToList()) : 0;

    //              double rateError = (total != 0) ? Math.Round(((double)((numbersUpper + numbersLower) * 100) / (total)), 3) : 0;
    //              double loss = (averageSample > standard_product_shift) ? Math.Round((((averageSample - standard_product_shift) * 100) / standard_product_shift), 2) : 0;
    //              int shiftId = datalogLast_Chid.ShiftId;

    //              StatisticalData statisticalData = new StatisticalData()
    //              {
    //                Shift = shiftId,
    //                Stdev = stdev,
    //                Average = averageSample,
    //                Target = standard_product_shift,
    //                Result = (averageSample >= standard_product_shift) ? "Đạt" : "Không đạt",
    //                TotalSample = total,
    //                NumberSampleOver = numbersUpper,
    //                NumberSampleLower = numbersLower,
    //                RateError = rateError,
    //                RateLoss = loss,
    //              };
    //              statisticalDatas.Add(statisticalData);
    //            }


    //            string titleReport = $"BÁO CÁO SẢN PHẨM: {productions.Name} - NGÀY: {dt_Report.ToString("dd/MM/yyyy")}";
    //            //Tạo Tab
    //            TabPage newTabPage = new TabPage();
    //            newTabPage.Text = $"SP {++cnt} trong tuần hiện tại";

    //            UcReport ucReport = new UcReport();
    //            ucReport.Name = $"SP{cnt}";
    //            ucReport.Dock = DockStyle.Fill;

    //            ucReport.SetDataInfor(excelClassInforProduct, titleReport);
    //            ucReport.StatisticalProductUI(statisticalDatas);
    //            ucReport.SetDataTable(item.ToList(), samples);
    //            //ucReport.SetChartHistogram(sampleDatalogs, productions.PacksizeLowerLimitFinal, productions.PacksizeUpperLimitFinal);
    //            //ucReport.SetChartLine(item.ToList());

    //            newTabPage.Controls.Add(ucReport);
    //            tabControl1.TabPages.Add(newTabPage);

    //          }
    //        }
    //      }
    //    }

    //  }
    //}

    private void SetDataProductDetail(List<DataReportByDate> reportDataWeek)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataProductDetail(reportDataWeek);
        }));
        return;
      }

      if (reportDataWeek.Count <= 0) return;

      int cntTab = 0;
      //Đi qua các Data theo ngày
      foreach (var item in reportDataWeek)
      {
        //Các sản phẩm ngày hôm đó
        DateTime dt_Report = item.DateTime;
        List<DataDateGroupByProduct> dataReportByDate = item.DataDateGroupByProducts.ToList();
        
        if (dataReportByDate.Count>0)
        {
          //Đi từng sản phẩm
          foreach (var dataDateGroupByProduct in dataReportByDate)
          {
            List<Datalog> datalogs = dataDateGroupByProduct.Datalogs.ToList();
            List<Sample> samples = dataDateGroupByProduct.Samples.ToList();

            if (datalogs?.Count>0)
            {
              Datalog datalogLast = datalogs?.LastOrDefault();

              Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(s => s.Id == datalogLast.ProductId).FirstOrDefault();
              double maxProduct = productions.UpperLimitFinal;
              double target = productions.StandardFinal;
              double minProduct = productions.LowerLimitFinal;

              string nameUser = AppCore.Ins._listShiftLeader?.Where(x => x.Id == datalogLast.UserId).Select(x => x.UserName).FirstOrDefault();
              //Tạo class thông tin sp
              ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct()
              {
                NameLine = AppCore.Ins._stationCurrent.Name,
                ModeTare = (datalogLast.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn",
                ProductName = productions.Name,
                Standard = target,
                Upper = maxProduct,
                Lower = minProduct,
                NameShiftLeader = nameUser,
              };



              //Tách thành các Shift
              List< StatisticalData> statisticalDatas = new List< StatisticalData>();
              
              var listDatalogByShift = datalogs.GroupBy(x => x.ShiftId).OrderBy(x => x.Key).ToList();
              foreach (var data in listDatalogByShift)
              {
                List<int> listValueDatalogId = data.Select(d => d.Id).ToList();
                List<Sample> samplesChid = AppCore.Ins.GetDataSampleByListIdDatalog(samples, listValueDatalogId);


                //SetDataTable(item.ToList(), samplesChid, productions);

                StatisticalData statisticalData1 = DataProcessing(data.ToList(), samplesChid);
                if (statisticalData1 != null) statisticalDatas.Add(statisticalData1);

                //DataAnalysis(datalogsCurrentBtn);
              }

              ChartLineData chartLineData = ProcessingDataChartLine(datalogs, samples, productions);
              double averageTotal_Product = statisticalDatas.Select(x => x.Average).Average();

              //Tạo Tab
              string titleReport = $"BÁO CÁO SẢN PHẨM: {productions.Name} - NGÀY: {dt_Report.ToString("dd/MM/yyyy")}";
              
              TabPage newTabPage = new TabPage();
              newTabPage.Text = $"SP {++cntTab} trong tuần hiện tại";

              UcReport ucReport = new UcReport();
              ucReport.Name = $"SP{cntTab}";
              ucReport.Dock = DockStyle.Fill;

              ucReport.SetDataInfor(excelClassInforProduct, titleReport);
              
              ucReport.StatisticalProductUI(statisticalDatas);
              ucReport.SetDataTable(datalogs, samples);
              ucReport.SetChartHistogram(samples, productions);
              ucReport.SetChartLine(chartLineData);
              ucReport.SetResultFinal(averageTotal_Product >= target);

              newTabPage.Controls.Add(ucReport);
              tabControl1.TabPages.Add(newTabPage);
            }  
          }
        }  

      } 

    }

    private ChartLineData ProcessingDataChartLine(List<Datalog> datalogs, List<Sample> samples, Models.Production productions)
    {
      List<double> averageRaw = new List<double>();
      double averageTotal = Math.Round(samples.Where(x=>x.isHasValue==true).Average(x => x.Value), 2);
      datalogs = datalogs.OrderBy(x => x.Id).ToList();
      foreach (Datalog datalog in datalogs)
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


    private StatisticalData DataProcessing(List<Datalog> datalogs, List<Sample> samples)
    {
      Datalog datalogFirst = datalogs.FirstOrDefault();
      List<double> valueSamples = samples.Where(x=>x.isHasValue==true).Select(x => x.Value).ToList();
      Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == datalogFirst.ProductId).FirstOrDefault();

      var numbersOver = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value > productions.UpperLimitFinal).Count() : 0;
      var numbersLower = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value < productions.LowerLimitFinal).Count() : 0;

      double stdev = AppCore.Ins.Stdev(valueSamples);
      double average = Math.Round(valueSamples.Average(), 2);
      double target = productions.StandardFinal;

      string result = (average >= target) ? "ĐẠT" : "KHÔNG ĐẠT";

      int totalSamples = samples.Count();
      int numberSamplesOver = (int)samples?.Where(x => x.Value > productions.UpperLimitFinal && x.isHasValue == true).Count();
      int numberSamplesLower = (int)samples?.Where(x => x.Value < productions.LowerLimitFinal && x.isHasValue == true).Count();

      double rateError = (totalSamples != 0) ? (((numberSamplesOver + numberSamplesLower) * 100) / (totalSamples)) : 0;
      rateError = Math.Round(rateError, 2);

      double rateLoss = (average > target) ? Math.Round((((average - target) * 100) / target), 2) : 0;

      StatisticalData statisticalData = new StatisticalData()
      {
        Shift = datalogFirst.ShiftId,
        Stdev = stdev,
        Average = average,
        Target = productions.StandardFinal,
        Result = result,
        TotalSample = valueSamples.Count(),
        NumberSampleOver = numberSamplesOver,
        NumberSampleLower = numberSamplesLower,
        RateError = rateError,
        RateLoss = rateLoss,
      };

      return statisticalData;
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


    private async Task<DataReportByDate> LoadDataProductByDate(DateTime dt_DB, List<int> shiftcode)
    {
      try
      {
        //Load tất cả Data ngày đó
        List<Datalog> allDatalogs = await AppCore.Ins.LoadAllDatalogs(dt_DB);
        List<Sample> allSamples = await AppCore.Ins.LoadAllSamplesContainZero(dt_DB);

        if (allDatalogs == null) return null;
        if (allDatalogs.Count == 0) return null;

        //Filter shift code 
        if (shiftcode.Count > 0)
        {
          foreach (int item in shiftcode)
          {
            allDatalogs = allDatalogs?.Where(x => x.ShiftId != item).ToList();
          }
        }

        if (allDatalogs.Count > 0)
        {
          //Các datalog theo sản phẩm
          Dictionary<int, List<Datalog>> groupDatalogs = allDatalogs
            .GroupBy(x => x.ProductId)
            .ToDictionary(group => group.Key, group => group.ToList());

          List<DataDateGroupByProduct> dataByDates = new List<DataDateGroupByProduct>();
          foreach (var item in groupDatalogs)
          {
            List<Datalog> listDatalog = item.Value.ToList();
            if (listDatalog.Count > 0)
            {
              int productId = listDatalog.FirstOrDefault().ProductId;
              List<int> listValueDatalogId = listDatalog.Select(x => x.Id).ToList();
              List<Sample> samples = AppCore.Ins.GetDataSampleByListIdDatalog(allSamples, listValueDatalogId);

              DataDateGroupByProduct dataDateGroupByProduct = new DataDateGroupByProduct()
              {
                ProductId = productId,
                Datalogs = listDatalog,
                Samples = samples,
              };
              dataByDates.Add(dataDateGroupByProduct);
            }
          }

          if (dataByDates.Count > 0)
          {
            DataReportByDate dataReportByDate = new DataReportByDate()
            {
              DateTime = dt_DB,
              DataDateGroupByProducts = dataByDates
            };

            return dataReportByDate;
          }
          else
          {
            return null;
          }
        }
        else
        {
          return null;
        }
      }
      catch (Exception)
      {
        return null;
      }
    }


    private Bitmap RenderControlToBitmap(System.Windows.Forms.Control control)
    {
      Bitmap bitmap = new Bitmap(control.Width, control.Height);
      //control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, (int)PageSize.A4.Height, (int)PageSize.A4.Width));
      control.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, 2500, 2500));
      return bitmap;
    }



    
    private void btnMonth_Click(object sender, EventArgs e)
    {
      FrmFilterMonth frmFilterMonth = new FrmFilterMonth();
      frmFilterMonth.OnSendMonthChoose += FrmFilterMonth_OnSendMonthChoose;
      frmFilterMonth.Location = new System.Drawing.Point(10, 500);
      frmFilterMonth.ShowDialog();
    }

    private void FrmFilterMonth_OnSendMonthChoose(List<DateTime> listDate)
    {
      if (listDate.Count>0)
      {
        monthCurrent = listDate.FirstOrDefault().Month;
      }  
      listDateHome = listDate;
      DataHome(listDateHome);
      //FilterDataPageHome(listDateHome);
    }

    private void btnWeek_Click(object sender, EventArgs e)
    {
      FrmFilterWeek frmFilterWeek = new FrmFilterWeek();
      frmFilterWeek.OnSendWeekChoose += FrmFilterWeek_OnSendWeekChoose;
      frmFilterWeek.Location = new System.Drawing.Point(100, 210);
      frmFilterWeek.ShowDialog();
    }

    private void FrmFilterWeek_OnSendWeekChoose(List<DateTime> listDate)
    {
      listDateHome = listDate;
      //FilterDataPageHome(listDateHome);
      DataHome(listDateHome);
    }

    private void btnDay_Click(object sender, EventArgs e)
    {
      FrmFilterDate frmFilterDate = new FrmFilterDate(monthCurrent);
      frmFilterDate.OnSendDateChoose += FrmFilterDate_OnSendDateChoose;
      frmFilterDate.Location = new System.Drawing.Point(200, 320);
      frmFilterDate.ShowDialog();
    }

    private void FrmFilterDate_OnSendDateChoose(List<DateTime> listDate)
    {
      listDateHome = listDate;
      //FilterDataPageHome(listDateHome);
      DataHome(listDateHome);
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
      if (listDate.Count>0 && listShift.Count>0)
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


    private void btnLoad_Click(object sender, EventArgs e)
    {
      List<DateTime> listDaysOfMonth = GetAllDaysInMonth(year, month);
      DataHome(listDaysOfMonth);
    }


    private void ExportToPdf(string pathReport)
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
            int tageStart = 0;
            for (; tageStart < numberTag; tageStart++)
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

      this.lbTitleHome.Visible = true;
      this.tablelayoutFilter.Visible = false;

      ExportToPdf(path);
      tabControl1.SelectedIndex = 0;

      this.lbTitleHome.Visible = false;
      this.tablelayoutFilter.Visible = true;
    }

    private void btnExxportPdfByWeek_Click(object sender, EventArgs e)
    {
      FrmExportPdfManual frmFilterWeek = new FrmExportPdfManual();
      frmFilterWeek.ShowDialog();
    }


  }
}
