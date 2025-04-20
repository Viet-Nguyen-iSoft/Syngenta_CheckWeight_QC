using Microsoft.Extensions.Logging;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmWaiting : Form
  {
    public delegate void SendActiveApp();
    public event SendActiveApp OnSendActiveApp;
    public FrmWaiting()
    {
      InitializeComponent();
    }

    #region Singleton parttern
    private static FrmWaiting _Instance = null;
    public static FrmWaiting Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmWaiting();
        }
        return _Instance;
      }
    }


    #endregion

    public System.Timers.Timer timerDateTime = new System.Timers.Timer();
    private void FrmWaiting_Load(object sender, EventArgs e)
    {
      timerDateTime.Interval = 1000;
      timerDateTime.Elapsed += TimerDateTime_Elapsed;
      timerDateTime.Start();
    }

   
    private void TimerDateTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      timerDateTime.Stop();
      try
      {
        //if (cnt<10)
        //{
        //  ChangeBackgroundColor();
        //}
        //else
        //{
        //  UpdateDateTimeUI();
        //  cnt = 0;
        //}
        //cnt++;

        UpdateDateTimeUI();
      }
      catch (Exception)
      {

      }
      finally { timerDateTime.Start(); }
    }

    private void UpdateDateTimeUI()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          UpdateDateTimeUI();
        }));
        return;
      }
      this.lbTimeDate.Text = DateTime.Now.ToString("HH:mm:ss  dd/MM/yyyy");
    }

    private void ChangeBackgroundColor()
    {
      Random random = new Random();
      Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
      this.tableLayoutPanel1.BackColor = randomColor;
    }

    private void btnActive_Click(object sender, EventArgs e)
    {
      OnSendActiveApp?.Invoke();
      this.Close();
    }

    private void FrmWaiting_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnActive.PerformClick();
      }

    }
  }
}
