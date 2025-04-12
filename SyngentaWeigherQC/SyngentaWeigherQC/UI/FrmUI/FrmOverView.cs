using SynCheckWeigherLoggerApp.DashboardViews;
using SynCheckWeigherLoggerApp.SettingsViews;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.UI.FrmUI
{
  public partial class FrmOverView : Form
  {
    //public delegate void SendChooseLineWeight(InforLine inforLine);
    //public event SendChooseLineWeight OnSendChooseLineWeight;

    public FrmOverView()
    {
      InitializeComponent();
      LoadLine();
    }

    #region Singleton parttern
    private static FrmOverView _Instance = null;
    public static FrmOverView Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new FrmOverView();
        }
        return _Instance;
      }
    }
    #endregion

    private void FrmOverView_Load(object sender, EventArgs e)
    {
      FrmSettingLine.Instance.OnSendChangeLine += Instance_OnSendChangeLine;
      FrmUser.Instance.OnSendChangeLine += Instance_OnSendChangeLineUser;
    }

    private void Instance_OnSendChangeLineUser()
    {
      
    }

    private void Instance_OnSendChangeLine()
    {
      LoadLine();
    }

    private Size _Size = new Size(820, 500);
    private void LoadLine()
    {
      try
      {
        flowLayoutPanelLine.Controls.Clear();

        if (AppCore.Ins._listInforLine.Count > 0)
        {
          var lines = AppCore.Ins._listInforLine?.Where(x=>x.IsEnable==true).ToList();
          var shift_leader = AppCore.Ins._listShiftLeader?.Where(x=>x.IsDelete==false).ToList();
          
          foreach (var item in lines)
          {
            var products = AppCore.Ins._listAllProductsBelongLine?.Where(x => x.LineCode == item.Code).ToList();

            UcOverViewMachine settingUC = new UcOverViewMachine(item);
            settingUC.Size = _Size;
            settingUC.Margin = new Padding(4);
            settingUC.Name = $"Setting{item.Name}";
            settingUC.Tag = item;

            settingUC.ShiftType = AppCore.Ins._listShiftType;
            settingUC.ShiftLeader = shift_leader;

            settingUC.SetShiftLeader();
            settingUC.SetShiftType();
            settingUC.InitEvent();

            settingUC.OnSendChooseLineWeight += SettingUC_OnSendChooseLineWeight;
            flowLayoutPanelLine.Controls.Add(settingUC);
          }
        }
      }
      catch (Exception ex)
      {
        AppCore.Ins.LogErrorToFileLog(ex);
      }
      
    }

    private void SettingUC_OnSendChooseLineWeight(InforLine inforLine)
    {
      FormMain.Instance.OpenPageWeight(inforLine);
    }
  }
}
