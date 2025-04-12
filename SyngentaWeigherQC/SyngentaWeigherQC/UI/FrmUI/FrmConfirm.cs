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
  public partial class FrmConfirm : Form
  {
    public delegate void SendSendOKClicked();
    public event SendSendOKClicked OnSendOKClicked;

    public FrmConfirm()
    {
      InitializeComponent();
    }

    public FrmConfirm(string content, eModeTare eModeTare):this()
    {
      this.lbInformation.Text = content;
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
