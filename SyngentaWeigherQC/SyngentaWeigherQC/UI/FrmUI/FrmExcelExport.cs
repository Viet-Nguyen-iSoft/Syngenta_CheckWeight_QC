using Aspose.Cells;
using Aspose.Cells.Timelines;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SynCheckWeigherLoggerApp.DashboardViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static ClosedXML.Excel.XLPredefinedFormat;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Application = System.Windows.Forms.Application;
using Color = System.Drawing.Color;
using DateTime = System.DateTime;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmExcelExport : Form
  {
    public FrmExcelExport()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmExcelExport _Instance = null;
    public static FrmExcelExport Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmExcelExport();
        }
        return _Instance;
      }
    }
    #endregion

    private void FrmExcelExport_Load(object sender, EventArgs e)
    {
      this.cbShift.SelectedIndex = 0;
      this.flowLayoutPanelProduct.Visible = false;
      this.flowLayoutPanelProduct.WrapContents = false;
      this.flowLayoutPanelProduct.FlowDirection = FlowDirection.LeftToRight;
      dataGridView1.CellClick += DataGridView1_CellMouseEnter;
    }

   

    private DateTime fromDate = DateTime.Now;
    private DateTime toDate = DateTime.Now;
    private int shiftIndex = 0;
    private void btnPreview_Click(object sender, EventArgs e)
    {
      if (!backgroundWorkerLoadData.IsBusy)
      {
        backgroundWorkerLoadData.RunWorkerAsync();
      }
    }

    private void AddListDataExcel(List<DataReportByDate> dataReportByDates)
    {
      if (dataReportByDates.Count <= 0) return;

      this.flowLayoutPanelProduct.Controls.Clear();
      //ĐI từng ngày
      int cnt = 0;
      foreach (var item in dataReportByDates)
      {
        cnt++;
        //Các sản phẩm ngày hôm đó
        DateTime dt_Report = item.DateTime;
        List<DataDateGroupByProduct> dataReportByDate = item.DataDateGroupByProducts.ToList();

        if (dataReportByDate.Count > 0)
        {
          //Đi từng sản phẩm
          foreach (var dataDateGroupByProduct in dataReportByDate)
          {
            //List<Datalog> datalogs = dataDateGroupByProduct.Datalogs.ToList();
            //List<Sample> samples = dataDateGroupByProduct.Samples.ToList();
            int idProduct = dataDateGroupByProduct.ProductId;
            string nameProduct = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == idProduct)?.Select(x => x.Name).FirstOrDefault();
            this.flowLayoutPanelProduct.Visible = true;
            string textButton = dt_Report.ToString($"dd/MM/yyyy - {nameProduct}");
            CreateButtonWithDataProduction(textButton, dataDateGroupByProduct);
          }

          if (flowLayoutPanelProduct.Controls.Count > 0 && flowLayoutPanelProduct.Controls[0] is Button)
          {
            Button firstButton = (Button)flowLayoutPanelProduct.Controls[0];
            firstButton.PerformClick();
          }
        }

        Progress((cnt * 100 / dataReportByDates.Count()));
      }
    }

    
    private async Task<DataReportByDate> LoadDataProductByDate(DateTime dt_DB, int shiftId)
    {
      //Load tất cả Data ngày đó
      List<Datalog> allDatalogs = await AppCore.Ins.LoadAllDatalogs(dt_DB);
      List<Sample> allSamples = await AppCore.Ins.LoadAllSamplesContainZero(dt_DB);

      if (allDatalogs == null) return null;
      if (allDatalogs.Count == 0) return null;

      //Filter shift code 
      if (shiftId !=0)
      {
        allDatalogs = allDatalogs?.Where(x => x.ShiftId == shiftId).ToList();
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

      this.flowLayoutPanelProduct.Controls.Clear();
      this.flowLayoutPanelProduct.Visible = false;
      this.dataGridView1.Rows.Clear();

      this.ucTemplateExcel1.UpdateInforProductUI(null);
      this.ucTemplateExcel1.ClearStatistical();
      this.ucChartHistogram1.SetDataChart(null, null);
      this.ucChartLine1.SetDataChart(null);
    }


    private void CreateButtonWithDataProduction(string text, DataDateGroupByProduct dataDateGroupByProduct)
    {
      //string title = $"{date.ToString("dd/MM/yyyy")}-"

      Button button = new System.Windows.Forms.Button();
      button.BackColor = Color.FromArgb(72, 61, 139);
      button.FlatAppearance.BorderColor = Color.FromArgb(102, 102, 153);
      button.FlatAppearance.BorderSize = 3;
      button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      button.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
      button.ForeColor = System.Drawing.Color.White;
      button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      button.Location = new System.Drawing.Point(3, 3);
      button.Name = text;
      button.Size = new System.Drawing.Size(300, 40);
      button.TabIndex = 16;
      button.Text = text;
      button.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      button.UseVisualStyleBackColor = false;
      button.Tag = dataDateGroupByProduct;
      button.Click += EvenButton_Click;
      this.flowLayoutPanelProduct.Controls.Add(button);
    }

    private void EvenButton_Click(object sender, EventArgs e)
    {
      ActiveColor(sender);
    }


    private List<Sample> samplesSrcCurrent = new List<Sample>();
    private void ActiveColor(object sender)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ActiveColor(sender);
        }));
        return;
      }

      if (sender is Button)
      {
        Button button = (Button)sender;
        for (int i = 0; i < this.flowLayoutPanelProduct.Controls.Count; i++)
        {
          if (this.flowLayoutPanelProduct.Controls[i] is Button)
          {
            Button localButton = (Button)(this.flowLayoutPanelProduct.Controls[i]);
            if (localButton != null)
            {
              localButton.BackColor = Color.FromArgb(72, 61, 139);
            }
          }
        }

        if (button.Tag != null)
        {
          DataDateGroupByProduct data = (DataDateGroupByProduct)button.Tag;

          button.BackColor = Color.FromArgb(204, 102, 255);

          if (data!=null)
          {
            List<Datalog> datalogs = data.Datalogs;
            samplesSrcCurrent = data.Samples;
            Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == data.ProductId).FirstOrDefault();
            
            //Vẽ Chart Line
            ChartLineData chartLineData = ProcessingDataChartLine(datalogs, samplesSrcCurrent, productions);
            ucChartLine1.SetDataChart(chartLineData);

            //Chart Histogram
            ucChartHistogram1.SetDataChart(samplesSrcCurrent, productions);


            Datalog dataFirst = datalogs?.FirstOrDefault();

            //Thông tin Report
            int stationId = dataFirst.StationId;
            string nameStation = AppCore.Ins._listInforLine?.Where(x=>x.Id== stationId)?.FirstOrDefault()?.Name;
            string modeTare = (dataFirst.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn";

            int userId = dataFirst.UserId;
            string leaderShift = AppCore.Ins._listShiftLeader?.Where(x => x.Id == userId)?.FirstOrDefault()?.UserName;
            ExcelClassInforProduct excelClassInforProduct = new ExcelClassInforProduct()
            {
              NameLine = nameStation,
              ModeTare = modeTare,
              ProductName = productions.Name,
              PackSize = productions.PackSize,
              Standard = productions.StandardFinal,
              Upper = productions.UpperLimitFinal,
              Lower = productions.LowerLimitFinal,
              NameShiftLeader = leaderShift,
            };

            ucTemplateExcel1.UpdateInforProductUI(excelClassInforProduct);

            //Chia theo ca
            dataGridView1.Rows.Clear();
            var listDatalogByShift = datalogs.GroupBy(x=>x.ShiftId).OrderBy(x=>x.Key).ToList();
            
            List<StatisticalData> listStatisticalData = new List<StatisticalData>();
            foreach (var item in listDatalogByShift)
            {
              List<int> listValueDatalogId = item.Select(d => d.Id).ToList();
              List<Sample> samplesChid = AppCore.Ins.GetDataSampleByListIdDatalog(samplesSrcCurrent, listValueDatalogId);
              

              SetDataTable(item.ToList(), samplesChid, productions);

              StatisticalData statisticalData1 = DataProcessing(item.ToList(), samplesChid);
              if (statisticalData1!=null) listStatisticalData.Add(statisticalData1);

              //DataAnalysis(datalogsCurrentBtn);
            }

            if (listStatisticalData.Count>0)
            {
              StatisticalData statisticalDataIndex0 = listStatisticalData?.Where(x => x.Shift == 1 || x.Shift == 4 || x.Shift==6).OrderBy(x => x.Shift).FirstOrDefault();
              StatisticalData statisticalDataIndex1 = listStatisticalData?.Where(x => x.Shift == 2 || x.Shift == 5).OrderBy(x => x.Shift).FirstOrDefault();
              StatisticalData statisticalDataIndex2 = listStatisticalData?.Where(x => x.Shift == 3).OrderBy(x => x.Shift).FirstOrDefault();

              this.ucTemplateExcel1.ClearStatistical();
              this.ucTemplateExcel1.UpdateSynthetic(statisticalDataIndex0, 0);
              this.ucTemplateExcel1.UpdateSynthetic(statisticalDataIndex1, 1);
              this.ucTemplateExcel1.UpdateSynthetic(statisticalDataIndex2, 2);

              double averageShift = 0;
              double target = 0;
              int cnt = 0;
              if (statisticalDataIndex0 != null)
              {
                averageShift += statisticalDataIndex0.Average;
                target = statisticalDataIndex0.Target;
                cnt++;
              }
              if (statisticalDataIndex1 != null)
              {
                averageShift += statisticalDataIndex1.Average;
                target = statisticalDataIndex1.Target;
                cnt++;
              }
              if (statisticalDataIndex2 != null)
              {
                averageShift += statisticalDataIndex2.Average;
                target = statisticalDataIndex2.Target;
                cnt++;
              }
              averageShift = averageShift / cnt;

              ucTemplateExcel1.SetResultFinal(averageShift >= target);
            }

          }

        }
      }
    }



    private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
    {
      

      int colmnIndex = e.ColumnIndex;
      int rowIndex = e.RowIndex;

      if (rowIndex == -1) return;
      if (colmnIndex >= 3 && colmnIndex <= 17)
      {
        //Value Sample cân lại
        object cellValueSample = dataGridView1.Rows[e.RowIndex].Cells["Column19"].Value;
        if (cellValueSample!=null)
        {
          int id = Convert.ToInt16(cellValueSample);
          var sample = samplesSrcCurrent?.Where(x => x.DatalogId == id).ToList();
          if (sample != null)
          {
            var sampleDetail = sample.Where(x => x.LocalId == colmnIndex - 2).FirstOrDefault();
            if (sampleDetail != null)
            {
              if (sampleDetail.isEdited)
              {
                FrmSeeHistoricalReweigher frmSeeHistoricalReweigher = new FrmSeeHistoricalReweigher(sampleDetail);
                frmSeeHistoricalReweigher.ShowDialog();
              }
            }
          }
        }
      }
    }

    private ChartLineData ProcessingDataChartLine(List<Datalog> datalogs, List<Sample> samples, Models.Production productions)
    {
      List<double> averageRaw = new List<double>();
      double averageTotal = Math.Round(samples.Average(x => x.Value), 2);
      datalogs = datalogs.OrderBy(x=>x.Id).ToList();
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
      List<double> valueSamples = samples.Where(x=>x.isHasValue==true && x.isEnable==true).Select(x => x.Value).ToList();
      Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == datalogFirst.ProductId).FirstOrDefault();

      var numbersOver = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value > productions.UpperLimitFinal).Count() : 0;
      var numbersLower = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value < productions.LowerLimitFinal).Count() : 0;

      double stdev = AppCore.Ins.Stdev(valueSamples);
      double average = Math.Round(valueSamples.Average(), 2);
      double target = productions.StandardFinal;

      string result = (average >= target) ? "ĐẠT" : "KHÔNG ĐẠT";

      int totalSamples = samples.Count();
      int numberSamplesOver = (int)samples?.Where(x => x.Value > productions.UpperLimitFinal && x.isHasValue == true).Count();
      int numberSamplesLower = (int)samples?.Where(x => x.Value < productions.LowerLimitFinal && x.isHasValue==true).Count();

      double rateError = (totalSamples != 0) ? (((double)(numberSamplesOver + numberSamplesLower) * 100) / (totalSamples)) : 0;
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

    private void SetDataTable(List<Datalog> datalogs, List<Sample> samples, Models.Production productions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(datalogs, samples, productions);
        }));
        return;
      }

      try
      {
        if (datalogs.Count() <= 0 || datalogs == null) return;

        samples = samples?.Where(x => x.isHasValue).ToList();
        double averageTotal = Math.Round(samples.Average(x => x.Value), 2);
        double stdev = Math.Round(AppCore.Ins.Stdev(samples.Select(x => x.Value).ToList()), 2);
        int stt = datalogs.Count();

        datalogs = datalogs.OrderBy(x=>x.Id).ToList();
        foreach (var datalog in datalogs)
        {
          List<Sample> samplesChid = samples?.Where(x => x.DatalogId == datalog.Id && x.isHasValue==true).ToList();
          double averageRaw = Math.Round(samplesChid.Average(x => x.Value), 2);

          int row = dataGridView1.Rows.Add();
          string shift = (datalog.ShiftId != -1) ? AppCore.Ins._listShift.Where(x => x.CodeShift == datalog.ShiftId).Select(x => x.Name).FirstOrDefault() : "";

          dataGridView1.Rows[row].Cells["Column1"].Value = shift;
          dataGridView1.Rows[row].Cells["Column2"].Value = datalog.No;
          dataGridView1.Rows[row].Cells["Column3"].Value = datalog.CreatedAt;

          //Sample 
          var samp1 = samplesChid?.Where(x => x.LocalId == 1).FirstOrDefault();
          var samp2 = samplesChid?.Where(x => x.LocalId == 2).FirstOrDefault();
          var samp3 = samplesChid?.Where(x => x.LocalId == 3).FirstOrDefault();
          var samp4 = samplesChid?.Where(x => x.LocalId == 4).FirstOrDefault();
          var samp5 = samplesChid?.Where(x => x.LocalId == 5).FirstOrDefault();
          var samp6 = samplesChid?.Where(x => x.LocalId == 6).FirstOrDefault();
          var samp7 = samplesChid?.Where(x => x.LocalId == 7).FirstOrDefault();
          var samp8 = samplesChid?.Where(x => x.LocalId == 8).FirstOrDefault();
          var samp9 = samplesChid?.Where(x => x.LocalId == 9).FirstOrDefault();
          var samp10 = samplesChid?.Where(x => x.LocalId == 10).FirstOrDefault();
          var samp11 = samplesChid?.Where(x => x.LocalId == 11).FirstOrDefault();
          var samp12 = samplesChid?.Where(x => x.LocalId == 12).FirstOrDefault();
          var samp13 = samplesChid?.Where(x => x.LocalId == 13).FirstOrDefault();
          var samp14 = samplesChid?.Where(x => x.LocalId == 14).FirstOrDefault();
          var samp15 = samplesChid?.Where(x => x.LocalId == 15).FirstOrDefault();


          //Sample 
          double samp1_Value = (samp1 != null) ? samp1.Value : 0;
          double samp2_Value = (samp2 != null) ? samp2.Value : 0;
          double samp3_Value = (samp3 != null) ? samp3.Value : 0;
          double samp4_Value = (samp4 != null) ? samp4.Value : 0;
          double samp5_Value = (samp5 != null) ? samp5.Value : 0;
          double samp6_Value = (samp6 != null) ? samp6.Value : 0;
          double samp7_Value = (samp7 != null) ? samp7.Value : 0;
          double samp8_Value = (samp8 != null) ? samp8.Value : 0;
          double samp9_Value = (samp9 != null) ? samp9.Value : 0;
          double samp10_Value = (samp10 != null) ? samp10.Value : 0;
          double samp11_Value = (samp11 != null) ? samp11.Value : 0;
          double samp12_Value = (samp12 != null) ? samp12.Value : 0;
          double samp13_Value = (samp13 != null) ? samp13.Value : 0;
          double samp14_Value = (samp14 != null) ? samp14.Value : 0;
          double samp15_Value = (samp15 != null) ? samp15.Value : 0;


          //Tô màu
          double max = productions.UpperLimitFinal;
          double min = productions.LowerLimitFinal;

          //Check Cân lại
          //Sample 
          var samp1Edit = (samp1 != null) ? samp1.isEdited : false;
          var samp2Edit = (samp2 != null) ? samp2.isEdited : false;
          var samp3Edit = (samp3 != null) ? samp3.isEdited : false;
          var samp4Edit = (samp4 != null) ? samp4.isEdited : false;
          var samp5Edit = (samp5 != null) ? samp5.isEdited : false;
          var samp6Edit = (samp6 != null) ? samp6.isEdited : false;
          var samp7Edit = (samp7 != null) ? samp7.isEdited : false;
          var samp8Edit = (samp8 != null) ? samp8.isEdited : false;
          var samp9Edit = (samp9 != null) ? samp9.isEdited : false;
          var samp10Edit = (samp10 != null) ? samp10.isEdited : false;
          var samp11Edit = (samp11 != null) ? samp11.isEdited : false;
          var samp12Edit = (samp12 != null) ? samp12.isEdited : false;
          var samp13Edit = (samp13 != null) ? samp13.isEdited : false;
          var samp14Edit = (samp14 != null) ? samp14.isEdited : false;
          var samp15Edit = (samp15 != null) ? samp15.isEdited : false;


          SetColorDgv(row, "Column4", (double)samp1_Value, min, max, samp1Edit);
          SetColorDgv(row, "Column5", (double)samp2_Value, min, max, samp2Edit);
          SetColorDgv(row, "Column6", (double)samp3_Value, min, max, samp3Edit);
          SetColorDgv(row, "Column7", (double)samp4_Value, min, max, samp4Edit);
          SetColorDgv(row, "Column8", (double)samp5_Value, min, max, samp5Edit);
          SetColorDgv(row, "Column9", (double)samp6_Value, min, max, samp6Edit);
          SetColorDgv(row, "Column10", (double)samp7_Value, min, max, samp7Edit);
          SetColorDgv(row, "Column11", (double)samp8_Value, min, max, samp8Edit);
          SetColorDgv(row, "Column20", (double)samp9_Value, min, max, samp9Edit);
          SetColorDgv(row, "Column21", (double)samp10_Value, min, max, samp10Edit);
          SetColorDgv(row, "Column22", (double)samp11_Value, min, max, samp11Edit);
          SetColorDgv(row, "Column23", (double)samp12_Value, min, max, samp12Edit);
          SetColorDgv(row, "Column24", (double)samp13_Value, min, max, samp13Edit);
          SetColorDgv(row, "Column25", (double)samp14_Value, min, max, samp14Edit);
          SetColorDgv(row, "Column26", (double)samp15_Value, min, max, samp15Edit);

          if (samp1_Value != 0)
            dataGridView1.Rows[row].Cells["Column4"].Value = samp1_Value;
          else
            dataGridView1.Rows[row].Cells["Column4"].Style.BackColor = Color.DimGray;

          if (samp2_Value != 0)
            dataGridView1.Rows[row].Cells["Column5"].Value = samp2_Value;
          else
            dataGridView1.Rows[row].Cells["Column5"].Style.BackColor = Color.DimGray;

          if (samp3_Value != 0)
            dataGridView1.Rows[row].Cells["Column6"].Value = samp3_Value;
          else
            dataGridView1.Rows[row].Cells["Column6"].Style.BackColor = Color.DimGray;

          if (samp4_Value != 0)
            dataGridView1.Rows[row].Cells["Column7"].Value = samp4_Value;
          else
            dataGridView1.Rows[row].Cells["Column7"].Style.BackColor = Color.DimGray;

          if (samp5_Value != 0)
            dataGridView1.Rows[row].Cells["Column8"].Value = samp5_Value;
          else
            dataGridView1.Rows[row].Cells["Column8"].Style.BackColor = Color.DimGray;

          if (samp6_Value != 0)
            dataGridView1.Rows[row].Cells["Column9"].Value = samp6_Value;
          else
            dataGridView1.Rows[row].Cells["Column9"].Style.BackColor = Color.DimGray;

          if (samp7_Value != 0)
            dataGridView1.Rows[row].Cells["Column10"].Value = samp7_Value;
          else
            dataGridView1.Rows[row].Cells["Column10"].Style.BackColor = Color.DimGray;

          if (samp8_Value != 0)
            dataGridView1.Rows[row].Cells["Column11"].Value = samp8_Value;
          else
            dataGridView1.Rows[row].Cells["Column11"].Style.BackColor = Color.DimGray;
          if (samp9_Value != 0)
            dataGridView1.Rows[row].Cells["Column20"].Value = samp9_Value;
          else
            dataGridView1.Rows[row].Cells["Column20"].Style.BackColor = Color.DimGray;

          if (samp10_Value != 0)
            dataGridView1.Rows[row].Cells["Column21"].Value = samp10_Value;
          else
            dataGridView1.Rows[row].Cells["Column21"].Style.BackColor = Color.DimGray;
          if (samp11_Value != 0)
            dataGridView1.Rows[row].Cells["Column22"].Value = samp11_Value;
          else
            dataGridView1.Rows[row].Cells["Column22"].Style.BackColor = Color.DimGray;
          if (samp12_Value != 0)
            dataGridView1.Rows[row].Cells["Column23"].Value = samp12_Value;
          else
            dataGridView1.Rows[row].Cells["Column23"].Style.BackColor = Color.DimGray;
          if (samp13_Value != 0)
            dataGridView1.Rows[row].Cells["Column24"].Value = samp13_Value;
          else
            dataGridView1.Rows[row].Cells["Column24"].Style.BackColor = Color.DimGray;
          if (samp14_Value != 0)
            dataGridView1.Rows[row].Cells["Column25"].Value = samp14_Value;
          else
            dataGridView1.Rows[row].Cells["Column25"].Style.BackColor = Color.DimGray;
          if (samp15_Value != 0)
            dataGridView1.Rows[row].Cells["Column26"].Value = samp15_Value;
          else
            dataGridView1.Rows[row].Cells["Column26"].Style.BackColor = Color.DimGray;

          dataGridView1.Rows[row].Cells["Column12"].Value = averageRaw;
          dataGridView1.Rows[row].Cells["Column13"].Value = averageTotal;
          dataGridView1.Rows[row].Cells["Column14"].Value = productions.StandardFinal;
          dataGridView1.Rows[row].Cells["Column15"].Value = (averageRaw >= productions.StandardFinal) ? "Đạt" : "Không đạt";
          dataGridView1.Rows[row].Cells["Column16"].Value = stdev;
          dataGridView1.Rows[row].Cells["Column17"].Value = samplesChid.Min(x => x.Value);
          dataGridView1.Rows[row].Cells["Column18"].Value = samplesChid.Max(x => x.Value);
          dataGridView1.Rows[row].Cells["Column19"].Value = datalog.Id;

          //Đánh giá
          dataGridView1.Rows[row].Cells[21].Style.BackColor = (averageRaw >= productions.StandardFinal) ? Color.Green : Color.Red;
          dataGridView1.Rows[row].Cells[21].Style.ForeColor = Color.White;
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
    }


    private void SetColorDgv(int rowIndex, string nameColumn, double value, double min, double max, bool isReWeigher = false)
    {
      if (isReWeigher == true)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.Purple;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
        return;
      }
      if (value < min)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.Red;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
      }
      else if (value > max)
      {
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.BackColor = Color.DarkOrange;
        dataGridView1.Rows[rowIndex].Cells[nameColumn].Style.ForeColor = Color.White;
      }
    }



    private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
    {
      fromDate = dateTimePickerFrom.Value;
    }

    private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
    {
      toDate = dateTimePickerTo.Value;
    }



    private List<DataReportByDate> dataReportByDates = new List<DataReportByDate>();
    private async void backgroundWorkerLoadData_DoWork(object sender, DoWorkEventArgs e)
    {
      TimeSpan duration = toDate - fromDate;
      int numberDate = (int)duration.TotalDays + 1;

      if (numberDate <= 0)
      {
        new FrmNotification().ShowMessage("Vui lòng chọn ngày bắt đầu nhở hơn ngày kết thúc !", eMsgType.Warning);
        return;
      }

      //Đi từng DB
      //Data theo từng ngày
      dataReportByDates = new List<DataReportByDate>();
      for (int i = 0; i < numberDate; i++)
      {
        string fileDB = Application.StartupPath + $"\\Database\\{fromDate.AddDays(i).ToString("yyyyMMdd")}_data_log.sqlite";
        if (!File.Exists(fileDB)) continue;

        DataReportByDate report = await LoadDataProductByDate(fromDate.AddDays(i), shiftIndex);

        if (report != null)
        {
          if (report.DataDateGroupByProducts.Count > 0)
            dataReportByDates.Add(report);
        }
      }
    }

    private void backgroundWorkerLoadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (dataReportByDates.Count > 0)
      {
        AddListDataExcel(dataReportByDates);
      }
      else
      {
        ClearPage();
        new FrmNotification().ShowMessage("Không có dữ liệu !", eMsgType.Info);
      }
    }


    private string nameFolder = "";

    private void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        if (dataReportByDates != null)
        {
          if (dataReportByDates.Count > 0)
          {
            //Chọn folder để lưu
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
              folderDialog.Description = "Chọn thư mục lưu";
              DialogResult result = folderDialog.ShowDialog();
              if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
              {
                nameFolder = folderDialog.SelectedPath;
                bool rs= false;
                for (int i = 0; i < dataReportByDates.Count; i++)
                {
                  Progress(((i + 1) * 100 / dataReportByDates.Count()));
                  rs= ReportExcel(dataReportByDates[i]);
                }

                if (rs)
                {
                  //FrmConfirmV2 frmConfirm = new FrmConfirmV2("Xuất report thành công !\n Bạn có muốn mở file bây giờ ?", eMsgType.Question);
                  //frmConfirm.OnSendOKClicked += FrmConfirm_OnSendOKClicked;
                  //frmConfirm.ShowDialog();
                }
              }
            }
          }
          else
          {
            new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Info);
          }
        }
        else
        {
          new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Info);
        }
      }
      catch (Exception)
      {
        return;
      }
      
    }

    private void Progress(int value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Progress(value);
        }));
        return;
      }

      value = value > 100 ? 100 : value;
      this.progressBar1.Value = value;
    }

    private void FrmConfirm_OnSendOKClicked(object sender)
    {
      Process.Start("explorer.exe", nameFolder);
      //Process.Start(fileNameExcel);
    }

    private bool ReportExcel(DataReportByDate dataReportByDate)
    {
      try
      {
        string templatePath = $@"{Application.StartupPath}\Template\AutoReportV2.xlsx";
        string passExxcel = AppCore.Ins._stationCurrent.PassReport;

        XLWorkbook workbook = new XLWorkbook(templatePath);
        DateTime dateTime = dataReportByDate.DateTime;
        string nameLine = "";
        List<DataDateGroupByProduct> dataDateGroupByProducts = dataReportByDate.DataDateGroupByProducts;

        int sheetId = 1;
        //double averageAllShift = 0;
        double averageShift = 0;
        foreach (var item in dataDateGroupByProducts)
        {
          //Fill từng sản phẩm vào các sheet
          IXLWorksheet worksheet = workbook.Worksheet($"Page{sheetId}");
          worksheet.Protect(passExxcel);

          //Datalog từng sản phẩm
          List<Datalog> datalogs = item.Datalogs;
          List<Sample> samples = item.Samples;
          Datalog datalogFirst = datalogs.FirstOrDefault();

          //Phần 1: Fill thông tin sản phẩm
          nameLine = AppCore.Ins._listInforLine?.Where(x => x.Id == datalogFirst.StationId).FirstOrDefault()?.Name;
          string modeTare = (datalogFirst.IsTareWithLabel == eModeTare.TareWithLabel) ? "Tare có nhãn" : "Tare không nhãn";

          Models.Production productions = AppCore.Ins._listAllProductsContainIsDelete?.Where(x => x.Id == datalogFirst.ProductId).FirstOrDefault();
          string nameProduct = (productions != null) ? productions.Name : "";
          double packSize = (productions != null) ? productions.PackSize : 0;
          double standard = (productions != null) ? productions.StandardFinal : 0;
          double upper = (productions != null) ? productions.UpperLimitFinal : 0;
          double lower = (productions != null) ? productions.LowerLimitFinal : 0;

          int idUser = datalogFirst.UserId;
          string nameShiftLeader = (idUser != 0) ? AppCore.Ins._listShiftLeader?.Where(x => x.Id == idUser).FirstOrDefault()?.UserName : "";

          int rowStartUnit1 = 3;
          worksheet.Cell($"I{rowStartUnit1++}").Value = nameLine;
          worksheet.Cell($"I{rowStartUnit1++}").Value = modeTare;
          worksheet.Cell($"I{rowStartUnit1++}").Value = nameProduct;
          worksheet.Cell($"I{rowStartUnit1++}").Value = packSize;
          worksheet.Cell($"I{rowStartUnit1++}").Value = standard;
          worksheet.Cell($"I{rowStartUnit1++}").Value = upper;
          worksheet.Cell($"I{rowStartUnit1++}").Value = lower;
          worksheet.Cell($"I{rowStartUnit1++}").Value = nameShiftLeader;

          worksheet.Cell($"B1").Value = $"BÁO CÁO SẢN PHẨM - {dateTime.ToString("dd/MM/yyyy")}: {nameProduct}";


          //Phần 2: Fill table
          int rowStart = 70;
          int shiftId = 0;
          string nameShift = "";
          double target = 0;
          var groupDatalogsByShift = datalogs.GroupBy(x => x.ShiftId).ToList();
          if (groupDatalogsByShift != null)
          {
            int indexRowAverageByShift = 70;
            int noAll = 1;
            foreach (var data in groupDatalogsByShift)
            {
              List<Datalog> datalog_by_shift = data.ToList();

              List<int> datalogId = datalog_by_shift.Select(x => x.Id).ToList();

              List<Sample> sample_by_shift = GetDataSampleByListIdDatalog(samples, datalogId);
              List<Sample> sample_by_shift_remove_valueZero = sample_by_shift.Where(x => x.isHasValue == true && x.isEnable == true).ToList();
              List<double> listSampleValue = sample_by_shift_remove_valueZero.Select(x => x.Value).ToList();

              //Trung bình Sample theo ca
              averageShift = Math.Round(listSampleValue.Average(), 2);

              //FillTable
              int stt = 1;
              for (int i = 0; i < datalog_by_shift.Count; i++)
              {
                shiftId = datalog_by_shift[i].ShiftId;
                nameShift = AppCore.Ins._listShift?.Where(x => x.CodeShift == shiftId).FirstOrDefault()?.Name;

                worksheet.Cell($"E{rowStart}").Value = datalog_by_shift[i].CreatedAt;
                worksheet.Cell($"AC{rowStart}").Value = datalog_by_shift[i].Id;
                worksheet.Cell($"B{rowStart}").Value = noAll++;
                worksheet.Cell($"C{rowStart}").Value = nameShift;
                worksheet.Cell($"W{rowStart}").FormulaA1 = $"AVERAGE(V{indexRowAverageByShift} : V{indexRowAverageByShift + datalog_by_shift.Count - 1})";
                worksheet.Cell($"Z{rowStart}").FormulaA1 = $"STDEV(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})";
                worksheet.Cell($"D{rowStart}").Value = stt++;

                //Sample cho từng Datalog
                List<Sample> sampleSingle = sample_by_shift?.Where(x => x.DatalogId == datalog_by_shift[i].Id).OrderBy(x=>x.CreatedAt).ToList();

                AppCore.Ins.SetColor(worksheet, sampleSingle[0].Value, lower, upper, $"G{rowStart}", sampleSingle[0].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[1].Value, lower, upper, $"H{rowStart}", sampleSingle[1].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[2].Value, lower, upper, $"I{rowStart}", sampleSingle[2].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[3].Value, lower, upper, $"J{rowStart}", sampleSingle[3].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[4].Value, lower, upper, $"K{rowStart}", sampleSingle[4].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[5].Value, lower, upper, $"L{rowStart}", sampleSingle[5].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[6].Value, lower, upper, $"M{rowStart}", sampleSingle[6].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[7].Value, lower, upper, $"N{rowStart}", sampleSingle[7].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[8].Value, lower, upper, $"O{rowStart}", sampleSingle[8].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[9].Value, lower, upper, $"P{rowStart}", sampleSingle[9].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[10].Value, lower, upper, $"Q{rowStart}", sampleSingle[10].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[11].Value, lower, upper, $"R{rowStart}", sampleSingle[11].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[12].Value, lower, upper, $"S{rowStart}", sampleSingle[12].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[13].Value, lower, upper, $"T{rowStart}", sampleSingle[13].isEdited);
                AppCore.Ins.SetColor(worksheet, sampleSingle[14].Value, lower, upper, $"U{rowStart}", sampleSingle[14].isEdited);

                AppCore.Ins.SetColor(worksheet, sampleSingle[0], $"AL{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[1], $"AM{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[2], $"AN{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[3], $"AQ{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[4], $"AP{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[5], $"AQ{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[6], $"AR{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[7], $"AS{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[8], $"AT{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[9], $"AU{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[10], $"AV{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[11], $"AW{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[12], $"AX{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[13], $"AY{rowStart}");
                AppCore.Ins.SetColor(worksheet, sampleSingle[14], $"AZ{rowStart}");


                rowStart++;
              }
              

              double stdev = AppCore.Ins.Stdev(listSampleValue);
              target = productions.StandardFinal;

              double minValue = listSampleValue.Min();
              double maxValue = listSampleValue.Max();

              //double Cp = (stdev != 0) ? ((maxValue - minValue) / (6 * stdev)) : 0;
              //double Cpk_H = (stdev != 0) ? ((maxValue - averageShift) / (3 * stdev)) : 0;
              //double Cpk_L = (stdev != 0) ? ((averageShift - minValue) / (3 * stdev)) : 0;
              //double Cpk = Math.Min(Cpk_H, Cpk_L);

              //int totalSamples = sample_by_shift_remove_valueZero.Where(x => x.isHasValue && x.isEnable).Count();
              //int numberSamplesOver = (sample_by_shift_remove_valueZero != null) ? sample_by_shift_remove_valueZero.Where(x => x.Value > productions.UpperLimitFinal).Count() : 0;
              //int numberSamplesLower = (sample_by_shift_remove_valueZero != null) ? sample_by_shift_remove_valueZero.Where(x => x.Value < productions.LowerLimitFinal).Count() : 0;

              int index = 13;
              if (shiftId == 2 || shiftId == 5) index = 14;
              else if (shiftId == 3) index = 15;

              worksheet.Cell($"B{index}").Value = nameShift;
              worksheet.Cell($"E{index}").FormulaA1 = $"ROUND(STDEV(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}),3)";
              worksheet.Cell($"G{index}").FormulaA1 = $"ROUND(AVERAGE(V{indexRowAverageByShift} : V{indexRowAverageByShift + datalog_by_shift.Count - 1}),3)";

              worksheet.Cell($"M{index}").FormulaA1 = $"ROUND((G{index} - I9)/(3*E{index}),3)";
              worksheet.Cell($"K{index}").FormulaA1 = $"ROUND((I8-G{index})/(3*E{index}),3)";
              //worksheet.Cell($"M{index}").FormulaA1 = $"ROUND((G{index} - MIN(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}))/(3*E{index}),3)";
              //worksheet.Cell($"K{index}").FormulaA1 = $"ROUND((MAX(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})-G{index})/(3*E{index}),3)";
              worksheet.Cell($"O{index}").FormulaA1 = $"MIN(M{index},K{index})";
              worksheet.Cell($"S{index}").FormulaA1 = $"COUNTA(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1})";
              worksheet.Cell($"V{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}, \"<\" & I9)";
              worksheet.Cell($"X{index}").FormulaA1 = $"COUNTIF(G{indexRowAverageByShift} : U{indexRowAverageByShift + datalog_by_shift.Count - 1}, \">\" & I8)";
              indexRowAverageByShift += datalog_by_shift.Count();
            }
          }

          for (int i = 328; i >= rowStart; i--)
          {
            worksheet.Row(i).Hide();
          }

          sheetId++;
        }

        for (int i = dataDateGroupByProducts.Count() + 1; i <= 10; i++)
        {
          workbook.Worksheets.Delete($"Page{i}");
        }

        string fileName = nameFolder + $"\\ReportManual_{nameLine}_{dateTime.ToString("yyyy_MM_dd")}.xlsx";
        fileName = nameFolder + $"\\ReportDailys_{nameLine}_{dateTime.ToString("yyyy_MM_dd")}.xlsx";

        workbook.SaveAs(fileName);
        return true;
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
        MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }


    private List<Sample> GetDataSampleByListIdDatalog(List<Sample> listSamples, List<int> listIdDatalogs)
    {
      List<Sample> rs = new List<Sample>();

      foreach (var id in listIdDatalogs)
      {
        List<Sample> samples = listSamples?.Where(x => x.DatalogId == id).ToList();
        rs.AddRange(samples);
      }
      return rs;
    }


    
    

    private void cbShift_SelectedIndexChanged(object sender, EventArgs e)
    {
      shiftIndex = cbShift.SelectedIndex; 
    }
  }
}
