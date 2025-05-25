using System.Collections.Generic;
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
      role += (ucCheckbox1.IsCheck()) ? $"{ePermit.ReportConsumption}" : "";
      role += (ucCheckbox2.IsCheck()) ? $"_{ePermit.ReportExcel}" : "";
      role += (ucCheckbox3.IsCheck()) ? $"_{ePermit.AddProduct}" : "";
      role += (ucCheckbox4.IsCheck()) ? $"_{ePermit.EditProduct}" : "";
      role += (ucCheckbox5.IsCheck()) ? $"_{ePermit.SeeHistoricalAddProduct}" : "";
      role += (ucCheckbox6.IsCheck()) ? $"_{ePermit.SettingInformationLine}" : "";
      role += (ucCheckbox7.IsCheck()) ? $"_{ePermit.SettingGeneral}" : "";
      role += (ucCheckbox8.IsCheck()) ? $"_{ePermit.SettingAccount}" : "";
      role += (ucCheckbox9.IsCheck()) ? $"_{ePermit.SettingShiftLeader}" : "";
      role += (ucCheckbox10.IsCheck()) ? $"_{ePermit.SettingDevice}" : "";
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
      ucCheckbox10.SetCheck(SetRoleRole(role, permits[9]));
    }

    public bool SetRoleRole(string role, ePermit permit)
    {
      return role.Contains(permit.ToString());
    }

  }
}
