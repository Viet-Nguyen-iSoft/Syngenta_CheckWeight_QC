using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmConfirm : Form
  {
    public delegate void SendSendOKClicked();
    public event SendSendOKClicked OnSendOKClicked;

    public FrmConfirm()
    {
      InitializeComponent();
    }

    public FrmConfirm(string content):this()
    {
      this.lbInformation.Text = content;
    }

    public FrmConfirm(string content, eMsgType nameImage) : this()
    {
      try
      {
        this.lbInformation.Text = content;
        this.picIcon.Image = new Bitmap(Application.StartupPath + $"\\Image\\{nameImage}.png");
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
      OnSendOKClicked?.Invoke();
      this.Close();
    }

    private void FrmConfirm_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnConfirm.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btnCancel.PerformClick();
      }
    }

   
  }
}
