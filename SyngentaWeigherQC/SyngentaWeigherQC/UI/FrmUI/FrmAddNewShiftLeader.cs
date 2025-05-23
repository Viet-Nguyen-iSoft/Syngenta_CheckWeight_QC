using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmAddNewShiftLeader : Form
  {
    public delegate void SendSendOKClicked();
    public event SendSendOKClicked OnSendOKClicked;

    public FrmAddNewShiftLeader()
    {
      InitializeComponent();
    }

    private ShiftLeader _shiftLeader = new ShiftLeader();
    private eEditUser _eEditUser = eEditUser.NewUser;
    public FrmAddNewShiftLeader(string title,ShiftLeader user):this()
    {
      _shiftLeader = user;
      _eEditUser = eEditUser.ChangeUser;

      this.lbTitle.Text = title;
      this.txtName.Texts = (_eEditUser == eEditUser.ChangeUser) ? _shiftLeader.UserName : "";
      this.btnAdd.Text = "Cập nhật";
    }
    public FrmAddNewShiftLeader(string title) : this()
    {
      _eEditUser = eEditUser.NewUser;

      this.lbTitle.Text = title;
      this.btnAdd.Text = "Thêm";
    }


    private async void btnAdd_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtName.Texts))
      {
        new FrmNotification().ShowMessage("Vui lòng nhập tên !", eMsgType.Warning);
        return;
      }
      if (_eEditUser == eEditUser.NewUser)
      {
        _shiftLeader.UserName = this.txtName.Texts;
        _shiftLeader.RoleId = 4;

        await AppCore.Ins.Add(_shiftLeader);
      }
      else
      {
        _shiftLeader.UserName = this.txtName.Texts;
        _shiftLeader.UpdatedAt = DateTime.Now;

        await AppCore.Ins.Update(_shiftLeader);
      }


      OnSendOKClicked?.Invoke();
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
