
using DocumentFormat.OpenXml.Wordprocessing;
using SyngentaWeigherQC;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.FrmUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Color = System.Drawing.Color;

namespace SynCheckWeigherLoggerApp.SettingsViews
{
  public partial class FrmSettings : Form
  {
    #region Singleton parttern
    private static FrmSettings _Instance = null;
     

   // private Stations _station

    public static FrmSettings Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmSettings();
        }
        return _Instance;
      }
    }
    #endregion
    public FrmSettings()
    {
      InitializeComponent();
    
    }

    #region Call form child
    private Form CurrentForm;
    public void OpenChildForm(AppModulSupport modulSupport, Form ChildForm)
    {
      bool Is_same_form = false;
      if (this.pnBody.Tag != null)
      {
        if (this.pnBody.Tag is Tuple<AppModulSupport, Form>)
        {
          Tuple<AppModulSupport, Form> TagAsForm = (Tuple<AppModulSupport, Form>)(this.pnBody.Tag);
          if (TagAsForm.Item1 == modulSupport)
          {
            Is_same_form = true;
          }
        }
      }

      if (CurrentForm != null)
      {
        CurrentForm.Visible = false;
      }
      this.pnBody.Tag = Tuple.Create(modulSupport, ChildForm);
      CurrentForm = ChildForm;
      ChildForm.TopLevel = false;
      ChildForm.FormBorderStyle = FormBorderStyle.None;
      ChildForm.Dock = DockStyle.Fill;
      ChildForm.BringToFront();
      this.pnBody.Controls.Add(ChildForm);
      ChildForm.Show();

      ActiveColor(modulSupport);
    }
    #endregion



    private void FrmSettings_Load(object sender, EventArgs e)
    {
      this.btnSettingLine.PerformClick();
      this.btnSettingDevice.Visible = true;
      //this.btnSettingDevice.Visible = (AppCore.Ins._roleCurrent?.Name == "iSOFT" || AppCore.Ins._roleCurrent?.Name == "Admin" || AppCore.Ins._isPermitDev==true);
      //FrmMain.Instance.OnSendChangeLogin += Instance_OnSendChangeLogin;
    }

    private void Instance_OnSendChangeLogin()
    {
      this.btnSettingDevice.Visible = (AppCore.Ins._roleCurrent.Name == "iSOFT" || AppCore.Ins._roleCurrent.Name == "Admin" || AppCore.Ins._isPermitDev == true);
    }

    private void btnSettingLine_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.LineSetting, FrmSettingLine.Instance);
    }

    private void btnShiftReport_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.SettingGeneral, FrmSettingGeneral.Instance);
    }

    private void btnImportProducts_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.SettingProducts, FrmSettingProducts.Instance);
    }

    private void btnSettingDevice_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.DeviceSetting, FrmSettingConfigSoftware.Instance);
    }

    private void btnSettingUser_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.UserSetting, FrmShiftLeader.Instance);
    }

    private void btnDecentralization_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.Decentralization, FrmSettingDecentralization.Instance);
    }

    private void btnChangePass_Click(object sender, EventArgs e)
    {
      OpenChildForm(AppModulSupport.PassChangeSetting, FrmSettingUser.Instance);
    }


    private Color colorNoSelect= Color.FromArgb(58, 109, 178);
    private Color colorSelect = Color.CornflowerBlue;
    private void ActiveColor(AppModulSupport appModul)
    {
      btnSettingLine.BackColor = colorNoSelect;
      btnShiftReport.BackColor = colorNoSelect;
      btnImportProducts.BackColor = colorNoSelect;
      btnSettingDevice.BackColor = colorNoSelect;
      btnSettingUser.BackColor = colorNoSelect;
      btnDecentralization.BackColor = colorNoSelect;
      btnChangePass.BackColor = colorNoSelect;

      if (appModul==AppModulSupport.SettingGeneral)
      {
        btnShiftReport.BackColor = colorSelect;
      }
      if (appModul == AppModulSupport.LineSetting)
      {
        btnSettingLine.BackColor = colorSelect;
      }
      else if (appModul == AppModulSupport.SettingProducts)
      {
        btnImportProducts.BackColor = colorSelect;
      }
      else if (appModul == AppModulSupport.DeviceSetting)
      {
        btnSettingDevice.BackColor = colorSelect;
      }
      else if (appModul == AppModulSupport.UserSetting)
      {
        btnSettingUser.BackColor = colorSelect;
      }
      else if (appModul == AppModulSupport.Decentralization)
      {
        btnDecentralization.BackColor = colorSelect;
      }
      else if (appModul == AppModulSupport.PassChangeSetting)
      {
        btnChangePass.BackColor = colorSelect;
      }

    }

   
  }
}
