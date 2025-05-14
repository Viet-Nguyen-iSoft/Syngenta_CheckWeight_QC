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

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcCheckBoxs : UserControl
  {
    public UcCheckBoxs()
    {
      InitializeComponent();
    }

    public string RoleUser()
    {
      string role = "";
      role += (ucCheckbox1.IsCheck()) ? $"{ePermit.Role_Setting_Line}" : "";
      role += (ucCheckbox2.IsCheck()) ? $"_{ePermit.Role_Setting_SettingGeneral}" : "";
      role += (ucCheckbox3.IsCheck()) ? $"_{ePermit.Role_Setting_Product}" : "";
      role += (ucCheckbox4.IsCheck()) ? $"_{ePermit.Role_Setting_User}" : "";
      role += (ucCheckbox5.IsCheck()) ? $"_{ePermit.Role_Setting_ShiftLeader}" : "";
      role += (ucCheckbox6.IsCheck()) ? $"_{ePermit.Role_Setting_Decentralization}" : "";
      role += (ucCheckbox7.IsCheck()) ? $"_{ePermit.Role_Setting_Connection}" : "";
      role += (ucCheckbox8.IsCheck()) ? $"_{ePermit.Role_Excel}" : "";
      role += (ucCheckbox9.IsCheck()) ? $"_{ePermit.Role_Consumption}" : "";
      return role;
    }

    public void SetRoleUser(string role, List<ePermit> permits)
    {
      ucCheckbox1.SetCheck(SetRoleRole(role, permits[0]));
      ucCheckbox2.SetCheck(SetRoleRole(role, permits[1]));
      ucCheckbox3.SetCheck(SetRoleRole(role, permits[2]));
      ucCheckbox4.SetCheck(SetRoleRole(role, permits[3]));
      ucCheckbox5.SetCheck(SetRoleRole(role, permits[4]));
      ucCheckbox6.SetCheck(SetRoleRole(role, permits[5]));
      ucCheckbox7.SetCheck(SetRoleRole(role, permits[6]));
      ucCheckbox8.SetCheck(SetRoleRole(role, permits[7]));
      ucCheckbox9.SetCheck(SetRoleRole(role, permits[8]));
    }

    public bool SetRoleRole(string role, ePermit permit)
    {
      return role.Contains(permit.ToString());
    }

  }
}
