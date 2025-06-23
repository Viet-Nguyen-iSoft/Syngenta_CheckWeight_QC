using DocumentFormat.OpenXml.Drawing.Charts;
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
  public partial class UcChartPie : UserControl
  {
    public UcChartPie()
    {
      InitializeComponent();
    }


    public void SetDataChart(int total,int lower, int upper)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart(total, lower, upper);
        }));
        return;
      }

      chart1.Series[0].Points.Clear();
      if (total == 0) return;

      double rateError = (total != 0) ? (((double)lower * 100) / total) : 0;
      double rateOver = (total != 0) ? (((double)upper * 100) / total) : 0;

      rateError = Math.Round(rateError, 2);
      rateOver = Math.Round(rateOver, 2);

      chart1.Series[0].Points.AddXY($"", rateError);
      chart1.Series[0].Points.AddXY($"", rateOver);
      chart1.Series[0].Points.AddXY($"", 100 - rateError - rateOver);

      chart1.Series[0].Points[0].Color = Color.Red;
      chart1.Series[0].Points[1].Color = Color.Orange;
      chart1.Series[0].Points[2].Color = Color.Green;

      chart1.Series[0].Points[0].LegendText = $"% Mẫu lỗi: {rateError}%";
      chart1.Series[0].Points[1].LegendText = $"% Mẫu cao: {rateOver}%";
      chart1.Series[0].Points[2].LegendText = $"% Mẫu tốt: {100 - rateError - rateOver}%";
      chart1.Legends[0].Font = new Font("Arial", 14);

    }

  }
}
