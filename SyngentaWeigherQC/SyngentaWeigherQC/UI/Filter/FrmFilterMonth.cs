using DocumentFormat.OpenXml.Bibliography;
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
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmFilterMonth : Form
  {
    public delegate void SendMonthChoose(int month);
    public event SendMonthChoose OnSendMonthChoose;

    public FrmFilterMonth()
    {
      InitializeComponent();
    }
    private int monthChoose = 0;

    private void FrmYear_Load(object sender, EventArgs e)
    {
      CreateAllCheckBox();
    }

    private void CreateAllCheckBox()
    {
      for(int i =1;i <= 12; i++)
      {
        CheckBox checkBox = new CheckBox();
        checkBox.Text = $"Tháng: {(i).ToString("00")}";
        checkBox.ForeColor = Color.Black;
        checkBox.Font = new Font(Font.FontFamily, 16);
        checkBox.AutoSize = true;
        checkBox.Tag = i;
        checkBox.CheckedChanged += AllCheckBox_CheckedChanged;
        flowLayoutPanel1.Controls.Add(checkBox);
      }
    }

    
    private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox changedCheckBox = sender as CheckBox;

      if (changedCheckBox.Checked)
      {
        foreach (var control in flowLayoutPanel1.Controls)
        {
          if (control is CheckBox cb && cb != changedCheckBox)
          {
            cb.Checked = false;
          }
        }

        monthChoose = (int)changedCheckBox.Tag;
      }
      else
      {
        monthChoose = 0;
      }  
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (monthChoose>0 && monthChoose<=12)
      {
        OnSendMonthChoose?.Invoke(monthChoose);
        this.Close();
      }
      else
      {
        new FrmNotification().ShowMessage("Vui lòng chọn tháng cần xem !", eMsgType.Warning);
      }  
    }


  }
}
