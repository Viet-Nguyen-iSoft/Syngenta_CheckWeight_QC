using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Production = SyngentaWeigherQC.Models.Production;


namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcOverViewMachine : UserControl
  {
    //Sự kiện
    public delegate void SendChangeProduct(object sender);
    public event SendChangeProduct OnSendChangeProduct;

    public delegate void SendChangeShiftLeader(object sender);
    public event SendChangeShiftLeader OnSendChangeShiftLeader;

    public delegate void SendChangeShiftType(object sender);
    public event SendChangeShiftType OnSendChangeShiftType;

    public delegate void SendChooseLineWeight(InforLine inforLine);
    public event SendChooseLineWeight OnSendChooseLineWeight;

    public UcOverViewMachine()
    {
      InitializeComponent();
    }

    private InforLine _inforLine;

    private ShiftLeader _ShiftLeaderCurrent = new ShiftLeader();
    private ShiftType _ShiftTypeCurrent = new ShiftType();
    private List<ShiftLeader> _ShiftLeaders = new List<ShiftLeader>();
    private List<ShiftType> _ShiftTypes = new List<ShiftType>();
    public UcOverViewMachine(InforLine line) : this()
    {
      _inforLine = line;

      //Ten line
      Title = this._inforLine.Name;

      //Loại Tare
      TypeTare = this._inforLine.eModeTare;

      //Check Chọn Product
      var listProducts = this._inforLine.Productions.ToList();
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
      else if (this._inforLine.eModeTare == eModeTare.TareWithLabel)
      {
        this.lbTareUpper.Text = $"{production.Tare_no_label_upperlimit}";
        this.lbTareTarget.Text = $"{production.Tare_no_label_standard}";
        this.lbTareLower.Text = $"{production.Tare_no_label_lowerlimit}";
      }
      else
      {
        this.lbTareUpper.Text = $"{production.Tare_with_label_upperlimit}";
        this.lbTareTarget.Text = $"{production.Tare_with_label_standard}";
        this.lbTareLower.Text = $"{production.Tare_with_label_lowerlimit}";
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


    public void InitEvent()
    {
      this.cbProductions.SelectedIndexChanged += cbProductions_SelectedIndexChanged;
      this.cbShiftLeader.SelectedIndexChanged += cbShiftLeader_SelectedIndexChanged;
      this.cbShiftTypes.SelectedIndexChanged += cbShiftTypes_SelectedIndexChanged;
    }

    public void DeInitEvent()
    {
      this.cbProductions.SelectedIndexChanged -= cbProductions_SelectedIndexChanged;
      this.cbShiftLeader.SelectedIndexChanged -= cbShiftLeader_SelectedIndexChanged;
      this.cbShiftTypes.SelectedIndexChanged -= cbShiftTypes_SelectedIndexChanged;
    }


    private void cbProductions_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        DeInitEvent();

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
        eLoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi sản phẩm thất bại !", eMsgType.Error);
      }
      finally
      {
        InitEvent();
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
        OnSendChangeProduct?.Invoke(this._inforLine);
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
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
          this._inforLine.ShiftLeader = data_choose;
          await AppCore.Ins.Update(this._inforLine);

          OnSendChangeShiftLeader?.Invoke(data_choose);
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
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
          this._inforLine.ShiftTypes = data_choose;
          await AppCore.Ins.Update(this._inforLine);

          OnSendChangeShiftType?.Invoke(data_choose);
        }
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Thay đổi loại ca thất bại !", eMsgType.Error);
      }
    }


  }
}
