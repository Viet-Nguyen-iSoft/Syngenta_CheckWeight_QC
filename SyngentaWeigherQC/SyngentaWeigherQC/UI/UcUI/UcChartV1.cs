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
  public partial class UcChartV1 : UserControl
  {
    public UcChartV1()
    {
      InitializeComponent();
    }

    public string SetTilte
    {
      set { chart1.Titles[0].Text = value; }
    }


    public void SetDataChart(Dictionary<string, double> groupData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChart(groupData);
        }));
        return;
      }

      chart1.Series[0].Points.Clear();
      if (groupData == null) return;

      if (groupData.Count>0)
      {
        foreach (var item in groupData)
        {
          chart1.Series[0].Points.AddXY(item.Key, Math.Round(item.Value, 3));
        }
      }  
    }

    public void SetDataChartV2(Dictionary<string, double> groupData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataChartV2(groupData);
        }));
        return;
      }

      chart1.Series[0].Points.Clear();
      if (groupData == null) return;

      if (groupData.Count > 0)
      {
        foreach (var item in groupData)
        {
          chart1.Series[0].Points.AddXY(item.Key, Math.Round(item.Value, 2));
        }
      }
    }


    public void IsVisible(bool isVisible)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          IsVisible(isVisible);
        }));
        return;
      }
      this.tableLayoutPanel2.Visible = isVisible;
    }






  }
}
