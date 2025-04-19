using SyngentaWeigherQC.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;
using Application = System.Windows.Forms.Application;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmNotification : Form
  {
    public FrmNotification()
    {
      InitializeComponent();
    }

    public void ShowMessage(string information, eMsgType nameImage)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowMessage(information, nameImage);
        }));
        return;
      }
      try
      {
        lbInformation.Text = information;
        this.picIcon.Image = new Bitmap(Application.StartupPath + $"\\Image\\{nameImage}.png");
        timer1.Enabled = true;
        this.ShowDialog();
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex.ToString());
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Stop();
      this.Close();
    }

    private void FrmNotification_Load_1(object sender, EventArgs e)
    {
      this.timer1.Start();
    }
  }
}
