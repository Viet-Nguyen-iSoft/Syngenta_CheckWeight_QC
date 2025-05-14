using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class PanelWeigher : UserControl
  {
    public PanelWeigher()
    {
      InitializeComponent();

      this.ucProductionDataInforPacksize.SetTitle = "PackSize";
      this.ucProductionDataInforTarget.SetTitle = "Chuẩn";
      this.ucProductionDataInforMax.SetTitle = "Cận trên";
      this.ucProductionDataInforMin.SetTitle = "Cận dưới";
    }

    public void SetInforProduct(Production production, eModeTare eModeTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforProduct(production, eModeTare);
        }));
        return;
      }

      if (production != null)
      {
        //Sản phẩm
        this.ucProductionDataInforPacksize.SetValue = production.PackSize.ToString();
        this.ucProductionDataInforTarget.SetValue = production.StandardFinal.ToString();
        this.ucProductionDataInforMax.SetValue = production.UpperLimitFinal.ToString();
        this.ucProductionDataInforMin.SetValue = production.LowerLimitFinal.ToString();

        //Tare
        if (eModeTare == eModeTare.TareWithLabel)
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_with_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_with_label_lowerlimit}";
          this.lblTareTarget.Text = $"{production.Tare_with_label_lowerlimit}";
        }
        else
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_no_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_no_label_lowerlimit}";
          this.lblTareTarget.Text = $"{production.Tare_no_label_standard}";
        }
      }
      else
      {
        //Sản phẩm
        this.ucProductionDataInforPacksize.SetValue = "---";
        this.ucProductionDataInforTarget.SetValue = "---";
        this.ucProductionDataInforMax.SetValue = "---";
        this.ucProductionDataInforMin.SetValue = "---";

        //Tare
        this.lblTareUpperLimit.Text = $"---";
        this.lblTareLowerLimit.Text = $"---";
      }
    }

    public void SetInforTare(InforLine inforLine)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforTare(inforLine);
        }));
        return;
      }

      if (inforLine.ProductionCurrent != null)
      {
        if (inforLine.eModeTare == eModeTare.TareWithLabel)
        {
          this.lblTareUpperLimit.Text = $" : {inforLine.ProductionCurrent.Tare_with_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {inforLine.ProductionCurrent.Tare_with_label_lowerlimit}";
          this.lblTareTarget.Text = $"{inforLine.ProductionCurrent.Tare_with_label_standard}";
        }
        else
        {
          this.lblTareUpperLimit.Text = $" : {inforLine.ProductionCurrent.Tare_no_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {inforLine.ProductionCurrent.Tare_no_label_lowerlimit}";
          this.lblTareTarget.Text = $"{inforLine.ProductionCurrent.Tare_no_label_standard}";
        }
      }
      else
      {
        //Sản phẩm
        this.ucProductionDataInforPacksize.SetValue = "---";
        this.ucProductionDataInforTarget.SetValue = "---";
        this.ucProductionDataInforMax.SetValue = "---";
        this.ucProductionDataInforMin.SetValue = "---";

        //Tare
        this.lblTareUpperLimit.Text = $"---";
        this.lblTareLowerLimit.Text = $"---";
      }
    }

    public void SetValueWeigherRealTime(double value, eEvaluateStatus eEvaluateStatus)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetValueWeigherRealTime(value, eEvaluateStatus);
        }));
        return;
      }

      lblPasFail.Visible = ((value >= 10));
      this.lblWeigherData.Text = value.ToString();

      switch (eEvaluateStatus)
      {
        case eEvaluateStatus.UNKNOWN:
          this.lblPasFail.Text = "CHƯA CHỌN SẢN PHẨM";
          this.lblPasFail.BackColor = Color.Orange;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eEvaluateStatus.FAIL:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH KHÔNG ĐẠT";
          this.lblPasFail.BackColor = Color.Red;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eEvaluateStatus.PASS:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH ĐẠT";
          this.lblPasFail.BackColor = Color.Green;
          this.lblPasFail.ForeColor = Color.White;
          break;
        case eEvaluateStatus.OVER:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH CAO";
          this.lblPasFail.BackColor = Color.Yellow;
          this.lblPasFail.ForeColor = Color.Black;
          break;
      }
    }

    public void SetSatutusConnectSerialWeigher(eStatusConnectWeight eStatusConnectWeight)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetSatutusConnectSerialWeigher(eStatusConnectWeight);
        }));
        return;
      }

      this.lblWeigherStatus.Text = eNumHelper.GetDescription(eStatusConnectWeight);

      if (eStatusConnectWeight == eStatusConnectWeight.Connected)
      {
        this.lblWeigherStatus.BackColor = Color.FromArgb(0, 192, 0);
      }
      else if (eStatusConnectWeight == eStatusConnectWeight.Disconnnect)
      {
        this.lblWeigherStatus.BackColor = Color.Red;
      }
      else
      {
        this.lblWeigherStatus.BackColor = Color.Silver;
      }
    }


  }
}
