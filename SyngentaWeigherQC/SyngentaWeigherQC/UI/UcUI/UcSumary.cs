using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcSumary : UserControl
  {
    public UcSumary()
    {
      InitializeComponent();
    }

    public void SetData(int numberProduct, int numberSamples, int numberSampleError, int numberSampleValueOver)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetData(numberProduct, numberSamples, numberSampleError, numberSampleValueOver);
        }));
        return;
      }

      this.lbNumberProduct.Text = numberProduct.ToString();
      this.lbNumberSamples.Text = numberSamples.ToString();
      this.lbNumberError.Text = numberSampleError.ToString();
      this.lbNumberSampleValueOver.Text = numberSampleValueOver.ToString();

      double rateError = (numberSamples != 0) ? (((double)numberSampleError * 100) / numberSamples) : 0;
      double rateOver = (numberSamples != 0) ? (((double)numberSampleValueOver * 100) / numberSamples) : 0;

      rateError = Math.Round(rateError, 2);
      rateOver = Math.Round(rateOver, 2);

      this.lbNumberErrorRate.Text = rateError.ToString() + " %";
      this.lbNumberSampleValueOverRate.Text = rateOver.ToString() + " %";
    }


  }
}
