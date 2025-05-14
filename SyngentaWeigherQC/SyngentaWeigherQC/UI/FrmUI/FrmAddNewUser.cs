using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmAddNewUser : Form
  {
    public delegate void SendSendOKClicked(ShiftLeader user);
    public event SendSendOKClicked OnSendOKClicked;

    public FrmAddNewUser()
    {
      InitializeComponent();
    }

    private ShiftLeader _user = new ShiftLeader();
    private eEditUser _eEditUser = new eEditUser();
    public FrmAddNewUser(string title,ShiftLeader user, eEditUser eEditUser)
    {
      InitializeComponent();
      _user = user;
      _eEditUser = eEditUser;

      this.lbTitle.Text = title;
      this.txtName.Texts = (eEditUser == eEditUser.ChangeUser) ? _user.UserName : "";
      this.btnAdd.Text = (eEditUser == eEditUser.ChangeUser) ? "Update" : "Add";
    }


    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtName.Texts))
      {
        new FrmNotification().ShowMessage("Vui lòng nhập tên !", eMsgType.Warning);
        return;
      }
      if (_eEditUser == eEditUser.NewUser)
      {
        _user.UserName = this.txtName.Texts;
        _user.Passwords = "";
        _user.RoleId = 4;
        _user.IsEnable = false;
        _user.CreatedAt = DateTime.Now;
        _user.UpdatedAt = DateTime.Now;
      }
      else
      {
        _user.UserName = this.txtName.Texts;
        _user.UpdatedAt = DateTime.Now;
      }

      OnSendOKClicked?.Invoke(_user);
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }



    private void FrmAddNewUser_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnAdd.PerformClick();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        this.btnCancel.PerformClick();
      }
    }
  }
}
