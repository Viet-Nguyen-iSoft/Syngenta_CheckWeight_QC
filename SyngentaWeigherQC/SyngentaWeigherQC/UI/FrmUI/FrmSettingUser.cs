using Aspose.Cells;
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
using static SyngentaWeigherQC.eNum.eUI;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmSettingUser : Form
  {
    public FrmSettingUser()
    {
      InitializeComponent();
    }
    #region Singleton parttern
    private static FrmSettingUser _Instance = null;
    public static FrmSettingUser Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettingUser();
        }
        return _Instance;
      }
    }



    #endregion



    private void FrmChangePass_Load(object sender, EventArgs e)
    {
      List<Roles> listRole = AppCore.Ins._listRoles;

      ucAccount1.SetAccount(listRole[0].Name, listRole[0].Passwords);
      ucAccount2.SetAccount(listRole[1].Name, listRole[1].Passwords);
      ucAccount3.SetAccount(listRole[2].Name, listRole[2].Passwords);
      ucAccount4.SetAccount(listRole[3].Name, listRole[3].Passwords);
      ucAccount5.SetAccount(listRole[4].Name, listRole[4].Passwords);

      var ClickSave1 = new Action(() => { Save(eRole.QC);});
      this.ucAccount1.SetTag(ClickSave1);

      var ClickSave2 = new Action(() => { Save(eRole.ME); });
      this.ucAccount2.SetTag(ClickSave2);

      var ClickSave3 = new Action(() => { Save(eRole.ShiftLeader); });
      this.ucAccount3.SetTag(ClickSave3);

      var ClickSave4 = new Action(() => { Save(eRole.OP); });
      this.ucAccount4.SetTag(ClickSave4);

      var ClickSave5 = new Action(() => { Save(eRole.Admin); });
      this.ucAccount5.SetTag(ClickSave5);

      Instance_OnSendChangeLogin();
    }

    private bool isRole = false;
    private void Instance_OnSendChangeLogin()
    {
      isRole = CheckRoleEditShiftInfor();
      this.ucAccount1.Role(isRole);
      this.ucAccount2.Role(isRole);
      this.ucAccount3.Role(isRole);
      this.ucAccount4.Role(isRole);
      this.ucAccount5.Role(isRole);
    }
    private bool CheckRoleEditShiftInfor()
    {
      return (AppCore.Ins.CheckRole(ePermit.Role_Setting_User) || AppCore.Ins._roleCurrent.Name == "iSOFT");
    }
    

    private async void Save(eRole eRole)
    {
      if (AppCore.Ins.CheckRole(ePermit.Role_Setting_User) || AppCore.Ins._roleCurrent.Name == "iSOFT")
      {
        string pass = "";
        switch (eRole)
        {
          case eRole.QC:
            pass = this.ucAccount1.GetPass;
            this.ucAccount1.CheckMode();
            break;
          case eRole.ME:
            pass = this.ucAccount2.GetPass;
            this.ucAccount2.CheckMode();
            break;
          case eRole.ShiftLeader:
            pass = this.ucAccount3.GetPass;
            this.ucAccount3.CheckMode();
            break;
          case eRole.OP:
            pass = this.ucAccount4.GetPass;
            this.ucAccount4.CheckMode();
            break;
          case eRole.Admin:
            pass = this.ucAccount5.GetPass;
            this.ucAccount5.CheckMode();
            break;
        }

        AppCore.Ins._listRoles[(int)eRole].Passwords = pass;
        await AppCore.Ins.UpdateRole(AppCore.Ins._listRoles[(int)eRole]);
      }
      else
      {
        new FrmNotification().ShowMessage("Tài khoản không có quyền thay đổi mật khẩu các Account!", eMsgType.Warning);
      }


    }





  }
}
