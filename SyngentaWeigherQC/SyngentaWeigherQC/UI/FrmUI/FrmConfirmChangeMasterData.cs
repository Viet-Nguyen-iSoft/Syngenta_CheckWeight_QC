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
using static SyngentaWeigherQC.eNum.enumSoftware;
using static System.Net.Mime.MediaTypeNames;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmConfirmChangeMasterData : Form
  {
    public FrmConfirmChangeMasterData()
    {
      InitializeComponent();
    }

    public delegate void SendSendOKClicked(object sender, string contentChange);
    public event SendSendOKClicked OnSendOKClicked;


    private async void btConfirm_Click_1(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(txtContentChange.Text))
      {
        new FrmNotification().ShowMessage("Vui lòng nhập nội dung thay đổi !", eMsgType.Warning);
        return;
      }

      HistoricalChangeMasterData historical = new HistoricalChangeMasterData()
      {
        Reason = txtContentChange.Text,
        UpdateBy = 1,
      };

      await AppCore.Ins.Add(historical);

      OnSendOKClicked?.Invoke(this, txtContentChange.Text);
      this.Close();
    }

    private void btCancel_Click_1(object sender, EventArgs e)
    {
      this.Close();
    }

    private void FrmConfirmChangeMasterData_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btConfirm.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btCancel.PerformClick();
      }
    }
  }
}
