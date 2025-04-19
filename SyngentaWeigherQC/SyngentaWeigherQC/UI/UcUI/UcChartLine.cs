using SyngentaWeigherQC.Models;
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
  public partial class UcChartLine : UserControl
  {
    public UcChartLine()
    {
      InitializeComponent();
      ChartSMCInit();
    }

    public void ChartSMCInit()
    {
      chart1.Series[0].Color = Color.FromArgb(166, 77, 121);
      chart1.Series[1].Color = Color.FromArgb(38,255,0);
      chart1.Series[2].Color = Color.DarkOrange;
      chart1.Series[3].Color = Color.Red;
      chart1.Series[4].Color = Color.Blue;


      chart1.Series[0].BorderWidth = 2;
      chart1.Series[1].BorderWidth = 2;
      chart1.Series[2].BorderWidth = 2;
      chart1.Series[3].BorderWidth = 2;
      chart1.Series[4].BorderWidth = 2;

      //chart1.ChartAreas[0].AxisX.Title = "Số lần lấy mẫu";
      //chart1.ChartAreas[0].AxisY.Title = "Value";
    }

   
    public void SetDataChart(ChartLineData chartLineData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart(chartLineData);
        }));
        return;
      }

      chart1.Series[0].Points.Clear();
      chart1.Series[1].Points.Clear();
      chart1.Series[2].Points.Clear();
      chart1.Series[3].Points.Clear();
      chart1.Series[4].Points.Clear();

      if (chartLineData == null) return;
      if (chartLineData.AverageRaw == null) return;
      if (chartLineData.AverageRaw.Count == 0) return;

      int i = 0;
      foreach (var item in chartLineData.AverageRaw)
      {
        AddDataChart(++i, chartLineData.Average, chartLineData.Target, chartLineData.Max, chartLineData.Min, item);
      }

      double min = chartLineData.AverageRaw.Min();
      double max = chartLineData.AverageRaw.Max();

      min = Math.Min(min, chartLineData.Min);
      max = Math.Max(max, chartLineData.Max);

      chart1.ChartAreas[0].AxisY.Minimum = Math.Round(min * 0.99, 1);
      chart1.ChartAreas[0].AxisY.Maximum = Math.Round(max * 1.01, 1);
    }

    public void AddDataChart(int sttSampleBlock,double averageAll, double standard, double max, double min, double average)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          AddDataChart(sttSampleBlock, averageAll, standard, max, min, average);
        }));
        return;
      }

      try
      {
        chart1.Series[0].Points.AddXY(sttSampleBlock, averageAll);
        chart1.Series[1].Points.AddXY(sttSampleBlock, standard);
        chart1.Series[2].Points.AddXY(sttSampleBlock, max);
        chart1.Series[3].Points.AddXY(sttSampleBlock, min);
        chart1.Series[4].Points.AddXY(sttSampleBlock, average);
        chart1.Invalidate();
      }
      catch (Exception)
      {
      }

    }



  }
}
