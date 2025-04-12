using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcTemplateExcel : UserControl
  {
    public UcTemplateExcel()
    {
      InitializeComponent();
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

      if (excelClassInforProduct==null)
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



    public void SetResultFinal(bool isPass)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetResultFinal(isPass);
        }));
        return;
      }

      if (isPass)
      {
        this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH ĐẠT";
        this.lblPasFail.BackColor = Color.Green;
      }
      else
      {
        this.lblPasFail.Text = "TRỌNG LƯỢNG TRUNG BÌNH CHƯA ĐẠT";
        this.lblPasFail.BackColor = Color.Red;
      }
    }

    public void UpdateSynthetic(StatisticalData statisticalData, int id)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateSynthetic(statisticalData, id);
        }));
        return;
      }

      if (statisticalData == null) return;

      if (id == 0)
      {
        this.lblShift1.Text = AppCore.Ins._listShift?.Where(x => x.CodeShift == statisticalData.Shift).FirstOrDefault().Name;
        this.lblShift1_Stdev.Text = statisticalData.Stdev.ToString();
        this.lblShift1_TB.Text = statisticalData.Average.ToString();
        this.lblShift1_Standard.Text = statisticalData.Target.ToString();
        this.lblShift1_PassFail.Text = statisticalData.Result;
        this.lblShift1_TotalSamples.Text = statisticalData.TotalSample.ToString();
        this.lblShift1_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
        this.lblShift1_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
        this.lblShift1_PercentFail.Text = statisticalData.RateError.ToString();
        this.lblShift1_Loss.Text = statisticalData.RateLoss.ToString();
        this.lblShift1_PassFail.BackColor = (statisticalData.Result == "ĐẠT") ? Color.Green : Color.Red;
      }
      else if (id == 1)
      {
        this.lblShift2.Text = AppCore.Ins._listShift?.Where(x => x.CodeShift == statisticalData.Shift).FirstOrDefault().Name; ;
        this.lblShift2_Stdev.Text = statisticalData.Stdev.ToString();
        this.lblShift2_TB.Text = statisticalData.Average.ToString();
        this.lblShift2_Standard.Text = statisticalData.Target.ToString();
        this.lblShift2_PassFail.Text = statisticalData.Result;
        this.lblShift2_TotalSamples.Text = statisticalData.TotalSample.ToString();
        this.lblShift2_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
        this.lblShift2_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
        this.lblShift2_PercentFail.Text = statisticalData.RateError.ToString();
        this.lblShift2_Loss.Text = statisticalData.RateLoss.ToString();
        this.lblShift2_PassFail.BackColor = (statisticalData.Result == "ĐẠT") ? Color.Green : Color.Red;
      }
      else if (id==2)
      {
        this.lblShift3.Text = AppCore.Ins._listShift?.Where(x => x.CodeShift == statisticalData.Shift).FirstOrDefault().Name;
        this.lblShift3_Stdev.Text = statisticalData.Stdev.ToString();
        this.lblShift3_TB.Text = statisticalData.Average.ToString();
        this.lblShift3_Standard.Text = statisticalData.Target.ToString();
        this.lblShift3_PassFail.Text = statisticalData.Result;
        this.lblShift3_TotalSamples.Text = statisticalData.TotalSample.ToString();
        this.lblShift3_TotalLowerLimit.Text = statisticalData.NumberSampleLower.ToString();
        this.lblShift3_TotalUpperLimit.Text = statisticalData.NumberSampleOver.ToString();
        this.lblShift3_PercentFail.Text = statisticalData.RateError.ToString();
        this.lblShift3_Loss.Text = statisticalData.RateLoss.ToString();
        this.lblShift3_PassFail.BackColor = (statisticalData.Result == "ĐẠT") ? Color.Green : Color.Red;
      }
    }

    public void UpdateSynthetic(List<StatisticalData> statisticalData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateSynthetic(statisticalData);
        }));
        return;
      }

      if (statisticalData == null) return;

      if (statisticalData.Count<=0) return;

      foreach (var item in statisticalData)
      {
        if (item.Shift==1 || item.Shift == 4 || item.Shift == 6)
        {
          UpdateSynthetic(item, 0);
        }
        else if (item.Shift == 2 || item.Shift == 5)
        {
          UpdateSynthetic(item, 1);
        }
        else if (item.Shift == 3)
        {
          UpdateSynthetic(item, 2);
        }
      }

      
    }


    public void ClearStatistical()
    {
      this.lblShift1.Text = "";
      this.lblShift1_Stdev.Text = "";
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
