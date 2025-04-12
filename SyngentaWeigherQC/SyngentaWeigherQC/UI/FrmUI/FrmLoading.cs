using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmLoading : Form
  {
    public FrmLoading()
    {
      InitializeComponent();

      this.WindowState = FormWindowState.Normal; this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.TopLevel = true;
      this.TopMost = true;
    }


    private static FrmLoading ins;

    public static FrmLoading Ins
    {
      get { return ins == null ? ins = new FrmLoading() : ins; }
    }

    public void ShowLoading()
    {
      if (!this.Visible)
        this.Visible = true;
    }
    public void CloseLoading()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CloseLoading();
        }));
        return;
      }
      this.Visible = false;
    }


    private void LoadingForm_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    public void ShowCO()
    {

    }
    public void CloseCO()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CloseLoading();
        }));
        return;
      }
      this.Visible = false;
    }



  }
}
