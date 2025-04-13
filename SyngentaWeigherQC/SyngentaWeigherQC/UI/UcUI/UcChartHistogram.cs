using Microsoft.Extensions.Logging;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
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
using static ClosedXML.Excel.XLPredefinedFormat;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcChartHistogram : UserControl
  {
    public UcChartHistogram()
    {
      InitializeComponent();
    }

    private void UcChartHistogram_Load(object sender, EventArgs e)
    {

    }

    #region Chart Histogram
    public void SetDataChart(List<Sample> listSample, Production productions)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart(listSample, productions);
        }));
        return;
      }

      try
      {
        chart1.Series[0].Points.Clear();
        chart1.Series[1].Points.Clear();
        chart1.Series[2].Points.Clear();
        chart1.Series[3].Points.Clear();
        dataGridView1.Rows.Clear();

        if (listSample == null) return;
        if (listSample.Count() <= 0) return;

        List<double> sampleData = listSample.Where(x=>x.isEnable == true && x.isHasValue == true).Select(x => x.Value).ToList();

        double mean = CalMean(sampleData);
        double stdev = CalStdev(sampleData);
        double maxValue = Math.Ceiling(sampleData.Max() * 100) / 100; 
        double minValue = Math.Floor(sampleData.Min()*100) / 100;
        int totalSample = sampleData.Count();

        int numbersCol = (int)Math.Round(Math.Sqrt(totalSample), MidpointRounding.AwayFromZero);
        double widthCol = (maxValue - minValue) / numbersCol;
        double valueColFirst = minValue;

        double[] arrayDataValueStartCol = new double[numbersCol + 1];
        double[] arrayDataValueCenterCol = new double[numbersCol];
        int[] arrayFrequency = new int[numbersCol];
        double[] arrayBell = new double[numbersCol];

        // Lấy giá trị các cột
        for (int i = 0; i <= numbersCol; i++)
        {
          arrayDataValueStartCol[i] = Math.Round(valueColFirst + widthCol * i, 3);

          if (i!= numbersCol)
            arrayDataValueCenterCol[i] = Math.Round(valueColFirst + widthCol * i + (widthCol / 2), 3);
        }

        for (int i = 0; i < numbersCol; i++)
        {
          arrayFrequency[i] = sampleData.Where(x => x >= arrayDataValueStartCol[i] && x < arrayDataValueStartCol[i + 1]).Count();
        }


        for (int i = 0; i < numbersCol; i++)
        {
          double valueBell = (arrayFrequency[i] * 100 / (double)totalSample);
          arrayBell[i] = Math.Round(valueBell, 2);
        }


        //Col //Bell
        for (int i = 0; i < numbersCol; i++)
        {
          chart1.Series[0].Points.AddXY(Math.Round(arrayDataValueCenterCol[i], 2), arrayFrequency[i]);
          chart1.Series[1].Points.AddXY(Math.Round(arrayDataValueCenterCol[i], 2), arrayBell[i]);
        }

        for (int i = 0; i < arrayFrequency.Count(); i++)
        {
          int row = dataGridView1.Rows.Add();
          dataGridView1.Rows[row].Cells["Column1"].Value = $"{arrayDataValueStartCol[i]} - {arrayDataValueStartCol[i+1]}" ;
          dataGridView1.Rows[row].Cells["Column2"].Value = arrayFrequency[i];
          dataGridView1.Rows[row].Cells["Column3"].Value = arrayBell[i];
        }

        // Lower2T
        chart1.Series[2].Points.AddXY(productions.LowerLimitFinal, 0);
        chart1.Series[2].Points.AddXY(productions.LowerLimitFinal, arrayFrequency.Max() * 1.1);

        chart1.Series[3].Points.AddXY(productions.UpperLimitFinal, 0);
        chart1.Series[3].Points.AddXY(productions.UpperLimitFinal, arrayFrequency.Max() * 1.1);

        //minValue = Math.Min(minValue, productions.LowerLimitFinal);
        //maxValue = Math.Max(maxValue, productions.UpperLimitFinal);

        //chart1.ChartAreas[0].AxisX.Minimum = Math.Round(minValue * 0.995, 1);
        //chart1.ChartAreas[0].AxisX.Maximum = Math.Round(maxValue * 1.005, 1);

        chart1.ChartAreas[0].AxisX.Minimum = Math.Round(productions.LowerLimitFinal * 0.995, 1);
        chart1.ChartAreas[0].AxisX.Maximum = Math.Round(productions.UpperLimitFinal * 1.005, 1);
      }
      catch (Exception ex)
      {
        eLoggerHelper.LogErrorToFileLog(ex);
      }

    }
    #endregion

    private double CalMean(List<double> list_data)
    {
      double x_tb = 0;
      foreach (var item in list_data)
      {
        x_tb += item;
      }
      return Math.Round(x_tb / list_data.Count, 2);
    }

    private double CalStdev(List<double> list_data)
    {
      double mean_x_tb = CalMean(list_data);
      double sumOfSquares = 0;
      foreach (double data_x in list_data)
        sumOfSquares += Math.Pow(data_x - mean_x_tb, 2);
      double stdDev = Math.Sqrt(sumOfSquares / (list_data.Count - 1));
      return Math.Round(stdDev, 2);
    }


  }
}
