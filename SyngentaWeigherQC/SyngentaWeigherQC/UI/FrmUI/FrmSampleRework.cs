using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SynCheckWeigherLoggerApp.DashboardViews
{
  public partial class FrmSampleRework : Form
  {
    public delegate void SendReWeigherDone(DatalogWeight datalogWeight);
    public event SendReWeigherDone OnSendReWeigherDone;

    public FrmSampleRework()
    {
      InitializeComponent();
    }

    private System.Timers.Timer timer = new System.Timers.Timer();
    private DatalogWeight _datalogWeight = new DatalogWeight();
    public FrmSampleRework(DatalogWeight datalogWeight):this()
    {
      _datalogWeight = datalogWeight;

      ShowDisplay(lblOldSampleValue, datalogWeight.ValuePrevious);

      AppCore.Ins.OnSendValueReweight += Ins_OnSendDataReWeigher;
    }

    private void Ins_OnSendDataReWeigher(double value)
    {
      ShowDisplay(lblNewValue, value);
      ShowStatus();

      timer.Interval = 1000;
      timer.Elapsed += Timer_Elapsed;
      timer.Start();

      _datalogWeight.Value = value;
      _datalogWeight.IsChange = true;
      _datalogWeight.UpdatedAt = DateTime.Now;
    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      timer.Stop();

      OnSendReWeigherDone?.Invoke(_datalogWeight);

      CloseForm();
    }

    private void CloseForm()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CloseForm();
        }));
        return;
      }

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

      label.Text = $"{value} g";
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
