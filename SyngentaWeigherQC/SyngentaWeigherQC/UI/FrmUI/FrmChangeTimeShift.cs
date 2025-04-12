using SyngentaWeigherQC.Control;
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
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmChangeTimeShift : Form
  {
    public delegate void SendSendChangeClicked(Shift shiftChange);
    public event SendSendChangeClicked OnSendSendChangeClicked;

    public FrmChangeTimeShift()
    {
      InitializeComponent();
    }

    private Shift _shift = new Shift();
    public FrmChangeTimeShift(Shift nameShift)
    {
      InitializeComponent();
      _shift = nameShift;
    }

    private void LoadInfo()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadInfo();
        }));
        return;
      }

      lbNameShift.Text = _shift.Name;
      ucTimeStart.SetData(_shift.StartHour, _shift.StartMinute, _shift.StartSecond);
      ucTimeEnd.SetData(_shift.EndHour, _shift.EndMinute, _shift.EndSecond);
    }


    private void FrmChangeTimeShift_Load(object sender, EventArgs e)
    {
      LoadInfo();
    }


    private void brnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnChange_Click(object sender, EventArgs e)
    {
      try
      {
        Time timerStart = ucTimeStart.GetTime();
        Time timerEnd = ucTimeEnd.GetTime();

        _shift.Name = lbNameShift.Text;
        if (timerStart != null)
        {
          _shift.StartHour = timerStart.Hour;
          _shift.StartMinute = timerStart.Minute;
          _shift.StartSecond = timerStart.Second;
        }
        if (timerEnd != null)
        {
          _shift.EndHour = timerEnd.Hour;
          _shift.EndMinute = timerEnd.Minute;
          _shift.EndSecond = timerEnd.Second;
        }

        OnSendSendChangeClicked?.Invoke(_shift);

        this.Close();
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex.ToString());
      }
      
    }

    private void FrmChangeTimeShift_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnChange.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btnCancel.PerformClick();
      }
    }
  }
}
