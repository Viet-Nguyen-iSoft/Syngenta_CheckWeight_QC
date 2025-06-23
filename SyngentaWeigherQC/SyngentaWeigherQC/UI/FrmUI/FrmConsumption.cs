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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
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
      this.chartErrorHome.SetTilte = "Biểu đồ Top % mẫu lỗi";
      this.chartLossHome.SetTilte = "Biểu đồ Top % hao hụt";
      this.chartCpkHome.SetTilte = "Biểu đồ Top Cpk";
      this.chartStdevHome.SetTilte = "Biểu đồ Top Stdev";
      this.chartSigmaHome.SetTilte = "Biểu đồ Top Sigma";

      //Set Title Chart 
      this.chartavgErrorHome.SetTilte = "Biểu đồ TB % mẫu lỗi";
      this.chartavgLossHome.SetTilte = "Biểu đồ TB % hao hụt";
      this.chartavgCpkHome.SetTilte = "Biểu đồ TB Cpk";
      this.chartavgStdevHome.SetTilte = "Biểu đồ TB Stdev";
      this.chartAvgSigmaHome.SetTilte = "Biểu đồ TB Sigma";

      //Month
      //Set Title Chart 
      this.chartTopErrorMonth.SetTilte = "Biểu đồ Top % mẫu lỗi";
      this.chartTopLossMonth.SetTilte = "Biểu đồ Top % hao hụt";
      this.chartTopCpkMonth.SetTilte = "Biểu đồ Top Cpk";
      this.chartTopStdevMonth.SetTilte = "Biểu đồ Top Stdev";
      this.chartTopSigmaMonth.SetTilte = "Biểu đồ Top Sigma";

      //Set Title Chart 
      this.chartAvgErrorMonth.SetTilte = "Biểu đồ TB % mẫu lỗi";
      this.chartAvgLossMonth.SetTilte = "Biểu đồ TB % hao hụt";
      this.chartAvgCpkMonth.SetTilte = "Biểu đồ TB Cpk";
      this.chartAvgStdevMonth.SetTilte = "Biểu đồ TB Stdev";
      this.chartAvgSigmaMonth.SetTilte = "Biểu đồ TB Sigma";

      //Week
      //Set Title Chart 
      this.chartTopErrorWeek.SetTilte = "Biểu đồ Top % mẫu lỗi";
      this.chartTopLossWeek.SetTilte = "Biểu đồ Top % hao hụt";
      this.chartTopCpkWeek.SetTilte = "Biểu đồ Top Cpk";
      this.chartTopStdevWeek.SetTilte = "Biểu đồ Top Stdev";
      this.chartTopSigmaWeek.SetTilte = "Biểu đồ Top Sigma";

      //Set Title Chart 
      this.chartAgvErrorWeek.SetTilte = "Biểu đồ TB % mẫu lỗi";
      this.chartAgvLossWeek.SetTilte = "Biểu đồ TB % hao hụt";
      this.chartAgvCpkWeek.SetTilte = "Biểu đồ TB Cpk";
      this.chartAgvStdevWeek.SetTilte = "Biểu đồ TB Stdev";
      this.chartAgvSigmaWeek.SetTilte = "Biểu đồ TB Sigma";
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

    private int _yearCurrent = 0;
    private int _monthCurrent = 0;
    private int _weekCurrent = 0;
    private int _lineId = 0;
    private eReportConsumption _eReportConsumption = eReportConsumption.Month;
    private List<DatalogWeight> _datalogWeights = new List<DatalogWeight>();

    private void cbbLine_SelectedIndexChanged(object sender, EventArgs e)
    {
      var lineChoose = cbbLine.SelectedItem as InforLine;
      _lineId = (lineChoose != null) ? lineChoose.Id : 0;
    }

    private async void FrmConsumption_Load(object sender, EventArgs e)
    {
      CbbLines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();

      // Lấy tuần tháng năm hiện tại
      DateTime dt_curent = DateTime.Now;
      _monthCurrent = dt_curent.Month;
      _yearCurrent = dt_curent.Year;

      CultureInfo cul = CultureInfo.CurrentCulture;
      _weekCurrent = cul.Calendar.GetWeekOfYear(dt_curent, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

      var lineChoose = cbbLine.SelectedItem as InforLine;
      _lineId = (lineChoose != null) ? lineChoose.Id : 0;

      //Dữ liệu tháng hiện tại
      _datalogWeights = await AppCore.Ins.LoadDataMonth(_yearCurrent, _monthCurrent, _lineId);
      LoadDataShowUI_PageHome(_datalogWeights);
      LoadDataShowUI_PageMonth(_datalogWeights);

      //Dữ liệu tuần hiện tại
      var weight_list_week = await AppCore.Ins.LoadDataWeek(_yearCurrent, _weekCurrent, _lineId);
      LoadDataShowUI_PageWeek(weight_list_week);
    }

    private void LoadDataShowUI_PageHome(List<DatalogWeight> datalogWeights)
    {
      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogWeights);
      var consumptionCharts = AppCore.Ins.CvtDataReportExcelToConsumptionChart(dataReportExcels);
      var consumptionTable = AppCore.Ins.CvtDataReportExcelToConsumptionTable(dataReportExcels);

      SetDataChart_PageHome(consumptionCharts);
      SetDataTable_PageHome(consumptionTable);
    }

    private void LoadDataShowUI_PageMonth(List<DatalogWeight> datalogWeights)
    {
      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogWeights);
      var consumptionCharts = AppCore.Ins.CvtDataReportExcelToConsumptionChart(dataReportExcels);
      SetData_PageMonth(consumptionCharts);
    }

    private void LoadDataShowUI_PageWeek(List<DatalogWeight> datalogWeights)
    {
      var dataReportExcels = AppCore.Ins.GenerateDataReport(datalogWeights);
      var consumptionCharts = AppCore.Ins.CvtDataReportExcelToConsumptionChart(dataReportExcels);
      SetData_PageWeek(consumptionCharts);
    }


    #region Month
    private void btnMonth_Click(object sender, EventArgs e)
    {
      FrmFilterMonth frmFilterMonth = new FrmFilterMonth();
      frmFilterMonth.OnSendMonthChoose += FrmFilterMonth_OnSendMonthChoose;
      frmFilterMonth.Location = new System.Drawing.Point(10, 500);
      frmFilterMonth.ShowDialog();
    }
    private async void FrmFilterMonth_OnSendMonthChoose(int month)
    {
      _eReportConsumption = eReportConsumption.Month;
      _datalogWeights = await AppCore.Ins.LoadDataMonth(_yearCurrent, month, _lineId);
      LoadDataShowUI_PageHome(_datalogWeights);
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

    private async void FrmFilterWeek_OnSendWeekChoose(int week)
    {
      _eReportConsumption = eReportConsumption.Week;
      _datalogWeights = await AppCore.Ins.LoadDataWeek(_yearCurrent, week, _lineId);
      LoadDataShowUI_PageHome(_datalogWeights);
    }
    #endregion


    #region Day //Ko sd
    private void btnDay_Click(object sender, EventArgs e)
    {
      FrmFilterDate frmFilterDate = new FrmFilterDate(_monthCurrent);
      frmFilterDate.OnSendDateChoose += FrmFilterDate_OnSendDateChoose;
      frmFilterDate.Location = new System.Drawing.Point(200, 320);
      frmFilterDate.ShowDialog();
    }

    private async void FrmFilterDate_OnSendDateChoose(List<DateTime> listDate)
    {
      _eReportConsumption = eReportConsumption.Date;
      if (listDate.Count > 0)
      {
        var from = listDate.FirstOrDefault();
        var to = listDate.LastOrDefault();
        var weight_list_week = await AppCore.Ins.LoadDataByRange(from, to, _lineId);
        LoadDataShowUI_PageHome(weight_list_week);
      }
    }

    #endregion

    private async void btnLoad_Click(object sender, EventArgs e)
    {
      _eReportConsumption = eReportConsumption.Month;
      var lineChoose = cbbLine.SelectedItem as InforLine;
      _lineId = (lineChoose != null) ? lineChoose.Id : 0;
      _datalogWeights = await AppCore.Ins.LoadDataMonth(_yearCurrent, _monthCurrent, _lineId);
      LoadDataShowUI_PageHome(_datalogWeights);
    }

    private void btnExportPdf_Click(object sender, EventArgs e)
    {
      if (_datalogWeights?.Count()<=0)
      {
        new FrmNotification().ShowMessage("Không có dữ liệu xuất Report !", eMsgType.Warning);
        return;
      }  

      string titleReport = _eReportConsumption == eReportConsumption.Month ?
                          $"BÁO CÁO THÁNG {_monthCurrent}" :
                          $"BÁO CÁO TUẦN {_weekCurrent}";

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

      FrmReportAutoPdf frmReportAutoPdf = new FrmReportAutoPdf(_datalogWeights, pathReport, titleReport);
      frmReportAutoPdf.ShowDialog();
    }

    private void SetDataChart_PageHome(List<ConsumptionChart> consumptionV2s)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart_PageHome(consumptionV2s);
        }));
        return;
      }

      Dictionary<string, double> groupDataTopError = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopLoss = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopCpk = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopStdev = new Dictionary<string, double>();
      Dictionary<string, double> groupDataTopSigma= new Dictionary<string, double>();

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
      this.chartErrorHome.SetDataChart(groupDataTopError);
      this.chartLossHome.SetDataChart(groupDataTopLoss);
      this.chartCpkHome.SetDataChart(groupDataTopCpk);
      this.chartStdevHome.SetDataChart(groupDataTopStdev);
      this.chartSigmaHome.SetDataChart(groupDataTopSigma);


      //Dãy dưới
      this.chartavgErrorHome.SetDataChart(groupDataAverageError);
      this.chartavgLossHome.SetDataChart(groupDataAverageLoss);
      this.chartavgCpkHome.SetDataChart(groupDataAverageCpk);
      this.chartavgStdevHome.SetDataChart(groupDataAverageStdev);
      this.chartAvgSigmaHome.SetDataChart(groupDataAverageSigma);

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

    private void SetData_PageMonth(List<ConsumptionChart> consumptionCharts)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetData_PageMonth(consumptionCharts);
        }));
        return;
      }

      this.lbTitleMonth.Text = $"BÁO CÁO THÁNG {_monthCurrent}";

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
      foreach (var item in consumptionCharts)
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
      this.chartTopErrorMonth.SetDataChart(groupDataTopError);
      this.chartTopLossMonth.SetDataChart(groupDataTopLoss);
      this.chartTopCpkMonth.SetDataChart(groupDataTopCpk);
      this.chartTopStdevMonth.SetDataChart(groupDataTopStdev);
      this.chartTopSigmaMonth.SetDataChart(groupDataTopSigma);


      //Dãy dưới
      this.chartAvgErrorMonth.SetDataChart(groupDataAverageError);
      this.chartAvgLossMonth.SetDataChart(groupDataAverageLoss);
      this.chartAvgCpkMonth.SetDataChart(groupDataAverageCpk);
      this.chartAvgStdevMonth.SetDataChart(groupDataAverageStdev);
      this.chartAvgSigmaMonth.SetDataChart(groupDataAverageSigma);

      //Chart Pie
      var totalSample = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalSample);
      var totalOver = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalOver);
      var totalLower = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalLower);

      chartPieMoth.SetDataChart(totalSample, totalLower, totalOver);
      sumaryMoth.SetData(consumptionCharts.Count(), totalSample, totalLower + totalOver, totalOver);
    }

    private void SetData_PageWeek(List<ConsumptionChart> consumptionCharts)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetData_PageWeek(consumptionCharts);
        }));
        return;
      }

      this.lbTitleWeek.Text = $"BÁO CÁO TUẦN {_weekCurrent}";

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
      foreach (var item in consumptionCharts)
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
      this.chartTopErrorWeek.SetDataChart(groupDataTopError);
      this.chartTopLossWeek.SetDataChart(groupDataTopLoss);
      this.chartTopCpkWeek.SetDataChart(groupDataTopCpk);
      this.chartTopStdevWeek.SetDataChart(groupDataTopStdev);
      this.chartTopSigmaWeek.SetDataChart(groupDataTopSigma);

      //Dãy dưới
      this.chartAgvErrorWeek.SetDataChart(groupDataAverageError);
      this.chartAgvLossWeek.SetDataChart(groupDataAverageLoss);
      this.chartAgvCpkWeek.SetDataChart(groupDataAverageCpk);
      this.chartAgvStdevWeek.SetDataChart(groupDataAverageStdev);
      this.chartAgvSigmaWeek.SetDataChart(groupDataAverageSigma);

      //Chart Pie
      var totalSample = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalSample);
      var totalOver = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalOver);
      var totalLower = (int)consumptionCharts
                        .SelectMany(chart => chart.DetailConsumptions)
                        .Sum(detail => detail.TotalLower);

      chartPieWeek.SetDataChart(totalSample, totalLower, totalOver);
      sumaryWeek.SetData(consumptionCharts.Count(), totalSample, totalLower + totalOver, totalOver);
    }

    private void SetDataTable_PageHome(List<ConsumptionTable> consumptions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable_PageHome(consumptions);
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

    private void btnExxportPdfByWeek_Click(object sender, EventArgs e)
    {
      FrmExportPdfManual frmFilterWeek = new FrmExportPdfManual();
      frmFilterWeek.ShowDialog();
    }

    
  }
}
