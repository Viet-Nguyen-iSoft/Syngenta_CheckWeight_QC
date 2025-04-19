using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SynCheckWeigherLoggerApp.DashboardViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Color = System.Drawing.Color;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmHome : Form
  {
    public FrmHome()
    {
      InitializeComponent();
    }

    public InforLine _inforLine;

    public FrmHome(InforLine inforLine) : this()
    {
      this._inforLine = inforLine;
    }

    #region Singleton pattern
    private static Dictionary<int, FrmHome> _instances = new Dictionary<int, FrmHome>();

    public static FrmHome GetInstance(InforLine key)
    {
      if (!_instances.ContainsKey(key.Id) || _instances[key.Id].IsDisposed)
      {
        _instances[key.Id] = new FrmHome(key);
      }
      return _instances[key.Id];
    }

    #endregion



    private List<ShiftLeader> _ShiftLeaders = new List<ShiftLeader>();
    private List<ShiftType> _ShiftTypes = new List<ShiftType>();

    private Production _ProductCurrent = new Production();
    private ShiftLeader _ShiftLeaderCurrent = new ShiftLeader();
    private ShiftType _ShiftTypeCurrent = new ShiftType();

    public List<DatalogWeight> _listDatalogByLine { get; set; } = new List<DatalogWeight>();
    public List<DatalogWeight> _listDatalogShiftCurrrent { get; set; } = new List<DatalogWeight>();
    public List<TableDatalogDTO> _listDatatableByLine { get; set; } = new List<TableDatalogDTO>();

    private int _indexColData = 0;

    private int _colShift = 0;
    private int _colNo = 1;
    private int _colDatetime = 2;
    private int _colDataWeight = 3; //.....
    private int _colAvgRaw = 13;
    private int _colAvgTotal = 14;
    private int _colEvaluate = 15;


    private void FrmHome_Load(object sender, EventArgs e)
    {
      this.uCinforDataShift.SetTitle = "Ca";
      this.uCinforDataStd.SetTitle = "STDEV";
      this.uCinforDataAverage.SetTitle = "TB(Ca)";
      this.uCinforDataStardard.SetTitle = "Tiêu chuẩn";
      this.uCinforDataResult.SetTitle = "Kết quả";
      this.uCinforDataSample.SetTitle = "Tổng mẫu KT";
      this.uCinforDataErrorLower.SetTitle = "Lỗi TL thấp";
      this.uCinforDataErrorOver.SetTitle = "Lỗi TL cao";
      this.uCinforDataRate.SetTitle = "Tỉ lệ lỗi (%)";
      this.uCinforDataLoss.SetTitle = "Hao hụt (%)";

      SetInforLine();

      

      _listDatalogByLine = AppCore.Ins._listDatalogWeight?.Where(x => x.InforLineId == this._inforLine.Id).ToList();
      //Thống kê 3 ca
      LoadSumaryByShift(_listDatalogByLine);

      //Lấy danh sách ca hiện tại vs sản phẩm tương ứng
      Shift _shiftIdCurrent = AppCore.Ins.GetShiftCode(this._inforLine);
      _listDatalogShiftCurrrent = _listDatalogByLine?.Where(x => x.ProductionId == _ProductCurrent.Id && x.ShiftId == _shiftIdCurrent.Id).ToList();

      //ChartLine
      var dataChartLine = AppCore.Ins.CvtDatalogWeightToChartLine(_listDatalogShiftCurrrent, this._ProductCurrent);
      ucChartLine.SetDataChart(dataChartLine);

      //Table
      _listDatatableByLine = AppCore.Ins.ConvertToDTOList(_listDatalogShiftCurrrent);
      SetDataTable(_listDatatableByLine);

      //Histogram
      ucChartHistogram.SetDataChart(_listDatalogShiftCurrrent, this._ProductCurrent);


      //Event
      this.dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
      this.cbShiftLeader.SelectedIndexChanged += CbShiftLeader_SelectedIndexChanged;
      this.cbShiftTypes.SelectedIndexChanged += CbShiftTypes_SelectedIndexChanged;

      AppCore.Ins.OnSendStatusConnectWeight += Ins_OnSendStatusConnectWeight;
      AppCore.Ins.OnSendValueDatalogWeight += Ins_OnSendValueDatalogWeight;
      AppCore.Ins.OnSendWarning += Ins_OnSendWarning;
      FrmOverView.Instance.OnSendChooseProduct += Instance_OnSendChooseProduct;
      FrmOverView.Instance.OnSendChooseShiftLeader += Instance_OnSendChooseShiftLeader;
    }

    private void Instance_OnSendChooseShiftLeader(InforLine inforLine)
    {
      //if (inforLine.Id != this._inforLine.Id) return;

      //this.cbShiftLeader.SelectedIndexChanged -= CbShiftLeader_SelectedIndexChanged;

      ////this.cbShiftLeader.SelectedItem = inforLine.ShiftLeader;

      //this.cbShiftLeader.SelectedIndexChanged += CbShiftLeader_SelectedIndexChanged;
    }

    private void SetInforLine()
    {
      this.lbLineName.Text = _inforLine?.Name;

      this._ShiftLeaders = AppCore.Ins._listShiftLeader;
      this._ShiftTypes = AppCore.Ins._listShiftType;

      SetProduction();

      SetShiftLeader();

      SetShiftType();

      this.panelWeigher1.SetInforProduction(_ProductCurrent, _inforLine.eModeTare);
      this.panelWeigher1.SetSatutusConnectSerialWeigher(_inforLine.eStatusConnectWeight);

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
      this.panelWeigher1.SetInforProduct(production, _inforLine.eModeTare);
    }




    private void CbProductions_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;

        var productChoose = cbProductions.SelectedItem as Production;

        if (productChoose != null)
        {
          FrmConfirmChangeProduct frmInformation = new FrmConfirmChangeProduct(productChoose);
          frmInformation.OnSendOKClicked += FrmInformation_OnSendOKClicked;
          frmInformation.OnSendCancelClicked += FrmInformation_OnSendCancelClicked;
          frmInformation.ShowDialog();
        }
        else
        {
          new FrmNotification().ShowMessage("Không tìm thấy sản phẩm !", eMsgType.Warning);
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi sản phẩm thất bại !", eMsgType.Error);
      }
      finally
      {
        this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
      }
    }

    private async void FrmInformation_OnSendOKClicked(object sender)
    {
      try
      {
        var productChoose = sender as Production;

        this._inforLine.Productions.ToList().ForEach(x => x.IsEnable = false);
        this._inforLine.Productions.Where(x => x.Id == productChoose.Id).ToList().ForEach(x => x.IsEnable = true);
        this._inforLine.RequestTare = true;
        await AppCore.Ins.UpdateRange(this._inforLine.Productions.ToList());

        //Cập nhật Line
        this._inforLine.RequestTare = true;
        await AppCore.Ins.Update(this._inforLine);

        await AppCore.Ins.ReloadInforLine();

        _inforLine.ProductionCurrent = productChoose;

        SetInforProduct(_inforLine.ProductionCurrent);

        FrmOverView.Instance.FindAndUpdateProduct(_inforLine);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi sản phẩm thất bại !", eMsgType.Error);
      }
    }

    private void FrmInformation_OnSendCancelClicked(object sender)
    {

    }

    private void CbShiftTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void CbShiftLeader_SelectedIndexChanged(object sender, EventArgs e)
    {

    }






    private void Instance_OnSendChooseProduct(InforLine inforLine)
    {
      if (inforLine.Id != _inforLine.Id) return;

      //Hủy sự kiện
      this.cbProductions.SelectedIndexChanged -= CbProductions_SelectedIndexChanged;

      this.cbProductions.SelectedItem = inforLine.ProductionCurrent;
      SetInforProduct(inforLine.ProductionCurrent);

      //Đăng kí sự kiện
      this.cbProductions.SelectedIndexChanged += CbProductions_SelectedIndexChanged;
    }

    private void LoadSumaryByShift(List<DatalogWeight> listDatalogByLine)
    {
      var rs = AppCore.Ins.SumaryDTO(listDatalogByLine);
      foreach (var item in rs)
      {
        SetValueSumary(item);
      }

      FrmOverView.Instance.FindAndUpdateStatisticalData(this._inforLine, rs);
    }


    private void LoadUiWhenAddData()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadUiWhenAddData();
        }));
        return;
      }
      //Table
      _listDatatableByLine = AppCore.Ins.ConvertToDTOList(_listDatalogShiftCurrrent);
      SetDataTable(_listDatatableByLine);

      //ChartLine
      var dataChartLine = AppCore.Ins.CvtDatalogWeightToChartLine(_listDatalogShiftCurrrent, this._ProductCurrent);
      ucChartLine.SetDataChart(dataChartLine);

      //Histogram
      ucChartHistogram.SetDataChart(_listDatalogShiftCurrrent, this._ProductCurrent);
    }


    private void SetValueSumary(StatisticalData statisticalData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetValueSumary(statisticalData);
        }));
        return;
      }


      if (statisticalData != null)
      {
        this.uCinforDataShift.SetText(statisticalData.Index, statisticalData.Shift);
        this.uCinforDataStd.SetText(statisticalData.Index, statisticalData.Stdev.ToString());
        this.uCinforDataAverage.SetText(statisticalData.Index, statisticalData.Average.ToString());
        this.uCinforDataStardard.SetText(statisticalData.Index, statisticalData.Target.ToString());
        this.uCinforDataResult.SetText(statisticalData.Index, eNumHelper.GetDescription(statisticalData.eEvaluate));
        this.uCinforDataSample.SetText(statisticalData.Index, statisticalData.TotalSample.ToString());
        this.uCinforDataErrorLower.SetText(statisticalData.Index, statisticalData.NumberSampleLower.ToString());
        this.uCinforDataErrorOver.SetText(statisticalData.Index, statisticalData.NumberSampleOver.ToString());
        this.uCinforDataRate.SetText(statisticalData.Index, statisticalData.RateError.ToString() + "%");
        this.uCinforDataLoss.SetText(statisticalData.Index, statisticalData.RateLoss.ToString() + "%");

        if (statisticalData.eEvaluate == eEvaluate.Pass)
        {
          this.uCinforDataResult.SetBackColor(statisticalData.Index, Color.Green);
          this.uCinforDataResult.SetForeColor(statisticalData.Index, Color.White);
        }
        else
        {
          this.uCinforDataResult.SetBackColor(statisticalData.Index, Color.Red);
          this.uCinforDataResult.SetForeColor(statisticalData.Index, Color.White);
        }
      }
      else
      {
        this.uCinforDataShift.SetText(statisticalData.Index, "");
        this.uCinforDataStd.SetText(statisticalData.Index, "");
        this.uCinforDataAverage.SetText(statisticalData.Index, "");
        this.uCinforDataStardard.SetText(statisticalData.Index, "");
        this.uCinforDataResult.SetText(statisticalData.Index, "");
        this.uCinforDataSample.SetText(statisticalData.Index, "");
        this.uCinforDataErrorLower.SetText(statisticalData.Index, "");
        this.uCinforDataErrorOver.SetText(statisticalData.Index, "");
        this.uCinforDataRate.SetText(statisticalData.Index, "");
        this.uCinforDataLoss.SetText(statisticalData.Index, "");
        this.uCinforDataResult.SetBackColor(statisticalData.Index, Color.White);
      }
    }


    private void Ins_OnSendValueDatalogWeight(InforLine inforLine, DatalogWeight datalogWeight)
    {
      if (inforLine.Id == this._inforLine?.Id)
      {
        //Set Data hiện tại
        eEvaluateStatus eEvaluateStatus = AppCore.Ins.EvaluateRetureStatus(datalogWeight.Value, this._ProductCurrent);
        panelWeigher1.SetValueWeigherRealTime(datalogWeight.Value, eEvaluateStatus);

        // Thêm dô list data
        _listDatalogByLine.Add(datalogWeight);
        _listDatalogShiftCurrrent.Add(datalogWeight);

        //Thống kê 3 ca
        LoadSumaryByShift(_listDatalogByLine);

        LoadUiWhenAddData();
      }
    }

    public void SetDataTable(List<TableDatalogDTO> tableDatalogDTOs)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(tableDatalogDTOs);
        }));
        return;
      }

      dataGridView1.Rows.Clear();
      if (tableDatalogDTOs.Count() > 0)
      {
        foreach (var item in tableDatalogDTOs)
        {
          dataGridView1.Rows.Insert(0);
          _indexColData = item.DatalogWeights.Count();

          dataGridView1.Rows[0].Cells[_colShift].Value = item.Shift;
          dataGridView1.Rows[0].Cells[_colNo].Value = item.No;
          dataGridView1.Rows[0].Cells[_colDatetime].Value = item.DateTime;

          for (int i = 1; i <= _indexColData; i++)
          {
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Value = item.DatalogWeights.ElementAtOrDefault(i - 1).Value;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Tag = item.DatalogWeights.ElementAtOrDefault(i - 1);

            var color = AppCore.Ins.EvaluateRetureColor(item.DatalogWeights.ElementAtOrDefault(i - 1), this._ProductCurrent);
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.BackColor = color.Item1;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.ForeColor = color.Item2;
          }

          dataGridView1.Rows[0].Cells[_colAvgRaw].Value = item.AvgRaw;
          dataGridView1.Rows[0].Cells[_colAvgTotal].Value = item.AvgTotal;
          dataGridView1.Rows[0].Cells[_colEvaluate].Value = eNumHelper.GetDescription(item.eEvaluate);

          dataGridView1.Rows[0].Cells[_colEvaluate].Style.BackColor = item.eEvaluate == eEvaluate.Pass ? Color.Green : Color.Red;
          dataGridView1.Rows[0].Cells[_colEvaluate].Style.ForeColor = Color.White;
        }
      }

      _indexColData += 1;
    }


    private void Ins_OnSendWarning(InforLine inforLine, string content, eMsgType eMsgType)
    {
      if (inforLine.Id == this._inforLine?.Id)
      {
        new FrmNotification().ShowMessage(content, eMsgType);
      }
    }

    private void Ins_OnSendStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      this.panelWeigher1.SetSatutusConnectSerialWeigher(_inforLine.eStatusConnectWeight);
    }

    private void btnTare_Click(object sender, EventArgs e)
    {
      AppCore.Ins.eStatusModeWeight = eStatusModeWeight.TareForLine;

      FrmTare frmTare = new FrmTare(_inforLine);
      frmTare.OnSendChangeTypeTare += FrmTare_OnSendChangeTypeTare;
      frmTare.OnSendCloseFrmTare += FrmTare_OnSendCloseFrmTare;
      frmTare.OnSendConfirmValueTare += FrmTare_OnSendConfirmValueTare;
      frmTare.ShowDialog();
    }

    private void FrmTare_OnSendConfirmValueTare(DatalogTare datalogTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          FrmTare_OnSendConfirmValueTare(datalogTare);
        }));
        return;
      }

      panelWeigher1.SetValueTare(datalogTare);
      //Kiểm tra có yêu cầu Tare
      this.lbRequestTare.Visible = _inforLine.RequestTare;

      //Cập nhật Tare ID
      FrmOverView.Instance.UpdateTareId(_inforLine, datalogTare);
    }

    private void FrmTare_OnSendCloseFrmTare()
    {
      AppCore.Ins.eStatusModeWeight = eStatusModeWeight.WeightForLine;
    }

    private async void FrmTare_OnSendChangeTypeTare()
    {
      await AppCore.Ins.Update(_inforLine);
      panelWeigher1.SetInforProduction(_ProductCurrent, _inforLine.eModeTare);

      FrmOverView.Instance.FindAndUpdateTypeTare(_inforLine);
    }



    private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      //int colmnIndex = e.ColumnIndex;
      //int rowIndex = e.RowIndex;

      //if (rowIndex == -1) return;
      //if (colmnIndex >= 3 && colmnIndex <= 17)
      //{
      //  //Value Sample cân lại
      //  object cellValueSample = dataGridView1.Rows[e.RowIndex].Cells["Column19"].Value;
      //  if (cellValueSample != null)
      //  {
      //    int id = Convert.ToInt16(cellValueSample);
      //    var sample = listSamplesCurentDB?.Where(x => x.DatalogId == id).ToList();
      //    if (sample != null)
      //    {
      //      var sampleDetail = sample.Where(x => x.LocalId == colmnIndex - 2).FirstOrDefault();
      //      if (sampleDetail != null)
      //      {
      //        if (sampleDetail.isEdited)
      //        {
      //          FrmSeeHistoricalReweigher frmSeeHistoricalReweigher = new FrmSeeHistoricalReweigher(sampleDetail);
      //          frmSeeHistoricalReweigher.ShowDialog();
      //        }
      //      }
      //    }
      //  }
      //}
    }




    private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int colmnIndex = e.ColumnIndex;
      int rowIndex = e.RowIndex;

      if (rowIndex == -1) return;

      if (colmnIndex >= 3 && colmnIndex < 13)
      {
        var tag = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
        if (tag == null) return;

        //Value Sample cân lại
        var cellValueSample = (KeyValuePair<int, double>)tag;

        var key = cellValueSample.Key;
        var value = cellValueSample.Value;

        DatalogWeight datalogWeight = _listDatalogShiftCurrrent?.FirstOrDefault(x => x.Id == cellValueSample.Key);
        if (datalogWeight != null)
        {
          AppCore.Ins.eStatusModeWeight = eStatusModeWeight.Reweight;

          FrmSampleRework frmSampleRework = new FrmSampleRework(datalogWeight);
          frmSampleRework.OnSendReWeigherDone += FrmSampleRework_OnSendReWeigherDone;
          frmSampleRework.ShowDialog();
          frmSampleRework.OnSendReWeigherDone -= FrmSampleRework_OnSendReWeigherDone;

          AppCore.Ins.eStatusModeWeight = eStatusModeWeight.WeightForLine;
        }
      }
    }

    private async void FrmSampleRework_OnSendReWeigherDone(DatalogWeight datalogWeight)
    {
      await AppCore.Ins.Update(datalogWeight);

      var item = _listDatalogShiftCurrrent.FirstOrDefault(d => d.Id == datalogWeight.Id);

      if (item != null)
      {
        item.Value = datalogWeight.Value;
        item.IsChange = datalogWeight.IsChange;

        //Thống kê 3 ca
        LoadSumaryByShift(_listDatalogByLine);

        LoadUiWhenAddData();
      }
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


  }
}
