using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmLogIn : Form
  {
    public delegate void SendLogInOK(Roles account);
    public event SendLogInOK OnSendLogInOK;
    public FrmLogIn()
    {
      InitializeComponent();
    }

    private List<Roles> _roles = new List<Roles>();
    public FrmLogIn(List<Roles> roles) : this()
    {
      _roles = roles;
    }

    private void FrmLogIn_Load(object sender, EventArgs e)
    {
      this.lbInfor.Visible = false;
      if (_roles != null)
      {
        this.cbAccount.DataSource = _roles?.Select(x => x.Name).ToList();
        this.cbAccount.SelectedIndex = 0;
      }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      string nameAccount = this.cbAccount.Text;

      if (nameAccount == "iSOFT")
      {
        if (this.txtPassword.Text == "058200005781")
        {
          //Thành công
          //OnSendLogInOK?.Invoke(this, nameAccount);
          this.Close();
        }
        else
        {
          this.lbInfor.Text = "Mật khẩu không chính xác !";
          this.lbInfor.Visible = true;
        }
      }
      else
      {
        Roles role = _roles?.Where(x => x.Name == nameAccount).FirstOrDefault();
        string pass = (role != null) ? role.Passwords : "";

        if (this.txtPassword.Text == pass)
        {
          //Thành công
          OnSendLogInOK?.Invoke(role);
          this.Close();
        }
        else
        {
          this.lbInfor.Text = "Mật khẩu không chính xác !";
          this.lbInfor.Visible = true;
        }
      }
    }

    private void FrmLogIn_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnLogin.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btnCancel.PerformClick();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
