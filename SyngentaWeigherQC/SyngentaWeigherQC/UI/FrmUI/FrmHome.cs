using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SynCheckWeigherLoggerApp.DashboardViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Color = System.Drawing.Color;
using DateTime = System.DateTime;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmHome : Form
  {
    public FrmHome()
    {
      InitializeComponent();
    }

    public FrmHome(InforLine inforLine) : this()
    {
      _inforLine = inforLine;
    }

    #region Singleton pattern
    private static FrmHome _Instance = null;
    public static FrmHome Instance(InforLine inforLine)
    {
      if (_Instance == null || _Instance.IsDisposed)
      {
        _Instance = new FrmHome(inforLine);
      }
      else
      {
        _inforLine = inforLine;
        _Instance.SetInforLine();
      }
      return _Instance;
    }


    #endregion


    private static InforLine _inforLine;
    private List<ShiftLeader> _ShiftLeaders = new List<ShiftLeader>();
    private List<ShiftType> _ShiftTypes = new List<ShiftType>();

    private Production _ProductCurrent = new Production();
    private ShiftLeader _ShiftLeaderCurrent = new ShiftLeader();
    private ShiftType _ShiftTypeCurrent = new ShiftType();
    
    private void SetInforLine()
    {
      this.lbLineName.Text = _inforLine.Name;

      this._ShiftLeaders = AppCore.Ins._listShiftLeader;
      this._ShiftTypes = AppCore.Ins._listShiftType;

      SetProduction();

      SetShiftLeader();

      SetShiftType();

      panelWeigher1.SetInforTare(_ProductCurrent, _inforLine.eModeTare, DateTime.Now);

      //Kiểm tra có yêu cầu Tare
      this.lbRequestTare.Visible = _inforLine.RequestTare;
    }

    private void SetProduction()
    {
      //Check Chọn Product
      var listProducts = _inforLine.Productions.ToList();
      ListProduction = listProducts;

      _ProductCurrent = listProducts?.FirstOrDefault(x => x.IsEnable);
      if (_ProductCurrent != null)
      {
        this.cbProductions.SelectedItem = _ProductCurrent;
        SetInforProduct(_ProductCurrent);
      }
      else
      {
        this.cbProductions.SelectedIndex = -1;
      }
    }

    public void SetShiftLeader()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetShiftLeader();
        }));
        return;
      }

      ShiftLeader = this._ShiftLeaders;

      _ShiftLeaderCurrent = this._ShiftLeaders?.FirstOrDefault(x => x.Id == _inforLine.ShiftLeaderId);
      if (_ShiftLeaderCurrent != null)
      {
        this.cbShiftLeader.SelectedItem = _ShiftLeaderCurrent;
      }
      else
      {
        this.cbShiftLeader.SelectedIndex = -1;
      }
    }

    public void SetShiftType()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetShiftType();
        }));
        return;
      }
      ShiftType = this._ShiftTypes;

      _ShiftTypeCurrent = this._ShiftTypes?.FirstOrDefault(x => x.Id == _inforLine.ShiftTypesId);
      if (_ShiftTypeCurrent != null)
      {
        this.cbShiftTypes.SelectedItem = _ShiftTypeCurrent;
      }
      else
      {
        this.cbShiftTypes.SelectedIndex = -1;
      }
    }

    public void SetInforProduct(Production production)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforProduct(production);
        }));
        return;
      }
      this.lbDensity.Text = $"{production?.Density}";

      if (production != null)
      {
        this.panelWeigher1.SetDataInforProduct(production.PackSize, production.StandardFinal, production.UpperLimitFinal, production.LowerLimitFinal);
      }

      this.panelWeigher1.SetValueTare(0, production.StandardFinal,
                                         production.UpperLimitFinal,
                                         production.LowerLimitFinal,
                                         1,
                                         DateTime.Now);
    }


    #region Custom Get Set Data
    public List<Production> ListProduction
    {
      set
      {
        this.cbProductions.DataSource = value;
        this.cbProductions.DisplayMember = "Name";
      }
    }
    public List<ShiftType> ShiftType
    {
      set
      {
        this.cbShiftTypes.DataSource = value;
        this.cbShiftTypes.DisplayMember = "Name";
      }
    }

    public List<ShiftLeader> ShiftLeader
    {
      set
      {
        this.cbShiftLeader.DataSource = value;
        this.cbShiftLeader.DisplayMember = "UserName";
      }
    }
    #endregion


    private void FrmHome_Load(object sender, EventArgs e)
    {
      this.uCinforDataShift.SetTitle = "Ca";
      this.uCinforDataStd.SetTitle = "STDEV";
      this.uCinforDataAverage.SetTitle = "TB(Ca)";
      this.uCinforDataStardard.SetTitle = "Tiêu chuẩn";
      this.uCinforDataResult.SetTitle = "Kết quả";
      this.uCinforDataSample.SetTitle = "Tổng mẫu kiểm tra";
      this.uCinforDataErrorLower.SetTitle = "Lỗi trọng lượng thấp";
      this.uCinforDataErrorOver.SetTitle = "Lỗi trọng lượng cao";
      this.uCinforDataRate.SetTitle = "Tỉ lệ lỗi (%)";
      this.uCinforDataLoss.SetTitle = "Hao hụt (%)";


      SetInforLine();

      return;
     

      numberGetRaw = Properties.Settings.Default.numberRaw;

      AppCore.Ins.OnSendUpdateDataDone += Ins_OnSendUpdateDataDone;
      AppCore.Ins.OnSendDataRealTimeWeigherHome += Ins_OnSendDataRealTimeWeigherHome;
      FrmMain.Instance.OnSendChooseProduct += Instance_OnSendChooseProduct;
      FrmSettingGeneral.Instance.OnSendSettingNumberRaw += Instance_OnSendSettingNumberRaw;
      Ins_OnSendUpdateDataDone();

      this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
      this.dataGridView1.CellClick += DataGridView1_CellClick;

      if (AppCore.Ins._inforValueSettingStation.IsChangeProductNoTare == true)
      {
        //this.panelInforTare.Visible = true;
        this.tableLayoutPanelCal.Visible = false;
      }
      else
      {
        //this.panelInforTare.Visible = false;
        this.tableLayoutPanelCal.Visible = true;
      }
    }

    private void btnTare_Click(object sender, EventArgs e)
    {
      AppCore.Ins.eStatusModeWeight = eStatusModeWeight.TareForLine;

      FrmTare frmTare = new FrmTare(_inforLine);
      frmTare.OnSendChangeTypeTare += FrmTare_OnSendChangeTypeTare;
      frmTare.OnSendCloseFrmTare += FrmTare_OnSendCloseFrmTare;
      frmTare.ShowDialog();
    }

    private void FrmTare_OnSendCloseFrmTare()
    {
      AppCore.Ins.eStatusModeWeight = eStatusModeWeight.WeightForLine;
    }

    private async void FrmTare_OnSendChangeTypeTare()
    {
      await AppCore.Ins.Update(_inforLine);
      panelWeigher1.SetInforTare(_ProductCurrent, _inforLine.eModeTare, DateTime.Now);

      FrmOverView.Instance.FindAndUpdateTypeTare(_inforLine);
    }

    private int numberGetRaw = 10;

    private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      int colmnIndex = e.ColumnIndex;
      int rowIndex = e.RowIndex;

      if (rowIndex == -1) return;
      if (colmnIndex >= 3 && colmnIndex <= 17)
      {
        //Value Sample cân lại
        object cellValueSample = dataGridView1.Rows[e.RowIndex].Cells["Column19"].Value;
        if (cellValueSample != null)
        {
          int id = Convert.ToInt16(cellValueSample);
          var sample = listSamplesCurentDB?.Where(x => x.DatalogId == id).ToList();
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

  
   
    private void Instance_OnSendSettingNumberRaw(int numberRaw)
    {
      numberGetRaw = numberRaw;
      Ins_OnSendUpdateDataDone();
    }

    private void Instance_OnSendChooseProduct(Models.Production production)
    {
      SetDataTable(null, null, null);
      SetChartLine(null);
      SetChartHistogram(null, null);
    }


    public void ConnectSerialWeigher(bool isConnect)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ConnectSerialWeigher(isConnect);
        }));
        return;
      }

      this.panelWeigher1.SetSatutusConnectSerialWeigher(isConnect);
    }


    private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int colmnIndex = e.ColumnIndex;
      int rowIndex = e.RowIndex;

      if (rowIndex == -1) return;

      if (colmnIndex >= 3 && colmnIndex < 18)
      {
        //Value Sample cân lại
        object cellValueSample = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        double valueSample = Convert.ToDouble(cellValueSample);


        if (valueSample > 0)
        {
          //Check xem có giới hạn cân lại mỗi ca không
          if (Properties.Settings.Default.isEnableLimitChangeRecord)
          {
            if (numberChangeDataSampleForShift >= Properties.Settings.Default.NumberChangeRecordForShift)
            {
              new FrmNotification().ShowMessage("Số lần thay đổi dữ liệu vượt quá cài đặt cho phép !", eMsgType.Warning);
              return;
            }
          }

          //Truy tìm Info Sample
          //VỊ trí DatalogId
          object cellValueDatalogId = dataGridView1.Rows[e.RowIndex].Cells["Column19"].Value;
          int valueDatalogId = Convert.ToUInt16(cellValueDatalogId);

          //Thứ tự Sample
          int valueLocal = colmnIndex - 2;
          Sample reWeigher = listSamples?.Where(x => x.DatalogId == valueDatalogId && x.LocalId == valueLocal).FirstOrDefault();

          if (reWeigher != null)
          {
            if (!reWeigher.isEdited)
            {
              AppCore.Ins._eWeigherMode = eWeigherMode.SampleRework;
              FrmSampleRework frmSampleRework = new FrmSampleRework(reWeigher);
              frmSampleRework.OnSendReWeigherDone += FrmSampleRework_OnSendReWeigherDone;
              frmSampleRework.ShowDialog();
              frmSampleRework.OnSendReWeigherDone -= FrmSampleRework_OnSendReWeigherDone;
            }

          }
        }
      }
    }

    private async void FrmSampleRework_OnSendReWeigherDone(Sample sample)
    {
      DateTime dt_fileDB = AppCore.Ins.GetDataTimeFileDB(DateTime.Now);

      AppCore.Ins._eWeigherMode = eWeigherMode.Normal;
      await AppCore.Ins.Update(sample, dt_fileDB);
      //AppCore.Ins.ReWeigher();

      Ins_OnSendUpdateDataDone();
      numberChangeDataSampleForShift++;
    }


    private List<Sample> listSamples = new List<Sample>();
    private List<DatalogWeight> listDatalogs = new List<DatalogWeight>();
    private bool isStartApp = true;
    private List<Sample> listSamplesCurentDB = new List<Sample>();
    public int numberChangeDataSampleForShift = 0;
    private async void Ins_OnSendUpdateDataDone()
    {
      //try
      //{
      //  //Load Datalog theo Group Id hiện tại
      //  DateTime dt_fileDB = AppCore.Ins.GetDataTimeFileDB(DateTime.Now);
      //  int groupId = AppCore.Ins.groupId_DB;

      //  //Load Datalog && Sample
      //  listDatalogs = await AppCore.Ins.LoadAllDatalogs(dt_fileDB);
      //  listSamples = await AppCore.Ins.LoadAllSamples(dt_fileDB);

      //  //Lits data hiển thị Table
      //  List<DatalogWeight> listDatalogsDashBoard = new List<DatalogWeight>();
      //  List<DatalogWeight> listDatalogsDashBoard_V2 = new List<DatalogWeight>();
      //  List<Sample> listSamplesDashBoard = new List<Sample>();


      //  //Lấy loại ca đang chọn
      //  Models.ShiftType shiftType = AppCore.Ins._shiftTypeCurrent;
      //  List<StatisticalData> listStatisticalData = new List<StatisticalData>();

      //  if (listDatalogs == null) return;

      //  if (listDatalogs.Count > 0)
      //  {
      //    //Tính toán theo từng ca
      //    var datalogsGroupByShift = listDatalogs.GroupBy(p => new { p.ShiftId, p.ProductId });

      //    foreach (var datalog in datalogsGroupByShift)
      //    {
      //      listDatalogsDashBoard = datalog.ToList();
      //      List<int> listValueId = listDatalogsDashBoard?.Select(d => d.Id).ToList();
      //      listSamplesDashBoard = AppCore.Ins.GetDataSampleByListIdDatalog(listSamples, listValueId);

      //      if (listDatalogsDashBoard?.Count > 0 && listSamplesDashBoard?.Count > 0)
      //      {
      //        if (!isStartApp)
      //        {
      //          if (listDatalogsDashBoard.FirstOrDefault().ShiftId == AppCore.Ins._shiftIdCurrent)
      //          {
      //            StatisticalData statisticalData = DataProcessing(listDatalogsDashBoard, listSamplesDashBoard);
      //            if (statisticalData != null)
      //              listStatisticalData.Add(statisticalData);
      //          }
      //        }
      //        else
      //        {
      //          StatisticalData statisticalData = DataProcessing(listDatalogsDashBoard, listSamplesDashBoard);
      //          if (statisticalData != null)
      //            listStatisticalData.Add(statisticalData);
      //        }

      //      }

      //      if (listDatalogsDashBoard.FirstOrDefault().ShiftId == AppCore.Ins._shiftIdCurrent)
      //      {
      //        listDatalogsDashBoard_V2 = listDatalogsDashBoard;
      //        listSamplesCurentDB = listSamplesDashBoard;
      //      }
      //    }

      //    //Check Case đặc biệt
      //    if (AppCore.Ins._currentProduct == null)
      //    {
      //      SetDataTable(null, null, null);
      //      SetChartLine(null);
      //      SetChartHistogram(null, null);
      //      return;
      //    }

      //    if (listDatalogsDashBoard.FirstOrDefault().ProductId != AppCore.Ins._currentProduct.Id)
      //    {
      //      SetDataTable(null, null, null);
      //      SetChartLine(null);
      //      SetChartHistogram(null, null);
      //      return;
      //    }

      //    if (isStartApp)
      //    {
      //      numberChangeDataSampleForShift = GetNumberChangeSample(listDatalogsDashBoard_V2, listSamplesCurentDB);
      //    }

      //    //Show DashBoard
      //    //SetDataTable(listDatalogsDashBoard, listSamplesDashBoard, AppCore.Ins._currentProduct);
      //    //20/05/2024
      //    SetDataTable(listDatalogsDashBoard_V2, listSamplesCurentDB, AppCore.Ins._currentProduct);
      //    //return;
      //    //Chart Line
      //    ChartLineData chartLineData = ProcessingDataChartLine(listDatalogsDashBoard, listSamplesDashBoard, AppCore.Ins._currentProduct);
      //    SetChartLine(chartLineData);

      //    //Chart Histogram
      //    SetChartHistogram(listSamplesDashBoard, AppCore.Ins._currentProduct);




      //    //Tính toán tổng kết mỗi shift
      //    if (AppCore.Ins._shiftIdCurrent != -1 && listStatisticalData?.Count > 0)
      //    {
      //      if (!isStartApp)
      //      {
      //        StatisticalData statisticalData = listStatisticalData.Where(x => x.Shift == AppCore.Ins._shiftIdCurrent).LastOrDefault();

      //        if (AppCore.Ins._shiftIdCurrent == 1 || AppCore.Ins._shiftIdCurrent == 4 || AppCore.Ins._shiftIdCurrent == 6)
      //        {
      //          var data = listStatisticalData?.Where(x => x.Shift == AppCore.Ins._shiftIdCurrent).LastOrDefault();
      //          UpdateSynthetic(data, 0);
      //        }
      //        else if (AppCore.Ins._shiftIdCurrent == 2 || AppCore.Ins._shiftIdCurrent == 5)
      //        {
      //          var data = listStatisticalData?.Where(x => x.Shift == AppCore.Ins._shiftIdCurrent).LastOrDefault();
      //          UpdateSynthetic(data, 1);
      //        }
      //        else if (AppCore.Ins._shiftIdCurrent == 3)
      //        {
      //          var data = listStatisticalData?.Where(x => x.Shift == AppCore.Ins._shiftIdCurrent).LastOrDefault();
      //          UpdateSynthetic(data, 2);
      //        }
      //      }
      //      else
      //      {
      //        StatisticalData statisticalDataIndex0 = null;
      //        StatisticalData statisticalDataIndex1 = null;
      //        StatisticalData statisticalDataIndex2 = null;

      //        if (shiftType.Code == 0)
      //        {
      //          statisticalDataIndex0 = listStatisticalData?.Where(x => x.Shift == 1).LastOrDefault();
      //          statisticalDataIndex1 = listStatisticalData?.Where(x => x.Shift == 2).LastOrDefault();
      //          statisticalDataIndex2 = listStatisticalData?.Where(x => x.Shift == 3).LastOrDefault();
      //        }
      //        else if (shiftType.Code == 1)
      //        {
      //          statisticalDataIndex0 = listStatisticalData?.Where(x => x.Shift == 4).LastOrDefault();
      //          statisticalDataIndex1 = listStatisticalData?.Where(x => x.Shift == 5).LastOrDefault();
      //        }
      //        else if (shiftType.Code == 2)
      //        {
      //          statisticalDataIndex0 = listStatisticalData?.Where(x => x.Shift == 6).LastOrDefault();
      //        }

      //        UpdateSynthetic(statisticalDataIndex0, 0);
      //        UpdateSynthetic(statisticalDataIndex1, 1);
      //        UpdateSynthetic(statisticalDataIndex2, 2);
      //      }
      //    }

      //    isStartApp = false;
      //  }
      //}
      //catch (Exception ex)
      //{
      //  eLoggerHelper.LogErrorToFileLog(ex);
      //}
    }


    private ChartLineData ProcessingDataChartLine(List<DatalogWeight> datalogs, List<Sample> samples, Models.Production productions)
    {
      if (datalogs == null) return null;
      if (datalogs.Count <= 0) return null;

      List<double> averageRaw = new List<double>();
      double averageTotal = Math.Round(samples.Average(x => x.Value), 2);
      //datalogs.Reverse();
      foreach (DatalogWeight datalog in datalogs)
      {
        double average = 0;
        var ave = samples.Where(x => x.DatalogId == datalog.Id).ToList();
        if (ave?.Count > 0)
        {
          average = ave.Average(x => x.Value);
        }

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
      ////try
      ////{
      ////  DatalogWeight datalogFirst = datalogs.FirstOrDefault();
      ////  List<double> valueSamples = samples.Select(x => x.Value).ToList();
      ////  Models.Production productions = AppCore.Ins._listProductsWithStation?.Where(x => x.Id == datalogFirst.ProductId).FirstOrDefault();
      ////  if (productions == null)
      ////  {
      ////    return null;
      ////  }
      ////  var numbersOver = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value > productions.UpperLimitFinal).Count() : 0;
      ////  var numbersLower = (samples != null) ? samples?.Where(x => x.isHasValue == true && x.isEnable == true && x.Value < productions.LowerLimitFinal).Count() : 0;

      ////  double stdev = AppCore.Ins.Stdev(valueSamples);
      ////  double average = Math.Round(valueSamples.Average(), 2);
      ////  double target = productions.StandardFinal;

      ////  string result = (average >= target) ? "ĐẠT" : "KHÔNG ĐẠT";

      ////  int totalSamples = samples.Count();
      ////  int numberSamplesOver = (int)samples?.Where(x => x.Value > productions.UpperLimitFinal).Count();
      ////  int numberSamplesLower = (int)samples?.Where(x => x.Value < productions.LowerLimitFinal).Count();

      ////  double rateError = (totalSamples != 0) ? ((double)((numberSamplesOver + numberSamplesLower) * 100) / (double)(totalSamples)) : 0;
      ////  rateError = Math.Round(rateError, 2);

      ////  double rateLoss = (average > target) ? Math.Round((((average - target) * 100) / target), 2) : 0;

      ////  StatisticalData statisticalData = new StatisticalData()
      ////  {
      ////    Shift = datalogFirst.ShiftId,
      ////    Stdev = stdev,
      ////    Average = average,
      ////    Target = productions.StandardFinal,
      ////    Result = result,
      ////    TotalSample = valueSamples.Count(),
      ////    NumberSampleOver = numberSamplesOver,
      ////    NumberSampleLower = numberSamplesLower,
      ////    RateError = rateError,
      ////    RateLoss = rateLoss,
      ////  };

      ////  return statisticalData;
      ////}
      ////catch (Exception ex)
      ////{
      ////  eLoggerHelper.LogErrorToFileLog(ex);
      ////  return null;
      ////}

    }

    private async void LoadDataDB()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataDB();
        }));
        return;
      }

      DateTime dt_fileDB = DateTime.Now;
      //Check shift
      if (dt_fileDB.Hour >= 0 && dt_fileDB.Hour < 6)
      {
        dt_fileDB = DateTime.Now.AddDays(-1);
      }

      Models.Production productions = AppCore.Ins._currentProduct;
      if (productions != null)
      {
        int productId = AppCore.Ins._currentProduct.Id;
        //Load Datalog
        List<DatalogWeight> datalogs = await AppCore.Ins.LoadAllDatalogsByProductId(productId, dt_fileDB);


        //CHia theo ca
        if (datalogs.Count > 0)
        {
          dataGridView1.Rows.Clear();
          datalogs.Reverse();
          int stt = datalogs.Count();
          var dataByShift = datalogs.GroupBy(x => x.ShiftId);
          foreach (var datalogChid in dataByShift)
          {
            List<DatalogWeight> datalogsUpdate = datalogChid.ToList();

            if (datalogsUpdate.Count() <= 0) return;

            List<int> listGroupIdDatalog = datalogsUpdate?.Select(d => d.Id).ToList();
            //Load all sample
            List<Sample> samples = await AppCore.Ins.LoadAllSamplesByDatalogId(listGroupIdDatalog, dt_fileDB);
          }
        }
      }
    }

    private void SetChartLine(ChartLineData chartLineData)
    {
      ucChartLine.SetDataChart(chartLineData);
    }

    private void SetChartHistogram(List<Sample> samples, Models.Production productions)
    {
      ucChartHistogram.SetDataChart(samples, productions);
    }

    private void SetDataTable(List<DatalogWeight> datalogs, List<Sample> samples, Models.Production productions)
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
        dataGridView1.Rows.Clear();
        if (datalogs == null) return;
        if (datalogs.Count() <= 0) return;

        double averageTotal = Math.Round(samples.Average(x => x.Value), 2);
        double stdev = Math.Round(AppCore.Ins.Stdev(samples.Select(x => x.Value).ToList()), 2);
        int stt = datalogs.Count();

        if (stt > numberGetRaw)
          datalogs = datalogs.Skip(stt - numberGetRaw).ToList();

        datalogs.Reverse();
        foreach (var datalog in datalogs)
        {
          List<Sample> samplesChid = samples?.Where(x => x.DatalogId == datalog.Id).ToList();

          double averageRaw = Math.Round(samplesChid.Average(x => x.Value), 2);

          int row = dataGridView1.Rows.Add();
          string shift = (datalog.ShiftId != -1) ? AppCore.Ins._listShift.Where(x => x.CodeShift == datalog.ShiftId).Select(x => x.Name).FirstOrDefault() : "";

          dataGridView1.Rows[row].Cells["Column1"].Value = shift;
          dataGridView1.Rows[row].Cells["Column2"].Value = stt--;
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
          dataGridView1.Rows[row].Cells[18].Style.BackColor = (averageRaw >= productions.StandardFinal) ? Color.Green : Color.Red;
          dataGridView1.Rows[row].Cells[18].Style.ForeColor = Color.White;
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private int GetNumberChangeSample(List<DatalogWeight> datalogs, List<Sample> samples)
    {
      try
      {
        int numberChangeSample = 0;
        foreach (var datalog in datalogs)
        {
          List<Sample> samplesChid = samples?.Where(x => x.DatalogId == datalog.Id).ToList();
          numberChangeSample += samplesChid.Count(x => x.isEdited == true);
        }

        return numberChangeSample;
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        return 0;
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



    public void UpdateInforProductCurrent(Models.Production production)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateInforProductCurrent(production);
        }));
        return;
      }

      try
      {
        if (production != null)
        {
          this.panelWeigher1.SetDataInforProduct(production.PackSize, production.StandardFinal, production.UpperLimitFinal, production.LowerLimitFinal);
          //this.txtNumberSample.Texts = production.FillterMax.ToString();
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }

    }

    public void ChangeProduct(bool isChangeProduct)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ChangeProduct(isChangeProduct);
        }));
        return;
      }

      //this.panelInforTare.Visible = isChangeProduct;
      this.tableLayoutPanelCal.Visible = !isChangeProduct;
      AppCore.Ins._readyReceidDataWeigher = (isChangeProduct) ? eReadyReceidWeigher.No : eReadyReceidWeigher.Yes;
    }

    public void UpdateValueTareUI(double avg, double standard, double upper, double lower, int isLabel, DateTime dateTime)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateValueTareUI(avg, standard, upper, lower, isLabel, dateTime);
        }));
        return;
      }

      this.panelWeigher1.SetValueTare(avg, standard, upper, lower, isLabel, dateTime);
    }

    private void Ins_OnSendDataRealTimeWeigherHome(double value, eValuate eValuate)
    {
      this.panelWeigher1.SetValueWeigherRealTime(value, eValuate);
    }



    public void ClearHome()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ClearHome();
        }));
        return;
      }

      UpdateSynthetic(null, 0);
      UpdateSynthetic(null, 1);
      UpdateSynthetic(null, 2);

      dataGridView1.Rows.Clear();

      ucChartLine.SetDataChart(null);
      ucChartHistogram.SetDataChart(null, null);

    }

    private void UpdateSynthetic(StatisticalData statisticalData, int id)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateSynthetic(statisticalData, id);
        }));
        return;
      }

      if (statisticalData != null)
      {
        this.uCinforDataShift.SetText(id, AppCore.Ins._listShift?.Where(x => x.CodeShift == statisticalData.Shift).FirstOrDefault()?.Name);
        this.uCinforDataStd.SetText(id, statisticalData.Stdev.ToString());
        this.uCinforDataAverage.SetText(id, statisticalData.Average.ToString());
        this.uCinforDataStardard.SetText(id, statisticalData.Target.ToString());
        this.uCinforDataResult.SetText(id, statisticalData.Result.ToString());
        this.uCinforDataSample.SetText(id, statisticalData.TotalSample.ToString());
        this.uCinforDataErrorLower.SetText(id, statisticalData.NumberSampleLower.ToString());
        this.uCinforDataErrorOver.SetText(id, statisticalData.NumberSampleOver.ToString());
        this.uCinforDataRate.SetText(id, statisticalData.RateError.ToString() + "%");
        this.uCinforDataLoss.SetText(id, statisticalData.RateLoss.ToString() + "%");

        if (statisticalData.Result == "ĐẠT")
        {
          this.uCinforDataResult.SetBackColor(id, Color.Green);
          this.uCinforDataResult.SetForeColor(id, Color.White);
        }
        else
        {
          this.uCinforDataResult.SetBackColor(id, Color.Red);
          this.uCinforDataResult.SetForeColor(id, Color.White);
        }
      }
      else
      {
        this.uCinforDataShift.SetText(id, "");
        this.uCinforDataStd.SetText(id, "");
        this.uCinforDataAverage.SetText(id, "");
        this.uCinforDataStardard.SetText(id, "");
        this.uCinforDataResult.SetText(id, "");
        this.uCinforDataSample.SetText(id, "");
        this.uCinforDataErrorLower.SetText(id, "");
        this.uCinforDataErrorOver.SetText(id, "");
        this.uCinforDataRate.SetText(id, "");
        this.uCinforDataLoss.SetText(id, "");
        this.uCinforDataResult.SetBackColor(id, Color.White);
      }


    }

  }
}
