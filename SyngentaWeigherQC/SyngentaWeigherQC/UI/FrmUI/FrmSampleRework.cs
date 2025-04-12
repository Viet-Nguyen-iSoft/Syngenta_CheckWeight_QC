
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static SyngentaWeigherQC.eNum.eUI;

namespace SynCheckWeigherLoggerApp.DashboardViews
{
  public partial class FrmSampleRework : Form
  {
    public delegate void SendReWeigherDone(Sample sample);
    public event SendReWeigherDone OnSendReWeigherDone;

    public FrmSampleRework()
    {
      InitializeComponent();
    }


    private Sample _sample = new Sample();
    public FrmSampleRework(Sample sample)
    {
      InitializeComponent();
      _sample = sample;
      ShowDisplay(lblOldSampleValue, _sample.Value);

      AppCore.Ins.OnSendDataReWeigher += Ins_OnSendDataReWeigher;
    }

    private System.Timers.Timer timer = new System.Timers.Timer();

    private void Ins_OnSendDataReWeigher(double value)
    {
      AppCore.Ins.OnSendDataReWeigher -= Ins_OnSendDataReWeigher;

      ShowDisplay(lblNewValue, value);
      ShowStatus();

      timer.Interval = 1000;
      timer.Elapsed += Timer_Elapsed;
      timer.Start();

      _sample.Value = value;
      _sample.isEdited= true;
      _sample.UpdatedAt = DateTime.Now;
    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      timer.Stop();
      CloseForm();
    }
    private void CloseForm()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new MethodInvoker(CloseForm));
        return;
      }
      OnSendReWeigherDone?.Invoke(_sample);
      this.Close();
    }


    private void ShowDisplay(Label label, double value)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowDisplay(label, value);
        }));
        return;
      }

      label.Text = value.ToString();
    }

    private void ShowStatus()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowStatus();
        }));
        return;
      }
      lblStatus.Text = "Cân xong";
      lblStatus.BackColor = Color.Green;
    }
  }
}
