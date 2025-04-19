using DocumentFormat.OpenXml.Office2010.Excel;
using SynCheckWeigherLoggerApp.SettingsViews;
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
using Color = System.Drawing.Color;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FormMain : Form
  {
    public FormMain()
    {
      InitializeComponent();
      this.WindowState = FormWindowState.Maximized;
      this.StartPosition = FormStartPosition.CenterScreen;

      this.Shown += FormMain_Shown;
    }

    private void FormMain_Shown(object sender, EventArgs e)
    {
      this.btnHome.PerformClick();
    }
    #region Singleton parttern
    private static FormMain _Instance = null;
    public static FormMain Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FormMain();
        }
        return _Instance;
      }
    }
    #endregion

    private static Color Select = Color.FromArgb(255, 255, 255);
    private static Color NoSelect = Color.FromArgb(49, 67, 107);

    private InforLine _InforLine;
    public void ChangePage(AppModulSupport button, string name = "name")
    {
      this.btnHome.ForeColor = NoSelect;
      this.btnSynthetic.ForeColor = NoSelect;
      this.btnReport.ForeColor = NoSelect;
      this.btnSetting.ForeColor = NoSelect;

      switch (button)
      {
        case AppModulSupport.OverView:
          this.btnHome.ForeColor = Select;
          OpenChildForm(AppModulSupport.OverView, FrmOverView.Instance);
          break;
        case AppModulSupport.Home:
          OpenChildForm(AppModulSupport.Home, FrmHome.GetInstance(_InforLine));
          break;
        //case AppModulSupport.MasterData:
        //  this.btnMasterData.ForeColor = Select;
        //  OpenChildForm(AppModulSupport.MasterData, FrmMasterData.Instance);
        //  break;
        //case AppModulSupport.Synthetic:
        //  this.btnHistorical.ForeColor = Select;
        //  OpenChildForm(AppModulSupport.Synthetic, FrmHistorical.Instance);
        //  break;
        case AppModulSupport.ReportExcel:
          this.btnReport.ForeColor = Select;
          OpenChildForm(AppModulSupport.ReportExcel, FrmExcelExport.Instance);
          break;
        case AppModulSupport.Setting:
          this.btnSetting.ForeColor = Select;
          OpenChildForm(AppModulSupport.Setting, FrmSettings.Instance);
          break;
          //case AppModulSupport.User:
          //  this.btnUser.ForeColor = Select;
          //  OpenChildForm(AppModulSupport.User, FrmManagerUser.Instance);
          //  break;
      }
    }

    #region Call form child
    private Form CurrentForm;
    public void OpenChildForm(AppModulSupport modulSupport, Form ChildForm)
    {
      bool Is_same_form = false;
      if (this.panelMain.Tag != null)
      {
        if (this.panelMain.Tag is Tuple<AppModulSupport, Form>)
        {
          Tuple<AppModulSupport, Form> TagAsForm = (Tuple<AppModulSupport, Form>)(this.panelMain.Tag);
          if (TagAsForm.Item1 == modulSupport)
          {
            Is_same_form = true;
          }
        }
      }
      if (Is_same_form == false)
      {
        if (CurrentForm != null)
        {
          CurrentForm.Visible = false;

        }
        this.panelMain.Tag = Tuple.Create(modulSupport, ChildForm);
        CurrentForm = ChildForm;
        ChildForm.TopLevel = false;
        ChildForm.FormBorderStyle = FormBorderStyle.None;
        ChildForm.Dock = DockStyle.Fill;
        ChildForm.BringToFront();
        this.panelMain.Controls.Add(ChildForm);
        ChildForm.Show();
      }
      else
      {
        //do not 
      }
    }
    #endregion


    private void btnHome_Click(object sender, EventArgs e)
    {
      AppCore.Ins.eStatusModeWeight = eNum.eUI.eStatusModeWeight.OverView;
      AppCore.Ins.inforLineOperation = null;
      ChangePage(AppModulSupport.OverView);
    }

    private void btnSynthetic_Click(object sender, EventArgs e)
    {

    }

    private void btnReport_Click(object sender, EventArgs e)
    {
      ChangePage(AppModulSupport.ReportExcel);
    }

    private void btnSetting_Click(object sender, EventArgs e)
    {
      ChangePage(AppModulSupport.Setting);
    }


    public void OpenPageWeight(InforLine inforLine)
    {
      _InforLine = inforLine;
      ChangePage(AppModulSupport.Home, inforLine.Name);
    }



    
  }
}
