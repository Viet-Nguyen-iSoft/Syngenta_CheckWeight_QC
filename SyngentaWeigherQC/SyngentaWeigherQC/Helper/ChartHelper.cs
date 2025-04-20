using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SyngentaWeigherQC.Helper
{
  public class ChartHelper
  {
    public void ChartControlInit(Chart nameChart)
    {
      //nameChart.Series[0].Color = Color.Blue;
      //nameChart.Series[1].Color = Color.Red;
      //nameChart.Series[2].Color = Color.Orange;
      //nameChart.Series[3].Color = Color.Green;
      //nameChart.Series[4].Color = Color.Orange;
      //nameChart.Series[5].Color = Color.Red;

      nameChart.Series[0].Color = Color.FromArgb(51, 204, 255);
      //nameChart.Series[0].Color = Color.White;
      nameChart.Series[1].Color = Color.Red;
      nameChart.Series[2].Color = Color.FromArgb(255, 204, 51);
      nameChart.Series[3].Color = Color.FromArgb(0, 255, 51);
      nameChart.Series[4].Color = Color.FromArgb(255, 204, 51);
      nameChart.Series[5].Color = Color.Red;

      nameChart.Series[0].BorderWidth = 3;
      nameChart.Series[1].BorderWidth = 3;
      nameChart.Series[2].BorderWidth = 3;
      nameChart.Series[3].BorderWidth = 3;
      nameChart.Series[4].BorderWidth = 3;
      nameChart.Series[5].BorderWidth = 3;

      nameChart.ChartAreas[0].AxisX.Title = "Thời gian";
      nameChart.ChartAreas[0].AxisY.Title = "Giá trị cân (g)";
    }

    public void ChartHistogramInit(Chart nameChart)
    {
      //nameChart.Series[0].Color = Color.FromArgb(0, 205, 102);//51, 204, 255
      nameChart.Series[0].Color = Color.FromArgb(51, 204, 255);// Màu cột


      nameChart.Series[1].Color = Color.Red;
      nameChart.Series[2].Color = Color.FromArgb(255, 165, 0);
      nameChart.Series[3].Color = Color.FromArgb(0, 0, 255);
      nameChart.Series[4].Color = Color.FromArgb(255, 165, 0);
      nameChart.Series[5].Color = Color.Red;
      nameChart.Series[6].Color = Color.FromArgb(255, 51, 255);
      nameChart.Series[3].Color = Color.FromArgb(0, 0, 221);

      nameChart.Series[7].Color = Color.FromArgb(0, 205, 102);


      nameChart.Series[1].BorderWidth = 3;
      nameChart.Series[2].BorderWidth = 3;
      nameChart.Series[3].BorderWidth = 3;
      nameChart.Series[4].BorderWidth = 3;
      nameChart.Series[5].BorderWidth = 3;
      nameChart.Series[6].BorderWidth = 3;

      nameChart.Series[7].BorderWidth = 3;
    }

    public void ChartPieInit(Chart nameChart)
    {
      nameChart.Series[0].Points.AddXY($"No Data", 100);
      nameChart.Series[0].Points[0].Color = Color.Gray;
      nameChart.Series[0].Points[0].LabelForeColor = Color.White;
    }


    #region Chart Control
    public void AddChartControl(Chart chartName, List<double> dataY, List<string> dataX, double up2T, double up1T, double target, double lo1T, double lo2T, double max)
    {
      try
      {
        if (dataX == null || dataY == null || dataX.Count == 0 || dataY.Count == 0)
        {
          chartName.Series[0].Points.Clear();
          chartName.Series[1].Points.Clear();
          chartName.Series[2].Points.Clear();
          chartName.Series[3].Points.Clear();
          chartName.Series[4].Points.Clear();
          chartName.Series[5].Points.Clear();
          return;
        }

        chartName.ChartAreas[0].AxisY.Maximum = (max < up2T) ? up2T + 5 : up2T * 1.01;
        chartName.ChartAreas[0].AxisY.Minimum = lo2T - 5;

        chartName.Series[0].Points.Clear();
        chartName.Series[1].Points.Clear();
        chartName.Series[2].Points.Clear();
        chartName.Series[3].Points.Clear();
        chartName.Series[4].Points.Clear();
        chartName.Series[5].Points.Clear();

        for (int i = 0; i < dataX.Count(); i++)
        {
          int indexOfFirstSpace = dataX[i].IndexOf(' ');
          string timeOnly = dataX[i].Substring(indexOfFirstSpace + 1);

          //string timeOnly = DateTime.ParseExact(dataX[i], "yyyy/MM/dd HH:mm:ss", null).ToString("HH:mm:ss");
          //string dateString = dataX[i];

          // Chuyển đổi chuỗi thành đối tượng DateTime
          //DateTime dateTime = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
          //string timeOnly = dateTime.ToString("HH:mm:ss tt");

          //string timeOnly = dataX[i].Substring(11, 8);
          chartName.Series[0].Points.AddXY(timeOnly, dataY[i]);

          chartName.Series[1].Points.AddXY(dataX[i], up2T);
          chartName.Series[2].Points.AddXY(dataX[i], up1T);
          chartName.Series[3].Points.AddXY(dataX[i], target);
          chartName.Series[4].Points.AddXY(dataX[i], lo1T);
          chartName.Series[5].Points.AddXY(dataX[i], lo2T);
        }
        chartName.Invalidate();
      }
      catch (Exception)
      {
      }

    }

    public static void SetDataChartPie(Chart nameChart, double valueOK, double valueOver, double valueReject)
    {
      double total = valueOK + valueOver + valueReject;
      nameChart.Series[0].Points.Clear();
      try
      {
        if (valueOK == 0 && valueOver == 0 && valueReject == 0)
        {
          nameChart.Series[0].Points.AddXY($"No Data", 100);
          nameChart.Series[0].Points[0].Color = Color.Gray;
          nameChart.Series[0].Points[0].LabelForeColor = Color.White;
          return;
        }
        if (valueOK > 0)
        {
          nameChart.Series[0].Points.AddXY($"{Math.Round(valueOK * 100 / total, 2)} %", valueOK);
          //nameChart.Series[0].Points.AddXY($"", valueOK);
        }
        if (valueOver > 0)
        {
          nameChart.Series[0].Points.AddXY($"{Math.Round(valueOver * 100 / total, 2)} %", valueOver);
          //nameChart.Series[0].Points.AddXY($"", valueOver);
        }
        if (valueReject > 0)
        {
          nameChart.Series[0].Points.AddXY($"{Math.Round(valueReject * 100 / total, 2)} %", valueReject);
          //nameChart.Series[0].Points.AddXY($"", valueReject);
        }

      }
      catch (Exception)
      {
      }
      finally
      {
        if (valueOK > 0 && valueOver > 0 && valueReject > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(0, 192, 0); //Xanh
          nameChart.Series[0].Points[1].Color = Color.FromArgb(255, 128, 0); // Cam
          nameChart.Series[0].Points[2].Color = Color.Red; // ĐỎ
          nameChart.Series[0].Points[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[1].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[2].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
          nameChart.Series[0].Points[1].LabelForeColor = Color.Transparent;
          nameChart.Series[0].Points[2].LabelForeColor = Color.Transparent;
        }

        else if (valueOK > 0 && valueOver > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(0, 192, 0); //Xanh
          nameChart.Series[0].Points[1].Color = Color.FromArgb(255, 128, 0); // Cam

          nameChart.Series[0].Points[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[1].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
          nameChart.Series[0].Points[1].LabelForeColor = Color.Transparent;
        }
        else if (valueOK > 0 && valueReject > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(0, 192, 0); //Xanh
          nameChart.Series[0].Points[1].Color = Color.Red; // ĐỎ

          nameChart.Series[0].Points[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[1].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
          nameChart.Series[0].Points[1].LabelForeColor = Color.Transparent;
        }
        else if (valueOver > 0 && valueReject > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(255, 128, 0); // Cam
          nameChart.Series[0].Points[1].Color = Color.Red; // ĐỎ
          nameChart.Series[0].Points[0].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[1].Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
          nameChart.Series[0].Points[1].LabelForeColor = Color.Transparent;
        }

        else if (valueOK > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(0, 192, 0); //Xanh
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
        }
        else if (valueOver > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.FromArgb(255, 128, 0); // Cam
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
        }
        else if (valueReject > 0)
        {
          nameChart.Series[0].Points[0].Color = Color.Red; // ĐỎ
          nameChart.Series[0].Points[0].LabelForeColor = Color.Transparent;
        }


      }

    }

  }
}

#endregion