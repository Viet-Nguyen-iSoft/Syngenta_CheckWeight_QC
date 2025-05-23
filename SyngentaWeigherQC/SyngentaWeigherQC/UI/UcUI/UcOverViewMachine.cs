using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Production = SyngentaWeigherQC.Models.Production;


namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcOverViewMachine : UserControl
  {
    //Sự kiện
    public delegate void SendChangeShiftLeader(InforLine inforLine);
    public event SendChangeShiftLeader OnSendChangeShiftLeader;

    public delegate void SendChangeShiftType(InforLine inforLine);
    public event SendChangeShiftType OnSendChangeShiftType;

    public delegate void SendChooseLineWeight(InforLine inforLine);
    public event SendChooseLineWeight OnSendChooseLineWeight;

    public delegate void SendChooseProduct(InforLine inforLine);
    public event SendChooseProduct OnSendChooseProduct;

    public UcOverViewMachine()
    {
      InitializeComponent();
      SetStatusConnectWeight(eStatusConnectWeight.Connected);
    }

    private InforLine _inforLine;
    private ShiftLeader _ShiftLeaderCurrent = new ShiftLeader();
    private ShiftType _ShiftTypeCurrent = new ShiftType();
    private List<ShiftLeader> _ShiftLeaders = new List<ShiftLeader>();
    private List<ShiftType> _ShiftTypes = new List<ShiftType>();

    public UcOverViewMachine(InforLine line) : this()
    {
      _inforLine = line;

      //Tên line
      Title = this._inforLine.Name;

      //Loại Tare
      TypeTare = this._inforLine.eModeTare;

      //Check Chọn Product
      var listProducts = this._inforLine.Productions.Where(x=>x.IsDelete==false).ToList();
      ListProduction = listProducts;

      //Check Last Product 
      _inforLine.ProductionCurrent = listProducts?.FirstOrDefault(x => x.IsEnable);
      if (_inforLine.ProductionCurrent != null)
      {
        this.cbProductions.SelectedItem = _inforLine.ProductionCurrent;
        SetInforProduct(_inforLine.ProductionCurrent);
      }
      else
      {
        this.cbProductions.SelectedIndex = -1;
      }

      ChartHelper.SetDataChartPie(chartPie, 0, 0, 0);
    }

    public void InitEventChangeInforLine()
    {
      this.cbProductions.SelectedIndexChanged += cbProductions_SelectedIndexChanged;
      this.cbShiftLeader.SelectedIndexChanged += cbShiftLeader_SelectedIndexChanged;
      this.cbShiftTypes.SelectedIndexChanged += cbShiftTypes_SelectedIndexChanged;
    }

    private void picIconWeight_Click(object sender, EventArgs e)
    {
      OnSendChooseLineWeight?.Invoke(this._inforLine);
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

      _ShiftLeaderCurrent = this._ShiftLeaders?.FirstOrDefault(x => x.Id == this._inforLine.ShiftLeaderId);
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

      _ShiftTypeCurrent = this._ShiftTypes?.FirstOrDefault(x => x.Id == this._inforLine.ShiftTypesId);
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

      this.lbDensity.Text = $"{production.Density}";

      this.lbPacksize.Text = $"{production.PackSize}";

      this.lbUpper.Text = $"{production.UpperLimitFinal}";
      this.lbTarget.Text = $"{production.StandardFinal}";
      this.lbLower.Text = $"{production.LowerLimitFinal}";

      if (this._inforLine.eModeTare == eModeTare.TareWithLabel)
      {
        this.lbTareUpper.Text = $"{production.Tare_with_label_upperlimit}";
        this.lbTareTarget.Text = $"{production.Tare_with_label_standard}";
        this.lbTareLower.Text = $"{production.Tare_with_label_lowerlimit}";
      }
      else
      {
        this.lbTareUpper.Text = $"{production.Tare_no_label_upperlimit}";
        this.lbTareTarget.Text = $"{production.Tare_no_label_standard}";
        this.lbTareLower.Text = $"{production.Tare_no_label_lowerlimit}";
      }
    }

    public void SetInforTare()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforTare();
        }));
        return;
      }

      this.lbTypeTare.Text = eNumHelper.GetDescription(this._inforLine.eModeTare);

      if (_inforLine.ProductionCurrent != null)
      {
        if (this._inforLine.eModeTare == eModeTare.TareWithLabel)
        {
          this.lbTareUpper.Text = $"{_inforLine.ProductionCurrent.Tare_with_label_upperlimit}";
          this.lbTareTarget.Text = $"{_inforLine.ProductionCurrent.Tare_with_label_standard}";
          this.lbTareLower.Text = $"{_inforLine.ProductionCurrent.Tare_with_label_lowerlimit}";
        }
        else
        {
          this.lbTareUpper.Text = $"{_inforLine.ProductionCurrent.Tare_no_label_upperlimit}";
          this.lbTareTarget.Text = $"{_inforLine.ProductionCurrent.Tare_no_label_standard}";
          this.lbTareLower.Text = $"{_inforLine.ProductionCurrent.Tare_no_label_lowerlimit}";
        }
      }
      else
      {
        this.lbTareUpper.Text = $"---";
        this.lbTareTarget.Text = $"---";
        this.lbTareLower.Text = $"---";
      }

    }

    public void SetStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetStatusConnectWeight(eStatusConnectWeight);
        }));
        return;
      }

      this.lbStatusWeight.Text = eNumHelper.GetDescription(eStatusConnectWeight);

      if (eStatusConnectWeight == eStatusConnectWeight.Connected)
      {
        this.lbStatusWeight.ForeColor = Color.FromArgb(0, 192, 0);
      }
      else if (eStatusConnectWeight == eStatusConnectWeight.Disconnnect)
      {
        this.lbStatusWeight.ForeColor = Color.Tomato;
      }
      else
      {
        this.lbStatusWeight.ForeColor = Color.Silver;
      }

      if (this._inforLine!=null)
        this._inforLine.eStatusConnectWeight = eStatusConnectWeight;
    }

    public string Title
    {
      set
      {
        this.lbTitle.Text = value;
      }
    }

    public DatalogTare UpdateTareCurrent
    {
      set
      {
        this._inforLine.DatalogTareCurrent = value;
      }
    }

    public eModeTare TypeTare
    {
      set
      {
        this.lbTypeTare.Text = eNumHelper.GetDescription(value); ;
      }
    }

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
        this._ShiftTypes = value;
        this.cbShiftTypes.DataSource = value;
        this.cbShiftTypes.DisplayMember = "Name";
      }
    }

    public List<ShiftLeader> ShiftLeader
    {
      set
      {
        this._ShiftLeaders = value;
        this.cbShiftLeader.DataSource = value;
        this.cbShiftLeader.DisplayMember = "UserName";
      }
    }

    private void cbProductions_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.cbProductions.SelectedIndexChanged -= cbProductions_SelectedIndexChanged;

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
        this.cbProductions.SelectedIndexChanged += cbProductions_SelectedIndexChanged;
      }
    }

    private void FrmInformation_OnSendCancelClicked(object sender)
    {
      cbProductions.SelectedItem = _inforLine.ProductionCurrent;
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

        //Gửi sự kiện cập nhật
        OnSendChooseProduct?.Invoke(_inforLine);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi sản phẩm thất bại !", eMsgType.Error);
      }
    }


    private async void cbShiftLeader_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        var data_choose = cbShiftLeader.SelectedItem as ShiftLeader;
        if (data_choose != null)
        {
          this._inforLine.ShiftLeaderId = data_choose.Id;
          await AppCore.Ins.Update(this._inforLine);
          OnSendChangeShiftLeader?.Invoke(this._inforLine);
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi trưởng ca thất bại !", eMsgType.Error);
      }
    }
    private async void cbShiftTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        var data_choose = cbShiftTypes.SelectedItem as ShiftType;
        if (data_choose != null)
        {
          this._inforLine.ShiftTypesId = data_choose.Id;
          await AppCore.Ins.Update(this._inforLine);
          OnSendChangeShiftType?.Invoke(this._inforLine);
        }
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi loại ca thất bại !", eMsgType.Error);
      }
    }


    public void SetSumary(List<DatalogWeight> datalogWeights)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetSumary(datalogWeights);
        }));
        return;
      }

      var rs_sumary = AppCore.Ins.SumaryDTO(datalogWeights);
      foreach (var sumary in rs_sumary)
      {
        SetValueSumary(sumary);
      }

      //Chart
      int total = rs_sumary.Sum(x => x.TotalSample);
      int lower = rs_sumary.Sum(x => x.NumberSampleLower);
      int over = rs_sumary.Sum(x => x.NumberSampleOver);
      int pass = total - lower - over;
      ChartHelper.SetDataChartPie(chartPie, pass, lower, over);
    }

    public void SetSumary(List<StatisticalData> statisticalDatas)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetSumary(statisticalDatas);
        }));
        return;
      }

      foreach (var sumary in statisticalDatas)
      {
        SetValueSumary(sumary);
      }

      //Chart
      int total = statisticalDatas.Sum(x => x.TotalSample);
      int lower = statisticalDatas.Sum(x => x.NumberSampleLower);
      int over = statisticalDatas.Sum(x => x.NumberSampleOver);
      int pass = total - lower - over;
      ChartHelper.SetDataChartPie(chartPie, pass, lower, over);
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
        switch (statisticalData.Index)
        {
          case 1:
            this.lbShift_1.Text = statisticalData.Shift;
            this.lbStdev_1.Text = statisticalData.Stdev.ToString();
            this.lbAverage_1.Text = statisticalData.Average.ToString();
            this.lbTotalSample_1.Text = statisticalData.TotalSample.ToString();
            this.lbResult_1.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);

            if (statisticalData.eEvaluate == eEvaluate.Pass)
            {
              this.lbResult_1.ForeColor = Color.Green;
            }
            else
            {
              this.lbResult_1.ForeColor = Color.Tomato;
            }
            break;
          case 2:
            this.lbShift_2.Text = statisticalData.Shift;
            this.lbStdev_2.Text = statisticalData.Stdev.ToString();
            this.lbAverage_2.Text = statisticalData.Average.ToString();
            this.lbTotalSample_2.Text = statisticalData.TotalSample.ToString();
            this.lbResult_2.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);

            if (statisticalData.eEvaluate == eEvaluate.Pass)
            {
              this.lbResult_2.ForeColor = Color.Green;
            }
            else
            {
              this.lbResult_2.ForeColor = Color.Tomato;
            }
            break;
          case 3:
            this.lbShift_3.Text = statisticalData.Shift;
            this.lbStdev_3.Text = statisticalData.Stdev.ToString();
            this.lbAverage_3.Text = statisticalData.Average.ToString();
            this.lbTotalSample_3.Text = statisticalData.TotalSample.ToString();
            this.lbResult_3.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);

            if (statisticalData.eEvaluate == eEvaluate.Pass)
            {
              this.lbResult_3.ForeColor = Color.Green;
            }
            else
            {
              this.lbResult_3.ForeColor = Color.Tomato;
            }
            break;
        }
      }
    }


    public void SelectProduct()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SelectProduct();
        }));
        return;
      }

      if (_inforLine.ProductionCurrent != null)
      {
        this.cbProductions.SelectedIndexChanged -= cbProductions_SelectedIndexChanged;

        this.cbProductions.SelectedItem = _inforLine.ProductionCurrent;
        SetInforProduct(_inforLine.ProductionCurrent);

        this.cbProductions.SelectedIndexChanged += cbProductions_SelectedIndexChanged;
      }
    }

    public void SelectTypeShift()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SelectTypeShift();
        }));
        return;
      }

      if (_inforLine.ShiftTypesId > 0)
      {
        this.cbShiftTypes.SelectedIndexChanged -= cbShiftTypes_SelectedIndexChanged;

        var data = cbShiftTypes.DataSource as List<ShiftType>;
        var data_rs = data?.FirstOrDefault(x => x.Id == _inforLine.ShiftTypesId);

        this.cbShiftTypes.SelectedItem = data_rs;

        this.cbShiftTypes.SelectedIndexChanged += cbShiftTypes_SelectedIndexChanged;
      }
    }

    public void SelectShiftLeader()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SelectShiftLeader();
        }));
        return;
      }

      if (_inforLine.ShiftLeaderId > 0)
      {
        this.cbShiftLeader.SelectedIndexChanged -= cbShiftLeader_SelectedIndexChanged;

        var data = cbShiftLeader.DataSource as List<ShiftLeader>;
        var data_rs = data?.FirstOrDefault(x => x.Id == _inforLine.ShiftLeaderId);

        this.cbShiftLeader.SelectedItem = data_rs;

        this.cbShiftLeader.SelectedIndexChanged += cbShiftLeader_SelectedIndexChanged;
      }
    }

  }
}
