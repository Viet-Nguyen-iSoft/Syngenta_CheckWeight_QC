using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
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
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmDecentralization : Form
  {
    public delegate void SendChangeDecentralization();
    public event SendChangeDecentralization OnSendChangeDecentralization;

    public FrmDecentralization()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmDecentralization _Instance = null;
    public static FrmDecentralization Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmDecentralization();
        }
        return _Instance;
      }
    }


    #endregion

    private async void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        string roleQC = ucCheckBoxs1.RoleUser();
        string roleME = ucCheckBoxs2.RoleUser();
        string roleShiftLeader = ucCheckBoxs3.RoleUser();
        string roleOP = ucCheckBoxs4.RoleUser();
        string roleAdmin = ucCheckBoxs5.RoleUser();

        AppCore.Ins._listRoles[0].Permission = roleQC;
        AppCore.Ins._listRoles[1].Permission = roleME;
        AppCore.Ins._listRoles[2].Permission = roleShiftLeader;
        AppCore.Ins._listRoles[3].Permission = roleOP;
        AppCore.Ins._listRoles[4].Permission = roleAdmin;

        await AppCore.Ins.UpdateRange(AppCore.Ins._listRoles);
        AppCore.Ins.UpdateAccountCurrent(AppCore.Ins._listRoles);
        OnSendChangeDecentralization?.Invoke();

        new FrmNotification().ShowMessage("Cài đặt phân quyền thành công.", eMsgType.Info);
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        new FrmNotification().ShowMessage("Cài đặt phân quyền thất bại !", eMsgType.Error);
      }
    }

    private void FrmDecentralization_Load(object sender, EventArgs e)
    {
      List<ePermit> ePermits = new List<ePermit>();
      ePermits.Add(ePermit.role_ImportProduct);
      ePermits.Add(ePermit.role_Synthetic);
      ePermits.Add(ePermit.role_Excel);
      ePermits.Add(ePermit.role_ShiftLineSetting);
      ePermits.Add(ePermit.role_AccountSetting);
      ePermits.Add(ePermit.role_ChangeProduct);
      ePermits.Add(ePermit.role_ChangeShiftType);
      ePermits.Add(ePermit.role_CreateUser);
      ePermits.Add(ePermit.role_ChangeUser);

      List<string> role = AppCore.Ins._listRoles?.Select(x => x.Permission).ToList();

      if (role != null)
      {
        ucCheckBoxs1.SetRoleUser(role[0], ePermits);
        ucCheckBoxs2.SetRoleUser(role[1], ePermits);
        ucCheckBoxs3.SetRoleUser(role[2], ePermits);
        ucCheckBoxs4.SetRoleUser(role[3], ePermits);
        ucCheckBoxs5.SetRoleUser(role[4], ePermits);
      }

      Instance_OnSendChangeLogin();
      FrmMain.Instance.OnSendChangeLogin += Instance_OnSendChangeLogin;
    }

    private void Instance_OnSendChangeLogin()
    {
      this.btnSave.Visible = CheckRoleEditShiftInfor();
    }

    private bool CheckRoleEditShiftInfor()
    {
      return (AppCore.Ins.CheckRole(ePermit.role_ShiftLineSetting));
    }

    private void FrmDecentralization_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        this.btnSave.PerformClick();
      }
    }
  }
}
