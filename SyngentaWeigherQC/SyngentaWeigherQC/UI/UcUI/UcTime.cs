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

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcTime : UserControl
  {
    public UcTime()
    {
      InitializeComponent();
    }

    public void SetData(int hour, int minute, int second)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetData(hour, minute, second);
        }));
        return;
      }
      this.numericUpDownHour.Value = hour;
      this.numericUpDownMinute.Value = minute;
      this.numericUpDownSecond.Value = second;
    }

    public Time GetTime()
    {
      Time time = new Time();
      time.Hour = (int) numericUpDownHour.Value;
      time.Minute = (int)numericUpDownMinute.Value;
      time.Second = (int)numericUpDownSecond.Value;

      return time;
    }


  }
}
