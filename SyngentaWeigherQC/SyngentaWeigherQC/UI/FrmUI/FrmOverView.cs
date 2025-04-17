using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.UI.UcUI;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.eUI;

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
      AppCore.Ins.OnSendStatusConnectWeight += Ins_OnSendStatusConnectWeight;
    }

    private void Ins_OnSendStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      UpdateStatusConnectWeight(eStatusConnectWeight);
      AppCore.Ins.ensureLoadUI = true;
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
          var lines = AppCore.Ins._listInforLine?.Where(x => x.IsEnable == true).ToList();
          var shift_leader = AppCore.Ins._listShiftLeader?.Where(x => x.IsDelete == false).ToList();

          foreach (var item in lines)
          {
            if (!item.RequestTare)
            {
              item.DatalogTareCurrent = item.DatalogTares.Where(x=>x.Id == item.LastTareId).FirstOrDefault();
            }

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
        eLoggerHelper.LogErrorToFileLog(ex);
      }

    }

    private void SettingUC_OnSendChooseLineWeight(InforLine inforLine)
    {
      FormMain.Instance.OpenPageWeight(inforLine);
      AppCore.Ins.eStatusModeWeight = eNum.eUI.eStatusModeWeight.WeightForLine;
      AppCore.Ins.inforLineOperation = inforLine;
    }



    public void FindAndUpdateTypeTare(InforLine inforLine)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          if (parametterSimpleUc.Tag == inforLine)
          {
            ((UcOverViewMachine)control).SetInforTare();
          }
        }
      }
    }

    public void UpdateStatusConnectWeight(eStatusConnectWeight eStatusConnectWeight)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;
          ((UcOverViewMachine)control).SetStatusConnectWeight(eStatusConnectWeight);
        }
      }
    }

    public void UpdateTareId(InforLine inforLine, DatalogTare datalogTare)
    {
      foreach (var control in flowLayoutPanelLine.Controls)
      {
        if (control is UcOverViewMachine)
        {
          UcOverViewMachine parametterSimpleUc = (UcOverViewMachine)control;

          var tag = parametterSimpleUc.Tag as InforLine;
          if (tag.Id == inforLine.Id)
          {
            ((UcOverViewMachine)control).UpdateTareCurrent = datalogTare;
          }  
        }
      }
    }




  }
}
