using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmInformation : Form
  {
    public delegate void SendSendOKClicked(object sender);
    public event SendSendOKClicked OnSendOKClicked;

    public delegate void SendCancelClicked(object sender);
    public event SendCancelClicked OnSendCancelClicked;

    public FrmInformation()
    {
      InitializeComponent();
    }

    public FrmInformation(string content):this()
    {
      this.Tag = content;
      this.lbInformation.Text = content;
    }
    public FrmInformation(Production product) : this()
    {
      this.Tag = product;
      this.lbInformation.Text = product.Name;
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
      OnSendOKClicked?.Invoke(this.Tag);
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      OnSendCancelClicked?.Invoke(this.Tag);
      this.Close();
    }


    private void FrmInformation_KeyUp(object sender, KeyEventArgs e)
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
