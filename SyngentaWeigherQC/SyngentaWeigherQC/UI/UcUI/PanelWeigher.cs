﻿using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
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

      if (production!=null)
      {
        //Sản phẩm
        this.ucProductionDataInforPacksize.SetValue = production.PackSize.ToString();
        this.ucProductionDataInforTarget.SetValue = production.StandardFinal.ToString();
        this.ucProductionDataInforMax.SetValue = production.UpperLimitFinal.ToString();
        this.ucProductionDataInforMin.SetValue = production.LowerLimitFinal.ToString();

        //Tare
        if(eModeTare == eModeTare.TareWithLabel)
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_with_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_with_label_lowerlimit}";
          this.lblTareStandard.Text = $" : {production.Tare_with_label_standard}";
        }
        else
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_no_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_no_label_lowerlimit}";
          this.lblTareStandard.Text = $" : {production.Tare_no_label_standard}";
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
        this.lblTareStandard.Text = $"---";
      }  
    }

    public void SetInforProduction(Production production, eModeTare eModeTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInforProduction(production, eModeTare);
        }));
        return;
      }

      this.btTareWithLabel.Text = "Kiểu Tare: " + eNumHelper.GetDescription(eModeTare);

      if (production != null)
      {
        if (eModeTare == eModeTare.TareWithLabel)
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_with_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_with_label_lowerlimit}";
          this.lblTareStandard.Text = $" : {production.Tare_with_label_standard}";

        }
        else
        {
          this.lblTareUpperLimit.Text = $" : {production.Tare_no_label_upperlimit}";
          this.lblTareLowerLimit.Text = $" : {production.Tare_no_label_lowerlimit}";
          this.lblTareStandard.Text = $" : {production.Tare_no_label_standard}";
        }
      }
      else
      {
        this.lblTareUpperLimit.Text = $" : ---";
        this.lblTareLowerLimit.Text = $" : ---";
        this.lblTareStandard.Text = $" : ---";
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

    public void SetValueTare(DatalogTare datalogTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetValueTare(datalogTare);
        }));
        return;
      }

      if (datalogTare!=null)
      {
        this.lblTareAvg.Text = datalogTare.Value.ToString();
        this.lblTareLastUpdated.Text = $"Last updated: {((DateTime)(datalogTare.CreatedAt)).ToString("yyyy/MM/dd HH:mm:ss")}";
      }  
      else
      {
        this.lblTareAvg.Text = "---";
        this.lblTareLastUpdated.Text = $"Last updated: ---";
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
