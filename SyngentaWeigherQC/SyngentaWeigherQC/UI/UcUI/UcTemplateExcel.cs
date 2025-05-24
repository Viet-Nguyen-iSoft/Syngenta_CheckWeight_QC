using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcTemplateExcel : UserControl
  {
    public UcTemplateExcel()
    {
      InitializeComponent();
    }

    public void SetValueSumary(StatisticalData statisticalData)
    {
      switch (statisticalData.Index)
      {
        case 1:
          this.lblShift1.Text = statisticalData.Shift;
          this.lblShift1_Stdev.Text = statisticalData.Stdev.ToString();
          this.lbCpkShift1.Text = statisticalData.Cpk.ToString();
          this.lbSigmaShift1.Text = statisticalData.Sigma.ToString();
          this.lblShift1_TB.Text = statisticalData.Average.ToString();
          this.lblShift1_Standard.Text = statisticalData.Target.ToString();
          this.lblShift1_PassFail.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);
          this.lblShift1_TotalSamples.Text = statisticalData.TotalSample.ToString();
          this.lblShift1_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
          this.lblShift1_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
          this.lblShift1_PercentFail.Text = statisticalData.RateError.ToString() + "%";
          this.lblShift1_Loss.Text = statisticalData.RateLoss.ToString() + "%";

          if (statisticalData.eEvaluate == eEvaluate.Pass)
          {
            this.lblShift1_PassFail.BackColor = Color.Green;
            this.lblShift1_PassFail.ForeColor = Color.White;
          }
          else
          {
            this.lblShift1_PassFail.BackColor = Color.Red;
            this.lblShift1_PassFail.ForeColor = Color.White;
          }
          break;
        case 2:
          this.lblShift2.Text = statisticalData.Shift;
          this.lblShift2_Stdev.Text = statisticalData.Stdev.ToString();
          this.lbCpkShift2.Text = statisticalData.Cpk.ToString();
          this.lbSigmaShift2.Text = statisticalData.Sigma.ToString();
          this.lblShift2_TB.Text = statisticalData.Average.ToString();
          this.lblShift2_Standard.Text = statisticalData.Target.ToString();
          this.lblShift2_PassFail.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);
          this.lblShift2_TotalSamples.Text = statisticalData.TotalSample.ToString();
          this.lblShift2_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
          this.lblShift2_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
          this.lblShift2_PercentFail.Text = statisticalData.RateError.ToString() + "%";
          this.lblShift2_Loss.Text = statisticalData.RateLoss.ToString() + "%";

          if (statisticalData.eEvaluate == eEvaluate.Pass)
          {
            this.lblShift2_PassFail.BackColor = Color.Green;
            this.lblShift2_PassFail.ForeColor = Color.White;
          }
          else
          {
            this.lblShift2_PassFail.BackColor = Color.Red;
            this.lblShift2_PassFail.ForeColor = Color.White;
          }
          break;
        case 3:
          this.lblShift3.Text = statisticalData.Shift;
          this.lblShift3_Stdev.Text = statisticalData.Stdev.ToString();
          this.lbCpkShift3.Text = statisticalData.Cpk.ToString();
          this.lbSigmaShift3.Text = statisticalData.Sigma.ToString();
          this.lblShift3_TB.Text = statisticalData.Average.ToString();
          this.lblShift3_Standard.Text = statisticalData.Target.ToString();
          this.lblShift3_PassFail.Text = eNumHelper.GetDescription(statisticalData.eEvaluate);
          this.lblShift3_TotalSamples.Text = statisticalData.TotalSample.ToString();
          this.lblShift3_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
          this.lblShift3_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
          this.lblShift3_PercentFail.Text = statisticalData.RateError.ToString() + "%";
          this.lblShift3_Loss.Text = statisticalData.RateLoss.ToString() + "%";

          if (statisticalData.eEvaluate == eEvaluate.Pass)
          {
            this.lblShift3_PassFail.BackColor = Color.Green;
            this.lblShift3_PassFail.ForeColor = Color.White;
          }
          else
          {
            this.lblShift3_PassFail.BackColor = Color.Red;
            this.lblShift3_PassFail.ForeColor = Color.White;
          }
          break;
      }
    }


    public void UpdateInforProductUI(ExcelClassInforProduct excelClassInforProduct)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateInforProductUI(excelClassInforProduct);
        }));
        return;
      }

      if (excelClassInforProduct == null)
      {
        this.lblStationName.Text = "";
        this.lblTareWithLabel.Text = "";
        this.lblCurrentProduct.Text = "";
        this.lblPacksize.Text = "";
        this.lblStandard.Text = "";
        this.lblUpperLimit.Text = "";
        this.lblLowerLimit.Text = "";
        this.lblToTruongChuyen.Text = "";
      }
      else
      {
        this.lblStationName.Text = excelClassInforProduct.NameLine;
        this.lblTareWithLabel.Text = excelClassInforProduct.ModeTare;
        this.lblCurrentProduct.Text = excelClassInforProduct.ProductName;
        this.lblPacksize.Text = excelClassInforProduct.PackSize.ToString();
        this.lblStandard.Text = excelClassInforProduct.Standard.ToString();
        this.lblUpperLimit.Text = excelClassInforProduct.Upper.ToString();
        this.lblLowerLimit.Text = excelClassInforProduct.Lower.ToString();
        this.lblToTruongChuyen.Text = excelClassInforProduct.NameShiftLeader;
      }
    }



    public void SetResultFinal(eEvaluate eEvaluate)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetResultFinal(eEvaluate);
        }));
        return;
      }

      switch (eEvaluate)
      {
        case eEvaluate.None:
          this.lblPasFail.Text = "CHƯA CÓ ĐÁNH GIÁ";
          this.lblPasFail.BackColor = Color.Gray;
          break;
        case eEvaluate.Pass:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH ĐẠT";
          this.lblPasFail.BackColor = Color.Green;
          break;
        case eEvaluate.Fail:
          this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH CHƯA ĐẠT";
          this.lblPasFail.BackColor = Color.Red;
          break;
      }
    }


    public void ClearSumary()
    {
      this.lblShift1.Text = "";
      this.lblShift1_Stdev.Text = "";
      this.lbCpkShift1.Text = "";
      this.lbSigmaShift1.Text = "";
      this.lblShift1_TB.Text = "";
      this.lblShift1_Standard.Text = "";
      this.lblShift1_PassFail.Text = "";
      this.lblShift1_TotalSamples.Text = "";
      this.lblShift1_TotalLowerLimit.Text = "";
      this.lblShift1_TotalUpperLimit.Text = "";
      this.lblShift1_PercentFail.Text = "";
      this.lblShift1_Loss.Text = "";
      this.lblShift1_PassFail.BackColor = Color.White;


      this.lblShift2.Text = "";
      this.lblShift2_Stdev.Text = "";
      this.lbCpkShift2.Text = "";
      this.lbSigmaShift2.Text = "";
      this.lblShift2_TB.Text = "";
      this.lblShift2_Standard.Text = "";
      this.lblShift2_PassFail.Text = "";
      this.lblShift2_TotalSamples.Text = "";
      this.lblShift2_TotalLowerLimit.Text = "";
      this.lblShift2_TotalUpperLimit.Text = "";
      this.lblShift2_PercentFail.Text = "";
      this.lblShift2_Loss.Text = "";
      this.lblShift2_PassFail.BackColor = Color.White;

      this.lblShift3.Text = "";
      this.lblShift3_Stdev.Text = "";
      this.lbCpkShift3.Text = "";
      this.lbSigmaShift3.Text = "";
      this.lblShift3_TB.Text = "";
      this.lblShift3_Standard.Text = "";
      this.lblShift3_PassFail.Text = "";
      this.lblShift3_TotalSamples.Text = "";
      this.lblShift3_TotalLowerLimit.Text = "";
      this.lblShift3_TotalUpperLimit.Text = "";
      this.lblShift3_PercentFail.Text = "";
      this.lblShift3_Loss.Text = "";
      this.lblShift3_PassFail.BackColor = Color.White;
    }

  }
}
