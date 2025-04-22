using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.UI.FrmUI;
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

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcAccount : UserControl
  {
    public UcAccount()
    {
      InitializeComponent();
    }

    public void SetAccount(string account, string password)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetAccount(account, password);
        }));
        return;
      }

      this.lbAccount.Text = account;
      this.txtPass.Texts = password;
    }

    public void Role(bool isOK)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          Role(isOK);
        }));
        return;
      }

      this.btnHidePass.Visible = isOK;
      this.btnSave.Visible = isOK;
      this.txtPass.PasswordChar = true;
    }

    public string GetPass
    {
      get { return this.txtPass.Texts; }
    }

    private Action _ClickEnable = null;
    public void SetTag(Action ClickEnable)
    {
      this._ClickEnable = ClickEnable;
    }
    private void btnSave_Click(object sender, EventArgs e)
    {
      _ClickEnable();
    }



    public void CheckMode()
    {
      if (this.btnSave.Text=="Thay đổi")
      {
        this.btnSave.Text = "Lưu thay đổi";
        this.txtPass.Enabled = true;
      }
      else
      {
        this.btnSave.Text = "Thay đổi";
        this.txtPass.Enabled = false;
      }
    }


    private void btnHidePass_Click(object sender, EventArgs e)
    {
      if (AppCore.Ins.CheckRole(ePermit.Role_Setting_User) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        this.txtPass.PasswordChar = !this.txtPass.PasswordChar;
        this.btnHidePass.Image = (this.txtPass.PasswordChar) ? Properties.Resources.passDisable32px : Properties.Resources.passEnable32px;
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền xem mật khẩu các Account!", eMsgType.Warning);
      }
      
    }
  }
}
